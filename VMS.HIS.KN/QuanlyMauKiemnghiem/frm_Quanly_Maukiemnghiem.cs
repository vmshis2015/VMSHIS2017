using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using SubSonic;
using VMS.HIS.KN.Classess;
using VMS.HIS.KN.QuanlyMauKiemnghiem;
using VNS.HIS.BusRule.Classes;
using VNS.HIS.DAL;
using VNS.Libs;
using AggregateFunction = Janus.Windows.GridEX.AggregateFunction;

namespace VMS.HIS.KN.NGOAITRU
{
    public partial class frm_Quanly_Maukiemnghiem : Form
    {
        private readonly string Args = "ALL";
        private readonly KCB_CHIDINH_CANLAMSANG _KCB_CHIDINH_CANLAMSANG = new KCB_CHIDINH_CANLAMSANG();
        private readonly KCB_DANGKY _KCB_DANGKY = new KCB_DANGKY();
        private readonly DataTable m_dataDataRegExam = new DataTable();
        private bool AllowTextChanged;
        private string MA_DTUONG = "DV";
        private DataTable m_PhongKham = new DataTable();
        private bool m_blnHasloaded;
        private DataTable m_dtChiDinhCLS = new DataTable();
        private DataTable m_dtDanhsachDichvuKCB = new DataTable();
        private DataTable m_dtPatient = new DataTable();
        private DataTable m_kieuKham;

        public frm_Quanly_Maukiemnghiem()
        {
            InitializeComponent();
            this.Args = Args;
            //this.InitTrace();
            KeyPreview = true;
            dtmFrom.Value = globalVariables.SysDate;
            dtmTo.Value = globalVariables.SysDate;

            InitEvents();
        }

        private void InitEvents()
        {
            txtPatientCode.KeyDown += txtPatientCode_KeyDown;
            txtPatient_ID.KeyDown += txtPatient_ID_KeyDown;
            cmdTimKiem.Click += cmdTimKiem_Click;

            grdList.SelectionChanged += grdList_SelectionChanged;
            grdList.MouseDoubleClick += grdList_MouseDoubleClick;


            cmdThemMoiBN.Click += cmdThemMoiBN_Click;
            cmdSuaThongTinBN.Click += cmdSuaThongTinBN_Click;
            cmdThemLanKham.Click += cmdThemLanKham_Click;
            cmdXoaBenhNhan.Click += cmdXoaBenhNhan_Click;
           // cmdExit.Click += cmdExit_Click;


            grdDetail.CellValueChanged += grdDetail_CellValueChanged;
            grdDetail.FormattingRow += grdDetail_FormattingRow;
            grdDetail.ColumnHeaderClick += grdDetail_ColumnHeaderClick;
            grdDetail.SelectionChanged += grdDetail_SelectionChanged;
            cmdXoaChiDinh.Click += cmdXoaChiDinh_Click;
            cmdSuaChiDinh.Click += cmdSuaChiDinh_Click;
            cmdThemChiDinh.Click += cmdThemChiDinh_Click;

            chkByDate.CheckedChanged += chkByDate_CheckedChanged;

            FormClosing += frm_Quanly_Maukiemnghiem_FormClosing;
            Load += frm_Quanly_Maukiemnghiem_Load;
            KeyDown += frm_Quanly_Maukiemnghiem_KeyDown;

            cmdPrintAssign.Click += cmdPrintAssign_Click;
            //cmdBangiao.Click += cmdBangiao_Click;
        }

        private void BangiaoMau(bool bangiao)
        {
            bool isValid = Utility.isValidGrid(grdDetail);
            bool autoChecked = false;
            try
            {
                bool asked = false;
                if (!grdDetail.GetCheckedRows().Any())
                {
                    if (!isValid)
                    {
                        Utility.ShowMsg("Bạn cần chọn ít nhất một mẫu kiểm nghiệm để bàn giao/hủy bàn giao");
                        return;
                    }
                    else
                    {
                        autoChecked = true;
                        grdDetail.CurrentRow.BeginEdit();
                        grdDetail.CurrentRow.IsChecked = true;
                        grdDetail.CurrentRow.EndEdit();
                    }
                }
                List<string> lstIdchidinhchitiet = (from chidinh in grdDetail.GetCheckedRows().AsEnumerable()
                    let x = Utility.sDbnull(chidinh.Cells[KnChidinhChitiet.Columns.IdChidinhChitiet].Value)
                    select x).ToList<string>();
                string idChitietchidinh = string.Join(",", lstIdchidinhchitiet.ToArray());
                string question = "";
                if (bangiao)
                {
                    question = string.Format("Bạn có muốn bàn giao các mẫu đang chọn hay không?");
                }
                else
                {
                    question = string.Format("Bạn có muốn hủy bàn giao các mẫu đang chọn hay không?");
                }
                if (bangiao)
                {
                    if (Utility.AcceptQuestion(question, "Thông báo", true))
                    {
                        long idkhachhang =
                            Utility.Int64Dbnull(grdList.CurrentRow.Cells[KnDangkyXn.Columns.IdKhachhang].Value, -1);
                        string tenkhachhang =
                            Utility.sDbnull(grdList.CurrentRow.Cells[KnDanhsachKhachhang.Columns.TenKhachhang].Value, "");
                        string diachi =
                            Utility.sDbnull(grdList.CurrentRow.Cells[KnDanhsachKhachhang.Columns.DiaChi].Value, "");
                        string madangky = Utility.sDbnull(grdList.CurrentRow.Cells[KnDangkyXn.Columns.MaDangky].Value,
                            "");
                        int iddichvu =
                            Utility.Int32Dbnull(grdDetail.CurrentRow.Cells[KnChidinhChitiet.Columns.IdDichvu].Value, "");
                        long idchidinh =
                            Utility.Int64Dbnull(grdDetail.CurrentRow.Cells[KnChidinhChitiet.Columns.IdChidinh].Value, "");
                        var frm = new FrmBanGiaoMauKiemNghiem();
                        frm.txtPatientName.Text = tenkhachhang.Trim();
                        frm.Iddichvu = iddichvu;
                        frm.IdChidinh = idchidinh;
                        frm.ShowDialog();
                        cmdBangiao.Tag = frm.Trangthai == 1 ? "1" : "0";
                        if (frm.Trangthai == 1)
                        {
                            new Update(KnChidinhChitiet.Schema).Set(KnChidinhChitiet.Columns.TrangThai)
                                .EqualTo(1)
                                .Where(KnChidinhChitiet.Columns.IdChidinh)
                                .IsEqualTo(idchidinh)
                                .Execute();
                        }

                        LoadChiDinh();
                        ModifycommandAssignDetail();
                    }
                }
                else 
                {
                    if (Utility.AcceptQuestion(question, "Thông báo", true))
                    {
                        long idkhachhang =
                            Utility.Int64Dbnull(grdList.CurrentRow.Cells[KnDangkyXn.Columns.IdKhachhang].Value, -1);
                        string tenkhachhang =
                            Utility.sDbnull(grdList.CurrentRow.Cells[KnDanhsachKhachhang.Columns.TenKhachhang].Value, "");
                        string diachi =
                            Utility.sDbnull(grdList.CurrentRow.Cells[KnDanhsachKhachhang.Columns.DiaChi].Value, "");
                        string madangky = Utility.sDbnull(grdList.CurrentRow.Cells[KnDangkyXn.Columns.MaDangky].Value,
                            "");
                        int iddichvu =
                            Utility.Int32Dbnull(grdDetail.CurrentRow.Cells[KnChidinhChitiet.Columns.IdDichvu].Value, "");
                        long idchidinh =
                            Utility.Int64Dbnull(grdDetail.CurrentRow.Cells[KnChidinhChitiet.Columns.IdChidinh].Value, "");
                        new Update(KnChidinhXn.Schema)
                            .Set(KnChidinhXn.Columns.TrangThai).EqualTo(0)
                            .Set(KnChidinhXn.Columns.LuongmauHoaly).EqualTo(0)
                            .Set(KnChidinhXn.Columns.LuongmauVisinh).EqualTo(0)
                            .Set(KnChidinhXn.Columns.LuongmauThauphu).EqualTo(0)
                            .Set(KnChidinhXn.Columns.NguoigiaoMau).EqualTo(string.Empty)
                            .Set(KnChidinhXn.Columns.NguoigiaoVisinh).EqualTo(string.Empty)
                            .Set(KnChidinhXn.Columns.NguoigiaoHoaly).EqualTo(string.Empty)
                            .Set(KnChidinhXn.Columns.NguoigiaoThauphu).EqualTo(string.Empty)
                            .Set(KnChidinhXn.Columns.NguoinhanHoaly).EqualTo(string.Empty)
                            .Set(KnChidinhXn.Columns.NguoinhanVisinh).EqualTo(string.Empty)
                            .Set(KnChidinhXn.Columns.NguoinhanThauphu).EqualTo(string.Empty)
                            .Set(KnChidinhXn.Columns.NgaygiaoMau).EqualTo(null)
                            .Where(KnChidinhXn.Columns.IdChidinh).IsEqualTo(idchidinh).Execute();
                        new Update(KnChidinhChitiet.Schema).Set(KnChidinhChitiet.Columns.TrangThai)
                            .EqualTo(0)
                            .Where(KnChidinhChitiet.Columns.IdChidinh)
                            .IsEqualTo(idchidinh)
                            .Execute();
                        LoadChiDinh();
                        ModifycommandAssignDetail();
                    }
                }
                #region Chuyển cũ 
                //byte trangthaicu = 0;
                //byte trangthaimoi = 1;
                //if (!bangiao)
                //{
                //    trangthaicu = 1;
                //    trangthaimoi = 0;
                //    question = string.Format("Bạn có muốn hủy bàn giao các mẫu đang chọn hay không?");
                //}
                //if (!Utility.AcceptQuestion(question, "Thông báo", true))
                //{
                //    return;
                //}
                //cmdBangiao.Text = bangiao ? "Hủy giao mẫu" : "Bàn giao mẫu";
                //cmdBangiao.Tag = bangiao ? 1 : 0;
                // _KCB_CHIDINH_CANLAMSANG.MaukiemnghiemCapnhattrangthai(idChitietchidinh, trangthaicu, trangthaimoi);
                //m_dtChiDinhCLS.AsEnumerable()
                //    .Where(
                //        c =>
                //            Utility.ByteDbnull(c[KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan], 0) == 1 &&
                //            Utility.sDbnull(c["trangthai_chuyencls"], "") == trangthaicu.ToString() &&
                //            lstIdchidinhchitiet.Contains(
                //                Utility.sDbnull(c.Field<long>(KcbChidinhclsChitiet.Columns.IdChitietchidinh))))
                //    .ToList()
                //    .ForEach(c1 =>
                //    {
                //        c1["trangthai_chuyencls"] = trangthaimoi;
                //        c1["ten_trangthai_chuyencls"] = trangthaimoi == 0 ? "Chưa bàn giao" : "Đã bàn giao";
                //    });
                //m_dtChiDinhCLS.AcceptChanges();
                //ModifycommandAssignDetail();
                #endregion
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                if (autoChecked)
                {
                    grdDetail.CurrentRow.BeginEdit();
                    grdDetail.CurrentRow.IsChecked = false;
                    grdDetail.CurrentRow.EndEdit();
                }
            }
        }

        private void cmdBangiao_Click(object sender, EventArgs e)
        {
            BangiaoMau(cmdBangiao.Tag.ToString() == "0");
        }

        private void grdList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            cmdSuaThongTinBN.PerformClick();
        }

        private void cmdPrintAssign_Click(object sender, EventArgs e)
        {
            try
            {
                string mayin = "";
                int v_AssignId = Utility.Int32Dbnull(grdDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), -1);
                string v_AssignCode = Utility.sDbnull(grdDetail.GetValue(KcbChidinhcl.Columns.MaChidinh), -1);
                string nhomincls = "ALL";

                KcbInphieu.InphieuDangkyKiemnghiem(v_AssignId);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }


        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtmTo.Enabled = dtmFrom.Enabled = chkByDate.Checked;
        }

        private void TimKiemThongTin(bool theongay)
        {
            try
            {
                int Hos_status = -1;
                //if (radNgoaiTru.Checked) Hos_status = 0;
                //if (radNoiTru.Checked) Hos_status = 1;
                m_dtPatient =
                    _KCB_DANGKY.KcbTiepdonLayDanhSachKhachhang(dtmFrom.Value, dtmTo.Value,
                        Utility.sDbnull(txtPatientName.Text, ""), Utility.Int64Dbnull(txtPatient_ID.Text, -1),
                        Utility.sDbnull(txtPatientCode.Text, ""), Hos_status);
                Utility.SetDataSourceForDataGridEx(grdList, m_dtPatient, true, true, "1=1",
                    KnDanhsachKhachhang.Columns.IdKhachhang + " desc");
                if (grdList.GetDataRows().Length <= 0)
                    m_dataDataRegExam.Rows.Clear();
                UpdateGroup();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
            finally
            {
                ModifyCommand();
            }
        }

        private void UpdateGroup()
        {
            try
            {
                var counts = m_dtPatient.AsEnumerable().GroupBy(x => x.Field<string>("ma_doituong_kcb"))
                    .Select(g => new {g.Key, Count = g.Count()});
                if (counts.Count() >= 2)
                {
                    if (grdList.RootTable.Groups.Count <= 0)
                    {
                        GridEXColumn gridExColumn = grdList.RootTable.Columns["ma_doituong_kcb"];
                        var gridExGroup = new GridEXGroup(gridExColumn);
                        gridExGroup.GroupPrefix = "Nhóm đối tượng KCB: ";
                        grdList.RootTable.Groups.Add(gridExGroup);
                    }
                }
                else
                {
                    GridEXColumn gridExColumn = grdList.RootTable.Columns["ma_doituong_kcb"];
                    var gridExGroup = new GridEXGroup(gridExColumn);
                    grdList.RootTable.Groups.Clear();
                }
                grdList.UpdateData();
                grdList.Refresh();
            }
            catch
            {
            }
        }

        /// <summary>
        ///     hàm thực hiện viecj tìm kiếm thông tin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdTimKiem_Click(object sender, EventArgs e)
        {
            TimKiemThongTin(true);
        }

        /// <summary>
        ///     hàm thực hiện việc thoát Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }


        /// <summary>
        ///     hàm thực hiện việc load thông tin của tiếp đón lên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Quanly_Maukiemnghiem_Load(object sender, EventArgs e)
        {
            ModifyCommand();
            DataTable dtNhomin = THU_VIEN_CHUNG.LayDulieuDanhmucChung("NHOM_INPHIEU_CLS", true);

            AllowTextChanged = true;
            LayDsach_DoituongKCB();

            TimKiemThongTin(true);
            AutoloadSaveAndPrintConfig();
            m_blnHasloaded = true;
        }

        private void LayDsach_DoituongKCB()
        {
            DataBinding.BindDataCombobox(cboObjectType, THU_VIEN_CHUNG.LaydanhsachDoituongKcb(),
                DmucDoituongkcb.Columns.IdDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb,
                "---Chọn đối tượng KCB---", true);
        }

        private void LoadChiDinh()
        {
            string maLuotkham = Utility.sDbnull(grdList.GetValue(KnDangkyXn.Columns.MaDangky));
            int patientId = Utility.Int32Dbnull(grdList.GetValue(KnDangkyXn.Columns.IdKhachhang));
            m_dtChiDinhCLS = SPs.KnLaydanhsachChidinhTheokhachhang(maLuotkham, patientId, 200).GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdDetail, m_dtChiDinhCLS, false, true, "1=1", "");
            UpdateWhanChanged();
            ModifycommandAssignDetail();
        }

        private void UpdateWhanChanged()
        {
            foreach (GridEXRow gridExRow in grdDetail.GetDataRows())
            {
                if (gridExRow.RowType == RowType.Record)
                {
                    gridExRow.BeginEdit();
                    gridExRow.Cells["TT"].Value =
                        Utility.DecimaltoDbnull(gridExRow.Cells[KnChidinhChitiet.Columns.DonGia].Value, 0)*
                        Utility.Int32Dbnull(gridExRow.Cells[KnChidinhChitiet.Columns.SoLuong].Value);
                }
            }
            grdList.UpdateData();
            m_dtChiDinhCLS.AcceptChanges();
            UpdateSumOfChiDinh();
        }

        private void UpdateSumOfChiDinh()
        {
            GridEXColumn gridExColumn = grdDetail.RootTable.Columns["TT"];
            GridEXColumn gridExColumnPhuThu = grdDetail.RootTable.Columns[KcbChidinhclsChitiet.Columns.PhuThu];
            decimal Thanhtien = Utility.DecimaltoDbnull(grdDetail.GetTotal(gridExColumn, AggregateFunction.Sum));
            decimal phuthu = Utility.DecimaltoDbnull(grdDetail.GetTotal(gridExColumnPhuThu, AggregateFunction.Sum));
        }

        private void grdList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    if (m_dataDataRegExam != null) m_dataDataRegExam.Clear();
                    objLuotkham = null;
                    return;
                }
                if (grdList.CurrentRow != null)
                {
                    objLuotkham = CreatePatientExam();
                    LoadChiDinh();
                }
            }
            catch (Exception ex)
            {
                Utility.sDbnull("Lỗi:" + ex.Message);
            }
            finally
            {
                ModifyCommand();
            }
        }

        /// <summary>
        ///     hàm thực hiện việc cáu hình trạng thái của nút
        /// </summary>
        private void ModifyCommand()
        {
            bool CoKhachHang = Utility.isValidGrid(grdList);

            cmdSuaThongTinBN.Enabled = CoKhachHang;
            cmdXoaBenhNhan.Enabled = CoKhachHang;
            cmdThemLanKham.Enabled = CoKhachHang;
        }

        /// <summary>
        ///     hàm thực hiện viecj nhận formating trên lưới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdDetail_FormattingRow(object sender, RowLoadEventArgs e)
        {
            if (e.Row.RowType == RowType.TotalRow)
            {
                e.Row.Cells[DmucDichvuclsChitiet.Columns.TenChitietdichvu].Value = "Tổng cộng :";
            }
        }

        private void frm_Quanly_Maukiemnghiem_FormClosing(object sender, FormClosingEventArgs e)
        {
        }


        private KnDangkyXn CreatePatientExam()
        {
            string maLuotkham = Utility.sDbnull(grdList.CurrentRow.Cells[KnDangkyXn.Columns.MaDangky].Value);
            int patientId = Utility.Int32Dbnull(grdList.CurrentRow.Cells[KnDangkyXn.Columns.IdKhachhang].Value);
            new KnDangkyXn();
            SqlQuery sqlQuery = new Select().From(KnDangkyXn.Schema)
                .Where(KnDangkyXn.Columns.MaDangky).IsEqualTo(maLuotkham)
                .And(KnDangkyXn.Columns.IdKhachhang).IsEqualTo(patientId);
            var knDangkyXn = sqlQuery.ExecuteSingle<KnDangkyXn>();
            return knDangkyXn;
        }

        private void ModifycommandAssignDetail()
        {
            try
            {
                bool coChitiet = Utility.isValidGrid(grdDetail);

                if (!coChitiet)
                {
                    cmdPrintAssign.Enabled =
                        cmdSuaChiDinh.Enabled = cmdXoaChiDinh.Enabled = cmdBangiao.Enabled = cmdInKQ.Enabled = false;
                    return;
                }
                cmdBangiao.Enabled = true;
                int trangthaiChuyencls = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdDetail.CurrentRow, "trangthai_chuyencls"), 0);
                int trangthaiThanhtoan = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdDetail.CurrentRow, "trangthai_thanhtoan"), 0);
                cmdSuaChiDinh.Enabled = trangthaiChuyencls <= 0;
                cmdXoaChiDinh.Enabled = grdDetail.GetCheckedRows().Length > 0 && trangthaiChuyencls <= 0;
                cmdPrintAssign.Enabled = true;
                cmdBangiao.Text = trangthaiChuyencls == 0 ? "Bàn giao mẫu" : "Hủy giao mẫu";
                cmdBangiao.Tag = trangthaiChuyencls == 0 ? 0 : 1;
                cmdInKQ.Enabled = trangthaiThanhtoan > 0 && trangthaiChuyencls >= 4;
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Loi: "+ exception.Message);
            }
        }

        private void cmdThemChiDinh_Click(object sender, EventArgs e)
        {
            KnDangkyXn patientExam = CreatePatientExam();
            if (patientExam != null)
            {
                var frm = new frm_Chidinh_Maukiemnghiem("MAUKIEMNGHIEM", 3);
                frm.Exam_ID = Utility.Int32Dbnull(-1, -1);
                frm.txtAssign_ID.Text = @"-100";
                frm.objLuotkham = patientExam;
                frm.m_eAction = action.Insert;
                frm.HosStatus = 0;
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    LoadChiDinh();
                    UpdateSumOfChiDinh();
                }
                ModifycommandAssignDetail();
            }
            ModifyCommand();
        }

        private bool InValiUpdateChiDinh()
        {
            int assignId = Utility.Int32Dbnull(grdDetail.GetValue(KnChidinhChitiet.Columns.IdChidinh), "-1");
            SqlQuery sqlQuery = new Select().From(KnChidinhXn.Schema)
                .Where(KnChidinhXn.Columns.IdChidinh).IsEqualTo(assignId)
                .And(KnChidinhXn.Columns.TrangthaiThanhtoan).IsGreaterThanOrEqualTo(1);
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg(
                    "Phiếu chỉ định này đã thanh toán, Bạn không được phép sửa(Có thể liên hệ với Quầy thanh toán để hủy thanh toán trước khi sửa lại)",
                    "Thông báo");
                cmdThemChiDinh.Focus();
                return false;
            }
            return true;
        }

        private void cmdSuaChiDinh_Click(object sender, EventArgs e)
        {
            KnDangkyXn patientExam = CreatePatientExam();
            if (patientExam != null)
            {
                if (!InValiUpdateChiDinh()) return;
                var frm = new frm_Chidinh_Maukiemnghiem("MAUKIEMNGHIEM", 3);
                frm.HosStatus = 0;

                frm.Exam_ID = Utility.Int32Dbnull(-1, -1);
                frm.objLuotkham = CreatePatientExam();
                frm.m_eAction = action.Update;
                frm.txtAssign_ID.Text = Utility.sDbnull(grdDetail.GetValue(KnChidinhChitiet.Columns.IdChidinh), "-1");
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    //  LoadChiDinhCLS();
                    LoadChiDinh();
                    UpdateSumOfChiDinh();
                }
                ModifycommandAssignDetail();
            }
            ModifyCommand();
        }

        private bool InValiAssign()
        {
            bool bCancel = false;
            if (grdDetail.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn một bản ghi thực hiện xóa chỉ định kiểm nghiệm", "Thông báo",
                    MessageBoxIcon.Warning);
                grdDetail.Focus();
                return false;
            }
            foreach (GridEXRow gridExRow in grdDetail.GetCheckedRows())
            {
                int assignDetailId =
                    Utility.Int32Dbnull(gridExRow.Cells[KnChidinhChitiet.Columns.IdChidinhChitiet].Value, -1);
                KnChidinhChitiet objchitiet = KnChidinhChitiet.FetchByID(assignDetailId);
                if (!globalVariables.IsAdmin)
                {
                    if (objchitiet != null && objchitiet.NguoiTao != globalVariables.UserName)
                    {
                        Utility.ShowMsg(
                            "Trong các chỉ định bạn chọn xóa, có một số chỉ định được kê bởi nhân viên khác nên bạn không được phép xóa." +
                            " Mời bạn chọn lại chỉ các chỉ định do chính bạn kê để thực hiện xóa");
                        return false;
                    }
                }
                if (objchitiet != null && Utility.ByteDbnull(objchitiet.TrangThai, 0) >= 1)
                {
                    Utility.ShowMsg(
                        "Chỉ định bạn chọn đã được chuyển làm kiểm nghiệm hoặc đã có kết quả nên không thể xóa. Đề nghị kiểm tra lại");
                    return false;
                }
            }
            return true;
        }

        private void PerforActionDeleteAssign()
        {
            long idchidinh = -1;
            foreach (GridEXRow gridExRow in grdDetail.GetCheckedRows())
            {
                long assignDetail =
                    Utility.Int64Dbnull(gridExRow.Cells[KnChidinhChitiet.Columns.IdChidinhChitiet].Value, -1);
                idchidinh = Utility.Int64Dbnull(gridExRow.Cells[KnChidinhChitiet.Columns.IdChidinh].Value, -1);
                new Delete().From(KnChidinhChitiet.Schema)
                    .Where(KnChidinhChitiet.Columns.IdChidinhChitiet)
                    .IsEqualTo(assignDetail)
                    .Execute();
                gridExRow.Delete();
                grdDetail.UpdateData();
                grdDetail.Refresh();
                m_dtChiDinhCLS.AcceptChanges();
            }

            if (grdDetail.GetDataRows().Length <= 0)
            {
                new Delete().From(KnChidinhXn.Schema)
                    .Where(KnChidinhXn.Columns.IdChidinh)
                    .IsEqualTo(idchidinh)
                    .Execute();
            }
            UpdateSumOfChiDinh();
        }

        private void cmdXoaChiDinh_Click(object sender, EventArgs e)
        {
            try
            {
                if (!InValiAssign()) return;
                string[] query = (from chidinh in grdDetail.GetCheckedRows().AsEnumerable()
                    let x = Utility.sDbnull(chidinh.Cells[DmucDichvuclsChitiet.Columns.TenChitietdichvu].Value)
                    select x).ToArray();
                string serviceDetailName = string.Join("; ", query);
                string question = string.Format("Bạn có muốn xóa thông tin chỉ định {0} \n đang chọn không",
                    serviceDetailName);
                if (Utility.AcceptQuestion(question, "Thông báo", true))
                {
                    PerforActionDeleteAssign();
                    //ModifyCommmand();
                    ModifycommandAssignDetail();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
        }

        private void grdDetail_ColumnHeaderClick(object sender, ColumnActionEventArgs e)
        {
            ModifycommandAssignDetail();
        }

        private void grdDetail_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
            ModifycommandAssignDetail();
        }

        private void txtTongChiPhiKham_TextChanged(object sender, EventArgs e)
        {
            //Utility.FormatCurrencyHIS(txtTongChiPhiKham);
        }

        private void cmdThemMoiBN_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new frm_Dangky_Kiemnghiem(Args);
               // frm._OnAssign += frm__OnAssign;
                frm.m_enAction = action.Insert;
                frm.m_dtPatient = m_dtPatient;

                frm._OnActionSuccess += frm__OnActionSuccess;
                frm.grdList = grdList;
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    UpdateGroup();
                    grdList_SelectionChanged(grdList, new EventArgs());
                }
                ModifyCommand();
                ModifycommandAssignDetail();
            }
            catch (Exception exception)
            {
            }
            finally
            {
                // CauHinh();
            }
        }

        //private void frm__OnAssign()
        //{
        //    cmdThemChiDinh.PerformClick();
        //}

        private void frm__OnActionSuccess()
        {
            UpdateGroup();
        }

        /// <summary>
        ///     hàm thục hiện việc thêm lần đăng ký
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdThemLanKham_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn phải chọn khách hàng để thêm lượt đăng ký mới");
                    return;
                }
                DataTable _temp =
                    _KCB_DANGKY.KcbLaythongtinBenhnhan(
                        Utility.Int64Dbnull(grdList.GetValue(KnDangkyXn.Columns.IdKhachhang)));
                if (_temp != null && Utility.ByteDbnull(_temp.Rows[0][KnDangkyXn.Columns.TrangThai], 0) > 0 &&
                    Utility.ByteDbnull(_temp.Rows[0][KnDangkyXn.Columns.TrangThai], 0) < 4)
                {
                    Utility.ShowMsg(
                        "khách hàng đang ở trạng thái xét nghiệm và chưa hoàn thành nên không thể thêm lần đăng ký mới. Đề nghị bạn xem lại");
                    return;
                }
                var frm = new frm_Dangky_Kiemnghiem(Args);
               // frm._OnAssign += frm__OnAssign;
                frm.txtMaBN.Text = Utility.sDbnull(grdList.GetValue(KnDangkyXn.Columns.IdKhachhang));
                frm.txtMaLankham.Text = Utility.sDbnull(grdList.GetValue(KnDangkyXn.Columns.MaDangky));

                frm.m_enAction = action.Add;
                frm._OnActionSuccess += frm__OnActionSuccess;
                frm.m_dtPatient = m_dtPatient;
                frm.grdList = grdList;
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    UpdateGroup();
                    grdList_SelectionChanged(grdList, new EventArgs());
                }
                ModifyCommand();
                ModifycommandAssignDetail();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
        }

        /// <summary>
        ///     hàm thực hiện sửa thông tin của khách hàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSuaThongTinBN_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn phải chọn ít nhất 1 khách hàng để sửa thông tin");
                    return;
                }

                var frm = new frm_Dangky_Kiemnghiem(Args);
                frm.txtMaBN.Text = Utility.sDbnull(grdList.GetValue(KnDangkyXn.Columns.IdKhachhang));
                frm.txtMaLankham.Text = Utility.sDbnull(grdList.GetValue(KnDangkyXn.Columns.MaDangky));

                frm._OnActionSuccess += frm__OnActionSuccess;
                frm.m_enAction = action.Update;
                frm.m_dtPatient = m_dtPatient;
                frm.grdList = grdList;
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    UpdateGroup();
                    grdList_SelectionChanged(grdList, new EventArgs());
                }
                ModifyCommand();
                ModifycommandAssignDetail();
            }
            catch (Exception)
            {
            }
            finally
            {
                //CauHinh();
            }
        }

        private void frm_Quanly_Maukiemnghiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11) Utility.ShowMsg(ActiveControl.Name);
            if (e.KeyCode == Keys.F3) cmdTimKiem.PerformClick();
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.Escape) Close();

            if (e.KeyCode == Keys.N && e.Control) cmdThemMoiBN.PerformClick();
            if (e.KeyCode == Keys.U && e.Control) cmdSuaThongTinBN.PerformClick();
            if (e.KeyCode == Keys.D && e.Control) cmdXoaBenhNhan.PerformClick();
            if (e.KeyCode == Keys.K && e.Control) cmdThemLanKham.PerformClick();
        }

        /// <summary>
        ///     hàm thực hiện việc xóa thông tin khách hàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdXoaBenhNhan_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn phải chọn ít nhất 1 khách hàng để xóa");
                    return;
                }
                string ErrMgs = "";
                string vMaLuotkham =
                    Utility.sDbnull(
                        grdList.GetValue(KnDangkyXn.Columns.MaDangky),
                        "");
                int vPatientId =
                    Utility.Int32Dbnull(grdList.GetValue(KnDangkyXn.Columns.IdKhachhang), -1);

                if (!IsValidDeleteData()) return;
                if (Utility.AcceptQuestion("Bạn có muốn xóa thông tin lần đăng ký này không", "Thông báo", true))
                {
                    ActionResult actionResult = _KCB_DANGKY.PerformActionDeletePatientExam(null, vMaLuotkham,
                        vPatientId, ref ErrMgs);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            try
                            {
                                grdList.CurrentRow.BeginEdit();
                                grdList.CurrentRow.Delete();
                                grdList.CurrentRow.EndEdit();
                                grdList.UpdateData();
                                grdList_SelectionChanged(grdList, e);
                            }
                            catch
                            {
                            }
                            m_dtPatient.AcceptChanges();
                            UpdateGroup();
                            //Utility.ShowMsg("Xóa lần đăng ký thành công", "Thành công");
                            break;
                        case ActionResult.Exception:
                            if (ErrMgs != "")
                                Utility.ShowMsg(ErrMgs);
                            else
                                Utility.ShowMsg(
                                    "khách hàng đã có thông tin chỉ định dịch vụ hoặc đơn thuốc... /n bạn không thể xóa lần đăng ký này",
                                    "Thông báo");
                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Có lỗi trong quá trình xóa thông tin", "Thông báo");
                            break;
                    }
                }

                ModifyCommand();
            }
            catch
            {
            }
            finally
            {
            }
        }

        private bool IsValidDeleteData()
        {
            string vMaLuotkham = Utility.sDbnull( grdList.GetValue(KnDangkyXn.Columns.MaDangky), "");
            int vPatientId = Utility.Int32Dbnull(grdList.GetValue(KnDangkyXn.Columns.IdKhachhang), -1);
            if (objLuotkham != null)
            {
                if (objLuotkham.TrangThai > 0)
                {
                    Utility.ShowMsg("khách hàng đang chọn đã được chọn để thực hiện kiểm nghiệm nên bạn không được phép xóa");
                    return false;
                }
            }
            SqlQuery sqlQuery = new Select().From(KcbDangkyKcb.Schema)
                .Where(KcbDangkyKcb.Columns.MaLuotkham).IsEqualTo(vMaLuotkham)
                .And(KcbDangkyKcb.Columns.IdBenhnhan).IsEqualTo(vPatientId)
                .And(KcbDangkyKcb.Columns.TrangthaiThanhtoan).IsEqualTo(1);
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Bản ghi trong đăng ký khám đã thanh toán, Bạn không xóa được");
                return false;
            }
            sqlQuery = new Select().From(KnChidinhChitiet.Schema)
                .Where(KnChidinhChitiet.Columns.IdChidinh).In(
                    new Select(KnChidinhXn.Columns.IdChidinh).From(KnChidinhXn.Schema).Where(
                        KnChidinhXn.Columns.MaDangky).IsEqualTo(vMaLuotkham).And(KnChidinhXn.Columns.IdKhachhang).
                        IsEqualTo(vPatientId))
                .And(KnChidinhChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1);
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg(
                    "Bạn không thể xóa khách hàng trên vì khách hàng đã thanh toán một số dịch vụ kiểm nghiệm",
                    "Thông báo");
                return false;
            }
            return true;
        }

        /// <summary>
        ///     ham thực hiện viecj in phiếu cỉnh định
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInPhieuChiDinh_Click(object sender, EventArgs e)
        {
            if (grdDetail.CurrentRow != null)
            {
                int v_AssignId = Utility.Int32Dbnull(grdDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), -1);
                KcbChidinhcl objAssignInfo = KcbChidinhcl.FetchByID(v_AssignId);
                //if (objAssignInfo != null)
                //{
                //    frm_INPHIEU_CLS frm = new frm_INPHIEU_CLS();
                //    frm.objAssignInfo = KcbChidinhcl.FetchByID(v_AssignId);
                //    frm.ShowDialog();
                //}
            }
        }

        /// <summary>
        ///     hàm thực hiện viec lọc thông in trên lưới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdList_ApplyingFilter(object sender, CancelEventArgs e)
        {
            ModifyCommand();
        }

        private void grdList_AddingRecord(object sender, CancelEventArgs e)
        {
            ModifyCommand();
        }


        private void grdDetail_SelectionChanged(object sender, EventArgs e)
        {
            ModifycommandAssignDetail();
        }

        private void txtPatientCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                errorProvider1.Clear();
                var dtPatient = new DataTable();
                if (e.KeyCode == Keys.Enter && Utility.DoTrim(txtPatientCode.Text.Trim()) != "")
                {
                    string _ID = txtPatient_ID.Text.Trim();
                    string _Code = "KN" + Utility.GetYY(DateTime.Now) +
                                   Utility.FormatNumberToString(Utility.Int32Dbnull(txtPatientCode.Text, 0), "000000");
                    txtPatient_ID.Clear();
                    txtPatientCode.Text = _Code;
                    TimKiemThongTin(false);
                    if (grdList.RowCount == 1) grdList_SelectionChanged(grdList, new EventArgs());
                    txtPatient_ID.Text = _ID;
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy thông tin khách hàng");
                //throw;
            }
        }

        private void txtPatient_ID_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                errorProvider1.Clear();
                var dtPatient = new DataTable();
                if (e.KeyCode == Keys.Enter && Utility.DoTrim(txtPatient_ID.Text.Trim()) != "")
                {
                    string _code = txtPatientCode.Text.Trim();
                    txtPatientCode.Clear();
                    TimKiemThongTin(false);
                    if (grdList.RowCount == 1) grdList_SelectionChanged(grdList, new EventArgs());
                    txtPatientCode.Text = _code;
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy thông tin khách hàng");
                //throw;
            }
        }

        #region "Thông tin khám chữa bệnh"

        private readonly string strSaveandprintPath = Application.StartupPath + @"\CAUHINH\SaveAndPrintConfig.txt";

        private readonly string strSaveandprintPath1 = Application.StartupPath +
                                                       @"\CAUHINH\DefaultPrinter_PhieuHoaSinh.txt";

        private string m_strDefaultLazerPrinterName = "";
        private KnDangkyXn objLuotkham;

        private void Try2CreateFolder()
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(strSaveandprintPath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(strSaveandprintPath));
            }
            catch
            {
            }
        }

        private void AutoloadSaveAndPrintConfig()
        {
            try
            {
                Try2CreateFolder();

                using (var _reader = new StreamReader(strSaveandprintPath1))
                {
                    m_strDefaultLazerPrinterName = _reader.ReadLine().Trim();

                    _reader.BaseStream.Flush();
                    _reader.Close();
                }
            }
            catch
            {
            }
            finally
            {
            }
        }

        #endregion

        private void cmdXoaBenhNhan_Click_1(object sender, EventArgs e)
        {

        }

    }
}