﻿using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using C1.C1Excel;
using CrystalDecisions.CrystalReports.Engine;
using Janus.Windows.GridEX;
using SubSonic;
using VNS.HIS.DAL;
using VNS.Libs;
using VNS.Properties;

namespace VNS.HIS.UI.DANHMUC
{
    public partial class frm_qhe_doituong_dichvuCls : Form
    {
        private readonly DataTable m_dtObjectRelationService = new DataTable();
        private readonly DataTable m_dtReportObjectType = new DataTable();
        private bool CLS_GIATHEO_KHOAKCB;
        private int ObjectType_ID = -1;
        private int ServiceDetail_ID = -1;
        private int ServiceObject_Type_ID = -1;
        private int Service_ID = -1;
        private string _rowFilter = "1=1";
        private ActionResult actionResult = ActionResult.Error;
        private DataSet ds = new DataSet();
        private DataTable dt_KhoaThucHien;
        private Query m_Query = QheDoituongDichvucl.CreateQuery();
        private bool m_blnLoaded;
        private DataTable m_dtDataDetailService = new DataTable();
        private DataTable m_dtObjectType = new DataTable();
        private DataTable m_dtObjectTypeDetailService = new DataTable();
        private DataTable m_dtServiceList = new DataTable();
        private DataTable m_dtServiceTypeList = new DataTable();
        private string rowFilters = "1=1";
        private DataTable v_DataPrint = new DataTable();
        private int v_ObjectType_ID = -1;
        private int v_ObjectType_Id = -1;

        /// <summary>
        ///     HAM THUC HIEN DICH VU, QUAN HE DOI TUONG -DICH VU
        /// </summary>
        private int v_ObjectType_Service_ID = -1;

        private int v_ServiceDetail_ID = -1;

        public frm_qhe_doituong_dichvuCls()
        {
            InitializeComponent();
            InitEvents();
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            KeyPreview = true;
        }

        private void InitEvents()
        {
            cboService.SelectedIndexChanged += cboService_SelectedIndexChanged;
            cmdAdd.Click += cmdAdd_Click;
            cmdDelete.Click += cmdDelete_Click;
            cmdSaveObjectAll.Click += cmdSaveObjectAll_Click;
            cmdDetailDeleteAll.Click += cmdDetailDeleteAll_Click;
            cmdThemMoi.Click += cmdThemMoi_Click;
            cmdCapNhap.Click += cmdCapNhap_Click;
            cmdPrint.Click += cmdPrint_Click;
            cmdPrintRelationObject.Click += cmdPrintRelationObject_Click;
            cmdExportExcel.Click += cmdExportExcel_Click;
            cmdClose.Click += cmdClose_Click;
            contextMenuStrip1.Opening += contextMenuStrip1_Opening;
            cmdUpdate.Click += cmdUpdate_Click;
            cboKhoaTH.SelectedIndexChanged += cboKhoaTH_SelectedIndexChanged;
            chkExpand.CheckedChanged += chkExpand_CheckedChanged;
            cmDeteleServiceDetail.Click += cmDeteleServiceDetail_Click_1;
            grdObjectTypeList.SelectionChanged += grdObjectTypeList_SelectionChanged;
            Load += frm_qhe_doituong_dichvuCls_Load;
            KeyDown += frm_qhe_doituong_dichvuCls_KeyDown;
            grdList.ApplyingFilter += grdList_ApplyingFilter;
            grdList.SelectionChanged += grdList_SelectionChanged;

            cmdCauhinhgia.Click += cmdCauhinhgia_Click;
            optQhe_tatca.CheckedChanged += optQhe_tatca_CheckedChanged;
            optCoQhe.CheckedChanged += optQhe_tatca_CheckedChanged;
            optKhongQhe.CheckedChanged += optQhe_tatca_CheckedChanged;

            optTatca.CheckedChanged += optQhe_tatca_CheckedChanged;
            optHieuluc.CheckedChanged += optQhe_tatca_CheckedChanged;
            optHethieuluc.CheckedChanged += optQhe_tatca_CheckedChanged;
        }


        private void optQhe_tatca_CheckedChanged(object sender, EventArgs e)
        {
            cboService_SelectedIndexChanged(cboService, e);
        }

        private void cmdCauhinhgia_Click(object sender, EventArgs e)
        {
            var _Properties = new frm_Properties(PropertyLib._QheGiaCLSProperties);
            _Properties.ShowDialog();
        }

        private void cboService_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!m_blnLoaded) return;
                SqlQuery _SqlQuery = new Select().From(VDmucDichvuclsChitiet.Schema);
                if (Utility.Int32Dbnull(cboService.SelectedValue, -1) > -1)
                    _SqlQuery.Where(VDmucDichvuclsChitiet.Columns.IdDichvu)
                        .IsEqualTo(Utility.Int32Dbnull(cboService.SelectedValue, -1));
                if (_SqlQuery.HasWhere)
                {
                    if (!optTatca.Checked)
                        _SqlQuery.And(VDmucDichvuclsChitiet.Columns.TrangThai).IsEqualTo(optHieuluc.Checked ? 1 : 0);
                    if (!optQhe_tatca.Checked)
                        _SqlQuery.And(VDmucDichvuclsChitiet.Columns.CoQhe).IsEqualTo(optCoQhe.Checked ? 1 : 0);
                }
                else
                {
                    if (!optTatca.Checked)
                        _SqlQuery.Where(VDmucDichvuclsChitiet.Columns.TrangThai).IsEqualTo(optHieuluc.Checked ? 1 : 0);
                    if (!optQhe_tatca.Checked)
                        _SqlQuery.And(VDmucDichvuclsChitiet.Columns.CoQhe).IsEqualTo(optCoQhe.Checked ? 1 : 0);
                }
                m_dtDataDetailService =
                    _SqlQuery.OrderAsc(VDmucDichvuclsChitiet.Columns.SttHthiLoaidvu,
                        VDmucDichvuclsChitiet.Columns.SttHthiDichvu, VDmucDichvuclsChitiet.Columns.SttHthi)
                        .ExecuteDataSet()
                        .Tables[0];
                Utility.SetDataSourceForDataGridEx(grdList, m_dtDataDetailService, true, true, "1=1",
                    "stt_hthi_dichvu,stt_hthi_chitiet," + DmucDichvuclsChitiet.Columns.TenChitietdichvu);
            }
            catch
            {
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ModifyCommand1()
        {
            cmdDetailDeleteAll.Enabled = grdQhe.RowCount > 0;
            cmdDelete.Enabled = grdQhe.RowCount > 0;
            cmdSaveObjectAll.Enabled = grdQhe.RowCount > 0;
        }


        private void grdList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!m_blnLoaded) return;
                if (grdList.CurrentRow != null && grdList.CurrentRow.RowType == RowType.Record)
                {
                    Service_ID =
                        Utility.Int32Dbnull(grdList.CurrentRow.Cells[DmucDichvuclsChitiet.Columns.IdDichvu].Value,
                            -1);
                    ServiceDetail_ID =
                        Utility.Int32Dbnull(
                            grdList.CurrentRow.Cells[DmucDichvuclsChitiet.Columns.IdChitietdichvu].Value,
                            -1);
                    _rowFilter = DmucDichvuclsChitiet.Columns.IdChitietdichvu + "=" + ServiceDetail_ID;
                    m_dtObjectTypeDetailService = new Select().From(VQheDoituongDichvucl.Schema)
                        .Where(VQheDoituongDichvucl.Columns.IdChitietdichvu)
                        .IsEqualTo(ServiceDetail_ID)
                        .OrderAsc(VQheDoituongDichvucl.Columns.SttHthi)
                        .ExecuteDataSet()
                        .Tables[0];
                    if (!m_dtObjectTypeDetailService.Columns.Contains("IsNew"))
                        m_dtObjectTypeDetailService.Columns.Add("IsNew", typeof (int));
                    if (!m_dtObjectTypeDetailService.Columns.Contains("CHON"))
                        m_dtObjectTypeDetailService.Columns.Add("CHON", typeof (int));

                    rowFilters = QheDoituongDichvucl.Columns.MaKhoaThuchien + " ='" +
                                 Utility.sDbnull(cboKhoaTH.SelectedValue, "") + "'";
                    Utility.SetDataSourceForDataGridEx(grdQhe, m_dtObjectTypeDetailService, true, true, rowFilters, "");
                }
                ModifyCommand();
                ModifyCommand1();
            }
            catch
            {
            }
        }

        /// <summary>
        ///     hàm thực hiện xóa nhiều bản ghi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDetailDeleteAll_Click(object sender, EventArgs e)
        {
            GridEXRow[] ArrCheckList = grdQhe.GetCheckedRows();
            if (ArrCheckList.Length <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn một bản ghi thực hiện xóa", "Thông báo");
                grdQhe.Focus();
                return;
            }
            string strLength = string.Format("Bạn có muốn xoá {0} bản ghi không", ArrCheckList.Length);
            if (Utility.AcceptQuestion(strLength, "Thông báo", true))
            {
                foreach (GridEXRow drv in ArrCheckList)
                {
                    new Delete().From(QheDoituongDichvucl.Schema)
                        .Where(QheDoituongDichvucl.Columns.MaDoituongKcb)
                        .IsEqualTo(Utility.sDbnull(drv.Cells[DmucDoituongkcb.Columns.MaDoituongKcb].Value, "-1"))
                        .And(QheDoituongDichvucl.Columns.IdChitietdichvu).IsEqualTo(ServiceDetail_ID)
                        .And(QheDoituongDichvucl.Columns.MaKhoaThuchien)
                        .IsEqualTo(Utility.sDbnull(cboKhoaTH.SelectedValue, "ALL"))
                        .Execute();
                    drv.Delete();
                    grdQhe.UpdateData();
                    grdQhe.Refresh();
                }
                m_dtObjectTypeDetailService.AcceptChanges();
                ModifyCommand1();
            }
            ModifyCommand();
        }

        /// <summary>
        ///     xóa thông tin của bản ghi hiện thời
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdQhe.CurrentRow != null)
                {
                    if (Utility.AcceptQuestion("Bạn có muốn xoá thông tin bản ghi đang chọn không", "Thông báo", true))
                    {
                        if (grdQhe.CurrentRow.Cells["IsNew"].Value.ToString() == "1")
                        {
                            grdQhe.CurrentRow.Delete();
                            grdQhe.UpdateData();
                            grdQhe.Refresh();
                        }
                        else
                        {
                            new Delete().From(QheDoituongDichvucl.Schema)
                                .Where(QheDoituongDichvucl.Columns.IdQuanhe).IsEqualTo(
                                    grdQhe.CurrentRow.Cells[QheDoituongDichvucl.Columns.IdQuanhe].Value).
                                Execute();
                            grdQhe.CurrentRow.Delete();
                            grdQhe.UpdateData();
                            grdQhe.Refresh();
                        }
                        m_dtObjectTypeDetailService.AcceptChanges();
                    }
                }
            }
            catch
            {
            }
            finally
            {
                ModifyCommand();
            }
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            if (grdList.RowCount <= 0) return;
            var frm = new frm_ChonDoituongKCB();
            frm._enObjectType = enObjectType.DichvuCLS;
            frm.m_dtObjectDataSource = m_dtObjectTypeDetailService;
            frm.Original_Price =
                Utility.DecimaltoDbnull(grdList.CurrentRow.Cells[DmucDichvuclsChitiet.Columns.DonGia].Value, 0);
            frm.MaKhoaTHIEN = Utility.sDbnull(cboKhoaTH.SelectedValue, "");
            frm.DetailService =
                Utility.Int32Dbnull(grdList.CurrentRow.Cells[DmucDichvuclsChitiet.Columns.IdChitietdichvu].Value, 0);
            frm.ShowDialog();
            if (!frm.m_blnCancel)
                cmdSaveObjectAll_Click(cmdSaveObjectAll, e);
            ModifyCommand1();
            ModifyCommand();
        }

        /// <summary>
        ///     HÀM THUWCJHIEENJ KHỞI TẠO CHI TIẾT ĐỐI TƯỢNG CHI TIÊT DỊCH VỤ
        /// </summary>
        /// <returns></returns>
        private QheDoituongDichvucl CreateDmucDoituongkcbDetailService()
        {
            var objectTypeService = new QheDoituongDichvucl();

            return objectTypeService;
        }

        private void ModifyCommand()
        {
            try
            {
                cmdDelete.Enabled = grdQhe.RowCount > 0;
                cmdDetailDeleteAll.Enabled = grdQhe.RowCount > 0;
                cmdUpdate.Enabled = cmdCapNhap.Enabled = grdList.RowCount > 0 &&
                                                         grdList.CurrentRow.RowType == RowType.Record;
                cmdThemMoi.Enabled = grdList.CurrentRow.RowType == RowType.Record;
                cmdSaveObjectAll.Enabled = grdQhe.RowCount > 0 &&
                                           grdQhe.CurrentRow.RowType == RowType.Record;
            }
            catch (Exception)
            {
            }
        }

        private void SetStatusMessage()
        {
            switch (actionResult)
            {
                case ActionResult.Success:

                    Utility.ShowMsg("Bạn thực hiện thành công", "Thông báo");
                    break;
                case ActionResult.Error:
                    Utility.ShowMsg("Lỗi trong quá trình cập nhập", "Thông báo");
                    break;
            }
        }

        private void InitData()
        {
            try
            {
                DataTable m_dtDichvuCLS = new Select().From(DmucDichvucl.Schema).ExecuteDataSet().Tables[0];
                DataTable m_dtDichvuCLS_new = m_dtDichvuCLS.Clone();
                foreach (DataRow dr in m_dtDichvuCLS.Rows)
                {
                    if (Utility.CoquyenTruycapDanhmuc(Utility.sDbnull(dr[DmucDichvucl.Columns.IdLoaidichvu]), "0"))
                    {
                        m_dtDichvuCLS_new.ImportRow(dr);
                    }
                }
                DataBinding.BindDataCombobox(cboService, m_dtDichvuCLS_new, DmucDichvucl.Columns.IdDichvu,
                    DmucDichvucl.Columns.TenDichvu, "---Chọn---", true);
                //  m_dtDataDetailService = SPs.DmucLaydanhmucDichvuclsChitietTheoID(Utility.Int16Dbnull(cboService.SelectedValue, -1)).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdList, m_dtDataDetailService, true, true, "1=1",
                    "stt_hthi_dichvu,stt_hthi_chitiet," + DmucDichvuclsChitiet.Columns.TenChitietdichvu);
                dt_KhoaThucHien = THU_VIEN_CHUNG.Laydanhmuckhoa("NGOAI", 0);
                cboKhoaTH.DataSource = dt_KhoaThucHien;
                cboKhoaTH.ValueMember = DmucKhoaphong.Columns.MaKhoaphong;
                cboKhoaTH.DisplayMember = DmucKhoaphong.Columns.TenKhoaphong;
                cboKhoaTH.SelectedIndex = 0;
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy thông tin khoa");
            }
        }

        private void frm_qhe_doituong_dichvuCls_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                CLS_GIATHEO_KHOAKCB = THU_VIEN_CHUNG.Laygiatrithamsohethong("CLS_GIATHEO_KHOAKCB", "0", true) == "1";
                cboKhoaTH.Enabled = CLS_GIATHEO_KHOAKCB;
                if (!CLS_GIATHEO_KHOAKCB) cboKhoaTH.Text = "Tất cả";
                InitData();
                ModifyCommand1();
                ModifyCommand();
                m_blnLoaded = true;
                cboService_SelectedIndexChanged(cboService, new EventArgs());
                if (grdList.GetDataRows().Length > 0) grdList.MoveFirst();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }


        private void cmdSearchOnGrid_Click(object sender, EventArgs e)
        {
            grdList.FilterMode = FilterMode.Automatic;
        }

        private void cmdSaveAll_Click(object sender, EventArgs e)
        {
            SaveAll();
        }


        private void SaveAll()
        {
            try
            {
                Utility.SetMsg(lblMsg, "", false);
                decimal GiaDV = LayGiaDV();
                int ServiceDetailId = -1;
                decimal GiaPhuThu = 0;
                decimal GiaBHYT = LayGiaBHYT();
                string KTH = "ALL";

                foreach (GridEXRow gridExRow in grdQhe.GetRows())
                {
                    ServiceDetailId =
                        Utility.Int32Dbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.IdChitietdichvu].Value, -1);
                    SqlQuery q =
                        new Select().From(QheDoituongDichvucl.Schema)
                            .Where(QheDoituongDichvucl.Columns.IdChitietdichvu)
                            .
                            IsEqualTo(
                                Utility.DecimaltoDbnull(
                                    gridExRow.Cells[QheDoituongDichvucl.Columns.IdChitietdichvu].Value, -1)).And(
                                        QheDoituongDichvucl.Columns.MaDoituongKcb)
                            .IsEqualTo(Utility.sDbnull(
                                gridExRow.Cells[QheDoituongDichvucl.Columns.MaDoituongKcb].Value, "-1"))
                            .And(QheDoituongDichvucl.Columns.MaKhoaThuchien)
                            .IsEqualTo(Utility.sDbnull(cboKhoaTH.SelectedValue, ""));
                    //.Or(QheDoituongDichvucl.Columns.MaDoituongKcb).IsEqualTo("BHYT");
                    GiaPhuThu =
                        Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.PhuthuTraituyen].Value, 0);
                    int ObjectTypeType =
                        Utility.Int32Dbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.IdLoaidoituongKcb].Value, 0);

                    //if (ObjectTypeType == 0) KTH = "ALL"; else 
                    KTH = Utility.sDbnull(cboKhoaTH.SelectedValue, "ALL");
                    //Nếu có lưu đối tượng BHYT và tồn tại giá DV thì tự động tính phụ thu trái tuyến cho đối tượng BHYT đó
                    if (gridExRow.Cells[QheDoituongDichvucl.Columns.IdLoaidoituongKcb].Value.ToString() == "0" &&
                        GiaDV > 0)
                    {
                        GiaBHYT = Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.DonGia].Value, 0);
                        if (PropertyLib._QheGiaCLSProperties.TudongDieuChinhGiaPTTT)
                            GiaPhuThu = GiaDV - GiaBHYT > 0 ? GiaDV - GiaBHYT : 0;
                    }
                    //Nếu đối tượng BHYT có tồn tại thì update lại thông tin trong đó có giá phụ thu trái tuyến
                    if (q.GetRecordCount() > 0)
                    {
                        new Update(QheDoituongDichvucl.Schema)
                            .Set(QheDoituongDichvucl.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                            .Set(QheDoituongDichvucl.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                            .Set(QheDoituongDichvucl.Columns.IdDichvu).EqualTo(
                                GetService_ID(
                                    Utility.Int32Dbnull(
                                        gridExRow.Cells[QheDoituongDichvucl.Columns.IdChitietdichvu].Value, -1)))
                            .Set(QheDoituongDichvucl.Columns.DonGia).EqualTo(
                                Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.DonGia].Value, 0))
                            .Set(QheDoituongDichvucl.Columns.PhuthuDungtuyen).EqualTo(
                                Utility.DecimaltoDbnull(
                                    gridExRow.Cells[QheDoituongDichvucl.Columns.PhuthuDungtuyen].Value, 0))
                            .Set(QheDoituongDichvucl.Columns.PhuthuTraituyen).EqualTo(GiaPhuThu)
                            .Set(QheDoituongDichvucl.Columns.MotaThem).EqualTo(
                                Utility.sDbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.MotaThem].Value, ""))
                            .Where(QheDoituongDichvucl.Columns.IdChitietdichvu).IsEqualTo(ServiceDetail_ID)
                            .And(QheDoituongDichvucl.Columns.IdLoaidoituongKcb).IsEqualTo(
                                Utility.Int32Dbnull(
                                    gridExRow.Cells[QheDoituongDichvucl.Columns.IdLoaidoituongKcb].Value, -1))
                            .And(QheDoituongDichvucl.Columns.MaKhoaThuchien).IsEqualTo(KTH)
                            .Execute();
                    }
                    else
                    {
                        DmucDoituongkcbCollection objectTypeCollection =
                            new DmucDoituongkcbController().FetchByQuery(
                                DmucDoituongkcb.CreateQuery().AddWhere(DmucDoituongkcb.Columns.MaDoituongKcb,
                                    Comparison.Equals,
                                    Utility.sDbnull(gridExRow.Cells[DmucDoituongkcb.Columns.MaDoituongKcb].Value, "-1")));

                        foreach (DmucDoituongkcb lObjectType in objectTypeCollection)
                        {
                            var _newItems = new QheDoituongDichvucl();
                            _newItems.IdDoituongKcb = lObjectType.IdDoituongKcb;
                            _newItems.IdDichvu =
                                Utility.Int16Dbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.IdDichvu].Value, -1);
                            _newItems.IdChitietdichvu =
                                Utility.Int32Dbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.IdChitietdichvu].Value,
                                    -1);
                            _newItems.TyleGiam = 0;
                            _newItems.KieuGiamgia = 0;
                            _newItems.DonGia =
                                Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.DonGia].Value, 0);
                            _newItems.PhuthuDungtuyen =
                                Utility.DecimaltoDbnull(
                                    gridExRow.Cells[QheDoituongDichvucl.Columns.PhuthuDungtuyen].Value, 0);
                            _newItems.PhuthuTraituyen = GiaPhuThu;
                            _newItems.IdLoaidoituongKcb =
                                Utility.Int32Dbnull(
                                    gridExRow.Cells[QheDoituongDichvucl.Columns.IdLoaidoituongKcb].Value, -1);
                            _newItems.MaDoituongKcb = lObjectType.MaDoituongKcb;

                            _newItems.NguoiTao = globalVariables.UserName;
                            _newItems.NgayTao = globalVariables.SysDate;
                            _newItems.MaKhoaThuchien = KTH;
                            _newItems.IsNew = true;
                            _newItems.Save();
                            gridExRow.BeginEdit();
                            gridExRow.Cells[QheDoituongDichvucl.Columns.IdQuanhe].Value = _newItems.IdQuanhe;
                            gridExRow.EndEdit();
                        }
                    }
                    gridExRow.BeginEdit();
                    gridExRow.Cells[QheDoituongDichvucl.Columns.PhuthuTraituyen].Value = GiaPhuThu;
                    gridExRow.EndEdit();
                    grdQhe.UpdateData();
                    //Tự động update giá dịch vụ cho tất cả các khoa là giống nhau và giống giá sửa cuối cùng
                    if (PropertyLib._QheGiaCLSProperties.TudongDieuChinhGiaDichVu)
                    {
                        SqlQuery sqlQuery = new Select().From(DmucDoituongkcb.Schema)
                            .Where(DmucDoituongkcb.Columns.IdLoaidoituongKcb).IsEqualTo(1)
                            .And(DmucDoituongkcb.Columns.MaDoituongKcb)
                            .IsEqualTo(Utility.sDbnull(
                                gridExRow.Cells[QheDoituongDichvucl.Columns.MaDoituongKcb].Value, "-1"));
                        var objectType = sqlQuery.ExecuteSingle<DmucDoituongkcb>();
                        if (objectType != null)
                        {
                            new Update(DmucDichvuclsChitiet.Schema)
                                .Set(DmucDichvuclsChitiet.Columns.DonGia)
                                .EqualTo(
                                    Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.DonGia].Value, 0))
                                .Where(DmucDichvuclsChitiet.Columns.IdChitietdichvu)
                                .IsEqualTo(
                                    Utility.Int32Dbnull(
                                        gridExRow.Cells[QheDoituongDichvucl.Columns.IdChitietdichvu].Value, -1))
                                .Execute();
                        }
                    }
                }
                //new Update(DmucDichvuclsChitiet.Schema).Set(DmucDichvuclsChitiet.Columns.DonGia).EqualTo(GiaDV)
                //   .Set(DmucDichvuclsChitiet.Columns.GiaBhyt).EqualTo(GiaBHYT)
                //   .Where(DmucDichvuclsChitiet.Columns.IdChitietdichvu).IsEqualTo(Utility.Int32Dbnull(grdList.CurrentRow.Cells[QheDoituongDichvucl.Columns.IdChitietdichvu].Value, -1))
                //   .Execute();
                //Cập nhật giá BHYT cho các khoa thực hiện
                if (PropertyLib._QheGiaCLSProperties.TudongDieuChinhGiaBHYT)
                {
                    if (GiaBHYT >= 0)
                    {
                        var lstItems =
                            new Select().From(QheDoituongDichvucl.Schema).
                                Where(QheDoituongDichvucl.Columns.IdChitietdichvu).
                                IsEqualTo(ServiceDetailId)
                                .And(QheDoituongDichvucl.MaKhoaThuchienColumn)
                                .IsNotEqualTo(KTH)
                                .ExecuteAsCollection<QheDoituongDichvuclCollection>();
                        foreach (QheDoituongDichvucl item in lstItems)
                        {
                            int ObjectTypeType = item.IdLoaidoituongKcb.Value;
                            if (ObjectTypeType == 1)
                                GiaDV = item.DonGia.Value;
                        }
                        GiaPhuThu = 0;
                        foreach (QheDoituongDichvucl item in lstItems)
                        {
                            int ObjectTypeType = item.IdLoaidoituongKcb.Value;
                            if (ObjectTypeType.ToString() == "0" && GiaDV > 0)
                            {
                                GiaPhuThu = GiaDV - GiaBHYT > 0 ? GiaDV - GiaBHYT : 0;
                                Update _update =
                                    new Update(QheDoituongDichvucl.Schema).Set(QheDoituongDichvucl.DonGiaColumn)
                                        .EqualTo(GiaBHYT);
                                if (PropertyLib._QheGiaCLSProperties.TudongDieuChinhGiaPTTT)
                                    _update.Set(QheDoituongDichvucl.PhuthuTraituyenColumn).EqualTo(GiaPhuThu);
                                _update.Where(QheDoituongDichvucl.IdLoaidoituongKcbColumn)
                                    .IsEqualTo(0)
                                    .And(QheDoituongDichvucl.IdChitietdichvuColumn)
                                    .IsEqualTo(ServiceDetailId)
                                    .And(QheDoituongDichvucl.MaKhoaThuchienColumn).IsNotEqualTo(KTH)
                                    .Execute();
                            }
                        }
                    }
                }
                Utility.SetMsg(lblMsg, "Bạn thực hiện cập nhập giá thành công", false);
            }
            catch (Exception exception)
            {
                Utility.SetMsg(lblMsg, "Lỗi trong quá trình cập nhập thông tin", false);
            }
        }

        private int GetService_ID(int v_ServiceDetail_ID)
        {
            int v_Service_ID = -1;
            DataRow[] arrDr =
                globalVariables.gv_dtDmucDichvuClsChitiet.Select(DmucDichvuclsChitiet.Columns.IdChitietdichvu + "=" +
                                                                 v_ServiceDetail_ID);
            if (arrDr.GetLength(0) > 0)
            {
                v_Service_ID = Utility.Int32Dbnull(arrDr[0][DmucDichvucl.Columns.IdDichvu], -1);
            }
            return v_Service_ID;
        }

        private short GetService_ID2(int v_ServiceDetail_ID)
        {
            short v_Service_ID = -1;
            DataRow[] arrDr =
                globalVariables.gv_dtDmucDichvuClsChitiet.Select(DmucDichvuclsChitiet.Columns.IdChitietdichvu + "=" +
                                                                 v_ServiceDetail_ID);
            if (arrDr.GetLength(0) > 0)
            {
                v_Service_ID = Utility.Int16Dbnull(arrDr[0][DmucDichvucl.Columns.IdDichvu], -1);
            }
            return v_Service_ID;
        }

        private decimal GetLastPrice(decimal Price, int v_DiscountType, decimal v_DiscountRate)
        {
            decimal v_LastPrice = 0;
            if (v_DiscountType == 1)
            {
                v_LastPrice = Price - v_DiscountRate;
            }
            if (v_DiscountType == 0)
            {
                v_LastPrice = Price*(100 - v_DiscountRate)/100;
            }
            return v_LastPrice;
        }


        private void frm_qhe_doituong_dichvuCls_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdClose.PerformClick();
            if (e.KeyCode == Keys.F5) LoadData();
            if (e.KeyCode == Keys.N && e.Control) cmdThemMoi.PerformClick();
            if (e.KeyCode == Keys.U && e.Control) cmdUpdate.PerformClick();
            if (e.KeyCode == Keys.S && e.Control) cmdSaveObjectAll_Click(cmdSaveObjectAll, new EventArgs());
        }

        private void grdList_ApplyingFilter(object sender, CancelEventArgs e)
        {
            ModifyCommand1();
            ModifyCommand();
        }


        private void txtDetailLastPrice_LostFocus(object sender, EventArgs e)
        {
        }


        private void cmdPrint_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();
        }

        private void cmdExportExcel_Click(object sender, EventArgs e)
        {
            v_DataPrint =
                SPs.DmucLaydulieuQhedichvuclsIn(Utility.Int32Dbnull(cboKieuIn.SelectedValue, 0)).GetDataSet().Tables[0];
            if (v_DataPrint.Rows.Count <= 0) return;
            string reportcode = "qhe_PhieuinGiaCLStheodoituong";
            string duongdan = Utility.GetPathExcel(reportcode);
            var book = new C1XLBook();
            book.Load(duongdan);
            book.DefaultFont = new Font("Time New Roman", 11, FontStyle.Regular);
            XLSheet sheet = book.Sheets[0];

            DataTable dt = v_DataPrint;
            int idxRow = 6;
            int idxColSh = 0;
            int sttloaidichvu = 1;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 0)
                {
                    sheet[idxRow, idxColSh].SetValue(
                        string.Format("{0}.{1}", sttloaidichvu, Convert.ToString(dt.Rows[i]["Ten_dichvu"])),
                        HamDungChung.styleStringLeft_Bold(book));
                    sttloaidichvu = sttloaidichvu + 1;
                    idxRow = idxRow + 1;
                }
                else
                {
                    if (dt.Rows[i]["Ten_dichvu"].ToString() !=
                        dt.Rows[i - 1]["Ten_dichvu"].ToString())
                    {
                        sheet[idxRow, idxColSh].SetValue(
                            string.Format("{0}.{1}", sttloaidichvu,
                                Convert.ToString(dt.Rows[i]["Ten_dichvu"])),
                            HamDungChung.styleStringLeft_Bold(book));
                        sttloaidichvu = sttloaidichvu + 1;
                        idxRow = idxRow + 1;
                    }
                }
                sheet[idxRow, idxColSh].SetValue(Convert.ToString(i.ToString()), HamDungChung.styleStringCenter(book));
                sheet[idxRow, idxColSh + 1].SetValue(Convert.ToString(dt.Rows[i]["ma_chitietdichvu"]), HamDungChung.styleStringCenter(book));
                sheet[idxRow, idxColSh + 2].SetValue(Convert.ToString(dt.Rows[i]["ma_chitietdichvu_bhyt"]), HamDungChung.styleStringLeft(book));
                sheet[idxRow, idxColSh + 3].SetValue(Convert.ToString(dt.Rows[i]["ten_chitietdichvu"]), HamDungChung.styleStringLeft(book));
                sheet[idxRow, idxColSh + 4].SetValue(Convert.ToDecimal(dt.Rows[i]["gia_bhyt"]), HamDungChung.styleNumber(book));
                sheet[idxRow, idxColSh + 5].SetValue(Convert.ToDecimal(dt.Rows[i]["gia_dv"]), HamDungChung.styleNumber(book));
                idxRow = idxRow + 1;
            }
            string getTime = Convert.ToString(DateTime.Now.ToString("yyyyMMddhhmmss"));
            string pathDirectory = AppDomain.CurrentDomain.BaseDirectory + "TemplateExcel\\ExportExcel\\";
            if (!Directory.Exists(pathDirectory))
            {
                Directory.CreateDirectory(pathDirectory);
            }

            book.Save(AppDomain.CurrentDomain.BaseDirectory + "\\TemplateExcel\\ExportExcel\\" + reportcode +
                      getTime + ".xls");
            Process.Start(
                new ProcessStartInfo(AppDomain.CurrentDomain.BaseDirectory + "\\TemplateExcel\\ExportExcel\\" +
                                     reportcode + getTime + ".xls"));
            //string sPath = "drug.xls";
            //FileStream fs = new FileStream(sPath, FileMode.Create);
            //gridEXExporter.Export(fs);
        }


        private void grdObjectTypeList_SelectionChanged(object sender, EventArgs e)
        {
            _rowFilter = "1=1";
            if (grdObjectTypeList.CurrentRow != null)
            {
                v_ObjectType_Id =
                    Utility.Int32Dbnull(
                        grdObjectTypeList.CurrentRow.Cells[QheDoituongDichvucl.Columns.IdDoituongKcb].Value, -1);
                _rowFilter = DmucDoituongkcb.Columns.IdDoituongKcb + "=" + v_ObjectType_Id;
            }
            m_dtObjectRelationService.DefaultView.RowFilter = _rowFilter;
            m_dtObjectRelationService.AcceptChanges();
        }


        private QheDoituongDichvucl CreateObjectTypeService(GridEXRow gridExRow)
        {
            var objectTypeService = new QheDoituongDichvucl();
            objectTypeService.DonGia = Utility.DecimaltoDbnull(
                gridExRow.Cells[QheDoituongDichvucl.Columns.DonGia].Value, 0);
            objectTypeService.PhuthuDungtuyen = Utility.DecimaltoDbnull(
                gridExRow.Cells[QheDoituongDichvucl.Columns.PhuthuDungtuyen].Value, 0);
            objectTypeService.IdChitietdichvu =
                Utility.Int16Dbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.IdChitietdichvu].Value);
            objectTypeService.MotaThem = gridExRow.Cells[QheDoituongDichvucl.Columns.MotaThem].Value.ToString();
            objectTypeService.IdDoituongKcb =
                Utility.Int16Dbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.IdDoituongKcb].Value, -1);
            objectTypeService.TyleGiam =
                Utility.Int16Dbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.TyleGiam].Value, -1);
            objectTypeService.KieuGiamgia =
                Utility.ByteDbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.KieuGiamgia].Value);
            return objectTypeService;
        }

        private decimal GetLastPrice(GridEXRow gridExRow)
        {
            if (gridExRow.Cells[QheDoituongDichvucl.Columns.KieuGiamgia].Value.ToString() == "0")
            {
                return Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.DonGia].Value, 0)*
                       (100 - Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.TyleGiam].Value))/100;
            }
            return Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.DonGia].Value, 0) -
                   Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.TyleGiam].Value, 0);
        }

        private void cmDeteleServiceDetail_Click_1(object sender, EventArgs e)
        {
        }


        private void txtDetailLastPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.OnlyDigit(e);
        }


        private void cmdSaveObjectAll_Click(object sender, EventArgs e)
        {
            SaveAll();
        }

        private decimal LayGiaDV()
        {
            foreach (GridEXRow gridExRow in grdQhe.GetRows())
            {
                if (gridExRow.Cells[DmucDoituongkcb.Columns.IdLoaidoituongKcb].Value.ToString() == "1")
                    return Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.DonGia].Value, 0);
            }
            return -1;
        }

        private decimal LayGiaBHYT()
        {
            foreach (GridEXRow gridExRow in grdQhe.GetRows())
            {
                if (gridExRow.Cells[DmucDoituongkcb.Columns.IdLoaidoituongKcb].Value.ToString() == "0")
                    return Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.DonGia].Value, 0);
            }
            return -1;
        }

        private void SaveQheDoituongDichvuCSL()
        {
            try
            {
                foreach (GridEXRow gridExRow in grdList.GetRows())
                {
                    SqlQuery q =
                        new Select().From(QheDoituongDichvucl.Schema)
                            .Where(QheDoituongDichvucl.Columns.IdChitietdichvu)
                            .
                            IsEqualTo(
                                Utility.DecimaltoDbnull(
                                    gridExRow.Cells[QheDoituongDichvucl.Columns.IdChitietdichvu].Value, -1)).And(
                                        QheDoituongDichvucl.Columns.IdLoaidoituongKcb).IsEqualTo(
                                            Utility.Int32Dbnull(
                                                gridExRow.Cells[DmucDoituongkcb.Columns.IdLoaidoituongKcb].Value, -1));
                    if (q.GetRecordCount() > 0)
                    {
                        new Update(QheDoituongDichvucl.Schema)
                            .Set(QheDoituongDichvucl.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                            .Set(QheDoituongDichvucl.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                            .Set(QheDoituongDichvucl.Columns.IdDichvu).EqualTo(
                                GetService_ID(
                                    Utility.Int32Dbnull(
                                        gridExRow.Cells[QheDoituongDichvucl.Columns.IdChitietdichvu].Value, -1)))
                            .Set(QheDoituongDichvucl.Columns.DonGia).EqualTo(
                                Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.DonGia].Value, 0))
                            .Set(QheDoituongDichvucl.Columns.PhuthuDungtuyen).EqualTo(
                                Utility.DecimaltoDbnull(
                                    gridExRow.Cells[QheDoituongDichvucl.Columns.PhuthuDungtuyen].Value, 0))
                            .Set(QheDoituongDichvucl.Columns.PhuthuTraituyen).EqualTo(
                                Utility.DecimaltoDbnull(
                                    gridExRow.Cells[QheDoituongDichvucl.Columns.PhuthuTraituyen].Value, 0))
                            .Set(QheDoituongDichvucl.Columns.MotaThem).EqualTo(
                                Utility.sDbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.MotaThem].Value, ""))
                            .Set(QheDoituongDichvucl.Columns.MaKhoaThuchien)
                            .EqualTo(Utility.sDbnull(cboKhoaTH.SelectedValue, ""))
                            .Where(QheDoituongDichvucl.Columns.IdChitietdichvu).IsEqualTo(ServiceDetail_ID)
                            .And(QheDoituongDichvucl.Columns.IdLoaidoituongKcb).IsEqualTo(
                                Utility.Int32Dbnull(gridExRow.Cells[DmucDoituongkcb.Columns.IdLoaidoituongKcb].Value, -1))
                            .Execute();
                    }
                    else
                    {
                        new QheDoituongDichvuclController().Insert(-1,
                            GetService_ID2(Utility.Int32Dbnull(
                                gridExRow.Cells[QheDoituongDichvucl.Columns.IdChitietdichvu].Value, -1)),
                            Utility.Int32Dbnull(
                                gridExRow.Cells[QheDoituongDichvucl.Columns.IdChitietdichvu].Value, -1),
                            0, Utility.ByteDbnull(1),
                            Utility.sDbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.MotaThem].Value, ""),
                            Utility.DecimaltoDbnull(
                                gridExRow.Cells[QheDoituongDichvucl.Columns.DonGia].Value, 0),
                            Utility.DecimaltoDbnull(
                                gridExRow.Cells[QheDoituongDichvucl.Columns.PhuthuDungtuyen].Value, 0),
                            Utility.Int32Dbnull(
                                gridExRow.Cells[DmucDoituongkcb.Columns.IdLoaidoituongKcb].Value, -1),
                            Utility.DecimaltoDbnull(
                                gridExRow.Cells[QheDoituongDichvucl.Columns.PhuthuTraituyen].Value, 0), "",
                            globalVariables.SysDate, globalVariables.UserName, null, null,
                            Utility.sDbnull(cboKhoaTH.SelectedValue, ""));
                    }

                    SqlQuery sqlQuery = new Select().From(DmucDoituongkcb.Schema)
                        .Where(DmucDoituongkcb.Columns.IdLoaidoituongKcb).IsEqualTo(1);
                    var objectType = sqlQuery.ExecuteSingle<DmucDoituongkcb>();
                    if (objectType != null && objectType.MaDoituongKcb == "DV")
                    {
                        new Update(DmucDichvuclsChitiet.Schema)
                            .Set(DmucDichvuclsChitiet.Columns.DonGia)
                            .EqualTo(Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.DonGia].Value,
                                0))
                            .Where(DmucDichvuclsChitiet.Columns.IdChitietdichvu)
                            .IsEqualTo(
                                Utility.Int32Dbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.IdChitietdichvu].Value,
                                    -1)).Execute();
                    }
                }
                Utility.ShowMsg("Bạn thực hiện cập nhập giá thành công", "Thông báo");
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi trong quá trình cập nhập thông tin", "Thông báo lỗi", MessageBoxIcon.Error);
            }
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            ActionUpdate();
        }

        private void ActionUpdate()
        {
            if (grdList.CurrentRow != null && grdList.CurrentRow.RowType == RowType.Record)
            {
                if (grdList.CurrentRow == null) return;
                v_ServiceDetail_ID =
                    Utility.Int32Dbnull(grdList.GetValue(DmucDichvuclsChitiet.Columns.IdChitietdichvu), -1);
                var frm = new frm_themmoi_dichvucls_chitiet();
                frm.txtID.Text = Utility.sDbnull(v_ServiceDetail_ID);
                frm.m_enAction = action.Update;
                frm.dtDataServiceDetail = m_dtDataDetailService;
                if (grdList.CurrentRow != null)
                    frm.drServiceDetail = Utility.FetchOnebyCondition(m_dtDataDetailService,
                        DmucDichvuclsChitiet.Columns.IdChitietdichvu + "=" + v_ServiceDetail_ID);
                frm.ShowDialog();
            }
            ModifyCommand();
            ModifyCommand1();
        }

        private void chkExpand_CheckedChanged(object sender, EventArgs e)
        {
            grdList.GroupMode = chkExpand.Checked ? GroupMode.Default : GroupMode.Expanded;
            chkExpand.Text = chkExpand.Checked ? "Mở rộng" : "Thu lại";
        }


        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            cmdUpdate.Enabled = false;
            if (grdList.RowCount > 0)
            {
                cmdUpdate.Enabled = grdList.RowCount > 0 && grdList.CurrentRow.RowType == RowType.Record;
            }
        }


        private void groupBox5_Enter(object sender, EventArgs e)
        {
        }

        private void cmdPrintRelationObject_Click(object sender, EventArgs e)
        {
            v_DataPrint =
                SPs.DmucLaydulieuQhedichvuclsIn(Utility.Int32Dbnull(cboKieuIn.SelectedValue, 0)).GetDataSet().Tables[0];
            THU_VIEN_CHUNG.CreateXML(v_DataPrint, "qhe_PhieuinGiaCLStheodoituong.XML");
            PrintReport(PropertyLib._QheGiaCLSProperties.TieudeBaocaoGiaCls);
        }

        private void ProcessDataReport(ref DataTable dataTable)
        {
            if (!dataTable.Columns.Contains("Price_DV")) dataTable.Columns.Add("Price_DV", typeof (decimal));
            if (!dataTable.Columns.Contains("Price_BHYT")) dataTable.Columns.Add("Price_BHYT", typeof (decimal));
            if (!dataTable.Columns.Contains("Price_KYC")) dataTable.Columns.Add("Price_KYC", typeof (decimal));
            foreach (DataRow drv in dataTable.Rows)
            {
                DataRow[] arrDr =
                    m_dtReportObjectType.Select(DmucDoituongkcb.Columns.IdLoaidoituongKcb + " =1 and " +
                                                DmucDichvuclsChitiet.Columns.IdChitietdichvu + "=" +
                                                Utility.Int32Dbnull(drv[QheDoituongDichvucl.Columns.IdChitietdichvu], -1));
                if (arrDr.GetLength(0) > 0)
                {
                    drv["Price_DV"] = Utility.DecimaltoDbnull(arrDr[0][QheDoituongDichvucl.Columns.DonGia], 0);
                }
                DataRow[] arrDrBH =
                    m_dtReportObjectType.Select(DmucDoituongkcb.Columns.IdLoaidoituongKcb + " =0 and " +
                                                DmucDichvuclsChitiet.Columns.IdChitietdichvu + "=" +
                                                Utility.Int32Dbnull(drv[QheDoituongDichvucl.Columns.IdChitietdichvu], -1));
                if (arrDrBH.GetLength(0) > 0)
                {
                    drv["Price_BHYT"] = Utility.DecimaltoDbnull(arrDrBH[0][QheDoituongDichvucl.Columns.DonGia], 0);
                }
                DataRow[] arrDrQNCS =
                    m_dtReportObjectType.Select(DmucDoituongkcb.Columns.IdLoaidoituongKcb + "= 2  and and " +
                                                DmucDichvuclsChitiet.Columns.IdChitietdichvu + "=" +
                                                Utility.Int32Dbnull(drv[QheDoituongDichvucl.Columns.IdChitietdichvu], -1));
                if (arrDrQNCS.GetLength(0) > 0)
                {
                    drv["Price_KYC"] = Utility.DecimaltoDbnull(arrDrQNCS[0][QheDoituongDichvucl.Columns.DonGia], 0);
                }
            }
            dataTable.AcceptChanges();
        }

        /// <summary>
        ///     hàm thực hiện in báo cáo
        /// </summary>
        /// <param name="sTitleReport"></param>
        private void PrintReport(string sTitleReport)
        {
            string tieude = "", reportname = "";
            ReportDocument crpt = Utility.GetReport("qhe_PhieuinGiaCLStheodoituong", ref tieude, ref reportname);
            if (crpt == null) return;
            var objFromPre = new frmPrintPreview(tieude, crpt, true, v_DataPrint.Rows.Count > 0);
            //var objFromPre =
            //    new frmPrintPreview(sTitleReport, crpt,false, true);
            Utility.WaitNow(this);
            crpt.SetDataSource(v_DataPrint);
            Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
            Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
            Utility.SetParameterValue(crpt, "sTitleReport", sTitleReport);
            Utility.SetParameterValue(crpt, "sDateTime", Utility.FormatDateTime(globalVariables.SysDate));
            Utility.SetParameterValue(crpt, "txtTrinhky",
                Utility.getTrinhky(objFromPre.mv_sReportFileName, globalVariables.SysDate));
            objFromPre.crptViewer.ReportSource = crpt;
            objFromPre.ShowDialog();
            Utility.DefaultNow(this);
        }


        private DataTable GetDataCheck()
        {
            DataTable dataTable = m_dtDataDetailService.Copy();
            foreach (GridEXRow gridExRow in grdList.GetCheckedRows())
            {
                DataRow[] arrDr =
                    dataTable.Select(DmucDichvuclsChitiet.Columns.IdChitietdichvu + "=" +
                                     Utility.Int32Dbnull(
                                         gridExRow.Cells[QheDoituongDichvucl.Columns.IdChitietdichvu].Value, -1));
                if (arrDr.GetLength(0) <= 0)
                {
                    arrDr[0].Delete();
                }
            }

            dataTable.AcceptChanges();
            return dataTable;
        }

        private void cmdThemMoi_Click(object sender, EventArgs e)
        {
            if (grdList.CurrentRow != null && grdList.CurrentRow.RowType == RowType.Record)
            {
                if (grdList.CurrentRow == null) return;
                var frm = new frm_themmoi_dichvucls_chitiet();
                frm.grdlist = grdList;
                frm.txtID.Text = "-1";
                frm.m_enAction = action.Insert;
                frm.dtDataServiceDetail = m_dtDataDetailService;
                if (grdList.CurrentRow != null)
                    frm.drServiceDetail = Utility.FetchOnebyCondition(m_dtDataDetailService,
                        DmucDichvuclsChitiet.Columns.IdChitietdichvu + "=" + v_ServiceDetail_ID);
                frm.ShowDialog();
            }
            ModifyCommand();
            ModifyCommand1();
        }

        private void cmdCapNhap_Click(object sender, EventArgs e)
        {
            ActionUpdate();
        }

        private void cboKhoaTH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_blnLoaded || !CLS_GIATHEO_KHOAKCB) return;
            grdList_SelectionChanged(grdList, e);
        }
    }
}