﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using NLog;
using SubSonic;
using VNS.HIS.BusRule.Classes;
using VNS.HIS.DAL;
using VNS.HIS.UCs;
using VNS.HIS.UI.Forms.Cauhinh;
using VNS.HIS.UI.Forms.NGOAITRU;
using VNS.HIS.UI.NGOAITRU;
using VNS.Libs;
using VNS.Libs.AppUI;
using VNS.Properties;
using VNS.UCs;
using VNS.UI.QMS;

namespace VNS.HIS.UI.NOITRU
{
    /// <summary>
    ///     06/11/2013 3h57
    /// </summary>
    public partial class frm_TonghopRavien : Form
    {
        private readonly string FileName = string.Format("{0}/{1}", Application.StartupPath,
            "SplitterDistanceThamKham.txt");

        private readonly bool _khoanoitrutonghop;
            //true= Từng khoa nội trú tổng hợp và chốt dữ liệu;False= Phòng tổng hợp tự chốt và bổ sung dữ liệu cho tất cả các khoa

        private readonly MoneyByLetter MoneyByLetter = new MoneyByLetter();
        private readonly KCB_CHIDINH_CANLAMSANG _KCB_CHIDINH_CANLAMSANG = new KCB_CHIDINH_CANLAMSANG();
        private readonly KCB_THAMKHAM _KCB_THAMKHAM = new KCB_THAMKHAM();

        private readonly Logger log;

        private readonly List<string> lstKQCochitietColumns = new List<string>
        {
            "ten_chitietdichvu",
            "Ket_qua",
            "bt_nam",
            "bt_nu"
        };

        private readonly List<string> lstKQKhongchitietColumns = new List<string> {"Ket_qua", "bt_nam", "bt_nu"};
        private readonly DataTable m_dtVTTH_tronggoi = new DataTable();

        private readonly AutoCompleteStringCollection _namesCollection = new AutoCompleteStringCollection();
        private readonly AutoCompleteStringCollection _namesCollectionBenhChinh = new AutoCompleteStringCollection();
        private readonly AutoCompleteStringCollection _namesCollectionBenhPhu = new AutoCompleteStringCollection();
        private readonly AutoCompleteStringCollection _namesCollectionKetLuan = new AutoCompleteStringCollection();
        private readonly AutoCompleteStringCollection _namesCollectionMaLanKham = new AutoCompleteStringCollection();

        private readonly string _strSaveandprintPath = Application.StartupPath +
                                                      @"\CAUHINH\DefaultPrinter_PhieuHoaSinh.txt";

        private bool _allowTextChanged;
        private int Distance = 488;

        private bool Selected;
        private string TEN_BENHPHU = "";
        private int _CurIdPhieudieutri = -1;
        private DMUC_CHUNG _dmucChung = new DMUC_CHUNG();
        private KCB_KEDONTHUOC _kcbKedonthuoc = new KCB_KEDONTHUOC();
        private KcbChandoanKetluan _KcbChandoanKetluan = null;
        private bool _buttonClick;
        private string _rowFilter = "1=1";
        private bool b_Hasloaded;
        private DataSet ds = new DataSet();
        private DataTable _dtIcd = new DataTable();
        private bool hasMorethanOne = true;

        private bool isLike = true;
        private bool isNhapVien = false;

        private List<string> _lstResultColumns = new List<string>
        {
            "ten_chitietdichvu",
            "ketqua_cls",
            "binhthuong_nam",
            "binhthuong_nu"
        };

        private List<string> lstVisibleColumns = new List<string>();
        private DataTable m_ExamTypeRelationList = new DataTable();
        private bool m_blnHasLoaded;
        public string ma_luotkham = "";
        public bool bCancel = false;
        private DataTable m_dtAssignDetail;
        private DataTable m_dtBuongGiuong;
        private DataTable m_dtChandoanKCB = new DataTable();
        private DataTable m_dtChedoDinhduong = new DataTable();
        private DataTable m_dtChiphithem;
        private DataTable m_dtDataVTYT = new DataTable();
        private DataTable m_dtDoctorAssign;

        private DataTable m_dtDonthuoc = new DataTable();
        private DataTable m_dtDonthuocChitiet_View = new DataTable();
        private DataTable m_dtGoidichvu;
        private DataTable m_dtPatients = new DataTable();
        private DataTable m_dtPhieudieutri;

        private DataTable m_dtReport = new DataTable();
        private DataTable m_dtTamung;
        private DataTable m_dtVTTH = new DataTable();
        private DataTable m_dtVTTHChitiet_View = new DataTable();
        private action m_enActChandoan = action.FirstOrFinished;
        private DataTable m_hdt = new DataTable();
        private DataTable m_kl;
        private string m_strDefaultLazerPrinterName = "";

        /// <summary>
        ///     hàm thực hiện việc chọn bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private string malankham = "";

        public KcbDanhsachBenhnhan objBenhnhan = null;
        public KcbLuotkham objLuotkham = null;
        private NoitruPhieudieutri objPhieudieutri;

        private frm_DanhSachKham objSoKham;
        private GridEXRow row_Selected;
        private bool trieuchung;
        private bool viewAll;

        public frm_TonghopRavien(string khoanoitrutonghop)
        {
            InitializeComponent();
            _khoanoitrutonghop = khoanoitrutonghop == "1";
            //cboKhoanoitru.Visible = _khoanoitrutonghop;
            //lblKhoatonghop.Visible = _khoanoitrutonghop;

            KeyPreview = true;
            log = LogManager.GetCurrentClassLogger();
            dtInput_Date.Value = globalVariables.SysDate;
            Cauhinh();
            InitEvents();
        }

        private void InitEvents()
        {
            FormClosing += frm_LAOKHOA_Add_TIEPDON_BN_FormClosing;
            Load += frm_TonghopRavien_Load;
            KeyDown += frm_TonghopRavien_KeyDown;

            txtPatient_Code.KeyDown += txtPatient_Code_KeyDown;

            grdGoidichvu.SelectionChanged += grdGoidichvu_SelectionChanged;
            grdGoidichvu.DoubleClick += grdGoidichvu_DoubleClick;


            grdAssignDetail.CellUpdated += grdAssignDetail_CellUpdated;
            grdAssignDetail.SelectionChanged += grdAssignDetail_SelectionChanged;


            grdPhieudieutri.ColumnButtonClick += grdPhieudieutri_ColumnButtonClick;
            grdPhieudieutri.SelectionChanged += grdPhieudieutri_SelectionChanged;


            cmdThemgoiDV.Click += cmdThemgoiDV_Click;
            cmdSuagoiDV.Click += cmdSuagoiDV_Click;
            cmdXoagoiDV.Click += cmdXoagoiDV_Click;


            grdPresDetail.SelectionChanged += grdPresDetail_SelectionChanged;
            grdVTTH.SelectionChanged += grdVTTH_SelectionChanged;
            grdChiphithem.SelectionChanged += grdChiphithem_SelectionChanged;
            cmdChuyengoi.Click += cmdChuyengoi_Click;

            grdBuongGiuong.UpdatingCell += grdBuongGiuong_UpdatingCell;
            lnkSize.Click += lnkSize_Click;

            cmdXacnhan.Click += cmdXacnhan_Click;
            cmdTonghop.Click += cmdTonghop_Click;
            cmdInphoiBHYT.Click += cmdInphoiBHYT_Click;
            cmdRavien.Click += cmdRavien_Click;
            cmdThemchiphithem.Click += cmdThemchiphithem_Click;
            cmdSuachiphithem.Click += cmdSuachiphithem_Click;
            cmdXoachiphithem.Click += cmdXoachiphithem_Click;
            lnkViewAll.Click += lnkViewAll_Click;
            mnuTronggoi.Click += mnuTronggoi_Click;
            mnuTutuc.Click += mnuTutuc_Click;
            mnuCancel.Click += mnuCancel_Click;
        }

        private void grdBuongGiuong_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                long id =
                    Utility.Int64Dbnull(Utility.getValueOfGridCell(grdBuongGiuong, NoitruPhanbuonggiuong.Columns.Id));
                if (e.Column.Key == "don_gia")
                {
                    noitru_nhapvien.Capnhatgia(id, Utility.DecimaltoDbnull(e.Value, 0), 1);
                    DataRow[] arrDr = m_dtBuongGiuong.Select(NoitruPhanbuonggiuong.Columns.Id + "=" + id);
                    if (arrDr.Length > 0)
                    {
                        arrDr[0][NoitruPhanbuonggiuong.Columns.DonGia] = e.Value;
                        arrDr[0]["thanh_tien"] =
                            Utility.DecimaltoDbnull(arrDr[0][NoitruPhanbuonggiuong.Columns.DonGia], 0)*
                            Utility.DecimaltoDbnull(arrDr[0][NoitruPhanbuonggiuong.Columns.SoLuong], 0);
                    }
                    m_dtBuongGiuong.AcceptChanges();
                }
                else if (e.Column.Key == "so_luong")
                {
                    noitru_nhapvien.CapnhatSoluong(id, Utility.DecimaltoDbnull(e.Value, 0), 1);
                    DataRow[] arrDr = m_dtBuongGiuong.Select(NoitruPhanbuonggiuong.Columns.Id + "=" + id);
                    if (arrDr.Length > 0)
                    {
                        arrDr[0][NoitruPhanbuonggiuong.Columns.SoLuong] = e.Value;
                        arrDr[0]["thanh_tien"] =
                            Utility.DecimaltoDbnull(arrDr[0][NoitruPhanbuonggiuong.Columns.DonGia], 0)*
                            Utility.DecimaltoDbnull(arrDr[0][NoitruPhanbuonggiuong.Columns.SoLuong], 0);
                    }
                    m_dtBuongGiuong.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }


        //0=Mới chỉ định;1=Đã chuyển CLS;2=Đang thực hiện;3= Đã có kết quả CLS;4=Đã xác nhận kết quả
        private void mnuCancel_Click(object sender, EventArgs e)
        {
            GridEX _grd = grdAssignDetail;
            try
            {
                if (objLuotkham.TrangthaiNoitru == 5)
                {
                    Utility.ShowMsg(
                        "Bệnh nhân đã được duyệt thanh toán nội trú nên bạn không thể sửa lại trạng thái tổng hợp nội trú");
                    return;
                }

                if (Utility.Byte2Bool(objLuotkham.TthaiThanhtoannoitru) || objLuotkham.TrangthaiNoitru == 6)
                {
                    Utility.ShowMsg(
                        "Bệnh nhân đã được thanh toán nội trú nên bạn không thể hủy các dịch vụ của Bệnh nhân");
                    return;
                }

                string ctrlName = ((ContextMenuStrip) ((ToolStripMenuItem) sender).Owner).SourceControl.Name;
                if (mnuCancel.Tag.ToString() == "0") //Hủy
                {
                    if (ctrlName == grdAssignDetail.Name)
                    {
                        long Id =
                            Utility.Int64Dbnull(
                                Utility.GetValueFromGridColumn(grdAssignDetail,
                                    KcbChidinhclsChitiet.Columns.IdChitietchidinh), -1);
                        int trangthai_chuyencls =
                            Utility.Int32Dbnull(
                                Utility.GetValueFromGridColumn(grdAssignDetail, KcbChidinhclsChitiet.Columns.TrangThai,
                                    "0"), 0);
                        int TrangthaiThanhtoan =
                            Utility.Int32Dbnull(
                                Utility.GetValueFromGridColumn(grdAssignDetail,
                                    KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan, "0"), 0);
                        if (trangthai_chuyencls >= 3) //Đã có kết quả
                        {
                            Utility.ShowMsg(
                                "Chỉ định bạn đang chọn đã có kết quả nên không cho phép hủy. Đề nghị bạn kiểm tra lại");
                            return;
                        }
                        if (TrangthaiThanhtoan > 0) //Đã thanh toán
                        {
                            Utility.ShowMsg(
                                "Chỉ định bạn đang chọn đã thanh toán nên không cho phép hủy. Đề nghị bạn kiểm tra lại");
                            return;
                        }
                        new Update(KcbChidinhclsChitiet.Schema).Set(KcbChidinhclsChitiet.Columns.TrangthaiHuy)
                            .EqualTo(1)
                            .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(Id).Execute();
                        grdAssignDetail.CurrentRow.BeginEdit();
                        grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.TrangthaiHuy].Value = 1;
                        grdAssignDetail.CurrentRow.EndEdit();
                        _grd = grdAssignDetail;
                    }
                    else if (ctrlName == grdPresDetail.Name)
                    {
                        long Id =
                            Utility.Int64Dbnull(
                                Utility.GetValueFromGridColumn(grdPresDetail,
                                    KcbDonthuocChitiet.Columns.IdChitietdonthuoc), -1);
                        int TrangThai =
                            Utility.Int32Dbnull(
                                Utility.GetValueFromGridColumn(grdPresDetail, KcbDonthuocChitiet.Columns.TrangThai, "0"),
                                0);
                        int TrangthaiThanhtoan =
                            Utility.Int32Dbnull(
                                Utility.GetValueFromGridColumn(grdPresDetail,
                                    KcbDonthuocChitiet.Columns.TrangthaiThanhtoan, "0"), 0);
                        if (TrangThai >= 1) //Đã xác nhận duyệt thuốc
                        {
                            Utility.ShowMsg(
                                "Thuốc bạn đang chọn đã được cấp phát nên không cho phép hủy. Đề nghị bạn kiểm tra lại");
                            return;
                        }
                        if (TrangthaiThanhtoan > 0) //Đã thanh toán
                        {
                            Utility.ShowMsg(
                                "Thuốc bạn đang chọn đã thanh toán nên không cho phép hủy. Đề nghị bạn kiểm tra lại");
                            return;
                        }
                        new Update(KcbDonthuocChitiet.Schema).Set(KcbDonthuocChitiet.Columns.TrangthaiHuy).EqualTo(1)
                            .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(Id).Execute();
                        grdPresDetail.CurrentRow.BeginEdit();
                        grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.TrangthaiHuy].Value = 1;
                        grdPresDetail.CurrentRow.EndEdit();
                        _grd = grdPresDetail;
                    }
                    else //VTTH
                    {
                        long Id =
                            Utility.Int64Dbnull(
                                Utility.GetValueFromGridColumn(grdPresDetail,
                                    KcbDonthuocChitiet.Columns.IdChitietdonthuoc), -1);
                        int TrangThai =
                            Utility.Int32Dbnull(
                                Utility.GetValueFromGridColumn(grdPresDetail, KcbDonthuocChitiet.Columns.TrangThai, "0"),
                                0);
                        int TrangthaiThanhtoan =
                            Utility.Int32Dbnull(
                                Utility.GetValueFromGridColumn(grdPresDetail,
                                    KcbDonthuocChitiet.Columns.TrangthaiThanhtoan, "0"), 0);
                        if (TrangThai >= 1) //Đã xác nhận duyệt VTTH
                        {
                            Utility.ShowMsg(
                                "Vật tư bạn đang chọn đã được cấp phát nên không cho phép hủy. Đề nghị bạn kiểm tra lại");
                            return;
                        }
                        if (TrangthaiThanhtoan > 0) //Đã thanh toán
                        {
                            Utility.ShowMsg(
                                "Vật tư bạn đang chọn đã thanh toán nên không cho phép hủy. Đề nghị bạn kiểm tra lại");
                            return;
                        }
                        new Update(KcbDonthuocChitiet.Schema).Set(KcbDonthuocChitiet.Columns.TrangthaiHuy).EqualTo(1)
                            .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(Id).Execute();
                        grdPresDetail.CurrentRow.BeginEdit();
                        grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.TrangthaiHuy].Value = 1;
                        grdPresDetail.CurrentRow.EndEdit();
                        _grd = grdPresDetail;
                    }
                }
                else //Sử dụng lại
                {
                    if (ctrlName == grdAssignDetail.Name)
                    {
                        long Id =
                            Utility.Int64Dbnull(
                                Utility.GetValueFromGridColumn(grdAssignDetail,
                                    KcbChidinhclsChitiet.Columns.IdChitietchidinh), -1);
                        int trangthai_chuyencls =
                            Utility.Int32Dbnull(
                                Utility.GetValueFromGridColumn(grdAssignDetail, KcbChidinhclsChitiet.Columns.TrangThai,
                                    "0"), 0);
                        int TrangthaiThanhtoan =
                            Utility.Int32Dbnull(
                                Utility.GetValueFromGridColumn(grdAssignDetail,
                                    KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan, "0"), 0);
                        if (TrangthaiThanhtoan > 0) //Đã thanh toán
                        {
                            Utility.ShowMsg(
                                "Chỉ định bạn đang chọn đã thanh toán nên không cho phép sử dụng lại. Đề nghị bạn kiểm tra lại");
                            return;
                        }
                        new Update(KcbChidinhclsChitiet.Schema).Set(KcbChidinhclsChitiet.Columns.TrangthaiHuy)
                            .EqualTo(0)
                            .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(Id).Execute();
                        grdAssignDetail.CurrentRow.BeginEdit();
                        grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.TrangthaiHuy].Value = 0;
                        grdAssignDetail.CurrentRow.EndEdit();
                        _grd = grdAssignDetail;
                    }
                    else if (ctrlName == grdPresDetail.Name)
                    {
                        long Id =
                            Utility.Int64Dbnull(
                                Utility.GetValueFromGridColumn(grdPresDetail,
                                    KcbDonthuocChitiet.Columns.IdChitietdonthuoc), -1);
                        int TrangThai =
                            Utility.Int32Dbnull(
                                Utility.GetValueFromGridColumn(grdPresDetail, KcbDonthuocChitiet.Columns.TrangThai, "0"),
                                0);
                        int TrangthaiThanhtoan =
                            Utility.Int32Dbnull(
                                Utility.GetValueFromGridColumn(grdPresDetail,
                                    KcbDonthuocChitiet.Columns.TrangthaiThanhtoan, "0"), 0);
                        if (TrangthaiThanhtoan > 0) //Đã thanh toán
                        {
                            Utility.ShowMsg(
                                "Thuốc bạn đang chọn đã thanh toán nên không cho phép sử dụng lại. Đề nghị bạn kiểm tra lại");
                            return;
                        }
                        new Update(KcbDonthuocChitiet.Schema).Set(KcbDonthuocChitiet.Columns.TrangthaiHuy).EqualTo(0)
                            .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(Id).Execute();
                        grdPresDetail.CurrentRow.BeginEdit();
                        grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.TrangthaiHuy].Value = 0;
                        grdPresDetail.CurrentRow.EndEdit();
                        _grd = grdPresDetail;
                    }
                    else //VTTH
                    {
                        long Id =
                            Utility.Int64Dbnull(
                                Utility.GetValueFromGridColumn(grdVTTH, KcbDonthuocChitiet.Columns.IdChitietdonthuoc),
                                -1);
                        int TrangThai =
                            Utility.Int32Dbnull(
                                Utility.GetValueFromGridColumn(grdVTTH, KcbDonthuocChitiet.Columns.TrangThai, "0"), 0);
                        int TrangthaiThanhtoan =
                            Utility.Int32Dbnull(
                                Utility.GetValueFromGridColumn(grdVTTH, KcbDonthuocChitiet.Columns.TrangthaiThanhtoan,
                                    "0"), 0);

                        if (TrangthaiThanhtoan > 0) //Đã thanh toán
                        {
                            Utility.ShowMsg(
                                "Vật tư bạn đang chọn đã thanh toán nên không cho phép sử dụng lại. Đề nghị bạn kiểm tra lại");
                            return;
                        }
                        new Update(KcbDonthuocChitiet.Schema).Set(KcbDonthuocChitiet.Columns.TrangthaiHuy).EqualTo(0)
                            .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(Id).Execute();
                        grdVTTH.CurrentRow.BeginEdit();
                        grdVTTH.CurrentRow.Cells[KcbDonthuocChitiet.Columns.TrangthaiHuy].Value = 0;
                        grdVTTH.CurrentRow.EndEdit();
                        _grd = grdVTTH;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                ChangeMenu(_grd.CurrentRow);
            }
        }

        private void grdChiphithem_SelectionChanged(object sender, EventArgs e)
        {
            ChangeMenu(grdChiphithem.CurrentRow);
        }

        private void grdVTTH_SelectionChanged(object sender, EventArgs e)
        {
            ChangeMenu(grdVTTH.CurrentRow);
        }

        private void ChangeMenu(GridEXRow _row)
        {
            mnuTutuc.Text = Utility.GetValueFromGridColumn(_row, KcbThanhtoanChitiet.Columns.TuTuc) == "1"
                ? "Giá đối tượng"
                : "Tự túc";
            mnuTutuc.Tag = Utility.GetValueFromGridColumn(_row, KcbThanhtoanChitiet.Columns.TuTuc);
            mnuTronggoi.Text = Utility.GetValueFromGridColumn(_row, KcbThanhtoanChitiet.Columns.TrongGoi) == "1"
                ? "Ngoài gói"
                : "Trong gói";
            mnuTronggoi.Tag = Utility.GetValueFromGridColumn(_row, KcbThanhtoanChitiet.Columns.TrongGoi);

            mnuCancel.Text = Utility.GetValueFromGridColumn(_row, KcbThanhtoanChitiet.Columns.TrangthaiHuy) == "1"
                ? "Sử dụng dịch vụ"
                : "Hủy dịch vụ";
            mnuCancel.Tag = Utility.GetValueFromGridColumn(_row, KcbThanhtoanChitiet.Columns.TrangthaiHuy);
        }

        private void mnuTutuc_Click(object sender, EventArgs e)
        {
            try
            {
                if (objLuotkham.TrangthaiNoitru == 5)
                {
                    Utility.ShowMsg(
                        "Bệnh nhân đã được duyệt thanh toán nội trú nên bạn không thể thay đổi trạng thái tự túc");
                    return;
                }

                if (Utility.Byte2Bool(objLuotkham.TthaiThanhtoannoitru) || objLuotkham.TrangthaiNoitru == 6)
                {
                    Utility.ShowMsg("Bệnh nhân đã được thanh toán nội trú nên bạn không thể thay đổi trạng thái tự túc");
                    return;
                }

                string ctrlName = ((ContextMenuStrip) ((ToolStripMenuItem) sender).Owner).SourceControl.Name;
                if (mnuTutuc.Tag.ToString() == "0") //Tự túc
                {
                    if (ctrlName == grdAssignDetail.Name)
                    {
                        long Id =
                            Utility.Int64Dbnull(
                                Utility.GetValueFromGridColumn(grdAssignDetail,
                                    KcbChidinhclsChitiet.Columns.IdChitietchidinh), -1);
                        int TrangthaiThanhtoan =
                            Utility.Int32Dbnull(
                                Utility.GetValueFromGridColumn(grdAssignDetail,
                                    KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan, "0"), 0);

                        if (TrangthaiThanhtoan > 0) //Đã thanh toán
                        {
                            Utility.ShowMsg(
                                "Chỉ định bạn đang chọn đã thanh toán nên không cho phép thay đổi trạng thái tự túc. Đề nghị bạn kiểm tra lại");
                            return;
                        }
                        new Update(KcbChidinhclsChitiet.Schema).Set(KcbChidinhclsChitiet.Columns.TuTuc).EqualTo(1)
                            .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(Id).Execute();
                        grdAssignDetail.CurrentRow.BeginEdit();
                        grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.TuTuc].Value = 1;
                        grdAssignDetail.CurrentRow.EndEdit();
                        ChangeMenu(grdAssignDetail.CurrentRow);
                    }
                    else if (ctrlName == grdPresDetail.Name)
                    {
                        long Id =
                            Utility.Int64Dbnull(
                                Utility.GetValueFromGridColumn(grdPresDetail,
                                    KcbDonthuocChitiet.Columns.IdChitietdonthuoc), -1);
                        int TrangthaiThanhtoan =
                            Utility.Int32Dbnull(
                                Utility.GetValueFromGridColumn(grdPresDetail,
                                    KcbDonthuocChitiet.Columns.TrangthaiThanhtoan, "0"), 0);

                        if (TrangthaiThanhtoan > 0) //Đã thanh toán
                        {
                            Utility.ShowMsg(
                                "Thuốc bạn đang chọn đã thanh toán nên không cho phép thay đổi trạng thái tự túc. Đề nghị bạn kiểm tra lại");
                            return;
                        }
                        new Update(KcbDonthuocChitiet.Schema).Set(KcbDonthuocChitiet.Columns.TuTuc).EqualTo(1)
                            .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(Id).Execute();
                        grdPresDetail.CurrentRow.BeginEdit();
                        grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.TuTuc].Value = 1;
                        grdPresDetail.CurrentRow.EndEdit();
                        ChangeMenu(grdPresDetail.CurrentRow);
                    }
                    else //VTTH
                    {
                        long Id =
                            Utility.Int64Dbnull(
                                Utility.GetValueFromGridColumn(grdVTTH, KcbDonthuocChitiet.Columns.IdChitietdonthuoc),
                                -1);
                        int TrangthaiThanhtoan =
                            Utility.Int32Dbnull(
                                Utility.GetValueFromGridColumn(grdVTTH, KcbDonthuocChitiet.Columns.TrangthaiThanhtoan,
                                    "0"), 0);

                        if (TrangthaiThanhtoan > 0) //Đã thanh toán
                        {
                            Utility.ShowMsg(
                                "Vật tư bạn đang chọn đã thanh toán nên không cho phép thay đổi trạng thái tự túc. Đề nghị bạn kiểm tra lại");
                            return;
                        }
                        new Update(KcbDonthuocChitiet.Schema).Set(KcbDonthuocChitiet.Columns.TuTuc).EqualTo(1)
                            .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(Id).Execute();
                        grdVTTH.CurrentRow.BeginEdit();
                        grdVTTH.CurrentRow.Cells[KcbDonthuocChitiet.Columns.TuTuc].Value = 1;
                        grdVTTH.CurrentRow.EndEdit();
                        ChangeMenu(grdVTTH.CurrentRow);
                    }
                }
                else //Không tự túc
                {
                    if (ctrlName == grdAssignDetail.Name)
                    {
                        long Id =
                            Utility.Int64Dbnull(
                                Utility.GetValueFromGridColumn(grdAssignDetail,
                                    KcbChidinhclsChitiet.Columns.IdChitietchidinh), -1);
                        int TrangthaiThanhtoan =
                            Utility.Int32Dbnull(
                                Utility.GetValueFromGridColumn(grdAssignDetail,
                                    KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan, "0"), 0);
                        if (TrangthaiThanhtoan > 0) //Đã thanh toán
                        {
                            Utility.ShowMsg(
                                "Chỉ định bạn đang chọn đã thanh toán nên không cho phép thay đổi trạng thái tự túc. Đề nghị bạn kiểm tra lại");
                            return;
                        }
                        new Update(KcbChidinhclsChitiet.Schema).Set(KcbChidinhclsChitiet.Columns.TuTuc).EqualTo(0)
                            .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(Id).Execute();
                        grdAssignDetail.CurrentRow.BeginEdit();
                        grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.TuTuc].Value = 0;
                        grdAssignDetail.CurrentRow.EndEdit();
                        ChangeMenu(grdAssignDetail.CurrentRow);
                    }
                    else if (ctrlName == grdPresDetail.Name)
                    {
                        long Id =
                            Utility.Int64Dbnull(
                                Utility.GetValueFromGridColumn(grdPresDetail,
                                    KcbDonthuocChitiet.Columns.IdChitietdonthuoc), -1);
                        int TrangthaiThanhtoan =
                            Utility.Int32Dbnull(
                                Utility.GetValueFromGridColumn(grdPresDetail,
                                    KcbDonthuocChitiet.Columns.TrangthaiThanhtoan, "0"), 0);
                        if (TrangthaiThanhtoan > 0) //Đã thanh toán
                        {
                            Utility.ShowMsg(
                                "Thuốc bạn đang chọn đã thanh toán nên không cho phép thay đổi trạng thái tự túc. Đề nghị bạn kiểm tra lại");
                            return;
                        }
                        new Update(KcbDonthuocChitiet.Schema).Set(KcbDonthuocChitiet.Columns.TuTuc).EqualTo(0)
                            .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(Id).Execute();
                        grdPresDetail.CurrentRow.BeginEdit();
                        grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.TuTuc].Value = 0;
                        grdPresDetail.CurrentRow.EndEdit();
                        ChangeMenu(grdPresDetail.CurrentRow);
                    }
                    else //VTTH
                    {
                        long Id =
                            Utility.Int64Dbnull(
                                Utility.GetValueFromGridColumn(grdVTTH, KcbDonthuocChitiet.Columns.IdChitietdonthuoc),
                                -1);
                        int TrangthaiThanhtoan =
                            Utility.Int32Dbnull(
                                Utility.GetValueFromGridColumn(grdVTTH, KcbDonthuocChitiet.Columns.TrangthaiThanhtoan,
                                    "0"), 0);

                        if (TrangthaiThanhtoan > 0) //Đã thanh toán
                        {
                            Utility.ShowMsg(
                                "Vật tư bạn đang chọn đã thanh toán nên không cho phép thay đổi trạng thái tự túc. Đề nghị bạn kiểm tra lại");
                            return;
                        }
                        new Update(KcbDonthuocChitiet.Schema).Set(KcbDonthuocChitiet.Columns.TuTuc).EqualTo(0)
                            .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(Id).Execute();
                        grdVTTH.CurrentRow.BeginEdit();
                        grdVTTH.CurrentRow.Cells[KcbDonthuocChitiet.Columns.TuTuc].Value = 0;
                        grdVTTH.CurrentRow.EndEdit();
                        ChangeMenu(grdVTTH.CurrentRow);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void mnuTronggoi_Click(object sender, EventArgs e)
        {
            try
            {
                if (objLuotkham.TrangthaiNoitru == 5)
                {
                    Utility.ShowMsg(
                        "Bệnh nhân đã được duyệt thanh toán nội trú nên bạn không thể thay đổi trạng thái trong gói/ngoài gói");
                    return;
                }

                if (Utility.Byte2Bool(objLuotkham.TthaiThanhtoannoitru) || objLuotkham.TrangthaiNoitru == 6)
                {
                    Utility.ShowMsg(
                        "Bệnh nhân đã được thanh toán nội trú nên bạn không thể thay đổi trạng thái trong gói/ngoài gói");
                    return;
                }

                string ctrlName = ((ContextMenuStrip) ((ToolStripMenuItem) sender).Owner).SourceControl.Name;
                if (mnuTronggoi.Tag.ToString() == "0") //Đưa vào trong gói
                {
                    if (ctrlName == grdAssignDetail.Name)
                    {
                        long Id =
                            Utility.Int64Dbnull(
                                Utility.GetValueFromGridColumn(grdAssignDetail,
                                    KcbChidinhclsChitiet.Columns.IdChitietchidinh), -1);
                        int TrangthaiThanhtoan =
                            Utility.Int32Dbnull(
                                Utility.GetValueFromGridColumn(grdAssignDetail,
                                    KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan, "0"), 0);

                        if (TrangthaiThanhtoan > 0) //Đã thanh toán
                        {
                            Utility.ShowMsg(
                                "Chỉ định bạn đang chọn đã thanh toán nên không cho phép thay đổi trạng thái trong gói/ngoài gói. Đề nghị bạn kiểm tra lại");
                            return;
                        }
                        new Update(KcbChidinhclsChitiet.Schema).Set(KcbChidinhclsChitiet.Columns.TrongGoi).EqualTo(1)
                            .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(Id).Execute();
                        grdAssignDetail.CurrentRow.BeginEdit();
                        grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.TrongGoi].Value = 1;
                        grdAssignDetail.CurrentRow.EndEdit();
                        grdAssignDetail.Refresh();
                        ChangeMenu(grdAssignDetail.CurrentRow);
                    }
                    else if (ctrlName == grdPresDetail.Name)
                    {
                        long Id =
                            Utility.Int64Dbnull(
                                Utility.GetValueFromGridColumn(grdPresDetail,
                                    KcbDonthuocChitiet.Columns.IdChitietdonthuoc), -1);
                        int TrangthaiThanhtoan =
                            Utility.Int32Dbnull(
                                Utility.GetValueFromGridColumn(grdPresDetail,
                                    KcbDonthuocChitiet.Columns.TrangthaiThanhtoan, "0"), 0);

                        if (TrangthaiThanhtoan > 0) //Đã thanh toán
                        {
                            Utility.ShowMsg(
                                "Thuốc bạn đang chọn đã thanh toán nên không cho phép thay đổi trạng thái trong gói/ngoài gói. Đề nghị bạn kiểm tra lại");
                            return;
                        }
                        new Update(KcbDonthuocChitiet.Schema).Set(KcbDonthuocChitiet.Columns.TrongGoi).EqualTo(1)
                            .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(Id).Execute();
                        grdPresDetail.CurrentRow.BeginEdit();
                        grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.TrongGoi].Value = 1;
                        grdPresDetail.CurrentRow.EndEdit();
                        grdPresDetail.Refresh();
                        ChangeMenu(grdPresDetail.CurrentRow);
                    }
                    else //VTTH
                    {
                        long Id =
                            Utility.Int64Dbnull(
                                Utility.GetValueFromGridColumn(grdVTTH, KcbDonthuocChitiet.Columns.IdChitietdonthuoc),
                                -1);
                        int TrangthaiThanhtoan =
                            Utility.Int32Dbnull(
                                Utility.GetValueFromGridColumn(grdVTTH, KcbDonthuocChitiet.Columns.TrangthaiThanhtoan,
                                    "0"), 0);
                        if (TrangthaiThanhtoan > 0) //Đã thanh toán
                        {
                            Utility.ShowMsg(
                                "Vật tư bạn đang chọn đã thanh toán nên không cho phép thay đổi trạng thái trong gói/ngoài gói. Đề nghị bạn kiểm tra lại");
                            return;
                        }
                        new Update(KcbDonthuocChitiet.Schema).Set(KcbDonthuocChitiet.Columns.TrongGoi).EqualTo(1)
                            .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(Id).Execute();
                        grdVTTH.CurrentRow.BeginEdit();
                        grdVTTH.CurrentRow.Cells[KcbDonthuocChitiet.Columns.TrongGoi].Value = 1;
                        grdVTTH.CurrentRow.EndEdit();
                        grdVTTH.Refresh();
                        ChangeMenu(grdVTTH.CurrentRow);
                    }
                }
                else //Ngoài gói
                {
                    if (ctrlName == grdAssignDetail.Name)
                    {
                        long Id =
                            Utility.Int64Dbnull(
                                Utility.GetValueFromGridColumn(grdAssignDetail,
                                    KcbChidinhclsChitiet.Columns.IdChitietchidinh), -1);
                        int TrangthaiThanhtoan =
                            Utility.Int32Dbnull(
                                Utility.GetValueFromGridColumn(grdAssignDetail,
                                    KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan, "0"), 0);
                        if (TrangthaiThanhtoan > 0) //Đã thanh toán
                        {
                            Utility.ShowMsg(
                                "Chỉ định bạn đang chọn đã thanh toán nên không cho phép thay đổi trạng thái trong gói/ngoài gói. Đề nghị bạn kiểm tra lại");
                            return;
                        }
                        new Update(KcbChidinhclsChitiet.Schema).Set(KcbChidinhclsChitiet.Columns.TrongGoi).EqualTo(0)
                            .Set(KcbChidinhclsChitiet.Columns.IdGoi).EqualTo(-1)
                            .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(Id).Execute();
                        grdAssignDetail.CurrentRow.BeginEdit();
                        grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.TrongGoi].Value = 0;
                        grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.IdGoi].Value = -1;
                        grdAssignDetail.CurrentRow.EndEdit();
                        grdAssignDetail.Refresh();
                        ChangeMenu(grdAssignDetail.CurrentRow);
                    }
                    else if (ctrlName == grdPresDetail.Name)
                    {
                        long Id =
                            Utility.Int64Dbnull(
                                Utility.GetValueFromGridColumn(grdPresDetail,
                                    KcbDonthuocChitiet.Columns.IdChitietdonthuoc), -1);
                        int TrangthaiThanhtoan =
                            Utility.Int32Dbnull(
                                Utility.GetValueFromGridColumn(grdPresDetail,
                                    KcbDonthuocChitiet.Columns.TrangthaiThanhtoan, "0"), 0);
                        if (TrangthaiThanhtoan > 0) //Đã thanh toán
                        {
                            Utility.ShowMsg(
                                "Thuốc bạn đang chọn đã thanh toán nên không cho phép thay đổi trạng thái trong gói/ngoài gói. Đề nghị bạn kiểm tra lại");
                            return;
                        }
                        new Update(KcbDonthuocChitiet.Schema).Set(KcbDonthuocChitiet.Columns.TrongGoi).EqualTo(0)
                            .Set(KcbDonthuocChitiet.Columns.IdGoi).EqualTo(-1)
                            .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(Id).Execute();
                        grdPresDetail.CurrentRow.BeginEdit();
                        grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.TrongGoi].Value = 0;
                        grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdGoi].Value = -1;
                        grdPresDetail.CurrentRow.EndEdit();
                        grdPresDetail.Refresh();
                        ChangeMenu(grdPresDetail.CurrentRow);
                    }
                    else //VTTH
                    {
                        long Id =
                            Utility.Int64Dbnull(
                                Utility.GetValueFromGridColumn(grdVTTH, KcbDonthuocChitiet.Columns.IdChitietdonthuoc),
                                -1);
                        int TrangthaiThanhtoan =
                            Utility.Int32Dbnull(
                                Utility.GetValueFromGridColumn(grdVTTH, KcbDonthuocChitiet.Columns.TrangthaiThanhtoan,
                                    "0"), 0);

                        if (TrangthaiThanhtoan > 0) //Đã thanh toán
                        {
                            Utility.ShowMsg(
                                "Vật tư bạn đang chọn đã thanh toán nên không cho phép thay đổi trạng thái trong gói/ngoài gói. Đề nghị bạn kiểm tra lại");
                            return;
                        }
                        new Update(KcbDonthuocChitiet.Schema).Set(KcbDonthuocChitiet.Columns.TrongGoi).EqualTo(0)
                            .Set(KcbDonthuocChitiet.Columns.IdGoi).EqualTo(-1)
                            .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(Id).Execute();
                        grdVTTH.CurrentRow.BeginEdit();
                        grdVTTH.CurrentRow.Cells[KcbDonthuocChitiet.Columns.TrongGoi].Value = 0;
                        grdVTTH.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdGoi].Value = -1;
                        grdVTTH.CurrentRow.EndEdit();
                        grdVTTH.Refresh();
                        ChangeMenu(grdVTTH.CurrentRow);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void lnkViewAll_Click(object sender, EventArgs e)
        {
            viewAll = true;
            LaythongtinPhieudieutri();
        }

        private void cmdXoachiphithem_Click(object sender, EventArgs e)
        {
            if (!IsValidChiphithem()) return;
            XoaChiphithem();
            ModifyCommmands();
        }

        private void cmdSuachiphithem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckPatientSelected()) return;
                var frm = new frm_KCB_CHIDINH_CLS("CHIPHITHEM", 2);
                frm.HosStatus = 1;
                frm.objPhieudieutriNoitru = null;
                    // NoitruPhieudieutri.FetchByID(Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdPhieudieutri, NoitruPhieudieutri.Columns.IdPhieudieutri)));
                frm.Exam_ID = -1;
                frm.objLuotkham = objLuotkham;
                frm.objBenhnhan = objBenhnhan;
                frm.m_eAction = action.Update;
                frm.txtAssign_ID.Text = Utility.sDbnull(grdChiphithem.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh),
                    "-1");
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    LaythongtinPhieudieutri();
                    TinhtoanTongchiphi();
                    ModifyCommmands();
                }
            }
            catch (Exception ex)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg("Lỗi trong quá trình sửa phiếu :" + e);
                }
                //throw;
            }
        }

        /// <summary>
        ///     Chỉ cho phép tạo một phiếu chi phí thêm duy nhất
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdThemchiphithem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckPatientSelected()) return;
                if (grdChiphithem.GetDataRows().Length > 0)
                    cmdSuachiphithem_Click(cmdSuachiphithem, e);
                else
                {
                    var frm = new frm_KCB_CHIDINH_CLS("CHIPHITHEM", 2);
                    frm.txtAssign_ID.Text = "-100";
                    frm.Exam_ID = -1;
                    frm.objLuotkham = objLuotkham;
                    frm.objBenhnhan = objBenhnhan;
                    frm.objPhieudieutriNoitru = null;
                        // NoitruPhieudieutri.FetchByID(Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdPhieudieutri, NoitruPhieudieutri.Columns.IdPhieudieutri)));
                    frm.m_eAction = action.Insert;
                    frm.txtAssign_ID.Text = "-1";
                    frm.HosStatus = 1;
                    frm.ShowDialog();
                    if (!frm.m_blnCancel)
                    {
                        LaythongtinPhieudieutri();
                        TinhtoanTongchiphi();
                        if (PropertyLib._ThamKhamProperties.TudongthugonCLS)
                            grdChiphithem.GroupMode = GroupMode.Collapsed;
                        Utility.GotoNewRowJanus(grdChiphithem, KcbChidinhclsChitiet.Columns.IdChidinh,
                            frm.txtAssign_ID.Text);
                        ModifyCommmands();
                    }
                }
            }
            catch (Exception)
            {
                //throw;
            }
            finally
            {
                txtPatient_Code.Focus();
                txtPatient_Code.SelectAll();
            }
        }

        private void cmdRavien_Click(object sender, EventArgs e)
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Bạn cần chọn bệnh nhân trước khi thực hiện hủy chuyển viện");
                return;
            }
            if (objLuotkham.TrangthaiNoitru == 4)
            {
                Utility.ShowMsg(
                    "Bệnh nhân đã được xác nhận dữ liệu nội trú để ra viện nên bạn không thể điều chỉnh phiếu ra viện");
                return;
            }
            if (objLuotkham.TrangthaiNoitru == 5)
            {
                Utility.ShowMsg(
                    "Bệnh nhân đã được duyệt thanh toán nội trú để ra viện nên bạn không thể điều chỉnh phiếu ra viện");
                return;
            }
            if (objLuotkham.TrangthaiNoitru == 6)
            {
                Utility.ShowMsg(
                    "Bệnh nhân đã kết thúc điều trị nội trú(Đã thanh toán xong) nên bạn không thể điều chỉnh phiếu ra viện");
                return;
            }

            var _Phieuravien = new frm_Phieuravien();
            _Phieuravien.objLuotkham = objLuotkham;
            _Phieuravien.AutoLoad = true;
            _Phieuravien.ShowDialog();
            if (!_Phieuravien.mv_blnCancel && _Phieuravien.objLuotkham != null)
            {
                objLuotkham = Utility.getKcbLuotkham(objLuotkham);
                cmdXacnhan.Enabled = _khoanoitrutonghop ||
                                     (_Phieuravien.objLuotkham.TrangthaiNoitru >= 3 &&
                                      _Phieuravien.objLuotkham.TrangthaiNoitru <= 4);
            }
            //Tính toán lại thông tin buồng giường cuối cùng
            LayLichsuBuongGiuong();
        }

        private void TudongtinhtoansongaynamvienCuoicung()
        {
            try
            {
                NoitruPhanbuonggiuong objNoitruPhanbuonggiuong = NoitruPhanbuonggiuong.FetchByID(objLuotkham.IdRavien);
                DataRow[] arrDr = m_dtBuongGiuong.Select(NoitruPhanbuonggiuong.Columns.Id + "=" + objLuotkham.IdRavien);
                if (arrDr.Length > 0 && objNoitruPhanbuonggiuong != null)
                {
                    objNoitruPhanbuonggiuong.NgayKetthuc = objLuotkham.NgayRavien;
                    objNoitruPhanbuonggiuong.TrangThai = 1;
                    objNoitruPhanbuonggiuong.TrangthaiChuyen = 0;
                    objNoitruPhanbuonggiuong.SoLuong =
                        THU_VIEN_CHUNG.Songay(objNoitruPhanbuonggiuong.NgayPhangiuong.Value,
                            objNoitruPhanbuonggiuong.NgayKetthuc.Value);
                    objNoitruPhanbuonggiuong.SoluongGio =
                        (int)
                            Math.Ceiling(
                                (objNoitruPhanbuonggiuong.NgayKetthuc.Value -
                                 objNoitruPhanbuonggiuong.NgayPhangiuong.Value).TotalHours);
                    objNoitruPhanbuonggiuong.MarkOld();
                    objNoitruPhanbuonggiuong.IsNew = true;
                    objNoitruPhanbuonggiuong.Save();
                    arrDr[0][NoitruPhanbuonggiuong.Columns.NgayKetthuc] = objNoitruPhanbuonggiuong.NgayKetthuc;
                    arrDr[0][NoitruPhanbuonggiuong.Columns.TrangThai] = objNoitruPhanbuonggiuong.TrangThai;
                    arrDr[0][NoitruPhanbuonggiuong.Columns.TrangthaiChuyen] = objNoitruPhanbuonggiuong.TrangthaiChuyen;
                    arrDr[0][NoitruPhanbuonggiuong.Columns.SoLuong] = objNoitruPhanbuonggiuong.SoLuong;
                    arrDr[0][NoitruPhanbuonggiuong.Columns.SoluongGio] = objNoitruPhanbuonggiuong.SoluongGio;
                }
                else
                {
                    Utility.ShowMsg(
                        "Không xác định được buồng giường hiện tại bệnh nhân đang nằm. Đề nghị liên hệ IT để được trợ giúp");
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void cmdInphoiBHYT_Click(object sender, EventArgs e)
        {
        }

        private void cmdTonghop_Click(object sender, EventArgs e)
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Bạn cần phải chọn bệnh nhân trước khi xem tổng hợp chi phí nội trú");
                return;
            }
            short idkhoanoitru = Utility.Int16Dbnull(cboKhoanoitru.SelectedValue, 0);
                //Khoa nội trú tổng hợp để chuyển khoa
            if (!_khoanoitrutonghop) //Điều dưỡng tổng hợp trước khi ra viện
                idkhoanoitru = -1;
            var _Xemtonghopchiphi = new frm_Xemtonghopchiphi(_khoanoitrutonghop, idkhoanoitru.ToString());

            _Xemtonghopchiphi.objLuotkham = objLuotkham;
            _Xemtonghopchiphi.ShowDialog();
        }

        private void cmdXacnhan_Click(object sender, EventArgs e)
        {
            if (!IsValidData())
                return;
            short idkhoanoitru = Utility.Int16Dbnull(cboKhoanoitru.SelectedValue, 0);
                //Khoa nội trú tổng hợp để chuyển khoa
            if (!_khoanoitrutonghop) //Điều dưỡng tổng hợp trước khi ra viện
            {
                idkhoanoitru = -1;
                objLuotkham.TthaiThopNoitru = Utility.Bool2byte(chkXacnhan.Checked);
                objLuotkham.TrangthaiNoitru = (byte) (chkXacnhan.Checked ? 4 : 3);
            }
            if (noitru_tonghopchiphi.TongHopChiPhi(objLuotkham, idkhoanoitru, _khoanoitrutonghop) == ActionResult.Success)
            {
                cmdRavien.Enabled = objLuotkham != null && objLuotkham.TrangthaiNoitru <= 3;
            }
        }

        private bool IsValidData()
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Bạn cần chọn bệnh nhân trước khi thực hiện xác nhận dữ liệu nội trú");
                return false;
            }
            if (objLuotkham.TrangthaiNoitru == 0)
            {
                Utility.ShowMsg(
                    "Bệnh nhân chưa nhập viện nội trú nên bạn không thể xác nhận dữ liệu nội trú. Đề nghị bạn kiểm tra lại");
                return false;
            }
            if (chkXacnhan.Checked)
                if (!_khoanoitrutonghop && objLuotkham.TrangthaiNoitru < 3)
                {
                    Utility.ShowMsg(
                        "Bệnh nhân chưa lập phiếu ra viện nên bạn không thể xác nhận dữ liệu nội trú. Đề nghị bạn lập phiếu ra viện trước");
                    cmdRavien.Focus();
                    return false;
                }
            if (objLuotkham.TrangthaiNoitru == 5)
            {
                Utility.ShowMsg(
                    "Bệnh nhân đã được duyệt thanh toán nội trú nên bạn không thể sửa lại trạng thái xác nhận dữ liệu nội trú");
                return false;
            }
            if (Utility.Byte2Bool(objLuotkham.TthaiThanhtoannoitru) || objLuotkham.TrangthaiNoitru == 6)
            {
                Utility.ShowMsg(
                    "Bệnh nhân đã được thanh toán nội trú nên bạn không thể sửa lại trạng thái xác nhận dữ liệu nội trú");
                return false;
            }
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_KT_TONGHOPTHUOC_KHIRAVIEN", "0", false) == "1")
            {
                //Kiểm tra cấp phát thuốc nội trú
                int reval = -1;
                StoredProcedure sp = SPs.NoitruKiemtratrangthaiDonthuocTruockhitonghop(objLuotkham.MaLuotkham,
                    objLuotkham.IdBenhnhan, reval);
                sp.Execute();
                reval = Utility.Int32Dbnull(sp.OutputValues[0], -1);
                if (reval == 1)
                {
                    Utility.ShowMsg(
                        "Bệnh nhân này còn một số đơn thuốc nội trú chưa được tổng hợp và lĩnh thuốc. Đề nghị bạn kiểm tra lại");
                    return false;
                }
            }
            string question = "Bạn có chắc chắc muốn xác nhận dữ liệu nội trú cho Bệnh nhân " + txtPatient_Name.Text;
            if (!_khoanoitrutonghop)
                question += "\nSau khi xác nhận xong, dữ liệu nội trú sẽ được phép thanh toán tại quầy thu ngân";
            else
                question +=
                    "\nSau khi xác nhận xong, bạn sẽ không được phép chỉnh sửa thông tin điều trị tại khoa nội trú này nữa";
            if (!Utility.AcceptQuestion(question, "Xác nhận dữ liệu", true))
            {
                return false;
            }
            return true;
        }

        private void lnkSize_Click(object sender, EventArgs e)
        {
            var _Properties = new frm_Properties(PropertyLib._DynamicInputProperties);
            if (_Properties.ShowDialog() == DialogResult.OK)
            {
                ShowResult();
            }
        }

        private void mnuShowResult_Click(object sender, EventArgs e)
        {
            //mnuShowResult.Tag = mnuShowResult.Checked ? "1" : "0";
            //if (PropertyLib._ThamKhamProperties.HienthiKetquaCLSTrongluoiChidinh)
            //{
            //    if (mnuShowResult.Checked)
            //        Utility.ShowColumns(grdAssignDetail, lstResultColumns);
            //    else
            //        Utility.ShowColumns(grdAssignDetail, lstVisibleColumns);
            //}
            //else
            //    grdAssignDetail_SelectionChanged(grdAssignDetail, e);
        }

        private void cmdChuyengoi_Click(object sender, EventArgs e)
        {
            var _chuyenVTTHvaotronggoiDV = new frm_chuyenVTTHvaotronggoiDV();
            _chuyenVTTHvaotronggoiDV.objLuotkham = objLuotkham;
            _chuyenVTTHvaotronggoiDV.ShowDialog();
            if (!_chuyenVTTHvaotronggoiDV.m_blnCancel)
            {
                LaythongtinPhieudieutri();
            }
        }


        private void grdGoidichvu_DoubleClick(object sender, EventArgs e)
        {
            grdGoidichvu_SelectionChanged(grdGoidichvu, e);
        }


        /// <summary>
        ///     Kiểm tra xem còn phiếu VTTH nào chưa cấp phát
        /// </summary>
        /// <returns></returns>
        private bool CanUpdateVTTH()
        {
            //Lấy về phiếu VTTH trong gói chưa được cấp phát vật tư
            EnumerableRowCollection<DataRow> q = from p in m_dtVTTH_tronggoi.AsEnumerable()
                where Utility.Int32Dbnull(p[KcbDonthuocChitiet.Columns.TrangThai], 0) == 0
                select p;
            return q.Count() > 0;
        }

        private void grdGoidichvu_SelectionChanged(object sender, EventArgs e)
        {
            ChangeMenu(grdGoidichvu.CurrentRow);
        }

        private void cmdNoitruConfig_Click(object sender, EventArgs e)
        {
            var frm = new frm_Properties(PropertyLib._NoitruProperties);
            frm.ShowDialog();
        }

        private void grdPresDetail_SelectionChanged(object sender, EventArgs e)
        {
            ChangeMenu(grdPresDetail.CurrentRow);
        }


        private void cmdXoagoiDV_Click(object sender, EventArgs e)
        {
            if (!IsValidGoidichvu()) return;
            XoaGoidichvu();
            ModifyCommmands();
        }

        private void cmdSuagoiDV_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckPatientSelected()) return;
                var frm = new frm_KCB_CHIDINH_CLS("GOI", 1);
                frm.HosStatus = 1;
                frm.objPhieudieutriNoitru =
                    NoitruPhieudieutri.FetchByID(
                        Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdPhieudieutri,
                            NoitruPhieudieutri.Columns.IdPhieudieutri)));
                frm.Exam_ID = -1;
                frm.objLuotkham = objLuotkham;
                frm.objBenhnhan = objBenhnhan;
                frm.m_eAction = action.Update;
                frm.txtAssign_ID.Text = Utility.sDbnull(grdGoidichvu.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh),
                    "-1");
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    LaythongtinPhieudieutri();
                    TinhtoanTongchiphi();
                    ModifyCommmands();
                }
            }
            catch (Exception ex)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg("Lỗi trong quá trình sửa phiếu :" + e);
                }
                //throw;
            }
        }

        private void cmdThemgoiDV_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckPatientSelected()) return;
                var frm = new frm_KCB_CHIDINH_CLS("GOI", 1);
                frm.txtAssign_ID.Text = "-100";
                frm.Exam_ID = -1;
                frm.objLuotkham = objLuotkham;
                frm.objBenhnhan = objBenhnhan;
                frm.objPhieudieutriNoitru =
                    NoitruPhieudieutri.FetchByID(
                        Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdPhieudieutri,
                            NoitruPhieudieutri.Columns.IdPhieudieutri)));
                frm.m_eAction = action.Insert;
                frm.txtAssign_ID.Text = "-1";
                frm.HosStatus = 1;
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    LaythongtinPhieudieutri();
                    TinhtoanTongchiphi();
                    if (PropertyLib._ThamKhamProperties.TudongthugonCLS)
                        grdGoidichvu.GroupMode = GroupMode.Collapsed;
                    Utility.GotoNewRowJanus(grdGoidichvu, KcbChidinhclsChitiet.Columns.IdChidinh, frm.txtAssign_ID.Text);
                    ModifyCommmands();
                }
            }
            catch (Exception)
            {
                //throw;
            }
            finally
            {
                txtPatient_Code.Focus();
                txtPatient_Code.SelectAll();
            }
        }

        private void cmdCauHinh_Click(object sender, EventArgs e)
        {
            var _Properties = new frm_Properties(PropertyLib._NoitruProperties);
            _Properties.ShowDialog();
            Cauhinh();
        }

        private void Cauhinh()
        {
        }


        private void grdPhieudieutri_SelectionChanged(object sender, EventArgs e)
        {
            if (!m_blnHasLoaded) return;
            viewAll = false;
            if (PropertyLib._NoitruProperties.ViewOnClick && !_buttonClick)
                Selectionchanged();
            _buttonClick = false;
        }

        private void Selectionchanged()
        {
            try
            {
                if (Utility.isValidGrid(grdPhieudieutri))
                {
                    _CurIdPhieudieutri =
                        Utility.Int32Dbnull(
                            Utility.sDbnull(grdPhieudieutri.GetValue(NoitruPhieudieutri.Columns.IdPhieudieutri), -1), -1);
                    txtIdPhieudieutri.Text = _CurIdPhieudieutri.ToString();
                    objPhieudieutri = NoitruPhieudieutri.FetchByID(_CurIdPhieudieutri);
                    if (objPhieudieutri != null)
                    {
                        LaythongtinPhieudieutri();
                    }
                    else
                    {
                        txtIdPhieudieutri.Text = "-1";
                        grdPresDetail.DataSource = null;
                        grdAssignDetail.DataSource = null;
                        grdVTTH.DataSource = null;
                    }
                }
                else
                {
                    txtIdPhieudieutri.Text = "-1";
                    grdPresDetail.DataSource = null;
                    grdAssignDetail.DataSource = null;
                    grdVTTH.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                ModifyCommmands();
            }
        }

        private int date2Int(string date)
        {
            string[] arrdate = date.Split('/');
            return Utility.Int32Dbnull(arrdate[2] + arrdate[1] + arrdate[0], 0);
        }

        private void grdPhieudieutri_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            if (e.Column.Key == "colView")
            {
                _buttonClick = true;
                Selectionchanged();
            }
        }


        private NoitruPhanbuonggiuong getNoitruBuonggiuong()
        {
            NoitruPhanbuonggiuong objDept =
                NoitruPhanbuonggiuong.FetchByID(Utility.Int32Dbnull(objLuotkham.IdRavien, -1));
            return objDept;
        }


        private void UpdateDatatable()
        {
        }


        private void frm_LAOKHOA_Add_TIEPDON_BN_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        /// <summary>
        ///     hàm thực hiện việc tìm kiếm thông tin của thăm khám
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            txtPatient_Code_KeyDown(txtPatient_Code, new KeyEventArgs(Keys.Enter));
        }


        /// <summary>
        ///     hàm thực hiện trạng thái của nút
        /// </summary>
        private void ModifyCommmands()
        {
            string noitruHienthiChandoankcbTheophieudieutri =
                THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_HIENTHI_CHANDOANKCB_THEOPHIEUDIEUTRI", "1", false);
            string noitruHienthiGoidichvuTheophieudieutri =
                THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_HIENTHI_GOIDICHVU_THEOPHIEUDIEUTRI", "1", false);
            string noitruHienthiPhieuvtthTheophieudieutri =
                THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_HIENTHI_PHIEUVTTH_THEOPHIEUDIEUTRI", "1", false);
            try
            {
                cmdXacnhan.Enabled = objLuotkham != null &&
                                     (_khoanoitrutonghop || objLuotkham.TrangthaiNoitru == 3 ||
                                      objLuotkham.TrangthaiNoitru == 4); //Phải ra viện mới được tổng hợp
                cmdRavien.Enabled = objLuotkham != null && objLuotkham.TrangthaiNoitru <= 3;
                cmdIngoiDV.Enabled = objLuotkham != null && grdGoidichvu.RowCount > 0 && objPhieudieutri != null;

                chkintachgoidichvu.Enabled = objLuotkham != null && cmdIngoiDV.Enabled;
                tabDiagInfo.Enabled = objLuotkham != null && objPhieudieutri != null;


                cmdSuagoiDV.Enabled =
                    cmdXoagoiDV.Enabled =
                        objLuotkham != null && Utility.isValidGrid(grdGoidichvu) &&
                        (noitruHienthiGoidichvuTheophieudieutri == "0" ||
                         (noitruHienthiGoidichvuTheophieudieutri == "1" && objPhieudieutri != null));
                cmdSuachiphithem.Enabled =
                    cmdXoachiphithem.Enabled = objLuotkham != null && Utility.isValidGrid(grdGoidichvu);
                if (objLuotkham.TrangthaiNoitru >= 4)
                {
                    grdBuongGiuong.RootTable.Columns["so_luong"].EditType = EditType.NoEdit;
                }
                else
                {
                    grdBuongGiuong.RootTable.Columns["so_luong"].EditType = EditType.TextBox;
                }
                //0=Ngoại trú;1=Nội trú;2=Đã điều trị(Lập phiếu);3=Đã tổng hợp chờ ra viện;4=Ra viện
                if (objLuotkham.TrangthaiNoitru > 2)
                {
                    cmdThemgoiDV.Enabled = cmdSuagoiDV.Enabled = cmdXoagoiDV.Enabled = false;
                    cmdThemchiphithem.Enabled = cmdSuachiphithem.Enabled = cmdXoachiphithem.Enabled = false;
                }
                else
                {
                    cmdThemchiphithem.Enabled = objLuotkham != null;
                    cmdThemgoiDV.Enabled = objLuotkham != null &&
                                           (noitruHienthiGoidichvuTheophieudieutri == "0" ||
                                            (noitruHienthiGoidichvuTheophieudieutri == "1" && objPhieudieutri != null));
                }
            }
            catch (Exception exception)
            {
            }
        }


        private void LaythongtinPhieudieutri()
        {
            bool IsAdmin = Utility.Coquyen("quyen_xemphieudieutricuabacsinoitrukhac")
                           ||
                           THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_CHOPHEPXEM_PHIEUDIEUTRI_CUABACSIKHAC", "0",
                               false) == "1";
            IsAdmin = IsAdmin || !_khoanoitrutonghop;
            ds =
                _KCB_THAMKHAM.NoitruLaythongtinclsThuocTheophieudieutri(Utility.Int32Dbnull(txtPatient_ID.Text, -1),
                    Utility.sDbnull(malankham, ""),
                    viewAll ? -1 : Utility.Int32Dbnull(txtIdPhieudieutri.Text), Utility.Bool2byte(!_khoanoitrutonghop)
                    , IsAdmin ? "-1" : Utility.sDbnull(cboKhoanoitru.SelectedValue, "0"));
            viewAll = false;
            m_dtAssignDetail = ds.Tables[0];
            m_dtDonthuoc = ds.Tables[1];
            m_dtVTTH = ds.Tables[2];
            m_dtGoidichvu = ds.Tables[3];
            m_dtChandoanKCB = ds.Tables[4];
            m_dtChedoDinhduong = ds.Tables[5];
            m_dtChiphithem = ds.Tables[6];
            Utility.SetDataSourceForDataGridEx_Basic(grdAssignDetail, m_dtAssignDetail, false, true, "",
                "stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu");
            Utility.SetDataSourceForDataGridEx_Basic(grdGoidichvu, m_dtGoidichvu, false, true, "",
                "stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu");
            Utility.SetDataSourceForDataGridEx_Basic(grdChiphithem, m_dtChiphithem, false, true, "",
                "stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu");

            m_dtDonthuocChitiet_View = m_dtDonthuoc.Clone();
            foreach (DataRow dr in m_dtDonthuoc.Rows)
            {
                dr["CHON"] = 0;
                DataRow[] drview
                    = m_dtDonthuocChitiet_View
                        .Select(KcbDonthuocChitiet.Columns.IdThuoc + "=" +
                                Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.IdThuoc], "-1")
                                + "AND " + KcbDonthuocChitiet.Columns.DonGia + "=" +
                                Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.DonGia], "-1")
                                + "AND " + KcbDonthuocChitiet.Columns.BnhanChitra + "=" +
                                Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.BnhanChitra], "-1")
                                + "AND " + KcbDonthuocChitiet.Columns.BhytChitra + "=" +
                                Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.BhytChitra], "-1")
                                + "AND " + KcbDonthuocChitiet.Columns.PhuThu + "=" +
                                Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.PhuThu], "-1")
                                + "AND " + KcbDonthuocChitiet.Columns.TuTuc + "=" +
                                Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.TuTuc], "-1")
                                + "AND " + KcbDonthuocChitiet.Columns.IdDonthuoc + "=" +
                                Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.IdDonthuoc], "-1")
                        );
                if (drview.Length <= 0)
                {
                    m_dtDonthuocChitiet_View.ImportRow(dr);
                }
                else
                {
                    drview[0][KcbDonthuocChitiet.Columns.SoLuong] =
                        Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong], 0) +
                        Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.SoLuong], 0);
                    drview[0]["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                                                   Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.DonGia]);
                    drview[0]["TT"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                                      (Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.DonGia]) +
                                       Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu]));
                    drview[0]["TT_BHYT"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                                           Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BhytChitra]);
                    drview[0]["TT_BN"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                                         (Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0) +
                                          Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu], 0));
                    drview[0]["TT_PHUTHU"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                                             Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu], 0);
                    drview[0]["TT_BN_KHONG_PHUTHU"] =
                        Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                        Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0);

                    drview[0][KcbDonthuocChitiet.Columns.SttIn] =
                        Math.Min(Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.SttIn], 0),
                            Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SttIn], 0));
                    m_dtDonthuocChitiet_View.AcceptChanges();
                }
            }

            Utility.SetDataSourceForDataGridEx_Basic(grdPresDetail, m_dtDonthuocChitiet_View, false, true, "",
                KcbDonthuocChitiet.Columns.SttIn);

            m_dtVTTHChitiet_View = m_dtVTTH.Clone();
            foreach (DataRow dr in m_dtVTTH.Rows)
            {
                dr["CHON"] = 0;
                DataRow[] drview
                    = m_dtVTTHChitiet_View
                        .Select(KcbDonthuocChitiet.Columns.IdThuoc + "=" +
                                Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.IdThuoc], "-1")
                                + "AND " + KcbDonthuocChitiet.Columns.DonGia + "=" +
                                Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.DonGia], "-1"));
                if (drview.Length <= 0)
                {
                    m_dtVTTHChitiet_View.ImportRow(dr);
                }
                else
                {
                    drview[0][KcbDonthuocChitiet.Columns.SoLuong] =
                        Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong], 0) +
                        Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.SoLuong], 0);
                    drview[0]["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                                                   Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.DonGia]);
                    drview[0]["TT"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                                      (Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.DonGia]) +
                                       Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu]));
                    drview[0]["TT_BHYT"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                                           Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BhytChitra]);
                    drview[0]["TT_BN"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                                         (Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0) +
                                          Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu], 0));
                    drview[0]["TT_PHUTHU"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                                             Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu], 0);
                    drview[0]["TT_BN_KHONG_PHUTHU"] =
                        Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                        Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0);

                    drview[0][KcbDonthuocChitiet.Columns.SttIn] =
                        Math.Min(Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.SttIn], 0),
                            Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SttIn], 0));
                    m_dtVTTHChitiet_View.AcceptChanges();
                }
            }
            //Old-->Utility.SetDataSourceForDataGridEx
            Utility.SetDataSourceForDataGridEx_Basic(grdVTTH, m_dtVTTHChitiet_View, false, true, "",
                KcbDonthuocChitiet.Columns.SttIn);

            ModifyCommmands();
        }


        private void frm_TonghopRavien_Load(object sender, EventArgs e)
        {
            try
            {
                _allowTextChanged = false;
                DataBinding.BindDataCombobox(cboKhoanoitru,
                    THU_VIEN_CHUNG.LaydanhsachKhoanoitruTheoBacsi(globalVariables.UserName,
                        Utility.Bool2byte(globalVariables.IsAdmin), 1),
                    DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong,
                    "---Khoa nội trú tổng hợp---", false);

                cboKhoanoitru.SelectedIndex = Utility.GetSelectedIndex(cboKhoanoitru,
                    globalVariablesPrivate.objKhoaphong.IdKhoaphong.ToString());
                if (cboKhoanoitru.SelectedIndex != 0 && cboKhoanoitru.Items.Count == 1) cboKhoanoitru.SelectedIndex = 0;
                ClearControl();
                lstVisibleColumns = Utility.GetVisibleColumns(grdAssignDetail);
                _allowTextChanged = true;
                m_blnHasLoaded = true;
                if (ma_luotkham != "")
                {
                    txtPatient_Code.Text = ma_luotkham;
                    TimKiemThongTin();
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                ModifyCommmands();
                m_blnHasLoaded = true;
                txtPatient_Code.Focus();
                txtPatient_Code.Select();
            }
        }


        private KcbLuotkham CreatePatientExam()
        {
            SqlQuery sqlQuery = new Select().From(KcbLuotkham.Schema)
                .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(malankham)
                .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(Utility.Int32Dbnull(txtPatient_ID.Text));
            var objPatientExam = sqlQuery.ExecuteSingle<KcbLuotkham>();
            return objPatientExam;
        }

        private void ClearControls(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is EditBox)
                {
                    ((EditBox) (control)).Clear();
                }
                else if (control is MaskedEditBox)
                {
                    control.Text = "";
                }
                else if (control is AutoCompleteTextbox)
                {
                    ((AutoCompleteTextbox) control)._Text = "";
                }
                else if (control is TextBox)
                {
                    ((TextBox) (control)).Clear();
                }
            }
        }

        private void ClearControl()
        {
            try
            {
                grdAssignDetail.DataSource = null;
                grdPresDetail.DataSource = null;
                grdVTTH.DataSource = null;
                grdPhieudieutri.DataSource = null;
                txtIdPhieudieutri.Text = "-1";
                txtGiuong.Clear();
                txtBuong.Clear();
                txtKhoanoitru.Clear();
                ClearControls(pnlThongtinBNKCB);
                ClearControls(pnlKetluan);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        ///     Lấy về thông tin bệnh nhân nội trú
        /// </summary>
        private void GetData()
        {
            try
            {
                objPhieudieutri = null;
                // Utility.SetMsg(lblMsg, "", false);
                string PatientCode = objLuotkham.MaLuotkham;
                malankham = PatientCode;
                long Patient_ID = objLuotkham.IdBenhnhan;
                objBenhnhan = KcbDanhsachBenhnhan.FetchByID(objLuotkham.IdBenhnhan);
                if (objLuotkham != null)
                {
                    ClearControl();
                    var objStaff =
                        new Select().From(DmucNhanvien.Schema)
                            .Where(DmucNhanvien.UserNameColumn)
                            .IsEqualTo(Utility.sDbnull(objLuotkham.NguoiKetthuc, ""))
                            .ExecuteSingle<DmucNhanvien>();
                    string TenNhanvien = objLuotkham.NguoiKetthuc;
                    if (objStaff != null)
                        TenNhanvien = objStaff.TenNhanvien;
                    pnlGoiDV.Enabled = true;
                    pnlVTTH.Enabled = true;
                    DataTable m_dtThongTin = _KCB_THAMKHAM.NoitruLaythongtinBenhnhan(objLuotkham.MaLuotkham,
                        Utility.Int32Dbnull(objLuotkham.IdBenhnhan, -1));

                    if (m_dtThongTin.Rows.Count > 0)
                    {
                        DataRow dr = m_dtThongTin.Rows[0];
                        if (dr != null)
                        {
                            dtInput_Date.Value = Convert.ToDateTime(dr[KcbLuotkham.Columns.NgayTiepdon]);
                            dtpNgaynhapvien.Value = objLuotkham.NgayNhapvien.Value;
                            txtGioitinh.Text = objBenhnhan.GioiTinh;
                            txtPatient_Name.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.TenBenhnhan], "");
                            txtPatient_ID.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.IdBenhnhan], "");
                            txtPatient_Code.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.MaLuotkham], "");
                            barcode.Data = malankham;
                            txtDiaChi.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.DiaChi], "");
                           // txtDiachiBHYT.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.DiachiBhyt], "");

                            txtObjectType_Name.Text = Utility.sDbnull(dr[DmucDoituongkcb.Columns.TenDoituongKcb], "");
                            txtSoBHYT.Text = string.Format("{0}-{1}{2}", Utility.sDbnull(dr[KcbLuotkham.Columns.MatheBhyt], ""),Utility.sDbnull(dr[KcbLuotkham.Columns.MaNoicapBhyt]),Utility.sDbnull(dr[KcbLuotkham.Columns.MaKcbbd], ""));
                            txtBHTT.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.PtramBhytGoc], "0");
                            dtpNgayhethanBHYT.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.NgayketthucBhyt],
                                globalVariables.SysDate.ToString("dd/MM/yyyy"));
                            txtTuoi.Text = Utility.sDbnull(Utility.Int32Dbnull(globalVariables.SysDate.Year) -
                                                           objBenhnhan.NgaySinh.Value.Year);
                            txtKhoanoitru.Text = Utility.sDbnull(dr["ten_khoanoitru"], "");
                            txtBuong.Text = Utility.sDbnull(dr["ten_buong"], "");
                            txtGiuong.Text = Utility.sDbnull(dr["ten_giuong"], "");
                            LaydanhsachPhieudieutri();
                            LayLichsuBuongGiuong();
                            LayLichsuTamung();
                            TinhtoanTongchiphi();
                        }
                    }
                }
                ModifyCommmands();
            }
            catch
            {
            }
            finally
            {
            }
        }

        private void LayLichsuTamung()
        {
            try
            {
                m_dtTamung = new KCB_THAMKHAM().NoitruTimkiemlichsuNoptientamung(objLuotkham.MaLuotkham,
                    (int) objLuotkham.IdBenhnhan, 0, -1, 1);
                Utility.SetDataSourceForDataGridEx_Basic(grdTamung, m_dtTamung, false, true, "1=1",
                    NoitruTamung.Columns.NgayTamung + " desc");
                grdTamung.MoveFirst();
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
        }

        private void LayLichsuBuongGiuong()
        {
            try
            {
                bool isAdmin = Utility.Coquyen("quyen_xemphieudieutricuabacsinoitrukhac")
                               ||
                               THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_CHOPHEPXEM_PHIEUDIEUTRI_CUABACSIKHAC", "0",
                                   false) == "1";
                isAdmin = isAdmin || !_khoanoitrutonghop;
                m_dtBuongGiuong = _KCB_THAMKHAM.NoitruTimkiemlichsuBuonggiuong(objLuotkham.MaLuotkham,
                    (int) objLuotkham.IdBenhnhan, isAdmin ? "-1" : Utility.sDbnull(cboKhoanoitru.SelectedValue, "0"));
                //Tính số lượng ngày nằm khoa cuối cùng nếu chưa xuất viện
                foreach (DataRow dr in m_dtBuongGiuong.Rows)
                {
                    if (Utility.Int32Dbnull(dr[NoitruPhanbuonggiuong.Columns.Id], 0) ==
                        Utility.Int32Dbnull(objLuotkham.IdRavien, 0))
                    {
                        NoitruPhanbuonggiuong objNoitruPhanbuonggiuong =
                            NoitruPhanbuonggiuong.FetchByID(Utility.Int64Dbnull(dr[NoitruPhanbuonggiuong.Columns.Id], 0));
                        if (objNoitruPhanbuonggiuong.NgayKetthuc == null)
                        {
                            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_TUDONG_TINHNGAYGIUONG", "0", false) == "1")
                            {
                                dr[NoitruPhanbuonggiuong.Columns.NgayKetthuc] = globalVariables.SysDate;
                                dr[NoitruPhanbuonggiuong.Columns.SoLuong] = THU_VIEN_CHUNG.SongayChuaRaVien(objNoitruPhanbuonggiuong.NgayVaokhoa, globalVariables.SysDate);
                                dr["thanh_tien"] = Utility.DecimaltoDbnull(dr[NoitruPhanbuonggiuong.Columns.DonGia], 0) *
                                                   Utility.DecimaltoDbnull(dr[NoitruPhanbuonggiuong.Columns.SoLuong], 0);
                            }
                            //else
                            //{
                                //dr[NoitruPhanbuonggiuong.Columns.NgayKetthuc] = globalVariables.SysDate;
                                //dr[NoitruPhanbuonggiuong.Columns.SoLuong] = 0;
                                //dr["thanh_tien"] = Utility.DecimaltoDbnull(dr[NoitruPhanbuonggiuong.Columns.DonGia], 0) *
                                //                   Utility.DecimaltoDbnull(dr[NoitruPhanbuonggiuong.Columns.SoLuong], 0);
                            //}
                          
                        }
                    }
                }
                m_dtBuongGiuong.AcceptChanges();
                Utility.SetDataSourceForDataGridEx_Basic(grdBuongGiuong, m_dtBuongGiuong, false, true, "1=1",
                    NoitruPhanbuonggiuong.Columns.NgayVaokhoa + " desc");
                grdBuongGiuong.MoveFirst();
            }
            catch (Exception ex)
            {
                log.Trace("Lỗi:"+ ex.Message);
            }
        }

        private void LaydanhsachPhieudieutri()
        {
            try
            {
                bool isAdmin = Utility.Coquyen("quyen_xemphieudieutricuabacsinoitrukhac")
                               ||
                               THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_CHOPHEPXEM_PHIEUDIEUTRI_CUABACSIKHAC", "0",
                                   false) == "1";
                isAdmin = isAdmin || !_khoanoitrutonghop;
                short idkhoanoitru = Utility.Int16Dbnull(cboKhoanoitru.SelectedValue, 0);
                    //Khoa nội trú tổng hợp để chuyển khoa
                if (!_khoanoitrutonghop) //Điều dưỡng tổng hợp trước khi ra viện
                    idkhoanoitru = -1;
                m_dtPhieudieutri = _KCB_THAMKHAM.NoitruTimkiemphieudieutriTheoluotkham(Utility.Bool2byte(isAdmin),
                    "01/01/1900",
                    objLuotkham.MaLuotkham, (int) objLuotkham.IdBenhnhan, idkhoanoitru.ToString(), -1);
                Utility.SetDataSourceForDataGridEx_Basic(grdPhieudieutri, m_dtPhieudieutri, false, true, "1=1",
                    NoitruPhieudieutri.Columns.NgayDieutri + " desc");
                grdPhieudieutri.MoveFirst();
            }
            catch (Exception ex)
            {
                log.Trace("Lỗi:" + ex.Message);
            }
        }


        private string GetTenBenh(string MaBenh)
        {
            string TenBenh = "";
            DataRow[] arrMaBenh =
                globalVariables.gv_dtDmucBenh.Select(string.Format(DmucBenh.Columns.MaBenh + "='{0}'", MaBenh));
            if (arrMaBenh.GetLength(0) > 0) TenBenh = Utility.sDbnull(arrMaBenh[0][DmucBenh.Columns.TenBenh], "");
            return TenBenh;
        }


        /// <summary>
        ///     hàm thực hiện viecj dóng form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        ///     hàm thực hiện việc dùng phím tắt in phiếu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_TonghopRavien_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
            if (e.Control && e.KeyCode == Keys.P)
            {
            }

            if (e.KeyCode == Keys.Escape) Close();

            if (e.Control && e.KeyCode == Keys.F5)
            {
                //splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;
            }
            //Keyvalue=49-->1
            if (e.KeyCode == Keys.F11 && PropertyLib._AppProperties.ShowActiveControl)
                Utility.ShowMsg(ActiveControl.Name);
            if (e.KeyCode == Keys.F6)
            {
                txtPatient_Code.SelectAll();
                txtPatient_Code.Focus();
                return;
            }
            if (e.KeyCode == Keys.F1)
            {
                Utility.Showhelps(GetType().Assembly.ManifestModule.Name.Replace(".DLL", "").Replace(".dll", ""), Name);
            }
        }


        private void grdAssignDetail_SelectionChanged(object sender, EventArgs e)
        {
            ChangeMenu(grdAssignDetail.CurrentRow);
            ShowResult();
        }

        private void ShowResult()
        {
            try
            {
                Int16 trangthaiChitiet =
                    Utility.Int16Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.TrangthaiChitiet), 0);
                Int16 coChitiet =
                    Utility.Int16Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.CoChitiet), 0);

                int idChitietdichvu =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdChitietdichvu), 0);
                int idDichvu =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdDichvu), 0);

                int idChitietchidinh =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdChitietchidinh), 0);
                int idChidinh =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdChidinh), 0);

                string maloaiDichvuCls =
                    Utility.sDbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.MaLoaidichvu), "XN");
                string maChidinh =
                    Utility.sDbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.MaChidinh),
                        "XN");
                string maBenhpham =
                    Utility.sDbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.MaBenhpham),
                        "XN");
                if (trangthaiChitiet < 3)
                    //0=Mới chỉ định;1=Đã chuyển CLS;2=Đang thực hiện;3= Đã có kết quả CLS;4=Đã xác nhận kết quả
                {
                    uiTabKqCls.Width = 0;
                    Application.DoEvents();
                }
                else
                {
                    if (maloaiDichvuCls == "XN")
                        pnlXN.BringToFront();
                    else
                        pnlXN.SendToBack();
                    if (PropertyLib._ThamKhamProperties.HienthiKetquaCLSTrongluoiChidinh)
                    {
                        if (coChitiet == 1 || maloaiDichvuCls != "XN")
                            uiTabKqCls.Width = PropertyLib._ThamKhamProperties.DorongVungKetquaCLS;
                        else
                            uiTabKqCls.Width = 0;
                    }
                    else
                    {
                        uiTabKqCls.Width = PropertyLib._ThamKhamProperties.DorongVungKetquaCLS;
                    }
                    if (coChitiet == 1)
                        Utility.ShowColumns(grdKetqua, lstKQCochitietColumns);
                    else
                        Utility.ShowColumns(grdKetqua, lstKQKhongchitietColumns);
                    //Lấy dữ liệu CLS
                    if (maloaiDichvuCls == "XN")
                    {
                        DataTable dt =
                            SPs.ClsTimkiemketquaXNChitiet(objLuotkham.MaLuotkham, maChidinh, maBenhpham, idChidinh,
                                coChitiet, idDichvu, idChitietdichvu).GetDataSet().Tables[0];
                        Utility.SetDataSourceForDataGridEx_Basic(grdKetqua, dt, true, true, "1=1",
                            "stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu");

                        Utility.focusCell(grdKetqua, KcbKetquaCl.Columns.KetQua);
                    }
                    else //XQ,SA,DT,NS
                    {
                        FillDynamicValues(idChitietdichvu, idChitietchidinh);
                    }
                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                log.Trace("Lỗi:" + ex.Message);
            }
        }

        private void FillDynamicValues(int idDichvuChitiet, int idchidinhchitiet)
        {
            try
            {
                pnlDynamicValues.Controls.Clear();

                DataTable dtData = clsHinhanh.GetDynamicFieldsValues(idDichvuChitiet, txtMauKQ.myCode, "", "", -1,
                    idchidinhchitiet);

                foreach (DataRow dr in dtData.Select("1=1", "Stt_hthi"))
                {
                    dr[DynamicValue.Columns.IdChidinhchitiet] = Utility.Int32Dbnull(idchidinhchitiet);
                    var ucTextSysparam = new ucDynamicParam(dr, true);
                    ucTextSysparam._ReadOnly = true;
                    ucTextSysparam.TabStop = true;
                    ucTextSysparam.TabIndex = 10 + Utility.Int32Dbnull(dr[DynamicField.Columns.Stt], 0);
                    ucTextSysparam.Init();
                    ucTextSysparam.Size = PropertyLib._DynamicInputProperties.DynamicSize;
                    ucTextSysparam.txtValue.Size = PropertyLib._DynamicInputProperties.TextSize;
                    ucTextSysparam.lblName.Size = PropertyLib._DynamicInputProperties.LabelSize;
                    pnlDynamicValues.Controls.Add(ucTextSysparam);
                }
            }
            catch (Exception ex)
            {
                log.Trace("Lỗi:" + ex.Message);
            }
        }

        private void TimKiemThongTin()
        {
            var dtPatient = new DataTable();
            string patientCode = Utility.AutoFullPatientCode(txtPatient_Code.Text);
            ClearControl();
            dtPatient = _KCB_THAMKHAM.TimkiemBenhnhan(txtPatient_Code.Text,
                -1, 1, 0);

            DataRow[] arrPatients = dtPatient.Select(KcbLuotkham.Columns.MaLuotkham + "='" + patientCode + "'");
            if (arrPatients.GetLength(0) <= 0)
            {
                if (dtPatient.Rows.Count > 1)
                {
                    var frm = new frm_DSACH_BN_TKIEM("ALL");
                    frm.MaLuotkham = txtPatient_Code.Text;
                    frm.dtPatient = dtPatient;
                    frm.ShowDialog();
                    if (!frm.has_Cancel)
                    {
                        txtPatient_Code.Text = frm.MaLuotkham;
                    }
                }
                else
                    txtPatient_Code.Text = patientCode;
            }
            else
            {
                txtPatient_Code.Text = patientCode;
            }
            objLuotkham = Utility.getKcbLuotkham(txtPatient_Code.Text);

            if (objLuotkham != null)
            {
                cmdInphoiBHYT.Visible = THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb);
                _allowTextChanged = false;
                GetData();
                txtPatient_Code.SelectAll();
                if (objLuotkham.TrangthaiNoitru == 0)
                {
                    Utility.ShowMsg(
                        "Bệnh nhân chưa nhập viện nội trú nên bạn không thể lập phiếu ra viện được. Đề nghị bạn kiểm tra lại");
                }
                //Kiểm tra lần nhập viện hoặc chuyển khoa gần nhất phải được phân buồng giường trước khi ra viện
                //NoitruPhanbuonggiuong objNoitruPhanbuonggiuong =
                //    NoitruPhanbuonggiuong.FetchByID(objLuotkham.IdRavien);
                //bool isValid = Utility.Int16Dbnull(objNoitruPhanbuonggiuong.IdBuong, 0) > 0 &&
                //               Utility.Int16Dbnull(objNoitruPhanbuonggiuong.IdBuong, 0) > 0;
                //if (!isValid)
                //{
                //    Utility.ShowMsg(
                //        "Hệ thống phát hiện Bệnh nhân chưa được phân buồng giường cho lần chuyển khoa gần nhất nên bạn không thể tổng hợp ra viện được.");
                //}
                //cmdRavien.Enabled = cmdTonghop.Enabled = isValid && objLuotkham.TrangthaiNoitru > 0;
            }
            else
            {
                string sPatientTemp = txtPatient_Code.Text;
                ClearControl();
                ModifyCommmands();
                txtPatient_Code.Text = sPatientTemp;
                Utility.ShowMsg("Không tìm được Bệnh nhân nào có mã lượt khám " + txtPatient_Code.Text);
                txtPatient_Code.SelectAll();
                cmdRavien.Enabled = cmdTonghop.Enabled = false;
            }
        }
        private void txtPatient_Code_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
               
                if (e.KeyCode == Keys.Enter)
                {
                    TimKiemThongTin();
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy thông tin bệnh nhân");
            }
            finally
            {
                _allowTextChanged = true;
            }
        }

        public static ReportDocument GetReport(string fileName) //, ref string ErrMsg)
        {
            try
            {
                var crpt = new ReportDocument();
                if (File.Exists(fileName))
                {
                    crpt.Load(fileName);
                }
                else
                {
                    return null;
                }
                return crpt;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi nạp báo cáo " + fileName + "-->\n" + ex.Message);
                //ErrMsg = ex.Message;
                return null;
            }
        }

        #region "chỉ định cận lâm sàng"

        private DataTable m_dtReportAssignInfo;

        /// <summary>
        ///     hàm thực hiện việc update thông tin của cell update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdAssignDetail_CellUpdated(object sender, ColumnActionEventArgs e)
        {
        }


        private bool IsValidGoidichvu()
        {
            bool b_Cancel = false;
            if (grdGoidichvu.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn một bản ghi thực hiện xóa gói dịch vụ", "Thông báo",
                    MessageBoxIcon.Warning);
                grdGoidichvu.Focus();
                return false;
            }

            foreach (GridEXRow gridExRow in grdGoidichvu.GetCheckedRows())
            {
                int AssignDetail_ID =
                    Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value,
                        -1);
                SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                    .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(AssignDetail_ID)
                    .And(KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    b_Cancel = true;
                    break;
                }
            }
            if (b_Cancel)
            {
                Utility.ShowMsg("Gói dịch vụ bạn chọn đã thanh toán nên không thể xóa. Mời bạn chọn lại !", "Thông báo",
                    MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private bool IsValidChiphithem()
        {
            bool b_Cancel = false;
            if (grdChiphithem.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn một bản ghi thực hiện xóa chi phí thêm", "Thông báo",
                    MessageBoxIcon.Warning);
                grdChiphithem.Focus();
                return false;
            }

            foreach (GridEXRow gridExRow in grdChiphithem.GetCheckedRows())
            {
                int AssignDetail_ID =
                    Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value,
                        -1);
                SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                    .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(AssignDetail_ID)
                    .And(KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    b_Cancel = true;
                    break;
                }
            }
            if (b_Cancel)
            {
                Utility.ShowMsg("Chi phí thêm bạn chọn đã thanh toán nên không thể xóa. Mời bạn chọn lại !", "Thông báo",
                    MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void XoaGoidichvu()
        {
            foreach (GridEXRow gridExRow in grdGoidichvu.GetCheckedRows())
            {
                int AssignDetail_ID =
                    Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value,
                        -1);
                int id_chidinh = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChidinh].Value,
                    -1);
                _KCB_CHIDINH_CANLAMSANG.GoidichvuXoachitiet(AssignDetail_ID);
                gridExRow.Delete();
                m_dtGoidichvu.AcceptChanges();
            }
        }

        private void XoaChiphithem()
        {
            foreach (GridEXRow gridExRow in grdChiphithem.GetCheckedRows())
            {
                int AssignDetail_ID =
                    Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value,
                        -1);
                int id_chidinh = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChidinh].Value,
                    -1);
                _KCB_CHIDINH_CANLAMSANG.GoidichvuXoachitiet(AssignDetail_ID);
                gridExRow.Delete();
                m_dtChiphithem.AcceptChanges();
            }
        }

        private bool CheckPatientSelected()
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg(
                    "Bạn phải chọn Bệnh nhân trước khi thực hiện các công việc chỉ định Thăm khám, CLS, Kê đơn");
                return false;
            }
            if (objPhieudieutri == null)
            {
                Utility.ShowMsg(
                    "Bạn phải chọn Phiếu chỉ định trước khi thực hiện các công việc chỉ định Thăm khám, CLS, Kê đơn");
                return false;
            }
            return true;
        }

        private decimal GetTotalDatatable(DataTable dataTable, string FiledName, string Filer)
        {
            return Utility.DecimaltoDbnull(dataTable.Compute("SUM(" + FiledName + ")", Filer), 0);
        }

        #endregion

        #region "khởi tạo các sụ kienj thông tin của thuốc"

        private bool ExistsDonThuoc()
        {
            try
            {
                string _kenhieudon = THU_VIEN_CHUNG.Laygiatrithamsohethong("KE_NHIEU_DON", "N", true);
                var lstPres =
                    new Select()
                        .From(KcbDonthuoc.Schema)
                        .Where(KcbDonthuoc.MaLuotkhamColumn).IsEqualTo(Utility.sDbnull(objLuotkham.MaLuotkham)).
                        ExecuteAsCollection<KcbDonthuocCollection>();

                IEnumerable<KcbDonthuoc> lstPres1 = from p in lstPres
                    where p.IdPhieudieutri == objPhieudieutri.IdPhieudieutri
                    select p;
                if (objLuotkham.MaDoituongKcb == "BHYT")
                {
                    if (_kenhieudon == "Y" && lstPres1.Count() <= 0) //Được phép kê mỗi phòng khám 1 đơn thuốc
                        return false;
                    if (_kenhieudon == "N" && lstPres.Count > 0 && lstPres1.Count() <= 0)
                        //Cảnh báo ko được phép kê đơn tiếp
                    {
                        Utility.ShowMsg(
                            "Chú ý: Bệnh nhân này thuộc đối tượng BHYT và đã được kê đơn thuốc tại phòng khám khác. Bạn cần trao đổi với bộ phận khác để được cấu hình kê đơn thuốc tại nhiều phòng khác khác nhau với đối tượng BHYT này",
                            "Thông báo");
                        return false;
                    }
                }
                else
                    //Bệnh nhân dịch vụ-->cho phép kê 1 đơn nếu đơn chưa thanh toán và nhiều đơn nếu các đơn trước đã thanh toán
                {
                    if (lstPres1.Count() > 0)
                        if (lstPres1.FirstOrDefault().TrangthaiThanhtoan == 0) //Chưa thanh toán-->Cần sửa đơn
                            return true;
                        else //Đã thanh toán-->Cho phép thêm đơn mới
                            return false;
                    return false;
                }
                return lstPres.Count > 0;
                //Tạm thời rem lại do vẫn có BN kê được >1 đơn thuốc
                //var query = from thuoc in grdPresDetail.GetDataRows().AsEnumerable()
                //                    select thuoc;
                //if (query.Any()) return true;
                //else return false;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi kiểm tra số lượng đơn thuốc của lần khám\n" + ex.Message);
                return false;
            }
        }


        private List<int> GetIdChitietVTTH(int IdDonthuoc, int id_thuoc, decimal don_gia, ref string s)
        {
            DataRow[] arrDr =
                m_dtVTTH.Select(KcbDonthuocChitiet.Columns.IdDonthuoc + "=" + IdDonthuoc + " AND " +
                                KcbDonthuocChitiet.Columns.IdThuoc + "=" + id_thuoc
                                + "AND " + KcbDonthuocChitiet.Columns.DonGia + "=" + don_gia);
            if (arrDr.Length > 0)
            {
                IEnumerable<string> p1 = (from q in arrDr.AsEnumerable()
                    select Utility.sDbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).Distinct();
                s = string.Join(",", p1.ToArray());
                IEnumerable<int> p = (from q in arrDr.AsEnumerable()
                    select Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).Distinct();
                return p.ToList();
            }
            return new List<int>();
        }

        private List<int> GetIdChitietVTTHtronggoi(int IdDonthuoc, int id_thuoc, decimal don_gia, ref string s)
        {
            DataRow[] arrDr =
                m_dtVTTH_tronggoi.Select(KcbDonthuocChitiet.Columns.IdDonthuoc + "=" + IdDonthuoc + " AND " +
                                         KcbDonthuocChitiet.Columns.IdThuoc + "=" + id_thuoc
                                         + "AND " + KcbDonthuocChitiet.Columns.DonGia + "=" + don_gia);
            if (arrDr.Length > 0)
            {
                IEnumerable<string> p1 = (from q in arrDr.AsEnumerable()
                    select Utility.sDbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).Distinct();
                s = string.Join(",", p1.ToArray());
                IEnumerable<int> p = (from q in arrDr.AsEnumerable()
                    select Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).Distinct();
                return p.ToList();
            }
            return new List<int>();
        }

        private List<int> GetIdChitiet(int IdDonthuoc, int id_thuoc, decimal don_gia, ref string s)
        {
            DataRow[] arrDr =
                m_dtDonthuoc.Select(KcbDonthuocChitiet.Columns.IdDonthuoc + "=" + IdDonthuoc + " AND " +
                                    KcbDonthuocChitiet.Columns.IdThuoc + "=" + id_thuoc
                                    + "AND " + KcbDonthuocChitiet.Columns.DonGia + "=" + don_gia);
            if (arrDr.Length > 0)
            {
                IEnumerable<string> p1 = (from q in arrDr.AsEnumerable()
                    select Utility.sDbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).Distinct();
                s = string.Join(",", p1.ToArray());
                IEnumerable<int> p = (from q in arrDr.AsEnumerable()
                    select Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).Distinct();
                return p.ToList();
            }
            return new List<int>();
        }

        private void XoaVTTHKhoiBangDulieu_trongoi(List<int> lstIdChitietDonthuoc)
        {
            try
            {
                DataRow[] p = (from q in m_dtVTTH.Select("1=1").AsEnumerable()
                    where
                        lstIdChitietDonthuoc.Contains(
                            Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc]))
                    select q).ToArray<DataRow>();
                for (int i = 0; i <= p.Length - 1; i++)
                    m_dtVTTH_tronggoi.Rows.Remove(p[i]);
                m_dtVTTH_tronggoi.AcceptChanges();
            }
            catch
            {
            }
        }

        #endregion

        #region "Xử lý tác vụ của phần lưu thông tin "

        private void cmdSave_Click(object sender, EventArgs e)
        {
        }


        private void TinhtoanTongchiphi()
        {
                DataSet dsData = SPs.NoitruTongchiphi(objLuotkham.MaLuotkham, (int) objLuotkham.IdBenhnhan).GetDataSet();
                decimal Tong_CLS = Utility.DecimaltoDbnull(dsData.Tables[0].Compute("SUM(TT)", "1=1"));
                decimal Tong_BN_TT = Utility.DecimaltoDbnull(dsData.Tables[0].Compute("SUM(TT_BN_TUTUC)", "1=1"));
                decimal Tong_Thuoc = Utility.DecimaltoDbnull(dsData.Tables[1].Compute("SUM(TT)", "1=1"));
                decimal Tong_VTTH = Utility.DecimaltoDbnull(dsData.Tables[2].Compute("SUM(TT)", "1=1"));
                decimal Tong_Giuong = Utility.DecimaltoDbnull(dsData.Tables[3].Compute("SUM(TT)", "1=1"));
                decimal Tong_Goi = Utility.DecimaltoDbnull(dsData.Tables[4].Compute("SUM(TT)", "1=1"));
                decimal Tong_Tamung = Utility.DecimaltoDbnull(m_dtTamung.Compute("SUM(so_tien)", "1=1"));
                decimal Tong_Chiphithem = Utility.DecimaltoDbnull(dsData.Tables[5].Compute("SUM(TT)", "1=1"));
                Tong_BN_TT = Tong_BN_TT + Tong_Chiphithem;
                decimal BH_Tong_CLS = Utility.DecimaltoDbnull(dsData.Tables[0].Compute("SUM(TT_BN)", "1=1"));
                decimal BH_Tong_Thuoc = Utility.DecimaltoDbnull(dsData.Tables[1].Compute("SUM(TT_BN)", "1=1"));
                decimal BH_Tong_VTTH = Utility.DecimaltoDbnull(dsData.Tables[2].Compute("SUM(TT_BN)", "1=1"));
                decimal BH_Tong_Giuong = Utility.DecimaltoDbnull(dsData.Tables[3].Compute("SUM(TT_BN)", "1=1"));
                decimal BH_Tong_Goi = Utility.DecimaltoDbnull(dsData.Tables[4].Compute("SUM(TT_BN)", "1=1"));

                decimal BH_Tong_chiphi = BH_Tong_CLS + BH_Tong_Thuoc + BH_Tong_Giuong + BH_Tong_Goi + BH_Tong_VTTH;

                decimal Tong_chiphi = Tong_Chiphithem + Tong_CLS + Tong_Thuoc + Tong_Giuong + Tong_Goi + Tong_VTTH;
                decimal Tong_BN_Tra = BH_Tong_chiphi + Tong_BN_TT;

                decimal BHYT_ChiTra = Tong_chiphi - Tong_BN_Tra;
                decimal BN_Nop = Tong_BN_Tra -  Tong_Tamung;
                lblChiphithem.Text = String.Format(Utility.FormatDecimal(),
                    Convert.ToDecimal(Tong_Chiphithem.ToString()));
                lblCLS.Text = String.Format(Utility.FormatDecimal(), Convert.ToDecimal(Tong_CLS.ToString()));
                lblThuoc.Text = String.Format(Utility.FormatDecimal(), Convert.ToDecimal(Tong_Thuoc.ToString()));
                lblVTTH.Text = String.Format(Utility.FormatDecimal(), Convert.ToDecimal(Tong_VTTH.ToString()));
                lblBuonggiuong.Text = String.Format(Utility.FormatDecimal(), Convert.ToDecimal(Tong_Giuong.ToString()));
                lblDichvu.Text = String.Format(Utility.FormatDecimal(), Convert.ToDecimal(Tong_Goi.ToString()));
                lblBHYTChiTra.Text = String.Format(Utility.FormatDecimal(), Convert.ToDecimal(BHYT_ChiTra.ToString()));
                lblTongChiphi.Text = String.Format(Utility.FormatDecimal(), Convert.ToDecimal(Tong_chiphi.ToString()));
                lblTongtamung.Text = String.Format(Utility.FormatDecimal(), Convert.ToDecimal(Tong_Tamung.ToString()));
                lblTuTuc.Text = String.Format(Utility.FormatDecimal(), Convert.ToDecimal(Tong_BN_TT.ToString()));
                lblBNChiTra.Text = String.Format(Utility.FormatDecimal(), Convert.ToDecimal(Tong_BN_Tra.ToString()));
                lblBNNop.Text = String.Format(Utility.FormatDecimal(), Convert.ToDecimal(BN_Nop.ToString())); 
                lblChiphithem.Text = Utility.DoTrim(lblChiphithem.Text) == "" ? "0" : lblChiphithem.Text;
                lblCLS.Text = Utility.DoTrim(lblCLS.Text) == "" ? "0" : lblCLS.Text;
                lblThuoc.Text = Utility.DoTrim(lblThuoc.Text) == "" ? "0" : lblThuoc.Text;
                lblVTTH.Text = Utility.DoTrim(lblVTTH.Text) == "" ? "0" : lblVTTH.Text;
                lblBuonggiuong.Text = Utility.DoTrim(lblBuonggiuong.Text) == "" ? "0" : lblBuonggiuong.Text;
                lblDichvu.Text = Utility.DoTrim(lblDichvu.Text) == "" ? "0" : lblDichvu.Text;
                lblTongChiphi.Text = Utility.DoTrim(lblTongChiphi.Text) == "" ? "0" : lblTongChiphi.Text;
                lblTongtamung.Text = Utility.DoTrim(lblTongtamung.Text) == "" ? "0" : lblTongtamung.Text;

                string canhbaotamung = THU_VIEN_CHUNG.Canhbaotamung(objLuotkham, dsData, m_dtTamung);
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_CANHBAOTAMUNG_PHIEUDIEUTRI", "1", false) == "1")
                {
                    if (canhbaotamung.Trim() != "")
                        ShowErrorStatus(canhbaotamung);
                    else
                    {
                        if (ucError1 != null)
                        {
                            UIAction._Visible(ucError1, false);
                            ucError1.Reset();
                        }
                    }
                }
                Utility.SetMsg(lblChenhlech,
                    String.Format(Utility.FormatDecimal(), Convert.ToDecimal((Tong_Tamung - Tong_chiphi).ToString())),
                    Tong_Tamung - Tong_chiphi > 0 ? false : true);
                lblChenhlech.Text = Utility.DoTrim(lblChenhlech.Text) == "" ? "0" : lblChenhlech.Text;
        }

        private void HideErrorStatus()
        {
            try
            {
                UIAction._Visible(ucError1, false);
                ucError1.Reset();
            }
            catch
            {
            }
        }

        private void ShowErrorStatus(string msg)
        {
            try
            {
                UIAction._Visible(ucError1, true);
                ucError1.Reset();
                ucError1.Start(msg);
            }
            catch
            {
            }
        }

        #endregion

        private void pnlPatientInfor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frm_TonghopRavien_FormClosed(object sender, FormClosedEventArgs e)
        {
            bCancel = false;
        }
        DataTable _mDtKhoanoitru;
        private bool isValidData_Phanbuonggiuong()
        {
            string maluotkham = Utility.sDbnull(txtPatient_Code.Text,"");
            int idBenhnhan = Utility.Int32Dbnull(txtPatient_ID.Text,-1);
            int idKhoanoitru = Utility.Int32Dbnull(cboKhoanoitru.SelectedValue);
            KcbLuotkham kcbLuotkham = Utility.getKcbLuotkham(idBenhnhan, maluotkham);
            if (kcbLuotkham == null)
            {
                Utility.ShowMsg("Không lấy được thông tin Bệnh nhân. Đề nghị bạn cần chọn ít nhất 1 Bệnh nhân trên lưới");
                return false;
            }
           
            if (Utility.Int32Dbnull(kcbLuotkham.TrangthaiNoitru, -1) == 4)
            {
                Utility.ShowMsg("Bệnh nhân đã được xác nhận dữ liệu nội trú để chuyển thanh toán nên không thể Hủy giường. Đề nghị bạn kiểm tra lại");
                return false;
            }
            if (kcbLuotkham.TrangthaiNoitru == 5)
            {
                Utility.ShowMsg("Bệnh nhân đã được duyệt thanh toán nội trú nên bạn không thể sửa lại trạng thái xác nhận dữ liệu nội trú");
                return false;
            }
            if (Utility.Byte2Bool(kcbLuotkham.TthaiThanhtoannoitru) || kcbLuotkham.TrangthaiNoitru == 6)
            {
                Utility.ShowMsg("Bệnh nhân đã được thanh toán nội trú nên bạn không thể sửa lại trạng thái xác nhận dữ liệu nội trú");
                return false;
            }
            if (kcbLuotkham.IdKhoanoitru != idKhoanoitru)
            {
                Utility.ShowMsg("Bạn không được phân buồng giường cho Bệnh nhân của khoa khác. Đề nghị bạn kiểm tra lại");
                return false;
            }
            int id = Utility.Int32Dbnull(grdBuongGiuong.GetValue(NoitruPhanbuonggiuong.Columns.Id));
             _noitruPhanbuonggiuong = new Select().From<NoitruPhanbuonggiuong>()
                .Where(NoitruPhanbuonggiuong.Columns.Id).IsEqualTo(id).ExecuteSingle<NoitruPhanbuonggiuong>();
            return true;
        }

        private NoitruPhanbuonggiuong _noitruPhanbuonggiuong;
        private void mnughepgiuong_Click(object sender, EventArgs e)
        {
            if (!isValidData_Phanbuonggiuong()) return;
           
            if (_noitruPhanbuonggiuong != null)
            {
                var frm = new frm_phanbuonggiuong
                {
                    txtPatientDept_ID = { Text = Utility.sDbnull(_noitruPhanbuonggiuong.Id) },
                    ObjPhanbuonggiuong = _noitruPhanbuonggiuong
                };
                frm.ShowDialog();
                if (!frm.BCancel)
                {
                  LayLichsuBuongGiuong();
                }
            }
        }

        private void tabDiagInfo_SelectedTabChanged(object sender, Janus.Windows.UI.Tab.TabEventArgs e)
        {
            if (tabDiagInfo.SelectedTab.Name == "tabPageCauhinh")
            {
                mnughepgiuong.Visible = true;
            }
            else
            {
                mnughepgiuong.Visible = false;
            }
        }

        private void cmdconfig2_Click(object sender, EventArgs e)
        {
            var properties = new frm_Properties(PropertyLib._NoitruProperties);
            properties.ShowDialog();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void txtGiuong_TextChanged(object sender, EventArgs e)
        {

        }
    }
}