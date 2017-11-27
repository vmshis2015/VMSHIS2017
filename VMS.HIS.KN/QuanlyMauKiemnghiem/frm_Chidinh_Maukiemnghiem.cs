using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using Microsoft.SqlServer.Server;
using NLog;
using SubSonic;
using SubSonic.Sugar;
using VMS.HIS.KN.Classess;
using VNS.HIS.UI.DANHMUC;
using VNS.Libs;
using VNS.HIS.DAL;
using SortOrder = Janus.Windows.GridEX.SortOrder;
using Strings = Microsoft.VisualBasic.Strings;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;
using VNS.HIS.Classes;
namespace VMS.HIS.KN.NGOAITRU
{
    public partial class frm_Chidinh_Maukiemnghiem : Form
    {
        private readonly Logger log;
        private int CurrentRowIndex = -1;
        public int Exam_ID = -1;
        decimal BHYT_PTRAM_TRAITUYENNOITRU=0;
        private string Help = "";
        KCB_CHIDINH_CANLAMSANG CHIDINH_CANLAMSANG = new KCB_CHIDINH_CANLAMSANG();
        KCB_THAMKHAM __THAMKHAM = new KCB_THAMKHAM();

        private int ServiceDetail_Id;
        bool m_blnAllowSelectionChanged = false;
        private ActionResult actionResult = ActionResult.Error;
        public bool m_blnCancel=true;


        private bool isSaved;
        private int lastIndex;
        private char lastKey;
        private DataTable m_dtChitietPhieuCLS = new DataTable();
        private DataTable m_dtReport = new DataTable();
        public DataTable m_dtDanhsachDichvuCLS = new DataTable();
        public action m_eAction = action.Insert;
        private bool neverFound = true;
        public KnDangkyXn objLuotkham;
        public KnDanhsachKhachhang  objBenhnhan;
        public DataTable p_AssignInfo;
        private string rowFilter = "1=1";
        private string strQuestion = "";
        private string strSaveandprintPath = Application.StartupPath + @"\CAUHINH\SaveAndPrintConfigKedonthuoc.txt";
        private int v_AssignId = -1;
        string nhomchidinh = "";
        byte kieu_chidinh = 3;
        DmucDichvuclsChitiet objMau = null;
        DmucDichvucl objDichvu = null;
        #region "khai báo khởi tạo ban đầu"

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nhomchidinh">Mô tả thêm của nhóm chỉ định CLS dùng cho việc tách form kê chỉ định CLS và kê gói dịch vụ</param>
        /// <param name="kieuChidinh">Kiểu chỉ định</param>
        public frm_Chidinh_Maukiemnghiem(string nhomchidinh, byte kieuChidinh)
        {
            InitializeComponent();
            InitEvents();
            this.nhomchidinh = nhomchidinh;
            this.kieu_chidinh = kieuChidinh;
            
            log = LogManager.GetCurrentClassLogger();
            dtRegDate.Value =dtpNgaytraKQ.Value= globalVariables.SysDate;
            if (globalVariables.gv_UserAcceptDeleted) FormatUserNhapChiDinh();
            CauHinh();
        }

        void InitEvents()
        {
            Load += new EventHandler(frm_Chidinh_Maukiemnghiem_Load);
            KeyDown += new KeyEventHandler(frm_Chidinh_Maukiemnghiem_KeyDown);
            FormClosing += new FormClosingEventHandler(frm_Chidinh_Maukiemnghiem_FormClosing);

            grdServiceDetail.CellValueChanged += new ColumnActionEventHandler(grdServiceDetail_CellValueChanged);
            grdServiceDetail.KeyDown += new KeyEventHandler(grdServiceDetail_KeyDown);
            grdServiceDetail.KeyPress += new KeyPressEventHandler(grdServiceDetail_KeyPress);
            grdServiceDetail.SelectionChanged += new EventHandler(grdServiceDetail_SelectionChanged);
            grdServiceDetail.UpdatingCell += new UpdatingCellEventHandler(grdServiceDetail_UpdatingCell);

            grdAssignDetail.CellUpdated += new ColumnActionEventHandler(grdAssignDetail_CellUpdated);
            grdAssignDetail.CellValueChanged += new ColumnActionEventHandler(grdAssignDetail_CellValueChanged);
            grdAssignDetail.ColumnHeaderClick += new ColumnActionEventHandler(grdAssignDetail_ColumnHeaderClick);
            grdAssignDetail.FormattingRow += new RowLoadEventHandler(grdAssignDetail_FormattingRow);
            grdAssignDetail.UpdatingCell += new UpdatingCellEventHandler(grdAssignDetail_UpdatingCell);
            grdAssignDetail.KeyDown += new KeyEventHandler(grdAssignDetail_KeyDown);
          //  cmdAddDetail.Click += new EventHandler(cmdAddDetail_Click);
            cmdCauHinh.Click += new EventHandler(cmdCauHinh_Click);

            chkSaveAndPrint.CheckedChanged += new EventHandler(chkSaveAndPrint_CheckedChanged);

            cmdExit.Click += new EventHandler(cmdExit_Click);
            cmdSave.Click += new EventHandler(cmdSave_Click);
            cmdInPhieuCLS.Click += new EventHandler(cmdInPhieuCLS_Click);
            cmdDelete.Click += new EventHandler(cmdDelete_Click);
            mnuDelete.Click += new EventHandler(mnuDelete_Click);
            txtMaukiemnghiem._OnEnterMe += txtMaukiemnghiem__OnEnterMe;
            txtTinhtrangmau._OnShowData += txtTinhtrangmau__OnShowData;
        }

        void txtTinhtrangmau__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtTinhtrangmau.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtTinhtrangmau.myCode;
                txtTinhtrangmau.Init();
                txtTinhtrangmau.SetCode(oldCode);
                txtTinhtrangmau.Focus();
            }
        }

        void txtMaukiemnghiem__OnEnterMe()
        {
            objMau = DmucDichvuclsChitiet.FetchByID(Utility.Int32Dbnull(txtMaukiemnghiem.MyID, -1));
            if (objMau != null)
                objDichvu = DmucDichvucl.FetchByID(objMau.IdDichvu.ToString());
            else
                objDichvu = null;
            if (objDichvu != null)
            {
                int songay = Utility.Int32Dbnull(objDichvu.SongayTraketqua, 7);
                if (songay <= 0) songay = 7;
               dtpNgaytraKQ.Value= globalVariables.SysDate.AddDays(songay);
            }
            if (Utility.Int16Dbnull(txtMaukiemnghiem.MyID, -1) > 0)
            {
                m_dtDanhsachDichvuCLS = CHIDINH_CANLAMSANG.MaukiemnghiemLaydanhsachdvukiemnghiem(Utility.Int16Dbnull(txtMaukiemnghiem.MyID, -1));
                if (!m_dtDanhsachDichvuCLS.Columns.Contains(KnChidinhChitiet.Columns.SoLuong))
                    m_dtDanhsachDichvuCLS.Columns.Add(KnChidinhChitiet.Columns.SoLuong, typeof(int));
                if (!m_dtDanhsachDichvuCLS.Columns.Contains("ten_donvitinh"))
                    m_dtDanhsachDichvuCLS.Columns.Add("ten_donvitinh", typeof(string));
                m_dtDanhsachDichvuCLS.AcceptChanges();
                Utility.SetDataSourceForDataGridEx(grdChitietDichvu, m_dtDanhsachDichvuCLS, false, true, "", "");
                GridEXColumn gridExColumnGroupIntOrder = grdChitietDichvu.RootTable.Columns["stt_hthi_dichvu"];
                GridEXColumn gridExColumnIntOrder = grdChitietDichvu.RootTable.Columns["stt_hthi_chitiet"];
                Utility.SetGridEXSortKey(grdChitietDichvu, gridExColumnGroupIntOrder, SortOrder.Ascending);
                Utility.SetGridEXSortKey(grdChitietDichvu, gridExColumnIntOrder, SortOrder.Ascending);
            }
        }

        void grdAssignDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Control && e.KeyCode == Keys.Delete) || e.KeyCode == Keys.Delete)
                mnuDelete_Click(mnuDelete, new EventArgs());
        }
        #endregion

        public int HosStatus { get; set; }
        public KcbDangkyKcb ObjRegExam { get; set; }
        public NoitruPhieudieutri objPhieudieutriNoitru { get; set; }
        // public KnDangkyXn objLuotkham;

        private void CauHinh()
        {
            if (PropertyLib._ThamKhamProperties != null)
            {
                chkSaveAndPrint.Checked = PropertyLib._MayInProperties.InCLSsaukhiluu;
            }
        }
        private void frm_Chidinh_Maukiemnghiem_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (!isSaved) cmdSave_Click(cmdSave, new EventArgs());
            if (grdAssignDetail.RowCount > 0)
            {
                if (!isSaved)
                {
                    if (m_dtChitietPhieuCLS.Select("NoSave=1").Length > 0)
                    {
                        if (
                            Utility.AcceptQuestion(
                                "Bạn đã thêm mới một số chỉ định chi tiết mà chưa nhấn Ghi.\nBạn nhấn yes để hệ thống tự động lưu thông tin.\nNhấn No để hủy bỏ các chỉ định vừa thêm mới.", "Thông báo",
                                true))
                        {
                            cmdSave.PerformClick();
                        }
                    }
                }
            }
            SaveCauHinh();
        }

        private void SaveCauHinh()
        {
            if (PropertyLib._ThamKhamProperties != null)
            {
                PropertyLib._MayInProperties.InCLSsaukhiluu = chkSaveAndPrint.Checked;
                PropertyLib.SaveProperty(PropertyLib._ThamKhamProperties);
                PropertyLib.SaveProperty(PropertyLib._MayInProperties);
            }
        }
        private void FormatUserNhapChiDinh()
        {
            try
            {
                GridEXColumn gridExColumn =
                    grdAssignDetail.RootTable.Columns[KnChidinhChitiet.Columns.NguoiTao];
                GridEXColumn gridExColumnTarget =
                    grdAssignDetail.RootTable.Columns[DmucDichvuclsChitiet.Columns.TenChitietdichvu];
                var gridExFormatCondition = new GridEXFormatCondition(gridExColumn, ConditionOperator.NotEqual,
                                                                      globalVariables.UserName);
                gridExFormatCondition.FormatStyle.BackColor = Color.Red;
                gridExFormatCondition.TargetColumn = gridExColumnTarget;
                grdAssignDetail.RootTable.FormatConditions.Add(gridExFormatCondition);
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
            }
        }
        /// <summary>
        /// hàm thực hiện việc đống form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// hàm hự hiện việc load form hiện tại lên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Chidinh_Maukiemnghiem_Load(object sender, EventArgs e)
        {
            try
            {
                LaydanhsachbacsiChidinh();
                BHYT_PTRAM_TRAITUYENNOITRU =
                    Utility.DecimaltoDbnull(
                        THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", false), 0m);
                InitData();
                GetData();
                if (m_eAction == action.Update)
                {
                    LoadNhomin();
                }
            }
            catch (Exception ex)
            {
                log.Trace(ex.ToString);
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
            finally
            {
                v_AssignId = Utility.Int32Dbnull(txtAssign_ID.Text, -1);
            }
        }
        private void LaydanhsachbacsiChidinh()
        {
            try
            {
                DataTable data = THU_VIEN_CHUNG.LaydanhsachBacsi(-1, HosStatus);
                txtBacsi.Init(data, new List<string>() { DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.MaNhanvien, DmucNhanvien.Columns.TenNhanvien });
                if (globalVariables.gv_intIDNhanvien <= 0)
                {
                    txtBacsi.SetId(-1);
                }
                else
                {
                    txtBacsi.SetCode(globalVariables.UserName);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
        }
        /// <summary>
        /// hàm thực hiện việc xóa thông tin trên lưới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidDataXoaCLS()) return;
                long idchidinh = -1;
                foreach (GridEXRow gridExRow in grdAssignDetail.GetCheckedRows())
                {
                    long assignDetail =
                        Utility.Int64Dbnull(gridExRow.Cells[KnChidinhChitiet.Columns.IdChidinhChitiet].Value, -1);
                    idchidinh = Utility.Int64Dbnull(gridExRow.Cells[KnChidinhChitiet.Columns.IdChidinh].Value, -1);
                    new Delete().From(KnChidinhChitiet.Schema)
                        .Where(KnChidinhChitiet.Columns.IdChidinhChitiet)
                        .IsEqualTo(assignDetail)
                        .Execute();
                    gridExRow.Delete();
                    grdAssignDetail.UpdateData();
                    grdAssignDetail.Refresh();
                    m_dtChitietPhieuCLS.AcceptChanges();
                }

                if (grdAssignDetail.GetDataRows().Length <= 0)
                {
                    m_eAction = action.Insert;
                    txtAssign_ID.Text = @"(Tự sinh)";
                    txtAssignCode.Text = THU_VIEN_CHUNG.SinhMaChidinhCLS();
                    new Delete().From(KnChidinhXn.Schema)
                        .Where(KnChidinhXn.Columns.IdChidinh)
                        .IsEqualTo(idchidinh)
                        .Execute();
                }
                m_blnCancel = false;
                ModifyCommand();
                ModifyButtonCommand();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
        }

        /// <summary>
        /// hàm thực hiện kiểm tra thông tin chỉ dịnh kiểm nghiệm
        /// </summary>
        /// <returns></returns>
        private bool IsValidDataXoaCLS()
        {
            if (grdAssignDetail.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải thực hiện chọn một bản ghi thực hiện xóa thông tin dịch vụ kiểm nghiệm",
                                "Thông báo", MessageBoxIcon.Warning);
                grdAssignDetail.Focus();
                return false;
            }
            foreach (GridEXRow gridExRow in grdAssignDetail.GetCheckedRows())
            {
                int assignDetailId =
                    Utility.Int32Dbnull(gridExRow.Cells[KnChidinhChitiet.Columns.IdChidinhChitiet].Value, -1);
                KnChidinhChitiet objchitiet = KnChidinhChitiet.FetchByID(assignDetailId);
                if (!globalVariables.IsAdmin)
                {
                    if (objchitiet != null && objchitiet.NguoiTao != globalVariables.UserName)
                    {
                        Utility.ShowMsg("Trong các chỉ định bạn chọn xóa, có một số chỉ định được kê bởi nhân viên khác nên bạn không được phép xóa." +
                                        " Mời bạn chọn lại chỉ các chỉ định do chính bạn kê để thực hiện xóa");
                        return false;

                    }
                }
                if (objchitiet != null && Utility.ByteDbnull(objchitiet.TrangthaiThanhtoan, 0) > 0)
                {
                    Utility.ShowMsg("Chỉ định bạn chọn đã được thanh toán nên bạn không thể xóa. Đề nghị kiểm tra lại");
                    return false;

                }
                if (objchitiet != null && Utility.ByteDbnull(objchitiet.TrangThai, 0) >= 1)
                {
                    Utility.ShowMsg("Chỉ định bạn chọn đã được chuyển làm kiểm nghiệm hoặc đã có kết quả nên không thể xóa. Đề nghị kiểm tra lại");
                    return false;

                }
            }

            return true;
        }

        private bool IsValidDataXoaCLS_Selected()
        {
            if (grdAssignDetail.RowCount <= 0 || grdAssignDetail.CurrentRow.RowType != RowType.Record)
            {
                Utility.ShowMsg("Bạn phải thực hiện chọn một bản ghi thực hiện xóa thông tin dịch vụ kiểm nghiệm",
                                "Thông báo", MessageBoxIcon.Warning);
                grdAssignDetail.Focus();
                return false;
            }
            int AssignDetail_ID =
                   Utility.Int32Dbnull(grdAssignDetail.CurrentRow.Cells[KnChidinhChitiet.Columns.IdChidinhChitiet].Value,
                                       -1);
            KnChidinhChitiet objchitiet = KnChidinhChitiet.FetchByID(AssignDetail_ID);
            if (!globalVariables.IsAdmin)
            {
                if (objchitiet != null && objchitiet.NguoiTao != globalVariables.UserName)
                {
                    Utility.ShowMsg("Trong các chỉ định bạn chọn xóa, có một số chỉ định được kê bởi Bác sĩ khác nên bạn không được phép xóa. Mời bạn chọn lại chỉ các chỉ định do chính bạn kê để thực hiện xóa");
                    return false;

                }
            }
            if (objchitiet != null && Utility.ByteDbnull(objchitiet.TrangthaiThanhtoan, 0) > 0)
            {
                Utility.ShowMsg("Chỉ định bạn chọn đã được thanh toán nên bạn không thể xóa. Đề nghị kiểm tra lại");
                return false;

            }
            if (objchitiet != null && Utility.ByteDbnull(objchitiet.TrangThai, 0) >= 1)
            {
                Utility.ShowMsg("Chỉ định bạn chọn đã được chuyển làm kiểm nghiệm hoặc đã có kết quả nên không thể xóa. Đề nghị kiểm tra lại");
                return false;

            }
            return true;
        }

        /// <summary>
        /// hàm thực hiện việc lưu lại thông tin của dịch vụ kiểm nghiệm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (!IsValidData()) return;
            isSaved = true;
            SetSaveStatus();
            PerformAction();
        }

        /// <summary>
        /// hàm thực hiện việc kiểm tra lại thông tin 
        /// </summary>
        /// <returns></returns>
        private bool IsValidData()
        {
            if (Utility.Int32Dbnull(txtBacsi.MyID, -1) <= 0)
            {
                Utility.SetMsg(uiStatusBar1.Panels["lblStatus"], "Bạn cần chọn người chỉ định trước khi thực hiện lưu mẫu kiểm nghiệm", true);
                txtBacsi.Focus();
                return false;
            }
            if (objPhieudieutriNoitru != null)
            {
                if (dtRegDate.Value.Date > objPhieudieutriNoitru.NgayDieutri.Value.Date)
                {
                    Utility.ShowMsg("Ngày kê đơn phải <= " + objPhieudieutriNoitru.NgayDieutri.Value.ToString("dd/MM/yyyy"));
                    dtRegDate.Focus();
                    return false;
                }
            }
            if (grdAssignDetail.RowCount <= 0)
            {
                Utility.ShowMsg("Hiện không có bản ghi nào thực hiện việc lưu lại thông tin", "Thông báo",
                                MessageBoxIcon.Warning);
                cmdSave.Focus();
                return false;
            }

            return true;
        }

        private void ModifyCommand()
        {
            try
            {
                cmdSave.Enabled = grdAssignDetail.RowCount > 0;
                cmdDelete.Enabled = grdAssignDetail.RowCount > 0;
                cmdInPhieuCLS.Enabled = grdAssignDetail.RowCount > 0;
                ModifyButtonCommand();
            }
            catch (Exception ex)
            {
                log.Error("Loi trong qua trinh trang thai cua nut {0}", ex);
            }
        }

        /// <summary>
        /// hàm thục hiện việc trạng thái của hoạt động của 
        /// 
        /// </summary>
        private void PerformAction()
        {
            try
            {
                Utility.EnableButton(cmdSave, false);
                switch (m_eAction)
                {
                    case action.Insert:
                        InsertDataCls();
                        break;
                    case action.Update:
                        UpdateDataCLS();
                        break;
                }
               
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
            finally
            {
                m_blnCancel = false;
                ModifyCommand();
                Utility.EnableButton(cmdSave, true);
            }
        }

       

        /// <summary>
        /// hàm thực hiện việc thêm mới thông tin kiểm nghiệm
        /// </summary>
        private void InsertDataCls()
        {
            KnChidinhXn objKnChidinhXns = TaoPhieuchidinh();
            actionResult =
               CHIDINH_CANLAMSANG.InsertDataChiDinhKn(
                    objKnChidinhXns, objLuotkham, TaoChitietchidinh());
            switch (actionResult)
            {
                case ActionResult.Success:
                    if (objKnChidinhXns != null)
                    {
                        txtAssign_ID.Text = Utility.sDbnull(objKnChidinhXns.IdChidinh);
                        txtAssignCode.Text = Utility.sDbnull(objKnChidinhXns.MaChidinh);
                        barcode1.Data = txtAssignCode.Text;
                    }
                    m_eAction = action.Update;
                    m_blnCancel = false;
                    //LayThongtinKiemnghiemChitietTheophieu();
                    if (chkSaveAndPrint.Checked)
                    {
                        Utility.EnableButton(cmdSave, false);
                        GetData();
                        LoadNhomin();
                        modifyRegions();
                        Utility.EnableButton(cmdSave, true);
                    }
                    else
                        Close();
                    break;
                case ActionResult.Error:
                    Utility.ShowMsg("Lỗi trong quá trình thêm mới thông tin", "Thông báo", MessageBoxIcon.Error);
                    break;
            }
            Utility.EnableButton(cmdSave, true);
            ModifyCommand();
        }
        bool hasLoadPrintType = false;
        void LoadNhomin()
        {
            if (hasLoadPrintType) return;
            hasLoadPrintType = true;         
        }
        void modifyRegions()
        {
            pnlQuickSearch.Height = 0;
            pnlLeft.Width = 0;
            cmdSave.Visible = false;
            cmdDelete.Visible = false;
        }
        /// <summary>
        /// hmf thực hiện cập n hập thông tin kiểm nghiệm
        /// </summary>
        private void UpdateDataCLS()
        {
            KnChidinhXn objKnChidinhXns = TaoPhieuchidinh();
            actionResult =
                CHIDINH_CANLAMSANG.UpdateDataChiDinhKiemNghiem(
                    objKnChidinhXns, objLuotkham, TaoChitietchidinh());
            switch (actionResult)
            {
                case ActionResult.Success:
                    m_blnCancel = false;
                    if (chkSaveAndPrint.Checked)
                    {
                      
                    }
                    //Utility.ShowMsg("Bạn thực hiện sửa chỉ định thành công", "Thông báo");
                    Close();
                    break;
                case ActionResult.Error:
                    Utility.ShowMsg("Lỗi trong quá trình sửa mới thông tin", "Thông báo", MessageBoxIcon.Error);
                    break;
            }
            Utility.EnableButton(cmdSave, true);
        }

        /// <summary>
        /// hàm thực hiện việc khởi tạo thông tin của phiếu chỉ định kiểm nghiệm
        /// </summary>
        /// <returns></returns>
        private KnChidinhXn TaoPhieuchidinh()
        {
            KnChidinhXn objKnChidinhXns = null;
            objKnChidinhXns = KnChidinhXn.FetchByID(Utility.Int32Dbnull(txtAssign_ID.Text, -1));
            if (objKnChidinhXns != null)
            {
                objKnChidinhXns.IsNew = false;
                objKnChidinhXns.MarkOld();
            }
            else
            {
                objKnChidinhXns = new KnChidinhXn();
                objKnChidinhXns.IsNew = true;
            }
            objKnChidinhXns.TrangThai = 0;
            objKnChidinhXns.TrangthaiThanhtoan = 0;
            objKnChidinhXns.TinhtrangMau = Utility.sDbnull(txtTinhtrangmau.myCode);
            objKnChidinhXns.Barcode = Utility.sDbnull(txtAssignCode.Text);
            objKnChidinhXns.UuTien = Utility.Bool2byte(chkUutien.Checked);
            objKnChidinhXns.LuongmauThetich = Utility.DecimaltoDbnull(txtThetichKhoiluong.Text,0);
            objKnChidinhXns.MaBenhpham = "";
            objKnChidinhXns.MaChidinh = Utility.sDbnull(txtAssignCode.Text);
            objKnChidinhXns.MaDangky = objLuotkham.MaDangky;
            objKnChidinhXns.IdKhachhang = Utility.Int64Dbnull(objLuotkham.IdKhachhang, -1);
            objKnChidinhXns.NguoiTao = Utility.sDbnull(txtBacsi.MyCode,"ADMIN");
            objKnChidinhXns.NgayChidinh = dtRegDate.Value;
            objKnChidinhXns.NgayhenTrakq = dtpNgaytraKQ.Value;
            if (m_eAction == action.Update)
            {
               
                objKnChidinhXns.NgaySua = globalVariables.SysDate;
                objKnChidinhXns.NguoiSua = globalVariables.UserName;
                objKnChidinhXns.IpMaysua = globalVariables.gv_strIPAddress;
            }
            else
            {
                objKnChidinhXns.NgayTao = globalVariables.SysDate;
                objKnChidinhXns.IpMaytao = globalVariables.gv_strIPAddress;
            }
            return objKnChidinhXns;
        }

        /// <summary>
        /// hàm thực hiện việc tạo mảng thông tin của chỉ định chi tiết kiểm nghiệm
        /// </summary>
        /// <returns></returns>
        private KnChidinhChitiet[] TaoChitietchidinh()
        {
            int i = 0;
            foreach (GridEXRow gridExRow in grdAssignDetail.GetDataRows())
            {
                if (gridExRow.RowType == RowType.Record) i++;
            }
            int idx = 0;
            var arrAssignDetail = new KnChidinhChitiet[i];
            foreach (GridEXRow gridExRow in grdAssignDetail.GetDataRows())
            {
                if (gridExRow.RowType == RowType.Record)
                {
                    arrAssignDetail[idx] = new KnChidinhChitiet();
                    if (Utility.Int64Dbnull(gridExRow.Cells[KnChidinhChitiet.Columns.IdChidinhChitiet].Value, -1) > 0)
                    {
                        arrAssignDetail[idx].IsLoaded = true;
                        arrAssignDetail[idx].IsNew = false;
                        arrAssignDetail[idx].MarkOld();
                        arrAssignDetail[idx].IdChidinhChitiet = Utility.Int64Dbnull(gridExRow.Cells[KnChidinhChitiet.Columns.IdChidinhChitiet].Value, -1);
                    }
                    else
                    {
                        arrAssignDetail[idx].IsNew = true;
                    }
                    arrAssignDetail[idx].IdChidinh = Utility.Int32Dbnull(txtAssign_ID.Text, -1);
                    arrAssignDetail[idx].IdDichvu = Utility.Int16Dbnull(gridExRow.Cells[KnChidinhChitiet.Columns.IdDichvu].Value, -1);
                    arrAssignDetail[idx].IdChitietdichvu = Utility.Int16Dbnull(gridExRow.Cells[KnChidinhChitiet.Columns.IdChitietdichvu].Value, -1);
                    arrAssignDetail[idx].SoLuong = Utility.Int16Dbnull(gridExRow.Cells[KnChidinhChitiet.Columns.SoLuong].Value, 1);
                    arrAssignDetail[idx].DonGia = Utility.DecimaltoDbnull( gridExRow.Cells[KnChidinhChitiet.Columns.DonGia].Value, 0);
                    arrAssignDetail[idx].PhuThu =  Utility.DecimaltoDbnull(gridExRow.Cells[KnChidinhChitiet.Columns.PhuThu].Value, 0);
                    if (m_eAction == action.Insert)
                    {
                        arrAssignDetail[idx].TrangThai = 0;
                        arrAssignDetail[idx].IpMaytao = globalVariables.gv_strIPAddress;
                        arrAssignDetail[idx].NguoiTao = globalVariables.UserName;
                        arrAssignDetail[idx].NgayTao = globalVariables.SysDate;
                    }
                    else
                    {
                        arrAssignDetail[idx].IpMaysua = globalVariables.gv_strIPAddress;
                        arrAssignDetail[idx].NguoiSua = globalVariables.UserName;
                        arrAssignDetail[idx].NgaySua = globalVariables.SysDate;
                    }
                    arrAssignDetail[idx].TrangthaiThanhtoan = Utility.ByteDbnull(gridExRow.Cells[KnChidinhChitiet.Columns.TrangthaiThanhtoan].Value, 0);
                    arrAssignDetail[idx].MahoaMau = Utility.sDbnull(gridExRow.Cells["Mahoa_mau"].Value, "");
                    arrAssignDetail[idx].ChitieuPhantich = Utility.sDbnull(gridExRow.Cells["chitieu_phantich"].Value, 0);
                    
                 
                    idx++;
                }
            }
            return arrAssignDetail;
        }

        /// <summary>
        /// hàm thực hiẹn eviệc thây đổi thông tin trên lưới
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdAssignDetail_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
            ModifyButtonCommand();
        }

        /// <summary>
        /// hàm thực hiện việc phím tắt của form thực hiện
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Chidinh_Maukiemnghiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProcessTabKey(true);
                return;
            }
            if (e.KeyCode == Keys.F1) Utility.ShowMsg(Help);
            if (e.KeyCode == Keys.F4 || (e.Control && e.KeyCode == Keys.P)) cmdInPhieuCLS_Click(cmdInPhieuCLS, new EventArgs());
            if (e.KeyCode == Keys.F11) Utility.ShowMsg(this.ActiveControl.Name);
            if (e.KeyCode == Keys.A && e.Control) cmdAddDetail.PerformClick();
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.Control && e.KeyCode == Keys.S) cmdSave.PerformClick();
            if (e.KeyCode == Keys.F2)
            {
                txtMaukiemnghiem.Focus();
                txtMaukiemnghiem.SelectAll();
            }
            if ((e.Control && e.KeyCode == Keys.F3) || e.KeyCode == Keys.F3)
            {
                //txtNhomDichvuCLS.Focus();
                //txtNhomDichvuCLS.SelectAll();
            }
            if (e.KeyCode == Keys.D && e.Control) cmdDelete.PerformClick();
            if (e.Alt && e.KeyCode == Keys.M) grdServiceDetail.Select();
        }
        /// <summary>
        /// hamf thuc hien viec them thong tin cua can lam sang
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdAddDetail_Click(object sender, EventArgs e)
        {
            ResetNewItem();
            AddDetail();
            UncheckItems();
        }
        void ResetNewItem()
        {
            if (m_dtChitietPhieuCLS == null || !m_dtChitietPhieuCLS.Columns.Contains("isnew")) return;
            foreach (DataRow dr in m_dtChitietPhieuCLS.Rows)
                dr["isnew"] = 0;
            m_dtChitietPhieuCLS.AcceptChanges();
        }
        void SetSaveStatus()
        {
            if (m_dtChitietPhieuCLS == null || !m_dtChitietPhieuCLS.Columns.Contains("isnew")) return;
            foreach (DataRow dr in m_dtChitietPhieuCLS.Rows)
                dr["NoSave"] = 0;
            m_dtChitietPhieuCLS.AcceptChanges();
        }
        bool IsValidDetailData()
        {
            if (Utility.Int32Dbnull(txtMaukiemnghiem.MyID, -1) <= 0)
            {
                Utility.SetMsg(uiStatusBar1.Panels["lblStatus"], "Bạn cần chọn mẫu kiểm nghiệm", true);
                txtMaukiemnghiem.Focus();
                return false;
            }
            
            if (txtTinhtrangmau.myCode=="-1")
            {
                Utility.SetMsg(uiStatusBar1.Panels["lblStatus"], "Bạn cần chọn tình trạng mẫu", true);
                txtTinhtrangmau.Focus();
                return false;
            }
            if (Utility.DoTrim( txtThetichKhoiluong.Text) == "")
            {
                Utility.SetMsg(uiStatusBar1.Panels["lblStatus"], "Bạn cần nhập thể tích/khối lượng mẫu", true);
                txtThetichKhoiluong.Focus();
                return false;
            }
            if (objDichvu != null )
            {
                if (Utility.Byte2Bool(objDichvu.TinhthetichTheochitieu))
                {
                    if (Utility.Int32Dbnull(txtChitieuphantich.Text, 0) < Utility.Int32Dbnull(objMau.SoluongChitieu, 0))
                    {
                        Utility.SetMsg(uiStatusBar1.Panels["lblStatus"], "Số lượng chỉ tiêu phải >=" + Utility.sDbnull(objMau.SoluongChitieu, "0"), true);
                        txtChitieuphantich.Focus();
                        return false;
                    }
                }
                else//Tính thể tích theo thể tích khối lượng nhập
                {
                    if (Utility.Int32Dbnull(Utility.DecimaltoDbnull(txtThetichKhoiluong.Text, 0)) < Utility.Int32Dbnull(objDichvu.ThetichToithieu, 0))
                    {
                        Utility.SetMsg(uiStatusBar1.Panels["lblStatus"], "Thể tích/khối lượng mẫu cần >=" + Utility.sDbnull(objDichvu.ThetichToithieu, "0"), true);
                        txtThetichKhoiluong.Focus();
                        return false;
                    }
                }
            }
            return true;
        }
        private void AddDetail()
        {
            try
            {
                if (!IsValidDetailData()) return;
                foreach (GridEXRow row in grdChitietDichvu.GetDataRows())
                {
                    if (Utility.Int32Dbnull(row.Cells["id_chitietdichvu"].Value, 0) == Utility.Int32Dbnull(txtChitieuphantich.MyID))
                        row.IsChecked = true;
                }
                string errMsg = string.Empty;
                string errMsgTemp = string.Empty;
                isSaved = false;
                bool selectnew = false;
                GridEXRow[] arrCheckList = grdChitietDichvu.GetCheckedRows();
                foreach (GridEXRow gridExRow in arrCheckList)
                {
                    Int32 serviceDetailId = Utility.Int32Dbnull(gridExRow.Cells[KnChidinhChitiet.Columns.IdChitietdichvu].Value, -1);
                    EnumerableRowCollection<DataRow> query = from loz in m_dtChitietPhieuCLS.AsEnumerable().Cast<DataRow>()
                                                             where
                                                                 Utility.Int32Dbnull(
                                                                     loz[KnChidinhChitiet.Columns.IdChitietdichvu], -1) ==
                                                                 serviceDetailId
                                                             select loz;
                    if (!query.Any())
                    {
                        DataRow newDr = m_dtChitietPhieuCLS.NewRow();
                        newDr[KnChidinhChitiet.Columns.IdChidinhChitiet] = -1;

                        newDr["stt_hthi_dichvu"] = Utility.Int32Dbnull(gridExRow.Cells["stt_hthi_dichvu"].Value, -1);
                        newDr["stt_hthi_chitiet"] = Utility.Int32Dbnull(gridExRow.Cells["stt_hthi_chitiet"].Value, -1);
                        newDr[KnChidinhChitiet.Columns.IdChidinh] = v_AssignId;
                        newDr[KnChidinhChitiet.Columns.IdDichvu] = Utility.Int16Dbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.IdDichvu].Value, -1);
                        newDr[KnChidinhChitiet.Columns.IdChitietdichvu] =  Utility.Int32Dbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.IdChitietdichvu].Value, -1);
                        newDr[KnChidinhChitiet.Columns.DonGia] = Utility.DecimaltoDbnull(gridExRow.Cells[KnChidinhChitiet.Columns.DonGia].Value, 0);
                        newDr[DmucDichvuclsChitiet.Columns.TenChitietdichvu] = Utility.sDbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.TenChitietdichvu].Value, "");
                        newDr[KnChidinhChitiet.Columns.Donvi] = Utility.sDbnull(gridExRow.Cells["ten_donvitinh"].Value, "");
                        newDr["IsNew"] = 1;
                        newDr["NoSave"] = 1;
                        newDr["IsLocked"] = 0;
                        newDr["trang_thai"] = 0;
                        newDr["trangthai_thanhtoan"] = 0;
                        newDr[KnChidinhChitiet.Columns.SoLuong] = 1;
                        newDr[KnChidinhChitiet.Columns.MaChidinh] = txtAssignCode.Text;
                        newDr[KnChidinhChitiet.Columns.SoLuong] = Utility.Int32Dbnull(gridExRow.Cells[KnChidinhChitiet.Columns.SoLuong].Value, 1);
                        newDr[DmucDichvucl.Columns.TenDichvu] = Utility.sDbnull(gridExRow.Cells[DmucDichvucl.Columns.TenDichvu].Value, "");
                        newDr[KnChidinhChitiet.Columns.PhuThu] = Utility.DecimaltoDbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.PhuthuDungtuyen].Value, 0);
                        //Phần mẫu kiểm nghiệm
                        newDr["Mahoa_mau"] = txtMahoamau.Text;
                        //Không tự túc thì tính theo giá BHYT nếu là BN BHYT
                        newDr[KnChidinhChitiet.Columns.NguoiTao] = globalVariables.UserName;
                        newDr[KnChidinhChitiet.Columns.NgayTao] = globalVariables.SysDate;
                        newDr[KnChidinhChitiet.Columns.ChitieuPhantich] = Utility.sDbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.TenChitietdichvu].Value, "");

                        m_dtChitietPhieuCLS.Rows.Add(newDr);
                        //txtMaukiemnghiem.ResetText();
                        //txtThetichKhoiluong.Clear();
                        //txtTinhtrangmau.ResetText();
                        //dtpNgaytraKQ.Value = globalVariables.SysDate;
                        //chkUutien.Checked = false;
                        //txtChitieuphantich.Clear();
                        //txtMaukiemnghiem.Focus();
                        if (!selectnew)
                        {
                            Utility.GonewRowJanus(grdAssignDetail, KnChidinhChitiet.Columns.IdChitietdichvu,
                                Utility.sDbnull(newDr[KnChidinhChitiet.Columns.IdChitietdichvu], "0"));
                            selectnew = true;
                        }
                    }
                    else
                    {
                        Utility.GotoNewRowJanus(grdAssignDetail, "id_chitietdichvu", Utility.sDbnull(txtMaukiemnghiem.MyID, ""));
                    }
                }
                m_dtChitietPhieuCLS.AcceptChanges();
                m_dtDanhsachDichvuCLS.AcceptChanges();
                ModifyButtonCommand();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
        }
       
        private void ModifyButtonCommand()
        {
            cmdDelete.Enabled = grdAssignDetail.GetCheckedRows().Length > 0;
            cmdSave.Enabled = grdAssignDetail.RowCount > 0;
        }

        private void UncheckItems()
        {
            if (grdChitietDichvu.RowCount <= 0) return;
            try
            {
                foreach (GridEXRow item in grdChitietDichvu.GetCheckedRows())
                {
                    item.IsChecked = false;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }
        private void grdServiceDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (grdServiceDetail.Focused)
                if (e.KeyCode == Keys.Enter)
                {
                    cmdAddDetail.PerformClick();
                    txtMaukiemnghiem.SelectAll();
                    txtMaukiemnghiem.Focus();
                }
                else if (e.KeyCode == Keys.Space && Utility.sDbnull(grdServiceDetail.CurrentColumn.Key, "") != "colCHON")
                {
                    grdServiceDetail.CurrentRow.IsChecked = !grdServiceDetail.CurrentRow.IsChecked;

                    if (grdServiceDetail.CurrentRow.IsChecked)
                    {
                        AddOneRow_ServiceDetail();
                    }

                }
        }

        /// <summary>
        /// hàm thực hiện việc kiểm tra số lượng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdAssignDetail_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                if (e.Column.Key == KnChidinhChitiet.Columns.SoLuong)
                {
                    if (!Numbers.IsNumber(e.Value.ToString()))
                    {
                        Utility.ShowMsg("Bạn phải số lượng phải là số", "Thông báo", MessageBoxIcon.Warning);
                        e.Cancel = true;
                    }
                    int quanlity = Utility.Int32Dbnull(e.InitialValue, 1);
                    int quanlitynew = Utility.Int32Dbnull(e.Value);
                    if (quanlitynew <= 0)
                    {
                        Utility.ShowMsg("Bạn phải số lượng phải >=1", "Thông báo", MessageBoxIcon.Warning);
                        e.Value = quanlity;
                        e.Cancel = true;
                    }
                    GridEXRow _row = grdAssignDetail.CurrentRow;
                    grdAssignDetail.UpdateData();

                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
            ModifyButtonCommand();
        }

       
        private void grdServiceDetail_SelectionChanged(object sender, EventArgs e)
        {
            if (!m_blnAllowSelectionChanged) return;
            if (!Utility.isValidGrid(grdServiceDetail)) return;
            if (!grdServiceDetail.Focused && grdServiceDetail.CurrentColumn == null)
            {
                Utility.focusCell(grdServiceDetail, DmucDichvucl.Columns.TenDichvu);
            }
        }

        private void clearGrid(int i)
        {
            grdServiceDetail.MoveFirst();
        }

        private void grdServiceDetail_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Char.IsLetter(e.KeyChar))
                {
                    neverFound = false;
                    if (grdServiceDetail.CurrentRow == null) grdServiceDetail.MoveFirst();
                    int oldIdex = grdServiceDetail.CurrentRow.Position;
                    CurrentRowIndex = grdServiceDetail.CurrentRow.Position;
                    bool hasFound = false;
                    bool lastIdx = false;
                    string _Keyvalue = e.KeyChar.ToString().ToUpper();
                    object value;
                    if (CurrentRowIndex + 1 > grdServiceDetail.RowCount - 1)
                    {
                        grdServiceDetail.MoveFirst();
                        grdServiceDetail.Focus();
                        for (int j = 0; j <= oldIdex; j++)
                        {
                            value = grdServiceDetail.CurrentRow.Cells[DmucDichvuclsChitiet.Columns.TenChitietdichvu].Value;
                            if (value != null &&
                                value.ToString().TrimStart().TrimEnd().Substring(0, 1).ToUpper().TrimStart().TrimEnd() ==
                                _Keyvalue)
                                hasFound = true;
                            if (hasFound) break;
                            if (!hasFound && j <= grdServiceDetail.RowCount - 1)
                            {
                                grdServiceDetail.MoveNext();
                                grdServiceDetail.Focus();
                            }
                        }
                    }
                    else
                    {
                        for (int i = CurrentRowIndex + 1; i <= grdServiceDetail.RowCount - 1; i++)
                        {
                            grdServiceDetail.MoveNext();
                            grdServiceDetail.Focus();
                            value = grdServiceDetail.CurrentRow.Cells[DmucDichvuclsChitiet.Columns.TenChitietdichvu].Value;
                            if (value != null &&
                                value.ToString().TrimStart().TrimEnd().Substring(0, 1).ToUpper().TrimStart().TrimEnd() ==
                                _Keyvalue)
                                hasFound = true;

                            if (hasFound) break;
                        }
                        if (!hasFound && oldIdex > 0)
                        {
                            grdServiceDetail.MoveFirst();
                            grdServiceDetail.Focus();

                            for (int j = 0; j <= oldIdex; j++)
                            {
                                value = grdServiceDetail.CurrentRow.Cells[DmucDichvuclsChitiet.Columns.TenChitietdichvu].Value;
                                if (value != null &&
                                    value.ToString().TrimStart().TrimEnd().Substring(0, 1).ToUpper().TrimStart().TrimEnd() ==
                                    _Keyvalue)
                                    hasFound = true;
                                if (hasFound) break;
                                if (!hasFound && j <= grdServiceDetail.RowCount - 1)
                                {
                                    grdServiceDetail.MoveNext();
                                    grdServiceDetail.Focus();
                                }
                            }
                        }
                    }
                    if (!hasFound) grdServiceDetail.MoveToRowIndex(oldIdex - 1);
                    CurrentRowIndex = grdServiceDetail.CurrentRow.Position;
                }
            }
            catch (Exception)
            {
                
                
            }
            
        }

        private void grdAssignDetail_FormattingRow(object sender, RowLoadEventArgs e)
        {
            if (e.Row.RowType == RowType.TotalRow)
            {
                e.Row.Cells[DmucDichvuclsChitiet.Columns.TenChitietdichvu].Value = "Tổng cộng :";
            }
        }

        private void grdServiceDetail_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
            if (grdServiceDetail.CurrentRow.IsChecked)
            {
                AddOneRow_ServiceDetail();
            }
        }

        private void AddOneRow_ServiceDetail()
        {
            try
            {
                string errMsg = string.Empty;
                string errMsg_temp = string.Empty;
                GridEXRow gridExRow = grdServiceDetail.CurrentRow;
                ResetNewItem();
                Int32 IdChitietdichvu = Utility.Int32Dbnull(gridExRow.Cells[KnChidinhChitiet.Columns.IdChitietdichvu].Value, -1);
                EnumerableRowCollection<DataRow> query = from loz in m_dtChitietPhieuCLS.AsEnumerable().Cast<DataRow>()
                                                         where
                                                             Utility.Int32Dbnull(
                                                                 loz[KnChidinhChitiet.Columns.IdChitietdichvu], -1) ==
                                                             IdChitietdichvu
                                                         select loz;
                if (query.Count() <= 0)
                {
                    DataRow newDr = m_dtChitietPhieuCLS.NewRow();
                    newDr[KnChidinhChitiet.Columns.IdChidinhChitiet] = -1;
                    newDr["stt_hthi_dichvu"] =
                        Utility.Int32Dbnull(gridExRow.Cells["stt_hthi_dichvu"].Value, -1);
                    newDr["stt_hthi_chitiet"] =
                        Utility.Int32Dbnull(gridExRow.Cells["stt_hthi_chitiet"].Value, -1);
                    newDr[KnChidinhChitiet.Columns.IdChidinh] = v_AssignId;
                    newDr[KnChidinhChitiet.Columns.IdDichvu] =
                        Utility.Int32Dbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.IdDichvu].Value, -1);
                    newDr[KnChidinhChitiet.Columns.IdChitietdichvu] =
                        Utility.Int32Dbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.IdChitietdichvu].Value, -1);
                    //Utility.DecimaltoDbnull(gridExRow.Cells[KnChidinhChitiet.Columns.PtramBhyt].Value, 0);
                    newDr[KnChidinhChitiet.Columns.DonGia] =
                        Utility.DecimaltoDbnull(gridExRow.Cells[KnChidinhChitiet.Columns.DonGia].Value, 0);
                    newDr[DmucDichvuclsChitiet.Columns.TenChitietdichvu] = Utility.sDbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.TenChitietdichvu].Value, "");
                    newDr["IsNew"] = 1;
                    newDr["IsLocked"] = 0;
                    newDr["NoSave"] = 1;
                    newDr[KnChidinhChitiet.Columns.SoLuong] = Utility.Int32Dbnull(gridExRow.Cells[KnChidinhChitiet.Columns.SoLuong].Value, 1);
                    newDr[DmucDichvucl.Columns.TenDichvu] = Utility.sDbnull(gridExRow.Cells[DmucDichvucl.Columns.TenDichvu].Value, "");
                    newDr[KnChidinhChitiet.Columns.PhuThu] =
                        Utility.DecimaltoDbnull(gridExRow.Cells[KnChidinhChitiet.Columns.PhuThu].Value, 0);
                    //Không tự túc thì tính theo giá BHYT nếu là BN BHYT
                        m_dtChitietPhieuCLS.Rows.Add(newDr);
                        Utility.GonewRowJanus(grdAssignDetail, KnChidinhChitiet.Columns.IdChitietdichvu, Utility.sDbnull(newDr[KnChidinhChitiet.Columns.IdChitietdichvu], "0"));
                        m_dtDanhsachDichvuCLS.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
            finally
            {
                ModifyButtonCommand();
            }
        }

     
        private void grdAssignDetail_ColumnHeaderClick(object sender, ColumnActionEventArgs e)
        {
            ModifyButtonCommand();
        }

        private void grdAssignDetail_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            ModifyButtonCommand();
        }


        private void mnuDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidDataXoaCLS_Selected()) return;

                long AssignDetail =
                    Utility.Int64Dbnull(grdAssignDetail.CurrentRow.Cells[KnChidinhChitiet.Columns.IdChidinhChitiet].Value, -1);

                CHIDINH_CANLAMSANG.XoaChiDinhCLSChitiet(AssignDetail);
                grdAssignDetail.CurrentRow.Delete();
                grdAssignDetail.UpdateData();
                grdAssignDetail.Refresh();
                m_dtChitietPhieuCLS.AcceptChanges();
                if (grdAssignDetail.GetDataRows().Length <= 0)
                {
                    m_eAction = action.Insert;
                    txtAssign_ID.Text = "(Tự sinh)";
                    txtAssignCode.Text = THU_VIEN_CHUNG.SinhMaChidinhCLS();
                }
                m_blnCancel = false;
                ModifyCommand();
                ModifyButtonCommand();
            }
            catch
            {
            }
        }

        /// <summary>
        /// hàm thực hiện việc cấu hình của cls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdCauHinh_Click(object sender, EventArgs e)
        {
            frm_Properties frm = new frm_Properties(PropertyLib._HISCLSProperties);
            frm.ShowDialog();
            CauHinh();
            //InitData();
        }

        private void chkSaveAndPrint_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                PropertyLib._MayInProperties.InCLSsaukhiluu = chkSaveAndPrint.Checked;
                PropertyLib.SaveProperty(PropertyLib._MayInProperties);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi lưu trạng thái-->" + ex.Message);
            }
        }

        /// <summary>
        /// hàm thực hiện việc in phiếu kiểm nghiệm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInPhieuCLS_Click(object sender, EventArgs e)
        {
            try
            {
                int vAssignId = Utility.Int32Dbnull(grdAssignDetail.GetValue(KnChidinhChitiet.Columns.IdChidinh), -1);
                string vAssignCode = Utility.sDbnull(grdAssignDetail.GetValue(KnChidinhXn.Columns.MaChidinh), -1);
                KcbInphieu.InphieuDangkyKiemnghiem(vAssignId);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void grdServiceDetail_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                if (e.Column.Key == KnChidinhChitiet.Columns.SoLuong)
                {
                    if (!Numbers.IsNumber(e.Value.ToString()))
                    {
                        Utility.ShowMsg("Bạn phải số lượng phải là số", "Thông báo", MessageBoxIcon.Warning);
                        e.Cancel = true;
                    }
                    int quanlity = Utility.Int32Dbnull(e.InitialValue, 1);
                    int quanlitynew = Utility.Int32Dbnull(e.Value);
                    if (quanlitynew <= 0)
                    {
                        Utility.ShowMsg("Bạn phải số lượng phải >=1", "Thông báo", MessageBoxIcon.Warning);
                        e.Value = quanlity;
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:"+ exception.Message);
            }
        }



        #region "Event Common"

        /// <summary>
        /// hàm thực hiện việc lấy thông tin 
        /// </summary>
        private void GetData()
        {
            KnChidinhXn objKnChidinhXns = KnChidinhXn.FetchByID(Utility.Int32Dbnull(txtAssign_ID.Text, -1));
            if (objKnChidinhXns != null)
            {
                txtBacsi.Enabled =  dtRegDate.Enabled = dtpNgaytraKQ.Enabled = globalVariables.IsAdmin;
                txtAssignCode.Text = objKnChidinhXns.MaChidinh;
                dtRegDate.Value = objKnChidinhXns.NgayChidinh;
                if (objKnChidinhXns.NgayhenTrakq != null) dtpNgaytraKQ.Value = objKnChidinhXns.NgayhenTrakq.Value;
                txtBacsi.SetCode(Utility.sDbnull(objKnChidinhXns.NguoiTao, ""));

            }
            else
            {
                txtAssignCode.Text = THU_VIEN_CHUNG.SinhMaChidinhKiemNghiem();
                barcode1.Data = Utility.sDbnull(txtAssignCode.Text);
            }
            if (objLuotkham != null)
            {
                if (objBenhnhan != null)
                {
                    this.Text = @"Chỉ định dịch vụ kiểm nghiệm cho:" + objBenhnhan.TenKhachhang + @".";
                }
            }
            LayThongtinKiemnghiemChitietTheophieu();
        }

        /// <summary>
        /// ham thc hien viecj lay thông tin chi tiết của cls
        /// </summary>
        private void LayThongtinKiemnghiemChitietTheophieu()
        {
            try
            {
                m_dtChitietPhieuCLS = SPs.KnLaythongtinChidinhclsTheoid(Utility.Int32Dbnull(txtAssign_ID.Text, -1)).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdAssignDetail, m_dtChitietPhieuCLS, false, true, "1=1",
                                                   "stt_hthi_dichvu,stt_hthi_chitiet," + DmucDichvuclsChitiet.Columns.TenChitietdichvu);
                if (m_dtChitietPhieuCLS.Rows.Count > 0)
                {
                    int idDichvu = Utility.Int16Dbnull(m_dtChitietPhieuCLS.Rows[0]["id_dichvu"].ToString());

                    decimal khoiluongmau = Utility.DecimaltoDbnull(m_dtChitietPhieuCLS.Rows[0]["thetichkhoiluong_mau"].ToString());
                    string tinhtrangmau = Utility.sDbnull(m_dtChitietPhieuCLS.Rows[0]["Tinhtrang_mau"].ToString());
                    txtThetichKhoiluong.Text = khoiluongmau.ToString("##.###");
                    txtTinhtrangmau.SetCode(tinhtrangmau);
                    if (idDichvu > 0)
                    {
                        txtMaukiemnghiem.SetId(idDichvu);
                        txtMaukiemnghiem.Enabled = false;
                    }
                    
                }
              
                grdAssignDetail.MoveFirst();
                ModifyCommand();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi lấy dữ liệu CLS chi tiết\n" + ex.Message);
            }
        }
        
        DataTable _mDtqheCamchidinhClsChungphieu = new DataTable();
        private DataTable _mDanhsachmauchidinh = new DataTable();
        /// <summary>
        /// khởi tạo thông tin của dữ liệu
        /// </summary>
        private void InitData()
        {
            try
            {
                txtTinhtrangmau.Init();
              //  _mDtqheCamchidinhClsChungphieu = new Select().From(QheCamchidinhChungphieu.Schema).Where(QheCamchidinhChungphieu.Columns.Loai).IsEqualTo(0).ExecuteDataSet().Tables[0];
                _mDanhsachmauchidinh =
                    new Select().From(DmucDichvucl.Schema)
                        .Where(DmucDichvucl.Columns.LaDvuKiemnghiem)
                        .IsEqualTo(1)
                        .ExecuteDataSet()
                        .Tables[0];
                txtMaukiemnghiem.Init(_mDanhsachmauchidinh, new List<string>() { DmucDichvucl.Columns.IdDichvu, DmucDichvucl.Columns.MaDichvu, DmucDichvucl.Columns.TenDichvu });
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi trong quá trình load thông tin :" + exception);
            }
        }
        #endregion

        private void txtMaukiemnghiem_TextChanged(object sender, EventArgs e)
        {
            if (Utility.Int16Dbnull(txtMaukiemnghiem.MyID, -1) > 0)
            {
                m_dtDanhsachDichvuCLS = CHIDINH_CANLAMSANG.MaukiemnghiemLaydanhsachdvukiemnghiem(Utility.Int16Dbnull(txtMaukiemnghiem.MyID, -1));
                if (!m_dtDanhsachDichvuCLS.Columns.Contains(KnChidinhChitiet.Columns.SoLuong))
                    m_dtDanhsachDichvuCLS.Columns.Add(KnChidinhChitiet.Columns.SoLuong, typeof(int));
                if (!m_dtDanhsachDichvuCLS.Columns.Contains("ten_donvitinh"))
                    m_dtDanhsachDichvuCLS.Columns.Add("ten_donvitinh", typeof(string));
                m_dtDanhsachDichvuCLS.AcceptChanges();
                Utility.SetDataSourceForDataGridEx(grdChitietDichvu, m_dtDanhsachDichvuCLS, false, true, "", "");
                GridEXColumn gridExColumnGroupIntOrder = grdChitietDichvu.RootTable.Columns["stt_hthi_dichvu"];
                GridEXColumn gridExColumnIntOrder = grdChitietDichvu.RootTable.Columns["stt_hthi_chitiet"];
                Utility.SetGridEXSortKey(grdChitietDichvu, gridExColumnGroupIntOrder, SortOrder.Ascending);
                Utility.SetGridEXSortKey(grdChitietDichvu, gridExColumnIntOrder, SortOrder.Ascending);
            }
        }

    }
}