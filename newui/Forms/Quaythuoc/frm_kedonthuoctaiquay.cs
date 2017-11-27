﻿
    using CrystalDecisions.CrystalReports.Engine;
    using Janus.Windows.GridEX;
    using Janus.Windows.GridEX.EditControls;
    using SubSonic;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Linq;
    using System.Windows.Forms;
    using VNS.HIS.BusRule.Classes;
    using VNS.HIS.DAL;
    using VNS.HIS.NGHIEPVU.THUOC;
    using VNS.HIS.UCs;
    using VNS.HIS.UI.DANHMUC;
    using VNS.Libs;
    using VNS.Properties;

namespace VNS.HIS.UI.NGOAITRU
{
     public partial class frm_kedonthuoctaiquay : Form
    {
         public string KIEU_THUOC_VT = "THUOC";
        private ActionResult _actionResult = ActionResult.Error;
        private bool _Found = false;
        private KCB_KEDONTHUOC _KEDONTHUOC = new KCB_KEDONTHUOC();
        private VNS.Libs.MoneyByLetter _moneyByLetter = new VNS.Libs.MoneyByLetter();
        private string _rowFilter = "1=1";
        private ActionResult _temp = ActionResult.Success;
        private bool AllowTextChanged = false;
        private bool APDUNG_GIATHUOC_DOITUONG = (Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("APDUNG_GIATHUOC_DOITUONG", "0", true), 0) == 1);
        public bool m_blnCancel=true;
        private bool blnHasLoaded = false;
        public CallActionKieuKeDon CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;

        private long currentIdthuockho = 0L;
        public DataTable dt_ICD = new DataTable();
        public DataTable dt_ICD_PHU = new DataTable();

        private DataTable dtStockList = null;
        public action m_enAct = action.Insert;
        public CallAction em_CallAction = CallAction.FromMenu;
        private bool FilterAgain;
        private bool Giathuoc_quanhe = false;

        private bool hasChanged = false;
        private bool hasMorethanOne = true;
        public int id_kham = -1;
        private long IdDonthuoc = -1;
        private bool isLike = true;
        public bool isLoaded = false;
        private bool isSaved = false;


        private string LOAIKHOTHUOC = "KHO";
        private Dictionary<long, string> lstChangeData = new Dictionary<long, string>();
        private bool m_blnGetDrugCodeFromList = false;
        private decimal m_decPrice = 0M;
        private DataTable m_dtCD_DVD = new DataTable();
        public DataTable m_dtDonthuocChitiet = new DataTable();
        public DataTable m_dtDonthuocChitiet_View = new DataTable();
        public DataTable m_dtDanhmucthuoc = new DataTable();
        private decimal m_Surcharge = 0M;
        private bool Manual = false;

        public DataTable m_dtPatients = new DataTable();
        private QheDoituongThuoc objectPolicy = null;
        private QheDoituongThuoc objectPolicyTutuc = null;
        public short ObjectType_Id = -1;

        private string rowFilter = "1=2";
        private bool Selected;

        private string strSaveandprintPath = (Application.StartupPath + @"\CAUHINH\SaveAndPrintConfigKedonthuoc.txt");

        private string TEN_BENHPHU = "";
        private int tu_tuc = 0;

        public int v_Patient_ID = -1;
        public string v_PatientCode = "";

        public delegate void OnPayment(long idBn);
        public event OnPayment _OnPayment;

        public frm_kedonthuoctaiquay()
        {
            this.InitializeComponent();
            txtNgheNghiep._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtNgheNghiep__OnShowData);
            txtChiDanThem._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtChiDanThem__OnShowData);
            txtDrugID.TextChanged += txtDrugID_TextChanged;
            txtdrug._OnChangedView += txtdrug__OnChangedView;
            txtdrug._OnEnterMe += txtdrug__OnEnterMe;
            txtdrug._OnGridSelectionChanged += txtdrug__OnGridSelectionChanged;
            base.KeyPreview = true;
            this.dtpCreatedDate.Value = this.dtNgayIn.Value = this.dtNgayKhamLai.Value = globalVariables.SysDate;
            this.InitEvents();
            this.CauHinh();
        }

        private void AddBenhphu()
        {
            Func<DataRow, bool> predicate = null;
            try
            {
                try
                {
                    if ((this.txtMaBenhphu.Text.TrimStart(new char[0]).TrimEnd(new char[0]) != "") && !(this.txtTenBenhPhu.Text.TrimStart(new char[0]).TrimEnd(new char[0]) == ""))
                    {
                        if (predicate == null)
                        {
                            predicate = benh => Utility.sDbnull(benh[DmucBenh.Columns.MaBenh]) == this.txtMaBenhphu.Text;
                        }
                        if (!this.dt_ICD_PHU.AsEnumerable().Where<DataRow>(predicate).Any<DataRow>())
                        {
                            this.AddMaBenh(this.txtMaBenhphu.Text, this.TEN_BENHPHU);
                            this.txtMaBenhphu.ResetText();
                            this.txtTenBenhPhu.ResetText();
                            this.txtMaBenhphu.Focus();
                            this.txtMaBenhphu.SelectAll();
                            this.Selected = false;
                        }
                        else
                        {
                            this.txtMaBenhphu.ResetText();
                            this.txtTenBenhPhu.ResetText();
                            this.txtMaBenhphu.Focus();
                            this.txtMaBenhphu.SelectAll();
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
            if (!this.dt_ICD_PHU.AsEnumerable().Where<DataRow>(benh => (Utility.sDbnull(benh[DmucBenh.Columns.MaBenh]) == MaBenh)).Any<DataRow>())
            {
                DataRow row = this.dt_ICD_PHU.NewRow();
                row[DmucBenh.Columns.MaBenh] = MaBenh;
                if (predicate == null)
                {
                    predicate = benh => Utility.sDbnull(benh[DmucBenh.Columns.MaBenh]) == MaBenh;
                }
                EnumerableRowCollection<string> source = globalVariables.gv_dtDmucBenh.AsEnumerable().Where<DataRow>(predicate).Select<DataRow, string>(benh => Utility.sDbnull(benh[DmucBenh.Columns.TenBenh]));
                if (source.Any<string>())
                {
                    row[DmucBenh.Columns.TenBenh] = Utility.sDbnull(source.FirstOrDefault<string>());
                }
                this.dt_ICD_PHU.Rows.Add(row);
                this.dt_ICD_PHU.AcceptChanges();
                this.grd_ICD.AutoSizeColumns();
            }
        }

        private void AddPreDetail()
        {
            try
            {
                string errMsg = string.Empty;
                string errMsg_temp = string.Empty;
                this.setMsg(this.lblMsg, "", false);
                this.tu_tuc = this.chkTutuc.Checked ? 1 : 0;
                if (Utility.Int32Dbnull(this.txtDrugID.Text) < 0)
                {
                    this.txtdrug.Focus();
                    this.txtdrug.SelectAll();
                }
                else if (Utility.DecimaltoDbnull(this.txtSoluong.Text) <= 0)
                {
                    this.setMsg(this.lblMsg, "Số lượng thuốc phải lớn hơn 0", true);
                    this.txtSoluong.Focus();
                }
                else
                {
                    if (CommonLoadDuoc.IsKiemTraTonKho(Utility.Int32Dbnull(this.cboStock.SelectedValue, 0)))
                    {
                        decimal num = CommonLoadDuoc.SoLuongTonTrongKho(-1L, Utility.Int32Dbnull(this.cboStock.SelectedValue), Utility.Int32Dbnull(this.txtDrugID.Text, -1), txtdrug.GridView ? this.id_thuockho : (long)this.txtdrug.id_thuockho, new int?(Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("KIEMTRATHUOC_CHOXACNHAN", "1", false), 1)), (byte)0);
                        if (Utility.DecimaltoDbnull(this.txtSoluong.Text, 0) > num)
                        {
                            Utility.ShowMsg(string.Format("Số lượng thuốc cấp phát {0} vượt quá số lượng thuốc trong kho {1}.\nCó thể trong lúc bạn chọn thuốc chưa kịp đưa vào đơn, các Bác sĩ khác hoặc Dược sĩ đã kê hoặc cấp phát mất một lượng thuốc so với thời điểm bạn chọn.\nMời bạn liên hệ phòng Dược kiểm tra lại", this.txtSoluong.Text, num.ToString()), "Cảnh báo", MessageBoxIcon.Hand);
                            this.txtSoluong.Focus();
                            return;
                        }
                    }
                    DataTable listdata = new XuatThuoc().GetObjThuocKhoCollection(Utility.Int32Dbnull(this.cboStock.SelectedValue, 0), Utility.Int32Dbnull(this.txtDrugID.Text, -1), txtdrug.GridView ? this.id_thuockho : this.txtdrug.id_thuockho, (int)Utility.DecimaltoDbnull(this.txtSoluong.Text, 0), (byte)1, (byte)0, (byte)0);
                    List<KcbDonthuocChitiet> list2 = new List<KcbDonthuocChitiet>();
                    foreach (DataRow thuockho in listdata.Rows)
                    {
                        int _soluong = Utility.Int32Dbnull(thuockho[TThuockho.Columns.SoLuong], 0);
                        if (_soluong > 0)
                        {
                            DataRow[] rowArray = this.m_dtDonthuocChitiet.Select(TThuockho.Columns.IdThuockho + "=" + Utility.sDbnull(thuockho[TThuockho.Columns.IdThuockho]));
                            if (rowArray.Length > 0)
                            {
                                rowArray[0][KcbDonthuocChitiet.Columns.SoLuong] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) + _soluong;
                                rowArray[0]["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.DonGia]);
                                rowArray[0]["TT"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.DonGia]) + Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu]));
                                rowArray[0]["TT_BHYT"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BhytChitra]);
                                rowArray[0]["TT_BN"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0) + Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu], 0));
                                rowArray[0]["TT_PHUTHU"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu], 0);
                                rowArray[0]["TT_BN_KHONG_PHUTHU"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0);
                                this.AddtoView(rowArray[0], _soluong);
                                list2.Add(this.GetNewItem(rowArray[0]));
                            }
                            else
                            {
                                byte? nullable;
                                DataRow row = this.m_dtDonthuocChitiet.NewRow();
                                row[DmucThuoc.Columns.TenThuoc] = Utility.sDbnull(this.txtDrug_Name.Text, "");
                                row[KcbDonthuocChitiet.Columns.SoLuong] = _soluong;
                                row[KcbDonthuocChitiet.Columns.PhuThu] = 0;// !this.Giathuoc_quanhe ? 0M : Utility.DecimaltoDbnull(this.txtSurcharge.Text, 0);
                                row[KcbDonthuocChitiet.Columns.IdThuoc] = Utility.Int32Dbnull(this.txtDrugID.Text, -1);
                                row[KcbDonthuocChitiet.Columns.IdDonthuoc] = this.IdDonthuoc;
                                row["IsNew"] = 1;
                                row[KcbDonthuocChitiet.Columns.IdThuockho] = Utility.Int64Dbnull(thuockho[TThuockho.Columns.IdThuockho], 0);
                                row[KcbDonthuocChitiet.Columns.GiaNhap] = Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.GiaNhap], 0);
                                row[KcbDonthuocChitiet.Columns.GiaBan] = Utility.DecimaltoDbnull(txtPrice.Text,0);
                                row[KcbDonthuocChitiet.Columns.GiaBhyt] = Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.GiaBhyt], 0);
                                row[KcbDonthuocChitiet.Columns.Vat] = Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.Vat], 0);
                                row[KcbDonthuocChitiet.Columns.SoLo] = Utility.sDbnull(thuockho[TThuockho.Columns.SoLo], "");
                                row[KcbDonthuocChitiet.Columns.MaNhacungcap] = Utility.sDbnull(thuockho[TThuockho.Columns.MaNhacungcap], "");
                                row["ten_donvitinh"] = this.txtDonViDung.Text;
                                row["sNgay_hethan"] = Utility.sDbnull(thuockho["sNgay_hethan"]);
                                row[KcbDonthuocChitiet.Columns.NgayHethan] = thuockho[TThuockho.Columns.NgayHethan];
                                row[KcbDonthuocChitiet.Columns.IdKho] = Utility.Int32Dbnull(this.cboStock.SelectedValue, -1);
                                row[TDmucKho.Columns.TenKho] = Utility.sDbnull(this.cboStock.Text, -1);
                                row[KcbDonthuocChitiet.Columns.DonviTinh] = this.txtDonViDung.Text;
                                row[DmucThuoc.Columns.HoatChat] = this.txtBietduoc.Text;
                                row[KcbDonthuocChitiet.Columns.ChidanThem] = this.txtChiDanThem.Text;
                                row[KcbDonthuocChitiet.Columns.MotaThem] = Utility.sDbnull(this.txtChiDanThem.Text);
                                row["mota_them_chitiet"] = Utility.sDbnull(this.txtChiDanDungThuoc.Text);
                                row[KcbDonthuocChitiet.Columns.CachDung] = Utility.sDbnull(this.txtCachDung.Text);
                                row[KcbDonthuocChitiet.Columns.SoluongDung] = Utility.sDbnull(this.txtSoLuongDung.Text);
                                row[KcbDonthuocChitiet.Columns.SolanDung] = Utility.sDbnull(this.txtSolan.Text);
                                row["ma_loaithuoc"] = this.txtdrugtypeCode.Text;
                                row[KcbDonthuocChitiet.Columns.TrangthaiThanhtoan] = 0;
                                row[KcbDonthuocChitiet.Columns.SttIn] = this.GetMaxSTT(this.m_dtDonthuocChitiet);
                                row[KcbDonthuocChitiet.Columns.TuTuc] = 0;
                                row[KcbDonthuocChitiet.Columns.DonGia] = Utility.DecimaltoDbnull(txtPrice.Text, 0);
                                row[KcbDonthuocChitiet.Columns.PtramBhyt] = 0;
                                row[KcbDonthuocChitiet.Columns.MaDoituongKcb] = "DV";
                                row[KcbDonthuocChitiet.Columns.PhuthuDungtuyen] = 0;
                                row[KcbDonthuocChitiet.Columns.PhuthuTraituyen] = 0;
                                row[KcbDonthuocChitiet.Columns.BhytChitra] = 0;
                                row[KcbDonthuocChitiet.Columns.PtramBhyt] = 0;
                                row[KcbDonthuocChitiet.Columns.BhytChitra] = 0;
                                row[KcbDonthuocChitiet.Columns.BnhanChitra] = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0);
                                row[KcbDonthuocChitiet.Columns.KieuBiendong] = thuockho["kieubiendong"];

                                row["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia]);
                                row["TT"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia]) + Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu]));
                                row["TT_BHYT"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BhytChitra]);
                                row["TT_BN"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BnhanChitra], 0) + Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu], 0));
                                row["TT_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu], 0);
                                row["TT_BN_KHONG_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BnhanChitra], 0);

                                errMsg_temp = KiemtraCamchidinhchungphieu(Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.IdThuoc], 0), Utility.sDbnull(row[DmucThuoc.Columns.TenThuoc], ""));
                                if (errMsg_temp != string.Empty)
                                {
                                    errMsg += errMsg_temp;
                                }
                                else
                                {
                                    this.m_dtDonthuocChitiet.Rows.Add(row);
                                    this.AddtoView(row, _soluong);
                                    list2.Add(this.GetNewItem(row));
                                }

                              
                            }
                        }
                    }
                    if (errMsg != string.Empty)
                    {
                        if (errMsg.Contains("Single-Service:"))
                        {
                            Utility.ShowMsg("Thuốc sau được đánh dấu không được phép kê chung đơn bất kỳ Thuốc nào. Đề nghị bạn kiểm tra lại:\n" + Utility.DoTrim(errMsg.Replace("Single-Service:", "")));
                        }
                        else
                            Utility.ShowMsg("Các cặp Thuốc sau đã được thiết lập chống kê chung đơn. Đề nghị bạn kiểm tra lại:\n" + errMsg);
                    }
                    else
                    {
                        //this.PerformAction(list2.ToArray());
                        Utility.GotoNewRowJanus(this.grdPresDetail, KcbDonthuocChitiet.Columns.IdThuoc, this.txtDrugID.Text);
                        this.UpdateDataWhenChanged();

                    }
                    this.ClearControl();
                    this.txtdrug.Focus();
                    this.txtdrug.SelectAll();
                    this.m_dtDanhmucthuoc.DefaultView.RowFilter = "1=2";
                    this.m_dtDanhmucthuoc.AcceptChanges();
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
        string KiemtraCamchidinhchungphieu(int id_thuoc, string ten_chitiet)
        {
            string _reval = "";
            string _tempt = "";
            List<string> lstKey = new List<string>();
            string _key = "";
            //Kiểm tra dịch vụ đang thêm có phải là dạng Single-Service hay không?
            DataRow[] _arrSingle = m_dtDanhmucthuoc.Select(DmucThuoc.Columns.SingleService + "=1 AND " + DmucThuoc.Columns.IdThuoc + "=" + id_thuoc);
            if (_arrSingle.Length > 0 && m_dtDonthuocChitiet.Select(KcbDonthuocChitiet.Columns.IdThuoc + "<>" + id_thuoc.ToString()).Length > 0)
            {
                return string.Format("Single-Service: {0}", ten_chitiet);
            }
            //Kiểm tra các dịch vụ đã thêm có cái nào là Single-Service hay không?
            List<int> lstID = m_dtDonthuocChitiet.AsEnumerable().Select(c => Utility.Int32Dbnull(c[KcbDonthuocChitiet.Columns.IdThuoc], 0)).Distinct().ToList<int>();
            var q = from p in m_dtDanhmucthuoc.AsEnumerable()
                    where Utility.ByteDbnull(p[DmucThuoc.Columns.SingleService], 0) == 1
                    && lstID.Contains(Utility.Int32Dbnull(p[DmucThuoc.Columns.IdThuoc], 0))
                    select p;
            if (q.Any())
            {
                return string.Format("Single-Service: {0}", Utility.sDbnull(q.FirstOrDefault()[DmucThuoc.Columns.TenThuoc], ""));
            }
            //Lấy các cặp cấm chỉ định chung cùng nhau
            DataRow[] arrDr = _mDtqheCamchidinhChungphieu.Select(QheCamchidinhChungphieu.Columns.IdDichvu + "=" + id_thuoc);
            DataRow[] arrDr1 = _mDtqheCamchidinhChungphieu.Select(QheCamchidinhChungphieu.Columns.IdDichvuCamchidinhchung + "=" + id_thuoc);
            foreach (DataRow dr in arrDr)
            {

                DataRow[] arrtemp = m_dtDonthuocChitiet.Select(KcbDonthuocChitiet.Columns.IdThuoc + "=" + Utility.sDbnull(dr[QheCamchidinhChungphieu.Columns.IdDichvuCamchidinhchung]));
                if (arrtemp.Length > 0)
                {

                    foreach (DataRow dr1 in arrtemp)
                    {
                        _tempt = string.Empty;
                        _key = id_thuoc.ToString() + "-" + Utility.sDbnull(dr1[KcbDonthuocChitiet.Columns.IdThuoc], "");
                        if (!lstKey.Contains(_key))
                        {
                            lstKey.Add(_key);
                            _tempt = string.Format("{0} - {1}", ten_chitiet, Utility.sDbnull(dr1[DmucThuoc.Columns.IdThuoc], ""));
                        }
                        if (_tempt != string.Empty)
                            _reval += _tempt + "\n";
                    }

                }
            }
            foreach (DataRow dr in arrDr1)
            {

                DataRow[] arrtemp = m_dtDonthuocChitiet.Select(KcbDonthuocChitiet.Columns.IdThuoc + "=" + Utility.sDbnull(dr[QheCamchidinhChungphieu.Columns.IdDichvu]));
                if (arrtemp.Length > 0)
                {

                    foreach (DataRow dr1 in arrtemp)
                    {
                        _tempt = string.Empty;
                        _key = id_thuoc.ToString() + "-" + Utility.sDbnull(dr1[KcbDonthuocChitiet.Columns.IdThuoc], "");
                        if (!lstKey.Contains(_key))
                        {
                            lstKey.Add(_key);
                            _tempt = string.Format("{0} - {1}", ten_chitiet, Utility.sDbnull(dr1[DmucThuoc.Columns.TenThuoc], ""));
                        }
                        if (_tempt != string.Empty)
                            _reval += _tempt + "\n";
                    }
                }
            }
            return _reval;
        }
        private void AddQuantity(int id_thuoc, int id_thuockho, int newQuantity)
        {
            try
            {
                this.tu_tuc = this.chkTutuc.Checked ? 1 : 0;
                this.setMsg(this.lblMsg, "", false);
                if (CommonLoadDuoc.IsKiemTraTonKho(Utility.Int32Dbnull(this.cboStock.SelectedValue, 0)))
                {
                    decimal num = CommonLoadDuoc.SoLuongTonTrongKho(-1L, Utility.Int32Dbnull(this.cboStock.SelectedValue),
                        id_thuoc, (long) id_thuockho,
                        new int?(
                            Utility.Int32Dbnull(
                                THU_VIEN_CHUNG.Laygiatrithamsohethong("KIEMTRATHUOC_CHOXACNHAN", "1", false), 1)),
                        (byte) 0);
                    if (newQuantity > num)
                    {
                        Utility.ShowMsg("Số lượng thuốc cấp phát vượt quá số lượng thuốc trong kho. Mời bạn kiểm tra lại", "Cảnh báo", MessageBoxIcon.Hand);
                        this.txtSoluong.Focus();
                        return;
                    }
                }
                DataTable listdata = new XuatThuoc().GetObjThuocKhoCollection(Utility.Int32Dbnull(this.cboStock.SelectedValue, 0), id_thuoc, id_thuockho, newQuantity, (byte)1, (byte)0, (byte)0);
                List<KcbDonthuocChitiet> list2 = new List<KcbDonthuocChitiet>();
                foreach (DataRow thuockho in listdata.Rows)
                {
                    int _soluong = Utility.Int32Dbnull(thuockho[TThuockho.Columns.SoLuong], 0);
                    if (_soluong > 0)
                    {
                        DataRow[] rowArray = this.m_dtDonthuocChitiet.Select(TThuockho.Columns.IdThuockho + "=" + Utility.sDbnull(thuockho[TThuockho.Columns.IdThuockho]));
                        if (rowArray.Length > 0)
                        {
                            rowArray[0][KcbDonthuocChitiet.Columns.SoLuong] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) + _soluong;
                            newQuantity -= _soluong;
                            rowArray[0]["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.DonGia]);
                            rowArray[0]["TT"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.DonGia]) + Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu]));
                            rowArray[0]["TT_BHYT"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BhytChitra]);
                            rowArray[0]["TT_BN"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0) + Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu], 0));
                            rowArray[0]["TT_PHUTHU"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu], 0);
                            rowArray[0]["TT_BN_KHONG_PHUTHU"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0);
                            this.AddtoView(rowArray[0], _soluong);
                            list2.Add(this.GetNewItem(rowArray[0]));
                        }
                        else
                        {
                            byte? nullable;
                            DataRow row = this.m_dtDonthuocChitiet.NewRow();
                            string donviTinh = "";
                            string chidanThem = "";
                            string motaThem = "";
                            string cachDung = "";
                            string soluongDung = "";
                            string solanDung = "";
                            string tenthuoc = "";
                            string str8 = "";
                            string hoatchat = "";
                            this.GetInfor(id_thuoc, ref tenthuoc, ref str8, ref hoatchat, ref donviTinh, ref chidanThem, ref motaThem, ref cachDung, ref soluongDung, ref solanDung);
                            this.txtDrugID.Text = id_thuoc.ToString();
                            this.txtDrugID_TextChanged(this.txtDrugID, new EventArgs());
                            row[DmucThuoc.Columns.TenThuoc] = Utility.sDbnull(this.txtDrug_Name.Text, "");
                            row[KcbDonthuocChitiet.Columns.SoLuong] = _soluong;
                            row[KcbDonthuocChitiet.Columns.PhuThu] = 0;// !this.Giathuoc_quanhe ? 0M : Utility.DecimaltoDbnull(this.txtSurcharge.Text, 0);
                            row[KcbDonthuocChitiet.Columns.IdThuoc] = id_thuoc;
                            row[KcbDonthuocChitiet.Columns.IdDonthuoc] = this.IdDonthuoc;
                            row["IsNew"] = 1;
                            row[KcbDonthuocChitiet.Columns.IdThuockho] = Utility.Int64Dbnull(thuockho[TThuockho.Columns.IdThuockho], 0);
                            row[KcbDonthuocChitiet.Columns.GiaNhap] = Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.GiaNhap], 0);
                            row[KcbDonthuocChitiet.Columns.GiaBan] = Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.GiaBan], 0);
                            row[KcbDonthuocChitiet.Columns.GiaBhyt] = Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.GiaBhyt], 0);
                            row[KcbDonthuocChitiet.Columns.Vat] = Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.Vat], 0);
                            row[KcbDonthuocChitiet.Columns.SoLo] = Utility.sDbnull(thuockho[TThuockho.Columns.SoLo], "");
                            row[KcbDonthuocChitiet.Columns.MaNhacungcap] = Utility.sDbnull(thuockho[TThuockho.Columns.MaNhacungcap], "");
                            row["ten_donvitinh"] = this.txtDonViDung.Text;
                            row["sNgay_hethan"] = Utility.sDbnull(thuockho["sNgay_hethan"]);
                            row[KcbDonthuocChitiet.Columns.NgayHethan] = thuockho[TThuockho.Columns.NgayHethan];
                            row[KcbDonthuocChitiet.Columns.IdKho] = Utility.Int32Dbnull(this.cboStock.SelectedValue, -1);
                            row[TDmucKho.Columns.TenKho] = Utility.sDbnull(this.cboStock.Text, -1);
                            row[KcbDonthuocChitiet.Columns.DonviTinh] = donviTinh;
                            row[DmucThuoc.Columns.HoatChat] = hoatchat;
                            row[KcbDonthuocChitiet.Columns.ChidanThem] = chidanThem;
                            row["mota_them_chitiet"] = chidanThem;
                            row[KcbDonthuocChitiet.Columns.MotaThem] = motaThem;
                            row[KcbDonthuocChitiet.Columns.CachDung] = cachDung;
                            row[KcbDonthuocChitiet.Columns.SoluongDung] = soluongDung;
                            row[KcbDonthuocChitiet.Columns.SolanDung] = solanDung;
                            row["ma_loaithuoc"] = this.txtdrugtypeCode.Text;
                            row[KcbDonthuocChitiet.Columns.TrangthaiThanhtoan] = 0;
                            row[KcbDonthuocChitiet.Columns.SttIn] = this.GetMaxSTT(this.m_dtDonthuocChitiet);
                            row[KcbDonthuocChitiet.Columns.TuTuc] = 0;
                            row[KcbDonthuocChitiet.Columns.DonGia] = (this.APDUNG_GIATHUOC_DOITUONG || this.Giathuoc_quanhe) ? Utility.DecimaltoDbnull(this.txtPrice.Text, 0) :Utility.DecimaltoDbnull( thuockho[TThuockho.Columns.GiaBan],0);
                            row[KcbDonthuocChitiet.Columns.PtramBhyt] = 0;
                            row[KcbDonthuocChitiet.Columns.MaDoituongKcb] = "DV";
                                row[KcbDonthuocChitiet.Columns.PtramBhyt] = 0;
                                row[KcbDonthuocChitiet.Columns.BhytChitra] = 0;
                                row[KcbDonthuocChitiet.Columns.BnhanChitra] = Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0);
                            row["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia]);
                            row["TT"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia]) + Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu]));
                            row["TT_BHYT"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BhytChitra]);
                            row["TT_BN"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BnhanChitra], 0) + Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu], 0));
                            row["TT_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu], 0);
                            row["TT_BN_KHONG_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BnhanChitra], 0);
                            this.m_dtDonthuocChitiet.Rows.Add(row);
                            int num4 = newQuantity - _soluong;
                            this.AddtoView(row, (num4 > 0) ? _soluong : newQuantity);
                            list2.Add(this.GetNewItem(row));
                        }
                    }
                }
                //this.PerformAction(list2.ToArray());
                Utility.GotoNewRowJanus(this.grdPresDetail, KcbDonthuocChitiet.Columns.IdThuoc, this.txtDrugID.Text);
                this.UpdateDataWhenChanged();
                this.ClearControl();
                this.txtdrug.Focus();
                this.txtdrug.SelectAll();
                this.m_dtDanhmucthuoc.DefaultView.RowFilter = "1=2";
                this.m_dtDanhmucthuoc.AcceptChanges();
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
            }
        }

        private void AddtoView(DataRow newDr, int newQuantity)
        {
            DataRow[] rowArray = this.m_dtDonthuocChitiet_View.Select(KcbDonthuocChitiet.Columns.IdThuoc + "=" + Utility.sDbnull(newDr[KcbDonthuocChitiet.Columns.IdThuoc], "-1") + "AND " + KcbDonthuocChitiet.Columns.DonGia + "=" + Utility.sDbnull(newDr[KcbDonthuocChitiet.Columns.DonGia], "-1"));
            if (rowArray.Length <= 0)
            {
                this.m_dtDonthuocChitiet_View.ImportRow(newDr);
            }
            else
            {
                rowArray[0][KcbDonthuocChitiet.Columns.SoLuong] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong], 0) + newQuantity;
                rowArray[0]["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.DonGia]);
                rowArray[0]["TT"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.DonGia]) + Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu]));
                rowArray[0]["TT_BHYT"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BhytChitra]);
                rowArray[0]["TT_BN"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0) + Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu], 0));
                rowArray[0]["TT_PHUTHU"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu], 0);
                rowArray[0]["TT_BN_KHONG_PHUTHU"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0);
                rowArray[0][KcbDonthuocChitiet.Columns.SttIn] = Math.Min(Utility.Int32Dbnull(newDr[KcbDonthuocChitiet.Columns.SttIn], 0), Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SttIn], 0));
                this.m_dtDonthuocChitiet_View.AcceptChanges();
            }
        }

        private void AutoCompleteDmucChung()
        {
            try
            {
                try
                {
                    List<string> lstLoai = new List<string> { "CDDT" };
                    DataTable source = THU_VIEN_CHUNG.LayDulieuDanhmucChung(lstLoai, true);
                    if (source != null)
                    {
                        if (!source.Columns.Contains("ShortCut"))
                        {
                            source.Columns.Add(new DataColumn("ShortCut", typeof(string)));
                        }
                        foreach (DataRow row in source.Rows)
                        {
                            string str = "";
                            string str2 = row["TEN"].ToString().Trim() + " " + Utility.Bodau(row["TEN"].ToString().Trim());
                            str = row["MA"].ToString().Trim();
                            string[] strArray = str2.ToLower().Split(new char[] { ' ' });
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
                        List<string> list = new List<string>();
                        list = source.AsEnumerable().Where<DataRow>(p => (p.Field<string>("LOAI").ToString() == "CDDT")).Select<DataRow, string>(p => ("-1#" + p.Field<string>("MA").ToString() + "@" + p.Field<string>("TEN").ToString() + "@" + p.Field<string>("shortcut").ToString())).ToList<string>();
                        this.txtCachDung.AutoCompleteList = list;
                        this.txtCachDung.TextAlign = HorizontalAlignment.Center;
                        this.txtCachDung.CaseSensitive = false;
                        this.txtCachDung.MinTypedCharacters = 1;
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
                this.AllowTextChanged = false;
                PropertyLib._MayInProperties.InDonthuocsaukhiluu = this.chkSaveAndPrint.Checked;
                PropertyLib.SaveProperty(PropertyLib._MayInProperties);
            }
            catch
            {
            }
            finally
            {
                this.AllowTextChanged = true;
            }
        }

        private void BindDoctorAssignInfo()
        {
            try
            {
                DataTable data = THU_VIEN_CHUNG.LaydanhsachBacsi(-1,0);
                txtBacsi.Init(data, new List<string>() { DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.MaNhanvien, DmucNhanvien.Columns.TenNhanvien });
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

        private void CauHinh()
        {
            if (PropertyLib._ThamKhamProperties != null)
            {
                this.cboA4.Text = (PropertyLib._MayInProperties.CoGiayInDonthuoc == Papersize.A4) ? "A4" : "A5";
            }
            this.chkAutoPaymentAfterSave.Checked = PropertyLib._QuaythuocProperties.Tudongthanhtoan;
            this.cboPrintPreview.SelectedIndex = PropertyLib._MayInProperties.PreviewInDonthuoc ? 0 : 1;
            this.cboLaserPrinters.Text = PropertyLib._MayInProperties.TenMayInBienlai;
            this.pnlPrint.Visible = PropertyLib._ThamKhamProperties.ChophepIndonthuoc;
            this.chkSaveAndPrint.Checked = PropertyLib._MayInProperties.InDonthuocsaukhiluu;
            //this.cmdPrintPres.Visible = PropertyLib._ThamKhamProperties.ChophepIndonthuoc;
            this.chkHienthithuoctheonhom.Checked = PropertyLib._ThamKhamProperties.Hienthinhomthuoc;
            globalVariables.KHOKEDON = PropertyLib._ThamKhamProperties.IDKho;
            chkThemmoilientuc.Checked = PropertyLib._QuaythuocProperties.Themmoilientuc;
            txtChiDanThem.TabStop = !PropertyLib._QuaythuocProperties.BoquaChidanthem;
            this.ModifyButton();
        }

        private void cboA4_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.CoGiayInDonthuoc = (this.cboA4.SelectedIndex == 0) ? Papersize.A4 : Papersize.A5;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        private void cboLaserPrinters_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveDefaultPrinter();
        }

        private void cboPrintPreview_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.PreviewInDonthuoc = this.cboPrintPreview.SelectedIndex == 0;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        private void cboStock_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((this.blnHasLoaded && (this.cboStock.Items.Count > 0)) && ((this.cboStock.SelectedValue == null) || (this.cboStock.SelectedValue.ToString() != "-1")))
                {
                    globalVariables.KHOKEDON = Utility.Int32Dbnull(this.cboStock.SelectedValue, -1);
                    PropertyLib._ThamKhamProperties.IDKho = globalVariables.KHOKEDON;
                    PropertyLib.SaveProperty(PropertyLib._ThamKhamProperties);
                    int num = Utility.Int32Dbnull(this.cboStock.SelectedValue, -1);
                    if ((num > 0) && (this.blnHasLoaded && (this.cboStock.Items.Count > 0)))
                    {
                        this.m_dtDanhmucthuoc = this._KEDONTHUOC.LayThuoctrongkhokedon(num, "THUOC", "DV", 0,0, globalVariables.MA_KHOA_THIEN);
                        this.ProcessData();
                        TDmucKho kho = ReadOnlyRecord<TDmucKho>.FetchByID(num);
                        this.rowFilter = "1=1";
                        this.txtdrug.AllowedSelectPrice = Utility.Byte2Bool(kho.ChophepChongia.Value);
                        this.txtdrug.dtData = this.m_dtDanhmucthuoc;
                        this.txtdrug.ChangeDataSource();
                        this.txtdrug.Focus();
                        this.txtdrug.SelectAll();
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
            string containGuide = this.GetContainGuide();
            this.txtChiDanDungThuoc.Text = containGuide;
        }

        private void chkAskbeforeDeletedrug_CheckedChanged(object sender, EventArgs e)
        {
            PropertyLib._ThamKhamProperties.Hoitruockhixoathuoc = this.chkAskbeforeDeletedrug.Checked;
            PropertyLib.SaveProperty(PropertyLib._ThamKhamProperties);
        }

        private void chkHienthithuoctheonhom_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                PropertyLib._ThamKhamProperties.Hienthinhomthuoc = this.chkHienthithuoctheonhom.Checked;
                PropertyLib.SaveProperty(PropertyLib._ThamKhamProperties);
                this.grdPresDetail.RootTable.Groups.Clear();
                if (this.chkHienthithuoctheonhom.Checked)
                {
                    GridEXColumn column = this.grdPresDetail.RootTable.Columns["ma_loaithuoc"];
                    GridEXGroup group = new GridEXGroup(column)
                    {
                        GroupPrefix = "Loại thuốc: "
                    };
                    this.grdPresDetail.RootTable.Groups.Add(group);
                }
            }
            catch
            {
            }
        }

        private void chkNgayTaiKham_CheckedChanged(object sender, EventArgs e)
        {
            this.dtNgayKhamLai.Enabled = this.chkNgayTaiKham.Checked;
        }

        private void chkSaveAndPrint_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                PropertyLib._MayInProperties.InDonthuocsaukhiluu = this.chkSaveAndPrint.Checked;
                PropertyLib.SaveProperty(PropertyLib._MayInProperties);
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi khi lưu trạng th\x00e1i-->" + exception.Message);
            }
        }

        private void ClearControl()
        {
            foreach (Control control in this.pnlKedon.Controls)
            {
                if (control is EditBox)
                {
                    ((EditBox)control).Clear();
                }
                if (control is TextBox)
                {
                    ((TextBox)control).Clear();
                }
                this.txtSoluong.Text = "1";
                this.txtChiDanDungThuoc.Clear();
            }
            this.ModifyButton();
        }

        private void cmdAddDetail_Click(object sender, EventArgs e)
        {
            this.AddPreDetail();
            this.Manual = true;
        }

        private void cmdCauHinh_Click(object sender, EventArgs e)
        {
            new frm_Properties(PropertyLib._ThamKhamProperties).ShowDialog();
            this.CauHinh();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                this.setMsg(this.lblMsg, "", false);
                if (this.grdPresDetail.GetCheckedRows().Length <= 0)
                {
                    this.setMsg(this.lblMsg, "Bạn phải chọn thuốc để xóa", true);
                    this.grdPresDetail.Focus();
                }
                else
                {
                    int num;
                    foreach (GridEXRow row in this.grdPresDetail.GetCheckedRows())
                    {
                        num = Utility.Int32Dbnull(row.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, -1);
                        if ((num > 0) && (new SubSonic.Select().From(KcbDonthuocChitiet.Schema).Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(num).And(KcbDonthuocChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1).GetRecordCount() > 0))
                        {
                            this.setMsg(this.lblMsg, "Bản ghi đã thanh toán, bạn không thể xóa", true);
                            this.grdPresDetail.Focus();
                            return;
                        }
                    }
                    if (!PropertyLib._ThamKhamProperties.Hoitruockhixoathuoc || Utility.AcceptQuestion("Bạn Có muốn xóa các thuốc đang chọn hay không?", "thông báo xóa", true))
                    {
                        foreach (GridEXRow row in this.grdPresDetail.GetCheckedRows())
                        {
                            num = Utility.Int32Dbnull(row.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, -1);
                            if (num > 0)
                            {
                                this._KEDONTHUOC.XoaChitietDonthuoc(num);
                            }
                            row.Delete();
                            this.grdPresDetail.UpdateData();
                            this.m_dtDonthuocChitiet.AcceptChanges();
                        }
                        this.m_dtDonthuocChitiet.AcceptChanges();
                        this.m_blnCancel = true;
                        this.UpdateDataWhenChanged();
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
                if (this.grdPresDetail.RowCount <= 0)
                {
                    this.m_enAct = action.Insert;
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
            cmdPrintPres.Enabled = false;
            cmdSavePres.PerformClick();
            this.PrintPres(Utility.Int32Dbnull(this.txtPres_ID.Text));
            cmdPrintPres.Enabled = true;
        }

        private void cmdSavePres_Click(object sender, EventArgs e)
        {
            try
            {
                this.cmdSavePres.Enabled = false;
                this.isSaved = true;
                if (this.grdPresDetail.RowCount <= 0)
                {
                    this.setMsg(this.lblMsg, "Bạn cần kê ít nhất một chi tiết thuốc trong đơn", true);
                    this.txtdrug.Focus();
                    this.txtdrug.SelectAll();
                }
                else
                {
                    List<KcbDonthuocChitiet> changedData = this.GetChangedData();
                    if(changedData ==null ) return;
                    this.PerformAction((changedData == null) ? new List<KcbDonthuocChitiet>().ToArray() : changedData.ToArray());
                }
                this.cmdSavePres.Enabled = true;
                if (PropertyLib._QuaythuocProperties.Tudongthanhtoan && _OnPayment != null)
                {
                    m_blnCancel = true;
                    if (_OnPayment != null) _OnPayment(objBenhnhan.IdBenhnhan);
                }
                this.Manual = false;
                this.hasChanged = false;
                if (!chkThemmoilientuc.Checked) base.Close();
                else
                    Prepare4Insert();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
        }
        void Prepare4Insert()
        {
            m_enAct = action.Insert;
            objBenhnhan = null;
            txtMaBN.Clear();
            txtMaLankham.Clear();
            txtTEN_BN.Clear();
            txtDiachi.Clear();
            txtSoDT.Clear();
            m_dtDonthuocChitiet.Rows.Clear();
            ModifyButton();
            txtTEN_BN.Focus();

        }
        private void cmdUpdateChiDan_Click(object sender, EventArgs e)
        {
            this.UpdateChiDanThem();
        }

       

        private KcbDonthuocChitiet[] CreateArrayPresDetail()
        {
            this._temp = ActionResult.Success;
            int index = 0;
            KcbDonthuocChitiet[] chitietArray = new KcbDonthuocChitiet[this.m_dtDonthuocChitiet.DefaultView.Count];
            try
            {
                foreach (DataRowView view in this.m_dtDonthuocChitiet.DefaultView)
                {
                    long num2 = Utility.Int64Dbnull(view[KcbDonthuocChitiet.Columns.IdThuockho], -1);
                    decimal num3 = CommonLoadDuoc.SoLuongTonTrongKho(-1L,
                        Utility.Int32Dbnull(view[KcbDonthuocChitiet.Columns.IdKho], -1),
                        Utility.Int16Dbnull(view[KcbDonthuocChitiet.Columns.IdThuoc], -1),
                        Utility.Int64Dbnull(view[KcbDonthuocChitiet.Columns.IdThuockho], -1),
                        new int?(
                            Utility.Int32Dbnull(
                                THU_VIEN_CHUNG.Laygiatrithamsohethong("KIEMTRATHUOC_CHOXACNHAN", "1", false), 1)),
                        (byte) 0);
                    if (this.m_enAct == action.Update)
                    {
                        decimal soLuong = 0;
                        KcbDonthuocChitiet chitiet =
                            new SubSonic.Select(new TableSchema.TableColumn[] {KcbDonthuocChitiet.SoLuongColumn}).From(
                                KcbDonthuocChitiet.Schema)
                                .Where(KcbDonthuocChitiet.IdChitietdonthuocColumn)
                                .IsEqualTo(Utility.Int32Dbnull(view[KcbDonthuocChitiet.Columns.IdChitietdonthuoc], -1))
                                .ExecuteSingle<KcbDonthuocChitiet>();
                        if (chitiet != null)
                        {
                            soLuong = chitiet.SoLuong;
                        }
                        num3 += soLuong;
                    }
                    if (Utility.Int32Dbnull(view[KcbDonthuocChitiet.Columns.SoLuong], 0) > num3)
                    {
                        Utility.ShowMsg(string.Format("Số lượng thuốc {0}({1}) vượt quá số lượng tồn trong kho{2}({3}) \nBạn cần chỉnh lại số lượng thuốc!", new object[] { Utility.sDbnull(view[DmucThuoc.Columns.TenThuoc], "").ToString(), Utility.Int32Dbnull(view[KcbDonthuocChitiet.Columns.SoLuong], 0).ToString(), Utility.sDbnull(view[TDmucKho.Columns.TenKho], "").ToString(), num3.ToString() }));
                        this._temp = ActionResult.NotEnoughDrugInStock;
                        return null;
                    }
                    chitietArray[index] = new KcbDonthuocChitiet();
                    chitietArray[index].IdDonthuoc = this.IdDonthuoc;
                    chitietArray[index].IdChitietdonthuoc = Utility.Int32Dbnull(view[KcbDonthuocChitiet.Columns.IdChitietdonthuoc], -1);
                    chitietArray[index].IdBenhnhan = -1;
                    chitietArray[index].MaLuotkham = "";
                    chitietArray[index].IdKho = new int?(Utility.Int32Dbnull(view[KcbDonthuocChitiet.Columns.IdKho], -1));
                    chitietArray[index].IdThuoc = Utility.Int16Dbnull(view[KcbDonthuocChitiet.Columns.IdThuoc], -1);
                    chitietArray[index].TrangthaiThanhtoan = Utility.ByteDbnull(view[KcbDonthuocChitiet.Columns.TrangthaiThanhtoan], 0);
                    chitietArray[index].SttIn = new short?(Utility.Int16Dbnull(view[KcbDonthuocChitiet.Columns.SttIn], 1));
                    chitietArray[index].TrangthaiHuy = 0;
                    chitietArray[index].IdThuockho = new long?(num2);
                    chitietArray[index].GiaNhap = new decimal?(Utility.DecimaltoDbnull(view[KcbDonthuocChitiet.Columns.GiaNhap], -1));
                    chitietArray[index].GiaBan = new decimal?(Utility.DecimaltoDbnull(view[KcbDonthuocChitiet.Columns.GiaBan], -1));
                    chitietArray[index].Vat = new decimal?(Utility.DecimaltoDbnull(view[KcbDonthuocChitiet.Columns.Vat], -1));
                    chitietArray[index].SoLo = Utility.sDbnull(view[KcbDonthuocChitiet.Columns.SoLo], -1);
                    chitietArray[index].MaNhacungcap = Utility.sDbnull(view[KcbDonthuocChitiet.Columns.MaNhacungcap], -1);
                    chitietArray[index].NgayHethan = Utility.ConvertDate(view["sNgayhethan"].ToString()).Date;
                    chitietArray[index].SoluongHuy = 0;
                    chitietArray[index].TuTuc = 0;
                    chitietArray[index].SoLuong = Utility.Int32Dbnull(view[KcbDonthuocChitiet.Columns.SoLuong], 0);
                    chitietArray[index].DonGia = Utility.DecimaltoDbnull(view[KcbDonthuocChitiet.Columns.DonGia], 0);
                    chitietArray[index].PhuThu = new decimal?(Utility.Int16Dbnull(view[KcbDonthuocChitiet.Columns.PhuThu], 0));
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
                    chitietArray[index].NgayTao = new DateTime?(globalVariables.SysDate);
                    chitietArray[index].NguoiTao = globalVariables.UserName;
                    chitietArray[index].MaDoituongKcb = Utility.sDbnull("DV");

                    chitietArray[index].BhytChitra = 0;
                    chitietArray[index].BnhanChitra = new decimal?(Utility.DecimaltoDbnull(view[KcbDonthuocChitiet.Columns.DonGia], 0));
                    chitietArray[index].PtramBhyt = 0;

                    index++;
                }
            }
            catch (Exception)
            {
            }
            return chitietArray;
        }

        private KcbDonthuoc CreateNewPres()
        {
            KcbDonthuoc donthuoc = new KcbDonthuoc
            {
                MaLuotkham = "",
                IdBenhnhan =-1,
                MaKhoaThuchien = globalVariables.MA_KHOA_THIEN
             
            };

            if (chktaikham.Checked)
            {
                donthuoc.NgayTaikham = dtpNgaytaikham.Value;
            }
            else
            {
                donthuoc.NgayTaikham = null;
            }
                
            
            donthuoc.NgayKedon = this.dtpCreatedDate.Value;
            donthuoc.MaDoituongKcb = "DV";
            donthuoc.TenDonthuoc = "Đơn thuốc tại quầy";
           
                donthuoc.IdPhongkham = -1;
                donthuoc.IdGiuongNoitru = -1;
           
            donthuoc.IdKhoadieutri = new int?(globalVariables.idKhoatheoMay);
            donthuoc.TrangthaiThanhtoan = 0;
            donthuoc.IdBacsiChidinh = new short?(globalVariables.gv_intIDNhanvien);
            donthuoc.TrangThai = 0;
            donthuoc.NguoiTao = globalVariables.UserName;
            donthuoc.NgayTao = globalVariables.SysDate;
            donthuoc.IdDonthuocthaythe = -1;
            donthuoc.IdKham = new long?((long)this.id_kham);
            donthuoc.NgayTao = globalVariables.SysDate;
            donthuoc.NguoiTao = globalVariables.UserName;
            donthuoc.Noitru = new byte?(this.Noi_tru);
            donthuoc.TaiKham = txtdoanbenh.Text;
            donthuoc.LoidanBacsi = txtdando.Text;
            donthuoc.KieuDonthuoc = 2;//0= Đơn thuốc thường;1= Đơn thuốc bổ sung;2=Đơn thuốc tại quầy;3=Đơn tiêm chủng
            donthuoc.KieuThuocvattu = KIEU_THUOC_VT;
            if (this.m_enAct == action.Update)
            {
                donthuoc.IdDonthuoc = Utility.Int32Dbnull(this.txtPres_ID.Text, -1);
            }
            if (this.m_enAct == action.Update)
            {
                donthuoc.NguoiSua = globalVariables.UserName;
                donthuoc.NgaySua = new DateTime?(globalVariables.SysDate);
            }
            return donthuoc;
        }

        private void CreateViewTable()
        {
            try
            {
                this.m_dtDonthuocChitiet_View = this.m_dtDonthuocChitiet.Clone();
                foreach (DataRow row in this.m_dtDonthuocChitiet.Rows)
                {
                    row["CHON"] = 0;
                    DataRow[] rowArray = this.m_dtDonthuocChitiet_View.Select(KcbDonthuocChitiet.Columns.IdThuoc + "=" + Utility.sDbnull(row[KcbDonthuocChitiet.Columns.IdThuoc], "-1") + "AND " + KcbDonthuocChitiet.Columns.DonGia + "=" + Utility.sDbnull(row[KcbDonthuocChitiet.Columns.DonGia], "-1"));
                    if (rowArray.Length <= 0)
                    {
                        row["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia]);
                        row["TT"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia]) + Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu]));
                        row["TT_BHYT"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BhytChitra]);
                        row["TT_BN"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BnhanChitra], 0) + Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu], 0));
                        row["TT_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.PhuThu], 0);
                        row["TT_BN_KHONG_PHUTHU"] = Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.BnhanChitra], 0);
                        this.m_dtDonthuocChitiet_View.ImportRow(row);
                    }
                    else
                    {
                        rowArray[0][KcbDonthuocChitiet.Columns.SoLuong] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong], 0) + Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong], 0);
                        rowArray[0]["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.DonGia]);
                        rowArray[0]["TT"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.DonGia]) + Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu]));
                        rowArray[0]["TT_BHYT"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BhytChitra]);
                        rowArray[0]["TT_BN"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0) + Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu], 0));
                        rowArray[0]["TT_PHUTHU"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu], 0);
                        rowArray[0]["TT_BN_KHONG_PHUTHU"] = Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0);
                        rowArray[0][KcbDonthuocChitiet.Columns.SttIn] = Math.Min(Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SttIn], 0), Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SttIn], 0));
                        this.m_dtDonthuocChitiet_View.AcceptChanges();
                    }
                }
                this.m_dtDonthuocChitiet_View.AcceptChanges();
                Utility.SetDataSourceForDataGridEx_Basic(this.grdPresDetail, this.m_dtDonthuocChitiet_View, false, true, "1=1", KcbDonthuocChitiet.Columns.SttIn);
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
                    predicate = q => lstIdChitietDonthuoc.Contains(Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc]));
                }
                DataRow[] rowArray = this.m_dtDonthuocChitiet.Select("1=1").AsEnumerable<DataRow>().Where<DataRow>(predicate).ToArray<DataRow>();
                for (int i = 0; i <= (rowArray.Length - 1); i++)
                {
                    this.m_dtDonthuocChitiet.Rows.Remove(rowArray[i]);
                }
                this.m_dtDonthuocChitiet.AcceptChanges();
            }
            catch
            {
            }
        }

        private void deletefromDatatable(List<int> lstDeleteId, int lastdetailid, int soluong)
        {
            Func<DataRow, bool> predicate = null;
            Func<DataRow, bool> func2 = null;
            try
            {
                int num;
                if (predicate == null)
                {
                    predicate = q => Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc]) == lastdetailid;
                }
                DataRow[] rowArray = this.m_dtDonthuocChitiet.Select("1=1").AsEnumerable<DataRow>().Where<DataRow>(predicate).ToArray<DataRow>();
                for (num = 0; num <= (rowArray.Length - 1); num++)
                {
                    if (soluong <= 0)
                    {
                        this.m_dtDonthuocChitiet.Rows.Remove(rowArray[num]);
                    }
                    else
                    {
                        rowArray[num][KcbDonthuocChitiet.Columns.SoLuong] = soluong;
                    }
                }
                if (func2 == null)
                {
                    func2 = q => lstDeleteId.Contains(Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc], 0));
                }
                rowArray = this.m_dtDonthuocChitiet.Select("1=1").AsEnumerable<DataRow>().Where<DataRow>(func2).ToArray<DataRow>();
                for (num = 0; num <= (rowArray.Length - 1); num++)
                {
                    this.m_dtDonthuocChitiet.Rows.Remove(rowArray[num]);
                }
                this.m_dtDonthuocChitiet.AcceptChanges();
            }
            catch
            {
            }
        }

      

        private void DSACH_ICD(EditBox tEditBox, string LOAITIMKIEM, int CP)
        {
            try
            {
                this.Selected = false;
                string filterExpression = "";
                if (LOAITIMKIEM.ToUpper() == DmucChung.Columns.Ten)
                {
                    filterExpression = " Disease_Name like '%" + tEditBox.Text + "%' OR FirstChar LIKE '%" + tEditBox.Text + "%'";
                }
                else if (LOAITIMKIEM == DmucChung.Columns.Ma)
                {
                    filterExpression = DmucBenh.Columns.MaBenh + " LIKE '%" + tEditBox.Text + "%'";
                }
                DataRow[] source = this.dt_ICD.Select(filterExpression);
                if (source.Length == 1)
                {
                    if (CP == 0)
                    {
                        this.txtMaBenhChinh.Text = "";
                        this.txtMaBenhChinh.Text = Utility.sDbnull(source[0][DmucBenh.Columns.MaBenh], "");
                        this.hasMorethanOne = false;
                        this.txtMaBenhChinh_TextChanged(this.txtMaBenhChinh, new EventArgs());
                        this.txtMaBenhChinh.Focus();
                    }
                    else if (CP == 1)
                    {
                        this.txtMaBenhphu.Text = Utility.sDbnull(source[0][DmucBenh.Columns.MaBenh], "");
                        this.hasMorethanOne = false;
                        this.txtMaBenhphu_TextChanged(this.txtMaBenhphu, new EventArgs());
                        this.txtMaBenhphu_KeyDown(this.txtMaBenhphu, new KeyEventArgs(Keys.Enter));
                        this.Selected = false;
                    }
                }
                else if (source.Length > 1)
                {
                    frm_DanhSach_ICD h_icd = new frm_DanhSach_ICD(CP)
                    {
                        dt_ICD = source.CopyToDataTable<DataRow>()
                    };
                    h_icd.ShowDialog();
                    if (!h_icd.has_Cancel)
                    {
                        List<GridEXRow> lstSelectedRows = h_icd.lstSelectedRows;
                        if (CP == 0)
                        {
                            this.isLike = false;
                            this.txtMaBenhChinh.Text = "";
                            this.txtMaBenhChinh.Text = Utility.sDbnull(lstSelectedRows[0].Cells[DmucBenh.Columns.MaBenh].Value, "");
                            this.hasMorethanOne = false;
                            this.txtMaBenhChinh_TextChanged(this.txtMaBenhChinh, new EventArgs());
                            this.txtMaBenhChinh_KeyDown(this.txtMaBenhChinh, new KeyEventArgs(Keys.Enter));
                            this.Selected = false;
                        }
                        else if (CP == 1)
                        {
                            if (lstSelectedRows.Count == 1)
                            {
                                this.isLike = false;
                                this.txtMaBenhphu.Text = "";
                                this.txtMaBenhphu.Text = Utility.sDbnull(lstSelectedRows[0].Cells[DmucBenh.Columns.MaBenh].Value, "");
                                this.hasMorethanOne = false;
                                this.txtMaBenhphu_TextChanged(this.txtMaBenhphu, new EventArgs());
                                this.txtMaBenhphu_KeyDown(this.txtMaBenhphu, new KeyEventArgs(Keys.Enter));
                                this.Selected = false;
                            }
                            else
                            {
                                foreach (GridEXRow row in lstSelectedRows)
                                {
                                    this.isLike = false;
                                    this.txtMaBenhphu.Text = "";
                                    this.txtMaBenhphu.Text = Utility.sDbnull(row.Cells[DmucBenh.Columns.MaBenh].Value, "");
                                    this.hasMorethanOne = false;
                                    this.txtMaBenhphu_TextChanged(this.txtMaBenhphu, new EventArgs());
                                    this.txtMaBenhphu_KeyDown(this.txtMaBenhphu, new KeyEventArgs(Keys.Enter));
                                    this.Selected = false;
                                }
                                this.hasMorethanOne = true;
                            }
                        }
                        tEditBox.Focus();
                    }
                    else
                    {
                        this.hasMorethanOne = true;
                        tEditBox.Focus();
                    }
                }
                else
                {
                    this.hasMorethanOne = true;
                    tEditBox.SelectAll();
                }
            }
            catch
            {
            }
            finally
            {
                this.isLike = true;
            }
        }

        private void frm_KE_DONTHUOC_BN_NEW_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.isSaved)
            {
                List<KcbDonthuocChitiet> changedData = this.GetChangedData();
                if (((changedData != null) && (changedData.Count > 0)) && Utility.AcceptQuestion("Bạn đã thay đổi đơn thuốc nhưng chưa lưu lại. Bạn Có muốn lưu đơn thuốc trước khi tho\x00e1t hay không? Nhấn Yes để lưu đơn thuốc. Nhấn No để không lưu đơn thuốc", "Cảnh báo", true))
                {
                    this.cmdSavePres_Click(this.cmdSavePres, new EventArgs());
                }
            }
        }

        private void frm_KE_DONTHUOC_BN_NEW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F11)
            {
                Utility.ShowMsg(base.ActiveControl.Name);
            }
            else if (e.KeyCode == Keys.F4 || (e.Control && e.KeyCode==Keys.P))
            {
                this.cmdPrintPres.PerformClick();
            }
            //else if (e.KeyCode == Keys.A && e.Control)
            //{
            //    this.cmdAddDetail.PerformClick();
            //}
            else if (e.KeyCode == Keys.S && e.Control)
            {
                this.cmdSavePres.PerformClick();
            }
            else if (e.KeyCode == Keys.F3)
            {
                this.txtdrug.Focus();
                this.txtdrug.SelectAll();
            }
            else
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (this.uiTabPage1.ActiveControl != null && this.uiTabPage1.ActiveControl.Name == txtMaBenhphu.Name)
                        return;
                    else
                        SendKeys.Send("{TAB}");
                }
                if (e.KeyCode == Keys.F5)
                {
                    this.cboStock_SelectedIndexChanged(this.cboStock, new EventArgs());
                }
                if (e.KeyCode == Keys.Escape)
                {
                    this.cmdExit_Click(this.cmdExit, new EventArgs());
                }
            }
        }
         DataTable dtBenhnhan = new DataTable();
         private void LoadDanhSachBenhNhan()
         {
             dtBenhnhan = SPs.KcbKedonLayDanhSachBenhNhan().GetDataSet().Tables[0];
             txtTEN_BN.Init(dtBenhnhan);
         }
        DataTable _mDtqheCamchidinhChungphieu = new DataTable();
        private void frm_KE_DONTHUOC_BN_NEW_Load(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    txtMaBN.Text = "-1";
                    cboPatientSex.SelectedIndex = 0;
                    this.AutoloadSaveAndPrintConfig();
                    this.BindDoctorAssignInfo();
                    this.GetStockRelatedToDoctor();
                    txtNgheNghiep.Init();
                    txtdando.Init();
                    txtdoanbenh.Init();
                    this.txtChanDoan.Init();
                    this.txtCachDung.Init();
                    txtChiDanThem.Init();
                    AddAutoCompleteDiaChi();
                    this.LoadLaserPrinters();
                  
                    this.GetData();
                    this.GetDataPresDetail();
                   
                    _mDtqheCamchidinhChungphieu = new Select().From(QheCamchidinhChungphieu.Schema).Where(QheCamchidinhChungphieu.Columns.Loai).IsEqualTo(1).ExecuteDataSet().Tables[0];
                    bool gridView = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("KEDONTHUOC_SUDUNGLUOI", "0", true), 0) == 1;
                    if (!gridView)
                    {
                        gridView = PropertyLib._AppProperties.GridView;
                    }
                    this.txtdrug.GridView = gridView;
                    this.isLoaded = true;
                    this.AllowTextChanged = true;
                    this.blnHasLoaded = true;
                    if (this.dtStockList.Select(TDmucKho.Columns.IdKho + "= " + globalVariables.KHOKEDON).Length > 0)
                    {
                        this.cboStock.SelectedIndex = Utility.GetSelectedIndex(this.cboStock, globalVariables.KHOKEDON.ToString());
                        this.cboStock_SelectedIndexChanged(this.cboStock, new EventArgs());
                    }
                    else
                    {
                        this.cboStock.SelectedIndex = -1;
                    }
                    if (this.cboStock.Items.Count == 0)
                    {
                        Utility.ShowMsg(string.Format("Bệnh nhân {0} thuộc đối tượng {1} chưa Có kho thuốc để kê đơn", this.txtTEN_BN.Text.Trim(), this.txtObjectName.Text.Trim()));
                        base.Close();
                    }
                    this.grdPresDetail.RootTable.Groups.Clear();
                    if (this.chkHienthithuoctheonhom.Checked)
                    {
                        GridEXColumn column = this.grdPresDetail.RootTable.Columns["ma_loaithuoc"];
                        GridEXGroup group = new GridEXGroup(column)
                        {
                            GroupPrefix = "Loại thuốc: "
                        };
                        this.grdPresDetail.RootTable.Groups.Add(group);
                    }
                    LoadDanhSachBenhNhan();
                    this.txtTEN_BN.Focus();
                    this.txtTEN_BN.Select();
                }
                catch(Exception ex)
                {
                    Utility.ShowMsg("Lỗi:"+ ex.Message);
                }
            }
            finally
            {
                txtTEN_BN.Focus();
            }
        }
        private List<KcbDonthuocChitiet> GetChangedData()
        {
            List<KcbDonthuocChitiet> list = new List<KcbDonthuocChitiet>();
            this._temp = ActionResult.Success;
            KcbDonthuocChitiet[] chitietArray = new KcbDonthuocChitiet[this.m_dtDonthuocChitiet.DefaultView.Count];
            try
            {
                foreach (DataRow row in this.m_dtDonthuocChitiet.Rows)
                {
                    long key = Utility.Int64Dbnull(row[KcbDonthuocChitiet.Columns.IdThuockho], -1);
                    //if (this.lstChangeData.ContainsKey(key))
                    //{
                        //string str = this.lstChangeData[key].ToString();
                        //if (this.isChanged(str))
                        //{
                    decimal num3 = CommonLoadDuoc.SoLuongTonTrongKho(-1L, Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.IdKho], -1), Utility.Int16Dbnull(row[KcbDonthuocChitiet.Columns.IdThuoc], -1), key, new int?(Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("KIEMTRATHUOC_CHOXACNHAN", "1", false), 1)), (byte)0);
                            if (this.m_enAct == action.Update)
                            {
                                decimal soLuong = 0;
                                KcbDonthuocChitiet chitiet = new SubSonic.Select(new TableSchema.TableColumn[] { KcbDonthuocChitiet.SoLuongColumn }).From(KcbDonthuocChitiet.Schema).Where(KcbDonthuocChitiet.IdChitietdonthuocColumn).IsEqualTo(Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.IdChitietdonthuoc], -1)).ExecuteSingle<KcbDonthuocChitiet>();
                                if (chitiet != null)
                                {
                                    soLuong = chitiet.SoLuong;
                                }
                                num3 += soLuong;
                            }
                            if (Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong], 0) > num3)
                            {
                                Utility.ShowMsg(string.Format("Số lượng thuốc {0}({1}) vượt quá số lượng tồn trong kho{2}({3}) \nBạn cần chỉnh lại số lượng thuốc!", new object[] { Utility.sDbnull(row[DmucThuoc.Columns.TenThuoc], "").ToString(), Utility.Int32Dbnull(row[KcbDonthuocChitiet.Columns.SoLuong], 0).ToString(), Utility.sDbnull(row[TDmucKho.Columns.TenKho], "").ToString(), num3.ToString() }));
                                this._temp = ActionResult.NotEnoughDrugInStock;
                                return null;
                            }
                            this.hasChanged = true;
                            list.Add(this.GetNewItem(row));
                        //}
                    //}
                }
                return list;
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi khi lấy dữ liệu cập nhật đơn thuốc:\n" + exception.Message);
                return null;
            }
        }

        private string GetContainGuide()
        {
            try
            {
                string yourString = "";
                yourString = yourString + this.txtCachDung.Text + " ";
                if (!string.IsNullOrEmpty(this.txtSoLuongDung.Text))
                {
                    string str3 = yourString;
                    yourString = str3 + "Mỗi ngày dùng " + this.txtSoLuongDung.Text.Trim() + " " + this.txtDonViDung.Text;
                }
                if (!string.IsNullOrEmpty(this.txtSolan.Text))
                {
                    yourString = yourString + " chia làm  " + this.txtSolan.Text + " lần";
                }
                if (!string.IsNullOrEmpty(this.txtChiDanThem.Text))
                {
                    yourString = yourString + ". " + this.txtChiDanThem.Text;
                }
                return Utility.ReplaceString(yourString);
            }
            catch (Exception exception)
            {
                return exception.ToString();
            }
        }



        private void GetData()
        {
            if (this.objBenhnhan != null)
            {
                txtMaBN.Text = Utility.sDbnull(objBenhnhan.IdBenhnhan, -1);
                this.txtDiachi._Text = Utility.sDbnull(this.objBenhnhan.DiaChi,"");
                this.txtObjectName.Text = "Khách hàng mua lẻ";
                this.txtPhone.Text = Utility.sDbnull(objBenhnhan.DienThoai,"");
                this.txtTEN_BN.Text = Utility.sDbnull(objBenhnhan.TenBenhnhan);
                this.txtYearBirth.Text = Utility.sDbnull(objBenhnhan.NamSinh);
                this.txtNgheNghiep._Text = Utility.sDbnull(objBenhnhan.NgheNghiep, "");
                this.txtTuoi.Text = Utility.sDbnull(DateTime.Now.Year - Utility.Int16Dbnull(objBenhnhan.NamSinh));
            }
            else
            {
                txtMaBN.Text = "-1";
            }
        }

        private void GetDataPresDetail()
        {
            KcbDonthuoc donthuoc = ReadOnlyRecord<KcbDonthuoc>.FetchByID(Utility.Int32Dbnull(this.txtPres_ID.Text));
            if (donthuoc != null)
            {
                this.IdDonthuoc = Utility.Int32Dbnull(donthuoc.IdDonthuoc);
                this.txtBacsi.SetId(Utility.Int16Dbnull(donthuoc.IdBacsiChidinh));
                this.dtpCreatedDate.Value = Convert.ToDateTime(donthuoc.NgayKedon);

                this.barcode.Data = Utility.sDbnull(this.IdDonthuoc);
                this.txtLoiDanBS.Text = Utility.sDbnull(donthuoc.LoidanBacsi);
                this.txtKhamLai.Text = Utility.sDbnull(donthuoc.TaiKham);
                if (!string.IsNullOrEmpty(Utility.sDbnull(donthuoc.NgayTaikham)))
                {
                    this.chkNgayTaiKham.Checked = true;
                    this.dtNgayKhamLai.Value = Convert.ToDateTime(donthuoc.NgayTaikham);
                }
            }
            this.m_dtDonthuocChitiet = this._KEDONTHUOC.Laythongtinchitietdonthuoc(this.IdDonthuoc);
            this.CreateViewTable();
            if (!this.m_dtDonthuocChitiet.Columns.Contains("CHON"))
            {
                this.m_dtDonthuocChitiet.Columns.Add("CHON", typeof(int));
            }
            this.UpdateDataWhenChanged();
        }

        private List<int> GetIdChitiet(int id_thuoc, decimal don_gia, ref string s)
        {
            DataRow[] source = this.m_dtDonthuocChitiet.Select(KcbDonthuocChitiet.Columns.IdThuoc + "=" + id_thuoc.ToString() + "AND " + KcbDonthuocChitiet.Columns.DonGia + "=" + don_gia.ToString());
            if (source.Length > 0)
            {
                IEnumerable<string> enumerable = (from q in source.AsEnumerable<DataRow>() select Utility.sDbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).Distinct<string>();
                s = string.Join(",", enumerable.ToArray<string>());
                return (from q in source.AsEnumerable<DataRow>() select Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).Distinct<int>().ToList<int>();
            }
            return new List<int>();
        }

        private void GetInfor(int idThuoc, ref string tenthuoc, ref string tenDonvitinh, ref string hoatchat, ref string donviTinh, ref string chidanThem, ref string motaThem, ref string cachDung, ref string soluongDung, ref string solanDung)
        {
            try
            {
                DataRow[] rowArray = this.m_dtDonthuocChitiet.Select("id_thuoc=" + idThuoc.ToString());
                if (rowArray.Length > 0)
                {
                    tenthuoc = Utility.sDbnull(rowArray[0][DmucThuoc.Columns.TenThuoc], "");
                    tenDonvitinh = Utility.sDbnull(rowArray[0]["ten_donvitinh"], "");
                    donviTinh = Utility.sDbnull(rowArray[0][KcbDonthuocChitiet.Columns.DonviTinh], "");
                    chidanThem = Utility.sDbnull(rowArray[0][KcbDonthuocChitiet.Columns.ChidanThem], "");
                    motaThem = Utility.sDbnull(rowArray[0]["mota_them_chitiet"], "");
                    hoatchat = Utility.sDbnull(rowArray[0][DmucThuoc.Columns.HoatChat], "");
                    cachDung = Utility.sDbnull(rowArray[0][KcbDonthuocChitiet.Columns.CachDung], "");
                    soluongDung = Utility.sDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoluongDung], "");
                    solanDung = Utility.sDbnull(rowArray[0][KcbDonthuocChitiet.Columns.SolanDung], "");
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
        }

        private int GetMaxSTT(DataTable dataTable)
        {
            try
            {
                return (Utility.Int32Dbnull(dataTable.AsEnumerable().Max<DataRow, short>(c => c.Field<short>(KcbDonthuocChitiet.Columns.SttIn)), 0) + 1);
            }
            catch (Exception)
            {
                return 1;
            }
        }

        private KcbDonthuocChitiet GetNewItem(DataRow drv)
        {
            KcbDonthuocChitiet chitiet;
            return new KcbDonthuocChitiet
            {
                IdDonthuoc = this.IdDonthuoc,
                IdChitietdonthuoc = Utility.Int32Dbnull(drv[KcbDonthuocChitiet.Columns.IdChitietdonthuoc], -1),
                IdKham = new long?((long)this.id_kham),
                IdKho = new int?(Utility.Int32Dbnull(drv[KcbDonthuocChitiet.Columns.IdKho], -1)),
                IdThuoc = Utility.Int16Dbnull(drv[KcbDonthuocChitiet.Columns.IdThuoc], -1),
                TrangthaiThanhtoan = Utility.ByteDbnull(drv[KcbDonthuocChitiet.Columns.TrangthaiThanhtoan], 0),
                SttIn = new short?(Utility.Int16Dbnull(drv[KcbDonthuocChitiet.Columns.SttIn], 1)),
                TrangthaiHuy = 0,
                IdThuockho = new long?((long)Utility.Int32Dbnull(drv[KcbDonthuocChitiet.Columns.IdThuockho], -1)),
                GiaNhap = new decimal?(Utility.DecimaltoDbnull(drv[KcbDonthuocChitiet.Columns.GiaNhap], -1)),
                GiaBan = new decimal?(Utility.DecimaltoDbnull(drv[KcbDonthuocChitiet.Columns.GiaBan], -1)),
                GiaBhyt = new decimal?(Utility.DecimaltoDbnull(drv[KcbDonthuocChitiet.Columns.GiaBhyt], -1)),
                Vat = new decimal?(Utility.DecimaltoDbnull(drv[KcbDonthuocChitiet.Columns.Vat], -1)),
                SoLo = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.SoLo], -1),
                MaNhacungcap = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.MaNhacungcap], -1),
                NgayHethan = Utility.ConvertDate(drv["sngay_hethan"].ToString()).Date,
                SoluongHuy = 0,
                SluongLinh = 0,
                SluongSua = 0,
                IdThanhtoan = -1,
                TuTuc = new byte?(Utility.ByteDbnull(drv[KcbDonthuocChitiet.Columns.TuTuc], 0)),
                SoLuong = Utility.Int32Dbnull(drv[KcbDonthuocChitiet.Columns.SoLuong], 0),
                DonGia = Utility.DecimaltoDbnull(drv[KcbDonthuocChitiet.Columns.DonGia], 0),
                PhuThu = new decimal?(Utility.Int16Dbnull(drv[KcbDonthuocChitiet.Columns.PhuThu], 0)),
                MotaThem = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.MotaThem], ""),
                TrangthaiBhyt = Utility.ByteDbnull(drv[KcbDonthuocChitiet.Columns.TuTuc], 0),
                TrangThai = 0,
                DaDung = 0,
                ChidanThem = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.ChidanThem], ""),
                CachDung = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.CachDung], ""),
                DonviTinh = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.DonviTinh], ""),
                SolanDung = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.SolanDung], null),
                SoluongDung = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.SoluongDung], null),
                NgayTao = new DateTime?(globalVariables.SysDate),
                NguoiTao = globalVariables.UserName,
                BhytChitra = new decimal?(Utility.DecimaltoDbnull(drv[KcbDonthuocChitiet.Columns.BhytChitra], 0)),
                BnhanChitra = new decimal?(Utility.DecimaltoDbnull(drv[KcbDonthuocChitiet.Columns.BnhanChitra], 0)),
                PtramBhyt = new decimal?(Utility.DecimaltoDbnull(drv[KcbDonthuocChitiet.Columns.PtramBhyt], 0)),
                MaDoituongKcb = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.MaDoituongKcb], "DV"),
                KieuBiendong = Utility.sDbnull(drv[KcbDonthuocChitiet.Columns.KieuBiendong], "EXP")
            };
        }

        private void GetStockRelatedToDoctor()
        {
            try
            {
                this.dtStockList = new DataTable();
                this.dtStockList = CommonLoadDuoc.LAYTHONGTIN_QUAYTHUOC();
                VNS.Libs.DataBinding.BindDataCombobox(this.cboStock, this.dtStockList, TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho);
                this.cboStock.SelectedIndex = Utility.GetSelectedIndex(this.cboStock, PropertyLib._ThamKhamProperties.IDKho.ToString());
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy thông tin combobox");
            }
        }

        private string GetTenBenh(string MaBenh)
        {
            string str = "";
            DataRow[] rowArray = globalVariables.gv_dtDmucBenh.Select(string.Format(DmucBenh.Columns.MaBenh + "='{0}'", MaBenh));
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
            this.CreateViewTable();
        }

        private void grdPresDetail_CellUpdated(object sender, ColumnActionEventArgs e)
        {
        }

        private void grdPresDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                this.mnuDelele_Click(this.mnuDelele, new EventArgs());
            }
        }

        private void grdPresDetail_SelectionChanged(object sender, EventArgs e)
        {
            this.ModifyButton();
        }

        private void grdPresDetail_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                if ((e.Column.Key != KcbDonthuocChitiet.Columns.TuTuc) && (e.Column.Key == KcbDonthuocChitiet.Columns.SoLuong))
                {
                    Func<DataRow, bool> predicate = null;
                    GridEXRow currentRow = this.grdPresDetail.CurrentRow;
                    int id_thuoc = Utility.Int32Dbnull(currentRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, 0);
                    int num = Utility.Int32Dbnull(currentRow.Cells[KcbDonthuocChitiet.Columns.IdThuockho].Value, 0);
                    decimal don_gia = Utility.DecimaltoDbnull(currentRow.Cells[KcbDonthuocChitiet.Columns.DonGia].Value, 0M);
                    this.hasChanged = true;
                    int num2 = Utility.Int32Dbnull(e.InitialValue, 0);
                    int num3 = Utility.Int32Dbnull(e.Value, 0);
                    int num4 = num3 - num2;
                    if (num3 != num2)
                    {
                        if (num3 > num2)
                        {
                            this.AddQuantity(id_thuoc, num, num3 - num2);
                        }
                        else
                        {
                            if (predicate == null)
                            {
                                predicate = q => (Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdThuoc], 0) == id_thuoc) && (Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.DonGia], 0) == don_gia);
                            }
                            DataRow[] rowArray = (from q in this.m_dtDonthuocChitiet.Select("1=1").AsEnumerable<DataRow>().Where<DataRow>(predicate)
                                                  orderby q[KcbDonthuocChitiet.Columns.SttIn] descending
                                                  select q).ToArray<DataRow>();
                            int num5 = num2 - num3;
                            Dictionary<int, int> dictionary = new Dictionary<int, int>();
                            List<int> lstDeleteId = new List<int>();
                            int iddetail = -1;
                            string lstIdChitietDonthuoc = "";
                            for (int i = 0; i <= (rowArray.Length - 1); i++)
                            {
                                if (num5 > 0)
                                {
                                    int num8 = Utility.Int32Dbnull(rowArray[i][KcbDonthuocChitiet.Columns.SoLuong], 0);
                                    if (num8 >= num5)
                                    {
                                        rowArray[i][KcbDonthuocChitiet.Columns.SoLuong] = num8 - num5;
                                        num5 = num8 - num5;
                                        dictionary.Add(Utility.Int32Dbnull(rowArray[i][KcbDonthuocChitiet.Columns.IdChitietdonthuoc]), num5);
                                        iddetail = Utility.Int32Dbnull(rowArray[i][KcbDonthuocChitiet.Columns.IdChitietdonthuoc]);
                                        if (num5 <= 0)
                                        {
                                            lstIdChitietDonthuoc = lstIdChitietDonthuoc + Utility.sDbnull(rowArray[i][KcbDonthuocChitiet.Columns.IdChitietdonthuoc], "-1") + ",";
                                        }
                                        break;
                                    }
                                    rowArray[i][KcbDonthuocChitiet.Columns.SoLuong] = 0;
                                    lstIdChitietDonthuoc = lstIdChitietDonthuoc + Utility.sDbnull(rowArray[i][KcbDonthuocChitiet.Columns.IdChitietdonthuoc], "-1") + ",";
                                    lstDeleteId.Add(Utility.Int32Dbnull(rowArray[i][KcbDonthuocChitiet.Columns.IdChitietdonthuoc]));
                                    num5 -= num8;
                                }
                            }
                            this._KEDONTHUOC.XoaChitietDonthuoc(lstIdChitietDonthuoc, iddetail, num5);
                            this.grdPresDetail.UpdateData();
                            this.deletefromDatatable(lstDeleteId, iddetail, num5);
                        }
                        int num9 = Utility.Int32Dbnull(this.m_dtDonthuocChitiet_View.Select(KcbDonthuocChitiet.Columns.IdThuoc + "=" + id_thuoc.ToString() + " AND " + KcbDonthuocChitiet.Columns.DonGia + "=" + don_gia.ToString())[0][KcbDonthuocChitiet.Columns.SoLuong], 0);
                        if (num4 > 0)
                        {
                            e.Value = num9;
                        }
                        else
                        {
                            num9 = Utility.Int32Dbnull(e.Value, 0);
                            e.Value = e.Value;
                        }
                        DataRow[] rowArray2 = this.m_dtDonthuocChitiet_View.Select(KcbDonthuocChitiet.Columns.IdThuoc + "=" + id_thuoc.ToString());
                        foreach (DataRow row2 in rowArray2)
                        {
                            if ((row2[KcbDonthuocChitiet.Columns.IdThuoc].ToString() == id_thuoc.ToString()) && (Utility.DecimaltoDbnull(row2[KcbDonthuocChitiet.Columns.DonGia], 0M) == don_gia))
                            {
                                row2[KcbDonthuocChitiet.Columns.SoLuong] = num9;
                            }
                            int num10 = Utility.Int32Dbnull(row2[KcbDonthuocChitiet.Columns.SoLuong], 0);
                            if (num10 > 0)
                            {
                                row2["TT_KHONG_PHUTHU"] = num10 * Utility.DecimaltoDbnull(row2[KcbDonthuocChitiet.Columns.DonGia]);
                                row2["TT"] = num10 * (Utility.DecimaltoDbnull(row2[KcbDonthuocChitiet.Columns.DonGia]) + Utility.DecimaltoDbnull(row2[KcbDonthuocChitiet.Columns.PhuThu]));
                                row2["TT_BHYT"] = num10 * Utility.DecimaltoDbnull(row2[KcbDonthuocChitiet.Columns.BhytChitra]);
                                row2["TT_BN"] = num10 * (Utility.DecimaltoDbnull(row2[KcbDonthuocChitiet.Columns.BnhanChitra], 0) + Utility.DecimaltoDbnull(row2[KcbDonthuocChitiet.Columns.PhuThu], 0));
                                row2["TT_PHUTHU"] = num10 * Utility.DecimaltoDbnull(row2[KcbDonthuocChitiet.Columns.PhuThu], 0);
                                row2["TT_BN_KHONG_PHUTHU"] = num10 * Utility.DecimaltoDbnull(row2[KcbDonthuocChitiet.Columns.BnhanChitra], 0);
                            }
                            else
                            {
                                this.m_dtDonthuocChitiet_View.Rows.Remove(row2);
                            }
                        }
                        this.m_dtDonthuocChitiet_View.AcceptChanges();
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
                    this.hasChanged = true;
                    string str = "";
                    long key = Utility.Int64Dbnull(this.grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdThuockho].Value, -1);
                    if (this.lstChangeData.ContainsKey(key))
                    {
                        str = this.lstChangeData[key];
                        str = str.Split(new char[] { '-' })[0] + "-" + e.Value.ToString();
                        this.lstChangeData[key] = str;
                    }
                    else
                    {
                        str = e.InitialValue + "-" + e.Value.ToString();
                        this.lstChangeData.Add(key, str);
                    }
                    DataRow[] rowArray = this.m_dtDonthuocChitiet.Select(KcbDonthuocChitiet.Columns.IdThuockho + "=" + key.ToString());
                    int num2 = Utility.Int32Dbnull(e.Value, Utility.Int32Dbnull(rowArray[0][KcbDonthuocChitiet.Columns.SoLuong]));
                    if (rowArray.Length > 0)
                    {
                        rowArray[0][KcbDonthuocChitiet.Columns.BhytChitra] = num2 * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BhytChitra]);
                        rowArray[0][KcbDonthuocChitiet.Columns.BnhanChitra] = num2 * (Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0) + Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu], 0));
                        rowArray[0]["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(num2) * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.DonGia]);
                        rowArray[0]["TT"] = Utility.Int32Dbnull(num2) * (Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.DonGia]) + Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu]));
                        rowArray[0]["TT_BHYT"] = Utility.Int32Dbnull(num2) * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BhytChitra]);
                        rowArray[0]["TT_BN"] = Utility.Int32Dbnull(num2) * (Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0) + Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu], 0));
                        rowArray[0]["TT_PHUTHU"] = Utility.Int32Dbnull(num2) * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.PhuThu], 0);
                        rowArray[0]["TT_BN_KHONG_PHUTHU"] = Utility.Int32Dbnull(num2) * Utility.DecimaltoDbnull(rowArray[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0);
                    }
                    this.m_dtDonthuocChitiet.AcceptChanges();
                }
            }
            catch
            {
            }
        }

        private void InitEvents()
        {
            base.Load += new EventHandler(this.frm_KE_DONTHUOC_BN_NEW_Load);
            base.KeyDown += new KeyEventHandler(this.frm_KE_DONTHUOC_BN_NEW_KeyDown);
            base.FormClosing += new FormClosingEventHandler(this.frm_KE_DONTHUOC_BN_NEW_FormClosing);
            this.grdPresDetail.KeyDown += new KeyEventHandler(this.grdPresDetail_KeyDown);
            this.grdPresDetail.UpdatingCell += new UpdatingCellEventHandler(this.grdPresDetail_UpdatingCell);
            this.grdPresDetail.CellEdited += new ColumnActionEventHandler(this.grdPresDetail_CellEdited);
            this.grdPresDetail.CellUpdated += new ColumnActionEventHandler(this.grdPresDetail_CellUpdated);
            this.grdPresDetail.SelectionChanged += new EventHandler(this.grdPresDetail_SelectionChanged);
            this.txtPres_ID.TextChanged += new EventHandler(this.txtPres_ID_TextChanged);
            this.txtSoluong.TextChanged += new EventHandler(this.txtSoluong_TextChanged);
            this.txtDrugID.TextChanged += new EventHandler(this.txtDrugID_TextChanged);
            this.txtSolan.TextChanged += new EventHandler(this.txtSolan_TextChanged);
            this.txtSoLuongDung.TextChanged += new EventHandler(this.txtSoLuongDung_TextChanged);
            this.txtCachDung._OnSelectionChanged += new AutoCompleteTextbox_Danhmucchung.OnSelectionChanged(this.txtCachDung__OnSelectionChanged);
            this.txtCachDung.TextChanged += new EventHandler(this.txtCachDung_TextChanged);
            this.chkSaveAndPrint.CheckedChanged += new EventHandler(this.chkSaveAndPrint_CheckedChanged);
            this.chkNgayTaiKham.CheckedChanged += new EventHandler(this.chkNgayTaiKham_CheckedChanged);
            this.mnuDelele.Click += new EventHandler(this.mnuDelele_Click);
            this.cmdSavePres.Click += new EventHandler(this.cmdSavePres_Click);
            this.cmdExit.Click += new EventHandler(this.cmdExit_Click);
            this.cmdDelete.Click += new EventHandler(this.cmdDelete_Click);
            this.cmdDonThuocDaKe.Click += new EventHandler(this.cmdDonThuocDaKe_Click);
            this.cmdUpdaeChiDan.Click += new EventHandler(this.cmdUpdateChiDan_Click);
            this.cmdPrintPres.Click += new EventHandler(this.cmdPrintPres_Click);
            this.cmdAddDetail.Click += new EventHandler(this.cmdAddDetail_Click);
            this.cmdCauHinh.Click += new EventHandler(this.cmdCauHinh_Click);
            this.cboStock.SelectedIndexChanged += new EventHandler(this.cboStock_SelectedIndexChanged);
            this.txtdrug._OnGridSelectionChanged += new AutoCompleteTextbox_Thuoc.OnGridSelectionChanged(this.txtdrug__OnGridSelectionChanged);
            this.cboPrintPreview.SelectedIndexChanged += new EventHandler(this.cboPrintPreview_SelectedIndexChanged);
            this.cboA4.SelectedIndexChanged += new EventHandler(this.cboA4_SelectedIndexChanged);
            this.cboLaserPrinters.SelectedIndexChanged += new EventHandler(this.cboLaserPrinters_SelectedIndexChanged);
            this.chkHienthithuoctheonhom.CheckedChanged += new EventHandler(this.chkHienthithuoctheonhom_CheckedChanged);
            this.chkAskbeforeDeletedrug.CheckedChanged += new EventHandler(this.chkAskbeforeDeletedrug_CheckedChanged);
            this.txtMaBenhChinh.KeyDown += new KeyEventHandler(this.txtMaBenhChinh_KeyDown);
            this.txtMaBenhChinh.TextChanged += new EventHandler(this.txtMaBenhChinh_TextChanged);
            this.txtMaBenhphu.GotFocus += new EventHandler(this.txtMaBenhphu_GotFocus);
            this.txtMaBenhphu.KeyDown += new KeyEventHandler(this.txtMaBenhphu_KeyDown);
            this.txtMaBenhphu.TextChanged += new EventHandler(this.txtMaBenhphu_TextChanged);
            this.txtCachDung._OnShowData += new AutoCompleteTextbox_Danhmucchung.OnShowData(this.txtCachDung__OnShowData);
            this.txtCachDung._OnSaveAs += new AutoCompleteTextbox_Danhmucchung.OnSaveAs(this.txtCachDung__OnSaveAs);
            this.txtdando._OnShowData += new AutoCompleteTextbox_Danhmucchung.OnShowData(this.txtdando__OnShowData);
            this.txtdando._OnSaveAs += new AutoCompleteTextbox_Danhmucchung.OnSaveAs(this.txtdando__OnSaveAs);
            this.txtdoanbenh._OnShowData += new AutoCompleteTextbox_Danhmucchung.OnShowData(this.txtdoanbenh__OnShowData);
            this.txtdoanbenh._OnSaveAs += new AutoCompleteTextbox_Danhmucchung.OnSaveAs(this.txtdoanbenh__OnSaveAs);
            this.txtdrug._OnChangedView += new AutoCompleteTextbox_Thuoc.OnChangedView(this.txtdrug__OnChangedView);
            txtdrug._OnEnterMe += new AutoCompleteTextbox_Thuoc.OnEnterMe(txtdrug__OnEnterMe);
            grd_ICD.ColumnButtonClick += new ColumnActionEventHandler(grd_ICD_ColumnButtonClick);
            cmdSearchBenhChinh.Click += new EventHandler(cmdSearchBenhChinh_Click);
            cmdSearchBenhPhu.Click += new EventHandler(cmdSearchBenhPhu_Click);
            chkAutoPaymentAfterSave.CheckedChanged += new EventHandler(chkAutoPaymentAfterSave_CheckedChanged);
            chkThemmoilientuc.CheckedChanged += new EventHandler(chkThemmoilientuc_CheckedChanged);
            chkBoquachidanthem.CheckedChanged += chkBoquachidanthem_CheckedChanged;
        }

         private void txtCachDung__OnSaveAs()
         {
             txtCachDung.Init();
         }
         private void txtdoanbenh__OnSaveAs()
         {
             txtdoanbenh.Init();
         }
         private void txtdando__OnSaveAs()
         {
             txtdando.Init();
         }
        void chkBoquachidanthem_CheckedChanged(object sender, EventArgs e)
        {
            PropertyLib._QuaythuocProperties.BoquaChidanthem = chkBoquachidanthem.Checked;
            PropertyLib.SaveProperty(PropertyLib._QuaythuocProperties);
            txtChiDanThem.TabStop = !PropertyLib._QuaythuocProperties.BoquaChidanthem;
        }

        void chkThemmoilientuc_CheckedChanged(object sender, EventArgs e)
        {
            PropertyLib._QuaythuocProperties.Themmoilientuc = chkThemmoilientuc.Checked;
            PropertyLib.SaveProperty(PropertyLib._QuaythuocProperties);
        }

        void chkAutoPaymentAfterSave_CheckedChanged(object sender, EventArgs e)
        {
            PropertyLib._QuaythuocProperties.Tudongthanhtoan = chkAutoPaymentAfterSave.Checked;
            PropertyLib.SaveProperty(PropertyLib._QuaythuocProperties);
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
        private bool AllowDrugChanged;
        void txtdrug__OnEnterMe()
        {
            if (Utility.Int32Dbnull(txtdrug.MyID, -1) <= 0) return;
            AllowDrugChanged = true;
            txtDrugID_TextChanged(txtDrugID, new EventArgs());
         //   AutoFill_Chidandungthuoc();
            txtSoluong.Focus();
            txtSoluong.SelectAll();
            //int _idthuoc = Utility.Int32Dbnull(txtdrug.MyID, -1);
            //txtDrugID.Text = _idthuoc.ToString();
        }

        void grd_ICD_ColumnButtonClick(object sender, ColumnActionEventArgs e)
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


        private void InsertPres()
        {
            try
            {
                Dictionary<long, long> lstChitietDonthuoc = new Dictionary<long, long>();
                KcbDonthuocChitiet[] arrDonthuocChitiet = this.CreateArrayPresDetail();
                if (arrDonthuocChitiet != null)
                {
                    this._actionResult = this._KEDONTHUOC.ThemDonThuoc(this.objBenhnhan, this.CreateNewPres(), arrDonthuocChitiet,  ref this.IdDonthuoc, ref lstChitietDonthuoc);
                    switch (this._actionResult)
                    {
                        case ActionResult.Error:
                            this.setMsg(this.lblMsg, "Lỗi trong quá trình lưu đơn thuốc", true);
                            break;

                        case ActionResult.Success:
                            this.UpdateChiDanThem();
                            this.txtPres_ID.Text = this.IdDonthuoc.ToString();
                            this.m_enAct = action.Update;
                            this.setMsg(this.lblMsg, "Bạn thực hiện lưu đơn thuốc thành công", false);
                            this.UpdateDetailID(lstChitietDonthuoc);
                            this.m_blnCancel = false;
                           // this.Close();
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
                if (this.Manual)
                {
                    this.m_enAct = action.Update;
                }
            }
        }
        private void AddAutoCompleteDiaChi()
        {
            txtDiachi.dtData = globalVariables.dtAutocompleteAddress.Copy();
            this.txtDiachi.AutoCompleteList = globalVariables.LstAutocompleteAddressSource;
            this.txtDiachi.CaseSensitive = false;
            this.txtDiachi.MinTypedCharacters = 1;
        }
        void txtNgheNghiep__OnShowData()
        {
            DMUC_DCHUNG dmucDchung = new DMUC_DCHUNG(txtNgheNghiep.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txtNgheNghiep.myCode;
                txtNgheNghiep.Init();
                txtNgheNghiep.SetCode(oldCode);
                txtNgheNghiep.Focus();
            }
        }
        void txtChiDanThem__OnShowData()
        {
            DMUC_DCHUNG dmucDchung = new DMUC_DCHUNG(txtChiDanThem.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txtChiDanThem.myCode;
                txtChiDanThem.Init();
                txtChiDanThem.SetCode(oldCode);
                txtChiDanThem.Focus();
            }
        }
        void txtdando__OnShowData()
        {
            DMUC_DCHUNG dmucDchung = new DMUC_DCHUNG(txtdando.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txtdando.myCode;
                txtdando.Init();
                txtdando.SetCode(oldCode);
                txtdando.Focus();
            }
        }
        void txtdoanbenh__OnShowData()
        {
            DMUC_DCHUNG dmucDchung = new DMUC_DCHUNG(txtdoanbenh.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txtdoanbenh.myCode;
                txtdoanbenh.Init();
                txtdoanbenh.SetCode(oldCode);
                txtdoanbenh.Focus();
            }
        }
        private  void CreatePatientInfo()
        {

            if (txtMaBN.Text == "-1")
            {
                objBenhnhan = new KcbDanhsachBenhnhan();
                objBenhnhan.IdBenhnhan = Utility.Int64Dbnull(txtMaBN.Text, -1);
            }
            else
            {
                objBenhnhan.IdBenhnhan = Utility.Int64Dbnull(txtMaBN.Text, -1);
            }
            objBenhnhan.TenBenhnhan = txtTEN_BN.Text;
            objBenhnhan.KieuBenhnhan = 1;
            objBenhnhan.DiaChi = txtDiachi.Text;
            objBenhnhan.DiachiBhyt = objBenhnhan.DiaChi;
            objBenhnhan.DienThoai = txtPhone.Text;
           // objBenhnhan.Email = Utility.sDbnull(txtEmail.Text, "");
            //objBenhnhan.Locked = 0;
            objBenhnhan.NgayTao = globalVariables.SysDate;
            objBenhnhan.NguoiTao = globalVariables.UserName;
            objBenhnhan.NguonGoc = "QUAYTHUOC";
            //objBenhnhan.Cmt = Utility.sDbnull(txtCMT.Text, "");
            objBenhnhan.CoQuan = string.Empty;
            objBenhnhan.NgheNghiep = txtNgheNghiep.Text;
            objBenhnhan.GioiTinh = cboPatientSex.Text;
            objBenhnhan.IdGioitinh = Utility.ByteDbnull(cboPatientSex.SelectedValue, 0);
            objBenhnhan.NamSinh = Utility.Int16Dbnull(txtNamsinh.Text, null);
            objBenhnhan.NgaySinh = new DateTime(Utility.Int16Dbnull(txtNamsinh.Text, DateTime.Now.Year), 1, 1);
            objBenhnhan.NgheNghiep = Utility.sDbnull(txtNgheNghiep.myCode, "");
            if (txtMaBN.Text == "-1")
            {
                objBenhnhan.IsNew = true;
                objBenhnhan.NgayTiepdon = dtpCreatedDate.Value;
                objBenhnhan.NguoiTao = globalVariables.UserName;
            }
            else
            {
                objBenhnhan.IsNew = false;
                objBenhnhan.MarkOld();
                objBenhnhan.NgaySua = globalVariables.SysDate;
                objBenhnhan.NguoiSua = globalVariables.UserName;
                objBenhnhan.NgayTiepdon = dtpCreatedDate.Value;
            }
        }

         private bool Invalidata()
         {
             if (string.IsNullOrEmpty(txtTEN_BN.Text))
             {
                 Utility.ShowMsg("Tên khách hàng không được bỏ trống");
                 txtTEN_BN.Focus();
                 txtTEN_BN.SelectAll();
                 return false;
             }
             return true;
         }
       
       public KcbDanhsachBenhnhan objBenhnhan = null;
        private void InsertPres(KcbDonthuocChitiet[] arrPresDetail)
        {
            try
            {

                Dictionary<long, long> lstChitietDonthuoc = new Dictionary<long, long>();
                if (arrPresDetail != null && arrPresDetail.Count()>0)
                {
                    if (!Invalidata()) return;
                    CreatePatientInfo();
                    this._actionResult = this._KEDONTHUOC.ThemDonThuoc(this.objBenhnhan, this.CreateNewPres(), arrPresDetail, ref this.IdDonthuoc, ref lstChitietDonthuoc);
                    switch (this._actionResult)
                    {
                        case ActionResult.Error:
                            this.setMsg(this.lblMsg, "Lỗi trong quá trình lưu đơn thuốc", true);
                            break;
                        case ActionResult.Success:
                            this.UpdateChiDanThem();
                            DataRow dr = m_dtPatients.NewRow();
                            KcbDanhsachBenhnhanCollection lstBN = new KcbDanhsachBenhnhanCollection();
                            lstBN.Add(objBenhnhan);
                            DataTable _temp = new DataTable();
                            _temp = lstBN.ToDataTable();
                            Utility.CopyData(_temp.Rows[0], ref dr);
                            dr[KcbDonthuoc.Columns.IdDonthuoc] = IdDonthuoc;
                            m_dtPatients.Rows.Add(dr);

                            this.txtPres_ID.Text = this.IdDonthuoc.ToString();
                            this.m_enAct = action.Update;
                            this.setMsg(this.lblMsg, "Bạn thực hiện lưu đơn thuốc thành công", false);
                            this.UpdateDetailID(lstChitietDonthuoc);
                            this.m_blnCancel = false;
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
                if (this.Manual)
                {
                    this.m_enAct = action.Update;
                }
            }
        }

        private bool InValiMaBenh(string mabenh)
        {
            return globalVariables.gv_dtDmucBenh.AsEnumerable().Where<DataRow>(benh => (Utility.sDbnull(benh[DmucBenh.Columns.MaBenh]) == Utility.sDbnull(mabenh))).Any<DataRow>();
        }

        private bool isChanged(string value)
        {
            string[] strArray = value.Split(new char[] { '-' });
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
                Utility.ShowMsg("Có lỗi trong quá trình lấy danh s\x00e1ch ICD");
            }
        }

       

        private void LoadLaserPrinters()
        {
            if (!string.IsNullOrEmpty(PropertyLib._MayInProperties.TenMayInBienlai))
            {
                PropertyLib._MayInProperties.TenMayInBienlai = Utility.GetDefaultPrinter();
            }
            if (PropertyLib._ThamKhamProperties != null)
            {
                try
                {
                    this.cboLaserPrinters.Items.Clear();
                    for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
                    {
                        string item = PrinterSettings.InstalledPrinters[i];
                        this.cboLaserPrinters.Items.Add(item);
                    }
                }
                catch
                {
                }
                finally
                {
                    this.cboLaserPrinters.Text = PropertyLib._MayInProperties.TenMayInBienlai;
                }
            }
        }

        private void mnuDelele_Click(object sender, EventArgs e)
        {
            try
            {
                this.setMsg(this.lblMsg, "", false);
                if ((this.grdPresDetail.RowCount <= 0) || (this.grdPresDetail.CurrentRow.RowType != RowType.Record))
                {
                    this.setMsg(this.lblMsg, "Bạn phải chọn thuốc để xóa", true);
                    this.grdPresDetail.Focus();
                }
                else
                {
                    int num = Utility.Int32Dbnull(this.grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, -1);
                    string s = "";
                    List<int> vals = this.GetIdChitiet(Utility.Int32Dbnull(this.grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, -1), Utility.DecimaltoDbnull(this.grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.DonGia].Value, -1), ref s);
                    if (new SubSonic.Select().From(KcbDonthuocChitiet.Schema).Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).In(vals).And(KcbDonthuocChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1).GetRecordCount() > 0)
                    {
                        this.setMsg(this.lblMsg, "Bản ghi đã thanh toán, bạn không thể xóa", true);
                        this.grdPresDetail.Focus();
                    }
                    else if (!PropertyLib._ThamKhamProperties.Hoitruockhixoathuoc || Utility.AcceptQuestion("Bạn Có muốn xóa các thuốc đang chọn hay không?", "thông báo xóa", true))
                    {
                        this._KEDONTHUOC.XoaChitietDonthuoc(s);
                        this.grdPresDetail.CurrentRow.Delete();
                        this.grdPresDetail.UpdateData();
                        this.deletefromDatatable(vals);
                        this.m_blnCancel = true;
                        this.UpdateDataWhenChanged();
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
                if (this.grdPresDetail.RowCount <= 0)
                {
                    this.m_enAct = action.Insert;
                }
            }
        }

       

        private void ModifyButton()
        {
            try
            {
                this.cmdSavePres.Enabled = this.grdPresDetail.RowCount > 0;
                this.cmdPrintPres.Enabled = this.grdPresDetail.RowCount > 0;
                this.cmdDelete.Enabled = this.grdPresDetail.RowCount > 0;
                this.cmdAddDetail.Enabled = Utility.Int32Dbnull(this.txtDrugID.Text) > 0;
            }
            catch (Exception)
            {
            }
        }

        private void PerformAction()
        {
            switch (this.m_enAct)
            {
                case action.Insert:
                    this.InsertPres();
                    break;

                case action.Update:
                    this.UpdatePres();
                    break;
            }
        }

        private void PerformAction(KcbDonthuocChitiet[] arrPresDetail)
        {
            this.isSaved = true;
            switch (this.m_enAct)
            {
                case action.Insert:
                    this.InsertPres(arrPresDetail);
                    break;

                case action.Update:
                    this.UpdatePres(arrPresDetail);
                    break;
            }
        }

        private void PrintPres(int PresID)
        {
            try
            {
                DataTable dataTable = this._KEDONTHUOC.LaythongtinDonthuoc_In(PresID);
                if (dataTable.Rows.Count <= 0)
                {
                    Utility.ShowMsg("không tìm  thấy thuốc, Có thể bạn chưa lưu được thuốc, \nMời bạn kiểm tra lại", "thông báo", MessageBoxIcon.Exclamation);
                    return;
                }
                Utility.AddColumToDataTable(ref dataTable, "BarCode", typeof(byte[]));
                THU_VIEN_CHUNG.CreateXML(dataTable, "thamkham_InDonthuocA4.xml");
                this.barcode.Data = Utility.sDbnull(this.txtPres_ID.Text);
                byte[] buffer = Utility.GenerateBarCode(this.barcode);
                string str = "";
                string str2 = "";
                if ((dataTable != null) && (dataTable.Rows.Count > 0))
                {
                }
                foreach (DataRow row in dataTable.Rows)
                {
                    row["BarCode"] = buffer;
                    row["chan_doan"] = (Utility.sDbnull(row["chan_doan"]).Trim() == "") ? str : (Utility.sDbnull(row["chan_doan"]) + ";" + str);
                    row["ma_icd"] = str2;
                }
                dataTable.AcceptChanges();
                Utility.UpdateLogotoDatatable(ref dataTable);
                string str3 = "A5";
                if (PropertyLib._MayInProperties.CoGiayInDonthuoc == Papersize.A4)
                {
                    str3 = "A4";
                }
                ReportDocument document = new ReportDocument();
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
                    frmPrintPreview preview = new frmPrintPreview("IN ĐƠN THUỐC BỆNH NH\x00c2N", rptDoc, true, true);
                    try
                    {
                        rptDoc.SetDataSource(dataTable);
                        Utility.SetParameterValue(rptDoc, "ParentBranchName", globalVariables.ParentBranch_Name);
                        Utility.SetParameterValue(rptDoc, "BranchName", globalVariables.Branch_Name);
                        Utility.SetParameterValue(rptDoc, "Address", globalVariables.Branch_Address);
                        Utility.SetParameterValue(rptDoc, "Phone", globalVariables.Branch_Phone);
                        Utility.SetParameterValue(rptDoc, "ReportTitle", "ĐƠN THUỐC");
                        Utility.SetParameterValue(rptDoc, "CurrentDate", Utility.FormatDateTime(this.dtNgayIn.Value));
                        Utility.SetParameterValue(rptDoc, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                        preview.crptViewer.ReportSource = rptDoc;
                        if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai, PropertyLib._MayInProperties.PreviewInDonthuoc))
                        {
                            preview.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
                            preview.ShowDialog();
                            this.cboLaserPrinters.Text = PropertyLib._MayInProperties.TenMayInBienlai;
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
            catch(Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void ProcessData()
        {
        }

        private void SaveDefaultPrinter()
        {
            try
            {
                PropertyLib._MayInProperties.TenMayInBienlai = Utility.sDbnull(this.cboLaserPrinters.Text);
                PropertyLib.SaveProperty(PropertyLib._MayInProperties);
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi khi lưu trạng th\x00e1i-->" + exception.Message);
            }
        }

        private void SaveMe()
        {
            try
            {
                this.cmdSavePres.Enabled = false;
                if (this.grdPresDetail.RowCount <= 0)
                {
                    this.setMsg(this.lblMsg, "Hiện chưa Có bản ghi nào để thực hiện  lưu lại", true);
                    this.txtdrug.Focus();
                    this.txtdrug.SelectAll();
                }
                else
                {
                    this.isSaved = true;
                    switch (this.em_CallAction)
                    {
                        case CallAction.FromMenu:
                            if (this.hasChanged)
                            {
                                this.PerformAction();
                            }
                            if (!((this._temp == ActionResult.NotEnoughDrugInStock) || this.Manual))
                            {
                                base.Close();
                            }
                            return;

                        case CallAction.FromParentFormList:
                            this.m_blnCancel = true;
                            if (!this.Manual)
                            {
                                base.Close();
                            }
                            return;

                        case CallAction.FromAnotherForm:
                            this.m_blnCancel = true;
                            if (this.hasChanged)
                            {
                                this.PerformAction();
                            }
                            if (!((this._temp == ActionResult.NotEnoughDrugInStock) || this.Manual))
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
                this.cmdSavePres.Enabled = true;
                this.Manual = false;
                this.hasChanged = false;
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

     
        private void txtCachDung__OnSelectionChanged()
        {
            this.ChiDanThuoc();
        }

        private void txtCachDung__OnShowData()
        {
        }

        private void txtCachDung_TextChanged(object sender, EventArgs e)
        {
            this.ChiDanThuoc();
        }

        private void txtdrug__OnChangedView(bool gridview)
        {
            PropertyLib._AppProperties.GridView = gridview;
            PropertyLib.SaveProperty(PropertyLib._AppProperties);
        }
        int id_thuockho = -1;
        private void txtdrug__OnGridSelectionChanged(string ID, int id_thuockho, string _name, string Dongia, string phuthu, int tutuc)
        {
            this.id_thuockho = id_thuockho;
            AllowDrugChanged = false;
            txtDrugID.Text = ID;
            txtIdThuocKho.Text = Utility.sDbnull(id_thuockho);
            txtPrice.Text = Dongia;
        }
         
        private void txtDrugID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!AllowDrugChanged) return;
                this.m_decPrice = 0M;
                this.m_Surcharge = 0M;
                this.AllowTextChanged = false;
               
                if ((Utility.DoTrim(this.txtDrugID.Text) == "") || (Utility.Int32Dbnull(this.txtDrugID.Text, -1) < 0))
                {
                    this.m_decPrice = 0M;
                    this.tu_tuc = 0;
                    this.txtDrugID.Text = "";
                    this.txtDrug_Name.Text = "";
                    this.txtBietduoc.Clear();
                    this.txtDonViDung.Clear();
                   // this.txtSurcharge.Text = "0";
                    this.txtPrice.Text = "0";
                    this.txtdrugtypeCode.Clear();
                    this.cmdAddDetail.Enabled = false;
                    return;
                }
                DataRow[] rowArray = this.m_dtDanhmucthuoc.Select(DmucThuoc.Columns.IdThuoc + "=" + this.txtDrugID.Text);
                if (rowArray.Length > 0)
                {
                    txtTonKho.Text =
                       CommonLoadDuoc.SoLuongTonTrongKho(-1L, Utility.Int32Dbnull(cboStock.SelectedValue),
                           Utility.Int32Dbnull(txtDrugID.Text, -1),
                           txtdrug.GridView
                               ? id_thuockho
                               : txtdrug.id_thuockho,
                           Utility.Int32Dbnull(
                               THU_VIEN_CHUNG.Laygiatrithamsohethong(
                                   "KIEMTRATHUOC_CHOXACNHAN", "1", false), 1),
                           0).ToString();
                    this.txtDonViDung.Text = rowArray[0]["ten_donvitinh"].ToString();
                    this.txtDrug_Name.Text = rowArray[0][DmucThuoc.Columns.TenThuoc].ToString();
                    this.txtBietduoc.Text = rowArray[0][DmucThuoc.Columns.HoatChat].ToString();
                    this.txtPrice.Text = rowArray[0]["GIA_BAN"].ToString();
                    this.tu_tuc = Utility.Int32Dbnull(rowArray[0]["tu_tuc"], 0);
                    //this.txtSurcharge.Text = rowArray[0]["PHU_THU"].ToString();
                    this.txtdrugtypeCode.Text = rowArray[0][DmucLoaithuoc.Columns.MaLoaithuoc].ToString();
                    this.cmdAddDetail.Enabled = true;
                }
                else
                {
                    this.m_decPrice = 0M;
                    this.tu_tuc = 0;
                    this.txtDrugID.Text = "";
                    this.txtDrug_Name.Text = "";
                    this.txtDonViDung.Clear();
                    //this.txtSurcharge.Text = "0";
                    this.txtPrice.Text = "0";
                    this.cmdAddDetail.Enabled = false;
                }
                this.m_blnGetDrugCodeFromList = false;
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
                this.chkTutuc.Checked = this.tu_tuc == 1;
                this.AllowTextChanged = true;
            }
            this.ModifyButton();
        }

        private void txtMaBenhChinh_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && this.hasMorethanOne)
            {
                this.DSACH_ICD(this.txtMaBenhChinh, DmucChung.Columns.Ma, 0);
                this.hasMorethanOne = false;
            }
        }

        private void txtMaBenhChinh_TextChanged(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    DataRow[] rowArray;
                    this.hasMorethanOne = true;
                    if (this.isLike)
                    {
                        rowArray = globalVariables.gv_dtDmucBenh.Select(DmucBenh.Columns.MaBenh + " like '" + Utility.sDbnull(this.txtMaBenhChinh.Text, "") + "%'");
                    }
                    else
                    {
                        rowArray = globalVariables.gv_dtDmucBenh.Select(DmucBenh.Columns.MaBenh + " = '" + Utility.sDbnull(this.txtMaBenhChinh.Text, "") + "'");
                    }
                    if (!string.IsNullOrEmpty(this.txtMaBenhChinh.Text))
                    {
                        if (rowArray.GetLength(0) == 1)
                        {
                            this.hasMorethanOne = false;
                            this.txtMaBenhChinh.Text = rowArray[0][DmucBenh.Columns.MaBenh].ToString();
                            this.txtTenBenhChinh.Text = Utility.sDbnull(rowArray[0][DmucBenh.Columns.TenBenh], "");
                        }
                        else
                        {
                            this.txtTenBenhChinh.Text = "";
                        }
                    }
                    else
                    {
                        this.txtTenBenhChinh.Text = "";
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
            this.txtMaBenhphu_TextChanged(this.txtMaBenhphu, e);
        }

        private void txtMaBenhphu_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (this.hasMorethanOne)
                    {
                        this.DSACH_ICD(this.txtMaBenhphu, DmucChung.Columns.Ma, 1);
                        this.txtMaBenhphu.SelectAll();
                    }
                    else
                    {
                        this.AddBenhphu();
                        this.txtMaBenhphu.SelectAll();
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
            this.hasMorethanOne = true;
            if (this.isLike)
            {
                rowArray = globalVariables.gv_dtDmucBenh.Select(DmucBenh.Columns.MaBenh + " like '" + Utility.sDbnull(this.txtMaBenhphu.Text, "") + "%'");
            }
            else
            {
                rowArray = globalVariables.gv_dtDmucBenh.Select(DmucBenh.Columns.MaBenh + " = '" + Utility.sDbnull(this.txtMaBenhphu.Text, "") + "'");
            }
            if (!string.IsNullOrEmpty(this.txtMaBenhphu.Text))
            {
                if (rowArray.GetLength(0) == 1)
                {
                    this.hasMorethanOne = false;
                    this.txtMaBenhphu.Text = rowArray[0][DmucBenh.Columns.MaBenh].ToString();
                    this.txtTenBenhPhu.Text = Utility.sDbnull(rowArray[0][DmucBenh.Columns.TenBenh], "");
                    this.TEN_BENHPHU = this.txtTenBenhPhu.Text;
                }
                else
                {
                    this.txtTenBenhPhu.Text = "";
                    this.TEN_BENHPHU = "";
                }
            }
            else
            {
                this.txtMaBenhphu.Text = "";
                this.TEN_BENHPHU = "";
            }
        }

        private void txtPres_ID_TextChanged(object sender, EventArgs e)
        {
            this.barcode.Visible = Utility.Int32Dbnull(this.txtPres_ID.Text) > 0;
            this.barcode.Data = Utility.sDbnull(this.txtPres_ID.Text);
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            Utility.FormatCurrencyHIS(this.txtPrice);
            txtThanhtien.Text = Utility.sDbnull(Utility.Int16Dbnull(txtSoluong.Text, 0) *
                                     Utility.DecimaltoDbnull(txtPrice.Text, 0));
        }

        private void txtSoluong_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Utility.DecimaltoDbnull(this.txtSoluong.Text) > 0) && (e.KeyCode == Keys.Enter))
            {
                if (!this._Found)
                {
                    if (globalVariables.gv_intChophepChinhgiathuocKhiKedon == 0)
                    {
                        this.txtSoLuongDung.Focus();
                        this.txtSoLuongDung.SelectAll();
                    }
                    else
                    {
                        this.txtPrice.Focus();
                        this.txtPrice.SelectAll();
                    }
                }
                else
                {
                    this.cmdAddDetail_Click(this.cmdAddDetail, new EventArgs());
                }
            }
        }

        private void txtSoluong_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Utility.ResetMessageError(this.errorProvider1);
                if (CommonLoadDuoc.IsKiemTraTonKho(Utility.Int32Dbnull(this.cboStock.SelectedValue, 0)) && (Utility.DecimaltoDbnull(this.txtSoluong.Text, 0) > Utility.Int32Dbnull(this.txtTonKho.Text, 0)))
                {
                    Utility.SetMsgError(this.errorProvider1, this.txtSoluong, "Số lượng thuốc cấp phát vượt quá số lượng thuốc trong kho. Mời bạn kiểm tra lại");
                    this.txtSoluong.Focus();
                }
                else
                {
                    txtThanhtien.Text = Utility.sDbnull(Utility.Int16Dbnull(txtSoluong.Text, 0) *
                                        Utility.DecimaltoDbnull(txtPrice.Text, 0));
                }
            }
            catch (Exception)
            {
            }
        }

        private void txtSolan_TextChanged(object sender, EventArgs e)
        {
            this.ChiDanThuoc();
        }

        private void txtSoLuongDung_TextChanged(object sender, EventArgs e)
        {
            this.ChiDanThuoc();
        }

        private void txtSurcharge_TextChanged(object sender, EventArgs e)
        {
            //Utility.FormatCurrencyHIS(this.txtSurcharge);
        }

        private void txtTenBenhChinh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!this.InValiMaBenh(this.txtMaBenhChinh.Text))
                {
                    this.DSACH_ICD(this.txtTenBenhChinh, DmucChung.Columns.Ten, 0);
                    this.txtMaBenhphu.Focus();
                }
                else
                {
                    this.txtMaBenhphu.Focus();
                }
            }
        }

        private void txtTenBenhChinh_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txtMaBenhChinh.TextLength <= 0)
                {
                    this.txtMaBenhChinh.ResetText();
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
                if (this.hasMorethanOne)
                {
                    this.DSACH_ICD(this.txtTenBenhPhu, DmucChung.Columns.Ten, 1);
                    this.txtTenBenhPhu.Focus();
                }
                else
                {
                    this.txtTenBenhPhu.Focus();
                }
            }
        }

        private void txtTenBenhPhu_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txtTenBenhPhu.TextLength <= 0)
                {
                    this.Selected = false;
                    this.txtMaBenhphu.ResetText();
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình xử lý sự kiện");
            }
        }

        private void UpdateChiDanThem()
        {
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
        }

        private void UpdateDetailID(Dictionary<long, long> lstPresDetail)
        {
            if (lstPresDetail.Count > 0)
            {
                foreach (DataRow row in this.m_dtDonthuocChitiet.Rows)
                {
                    if (lstPresDetail.ContainsKey(Utility.Int64Dbnull(row[KcbDonthuocChitiet.Columns.IdThuockho])))
                    {
                        row[KcbDonthuocChitiet.Columns.IdChitietdonthuoc] = lstPresDetail[Utility.Int64Dbnull(row[KcbDonthuocChitiet.Columns.IdThuockho])];
                    }
                }
                this.m_dtDonthuocChitiet.AcceptChanges();

                CreateViewTable();

            }
        }

        private void UpdatePres()
        {
            Dictionary<long, long> lstChitietDonthuoc = new Dictionary<long, long>();
            KcbDonthuocChitiet[] arrDonthuocChitiet = this.CreateArrayPresDetail();
            if (arrDonthuocChitiet != null)
            {
                 CreatePatientInfo();
                this._actionResult = this._KEDONTHUOC.CapnhatDonthuoc(this.objBenhnhan, this.CreateNewPres(), arrDonthuocChitiet, ref this.IdDonthuoc, ref lstChitietDonthuoc);
                switch (this._actionResult)
                {
                    case ActionResult.Error:
                        this.setMsg(this.lblMsg, "Lỗi trong quá trình lưu đơn thuốc", true);
                        break;

                    case ActionResult.Success:
                        this.UpdateChiDanThem();
                        DataRow[] arrDr = m_dtPatients.Select(KcbDanhsachBenhnhan.Columns.IdBenhnhan+ "="+objBenhnhan.IdBenhnhan.ToString());
                        if (arrDr!=null && arrDr.Length > 0)
                        {
                            KcbDanhsachBenhnhanCollection lstBN = new KcbDanhsachBenhnhanCollection();
                            lstBN.Add(objBenhnhan);
                            DataTable _temp = new DataTable();
                            _temp = lstBN.ToDataTable();
                            Utility.CopyData(_temp.Rows[0], ref arrDr[0]);
                            m_dtPatients.AcceptChanges();
                        }
                        this.setMsg(this.lblMsg, "Bạn thực hiện lưu đơn thuốc thành công", false);
                        this.UpdateDetailID(lstChitietDonthuoc);
                        this.m_blnCancel = false;
                            //this.Close();
                        break;
                }
            }
        }

        private void UpdatePres(KcbDonthuocChitiet[] arrPresDetail)
        {
            Dictionary<long, long> lstChitietDonthuoc = new Dictionary<long, long>();
            if (arrPresDetail != null)
            {
                
                this._actionResult = this._KEDONTHUOC.CapnhatDonthuoc(this.objBenhnhan, this.CreateNewPres(), arrPresDetail, ref this.IdDonthuoc, ref lstChitietDonthuoc);
                switch (this._actionResult)
                {
                    case ActionResult.Error:
                        this.setMsg(this.lblMsg, "Lỗi trong quá trình lưu đơn thuốc", true);
                        break;

                    case ActionResult.Success:
                        this.UpdateChiDanThem();
                         DataRow[] arrDr = m_dtPatients.Select(KcbDanhsachBenhnhan.Columns.IdBenhnhan+ "="+objBenhnhan.IdBenhnhan.ToString());
                        if (arrDr!=null && arrDr.Length > 0)
                        {
                            KcbDanhsachBenhnhanCollection lstBN = new KcbDanhsachBenhnhanCollection();
                            lstBN.Add(objBenhnhan);
                            DataTable _temp = new DataTable();
                            _temp = lstBN.ToDataTable();
                            Utility.CopyData(_temp.Rows[0], ref arrDr[0]);
                            m_dtPatients.AcceptChanges();
                        }
                        this.setMsg(this.lblMsg, "Bạn thực hiện lưu đơn thuốc thành công", false);
                        this.UpdateDetailID(lstChitietDonthuoc);
                        this.m_blnCancel = false;
                           // this.Close();
                        break;
                }
            }
        }

      

      

        private int ID_Goi_Dvu { get; set; }

       

        public byte Noi_tru { get; set; }

       


        public int PreType { get; set; }

        public int TrongGoi { get; set; }

        private void txtTuoi_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTuoi.Text))
            {
                txtNamsinh.Text = Utility.sDbnull(DateTime.Now.Year - Utility.Int16Dbnull(txtTuoi.Text, 0));
            }
        }

        private void txtNamsinh_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNamsinh.Text) && txtNamsinh.TextLength >3)
            {
                txtTuoi.Text = Utility.sDbnull(DateTime.Now.Year - Utility.Int16Dbnull(txtNamsinh.Text, 0));
            }
        }

        private void txtNamsinh_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNamsinh.Text) && txtNamsinh.TextLength > 3)
            {
                txtTuoi.Text = Utility.sDbnull(DateTime.Now.Year - Utility.Int16Dbnull(txtNamsinh.Text, 0));
            }
        }

        private void txtTuoi_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTuoi.Text))
            {
                txtNamsinh.Text = Utility.sDbnull(DateTime.Now.Year - Utility.Int16Dbnull(txtTuoi.Text, 0));
            }
        }

         private void chktaikham_CheckedChanged(object sender, EventArgs e)
         {
             if (chktaikham.Checked)
             {
                 dtpNgaytaikham.Enabled = true;
                 dtpNgaytaikham.Value = DateTime.Now;
             }
             else
             {
                 dtpNgaytaikham.Enabled = false;

             }
         }

         private void txtTEN_BN_Leave(object sender, EventArgs e)
        {
            string[] result = txtTEN_BN.Text.Split(',');
            if (Utility.Int64Dbnull(result[0]) > 0)
            {
                 objBenhnhan =
                    new Select().From(KcbDanhsachBenhnhan.Schema)
                        .Where(KcbDanhsachBenhnhan.Columns.IdBenhnhan)
                        .IsEqualTo(Utility.Int64Dbnull(result[0])).ExecuteSingle<KcbDanhsachBenhnhan>();
                 if (objBenhnhan != null)
                {
                    txtMaBN.Text = Utility.sDbnull(objBenhnhan.IdBenhnhan, "-1");
                    txtTEN_BN.Text = Utility.sDbnull(objBenhnhan.TenBenhnhan, "");
                    cboPatientSex.SelectedValue = Utility.Int16Dbnull(objBenhnhan.IdGioitinh, 0);
                    txtTuoi.Text = Utility.sDbnull(DateTime.Now.Year - objBenhnhan.NamSinh);
                    txtDiachi.Text = Utility.sDbnull(objBenhnhan.DiaChi);
                    txtSoDT.Text = Utility.sDbnull(objBenhnhan.DienThoai);
                    txtNgheNghiep.Text = Utility.sDbnull(objBenhnhan.NgheNghiep);
                    txtdrug.Focus();
                    
                    return;
                }

            }

            txtTEN_BN.Text = Utility.CapitalizeWords(txtTEN_BN.Text.Trim());
            
        }
        private void txtIdThuocKho_TextChanged(object sender, EventArgs e)
        {
          //  txtDrugID_TextChanged(e, new EventArgs());
        }
     }
}

