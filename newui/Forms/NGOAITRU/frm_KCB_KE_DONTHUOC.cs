using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using NLog;
using SubSonic;
using VNS.HIS.BusRule.Classes;
using VNS.HIS.DAL;
using VNS.HIS.NGHIEPVU.THUOC;
using VNS.HIS.UI.DANHMUC;
using VNS.HIS.UI.Forms.NGOAITRU;
using VNS.Libs;
using VNS.Properties;

namespace VNS.HIS.UI.NGOAITRU
{
    public partial class frm_KCB_KE_DONTHUOC : Form
    {
        private readonly KCB_KEDONTHUOC _kedonthuoc = new KCB_KEDONTHUOC();
        private readonly Logger log;
        private readonly Dictionary<long, string> lstChangeData = new Dictionary<long, string>();

        private bool APDUNG_GIATHUOC_DOITUONG =
            (Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("APDUNG_GIATHUOC_DOITUONG", "0", true), 0) == 1);
        private bool _allowDrugChanged;
        private bool AllowTextChanged;
        private bool _autoFill;
        private decimal _bhytPtramTraituyennoitru;
        public CallActionKieuKeDon CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
        private bool FilterAgain;
        private bool Giathuoc_quanhe;
        private long IdDonthuoc = -1;
        public string KIEU_THUOC_VT = "THUOC";
        private string LOAIKHOTHUOC = "KHO";
        private bool Manual;
        public short ObjectType_Id = -1;
        private bool Selected;
        private string TEN_BENHPHU = "";
        public KcbChandoanKetluan KcbChandoanKetluan;
        private ActionResult _actionResult = ActionResult.Error;
        private MoneyByLetter _moneyByLetter = new MoneyByLetter();
        private string _rowFilter = "1=1";
        private ActionResult _temp = ActionResult.Success;
        private bool blnHasLoaded;

        private long currentIdthuockho = 0L;
        public int departmentID = -1;
        private DataTable dtStockList;
        public DataTable DtIcd = new DataTable();
        public DataTable dt_ICD_PHU = new DataTable();

        public action em_Action = action.Insert;
        public CallAction em_CallAction = CallAction.FromMenu;
        public bool forced2Add = false;

        private bool hasChanged;
        private bool hasMorethanOne = true;
        public int id_goidv = -1;
        public int id_kham = -1;
        private int id_thuockho = -1;
        private bool isLike = true;
        public bool isLoaded = false;
        private bool isSaved;
        private decimal m_Surcharge;
        public bool m_blnCancel = true;


        private bool m_blnGetDrugCodeFromList;
        private decimal m_decPrice;
        private DataTable m_dtCD_DVD = new DataTable();
        public DataTable m_dtDanhmucthuoc = new DataTable();
        public DataTable m_dtDonthuocChitiet = new DataTable();
        public DataTable m_dtDonthuocChitiet_View = new DataTable();
        private DataTable m_dtqheCamchidinhChungphieu = new DataTable();
        private string madoituong_gia = "DV";
        public int noitru = 0;
        private TDmucKho objDKho;


        private QheDoituongThuoc objectPolicy = null;
        private QheDoituongThuoc objectPolicyTutuc = null;

        private string rowFilter = "1=2";

        private string strSaveandprintPath = (Application.StartupPath + @"\CAUHINH\SaveAndPrintConfigKedonthuoc.txt");
        public byte trong_goi = 0;

        private int tu_tuc;

        public string v_PatientCode = "";
        public int v_Patient_ID = -1;

        public frm_KCB_KE_DONTHUOC(string KIEU_THUOC_VT)
        {
            InitializeComponent();
            log = LogManager.GetLogger("KCB_KEDONTHUOC");
            this.KIEU_THUOC_VT = KIEU_THUOC_VT;
            if (KIEU_THUOC_VT == "VT")
            {
                Text = "KÊ VẬT TƯ";
            }
            else
            {
                Text = "KÊ ĐƠN THUỐC";
            }
            base.KeyPreview = true;
            dtpCreatedDate.Value = dtNgayIn.Value = dtNgayKhamLai.Value = globalVariables.SysDate;
            InitEvents();
            CauHinh();
        }

        public string _Chandoan
        {
            get { return txtChanDoan.Text; }
            set { txtChanDoan._Text = value; }
        }

        public string _MabenhChinh
        {
            get { return txtMaBenhChinh.Text; }
            set { txtMaBenhChinh.Text = value; }
        }

        private int ID_Goi_Dvu { get; set; }

        public string MaDoiTuong { get; set; }


        public KcbLuotkham objLuotkham { get; set; }

        public KcbDangkyKcb objRegExam { get; set; }
        public NoitruPhieudieutri objPhieudieutriNoitru { get; set; }
        public int m_intKieudonthuoc { get; set; }

        public int TrongGoi { get; set; }

        private void AddBenhphu()
        {
            Func<DataRow, bool> predicate = null;
            try
            {
                try
                {
                    if ((txtMaBenhphu.Text.TrimStart(new char[0]).TrimEnd(new char[0]) != "") &&
                        !(txtTenBenhPhu.Text.TrimStart(new char[0]).TrimEnd(new char[0]) == ""))
                    {
                        if (predicate == null)
                        {
                            predicate = benh => Utility.sDbnull(benh[DmucBenh.Columns.MaBenh]) == txtMaBenhphu.Text;
                        }
                        if (!dt_ICD_PHU.AsEnumerable().Where(predicate).Any())
                        {
                            AddMaBenh(txtMaBenhphu.Text, TEN_BENHPHU);
                            txtMaBenhphu.ResetText();
                            txtTenBenhPhu.ResetText();
                            txtMaBenhphu.Focus();
                            txtMaBenhphu.SelectAll();
                            Selected = false;
                        }
                        else
                        {
                            txtMaBenhphu.ResetText();
                            txtTenBenhPhu.ResetText();
                            txtMaBenhphu.Focus();
                            txtMaBenhphu.SelectAll();
                        }
                    }
                }
                catch (Exception)
                {
                    Utility.ShowMsg("Có lỗi trong quá trình thêm thông tin vào lưới");
                }
            }
            finally
            {
            }
        }

        private void AddMaBenh(string MaBenh, string TenBenh)
        {
            Func<DataRow, bool> predicate = null;
            if (
                !dt_ICD_PHU.AsEnumerable()
                    .Where(benh => (Utility.sDbnull(benh[DmucBenh.Columns.MaBenh]) == MaBenh))
                    .Any())
            {
                DataRow row = dt_ICD_PHU.NewRow();
                row[DmucBenh.Columns.MaBenh] = MaBenh;
                if (predicate == null)
                {
                    predicate = benh => Utility.sDbnull(benh[DmucBenh.Columns.MaBenh]) == MaBenh;
                }
                EnumerableRowCollection<string> source =
                    globalVariables.gv_dtDmucBenh.AsEnumerable()
                        .Where(predicate)
                        .Select(benh => Utility.sDbnull(benh[DmucBenh.Columns.TenBenh]));
                if (source.Any())
                {
                    row[DmucBenh.Columns.TenBenh] = Utility.sDbnull(source.FirstOrDefault());
                }
                dt_ICD_PHU.Rows.Add(row);
                dt_ICD_PHU.AcceptChanges();
                grd_ICD.AutoSizeColumns();
            }
        }

        private string KiemtraCamchidinhchungphieu(int id_thuoc, string ten_chitiet)
        {
            string _reval = "";
            string _tempt = "";
            var lstKey = new List<string>();
            string _key = "";
            //Kiểm tra dịch vụ đang thêm có phải là dạng Single-Service hay không?
            DataRow[] _arrSingle =
                m_dtDanhmucthuoc.Select(DmucThuoc.Columns.SingleService + "=1 AND " + DmucThuoc.Columns.IdThuoc + "=" +
                                        id_thuoc);
            if (_arrSingle.Length > 0 &&
                m_dtDonthuocChitiet.Select(KcbDonthuocChitiet.Columns.IdThuoc + "<>" + id_thuoc).Length > 0)
            {
                return string.Format("Single-Service: {0}", ten_chitiet);
            }
            //Kiểm tra các dịch vụ đã thêm có cái nào là Single-Service hay không?
            List<int> lstID =
                m_dtDonthuocChitiet.AsEnumerable()
                    .Select(c => Utility.Int32Dbnull(c[KcbDonthuocChitiet.Columns.IdThuoc], 0))
                    .Distinct()
                    .ToList();
            EnumerableRowCollection<DataRow> q = from p in m_dtDanhmucthuoc.AsEnumerable()
                where Utility.ByteDbnull(p[DmucThuoc.Columns.SingleService], 0) == 1
                      && lstID.Contains(Utility.Int32Dbnull(p[DmucThuoc.Columns.IdThuoc], 0))
                select p;
            if (q.Any())
            {
                return string.Format("Single-Service: {0}",
                    Utility.sDbnull(q.FirstOrDefault()[DmucThuoc.Columns.TenThuoc], ""));
            }
            //Lấy các cặp cấm chỉ định chung cùng nhau
            DataRow[] arrDr =
                m_dtqheCamchidinhChungphieu.Select(QheCamchidinhChungphieu.Columns.IdDichvu + "=" + id_thuoc);
            DataRow[] arrDr1 =
                m_dtqheCamchidinhChungphieu.Select(QheCamchidinhChungphieu.Columns.IdDichvuCamchidinhchung + "=" +
                                                   id_thuoc);
            foreach (DataRow dr in arrDr)
            {
                DataRow[] arrtemp =
                    m_dtDonthuocChitiet.Select(KcbDonthuocChitiet.Columns.IdThuoc + "=" +
                                               Utility.sDbnull(
                                                   dr[QheCamchidinhChungphieu.Columns.IdDichvuCamchidinhchung]));
                if (arrtemp.Length > 0)
                {
                    foreach (DataRow dr1 in arrtemp)
                    {
                        _tempt = string.Empty;
                        _key = id_thuoc + "-" + Utility.sDbnull(dr1[KcbDonthuocChitiet.Columns.IdThuoc], "");
                        if (!lstKey.Contains(_key))
                        {
                            lstKey.Add(_key);
                            _tempt = string.Format("{0} - {1}", ten_chitiet,
                                Utility.sDbnull(dr1[DmucThuoc.Columns.IdThuoc], ""));
                        }
                        if (_tempt != string.Empty)
                            _reval += _tempt + "\n";
                    }
                }
            }
            foreach (DataRow dr in arrDr1)
            {
                DataRow[] arrtemp =
                    m_dtDonthuocChitiet.Select(KcbDonthuocChitiet.Columns.IdThuoc + "=" +
                                               Utility.sDbnull(dr[QheCamchidinhChungphieu.Columns.IdDichvu]));
                if (arrtemp.Length > 0)
                {
                    foreach (DataRow dr1 in arrtemp)
                    {
                        _tempt = string.Empty;
                        _key = id_thuoc + "-" + Utility.sDbnull(dr1[KcbDonthuocChitiet.Columns.IdThuoc], "");
                        if (!lstKey.Contains(_key))
                        {
                            lstKey.Add(_key);
                            _tempt = string.Format("{0} - {1}", ten_chitiet,
                                Utility.sDbnull(dr1[DmucThuoc.Columns.TenThuoc], ""));
                        }
                        if (_tempt != string.Empty)
                            _reval += _tempt + "\n";
                    }
                }
            }
            return _reval;
        }

        private void AddPreDetail()
        {
            try
            {
                if (Utility.Int32Dbnull(txtDrugID.Text, -1) <= 0) return;
                string errMsg = string.Empty;
                string errMsg_temp = string.Empty;
                setMsg(lblMsg, "", false);
                tu_tuc = chkTutuc.Checked ? 1 : 0;
                if (objDKho == null)
                {
                    setMsg(lblMsg, "Bạn cần chọn kho thuốc trước khi chọn thuốc kê đơn", true);
                    cboStock.Focus();
                }
                else if (Utility.Int32Dbnull(txtDrugID.Text) < 0)
                {
                    txtdrug.Focus();
                    txtdrug.SelectAll();
                }
                else if ((noitru == 0 && Utility.DecimaltoDbnull(txtSoluong.Text, 0) <= 0) ||
                         (noitru == 1 && Utility.DecimaltoDbnull(txtSoluong.Text, 0) <= 0 &&
                          Utility.Int32Dbnull(txtDonvichiaBut.Text, 0) <= 0))
                {
                    setMsg(lblMsg, "Số lượng " + (KIEU_THUOC_VT == "THUOC" ? "thuốc" : "vật tư") + " phải lớn hơn 0",
                        true);
                    txtSoluong.Focus();
                }
                else if (Utility.Int32Dbnull(txtGioihanke.Text, -1) > 0 &&
                         Utility.DecimaltoDbnull(txtSoluong.Text, 0) > Utility.Int32Dbnull(txtGioihanke.Text, -1))
                {
                    setMsg(lblMsg,
                        "Thuốc đã đặt giới hạn kê tối đa 1 lần nhỏ hơn hoặc bằng " +
                        Utility.Int32Dbnull(txtGioihanke.Text, 0) + " " + txtDonViTinh.Text, true);
                    txtSoluong.Focus();
                }
                else
                {
                    log.Trace(
                        "Bat dau them chi tiet don thuoc.......................................................................................");
                    if (Utility.Int32Dbnull(objDKho.KtraTon) == 1)
                    {
                        decimal num = CommonLoadDuoc.SoLuongTonTrongKho(-1L,
                            Utility.Int32Dbnull(cboStock.SelectedValue), Utility.Int32Dbnull(txtDrugID.Text, -1),
                            txtdrug.GridView ? id_thuockho : txtdrug.id_thuockho,
                            Utility.Int32Dbnull(
                                THU_VIEN_CHUNG.Laygiatrithamsohethong("KIEMTRATHUOC_CHOXACNHAN", "1", false), 1),
                            Utility.ByteDbnull(objLuotkham.Noitru, 0));
                        log.Trace("1. Lay xong so luong ton kho ke don");
                        if (Utility.DecimaltoDbnull(txtSoluong.Text, 0) > num)
                        {
                            Utility.ShowMsg(
                                string.Format(
                                    "Số lượng " + (KIEU_THUOC_VT == "THUOC" ? "thuốc" : "vật tư") +
                                    " cấp phát {0} vượt quá số lượng " +
                                    (KIEU_THUOC_VT == "THUOC" ? "thuốc" : "vật tư") +
                                    " trong kho {1}.\nCó thể trong lúc bạn chọn " +
                                    (KIEU_THUOC_VT == "THUOC" ? "thuốc" : "vật tư") +
                                    " chưa kịp đưa vào đơn, các Bác sĩ khác hoặc Dược sĩ đã kê hoặc cấp phát mất một lượng " +
                                    (KIEU_THUOC_VT == "THUOC" ? "thuốc" : "vật tư") +
                                    " so với thời điểm bạn chọn.\nMời bạn liên hệ phòng Dược kiểm tra lại",
                                    txtSoluong.Text, num), "Cảnh báo", MessageBoxIcon.Hand);
                            txtSoluong.Focus();
                            return;
                        }
                    }
                    DataTable listdata =
                        new XuatThuoc().GetObjThuocKhoCollection(
                            Utility.Int32Dbnull(cboStock.SelectedValue, 0),
                            Utility.Int32Dbnull(txtDrugID.Text, -1),
                            txtdrug.GridView ? id_thuockho : txtdrug.id_thuockho,
                            (decimal) Utility.DecimaltoDbnull(txtSoluong.Text, 0),
                            Utility.ByteDbnull(objLuotkham.IdLoaidoituongKcb.Value, 0),
                            Utility.ByteDbnull(objLuotkham.DungTuyen.Value, 0), (byte) noitru);
                    var list2 = new List<KcbDonthuocChitiet>();
                    foreach (DataRow thuockho in listdata.Rows)
                    {
                        decimal _soluong = Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.SoLuong], 0);
                        if (_soluong > 0)
                        {
                            DataRow[] rowArray =
                                m_dtDonthuocChitiet.Select(TThuockho.Columns.IdThuockho + "=" +
                                                           Utility.sDbnull(
                                                               thuockho[TThuockho.Columns.IdThuockho]) +
                                                           " AND tu_tuc=" + (chkTutuc.Checked ? 1 : tu_tuc));
                            if (rowArray.Length > 0)
                            {
                                rowArray[0][KcbDonthuocChitiet.Columns.SoLuong] =
                                    Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) +
                                    _soluong;
                                rowArray[0]["TT_KHONG_PHUTHU"] =
                                    Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                    Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.DonGia]);
                                rowArray[0]["TT"] =
                                    Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                    (Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.DonGia]) +
                                     Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu]));
                                rowArray[0]["TT_BHYT"] =
                                    Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                    Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BhytChitra]);
                                rowArray[0]["TT_BN"] =
                                    Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                    (Utility.DecimaltoDbnull(
                                        rowArray[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0) +
                                     Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu], 0));
                                rowArray[0]["TT_PHUTHU"] =
                                    Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                    Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu], 0);
                                rowArray[0]["TT_BN_KHONG_PHUTHU"] =
                                    Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                    Utility.DecimaltoDbnull(
                                        rowArray[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0);
                                AddtoView(rowArray[0], _soluong);
                                list2.Add(getNewItem(rowArray[0]));
                            }
                            else
                            {
                                DataRow row = m_dtDonthuocChitiet.NewRow();
                                row[DmucThuoc.Columns.TenThuoc] = Utility.sDbnull(txtDrug_Name.Text, "");
                                row[KcbDonthuocChitiet.Columns.SoLuong] = _soluong;

                                row[KcbDonthuocChitiet.Columns.PhuThu] =
                                    Utility.DecimaltoDbnull(thuockho["phu_thu"], 0);
                                ;
                                    // !this.Giathuoc_quanhe ? 0M : Utility.DecimaltoDbnull(this.txtSurcharge.Text, 0);
                                row[KcbDonthuocChitiet.Columns.PhuthuDungtuyen] =
                                    Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.PhuthuDungtuyen], 0);
                                row[KcbDonthuocChitiet.Columns.PhuthuTraituyen] =
                                    Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.PhuthuTraituyen], 0);

                                row[KcbDonthuocChitiet.Columns.IdThuoc] = Utility.Int32Dbnull(txtDrugID.Text, -1);
                                row[KcbDonthuocChitiet.Columns.IdDonthuoc] = IdDonthuoc;
                                row["IsNew"] = 1;
                                row[KcbDonthuocChitiet.Columns.MadoituongGia] = madoituong_gia;
                                row[KcbDonthuocChitiet.Columns.IdThuockho] =
                                    Utility.Int64Dbnull(thuockho[TThuockho.Columns.IdThuockho], -1);
                                row[KcbDonthuocChitiet.Columns.GiaNhap] =
                                    Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.GiaNhap], 0);
                                row[KcbDonthuocChitiet.Columns.GiaBan] =
                                    Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.GiaBan], 0);
                                row[KcbDonthuocChitiet.Columns.GiaBhyt] =
                                    Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.GiaBhyt], 0);
                                row[KcbDonthuocChitiet.Columns.Vat] =
                                    Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.Vat], 0);
                                row[KcbDonthuocChitiet.Columns.SoLo] =
                                    Utility.sDbnull(thuockho[TThuockho.Columns.SoLo], "");
                                row[KcbDonthuocChitiet.Columns.SoDky] =
                                    Utility.sDbnull(thuockho[TThuockho.Columns.SoDky], "");
                                row[KcbDonthuocChitiet.Columns.SoQdinhthau] =
                                    Utility.sDbnull(thuockho[TThuockho.Columns.SoQdinhthau], "");
                                row[KcbDonthuocChitiet.Columns.MaNhacungcap] =
                                    Utility.sDbnull(thuockho[TThuockho.Columns.MaNhacungcap], "");
                                row["ten_donvitinh"] = txtDonViTinh.Text;
                                row["sNgay_hethan"] = Utility.sDbnull(thuockho["sNgay_hethan"], "");
                                row["sNgay_nhap"] = Utility.sDbnull(thuockho["sNgay_nhap"], "");
                                row[KcbDonthuocChitiet.Columns.NgayHethan] =
                                    thuockho[TThuockho.Columns.NgayHethan];
                                row[KcbDonthuocChitiet.Columns.NgayNhap] = thuockho[TThuockho.Columns.NgayNhap];
                                row[KcbDonthuocChitiet.Columns.IdKho] =
                                    Utility.Int32Dbnull(cboStock.SelectedValue, -1);
                                row[TDmucKho.Columns.TenKho] = Utility.sDbnull(cboStock.Text, -1);
                                row[KcbDonthuocChitiet.Columns.DonviTinh] = txtDonViTinh.Text;
                                row[DmucThuoc.Columns.HoatChat] = txtBietduoc.Text;
                                row[KcbDonthuocChitiet.Columns.ChidanThem] = txtChiDanThem.Text;
                                row[KcbDonthuocChitiet.Columns.MotaThem] =
                                    Utility.sDbnull(txtChiDanDungThuoc.Text);
                                row["mota_them_chitiet"] = Utility.sDbnull(txtChiDanDungThuoc.Text);
                                row[KcbDonthuocChitiet.Columns.CachDung] = Utility.sDbnull(txtCachDung.Text);
                                row[KcbDonthuocChitiet.Columns.SoluongDung] =
                                    Utility.sDbnull(txtSoLuongDung.Text);
                                row[KcbDonthuocChitiet.Columns.SolanDung] = Utility.sDbnull(txtSolan.Text);
                                row["ma_loaithuoc"] = txtdrugtypeCode.Text;
                                row["ten_loaithuoc"] = txttenloaithuoc.Text;
                                row[KcbDonthuocChitiet.Columns.TrangthaiThanhtoan] = 0;
                                row[KcbDonthuocChitiet.Columns.SttIn] = GetMaxSTT(m_dtDonthuocChitiet);
                                row[KcbDonthuocChitiet.Columns.TuTuc] = chkTutuc.Checked ? 1 : tu_tuc;
                                row[KcbDonthuocChitiet.Columns.DonGia] =
                                    Utility.DecimaltoDbnull(thuockho["GIA_BAN"], 0);
                                // (this.APDUNG_GIATHUOC_DOITUONG || this.Giathuoc_quanhe) ?
                                // (Utility.DecimaltoDbnull(this.txtPrice.Text, 0)) : 
                                // (this.objLuotkham.IdLoaidoituongKcb== 1 ? 
                                //Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.GiaBan],0) : Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.GiaBhyt],0));
                                row[KcbDonthuocChitiet.Columns.PtramBhyt] = objLuotkham.PtramBhyt;
                                row[KcbDonthuocChitiet.Columns.PtramBhytGoc] = objLuotkham.PtramBhytGoc;
                                row[KcbDonthuocChitiet.Columns.MaDoituongKcb] = MaDoiTuong;
                                row[KcbDonthuocChitiet.Columns.KieuBiendong] = thuockho["kieubiendong"];
                                if (em_CallAction == CallAction.FromMenu)
                                {
                                    if (tu_tuc == 0)
                                    {
                                        decimal BHCT = 0m;
                                        if (objLuotkham.DungTuyen == 1)
                                        {
                                            BHCT =
                                                Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia],
                                                    0)*(Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0)/100);
                                        }
                                        else
                                        {
                                            if (objLuotkham.TrangthaiNoitru <= 0)
                                                BHCT =
                                                    Utility.DecimaltoDbnull(
                                                        row[KcbDonthuocChitiet.Columns.DonGia], 0)*
                                                    (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0)/100);
                                            else //Nội trú cần tính=đơn giá * % đầu thẻ * % tuyến
                                                BHCT =
                                                    Utility.DecimaltoDbnull(
                                                        row[KcbDonthuocChitiet.Columns.DonGia], 0)*
                                                    (Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0)/100)*
                                                    (_bhytPtramTraituyennoitru/100);
                                        }
                                        // decimal num2 = (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0) * Utility.DecimaltoDbnull(this.objLuotkham.PtramBhyt, 0)) / 100M;
                                        decimal num3 =
                                            Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0) -
                                            BHCT;
                                        row[KcbDonthuocChitiet.Columns.BhytChitra] = BHCT;
                                        row[KcbDonthuocChitiet.Columns.BnhanChitra] = num3;
                                    }
                                    else
                                    {
                                        row[KcbDonthuocChitiet.Columns.PtramBhyt] = 0;
                                        row[KcbDonthuocChitiet.Columns.BhytChitra] = 0;
                                        row[KcbDonthuocChitiet.Columns.BnhanChitra] =
                                            Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0);
                                    }
                                }
                                row["TT_KHONG_PHUTHU"] =
                                    Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) *
                                    Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia]);
                                row["TT"] = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) *
                                            (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia]) +
                                             Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu]));
                                row["TT_BHYT"] = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) *
                                                 Utility.DecimaltoDbnull(
                                                     row[KcbDonthuocChitiet.Columns.BhytChitra]);
                                row["TT_BN"] = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) *
                                               (Utility.DecimaltoDbnull(
                                                   row[KcbDonthuocChitiet.Columns.BnhanChitra], 0) +
                                                Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu],
                                                    0));
                                row["TT_PHUTHU"] = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) *
                                                   Utility.DecimaltoDbnull(
                                                       row[KcbDonthuocChitiet.Columns.PhuThu], 0);
                                row["TT_BN_KHONG_PHUTHU"] =
                                    Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) *
                                    Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BnhanChitra], 0);

                                errMsg_temp =
                                    KiemtraCamchidinhchungphieu(
                                        Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.IdThuoc], 0),
                                        Utility.sDbnull(row[DmucThuoc.Columns.TenThuoc], ""));
                                log.Trace("3. Đã kiểm tra xong cấm chỉ định chung đơn thuốc");
                                if (errMsg_temp != string.Empty)
                                {
                                    errMsg += errMsg_temp;
                                }
                                else
                                {
                                    m_dtDonthuocChitiet.Rows.Add(row);
                                    AddtoView(row, _soluong);
                                    list2.Add(getNewItem(row));
                                }
                            }
                            ClearControl();
                            txtdrug.Focus();
                            txtdrug.SelectAll();
                        }
                    }
                    if (errMsg != string.Empty)
                    {
                        if (errMsg.Contains("Single-Service:"))
                        {
                            Utility.ShowMsg(
                                "Thuốc sau được đánh dấu không được phép kê chung đơn bất kỳ Thuốc nào. Đề nghị bạn kiểm tra lại:\n" +
                                Utility.DoTrim(errMsg.Replace("Single-Service:", "")));
                        }
                        else
                            Utility.ShowMsg(
                                "Các cặp Thuốc sau đã được thiết lập chống kê chung đơn. Đề nghị bạn kiểm tra lại:\n" +
                                errMsg);
                    }
                    else
                    {
                        PerformAction(list2.ToArray());
                        Utility.GotoNewRowJanus(grdPresDetail, KcbDonthuocChitiet.Columns.IdThuoc,
                            txtDrugID.Text);
                        UpdateDataWhenChanged();
                    }
                    //this.ClearControl();
                    //this.txtdrug.Focus();
                    //this.txtdrug.SelectAll();
                    m_dtDanhmucthuoc.DefaultView.RowFilter = "1=2";
                    m_dtDanhmucthuoc.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
                //Utility.CatchException(ex);
            }
            finally
            {
                log.Trace(
                    "KẾT THÚC THÊM CHI TIẾT THUỐC.......................................................................................");
            }
        }

        private void AddQuantity(int id_thuoc, int id_thuockho, decimal newQuantity)
        {
            try
            {
                tu_tuc = chkTutuc.Checked ? 1 : 0;
                setMsg(lblMsg, "", false);
                if (Utility.Int32Dbnull(objDKho.KtraTon) == 1)
                {
                    decimal num = CommonLoadDuoc.SoLuongTonTrongKho(-1L, Utility.Int32Dbnull(cboStock.SelectedValue),
                        id_thuoc, id_thuockho,
                        Utility.Int32Dbnull(
                            THU_VIEN_CHUNG.Laygiatrithamsohethong("KIEMTRATHUOC_CHOXACNHAN", "1", false), 1),
                        Utility.ByteDbnull(objLuotkham.Noitru, 0));
                    if (newQuantity > num)
                    {
                        Utility.ShowMsg(
                            "Số lượng " + (KIEU_THUOC_VT == "THUOC" ? "thuốc" : "vật tư") +
                            " cấp phát vượt quá số lượng " + (KIEU_THUOC_VT == "THUOC" ? "thuốc" : "vật tư") +
                            " trong kho. Mời bạn kiểm tra lại", "Cảnh báo", MessageBoxIcon.Hand);
                        txtSoluong.Focus();
                        return;
                    }
                }
                DataTable listdata =
                    new XuatThuoc().GetObjThuocKhoCollection(Utility.Int32Dbnull(cboStock.SelectedValue, 0), id_thuoc,
                        id_thuockho, newQuantity, Utility.ByteDbnull(objLuotkham.IdLoaidoituongKcb.Value, 0),
                        Utility.ByteDbnull(objLuotkham.DungTuyen.Value, 0), (byte) noitru);
                var list2 = new List<KcbDonthuocChitiet>();
                foreach (DataRow thuockho in listdata.Rows)
                {
                    decimal _soluong = Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.SoLuong], 0);
                    if (_soluong > 0)
                    {
                        DataRow[] rowArray =
                            m_dtDonthuocChitiet.Select(TThuockho.Columns.IdThuockho + "=" +
                                                       Utility.sDbnull(thuockho[TThuockho.Columns.IdThuockho]) +
                                                       " AND tu_tuc=" + (chkTutuc.Checked ? 1 : tu_tuc));
                        if (rowArray.Length > 0)
                        {
                            rowArray[0][KcbDonthuocChitiet.Columns.SoLuong] =
                                Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) + _soluong;
                            newQuantity -= _soluong;
                            rowArray[0]["TT_KHONG_PHUTHU"] =
                                Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.DonGia]);
                            rowArray[0]["TT"] = Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                                (Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.DonGia]) +
                                                 Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu]));
                            rowArray[0]["TT_BHYT"] =
                                Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BhytChitra]);
                            rowArray[0]["TT_BN"] = Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                                   (Utility.DecimaltoDbnull(
                                                       rowArray[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0) +
                                                    Utility.DecimaltoDbnull(
                                                        rowArray[0][KcbDonthuocChitiet.Columns.PhuThu], 0));
                            rowArray[0]["TT_PHUTHU"] =
                                Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu], 0);
                            rowArray[0]["TT_BN_KHONG_PHUTHU"] =
                                Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0);
                            AddtoView(rowArray[0], _soluong);
                            list2.Add(getNewItem(rowArray[0]));
                        }
                        else
                        {
                            byte? nullable;
                            DataRow row = m_dtDonthuocChitiet.NewRow();
                            string donviTinh = "";
                            string chidanThem = "";
                            string motaThem = "";
                            string cachDung = "";
                            string soluongDung = "";
                            string solanDung = "";
                            string tenthuoc = "";
                            string str8 = "";
                            string hoatchat = "";
                            getInfor(id_thuoc, ref tenthuoc, ref str8, ref hoatchat, ref donviTinh, ref chidanThem,
                                ref motaThem, ref cachDung, ref soluongDung, ref solanDung);
                            txtDrugID.Text = id_thuoc.ToString();
                            txtDrugID_TextChanged(txtDrugID, new EventArgs());
                            row[DmucThuoc.Columns.TenThuoc] = Utility.sDbnull(txtDrug_Name.Text, "");
                            row[KcbDonthuocChitiet.Columns.SoLuong] = _soluong;
                            row[KcbDonthuocChitiet.Columns.PhuThu] = Utility.DecimaltoDbnull(thuockho["phu_thu"], 0);
                                // !this.Giathuoc_quanhe ? 0M : Utility.DecimaltoDbnull(this.txtSurcharge.Text, 0);
                            row[KcbDonthuocChitiet.Columns.PhuthuDungtuyen] =
                                Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.PhuthuDungtuyen], 0);
                            row[KcbDonthuocChitiet.Columns.PhuthuTraituyen] =
                                Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.PhuthuTraituyen], 0);
                            row[KcbDonthuocChitiet.Columns.IdThuoc] = id_thuoc;
                            row[KcbDonthuocChitiet.Columns.IdDonthuoc] = IdDonthuoc;
                            row["IsNew"] = 1;
                            row[KcbDonthuocChitiet.Columns.MadoituongGia] = madoituong_gia;
                            row[KcbDonthuocChitiet.Columns.IdThuockho] =
                                Utility.Int64Dbnull(thuockho[TThuockho.Columns.IdThuockho], 0);
                            row[KcbDonthuocChitiet.Columns.GiaNhap] =
                                Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.GiaNhap], 0);
                            row[KcbDonthuocChitiet.Columns.GiaBan] =
                                Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.GiaBan], 0);
                            row[KcbDonthuocChitiet.Columns.GiaBhyt] =
                                Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.GiaBhyt], 0);
                            row[KcbDonthuocChitiet.Columns.Vat] =
                                Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.Vat], 0);
                            row[KcbDonthuocChitiet.Columns.SoLo] = Utility.sDbnull(thuockho[TThuockho.Columns.SoLo], "");
                            row[KcbDonthuocChitiet.Columns.SoDky] = Utility.sDbnull(thuockho[TThuockho.Columns.SoDky],
                                "");
                            row[KcbDonthuocChitiet.Columns.SoQdinhthau] =
                                Utility.sDbnull(thuockho[TThuockho.Columns.SoQdinhthau], "");
                            row[KcbDonthuocChitiet.Columns.MaNhacungcap] =
                                Utility.sDbnull(thuockho[TThuockho.Columns.MaNhacungcap], "");
                            row["ten_donvitinh"] = txtDonViTinh.Text;
                            row["sNgay_hethan"] = Utility.sDbnull(thuockho["sNgay_hethan"]);
                            row["sNgay_nhap"] = Utility.sDbnull(thuockho["sNgay_nhap"], "");
                            row[KcbDonthuocChitiet.Columns.NgayHethan] = thuockho[TThuockho.Columns.NgayHethan];
                            row[KcbDonthuocChitiet.Columns.NgayNhap] = thuockho[TThuockho.Columns.NgayNhap];
                            row[KcbDonthuocChitiet.Columns.IdKho] = Utility.Int32Dbnull(cboStock.SelectedValue, -1);
                            row[TDmucKho.Columns.TenKho] = Utility.sDbnull(cboStock.Text, -1);
                            row[KcbDonthuocChitiet.Columns.DonviTinh] = donviTinh;
                            row[DmucThuoc.Columns.HoatChat] = hoatchat;
                            row[KcbDonthuocChitiet.Columns.ChidanThem] = chidanThem;
                            row["mota_them_chitiet"] = chidanThem;
                            row[KcbDonthuocChitiet.Columns.MotaThem] = motaThem;
                            row[KcbDonthuocChitiet.Columns.CachDung] = cachDung;
                            row[KcbDonthuocChitiet.Columns.SoluongDung] = soluongDung;
                            row[KcbDonthuocChitiet.Columns.SolanDung] = solanDung;
                            row["ma_loaithuoc"] = txtdrugtypeCode.Text;
                            row["ten_loaithuoc"] = txttenloaithuoc.Text;
                            row[KcbDonthuocChitiet.Columns.TrangthaiThanhtoan] = 0;
                            row[KcbDonthuocChitiet.Columns.SttIn] = GetMaxSTT(m_dtDonthuocChitiet);
                            row[KcbDonthuocChitiet.Columns.TuTuc] = chkTutuc.Checked ? 1 : tu_tuc;
                            row[KcbDonthuocChitiet.Columns.DonGia] = Utility.DecimaltoDbnull(thuockho["GIA_BAN"], 0);
                            ;
                                // (this.APDUNG_GIATHUOC_DOITUONG || this.Giathuoc_quanhe) ? new decimal?(Utility.DecimaltoDbnull(this.txtPrice.Text, 0)) : ((((nullable = this.objLuotkham.IdLoaidoituongKcb).GetValueOrDefault() == 1) && nullable.HasValue) ? new decimal?(Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.GiaBan], 0)) : Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.GiaBhyt], 0));
                            row[KcbDonthuocChitiet.Columns.PtramBhyt] = objLuotkham.PtramBhyt;
                            row[KcbDonthuocChitiet.Columns.PtramBhytGoc] = objLuotkham.PtramBhytGoc;
                            row[KcbDonthuocChitiet.Columns.MaDoituongKcb] = MaDoiTuong;
                            row[KcbDonthuocChitiet.Columns.KieuBiendong] = thuockho["kieubiendong"];
                            if (em_CallAction == CallAction.FromMenu)
                            {
                                if (tu_tuc == 0)
                                {
                                    decimal BHCT = 0m;
                                    if (objLuotkham.DungTuyen == 1)
                                    {
                                        BHCT = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0)*
                                               (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0)/100);
                                    }
                                    else
                                    {
                                        if (objLuotkham.TrangthaiNoitru <= 0)
                                            BHCT = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0)*
                                                   (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0)/100);
                                        else //Nội trú cần tính=đơn giá * % đầu thẻ * % tuyến
                                            BHCT = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0)*
                                                   (Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0)/100)*
                                                   (_bhytPtramTraituyennoitru/100);
                                    }
                                    //decimal num2 = (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0) * Utility.DecimaltoDbnull(this.objLuotkham.PtramBhyt, 0)) / 100M;
                                    decimal num3 = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0) -
                                                   BHCT;
                                    row[KcbDonthuocChitiet.Columns.BhytChitra] = BHCT;
                                    row[KcbDonthuocChitiet.Columns.BnhanChitra] = num3;
                                }
                                else
                                {
                                    row[KcbDonthuocChitiet.Columns.PtramBhyt] = 0;

                                    row[KcbDonthuocChitiet.Columns.BhytChitra] = 0;
                                    row[KcbDonthuocChitiet.Columns.BnhanChitra] =
                                        Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0);
                                }
                            }
                            row["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong])*
                                                     Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia]);
                            row["TT"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong])*
                                        (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia]) +
                                         Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu]));
                            row["TT_BHYT"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong])*
                                             Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BhytChitra]);
                            row["TT_BN"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong])*
                                           (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BnhanChitra], 0) +
                                            Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu], 0));
                            row["TT_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong])*
                                               Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu], 0);
                            row["TT_BN_KHONG_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong])*
                                                        Utility.DecimaltoDbnull(
                                                            row[KcbDonthuocChitiet.Columns.BnhanChitra], 0);
                            m_dtDonthuocChitiet.Rows.Add(row);
                            decimal num4 = newQuantity - _soluong;
                            AddtoView(row, (num4 > 0) ? _soluong : newQuantity);
                            list2.Add(getNewItem(row));
                        }
                    }
                }
                PerformAction(list2.ToArray());
                Utility.GotoNewRowJanus(grdPresDetail, KcbDonthuocChitiet.Columns.IdThuoc, txtDrugID.Text);
                UpdateDataWhenChanged();
                ClearControl();
                txtdrug.Focus();
                txtdrug.SelectAll();
                m_dtDanhmucthuoc.DefaultView.RowFilter = "1=2";
                m_dtDanhmucthuoc.AcceptChanges();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void AddtoView(DataRow newDr, decimal newQuantity)
        {
            string filter = KcbDonthuocChitiet.Columns.IdThuoc + "=" +
                                                Utility.sDbnull(newDr[KcbDonthuocChitiet.Columns.IdThuoc], "-1") +
                                                " AND " + KcbDonthuocChitiet.Columns.DonGia + "=" +
                                                Utility.sDbnull(newDr[KcbDonthuocChitiet.Columns.DonGia], "-1")
                                                + " AND PHU_THU=" +
                                                Utility.sDbnull(newDr[KcbDonthuocChitiet.Columns.PhuThu], "-1")
                                                + " AND tu_tuc=" +
                                                Utility.sDbnull(newDr[KcbDonthuocChitiet.Columns.TuTuc], "-1");
            DataRow[] rowArray =
                m_dtDonthuocChitiet_View.Select(filter);
            if (rowArray.Length <= 0)
            {
                m_dtDonthuocChitiet_View.ImportRow(newDr);
            }
            else
            {
                rowArray[0][KcbDonthuocChitiet.Columns.SoLuong] =
                    Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong], 0) + newQuantity;
                rowArray[0]["TT_KHONG_PHUTHU"] = Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                                 Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.DonGia]);
                rowArray[0]["TT"] = Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                    (Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.DonGia]) +
                                     Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu]));
                rowArray[0]["TT_BHYT"] = Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                         Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BhytChitra]);
                rowArray[0]["TT_BN"] = Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                       (Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0) +
                                        Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu], 0));
                rowArray[0]["TT_PHUTHU"] = Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                           Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu], 0);
                rowArray[0]["TT_BN_KHONG_PHUTHU"] =
                    Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                    Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0);
                rowArray[0][KcbDonthuocChitiet.Columns.SttIn] =
                    Math.Min(Utility.Int32Dbnull(newDr[KcbDonthuocChitiet.Columns.SttIn], 0),
                        Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SttIn], 0));
                m_dtDonthuocChitiet_View.AcceptChanges();
            }
        }

        private void AutoCompleteDmucChung()
        {
            try
            {
                try
                {
                    var lstLoai = new List<string> {"CDDT"};
                    DataTable source = THU_VIEN_CHUNG.LayDulieuDanhmucChung(lstLoai, true);
                    if (source != null)
                    {
                        if (!source.Columns.Contains("ShortCut"))
                        {
                            source.Columns.Add(new DataColumn("ShortCut", typeof (string)));
                        }
                        foreach (DataRow row in source.Rows)
                        {
                            string str = "";
                            string str2 = row["TEN"].ToString().Trim() + " " +
                                          Utility.Bodau(row["TEN"].ToString().Trim());
                            str = row["MA"].ToString().Trim();
                            string[] strArray = str2.ToLower().Split(new[] {' '});
                            string str3 = "";
                            foreach (string str5 in strArray)
                            {
                                if (str5.Trim() != "")
                                {
                                    str3 = str3 + str5 + " ";
                                }
                            }
                            str = str + str3;
                            foreach (string str5 in strArray)
                            {
                                if (str5.Trim() != "")
                                {
                                    str = str + str5.Substring(0, 1);
                                }
                            }
                            row["ShortCut"] = str;
                        }
                        var list = new List<string>();
                        list =
                            source.AsEnumerable()
                                .Where(p => (p.Field<string>("LOAI").ToString() == "CDDT"))
                                .Select(
                                    p =>
                                        ("-1#" + p.Field<string>("MA").ToString() + "@" +
                                         p.Field<string>("TEN").ToString() + "@" +
                                         p.Field<string>("shortcut").ToString()))
                                .ToList<string>();
                        txtCachDung.AutoCompleteList = list;
                        txtCachDung.TextAlign = HorizontalAlignment.Center;
                        txtCachDung.CaseSensitive = false;
                        txtCachDung.MinTypedCharacters = 1;
                    }
                }
                catch
                {
                }
            }
            finally
            {
            }
        }

        private void AutoloadSaveAndPrintConfig()
        {
            try
            {
                AllowTextChanged = false;
                PropertyLib._MayInProperties.InDonthuocsaukhiluu = chkSaveAndPrint.Checked;
                PropertyLib.SaveProperty(PropertyLib._MayInProperties);
            }
            catch
            {
            }
            finally
            {
                AllowTextChanged = true;
            }
        }

        private void LaydanhsachBSKedon()
        {
            try
            {
            //    DataTable data = THU_VIEN_CHUNG.LaydanhsachBacsi(departmentID, noitru);
                txtBacsi.Init(globalVariables.gv_dtDmucNhanvien,
                    new List<string>
                    {
                        DmucNhanvien.Columns.IdNhanvien,
                        DmucNhanvien.Columns.MaNhanvien,
                        DmucNhanvien.Columns.TenNhanvien
                    });
                if (globalVariables.gv_intIDNhanvien <= 0)
                {
                    txtBacsi.SetId(-1);
                }
                else
                {
                    txtBacsi.SetId(globalVariables.gv_intIDNhanvien);
                }
            }
            catch (Exception)
            {
            }
        }

        private void AutoWarning()
        {
            try
            {
                string Canhbaotamung = THU_VIEN_CHUNG.Canhbaotamung(objLuotkham);
                Utility.SetMsg(lblMsg, Canhbaotamung, true);
            }
            catch (Exception ex)
            {
            }
        }

        private void CauHinh()
        {
            if (PropertyLib._ThamKhamProperties != null)
            {
                cboA4.Text = (PropertyLib._MayInProperties.CoGiayInDonthuoc == Papersize.A4) ? "A4" : "A5";
            }
            cboPrintPreview.SelectedIndex = PropertyLib._MayInProperties.PreviewInDonthuoc ? 0 : 1;
            cboLaserPrinters.Text = PropertyLib._MayInProperties.TenMayInBienlai;
            pnlPrint.Visible = PropertyLib._ThamKhamProperties.ChophepIndonthuoc;
            chkSaveAndPrint.Checked = PropertyLib._MayInProperties.InDonthuocsaukhiluu;
            cmdPrintPres.Visible = PropertyLib._ThamKhamProperties.ChophepIndonthuoc;
            chkHienthithuoctheonhom.Checked = PropertyLib._ThamKhamProperties.Hienthinhomthuoc;
            globalVariables.KHOKEDON = PropertyLib._ThamKhamProperties.IDKho;
            ModifyButton();
        }

        private void cboA4_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.CoGiayInDonthuoc = (cboA4.SelectedIndex == 0) ? Papersize.A4 : Papersize.A5;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        private void cboLaserPrinters_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveDefaultPrinter();
        }

        private void cboPrintPreview_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.PreviewInDonthuoc = cboPrintPreview.SelectedIndex == 0;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        private void cboStock_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((blnHasLoaded && (cboStock.Items.Count > 0)) &&
                    ((cboStock.SelectedValue == null) || (cboStock.SelectedValue.ToString() != "-1")))
                {
                    globalVariables.KHOKEDON = Utility.Int32Dbnull(cboStock.SelectedValue, -1);
                    if (KIEU_THUOC_VT == "THUOC")
                        PropertyLib._ThamKhamProperties.IDKho = globalVariables.KHOKEDON;
                    else
                        PropertyLib._ThamKhamProperties.IDKhoVT = globalVariables.KHOKEDON;
                    PropertyLib.SaveProperty(PropertyLib._ThamKhamProperties);
                    int num = Utility.Int32Dbnull(cboStock.SelectedValue, -1);
                    if ((num > 0) && ((cboStock.Items.Count > 0)))
                    {
                        if (objLuotkham.DungTuyen != null)
                            m_dtDanhmucthuoc = _kedonthuoc.LayThuoctrongkhokedon(num, KIEU_THUOC_VT,
                                Utility.sDbnull(objLuotkham.MaDoituongKcb, "DV"),
                                Utility.Int32Dbnull(objLuotkham.DungTuyen.Value, 0), noitru, globalVariables.MA_KHOA_THIEN);
                        //  ProcessData();
                        objDKho = ReadOnlyRecord<TDmucKho>.FetchByID(num);
                        rowFilter = "1=1";
                        txtdrug.AllowedSelectPrice = Utility.Byte2Bool(objDKho.ChophepChongia);
                        txtdrug.dtData = m_dtDanhmucthuoc;
                        txtdrug.ChangeDataSource();
                        txtdrug.Focus();
                        txtdrug.SelectAll();
                    }
                    else
                    {
                        objDKho = null;
                    }
                }
            }
            catch (Exception exception)
            {
                Utility.CatchException(exception);
            }
        }

        private void ChiDanThuoc()
        {
            string containGuide = GetContainGuide();
            txtChiDanDungThuoc.Text = containGuide;
        }

        private void chkAskbeforeDeletedrug_CheckedChanged(object sender, EventArgs e)
        {
            PropertyLib._ThamKhamProperties.Hoitruockhixoathuoc = chkAskbeforeDeletedrug.Checked;
            PropertyLib.SaveProperty(PropertyLib._ThamKhamProperties);
        }

        private void chkHienthithuoctheonhom_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                PropertyLib._ThamKhamProperties.Hienthinhomthuoc = chkHienthithuoctheonhom.Checked;
                PropertyLib.SaveProperty(PropertyLib._ThamKhamProperties);
                grdPresDetail.RootTable.Groups.Clear();
                if (chkHienthithuoctheonhom.Checked)
                {
                    GridEXColumn column = grdPresDetail.RootTable.Columns["ma_loaithuoc"];
                    var group = new GridEXGroup(column)
                    {
                        GroupPrefix = "Loại " + (KIEU_THUOC_VT == "THUOC" ? "thuốc" : "vật tư") + " :"
                    };
                    grdPresDetail.RootTable.Groups.Add(group);
                }
            }
            catch
            {
            }
        }

        private void chkNgayTaiKham_CheckedChanged(object sender, EventArgs e)
        {
            dtNgayKhamLai.Enabled = chkNgayTaiKham.Checked;
        }

        private void chkSaveAndPrint_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                PropertyLib._MayInProperties.InDonthuocsaukhiluu = chkSaveAndPrint.Checked;
                PropertyLib.SaveProperty(PropertyLib._MayInProperties);
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi khi lưu trạng thái-->" + exception.Message);
            }
        }

        private void ClearControl()
        {
            foreach (Control control in grpkedon.Controls)
            {
                if (control is EditBox)
                {
                    ((EditBox) control).Clear();
                }
                if (control is TextBox)
                {
                    ((TextBox) control).Clear();
                }
                txtSoluong.Text = "";
                txtDrugID.Clear();
                txtChiDanDungThuoc.Clear();
            }
            txtDrugID.Clear();
            ModifyButton();
        }

        private void cmdAddDetail_Click(object sender, EventArgs e)
        {
            try
            {
                cmdAddDetail.Enabled = false;
                if (Utility.Int32Dbnull(txtBacsi.MyID, -1) <= 0)
                {
                    Utility.SetMsg(lblMsg, "Bạn cần chọn bác sĩ chỉ định trước khi thực hiện kê đơn thuốc", true);
                    txtBacsi.Focus();
                    return;
                }
                if (objPhieudieutriNoitru != null)
                {
                    if (dtpCreatedDate.Value.Date > objPhieudieutriNoitru.NgayDieutri.Value.Date)
                    {
                        Utility.ShowMsg("Ngày kê đơn phải <= " +
                                        objPhieudieutriNoitru.NgayDieutri.Value.ToString("dd/MM/yyyy"));
                        return;
                    }
                }
                if (Utility.Int32Dbnull(txtDrugID.Text, -1) <= 0 || Utility.sDbnull(txtDrug_Name.Text, "") == "") return;
                AddPreDetail();
                Manual = true;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
            finally
            {
                Thread.Sleep(150);
                cmdAddDetail.Enabled = true;
            }
        }

        private void cmdCauHinh_Click(object sender, EventArgs e)
        {
            new frm_Properties(PropertyLib._ThamKhamProperties).ShowDialog();
            CauHinh();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                setMsg(lblMsg, "", false);
                if (grdPresDetail.GetCheckedRows().Length <= 0)
                {
                    setMsg(lblMsg, "Bạn phải chọn " + (KIEU_THUOC_VT == "THUOC" ? "thuốc" : "vật tư") + " để xóa", true);
                    grdPresDetail.Focus();
                }
                else
                {
                    int num;
                    foreach (GridEXRow row in grdPresDetail.GetCheckedRows())
                    {
                       
                        num = Utility.Int32Dbnull(row.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, -1);
                        int i =
                            m_dtDonthuocChitiet.Select(KcbDonthuocChitiet.Columns.IdChitietdonthuoc + "=" + num +
                                                       KcbDonthuocChitiet.Columns.TrangthaiThanhtoan + "= 1").Count();
                        if ((num > 0) && i >0)
                        {
                            setMsg(lblMsg, "Bản ghi đã thanh toán, bạn không thể xóa", true);
                            grdPresDetail.Focus();
                            return;
                        }
                    }
                    if (!PropertyLib._ThamKhamProperties.Hoitruockhixoathuoc ||
                        Utility.AcceptQuestion(
                            "Bạn Có muốn xóa các " + (KIEU_THUOC_VT == "THUOC" ? "thuốc" : "vật tư") +
                            " đang chọn hay không?", "thông báo xóa", true))
                    {
                        foreach (GridEXRow row in grdPresDetail.GetCheckedRows())
                        {
                            num = Utility.Int32Dbnull(row.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, -1);
                            if (num > 0)
                            {
                                _kedonthuoc.XoaChitietDonthuoc(num);
                            }
                            row.Delete();
                            grdPresDetail.UpdateData();
                            m_dtDonthuocChitiet.AcceptChanges();
                        }
                        m_dtDonthuocChitiet.AcceptChanges();
                        m_blnCancel = false;
                        UpdateDataWhenChanged();
                    }
                }
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
            }
            finally
            {
                if (grdPresDetail.RowCount <= 0)
                {
                    em_Action = action.Insert;
                }
            }
        }

        private void cmdDonThuocDaKe_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            base.Close();
        }


        private void cmdPrintPres_Click(object sender, EventArgs e)
        {
            // if(THU_VIEN_CHUNG.)
            PrintPres(Utility.Int32Dbnull(txtPres_ID.Text));
        }

        private void cmdSavePres_Click(object sender, EventArgs e)
        {
            try
            {
                cmdSavePres.Enabled = false;
                isSaved = true;
                if (Utility.Int32Dbnull(txtBacsi.MyID, -1) <= 0)
                {
                    Utility.SetMsg(lblMsg, "Bạn cần chọn bác sĩ chỉ định trước khi thực hiện kê đơn thuốc", true);
                    txtBacsi.Focus();
                    return;
                }
                if (objPhieudieutriNoitru != null)
                {
                    if (dtpCreatedDate.Value.Date > objPhieudieutriNoitru.NgayDieutri.Value.Date)
                    {
                        Utility.ShowMsg("Ngày kê đơn phải <= " +
                                        objPhieudieutriNoitru.NgayDieutri.Value.ToString("dd/MM/yyyy"));
                        dtpCreatedDate.Focus();
                        return;
                    }
                }

                if (grdPresDetail.RowCount <= 0)
                {
                    setMsg(lblMsg, "Hiện chưa Có bản ghi nào để thực hiện  lưu lại", true);
                    txtdrug.Focus();
                    txtdrug.SelectAll();
                }
                else
                {
                    List<KcbDonthuocChitiet> changedData = GetChangedData();
                    PerformAction((changedData == null)
                        ? new List<KcbDonthuocChitiet>().ToArray()
                        : changedData.ToArray());
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
            finally
            {
                cmdSavePres.Enabled = true;
                Manual = false;
                hasChanged = false;
                base.Close();
            }
        }

        private void cmdUpdateChiDan_Click(object sender, EventArgs e)
        {
            UpdateChiDanThem();
        }

        private void Create_ChandoanKetluan()
        {
            if (((Utility.DoTrim(txtTenBenhChinh.Text) != "") || (grd_ICD.GetDataRows().Length > 0)) ||
                (Utility.DoTrim(txtChanDoan.Text) != ""))
            {
                SqlQuery sqlkt =
                    new Select().From(KcbChandoanKetluan.Schema)
                        .Where(KcbChandoanKetluan.Columns.IdKham)
                        .IsEqualTo(Utility.Int64Dbnull(id_kham));
                if (KcbChandoanKetluan == null || sqlkt.GetRecordCount() <= 0)
                {
                    KcbChandoanKetluan = new KcbChandoanKetluan();
                }
                KcbChandoanKetluan.IdKham = id_kham;
                KcbChandoanKetluan.MaLuotkham = objLuotkham.MaLuotkham;
                KcbChandoanKetluan.IdBenhnhan = objLuotkham.IdBenhnhan;
                KcbChandoanKetluan.MotaBenhchinh = Utility.sDbnull(txtTenBenhChinh.Text);
                KcbChandoanKetluan.MabenhChinh = Utility.sDbnull(txtMaBenhChinh.Text, "");
                if (Utility.Int16Dbnull(txtBacsi.MyID, -1) > 0)
                {
                    KcbChandoanKetluan.IdBacsikham = Utility.Int16Dbnull(txtBacsi.MyID, -1);
                }
                else
                {
                    KcbChandoanKetluan.IdBacsikham = globalVariables.gv_intIDNhanvien;
                }
                KcbChandoanKetluan.MabenhPhu = Utility.sDbnull(GetDanhsachBenhphu(), "");
                if (KcbChandoanKetluan.IsNew)
                {
                    KcbChandoanKetluan.NgayTao = dtpCreatedDate.Value;
                    KcbChandoanKetluan.NguoiTao = globalVariables.UserName;

                    KcbChandoanKetluan.IpMaytao = globalVariables.gv_strIPAddress;
                    KcbChandoanKetluan.TenMaytao = globalVariables.gv_strComputerName;
                }
                else
                {
                    KcbChandoanKetluan.NgaySua = dtpCreatedDate.Value;
                    KcbChandoanKetluan.NguoiSua = globalVariables.UserName;

                    KcbChandoanKetluan.IpMaysua = globalVariables.gv_strIPAddress;
                    KcbChandoanKetluan.TenMaysua = globalVariables.gv_strComputerName;
                }
                KcbChandoanKetluan.NgayChandoan = dtpCreatedDate.Value;
                KcbChandoanKetluan.IdBacsikham = globalVariables.gv_intIDNhanvien;
                KcbChandoanKetluan.Chandoan = Utility.ReplaceString(txtChanDoan.Text);
                KcbChandoanKetluan.Noitru = (byte) noitru;
            }
        }

        private KcbDonthuocChitiet[] CreateArrayPresDetail()
        {
            _temp = ActionResult.Success;
            int index = 0;
            var chitietArray = new KcbDonthuocChitiet[m_dtDonthuocChitiet.DefaultView.Count];
            try
            {
                foreach (DataRowView view in m_dtDonthuocChitiet.DefaultView)
                {
                    long num2 = Utility.Int64Dbnull(view[KcbDonthuocChitiet.Columns.IdThuockho], -1);
                    decimal num3 = CommonLoadDuoc.SoLuongTonTrongKho(-1L,
                        Utility.Int32Dbnull(view[KcbDonthuocChitiet.Columns.IdKho], -1),
                        Utility.Int16Dbnull(view[KcbDonthuocChitiet.Columns.IdThuoc], -1),
                        Utility.Int64Dbnull(view[KcbDonthuocChitiet.Columns.IdThuockho], -1),
                        Utility.Int32Dbnull(
                            THU_VIEN_CHUNG.Laygiatrithamsohethong("KIEMTRATHUOC_CHOXACNHAN", "1", false), 1),
                        Utility.ByteDbnull(objLuotkham.Noitru, 0));
                    if (em_Action == action.Update)
                    {
                        int soLuong = 0;
                        DataTable dtChitiet =
                            SPs.SpKcbLaydulieuChitietDonthuoc(
                                Utility.Int64Dbnull(view[KcbDonthuocChitiet.Columns.IdChitietdonthuoc], -1))
                                .GetDataSet()
                                .Tables[0];
                        if (dtChitiet != null && dtChitiet.Rows.Count > 0)
                        {
                            soLuong = Utility.Int32Dbnull(dtChitiet.Rows[0]["So_Luong"], 0);
                        }
                        num3 += soLuong;
                    }
                    if (Utility.Int32Dbnull(view[KcbDonthuocChitiet.Columns.SoLuong], 0) > num3)
                    {
                        Utility.ShowMsg(
                            string.Format(
                                "Số lượng " + (KIEU_THUOC_VT == "THUOC" ? "thuốc" : "vật tư") +
                                " {0}({1}) vượt quá số lượng tồn trong kho{2}({3}) \nBạn cần chỉnh lại số lượng " +
                                (KIEU_THUOC_VT == "THUOC" ? "thuốc" : "vật tư") + "!",
                                new object[]
                                {
                                    Utility.sDbnull(view[DmucThuoc.Columns.TenThuoc], ""),
                                    Utility.Int32Dbnull(view[KcbDonthuocChitiet.Columns.SoLuong], 0).ToString(),
                                    Utility.sDbnull(view[TDmucKho.Columns.TenKho], ""), num3.ToString()
                                }));
                        _temp = ActionResult.NotEnoughDrugInStock;
                        return null;
                    }
                    chitietArray[index] = new KcbDonthuocChitiet();
                    chitietArray[index].IdDonthuoc = IdDonthuoc;
                    chitietArray[index].IdChitietdonthuoc =
                        Utility.Int32Dbnull(view[KcbDonthuocChitiet.Columns.IdChitietdonthuoc], -1);
                    chitietArray[index].IdBenhnhan = objLuotkham.IdBenhnhan;
                    chitietArray[index].MaLuotkham = objLuotkham.MaLuotkham;
                    chitietArray[index].IdKho = Utility.Int32Dbnull(view[KcbDonthuocChitiet.Columns.IdKho], -1);
                    chitietArray[index].IdThuoc = Utility.Int16Dbnull(view[KcbDonthuocChitiet.Columns.IdThuoc], -1);
                    chitietArray[index].TrangthaiThanhtoan =
                        Utility.ByteDbnull(view[KcbDonthuocChitiet.Columns.TrangthaiThanhtoan], 0);
                    chitietArray[index].SttIn = Utility.Int16Dbnull(view[KcbDonthuocChitiet.Columns.SttIn], 1);
                    chitietArray[index].TrangthaiHuy = 0;
                    chitietArray[index].IdThuockho = num2;
                    chitietArray[index].GiaNhap = Utility.DecimaltoDbnull(view[KcbDonthuocChitiet.Columns.GiaNhap], -1);
                    chitietArray[index].GiaBan = Utility.DecimaltoDbnull(view[KcbDonthuocChitiet.Columns.GiaBan], -1);
                    chitietArray[index].Vat = Utility.DecimaltoDbnull(view[KcbDonthuocChitiet.Columns.Vat], -1);
                    chitietArray[index].SoLo = Utility.sDbnull(view[KcbDonthuocChitiet.Columns.SoLo], -1);
                    chitietArray[index].MaNhacungcap = Utility.sDbnull(view[KcbDonthuocChitiet.Columns.MaNhacungcap], -1);
                    chitietArray[index].NgayHethan = Utility.ConvertDate(view["sNgayhethan"].ToString()).Date;
                    chitietArray[index].SoluongHuy = 0;
                    chitietArray[index].TuTuc = Utility.ByteDbnull(view[KcbDonthuocChitiet.Columns.TuTuc], 0);
                    chitietArray[index].SoLuong = Utility.Int32Dbnull(view[KcbDonthuocChitiet.Columns.SoLuong], 0);
                    chitietArray[index].DonGia = Utility.DecimaltoDbnull(view[KcbDonthuocChitiet.Columns.DonGia], 0);
                    chitietArray[index].PhuThu = Utility.Int16Dbnull(view[KcbDonthuocChitiet.Columns.PhuThu], 0);
                    chitietArray[index].MotaThem = Utility.sDbnull(view[KcbDonthuocChitiet.Columns.MotaThem], "");
                    chitietArray[index].ChidanThem = Utility.sDbnull(view[KcbDonthuocChitiet.Columns.ChidanThem], "");
                    chitietArray[index].CachDung = Utility.sDbnull(view[KcbDonthuocChitiet.Columns.CachDung], "");
                    chitietArray[index].DonviTinh = Utility.sDbnull(view[KcbDonthuocChitiet.Columns.DonviTinh], "");
                    chitietArray[index].SolanDung = Utility.sDbnull(view[KcbDonthuocChitiet.Columns.SolanDung], null);
                    chitietArray[index].SoluongDung = Utility.sDbnull(view[KcbDonthuocChitiet.Columns.SoluongDung], null);
                    chitietArray[index].SluongSua = 0;
                    chitietArray[index].SluongLinh = 0;
                    chitietArray[index].TrangThai = 0;
                    chitietArray[index].TrangthaiBhyt = 1;
                    chitietArray[index].IdThanhtoan = -1;
                    chitietArray[index].IdGoi = id_goidv;
                    chitietArray[index].TrongGoi = trong_goi;
                    chitietArray[index].NgayTao = globalVariables.SysDate;
                    chitietArray[index].NguoiTao = globalVariables.UserName;
                    chitietArray[index].MaDoituongKcb = Utility.sDbnull(MaDoiTuong);
                    chitietArray[index].PtramBhyt = objLuotkham.TrangthaiNoitru <= 0
                        ? objLuotkham.PtramBhyt
                        : objLuotkham.PtramBhytGoc;
                    chitietArray[index].PtramBhytGoc = objLuotkham.PtramBhytGoc;
                    if (Utility.Int32Dbnull(view[KcbDonthuocChitiet.Columns.TuTuc], 0) == 0)
                    {
                        decimal BHCT = 0m;
                        if (objLuotkham.DungTuyen == 1)
                        {
                            BHCT = Utility.DecimaltoDbnull(view[KcbDonthuocChitiet.Columns.DonGia], 0)*
                                   (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0)/100);
                        }
                        else
                        {
                            if (objLuotkham.TrangthaiNoitru <= 0)
                                BHCT = Utility.DecimaltoDbnull(view[KcbDonthuocChitiet.Columns.DonGia], 0)*
                                       (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0)/100);
                            else //Nội trú cần tính=đơn giá * % đầu thẻ * % tuyến
                                BHCT = Utility.DecimaltoDbnull(view[KcbDonthuocChitiet.Columns.DonGia], 0)*
                                       (Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0)/100)*
                                       (_bhytPtramTraituyennoitru/100);
                        }
                        //decimal num5 = (Utility.DecimaltoDbnull(view[KcbDonthuocChitiet.Columns.DonGia], 0) * Utility.DecimaltoDbnull(this.objLuotkham.PtramBhyt, 0)) / 100M;
                        decimal num6 = Utility.DecimaltoDbnull(view[KcbDonthuocChitiet.Columns.DonGia], 0) - BHCT;
                        chitietArray[index].BhytChitra = BHCT;
                        chitietArray[index].BnhanChitra = num6;
                    }
                    else //BHYT Tự túc
                    {
                        chitietArray[index].BhytChitra = 0;
                        chitietArray[index].BnhanChitra =
                            Utility.DecimaltoDbnull(view[KcbDonthuocChitiet.Columns.DonGia], 0);
                        chitietArray[index].PtramBhyt = 0;
                    }
                    if (objLuotkham.MaDoituongKcb == "BHYT")
                    {
                    }
                    index++;
                }
            }
            catch (Exception)
            {
            }
            return chitietArray;
        }

        private KcbDonthuoc TaoDonthuoc()
        {
            var donthuoc = new KcbDonthuoc
            {
                MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham, ""),
                IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan, -1),
                MaKhoaThuchien = globalVariables.MA_KHOA_THIEN,
                LoidanBacsi = Utility.sDbnull(txtLoiDanBS.Text),
                TaiKham = Utility.sDbnull(txtKhamLai.Text)
            };
            if (chkNgayTaiKham.Checked)
            {
                donthuoc.NgayTaikham = dtNgayKhamLai.Value;
            }
            else
            {
                donthuoc.NgayTaikham = null;
            }
            donthuoc.IdLichsuDoituongKcb = objLuotkham.IdLichsuDoituongKcb;
            donthuoc.MatheBhyt = objLuotkham.MatheBhyt;
            donthuoc.NgayKedon = dtpCreatedDate.Value;
            donthuoc.MaDoituongKcb = MaDoiTuong;
            donthuoc.TenDonthuoc = THU_VIEN_CHUNG.TaoTenDonthuoc(objLuotkham.MaLuotkham,
                Utility.Int32Dbnull(objLuotkham.IdBenhnhan, -1));
            DataTable dtRegExam = SPs.SpKcbLaydoituongDangkyKCB(id_kham).GetDataSet().Tables[0];
            if (dtRegExam != null && dtRegExam.Rows.Count > 0)
            {
                donthuoc.IdKhoadieutri = Utility.Int16Dbnull(dtRegExam.Rows[0]["id_khoakcb"]);
                donthuoc.IdPhongkham = Utility.Int16Dbnull(dtRegExam.Rows[0]["id_phongkham"]);
                donthuoc.IdGiuongNoitru = -1;
            }
            else
            {
                donthuoc.IdKhoadieutri = globalVariables.idKhoatheoMay;
                donthuoc.IdPhongkham = globalVariables.idKhoatheoMay;
                donthuoc.IdGiuongNoitru = -1;
            }

            donthuoc.IdGoi = id_goidv;
            donthuoc.TrongGoi = trong_goi;
            if (objPhieudieutriNoitru != null)
            {
                donthuoc.IdPhieudieutri = objPhieudieutriNoitru.IdPhieudieutri;
                donthuoc.IdKhoadieutri = objPhieudieutriNoitru.IdKhoanoitru;
                donthuoc.IdPhongkham = objPhieudieutriNoitru.IdKhoanoitru;
                donthuoc.IdBuongGiuong = objPhieudieutriNoitru.IdBuongGiuong;
                donthuoc.IdBuongNoitru = objLuotkham.IdBuong;
                donthuoc.IdGiuongNoitru = objLuotkham.IdGiuong;
            }
            donthuoc.TrangthaiThanhtoan = 0;
            donthuoc.IdBacsiChidinh =  Utility.Int16Dbnull(txtBacsi.MyID, globalVariables.gv_intIDNhanvien);
            donthuoc.TrangThai = 0;
            donthuoc.NguoiTao = globalVariables.UserName;
            donthuoc.NgayTao = globalVariables.SysDate;
            donthuoc.IdDonthuocthaythe = -1;
            donthuoc.IdKham = id_kham;
            donthuoc.NgayTao = globalVariables.SysDate;
            donthuoc.NguoiTao = globalVariables.UserName;
            donthuoc.Noitru = (byte) noitru;

            donthuoc.KieuDonthuoc = (byte) (chkAdditional.Checked ? 1 : 0);
            donthuoc.KieuThuocvattu = KIEU_THUOC_VT; // (this.m_intKieudonthuoc == 1) ? "VT" : "THUOC";
            if (em_Action == action.Update)
            {
                donthuoc.IdDonthuoc = Utility.Int32Dbnull(txtPres_ID.Text, -1);
                donthuoc.NguoiSua = globalVariables.UserName;
                donthuoc.NgaySua = globalVariables.SysDate;

                donthuoc.IpMaysua = globalVariables.gv_strIPAddress;
                donthuoc.TenMaysua = globalVariables.gv_strComputerName;
                donthuoc.IsNew = false;
                donthuoc.MarkOld();
            }
            else
            {
                donthuoc.IpMaytao = globalVariables.gv_strIPAddress;
                donthuoc.TenMaytao = globalVariables.gv_strComputerName;
            }

            return donthuoc;
        }

        private void CreateViewTable()
        {
            try
            {
                m_dtDonthuocChitiet_View = m_dtDonthuocChitiet.Clone();
                foreach (DataRow row in m_dtDonthuocChitiet.Rows)
                {
                    row["CHON"] = 0;
                    DataRow[] rowArray =
                        m_dtDonthuocChitiet_View.Select(KcbDonthuocChitiet.Columns.IdThuoc + "=" +
                                                        Utility.sDbnull(row[KcbDonthuocChitiet.Columns.IdThuoc], "-1") +
                                                        " AND " + KcbDonthuocChitiet.Columns.DonGia + "=" +
                                                        Utility.sDbnull(row[KcbDonthuocChitiet.Columns.DonGia], "-1") +
                                                        " AND " + KcbDonthuocChitiet.Columns.TuTuc + "=" +
                                                        Utility.sDbnull(row[KcbDonthuocChitiet.Columns.TuTuc], "-1"));
                    if (rowArray.Length <= 0)
                    {
                        row["TT_KHONG_PHUTHU"] = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) *
                                                 Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia]);
                        row["TT"] = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) *
                                    (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia]) +
                                     Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu]));
                        row["TT_BHYT"] = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) *
                                         Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BhytChitra]);
                        row["TT_BN"] = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) *
                                       (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BnhanChitra], 0) +
                                        Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu], 0));
                        row["TT_PHUTHU"] = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) *
                                           Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu], 0);
                        row["TT_BN_KHONG_PHUTHU"] = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) *
                                                    Utility.DecimaltoDbnull(
                                                        row[KcbDonthuocChitiet.Columns.BnhanChitra], 0);
                        m_dtDonthuocChitiet_View.ImportRow(row);
                    }
                    else
                    {
                        rowArray[0][KcbDonthuocChitiet.Columns.SoLuong] =
                            Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong], 0) +
                            Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.SoLuong], 0);
                        rowArray[0]["TT_KHONG_PHUTHU"] =
                            Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                            Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.DonGia]);
                        rowArray[0]["TT"] = Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                            (Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.DonGia]) +
                                             Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu]));
                        rowArray[0]["TT_BHYT"] = Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                                 Utility.DecimaltoDbnull(
                                                     rowArray[0][KcbDonthuocChitiet.Columns.BhytChitra]);
                        rowArray[0]["TT_BN"] = Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                               (Utility.DecimaltoDbnull(
                                                   rowArray[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0) +
                                                Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu],
                                                    0));
                        rowArray[0]["TT_PHUTHU"] = Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                                                   Utility.DecimaltoDbnull(
                                                       rowArray[0][KcbDonthuocChitiet.Columns.PhuThu], 0);
                        rowArray[0]["TT_BN_KHONG_PHUTHU"] =
                            Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) *
                            Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0);
                        rowArray[0][KcbDonthuocChitiet.Columns.SttIn] =
                            Math.Min(Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.SttIn], 0),
                                Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SttIn], 0));
                        m_dtDonthuocChitiet_View.AcceptChanges();
                    }
                }
                m_dtDonthuocChitiet_View.AcceptChanges();
                Utility.SetDataSourceForDataGridEx_Basic(grdPresDetail, m_dtDonthuocChitiet_View, false, true, "1=1",
                    KcbDonthuocChitiet.Columns.SttIn);
            }
            catch
            {
            }
        }

        private void deletefromDatatable(List<int> lstIdChitietDonthuoc)
        {
            Func<DataRow, bool> predicate = null;
            try
            {
                if (predicate == null)
                {
                    predicate =
                        q =>
                            lstIdChitietDonthuoc.Contains(
                                Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc]));
                }
                DataRow[] rowArray =
                    m_dtDonthuocChitiet.Select("1=1").AsEnumerable().Where(predicate).ToArray<DataRow>();
                for (int i = 0; i <= (rowArray.Length - 1); i++)
                {
                    m_dtDonthuocChitiet.Rows.Remove(rowArray[i]);
                }
                m_dtDonthuocChitiet.AcceptChanges();
            }
            catch
            {
            }
        }

        private void deletefromDatatable(List<int> lstDeleteId, int lastdetailid, decimal soluong)
        {
            Func<DataRow, bool> predicate = null;
            Func<DataRow, bool> func2 = null;
            try
            {
                int num;
                if (predicate == null)
                {
                    predicate =
                        q => Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc]) == lastdetailid;
                }
                DataRow[] rowArray =
                    m_dtDonthuocChitiet.Select("1=1").AsEnumerable().Where(predicate).ToArray<DataRow>();
                for (num = 0; num <= (rowArray.Length - 1); num++)
                {
                    if (soluong <= 0)
                    {
                        m_dtDonthuocChitiet.Rows.Remove(rowArray[num]);
                    }
                    else
                    {
                        rowArray[num][KcbDonthuocChitiet.Columns.SoLuong] = soluong;
                    }
                }
                if (func2 == null)
                {
                    func2 =
                        q =>
                            lstDeleteId.Contains(Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc], 0));
                }
                rowArray = m_dtDonthuocChitiet.Select("1=1").AsEnumerable().Where(func2).ToArray<DataRow>();
                for (num = 0; num <= (rowArray.Length - 1); num++)
                {
                    m_dtDonthuocChitiet.Rows.Remove(rowArray[num]);
                }
                m_dtDonthuocChitiet.AcceptChanges();
            }
            catch
            {
            }
        }


        private void DSACH_ICD(EditBox tEditBox, string LOAITIMKIEM, int CP)
        {
            try
            {
                Selected = false;
                string filterExpression = "";
                if (LOAITIMKIEM.ToUpper() == DmucChung.Columns.Ten)
                {
                    filterExpression = " Disease_Name like '%" + tEditBox.Text + "%' OR FirstChar LIKE '%" +
                                       tEditBox.Text + "%'";
                }
                else if (LOAITIMKIEM == DmucChung.Columns.Ma)
                {
                    filterExpression = DmucBenh.Columns.MaBenh + " LIKE '%" + tEditBox.Text + "%'";
                }
                DataRow[] source = DtIcd.Select(filterExpression);
                if (source.Length == 1)
                {
                    if (CP == 0)
                    {
                        txtMaBenhChinh.Text = "";
                        txtMaBenhChinh.Text = Utility.sDbnull(source[0][DmucBenh.Columns.MaBenh], "");
                        hasMorethanOne = false;
                        txtMaBenhChinh_TextChanged(txtMaBenhChinh, new EventArgs());
                        txtMaBenhChinh.Focus();
                    }
                    else if (CP == 1)
                    {
                        txtMaBenhphu.Text = Utility.sDbnull(source[0][DmucBenh.Columns.MaBenh], "");
                        hasMorethanOne = false;
                        txtMaBenhphu_TextChanged(txtMaBenhphu, new EventArgs());
                        txtMaBenhphu_KeyDown(txtMaBenhphu, new KeyEventArgs(Keys.Enter));
                        Selected = false;
                    }
                }
                else if (source.Length > 1)
                {
                    var h_icd = new frm_DanhSach_ICD(CP)
                    {
                        dt_ICD = source.CopyToDataTable()
                    };
                    h_icd.ShowDialog();
                    if (!h_icd.has_Cancel)
                    {
                        List<GridEXRow> lstSelectedRows = h_icd.lstSelectedRows;
                        if (CP == 0)
                        {
                            isLike = false;
                            txtMaBenhChinh.Text = "";
                            txtMaBenhChinh.Text =
                                Utility.sDbnull(lstSelectedRows[0].Cells[DmucBenh.Columns.MaBenh].Value, "");
                            hasMorethanOne = false;
                            txtMaBenhChinh_TextChanged(txtMaBenhChinh, new EventArgs());
                            txtMaBenhChinh_KeyDown(txtMaBenhChinh, new KeyEventArgs(Keys.Enter));
                            Selected = false;
                        }
                        else if (CP == 1)
                        {
                            if (lstSelectedRows.Count == 1)
                            {
                                isLike = false;
                                txtMaBenhphu.Text = "";
                                txtMaBenhphu.Text =
                                    Utility.sDbnull(lstSelectedRows[0].Cells[DmucBenh.Columns.MaBenh].Value, "");
                                hasMorethanOne = false;
                                txtMaBenhphu_TextChanged(txtMaBenhphu, new EventArgs());
                                txtMaBenhphu_KeyDown(txtMaBenhphu, new KeyEventArgs(Keys.Enter));
                                Selected = false;
                            }
                            else
                            {
                                foreach (GridEXRow row in lstSelectedRows)
                                {
                                    isLike = false;
                                    txtMaBenhphu.Text = "";
                                    txtMaBenhphu.Text = Utility.sDbnull(row.Cells[DmucBenh.Columns.MaBenh].Value, "");
                                    hasMorethanOne = false;
                                    txtMaBenhphu_TextChanged(txtMaBenhphu, new EventArgs());
                                    txtMaBenhphu_KeyDown(txtMaBenhphu, new KeyEventArgs(Keys.Enter));
                                    Selected = false;
                                }
                                hasMorethanOne = true;
                            }
                        }
                        tEditBox.Focus();
                    }
                    else
                    {
                        hasMorethanOne = true;
                        tEditBox.Focus();
                    }
                }
                else
                {
                    hasMorethanOne = true;
                    tEditBox.SelectAll();
                }
            }
            catch
            {
            }
            finally
            {
                isLike = true;
            }
        }

        private void frm_KCB_KE_DONTHUOC_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isSaved)
            {
                List<KcbDonthuocChitiet> changedData = GetChangedData();
                if (((changedData != null) && (changedData.Count > 0)) &&
                    Utility.AcceptQuestion(
                        "Bạn đã thay đổi đơn " + (KIEU_THUOC_VT == "THUOC" ? "thuốc" : "vật tư") +
                        " nhưng chưa lưu lại. Bạn Có muốn lưu đơn " + (KIEU_THUOC_VT == "THUOC" ? "thuốc" : "vật tư") +
                        " trước khi thoát hay không? Nhấn Yes để lưu đơn " +
                        (KIEU_THUOC_VT == "THUOC" ? "thuốc" : "vật tư") + ". Nhấn No để không lưu đơn " +
                        (KIEU_THUOC_VT == "THUOC" ? "thuốc" : "vật tư"), "Cảnh báo", true))
                {
                    cmdSavePres_Click(cmdSavePres, new EventArgs());
                }
            }
        }

        private void frm_KCB_KE_DONTHUOC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F11)
            {
                Utility.ShowMsg(base.ActiveControl.Name);
            }
            if (e.KeyCode == Keys.F4 || (e.Control && e.KeyCode == Keys.P))
            {
                cmdPrintPres_Click(cmdPrintPres, new EventArgs());
            }
            if ((e.KeyCode == Keys.A) && e.Control)
            {
                cmdAddDetail_Click(cmdAddDetail, new EventArgs());
            }
            if (e.KeyCode == Keys.S && e.Control)
            {
                cmdSavePres_Click(cmdSavePres, new EventArgs());
            }
            if (e.KeyCode == Keys.F3)
            {
                txtdrug.Focus();
                txtdrug.SelectAll();
            }
            if ((e.Shift || e.Alt) && (e.KeyCode == Keys.S))
            {
            }
            else
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (uiTabPage1.ActiveControl != null && uiTabPage1.ActiveControl.Name == splitContainer2.Name)
                        return;
                    if (uiTabPage1.ActiveControl != null && uiTabPage1.ActiveControl.Name == splitContainer4.Name &&
                        (Utility.DecimaltoDbnull(txtSoluong.Text, 0) > 0))
                    {
                        if (!_autoFill)
                        {
                            if (globalVariables.gv_intChophepChinhgiathuocKhiKedon == 0)
                            {
                                if (Utility.DecimaltoDbnull(txtSoLuongDung.Text, 0) > 0)
                                {
                                    SendKeys.Send("{TAB}");
                                }
                                 else
                                {
                                     txtSoLuongDung.Focus();
                                    txtSoLuongDung.SelectAll();
                                }
                            }
                            else
                            {
                                txtPrice.Focus();
                                txtPrice.SelectAll();
                            }
                        }
                        else
                        {
                            cmdAddDetail_Click(cmdAddDetail, new EventArgs());
                        }
                    }
                    else
                        SendKeys.Send("{TAB}");
                }
                if (e.KeyCode == Keys.F5)
                {
                    cboStock_SelectedIndexChanged(cboStock, new EventArgs());
                }
                if (e.KeyCode == Keys.Escape)
                {
                    cmdExit_Click(cmdExit, new EventArgs());
                }
            }
        }

        private void frm_KCB_KE_DONTHUOC_Load(object sender, EventArgs e)
        {
            try
            {
                chkAdditional.Checked = forced2Add;
                chkAdditional.Visible = !forced2Add && objLuotkham.TrangthaiNoitru > 0;
                txtptramdauthe.Visible = objLuotkham.IdLoaidoituongKcb == 0;
                lblphantramdauthe.Visible = objLuotkham.IdLoaidoituongKcb == 0;
                pnlChandoanNgoaitru.Visible = objLuotkham.TrangthaiNoitru <= 0;
                m_dtqheCamchidinhChungphieu = globalVariables.gv_dtDmucQheCamCLSChungPhieu;
                    //new Select().From(QheCamchidinhChungphieu.Schema)
                    //    .Where(QheCamchidinhChungphieu.Columns.Loai)
                    //    .IsEqualTo(1)
                    //    .ExecuteDataSet()
                    //    .Tables[0];
                _bhytPtramTraituyennoitru =
                    Utility.DecimaltoDbnull(
                        THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", false), 0m);
                m_intKieudonthuoc = KIEU_THUOC_VT == "THUOC" ? 2 : 1;
                txtCachDung.LOAI_DANHMUC = KIEU_THUOC_VT == "THUOC" ? "CDDT" : "CHIDAN_KEVATTU";
                AutoloadSaveAndPrintConfig();
                LaydanhsachBSKedon();
                LaydanhsachKhotheoBS();
                LaydanhsachMayin();
                txtCachDung.Init();
                GetData();
                GetDataPresDetail();
                txtChanDoan.Init();
                LoadBenh();
                mnuThuoctutuc.Visible = THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb);
                chkTutuc.Visible = THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb);
                if (!chkTutuc.Visible) chkTutuc.Checked = false;
                MaDoiTuong = objLuotkham.MaDoituongKcb;
                SqlQuery sqlkt = null;
                if (objLuotkham.Noitru == 0)
                {
                    sqlkt =
                        new Select().From(KcbChandoanKetluan.Schema)
                            .Where(KcbChandoanKetluan.Columns.IdKham)
                            .IsEqualTo(objRegExam.IdKham);
                }
                else
                {
                    sqlkt =
                        new Select().From(KcbChandoanKetluan.Schema)
                            .Where(KcbChandoanKetluan.Columns.IdPhieudieutri)
                            .IsEqualTo(objPhieudieutriNoitru.IdPhieudieutri);
                }

                if (KcbChandoanKetluan == null || sqlkt.GetRecordCount() <= 0)
                {
                    KcbChandoanKetluan = new KcbChandoanKetluan();
                    KcbChandoanKetluan.IsNew = true;
                }
                else
                {
                    KcbChandoanKetluan.IsNew = false;
                    KcbChandoanKetluan.MarkOld();
                }
                bool gridView =
                    Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("KEDONTHUOC_SUDUNGLUOI", "0", true), 0) ==
                    1;
                if (!gridView)
                {
                    gridView = PropertyLib._AppProperties.GridView;
                }
                txtdrug.GridView = gridView;
                isLoaded = true;
                AllowTextChanged = true;
                blnHasLoaded = true;
                if (KIEU_THUOC_VT == "THUOC")
                    globalVariables.KHOKEDON = PropertyLib._ThamKhamProperties.IDKho;
                else
                    globalVariables.KHOKEDON = PropertyLib._ThamKhamProperties.IDKhoVT;

                if (dtStockList.Select(TDmucKho.Columns.IdKho + "= " + globalVariables.KHOKEDON).Length > 0)
                {
                    cboStock.SelectedIndex = Utility.GetSelectedIndex(cboStock, globalVariables.KHOKEDON.ToString());
                    cboStock_SelectedIndexChanged(cboStock, new EventArgs());
                }
                else
                {
                    cboStock.SelectedIndex = -1;
                }
                if (cboStock.Items.Count == 0)
                {
                    Utility.ShowMsg(
                        string.Format(
                            "Bệnh nhân {0} thuộc đối tượng {1} chưa Có kho " +
                            (KIEU_THUOC_VT == "THUOC" ? "thuốc" : "vật tư") + " để kê đơn", txtPatientName.Text.Trim(),
                            txtObjectName.Text.Trim()));
                    base.Close();
                }
                grdPresDetail.RootTable.Groups.Clear();
                if (chkHienthithuoctheonhom.Checked)
                {
                    GridEXColumn column = grdPresDetail.RootTable.Columns["ten_loaithuoc"];
                    var group = new GridEXGroup(column)
                    {
                        GroupPrefix = "Loại " + (KIEU_THUOC_VT == "THUOC" ? "Thuốc" : "Vật tư") + ": "
                    };
                    grdPresDetail.RootTable.Groups.Add(group);
                }
                txtdrug.Focus();
                txtdrug.Select();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }

        private void GetChanDoan(string ICD_chinh, string IDC_Phu, ref string ICD_Name, ref string ICD_Code)
        {
            try
            {
                List<string> paramValue = ICD_chinh.Split(new[] {','}).ToList();
                DmucBenhCollection benhs =
                    new DmucBenhController().FetchByQuery(
                        DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, paramValue));
                foreach (DmucBenh benh in benhs)
                {
                    ICD_Name = ICD_Name + benh.TenBenh + ";";
                    ICD_Code = ICD_Code + benh.MaBenh + ";";
                }
                paramValue = IDC_Phu.Split(new[] {','}).ToList();
                benhs =
                    new DmucBenhController().FetchByQuery(
                        DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, paramValue));
                foreach (DmucBenh benh in benhs)
                {
                    ICD_Name = ICD_Name + benh.TenBenh + ";";
                    ICD_Code = ICD_Code + benh.MaBenh + ";";
                }
                if (ICD_Name.Trim() != "")
                {
                    ICD_Name = ICD_Name.Substring(0, ICD_Name.Length - 1);
                }
                if (ICD_Code.Trim() != "")
                {
                    ICD_Code = ICD_Code.Substring(0, ICD_Code.Length - 1);
                }
            }
            catch
            {
            }
        }

        private List<KcbDonthuocChitiet> GetChangedData()
        {
            var list = new List<KcbDonthuocChitiet>();
            _temp = ActionResult.Success;
            var chitietArray = new KcbDonthuocChitiet[m_dtDonthuocChitiet.DefaultView.Count];
            try
            {
                foreach (DataRow row in m_dtDonthuocChitiet.Rows)
                {
                    long key = Utility.Int64Dbnull(row[KcbDonthuocChitiet.Columns.IdThuockho], -1);
                    if (lstChangeData.ContainsKey(key))
                    {
                        string str = lstChangeData[key];
                        if (isChanged(str))
                        {
                            decimal num3 = CommonLoadDuoc.SoLuongTonTrongKho(-1L,
                                Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.IdKho], -1),
                                Utility.Int16Dbnull(row[KcbDonthuocChitiet.Columns.IdThuoc], -1), key,
                                Utility.Int32Dbnull(
                                    THU_VIEN_CHUNG.Laygiatrithamsohethong("KIEMTRATHUOC_CHOXACNHAN", "1", false), 1),
                                Utility.ByteDbnull(objLuotkham.Noitru, 0));
                            if (em_Action == action.Update)
                            {
                                int soLuong = 0;
                                DataTable dtChitiet =
                                    SPs.SpKcbLaydulieuChitietDonthuoc(
                                        Utility.Int64Dbnull(row[KcbDonthuocChitiet.Columns.IdChitietdonthuoc], -1))
                                        .GetDataSet()
                                        .Tables[0];
                                if (dtChitiet != null && dtChitiet.Rows.Count > 0)
                                {
                                    soLuong = Utility.Int32Dbnull(dtChitiet.Rows[0]["So_Luong"], 0);
                                }
                                num3 += soLuong;
                            }
                            if (Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong], 0) > num3)
                            {
                                Utility.ShowMsg(
                                    string.Format(
                                        "Số lượng " + (KIEU_THUOC_VT == "THUOC" ? "thuốc" : "vật tư") +
                                        " {0}({1}) vượt quá số lượng tồn trong kho{2}({3}) \nBạn cần chỉnh lại số lượng " +
                                        (KIEU_THUOC_VT == "THUOC" ? "thuốc" : "vật tư") + "!",
                                        new object[]
                                        {
                                            Utility.sDbnull(row[DmucThuoc.Columns.TenThuoc], ""),
                                            Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong], 0).ToString(),
                                            Utility.sDbnull(row[TDmucKho.Columns.TenKho], ""), num3.ToString()
                                        }));
                                _temp = ActionResult.NotEnoughDrugInStock;
                                return null;
                            }
                            hasChanged = true;
                            list.Add(getNewItem(row));
                        }
                    }
                }
                return list;
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi khi lấy dữ liệu cập nhật đơn " + (KIEU_THUOC_VT == "THUOC" ? "thuốc" : "vật tư") +
                                ":\n" + exception.Message);
                return null;
            }
        }

        private string GetContainGuide()
        {
            try
            {
                string yourString = "";
                //   yourString = yourString + this.txtCachDung.Text + " ";
                if (!string.IsNullOrEmpty(txtSoLuongDung.Text))
                {
                    yourString = "Mỗi ngày dùng " + txtSoLuongDung.Text.Trim() + " " + txtDonViDung.Text;
                }
                if (!string.IsNullOrEmpty(txtSolan.Text))
                {
                    string str3 = yourString;
                    yourString = yourString + " chia làm  " + txtSolan.Text + " lần";
                }
                yourString = yourString + " " + txtCachDung.Text;
                //if (!string.IsNullOrEmpty(this.txtChiDanThem.Text))
                //{
                //    yourString = yourString + ". " + this.txtChiDanThem.Text;
                //}
                return Utility.ReplaceString(yourString);
            }
            catch (Exception exception)
            {
                return exception.ToString();
            }
        }

        private string GetDanhsachBenhphu()
        {
            var builder = new StringBuilder("");
            try
            {
                int num = 0;
                if (DtIcd.Rows.Count > 0)
                {
                    foreach (DataRow row in dt_ICD_PHU.Rows)
                    {
                        if (num > 0)
                        {
                            builder.Append(",");
                        }
                        builder.Append(Utility.sDbnull(row[DmucBenh.Columns.MaBenh], ""));
                        num++;
                    }
                }
                return builder.ToString();
            }
            catch
            {
                return "";
            }
        }

        private void GetData()
        {
            if (objLuotkham != null)
            {
                txtSoBHYT.Text = Utility.sDbnull(objLuotkham.MatheBhyt);
                txtPtramBHYT.Text = (objLuotkham.TrangthaiNoitru <= 0
                    ? Utility.sDbnull(objLuotkham.PtramBhyt, "0")
                    : Utility.sDbnull(objLuotkham.PtramBhytGoc, "0")) + " %";
                txtptramdauthe.Text = Utility.sDbnull(objLuotkham.PtramBhytGoc, "0") + " %";
                txtAddress.Text = Utility.sDbnull(objLuotkham.DiaChi);
                txtdiachiBhyt.Text = Utility.sDbnull(objLuotkham.DiachiBhyt);
                DmucDoituongkcb doituongkcb = ReadOnlyRecord<DmucDoituongkcb>.FetchByID(objLuotkham.IdDoituongKcb);
                if (doituongkcb != null)
                {
                    Giathuoc_quanhe = Utility.ByteDbnull(doituongkcb.GiathuocQuanhe, 0) == 1;
                    txtObjectName.Text = Utility.sDbnull(doituongkcb.TenDoituongKcb);
                    chkTutuc.Visible = THU_VIEN_CHUNG.IsBaoHiem(doituongkcb.IdLoaidoituongKcb);
                    chkTutuc.Checked = false;
                    mnuThuoctutuc.Visible = THU_VIEN_CHUNG.IsBaoHiem(doituongkcb.IdLoaidoituongKcb);
                }
                //KcbDanhsachBenhnhan benhnhan = ReadOnlyRecord<KcbDanhsachBenhnhan>.FetchByID(objLuotkham.IdBenhnhan);
                //if (benhnhan != null)
                //{
                //    txtSoDT.Text = Utility.sDbnull(benhnhan.DienThoai);
                //    txtPatientName.Text = Utility.sDbnull(benhnhan.TenBenhnhan);
                //    txtYearBirth.Text = Utility.sDbnull(benhnhan.NamSinh);
                //    txtSex.Text = Utility.sDbnull(benhnhan.GioiTinh);
                //}
            }
        }

        private void GetDataPresDetail()
        {
            KcbDonthuoc donthuoc = ReadOnlyRecord<KcbDonthuoc>.FetchByID(Utility.Int32Dbnull(txtPres_ID.Text));
            if (donthuoc != null)
            {
                IdDonthuoc = Utility.Int32Dbnull(donthuoc.IdDonthuoc);
                barcode.Data = Utility.sDbnull(IdDonthuoc);
                txtLoiDanBS.Text = Utility.sDbnull(donthuoc.LoidanBacsi);
                txtKhamLai.Text = Utility.sDbnull(donthuoc.TaiKham);
                txtBacsi.SetId(Utility.sDbnull(donthuoc.IdBacsiChidinh, ""));
                dtpCreatedDate.Value = donthuoc.NgayKedon;
                if (donthuoc.NgayTaikham != null)
                {
                    chkNgayTaiKham.Checked = true;
                    dtNgayKhamLai.Value = donthuoc.NgayTaikham.Value;
                }
            }
            else
            {
                if (objPhieudieutriNoitru != null)
                    dtpCreatedDate.Value = objPhieudieutriNoitru.NgayDieutri.Value;
                else
                    dtpCreatedDate.Value = globalVariables.SysDate;
            }
            m_dtDonthuocChitiet = _kedonthuoc.Laythongtinchitietdonthuoc(IdDonthuoc);
            CreateViewTable();
            if (!m_dtDonthuocChitiet.Columns.Contains("CHON"))
            {
                m_dtDonthuocChitiet.Columns.Add("CHON", typeof (int));
            }
            UpdateDataWhenChanged();
        }

        private List<int> GetIdChitiet(int id_thuoc, decimal don_gia, ref string s)
        {
            DataRow[] source =
                m_dtDonthuocChitiet.Select(KcbDonthuocChitiet.Columns.IdThuoc + "=" + id_thuoc + "AND " +
                                           KcbDonthuocChitiet.Columns.DonGia + "=" + don_gia);
            if (source.Length > 0)
            {
                IEnumerable<string> enumerable =
                    (from q in source.AsEnumerable()
                        select Utility.sDbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).Distinct<string>();
                s = string.Join(",", enumerable.ToArray());
                return
                    (from q in source.AsEnumerable()
                        select Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).Distinct<int>()
                        .ToList<int>();
            }
            return new List<int>();
        }

        private void getInfor(int id_thuoc, ref string tenthuoc, ref string ten_donvitinh, ref string hoatchat,
            ref string DonviTinh, ref string ChidanThem, ref string MotaThem, ref string CachDung,
            ref string SoluongDung, ref string SolanDung)
        {
            try
            {
                DataRow[] rowArray = m_dtDonthuocChitiet.Select("id_thuoc=" + id_thuoc);
                if (rowArray.Length > 0)
                {
                    tenthuoc = Utility.sDbnull(rowArray[0][DmucThuoc.Columns.TenThuoc], "");
                    ten_donvitinh = Utility.sDbnull(rowArray[0]["ten_donvitinh"], "");
                    DonviTinh = Utility.sDbnull(rowArray[0][KcbDonthuocChitiet.Columns.DonviTinh], "");
                    ChidanThem = Utility.sDbnull(rowArray[0][KcbDonthuocChitiet.Columns.ChidanThem], "");
                    MotaThem = Utility.sDbnull(rowArray[0]["mota_them_chitiet"], "");
                    hoatchat = Utility.sDbnull(rowArray[0][DmucThuoc.Columns.HoatChat], "");
                    CachDung = Utility.sDbnull(rowArray[0][KcbDonthuocChitiet.Columns.CachDung], "");
                    SoluongDung = Utility.sDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoluongDung], "");
                    SolanDung = Utility.sDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SolanDung], "");
                }
            }
            catch
            {
            }
        }

        private int GetMaxSTT(DataTable dataTable)
        {
            try
            {
                return
                    (Utility.Int32Dbnull(
                        dataTable.AsEnumerable().Max(c => c.Field<short>(KcbDonthuocChitiet.Columns.SttIn)), 0) + 1);
            }
            catch (Exception)
            {
                return 1;
            }
        }

        private KcbDonthuocChitiet getNewItem(DataRow drv)
        {
            KcbDonthuocChitiet chitiet;
            return new KcbDonthuocChitiet
            {
                IdDonthuoc = IdDonthuoc,
                IdChitietdonthuoc = Utility.Int32Dbnull(drv[KcbDonthuocChitiet.Columns.IdChitietdonthuoc], -1),
                IdKham = id_kham,
                IdKho = Utility.Int32Dbnull(drv[KcbDonthuocChitiet.Columns.IdKho], -1),
                IdThuoc = Utility.Int16Dbnull(drv[KcbDonthuocChitiet.Columns.IdThuoc], -1),
                TrangthaiThanhtoan = Utility.ByteDbnull(drv[KcbDonthuocChitiet.Columns.TrangthaiThanhtoan], 0),
                SttIn = Utility.Int16Dbnull(drv[KcbDonthuocChitiet.Columns.SttIn], 1),
                TrangthaiHuy = 0,
                IdThuockho = Utility.Int32Dbnull(drv[KcbDonthuocChitiet.Columns.IdThuockho], -1),
                GiaNhap = Utility.DecimaltoDbnull(drv[KcbDonthuocChitiet.Columns.GiaNhap], -1),
                GiaBan = Utility.DecimaltoDbnull(drv[KcbDonthuocChitiet.Columns.GiaBan], -1),
                GiaBhyt = Utility.DecimaltoDbnull(drv[KcbDonthuocChitiet.Columns.GiaBhyt], -1),
                Vat = Utility.DecimaltoDbnull(drv[KcbDonthuocChitiet.Columns.Vat], -1),
                SoLo = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.SoLo], ""),
                SoDky = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.SoDky], ""),
                SoQdinhthau = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.SoQdinhthau], ""),
                MaNhacungcap = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.MaNhacungcap], -1),
                NgayHethan = Utility.ConvertDate(drv["sngay_hethan"].ToString()).Date,
                NgayNhap = Utility.ConvertDate(drv["sngay_nhap"].ToString()).Date,
                SoluongHuy = 0,
                SluongLinh = 0,
                SluongSua = 0,
                IdThanhtoan = -1,
                TrangthaiTonghop = 0,
                MadoituongGia =
                    Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.MadoituongGia], objLuotkham.MadoituongGia),
                TrangthaiChuyen = 0,
                IdGoi = -1,
                TrongGoi = 0,
                NguonThanhtoan = (byte) (noitru == 0 ? 0 : 1),
                TuTuc = Utility.ByteDbnull(drv[KcbDonthuocChitiet.Columns.TuTuc], 0),
                SoLuong = Utility.DecimaltoDbnull(drv[KcbDonthuocChitiet.Columns.SoLuong], 0),
                DonGia = Utility.DecimaltoDbnull(drv[KcbDonthuocChitiet.Columns.DonGia], 0),
                PhuThu = Utility.DecimaltoDbnull(drv[KcbDonthuocChitiet.Columns.PhuThu], 0),
                PhuthuDungtuyen = Utility.DecimaltoDbnull(drv[KcbDonthuocChitiet.Columns.PhuthuDungtuyen], 0),
                PhuthuTraituyen = Utility.DecimaltoDbnull(drv[KcbDonthuocChitiet.Columns.PhuthuTraituyen], 0),
                MotaThem = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.MotaThem], ""),
                TrangthaiBhyt = Utility.ByteDbnull(drv[KcbDonthuocChitiet.Columns.TuTuc], 0),
                TrangThai = 0,
                ChidanThem = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.ChidanThem], ""),
                CachDung = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.CachDung], ""),
                DonviTinh = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.DonviTinh], ""),
                SolanDung = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.SolanDung], null),
                SoluongDung = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.SoluongDung], null),
                NgayTao = globalVariables.SysDate,
                NguoiTao = globalVariables.UserName,
                BhytChitra = Utility.DecimaltoDbnull(drv[KcbDonthuocChitiet.Columns.BhytChitra], 0),
                BnhanChitra = Utility.DecimaltoDbnull(drv[KcbDonthuocChitiet.Columns.BnhanChitra], 0),
                PtramBhyt = Utility.DecimaltoDbnull(drv[KcbDonthuocChitiet.Columns.PtramBhyt], 0),
                PtramBhytGoc = objLuotkham.PtramBhytGoc,
                MaDoituongKcb = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.MaDoituongKcb], "DV"),
                KieuBiendong = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.KieuBiendong], "EXP"),
                DaDung = 0,
                IpMaytao = globalVariables.gv_strIPAddress,
                TenMaytao = globalVariables.gv_strComputerName,
                IpMaysua = globalVariables.gv_strIPAddress,
                TenMaysua = globalVariables.gv_strComputerName
            };
        }

        private void LaydanhsachKhotheoBS()
        {
            try
            {
                dtStockList = new DataTable();
                if (noitru == 0)
                {
                    if (KIEU_THUOC_VT == "THUOC")
                    {
                        dtStockList = CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_LE_NGOAI_TRU_KEDON(objLuotkham.MaDoituongKcb);
                    }
                    else
                    {
                        var lstLoaiBn = new List<string> {"TATCA"};
                        if (noitru == 1)
                            lstLoaiBn.Add("NOITRU");
                        else
                            lstLoaiBn.Add("NGOAITRU");
                        dtStockList = CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_LE(lstLoaiBn);
                    }
                }
                else //Nội trú
                {
                    if (KIEU_THUOC_VT == "THUOC")
                    {
                        dtStockList =
                            CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_TUTHUOC_NOITRU_THEOKHOA((int) objLuotkham.IdKhoanoitru);
                    }
                    else
                    {
                        dtStockList = CommonLoadDuoc.LAYTHONGTIN_VATTU_KHOA((int) objLuotkham.IdKhoanoitru);
                    }
                }
                DataBinding.BindDataCombobox(cboStock, dtStockList, TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho);
                cboStock.SelectedIndex = Utility.GetSelectedIndex(cboStock,
                    PropertyLib._ThamKhamProperties.IDKho.ToString());
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy thông tin combobox");
            }
        }

        private string GetTenBenh(string MaBenh)
        {
            string str = "";
            DataRow[] rowArray =
                globalVariables.gv_dtDmucBenh.Select(string.Format(DmucBenh.Columns.MaBenh + "='{0}'", MaBenh));
            if (rowArray.GetLength(0) > 0)
            {
                str = Utility.sDbnull(rowArray[0][DmucBenh.Columns.TenBenh], "");
            }
            return str;
        }

        private string GetUnitName(string ma)
        {
            try
            {
                DmucChung chung = THU_VIEN_CHUNG.LaydoituongDmucChung("DONVITINH", ma);
                if (chung != null)
                {
                    return chung.Ten;
                }
                return "";
            }
            catch (Exception)
            {
                return "Lượt";
            }
        }

        private void grdPresDetail_CellEdited(object sender, ColumnActionEventArgs e)
        {
            CreateViewTable();
        }

        private void grdPresDetail_CellUpdated(object sender, ColumnActionEventArgs e)
        {
        }

        private void grdPresDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                mnuDelele_Click(mnuDelele, new EventArgs());
            }
        }

        private void grdPresDetail_SelectionChanged(object sender, EventArgs e)
        {
            ModifyButton();
        }

        private void grdPresDetail_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                GridEXRow currentRow = grdPresDetail.CurrentRow;
                if (e.Column.Key == "stt_in")
                {
                    long IdChitietdonthuoc =
                        Utility.Int64Dbnull(currentRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, 0);
                    if (IdChitietdonthuoc > -1)
                        _kedonthuoc.Capnhatchidanchitiet(IdChitietdonthuoc, KcbDonthuocChitiet.Columns.SttIn,
                            e.Value.ToString());
                    grdPresDetail.UpdateData();
                    DataRow[] arrSourceTable =
                        m_dtDonthuocChitiet.Select(KcbDonthuocChitiet.Columns.IdChitietdonthuoc + "=" +
                                                   IdChitietdonthuoc);
                    foreach (DataRow dr in arrSourceTable)
                        dr[KcbDonthuocChitiet.Columns.SttIn] = e.Value;
                    arrSourceTable =
                        m_dtDonthuocChitiet_View.Select(KcbDonthuocChitiet.Columns.IdChitietdonthuoc + "=" +
                                                        IdChitietdonthuoc);
                    foreach (DataRow dr in arrSourceTable)
                        dr[KcbDonthuocChitiet.Columns.SttIn] = e.Value;
                    m_dtDonthuocChitiet.AcceptChanges();
                    m_dtDonthuocChitiet_View.AcceptChanges();
                }
                if (e.Column.Key == "mota_them_chitiet")
                {
                    long IdChitietdonthuoc =
                        Utility.Int64Dbnull(currentRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, 0);
                    if (IdChitietdonthuoc > -1)
                        _kedonthuoc.Capnhatchidanchitiet(IdChitietdonthuoc, KcbDonthuocChitiet.Columns.MotaThem,
                            e.Value.ToString());
                    grdPresDetail.UpdateData();
                    DataRow[] arrSourceTable =
                        m_dtDonthuocChitiet.Select(KcbDonthuocChitiet.Columns.IdChitietdonthuoc + "=" +
                                                   IdChitietdonthuoc);
                    foreach (DataRow dr in arrSourceTable)
                        dr["mota_them_chitiet"] = e.Value;
                    arrSourceTable =
                        m_dtDonthuocChitiet_View.Select(KcbDonthuocChitiet.Columns.IdChitietdonthuoc + "=" +
                                                        IdChitietdonthuoc);
                    foreach (DataRow dr in arrSourceTable)
                        dr["mota_them_chitiet"] = e.Value;
                    m_dtDonthuocChitiet.AcceptChanges();
                    m_dtDonthuocChitiet_View.AcceptChanges();
                }
                else if ((e.Column.Key != KcbDonthuocChitiet.Columns.TuTuc) &&
                         (e.Column.Key == KcbDonthuocChitiet.Columns.SoLuong))
                {
                    Func<DataRow, bool> predicate = null;
                    int id_thuoc = Utility.Int32Dbnull(currentRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, 0);
                    int num = Utility.Int32Dbnull(currentRow.Cells[KcbDonthuocChitiet.Columns.IdThuockho].Value, 0);
                    decimal don_gia =
                        Utility.DecimaltoDbnull(currentRow.Cells[KcbDonthuocChitiet.Columns.DonGia].Value, 0M);
                    hasChanged = true;
                    decimal num2 = Utility.DecimaltoDbnull(e.InitialValue, 0);
                    decimal num3 = Utility.DecimaltoDbnull(e.Value, 0);
                    if (num3 <= 0)
                    {
                        Utility.ShowMsg(
                            "Nhập số lượng =0 hoặc để trống tương đương với việc xóa chi tiết thuốc. Bạn nên nháy chuột phải và chọn Xóa để thực hiện điều này");
                        e.Value = num2;
                        e.Cancel = true;
                        grdPresDetail.Invalidate();
                        return;
                    }
                    decimal num4 = num3 - num2;
                    if (num3 != num2)
                    {
                        if (num3 > num2)
                        {
                            AddQuantity(id_thuoc, num, num3 - num2);
                        }
                        else
                        {
                            if (predicate == null)
                            {
                                predicate =
                                    q =>
                                        (Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdThuoc], 0) == id_thuoc) &&
                                        (Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.DonGia], 0) == don_gia);
                            }
                            DataRow[] rowArray =
                                (from q in m_dtDonthuocChitiet.Select("1=1").AsEnumerable().Where(predicate)
                                    orderby q[KcbDonthuocChitiet.Columns.SttIn] descending
                                    select q).ToArray<DataRow>();
                            decimal num5 = num2 - num3;
                            var dictionary = new Dictionary<int, decimal>();
                            var lstDeleteId = new List<int>();
                            int iddetail = -1;
                            string lstIdChitietDonthuoc = "";
                            for (int i = 0; i <= (rowArray.Length - 1); i++)
                            {
                                if (num5 > 0)
                                {
                                    decimal num8 = Utility.DecimaltoDbnull(rowArray[i][KcbDonthuocChitiet.Columns.SoLuong],
                                        0);
                                    if (num8 >= num5)
                                    {
                                        rowArray[i][KcbDonthuocChitiet.Columns.SoLuong] = num8 - num5;
                                        num5 = num8 - num5;
                                        dictionary.Add(Utility.Int32Dbnull( rowArray[i][KcbDonthuocChitiet.Columns.IdChitietdonthuoc]), num5);
                                        iddetail =
                                            Utility.Int32Dbnull(
                                                rowArray[i][KcbDonthuocChitiet.Columns.IdChitietdonthuoc]);
                                        if (num5 <= 0)
                                        {
                                            lstIdChitietDonthuoc = lstIdChitietDonthuoc +
                                                                   Utility.sDbnull(
                                                                       rowArray[i][
                                                                           KcbDonthuocChitiet.Columns
                                                                               .IdChitietdonthuoc], "-1") + ",";
                                        }
                                        break;
                                    }
                                    rowArray[i][KcbDonthuocChitiet.Columns.SoLuong] = 0;
                                    lstIdChitietDonthuoc = lstIdChitietDonthuoc +
                                                           Utility.sDbnull(
                                                               rowArray[i][
                                                                   KcbDonthuocChitiet.Columns.IdChitietdonthuoc],
                                                               "-1") + ",";
                                    lstDeleteId.Add(
                                        Utility.Int32Dbnull(
                                            rowArray[i][KcbDonthuocChitiet.Columns.IdChitietdonthuoc]));
                                    num5 -= num8;
                                }
                            }
                            _kedonthuoc.XoaChitietDonthuoc(lstIdChitietDonthuoc, iddetail, num5);
                            grdPresDetail.UpdateData();
                            deletefromDatatable(lstDeleteId, iddetail, num5);
                        }
                        decimal num9 =
                            Utility.DecimaltoDbnull(
                                m_dtDonthuocChitiet_View.Select(KcbDonthuocChitiet.Columns.IdThuoc + "=" + id_thuoc +
                                                                " AND " + KcbDonthuocChitiet.Columns.DonGia + "=" +
                                                                don_gia)[0][KcbDonthuocChitiet.Columns.SoLuong], 0);
                        if (num4 > 0)
                        {
                            e.Value = num9;
                        }
                        else
                        {
                            num9 = Utility.DecimaltoDbnull(e.Value, 0);
                            e.Value = e.Value;
                        }
                        DataRow[] rowArray2 =
                            m_dtDonthuocChitiet_View.Select(KcbDonthuocChitiet.Columns.IdThuoc + "=" + id_thuoc);
                        foreach (DataRow row2 in rowArray2)
                        {
                            if ((row2[KcbDonthuocChitiet.Columns.IdThuoc].ToString() == id_thuoc.ToString()) &&
                                (Utility.DecimaltoDbnull(row2[KcbDonthuocChitiet.Columns.DonGia], 0M) == don_gia))
                            {
                                row2[KcbDonthuocChitiet.Columns.SoLuong] = num9;
                            }
                            decimal num10 = Utility.DecimaltoDbnull(row2[KcbDonthuocChitiet.Columns.SoLuong], 0);
                            if (num10 > 0)
                            {
                                row2["TT_KHONG_PHUTHU"] = num10*
                                                          Utility.DecimaltoDbnull(
                                                              row2[KcbDonthuocChitiet.Columns.DonGia]);
                                row2["TT"] = num10*
                                             (Utility.DecimaltoDbnull(row2[KcbDonthuocChitiet.Columns.DonGia]) +
                                              Utility.DecimaltoDbnull(row2[KcbDonthuocChitiet.Columns.PhuThu]));
                                row2["TT_BHYT"] = num10*
                                                  Utility.DecimaltoDbnull(
                                                      row2[KcbDonthuocChitiet.Columns.BhytChitra]);
                                row2["TT_BN"] = num10*
                                                (Utility.DecimaltoDbnull(
                                                    row2[KcbDonthuocChitiet.Columns.BnhanChitra], 0) +
                                                 Utility.DecimaltoDbnull(row2[KcbDonthuocChitiet.Columns.PhuThu], 0));
                                row2["TT_PHUTHU"] = num10*
                                                    Utility.DecimaltoDbnull(
                                                        row2[KcbDonthuocChitiet.Columns.PhuThu], 0);
                                row2["TT_BN_KHONG_PHUTHU"] = num10*
                                                             Utility.DecimaltoDbnull(
                                                                 row2[KcbDonthuocChitiet.Columns.BnhanChitra], 0);
                            }
                            else
                            {
                                m_dtDonthuocChitiet_View.Rows.Remove(row2);
                            }
                        }
                        m_dtDonthuocChitiet_View.AcceptChanges();
                    }
                }
            }
            catch
            {
            }
        }

        private void grdPresDetail_UpdatingCell_old(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                if (e.Column.Key == KcbDonthuocChitiet.Columns.SoLuong)
                {
                    hasChanged = true;
                    string str = "";
                    long key =
                        Utility.Int64Dbnull(
                            grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdThuockho].Value, -1);
                    if (lstChangeData.ContainsKey(key))
                    {
                        str = lstChangeData[key];
                        str = str.Split(new[] {'-'})[0] + "-" + e.Value;
                        lstChangeData[key] = str;
                    }
                    else
                    {
                        str = e.InitialValue + "-" + e.Value;
                        lstChangeData.Add(key, str);
                    }
                    DataRow[] rowArray = m_dtDonthuocChitiet.Select(KcbDonthuocChitiet.Columns.IdThuockho + "=" + key);
                    int num2 = Utility.Int32Dbnull(e.Value,
                        Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]));
                    if (rowArray.Length > 0)
                    {
                        rowArray[0][KcbDonthuocChitiet.Columns.BhytChitra] = num2*
                                                                             Utility.DecimaltoDbnull(
                                                                                 rowArray[0][
                                                                                     KcbDonthuocChitiet.Columns
                                                                                         .BhytChitra]);
                        rowArray[0][KcbDonthuocChitiet.Columns.BnhanChitra] = num2*
                                                                              (Utility.DecimaltoDbnull(
                                                                                  rowArray[0][
                                                                                      KcbDonthuocChitiet.Columns
                                                                                          .BnhanChitra], 0) +
                                                                               Utility.DecimaltoDbnull(
                                                                                   rowArray[0][
                                                                                       KcbDonthuocChitiet.Columns.PhuThu
                                                                                       ], 0));
                        rowArray[0]["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(num2)*
                                                         Utility.DecimaltoDbnull(
                                                             rowArray[0][KcbDonthuocChitiet.Columns.DonGia]);
                        rowArray[0]["TT"] = Utility.Int32Dbnull(num2)*
                                            (Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.DonGia]) +
                                             Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu]));
                        rowArray[0]["TT_BHYT"] = Utility.Int32Dbnull(num2)*
                                                 Utility.DecimaltoDbnull(
                                                     rowArray[0][KcbDonthuocChitiet.Columns.BhytChitra]);
                        rowArray[0]["TT_BN"] = Utility.Int32Dbnull(num2)*
                                               (Utility.DecimaltoDbnull(
                                                   rowArray[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0) +
                                                Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu],
                                                    0));
                        rowArray[0]["TT_PHUTHU"] = Utility.Int32Dbnull(num2)*
                                                   Utility.DecimaltoDbnull(
                                                       rowArray[0][KcbDonthuocChitiet.Columns.PhuThu], 0);
                        rowArray[0]["TT_BN_KHONG_PHUTHU"] = Utility.Int32Dbnull(num2)*
                                                            Utility.DecimaltoDbnull(
                                                                rowArray[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0);
                    }
                    m_dtDonthuocChitiet.AcceptChanges();
                }
            }
            catch
            {
            }
        }

        private void InitEvents()
        {
            base.Load += frm_KCB_KE_DONTHUOC_Load;
            base.KeyDown += frm_KCB_KE_DONTHUOC_KeyDown;
            base.FormClosing += frm_KCB_KE_DONTHUOC_FormClosing;
            grdPresDetail.KeyDown += grdPresDetail_KeyDown;
            grdPresDetail.UpdatingCell += grdPresDetail_UpdatingCell;
            grdPresDetail.CellEdited += grdPresDetail_CellEdited;
            grdPresDetail.CellUpdated += grdPresDetail_CellUpdated;
            grdPresDetail.SelectionChanged += grdPresDetail_SelectionChanged;
            txtPres_ID.TextChanged += txtPres_ID_TextChanged;
            txtSoluong.TextChanged += txtSoluong_TextChanged;
            txtDrugID.TextChanged += txtDrugID_TextChanged;
            txtSolan.TextChanged += txtSolan_TextChanged;
            txtSoLuongDung.TextChanged += txtSoLuongDung_TextChanged;
            txtCachDung._OnSelectionChanged += txtCachDung__OnSelectionChanged;
            txtCachDung.TextChanged += txtCachDung_TextChanged;
            chkSaveAndPrint.CheckedChanged += chkSaveAndPrint_CheckedChanged;
            chkNgayTaiKham.CheckedChanged += chkNgayTaiKham_CheckedChanged;
            mnuDelele.Click += mnuDelele_Click;
            cmdSavePres.Click += cmdSavePres_Click;
            cmdExit.Click += cmdExit_Click;
            cmdDelete.Click += cmdDelete_Click;
            cmdDonThuocDaKe.Click += cmdDonThuocDaKe_Click;
            cmdPrintPres.Click += cmdPrintPres_Click;
            cmdAddDetail.Click += cmdAddDetail_Click;
            cmdCauHinh.Click += cmdCauHinh_Click;
            cboStock.SelectedIndexChanged += cboStock_SelectedIndexChanged;
            txtdrug._OnGridSelectionChanged += txtdrug__OnGridSelectionChanged;
            cboPrintPreview.SelectedIndexChanged += cboPrintPreview_SelectedIndexChanged;
            cboA4.SelectedIndexChanged += cboA4_SelectedIndexChanged;
            cboLaserPrinters.SelectedIndexChanged += cboLaserPrinters_SelectedIndexChanged;
            chkHienthithuoctheonhom.CheckedChanged += chkHienthithuoctheonhom_CheckedChanged;
            chkAskbeforeDeletedrug.CheckedChanged += chkAskbeforeDeletedrug_CheckedChanged;
            txtMaBenhChinh.KeyDown += txtMaBenhChinh_KeyDown;
            txtMaBenhChinh.TextChanged += txtMaBenhChinh_TextChanged;
            txtMaBenhphu.GotFocus += txtMaBenhphu_GotFocus;
            txtMaBenhphu.KeyDown += txtMaBenhphu_KeyDown;
            txtMaBenhphu.TextChanged += txtMaBenhphu_TextChanged;
            mnuThuoctutuc.Click += mnuThuoctutuc_Click;
            txtCachDung._OnShowData += txtCachDung__OnShowData;
            txtCachDung._OnSaveAs += txtCachDung__OnSaveAs;
            txtdrug._OnChangedView += txtdrug__OnChangedView;
            txtdrug._OnEnterMe += txtdrug__OnEnterMe;
            grd_ICD.ColumnButtonClick += grd_ICD_ColumnButtonClick;
            cmdSearchBenhChinh.Click += cmdSearchBenhChinh_Click;
            cmdSearchBenhPhu.Click += cmdSearchBenhPhu_Click;
            cmdLuuchidan.Click += cmdLuuchidan_Click;
        }

        private void cmdLuuchidan_Click(object sender, EventArgs e)
        {
            Utility.SetMsg(lblMsg, "", false);
            if (txtdrug.MyCode == "-1")
            {
                Utility.SetMsg(lblMsg, "Bạn cần chọn thuốc trước khi lưu chỉ dẫn dùng thuốc", true);
                txtdrug.Focus();
                return;
            }
            try
            {
                var objChidan =
                    new Select().From(DmucChidanKedonthuoc.Schema)
                        .Where(DmucChidanKedonthuoc.Columns.IdThuoc)
                        .IsEqualTo(txtDrugID.Text)
                        .And(DmucChidanKedonthuoc.Columns.IdBacsi)
                        .IsEqualTo(globalVariables.gv_intIDNhanvien)
                        .ExecuteSingle<DmucChidanKedonthuoc>();
                if (objChidan == null)
                {
                    objChidan = new DmucChidanKedonthuoc();
                    objChidan.IsNew = true;
                }
                else
                {
                    objChidan.IsNew = false;
                    objChidan.MarkOld();
                }
                objChidan.IdBacsi = globalVariables.gv_intIDNhanvien;
                objChidan.IdThuoc = Utility.Int32Dbnull(txtdrug.MyID, -1);
                objChidan.SolanDung = Utility.DoTrim(txtSolan.Text);
                objChidan.SoluongDung = Utility.DoTrim(txtSoLuongDung.Text);
                objChidan.SoLuong = Utility.Int32Dbnull(Utility.DecimaltoDbnull(txtSoluong.Text, 0));
                objChidan.ChidanThem = Utility.DoTrim(txtChiDanThem.Text);
                objChidan.CachDung = Utility.DoTrim(txtCachDung.Text);
                //   objChidan.DonviDung = Utility.DoTrim(txtDonViDung.Text);
                objChidan.Save();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cmdSearchBenhChinh_Click(object sender, EventArgs e)
        {
            ShowDiseaseList(txtMaBenhphu);
        }

        private void cmdSearchBenhPhu_Click(object sender, EventArgs e)
        {
            ShowDiseaseList(txtMaBenhphu);
        }

        private void ShowDiseaseList(EditBox txt)
        {
            try
            {
                var frm = new frm_QuickSearchDiseases();
                frm.DiseaseType_ID = -1;
                frm.ShowDialog();
                if (frm.m_blnCancel)
                {
                    txt.Text = frm.v_DiseasesCode;
                    txt.Focus();
                    txt.SelectAll();
                }
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
            }
        }

        private void txtdrug__OnEnterMe()
        {
            if (Utility.Int32Dbnull(txtdrug.MyID, -1) <= 0) return;
            _allowDrugChanged = true;
            txtDrugID_TextChanged(txtDrugID, new EventArgs());
            AutoFill_Chidandungthuoc();
            txtSoluong.Focus();
            txtSoluong.SelectAll();
        }

        private void grd_ICD_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            try
            {
                if (e.Column.Key == "XOA")
                {
                    grd_ICD.CurrentRow.Delete();
                    dt_ICD_PHU.AcceptChanges();
                    grd_ICD.Refetch();
                    grd_ICD.AutoSizeColumns();
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình xóa thông tin Mã ICD");
                throw;
            }
            finally
            {
            }
        }


        private void ThemDonthuoc()
        {
            try
            {
                var lstChitietDonthuoc = new Dictionary<long, long>();
                KcbDonthuocChitiet[] arrDonthuocChitiet = CreateArrayPresDetail();
                if (arrDonthuocChitiet != null)
                {
                    _actionResult = _kedonthuoc.ThemDonThuoc(objLuotkham, TaoDonthuoc(), arrDonthuocChitiet,
                        KcbChandoanKetluan, ref IdDonthuoc, ref lstChitietDonthuoc);
                    switch (_actionResult)
                    {
                        case ActionResult.Error:
                            setMsg(lblMsg,
                                "Lỗi trong quá trình lưu đơn " + (KIEU_THUOC_VT == "THUOC" ? "thuốc" : "vật tư"), true);
                            break;

                        case ActionResult.Success:
                            UpdateChiDanThem();
                            txtPres_ID.Text = IdDonthuoc.ToString();
                            em_Action = action.Update;
                            setMsg(lblMsg,
                                "Bạn thực hiện lưu đơn " + (KIEU_THUOC_VT == "THUOC" ? "thuốc" : "vật tư") +
                                " thành công", false);
                            UpdateDetailID(lstChitietDonthuoc);
                            m_blnCancel = false;
                            break;
                    }
                }
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
            }
            finally
            {
                if (Manual)
                {
                    em_Action = action.Update;
                }
            }
        }

        private void ThemDonthuoc(KcbDonthuocChitiet[] arrPresDetail)
        {
            try
            {
                var lstChitietDonthuoc = new Dictionary<long, long>();
                if (arrPresDetail != null)
                {
                    _actionResult = _kedonthuoc.ThemDonThuoc(objLuotkham, TaoDonthuoc(), arrPresDetail,
                        KcbChandoanKetluan, ref IdDonthuoc, ref lstChitietDonthuoc);
                    switch (_actionResult)
                    {
                        case ActionResult.Error:
                            setMsg(lblMsg,
                                "Lỗi trong quá trình lưu đơn " + (KIEU_THUOC_VT == "THUOC" ? "thuốc" : "vật tư"), true);
                            break;

                        case ActionResult.Success:
                            UpdateChiDanThem();
                            txtPres_ID.Text = IdDonthuoc.ToString();
                            em_Action = action.Update;
                            setMsg(lblMsg,
                                "Bạn thực hiện lưu đơn " + (KIEU_THUOC_VT == "THUOC" ? "Thuốc" : "Vật tư") +
                                " thành công", false);
                            UpdateDetailID(lstChitietDonthuoc);
                            m_blnCancel = false;
                            break;
                    }
                }
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
            }
            finally
            {
                if (Manual)
                {
                    em_Action = action.Update;
                }
            }
        }

        private bool InValiMaBenh(string mabenh)
        {
            return
                globalVariables.gv_dtDmucBenh.AsEnumerable()
                    .Where(benh => (Utility.sDbnull(benh[DmucBenh.Columns.MaBenh]) == Utility.sDbnull(mabenh)))
                    .Any();
        }

        private bool isChanged(string value)
        {
            string[] strArray = value.Split(new[] {'-'});
            if (strArray.Length != 2)
            {
                return false;
            }
            return (Utility.Int32Dbnull(strArray[0], 0) != Utility.Int32Dbnull(strArray[1], 0));
        }

        private void Load_DSach_ICD()
        {
            try
            {
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy danh sách ICD");
            }
        }

        private void LoadBenh()
        {
            try
            {
                AllowTextChanged = true;
                isLike = false;
                txtChanDoan._Text = Utility.sDbnull(_Chandoan);
                txtMaBenhChinh.Text = Utility.sDbnull(_MabenhChinh);
                isLike = true;
                AllowTextChanged = false;
                grd_ICD.DataSource = dt_ICD_PHU;
            }
            catch
            {
            }
        }

        private void LaydanhsachMayin()
        {
            if (!string.IsNullOrEmpty(PropertyLib._MayInProperties.TenMayInBienlai))
            {
                PropertyLib._MayInProperties.TenMayInBienlai = Utility.GetDefaultPrinter();
            }
            if (PropertyLib._ThamKhamProperties != null)
            {
                try
                {
                    cboLaserPrinters.Items.Clear();
                    for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
                    {
                        string item = PrinterSettings.InstalledPrinters[i];
                        cboLaserPrinters.Items.Add(item);
                    }
                }
                catch
                {
                }
                finally
                {
                    cboLaserPrinters.Text = PropertyLib._MayInProperties.TenMayInBienlai;
                }
            }
        }

        private void mnuDelele_Click(object sender, EventArgs e)
        {
            try
            {
                setMsg(lblMsg, "", false);
                if ((grdPresDetail.RowCount <= 0) || (grdPresDetail.CurrentRow.RowType != RowType.Record))
                {
                    setMsg(lblMsg, "Bạn phải chọn " + (KIEU_THUOC_VT == "THUOC" ? "thuốc" : "vật tư") + " để xóa", true);
                    grdPresDetail.Focus();
                }
                else
                {
                    int num =
                        Utility.Int32Dbnull(
                            grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, -1);
                    string s = "";
                    List<int> vals =
                        GetIdChitiet(
                            Utility.Int32Dbnull(
                                grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, -1),
                            Utility.DecimaltoDbnull(
                                grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.DonGia].Value, -1), ref s);
                    DataTable dtTempt =
                        SPs.SpKcbKiemtraThanhtoanChitietDonthuoc(String.Join(",", vals.ToArray())).GetDataSet().Tables[0
                            ];
                    if (dtTempt != null && dtTempt.Rows.Count > 0)
                    {
                        setMsg(lblMsg,
                            "Hệ thống phát hiện một số chi tiết đơn thuốc bạn chọn xóa đã được thanh toán nên bạn không thể xóa. Đề nghị kiểm tra lại",
                            true);
                        grdPresDetail.Focus();
                    }
                    else if (!PropertyLib._ThamKhamProperties.Hoitruockhixoathuoc ||
                             Utility.AcceptQuestion(
                                 "Bạn Có muốn xóa các " + (KIEU_THUOC_VT == "THUOC" ? "thuốc" : "vật tư") +
                                 " đang chọn hay không?", "thông báo xóa", true))
                    {
                        _kedonthuoc.XoaChitietDonthuoc(s);
                        THU_VIEN_CHUNG.Log(Name, globalVariables.UserName,
                            string.Format(
                                "Xóa thuốc có mã là: {0} - đơn thuôc: {3} của bệnh nhân có mã lần khám: {1} và mã bệnh nhân là: {2}",
                                Utility.Int32Dbnull(
                                    grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, -1),
                                objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, Utility.Int32Dbnull(
                                    grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value, -1)),
                            action.Delete);
                        grdPresDetail.CurrentRow.Delete();
                        grdPresDetail.UpdateData();
                        deletefromDatatable(vals);
                        m_blnCancel = false;
                        UpdateDataWhenChanged();
                    }
                }
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
            }
            finally
            {
                if (grdPresDetail.RowCount <= 0)
                {
                    em_Action = action.Insert;
                }
            }
        }

        private void mnuThuoctutuc_Click(object sender, EventArgs e)
        {
            Updatethuoctutuc();
        }

        private void ModifyButton()
        {
            try
            {
                cmdSavePres.Enabled = grdPresDetail.RowCount > 0;
                cmdPrintPres.Enabled = (Utility.isValidGrid(grdPresDetail) &&
                                        PropertyLib._ThamKhamProperties.ChophepIndonthuoc) &&
                                       (grdPresDetail.RowCount > 0);
                cmdDelete.Enabled = Utility.isValidGrid(grdPresDetail);
                cmdAddDetail.Enabled = Utility.Int32Dbnull(txtDrugID.Text) > 0;
                mnuThuoctutuc.Enabled = Utility.isValidGrid(grdPresDetail);
            }
            catch (Exception)
            {
            }
        }

        private void PerformAction()
        {
            switch (em_Action)
            {
                case action.Insert:
                    ThemDonthuoc();
                    break;

                case action.Update:
                    CapnhatDonthuoc();
                    break;
            }
        }

        private void PerformAction(KcbDonthuocChitiet[] arrPresDetail)
        {
            isSaved = true;

            Create_ChandoanKetluan();

            switch (em_Action)
            {
                case action.Insert:
                    ThemDonthuoc(arrPresDetail);
                    break;

                case action.Update:
                    CapnhatDonthuoc(arrPresDetail);
                    break;
            }
        }

        private void PrintPres(int PresID)
        {
            DataTable dataTable = _kedonthuoc.LaythongtinDonthuoc_In(PresID);
            if (dataTable.Rows.Count <= 0)
            {
                Utility.ShowMsg(
                    "không tìm  thấy " + (KIEU_THUOC_VT == "THUOC" ? "thuốc" : "vật tư") + ", Có thể bạn chưa lưu được " +
                    (KIEU_THUOC_VT == "THUOC" ? "thuốc" : "vật tư") + ", \nMời bạn kiểm tra lại", "thông báo",
                    MessageBoxIcon.Exclamation);
                return;
            }
            Utility.AddColumToDataTable(ref dataTable, "BarCode", typeof (byte[]));
            THU_VIEN_CHUNG.CreateXML(dataTable, "thamkham_InDonthuocA4.xml");
            barcode.Data = Utility.sDbnull(txtPres_ID.Text);
            byte[] buffer = Utility.GenerateBarCode(barcode);
            string str = "";
            string str2 = "";
            if ((dataTable != null) && (dataTable.Rows.Count > 0))
            {
                GetChanDoan(Utility.sDbnull(dataTable.Rows[0]["mabenh_chinh"], ""),
                    Utility.sDbnull(dataTable.Rows[0]["mabenh_phu"], ""), ref str, ref str2);
            }
            foreach (DataRow row in dataTable.Rows)
            {
                row["BarCode"] = buffer;
                row["chan_doan"] = (Utility.sDbnull(row["chan_doan"]).Trim() == "")
                    ? str
                    : (Utility.sDbnull(row["chan_doan"]) + ";" + str);
                row["ma_icd"] = str2;
            }
            dataTable.AcceptChanges();
            Utility.UpdateLogotoDatatable(ref dataTable);
            string str3 = "A5";
            if (PropertyLib._MayInProperties.CoGiayInDonthuoc == Papersize.A4)
            {
                str3 = "A4";
            }
            var document = new ReportDocument();
            string tieude = "";
            string fileName = "";
            string str6 = str3;
            if (str6 != null)
            {
                if (!(str6 == "A5"))
                {
                    if (str6 == "A4")
                    {
                        document = Utility.GetReport("thamkham_InDonthuocA4", ref tieude, ref fileName);
                        goto Label_0252;
                    }
                }
                else
                {
                    document = Utility.GetReport("thamkham_InDonthuocA5", ref tieude, ref fileName);
                    goto Label_0252;
                }
            }
            document = Utility.GetReport("thamkham_InDonthuocA5", ref tieude, ref fileName);
            Label_0252:
            if (document != null)
            {
                Utility.WaitNow(this);
                ReportDocument rptDoc = document;
                var preview =
                    new frmPrintPreview("IN ĐƠN " + (KIEU_THUOC_VT == "THUOC" ? "thuốc" : "vật tư") + " BỆNH NHÂN",
                        rptDoc, true, true);
                try
                {
                    rptDoc.SetDataSource(dataTable);
                    Utility.SetParameterValue(rptDoc, "ParentBranchName", globalVariables.ParentBranch_Name);
                    Utility.SetParameterValue(rptDoc, "BranchName", globalVariables.Branch_Name);
                    Utility.SetParameterValue(rptDoc, "Address", globalVariables.Branch_Address);
                    Utility.SetParameterValue(rptDoc, "Phone", globalVariables.Branch_Phone);
                    Utility.SetParameterValue(rptDoc, "ReportTitle",
                        "ĐƠN " + (KIEU_THUOC_VT == "THUOC" ? "THUỐC" : "VẬT TƯ"));
                    Utility.SetParameterValue(rptDoc, "CurrentDate", Utility.FormatDateTime(dtNgayIn.Value));
                    Utility.SetParameterValue(rptDoc, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                    preview.crptViewer.ReportSource = rptDoc;
                    if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai,
                        PropertyLib._MayInProperties.PreviewInDonthuoc))
                    {
                        preview.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
                        preview.ShowDialog();
                        cboLaserPrinters.Text = PropertyLib._MayInProperties.TenMayInBienlai;
                    }
                    else
                    {
                        preview.addTrinhKy_OnFormLoad();
                        rptDoc.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                        rptDoc.PrintToPrinter(1, false, 0, 0);
                    }
                    Utility.DefaultNow(this);
                }
                catch (Exception)
                {
                    Utility.DefaultNow(this);
                }
            }
        }

        private void SaveDefaultPrinter()
        {
            try
            {
                PropertyLib._MayInProperties.TenMayInBienlai = Utility.sDbnull(cboLaserPrinters.Text);
                PropertyLib.SaveProperty(PropertyLib._MayInProperties);
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi khi lưu trạng thái-->" + exception.Message);
            }
        }

        private void SaveMe()
        {
            try
            {
                cmdSavePres.Enabled = false;
                if (grdPresDetail.RowCount <= 0)
                {
                    setMsg(lblMsg, "Hiện chưa Có bản ghi nào để thực hiện  lưu lại", true);
                    txtdrug.Focus();
                    txtdrug.SelectAll();
                }
                else
                {
                    isSaved = true;
                    switch (em_CallAction)
                    {
                        case CallAction.FromMenu:
                            if (hasChanged)
                            {
                                PerformAction();
                            }
                            if (!((_temp == ActionResult.NotEnoughDrugInStock) || Manual))
                            {
                                base.Close();
                            }
                            return;

                        case CallAction.FromParentFormList:
                            m_blnCancel = false;
                            if (!Manual)
                            {
                                base.Close();
                            }
                            return;

                        case CallAction.FromAnotherForm:
                            m_blnCancel = false;
                            if (hasChanged)
                            {
                                PerformAction();
                            }
                            if (!((_temp == ActionResult.NotEnoughDrugInStock) || Manual))
                            {
                                base.Close();
                            }
                            return;
                    }
                }
            }
            catch
            {
            }
            finally
            {
                cmdSavePres.Enabled = true;
                Manual = false;
                hasChanged = false;
            }
        }

        private void setMsg(Label item, string msg, bool isError)
        {
            try
            {
                item.Text = msg;
                if (isError)
                {
                    item.ForeColor = Color.Red;
                }
                else
                {
                    item.ForeColor = Color.DarkBlue;
                }
                Application.DoEvents();
            }
            catch
            {
            }
        }


        private void txtCachDung__OnSaveAs()
        {
        }

        private void txtCachDung__OnSelectionChanged()
        {
            ChiDanThuoc();
        }

        private void txtCachDung__OnShowData()
        {
            var _DMUC_DCHUNG = new DMUC_DCHUNG(txtCachDung.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtCachDung.myCode;
                txtCachDung.Init();
                txtCachDung.SetCode(oldCode);
                txtCachDung.Focus();
            }
        }

        private void txtCachDung_TextChanged(object sender, EventArgs e)
        {
            ChiDanThuoc();
        }

        private void txtdrug__OnChangedView(bool gridview)
        {
            PropertyLib._AppProperties.GridView = gridview;
            PropertyLib.SaveProperty(PropertyLib._AppProperties);
        }

        private void txtdrug__OnGridSelectionChanged(string ID, int id_thuockho, string _name, string Dongia,
            string phuthu, int tutuc)
        {
            this.id_thuockho = id_thuockho;
            _allowDrugChanged = false;
            txtDrugID.Text = ID;
            txtPrice.Text = Dongia;
            txtSurcharge.Text = phuthu;
        }

        private void AutoFill_Chidandungthuoc()
        {
            try
            {
                _autoFill = false;
                var objChidan =
                    new Select().From(DmucChidanKedonthuoc.Schema)
                        .Where(DmucChidanKedonthuoc.Columns.IdThuoc)
                        .IsEqualTo(txtDrugID.Text)
                        .And(DmucChidanKedonthuoc.Columns.IdBacsi)
                        .IsEqualTo(globalVariables.gv_intIDNhanvien)
                        .ExecuteSingle<DmucChidanKedonthuoc>();
                if (objChidan != null)
                {
                    _autoFill = true;
                    txtSoluong.Text = objChidan.SoLuong.ToString();
                    txtSoLuongDung.Text = objChidan.SoluongDung;
                    txtSolan.Text = objChidan.SolanDung;
                    txtCachDung._Text = objChidan.CachDung;
                    txtChiDanThem.Text = objChidan.ChidanThem;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void txtDrugID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!_allowDrugChanged) return;
                m_decPrice = 0M;
                m_Surcharge = 0M;
                AllowTextChanged = false;
                Utility.SetMsg(lblMsg, "", false);
                if ((Utility.DoTrim(txtDrugID.Text) == "") || (Utility.Int32Dbnull(txtDrugID.Text, -1) < 0))
                {
                    m_decPrice = 0M;
                    //this.tu_tuc = 0;
                    txtDrugID.Text = "";
                    txtDrug_Name.Text = "";
                    txtBietduoc.Clear();
                    txtDonViTinh.Clear();
                    txtDonViDung.Clear();
                    txtTinhchat.Text = "0";
                    txtGioihanke.Text = "";
                    txtDonvichiaBut.Text = "";
                    txtMotathem.Clear();
                    txtSurcharge.Text = "0";
                    txtPrice.Text = "0";
                    txtdrugtypeCode.Clear();
                    txttenloaithuoc.Clear();
                    cmdAddDetail.Enabled = false;
                    return;
                }
                DataRow[] rowArray = m_dtDanhmucthuoc.Select(DmucThuoc.Columns.IdThuoc + "=" + txtDrugID.Text);
                if (rowArray.Length > 0)
                {
                    madoituong_gia = rowArray[0]["madoituong_gia"].ToString();
                    txtTonKho.Text =
                        CommonLoadDuoc.SoLuongTonTrongKho(-1L, Utility.Int32Dbnull(cboStock.SelectedValue),
                            Utility.Int32Dbnull(txtDrugID.Text, -1),
                            txtdrug.GridView
                                ? id_thuockho
                                : txtdrug.id_thuockho,
                            Utility.Int32Dbnull(
                                THU_VIEN_CHUNG.Laygiatrithamsohethong(
                                    "KIEMTRATHUOC_CHOXACNHAN", "1", false), 1),
                            Utility.ByteDbnull(objLuotkham.Noitru, 0)).ToString();
                    txtDonViTinh.Text = rowArray[0]["ten_donvitinh"].ToString();
                    txtDonViDung.Text = rowArray[0]["ten_donvidung"].ToString();
                    txtDrug_Name.Text = rowArray[0][DmucThuoc.Columns.TenThuoc].ToString();
                    txtBietduoc.Text = rowArray[0][DmucThuoc.Columns.HoatChat].ToString();
                    txtTinhchat.Text = rowArray[0][DmucThuoc.Columns.TinhChat].ToString();
                    if (Utility.Int64Dbnull(txtTonKho.Text) <= 500)
                        txtTonKho.BackColor = Color.OrangeRed;
                    else txtTonKho.BackColor = Color.SteelBlue;
                    txtGioihanke.Text = Utility.sDbnull(rowArray[0][DmucThuoc.Columns.GioihanKedon], "");
                    txtMotathem.Text = rowArray[0][DmucThuoc.Columns.MotaThem].ToString();
                    dtExpire_Date.Value = Convert.ToDateTime(rowArray[0]["ngay_hethan"]);
                    txtDonvichiaBut.Text = Utility.sDbnull(rowArray[0][DmucThuoc.Columns.DonviBut], "");
                    txtPrice.Text = rowArray[0]["GIA_BAN"].ToString();
                    //this.tu_tuc = Utility.Int32Dbnull(rowArray[0]["tu_tuc"], 0);
                    txtSurcharge.Text = rowArray[0]["PHU_THU"].ToString();
                    txtdrugtypeCode.Text = rowArray[0][DmucLoaithuoc.Columns.MaLoaithuoc].ToString();
                    txttenloaithuoc.Text = rowArray[0][DmucLoaithuoc.Columns.TenLoaithuoc].ToString();
                    if (txtTinhchat.Text == "1")
                        if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_NOIDUNGCANHBAO_THUOCDOCHAI", "0", false) == "1")
                            Utility.SetMsg(lblMsg,
                                THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_NOIDUNGCANHBAO_THUOCDOCHAI",
                                    Utility.DoTrim(txtMotathem.Text) == ""
                                        ? "Chú ý: THUỐC CÓ TÍNH CHẤT ĐỘC HẠI"
                                        : Utility.DoTrim(txtMotathem.Text), false), true);
                        else
                            Utility.SetMsg(lblMsg, "", false);
                    if (Utility.Int32Dbnull(txtSoluong.Text) > 0)
                        cmdAddDetail.Enabled = true;
                }
                else
                {
                    madoituong_gia = "DV";
                    m_decPrice = 0M;
                    //this.tu_tuc = 0;
                    txtDrugID.Text = "";
                    txtDrug_Name.Text = "";
                    txtDonViTinh.Clear();
                    txtDonViDung.Clear();
                    txtTinhchat.Text = "0";
                    txtGioihanke.Text = "";
                    txtDonvichiaBut.Text = "";
                    txtMotathem.Clear();
                    txtSurcharge.Text = "0";
                    txtPrice.Text = "0";
                    cmdAddDetail.Enabled = false;
                }
                m_blnGetDrugCodeFromList = false;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                //this.chkTutuc.Checked = this.tu_tuc == 1;
                AllowTextChanged = true;
            }
            ModifyButton();
        }

        private void txtMaBenhChinh_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && hasMorethanOne)
            {
                DSACH_ICD(txtMaBenhChinh, DmucChung.Columns.Ma, 0);
                hasMorethanOne = false;
            }
        }

        private void txtMaBenhChinh_TextChanged(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    DataRow[] rowArray;
                    hasMorethanOne = true;
                    if (isLike)
                    {
                        rowArray =
                            globalVariables.gv_dtDmucBenh.Select(DmucBenh.Columns.MaBenh + " like '" +
                                                                 Utility.sDbnull(txtMaBenhChinh.Text, "") + "%'");
                    }
                    else
                    {
                        rowArray =
                            globalVariables.gv_dtDmucBenh.Select(DmucBenh.Columns.MaBenh + " = '" +
                                                                 Utility.sDbnull(txtMaBenhChinh.Text, "") + "'");
                    }
                    if (!string.IsNullOrEmpty(txtMaBenhChinh.Text))
                    {
                        if (rowArray.GetLength(0) == 1)
                        {
                            hasMorethanOne = false;
                            txtMaBenhChinh.Text = rowArray[0][DmucBenh.Columns.MaBenh].ToString();
                            txtTenBenhChinh.Text = Utility.sDbnull(rowArray[0][DmucBenh.Columns.TenBenh], "");
                        }
                        else
                        {
                            txtTenBenhChinh.Text = "";
                        }
                    }
                    else
                    {
                        txtTenBenhChinh.Text = "";
                    }
                }
                catch
                {
                }
            }
            finally
            {
            }
        }

        private void txtMaBenhphu_GotFocus(object sender, EventArgs e)
        {
            txtMaBenhphu_TextChanged(txtMaBenhphu, e);
        }

        private void txtMaBenhphu_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (hasMorethanOne)
                    {
                        DSACH_ICD(txtMaBenhphu, DmucChung.Columns.Ma, 1);
                        txtMaBenhphu.SelectAll();
                    }
                    else
                    {
                        AddBenhphu();
                        txtMaBenhphu.SelectAll();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void txtMaBenhphu_TextChanged(object sender, EventArgs e)
        {
            DataRow[] rowArray;
            hasMorethanOne = true;
            if (isLike)
            {
                rowArray =
                    globalVariables.gv_dtDmucBenh.Select(DmucBenh.Columns.MaBenh + " like '" +
                                                         Utility.sDbnull(txtMaBenhphu.Text, "") + "%'");
            }
            else
            {
                rowArray =
                    globalVariables.gv_dtDmucBenh.Select(DmucBenh.Columns.MaBenh + " = '" +
                                                         Utility.sDbnull(txtMaBenhphu.Text, "") + "'");
            }
            if (!string.IsNullOrEmpty(txtMaBenhphu.Text))
            {
                if (rowArray.GetLength(0) == 1)
                {
                    hasMorethanOne = false;
                    txtMaBenhphu.Text = rowArray[0][DmucBenh.Columns.MaBenh].ToString();
                    txtTenBenhPhu.Text = Utility.sDbnull(rowArray[0][DmucBenh.Columns.TenBenh], "");
                    TEN_BENHPHU = txtTenBenhPhu.Text;
                }
                else
                {
                    txtTenBenhPhu.Text = "";
                    TEN_BENHPHU = "";
                }
            }
            else
            {
                txtMaBenhphu.Text = "";
                TEN_BENHPHU = "";
            }
        }

        private void txtPres_ID_TextChanged(object sender, EventArgs e)
        {
            barcode.Visible = Utility.Int32Dbnull(txtPres_ID.Text) > 0;
            barcode.Data = Utility.sDbnull(txtPres_ID.Text);
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            Utility.FormatCurrencyHIS(txtPrice);
        }

        private void txtSoluong_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Utility.DecimaltoDbnull(txtSoluong.Text, 0) > 0) && (e.KeyCode == Keys.Enter))
            {
                if (!_autoFill)
                {
                    if (globalVariables.gv_intChophepChinhgiathuocKhiKedon == 0)
                    {
                        txtSoLuongDung.Focus();
                        txtSoLuongDung.SelectAll();
                    }
                    else
                    {
                        txtPrice.Focus();
                        txtPrice.SelectAll();
                    }
                }
                else
                {
                    cmdAddDetail_Click(cmdAddDetail, new EventArgs());
                }
            }
        }

        private void txtSoluong_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Utility.ResetMessageError(errorProvider1);
                if (Utility.DoTrim(txtTonKho.Text) != "")
                {
                    if (Utility.Int32Dbnull(objDKho.KtraTon) == 1 &&
                        (Utility.DecimaltoDbnull(txtSoluong.Text, 0) > Utility.Int32Dbnull(txtTonKho.Text, 0)))
                    {
                        Utility.SetMsgError(errorProvider1, txtSoluong,
                            "Số lượng " + (KIEU_THUOC_VT == "THUOC" ? "thuốc" : "vật tư") +
                            " cấp phát vượt quá số lượng " + (KIEU_THUOC_VT == "THUOC" ? "thuốc" : "vật tư") +
                            " trong kho. Mời bạn kiểm tra lại");
                        txtSoluong.Focus();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void txtSolan_TextChanged(object sender, EventArgs e)
        {
            ChiDanThuoc();
        }

        private void txtSoLuongDung_TextChanged(object sender, EventArgs e)
        {
            ChiDanThuoc();
        }

        private void txtSurcharge_TextChanged(object sender, EventArgs e)
        {
            Utility.FormatCurrencyHIS(txtSurcharge);
        }

        private void txtTenBenhChinh_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtTenBenhChinh_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtMaBenhChinh.TextLength <= 0)
                {
                    txtMaBenhChinh.ResetText();
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình xử lý sự kiện");
            }
        }

        private void txtTenBenhPhu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (hasMorethanOne)
                {
                    DSACH_ICD(txtTenBenhPhu, DmucChung.Columns.Ten, 1);
                    txtTenBenhPhu.Focus();
                }
                else
                {
                    txtTenBenhPhu.Focus();
                }
            }
        }

        private void txtTenBenhPhu_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtTenBenhPhu.TextLength <= 0)
                {
                    Selected = false;
                    txtMaBenhphu.ResetText();
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình xử lý sự kiện");
            }
        }

        private void UpdateChiDanThem()
        {
            //Tạm bỏ đi
            //if (!string.IsNullOrEmpty(txtChiDanThem.Text))
            //{
            //    new Delete().From(DmucChung.Schema)
            //        .Where(DmucChung.Columns.Loai).IsEqualTo("CDDT")
            //        .And(DmucChung.Columns.NguoiTao).IsEqualTo(globalVariables.UserName)
            //        .And(DmucChung.Columns.Ma).IsEqualTo(Utility.UnSignedCharacter(txtChiDanThem.Text.ToUpper())).Execute();
            //    DmucChung objDmucChung = new DmucChung();
            //    objDmucChung.NguoiTao = globalVariables.UserName;
            //    objDmucChung.NgayTao = globalVariables.SysDate;
            //    objDmucChung.Ten = Utility.sDbnull(Utility.chuanhoachuoi(txtChiDanThem.Text.Trim()));
            //    objDmucChung.TrangThai = 1;
            //    objDmucChung.MotaThem = Utility.sDbnull(Utility.chuanhoachuoi(txtChiDanThem.Text.Trim()));
            //    objDmucChung.Ma = Utility.UnSignedCharacter(objDmucChung.Ten.ToUpper());
            //    objDmucChung.Loai = "CDDT";
            //    objDmucChung.IsNew = true;
            //    objDmucChung.Save();

            //}
        }

        private void UpdateDataWhenChanged()
        {
            try
            {
                decimal tongtien = Utility.DecimaltoDbnull(m_dtDonthuocChitiet.Compute("SUM(TT_BN)", "1=1"), "0");
                Utility.SetMsg(StatusBar.Panels["Tongtien"], "Tổng tiền BN: " + String.Format(Utility.FormatDecimal(), tongtien));
            }
            catch (Exception ex)
            {
            }
        }

        private void UpdateDetailID(Dictionary<long, long> lstPresDetail)
        {
            if (lstPresDetail.Count > 0)
            {
                foreach (DataRow row in m_dtDonthuocChitiet.Rows)
                {
                    if (lstPresDetail.ContainsKey(Utility.Int64Dbnull(row[KcbDonthuocChitiet.Columns.IdThuockho])))
                    {
                        row[KcbDonthuocChitiet.Columns.IdChitietdonthuoc] =
                            lstPresDetail[Utility.Int64Dbnull(row[KcbDonthuocChitiet.Columns.IdThuockho])];
                    }
                }
                m_dtDonthuocChitiet.AcceptChanges();

                CreateViewTable();
            }
        }

        private void CapnhatDonthuoc()
        {
            var lstChitietDonthuoc = new Dictionary<long, long>();
            KcbDonthuocChitiet[] arrDonthuocChitiet = CreateArrayPresDetail();
            if (arrDonthuocChitiet != null)
            {
                _actionResult = _kedonthuoc.CapnhatDonthuoc(objLuotkham, TaoDonthuoc(), arrDonthuocChitiet,
                    KcbChandoanKetluan, ref IdDonthuoc, ref lstChitietDonthuoc);
                switch (_actionResult)
                {
                    case ActionResult.Error:
                        setMsg(lblMsg, "Lỗi trong quá trình lưu đơn " + (KIEU_THUOC_VT == "THUOC" ? "thuốc" : "vật tư"),
                            true);
                        break;

                    case ActionResult.Success:
                        UpdateChiDanThem();
                        setMsg(lblMsg,
                            "Bạn thực hiện lưu đơn " + (KIEU_THUOC_VT == "THUOC" ? "thuốc" : "vật tư") + " thành công",
                            false);
                        UpdateDetailID(lstChitietDonthuoc);
                        m_blnCancel = false;
                        break;
                }
            }
        }

        private void CapnhatDonthuoc(KcbDonthuocChitiet[] arrPresDetail)
        {
            var lstChitietDonthuoc = new Dictionary<long, long>();
            if (arrPresDetail != null)
            {
                _actionResult = _kedonthuoc.CapnhatDonthuoc(objLuotkham, TaoDonthuoc(), arrPresDetail,
                    KcbChandoanKetluan, ref IdDonthuoc, ref lstChitietDonthuoc);
                switch (_actionResult)
                {
                    case ActionResult.Error:
                        setMsg(lblMsg, "Lỗi trong quá trình lưu đơn " + (KIEU_THUOC_VT == "THUOC" ? "thuốc" : "vật tư"),
                            true);
                        break;

                    case ActionResult.Success:
                        UpdateChiDanThem();
                        setMsg(lblMsg,
                            "Bạn thực hiện lưu đơn " + (KIEU_THUOC_VT == "THUOC" ? "thuốc" : "vật tư") + " thành công",
                            false);
                        UpdateDetailID(lstChitietDonthuoc);
                        m_blnCancel = false;
                        break;
                }
            }
        }

        private void Updatethuoctutuc()
        {
            try
            {
                if (Utility.isValidGrid(grdPresDetail))
                {
                    int num = Utility.Int32Dbnull(grdPresDetail.GetValue(KcbDonthuocChitiet.Columns.IdChitietdonthuoc));
                    decimal num2 = Utility.Int32Dbnull(grdPresDetail.GetValue(KcbDonthuocChitiet.Columns.DonGia));
                    int num3 = Utility.Int32Dbnull(grdPresDetail.GetValue(KcbDonthuocChitiet.Columns.IdThuoc));
                    foreach (DataRow row in m_dtDonthuocChitiet.Rows)
                    {
                        if ((Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.DonGia], -1) == num2) &&
                            (Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.IdThuoc], -1) == num3))
                        {
                            row[KcbDonthuocChitiet.Columns.TuTuc] = 1;
                            if (tu_tuc == 0)
                            {
                                decimal BHCT = 0m;
                                if (objLuotkham.DungTuyen == 1)
                                {
                                    BHCT = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0)*
                                           (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0)/100);
                                }
                                else
                                {
                                    if (objLuotkham.TrangthaiNoitru <= 0)
                                        BHCT = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0)*
                                               (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0)/100);
                                    else //Nội trú cần tính=đơn giá * % đầu thẻ * % tuyến
                                        BHCT = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0)*
                                               (Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0)/100)*
                                               (_bhytPtramTraituyennoitru/100);
                                }
                                //decimal num4 = (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0) * Utility.DecimaltoDbnull(this.objLuotkham.PtramBhyt, 0)) / 100M;
                                decimal num5 = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0) - BHCT;
                                row[KcbDonthuocChitiet.Columns.BhytChitra] = BHCT;
                                row[KcbDonthuocChitiet.Columns.BnhanChitra] = num5;
                            }
                            else
                            {
                                row[KcbDonthuocChitiet.Columns.PtramBhyt] = 0;
                                row[KcbDonthuocChitiet.Columns.BhytChitra] = 0;
                                row[KcbDonthuocChitiet.Columns.BnhanChitra] =
                                    Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0);
                            }
                        }
                    }
                    CreateViewTable();
                }
            }
            catch
            {
            }
        }
        private void txtPatientCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPatientID_TextChanged(object sender, EventArgs e)
        {
            if (Utility.Int64Dbnull(txtPatientID.Text, -1) > 0)
            {
                DataTable dtListDonThuoc =
                    SPs.KcbLaydanhsachDonthuocOld(Utility.Int64Dbnull(txtPatientID.Text, -1)).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdListDonThuocCu,dtListDonThuoc,false,true,"","");
            }
        }

        private void grdListDonThuocCu_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            if (e.Column.Key == "Chon")
            {
                var frm = new FrmThongTinDonThuoc(KIEU_THUOC_VT);
                frm.txtPatientCode.Text = Utility.sDbnull(txtPatientCode.Text);
                frm.txtPatientID.Text = Utility.sDbnull(txtPatientID.Text);
                frm.txtPatientName.Text = Utility.sDbnull(txtPatientName.Text);
                frm.txtSex.Text = Utility.sDbnull(txtSex.Text);
                frm.txtPres_ID.Text = Utility.sDbnull(grdListDonThuocCu.GetValue(KcbDonthuoc.Columns.IdDonthuoc));
                frm.txtYearBirth.Text = Utility.sDbnull(txtYearBirth.Text);
                frm.txtMaBenhChinh.Text = Utility.sDbnull(grdListDonThuocCu.GetValue("mabenh_chinh"));
                frm.idkhosaochep = Utility.Int16Dbnull(cboStock.SelectedValue, -1);
                frm.id_kham = Utility.Int32Dbnull(id_kham,-1);
                frm.m_dtDanhmucthuoc = m_dtDanhmucthuoc;
                frm.grd_ICD = grd_ICD;
                frm.txtTenBenhChinh.Text = Utility.sDbnull(txtTenBenhChinh.Text);
                frm.txtchandoan_new.Text = Utility.sDbnull(txtChanDoan.Text);
                frm.ngaykedon = Convert.ToDateTime(dtpCreatedDate.Value);
                frm.ShowDialog();
            }
        }
    }
}