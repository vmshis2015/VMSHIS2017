using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using VNS.HIS.UI.Baocao;
using VNS.HIS.UI.Forms.Noitru;
using VNS.Libs;
using VNS.HIS.DAL;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;
using SubSonic;
namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_Quanlyphanbuonggiuong : BaseForm
    {
        private DataTable _mDtTimKiembenhNhan=new DataTable();
        public TrangthaiNoitru TrangthaiNoitru = TrangthaiNoitru.NoiTru;
        DataTable _mDtKhoanoitru;
        public frm_Quanlyphanbuonggiuong()
        {
            InitializeComponent();
            dtToDate.Value = dtFromDate.Value =globalVariables.SysDate;
            Utility.VisiableGridEx(grdList,"ID",globalVariables.IsAdmin);
            InitEvents();
        }
        void InitEvents()
        {
            cmdPhanGiuong.Click += cmdPhanGiuong_Click;
            cmdHuyphangiuong.Click += cmdHuyphangiuong_Click;
            cmdChuyenKhoa.Click += cmdChuyenKhoa_Click;
            cmdHuychuyenkhoa.Click += cmdHuychuyenkhoa_Click;
            cmdChuyenGiuong.Click += cmdChuyenGiuong_Click;
            cmdConfig.Click += cmdConfig_Click;
            cmdExit.Click += cmdExit_Click;
            cmdTimKiem.Click += cmdTimKiem_Click;
            txtPatientCode.KeyDown += txtPatientCode_KeyDown;
            chkByDate.CheckedChanged += chkByDate_CheckedChanged;
            Load += frm_Quanlyphanbuonggiuong_Load;
            KeyDown += frm_Quanlyphanbuonggiuong_KeyDown;
            grdList.SelectionChanged+=grdList_SelectionChanged;
            cmdThemMoiBN.Click+=cmdThemMoiBN_Click;
            cmdSuaThongTinBN.Click+=cmdSuaThongTinBN_Click;
            cmdXoaBN.Click+=cmdXoaBN_Click;
        }
        /// <summary>
        /// hàm thực hiện việc thoát Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_Quanlyphanbuonggiuong_Load(object sender, EventArgs e)
        {
            
            InitData();
            TimKiemThongTin();
            ModifyCommand();
            
        }
        /// <summary>
        /// hàm thực hiện việc lấy thông tin khoa nội trú
        /// </summary>
        private void InitData()
        {
            _mDtKhoanoitru= THU_VIEN_CHUNG.LaydanhsachKhoanoitruTheoBacsi(globalVariables.UserName, Utility.Bool2byte(globalVariables.IsAdmin), 1);
            DataBinding.BindDataCombobox(cboKhoaChuyenDen, _mDtKhoanoitru,
                                                 DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong,
                                                 "---Chọn khoa nội trú---", false,false);
            dtpNgayin.Value = globalVariables.SysDate;

        }
        /// <summary>
        /// hàm thực hiện việc tìm kiếm thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdTimKiem_Click(object sender, EventArgs e)
        {
            TimKiemThongTin();
        }
        private void ModifyCommand()
        {
            bool isValid = Utility.isValidGrid(grdList);
            cmdSuaThongTinBN.Enabled = isValid &&  Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.TrangthaiCapcuu)) > 0;
            cmdXoaBN.Enabled = isValid && Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.TrangthaiCapcuu)) > 0;
            cmdNhapvien.Enabled = cmdNhapvien.Visible=isValid && Utility.Int32Dbnull(grdList.GetValue(NoitruDmucGiuongbenh.Columns.IdKhoanoitru)) <= 0;
            //cmdPhanGiuong.Enabled = isValid && Utility.Int32Dbnull(grdList.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong)) <= 0;
            cmdHuyphangiuong.Enabled = isValid && Utility.Int32Dbnull(grdList.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong)) > 0;
            cmdHuyphangiuong.Enabled = isValid && Utility.Int32Dbnull(grdList.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong)) > 0;
            cmdChuyenKhoa.Enabled = isValid && Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.IdKhoanoitru)) > 0 ;//&& Utility.Int32Dbnull(grdList.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong)) > 0;
            cmdHuychuyenkhoa.Enabled = isValid && Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.IdChuyen)) > 0 && Utility.Int32Dbnull(grdList.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong)) <= 0;
            cmdChuyenGiuong.Enabled = isValid && Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.IdKhoanoitru)) > 0 && Utility.Int32Dbnull(grdList.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong)) > 0;
        }

        private void TimKiemThongTin()
        {
            if (cboKhoaChuyenDen.Items.Count <= 0)
            {
                Utility.ShowMsg("Người dùng đang sử dụng chưa được gắn với khoa nội trú nào nên không thể tìm kiếm. Đề nghị kiểm tra lại");
                return;
            }
            _mDtTimKiembenhNhan =SPs.NoitruTimkiembenhnhan(Utility.Int32Dbnull(cboKhoaChuyenDen.SelectedValue,-1),
                                                txtPatientCode.Text, 1,
                                                chkByDate.Checked ? dtFromDate.Value.ToString("dd/MM/yyyy") : "01/01/1900",
                                                chkByDate.Checked ? dtToDate.Value.ToString("dd/MM/yyyy") : "01/01/1900",
                                                string.Empty, (Int16) (chkCapcuu.Checked?1:-1),-1,0).
                    GetDataSet().Tables[0];
                if (_mDtBuongGiuong != null) _mDtBuongGiuong.Clear();
            Utility.SetDataSourceForDataGridEx(grdList, _mDtTimKiembenhNhan, true, true, "1=1", "");
            ModifyCommand();
        }

        /// <summary>
        /// hàm thực hiện trạng thái của tmf kiếm từ ngày đến ngày
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Enabled = dtFromDate.Enabled = chkByDate.Checked;
        }
        /// <summary>
        /// hàm thực hiện việc thêm mới nhập vện cấp cứu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdThemMoiBN_Click(object sender, EventArgs e)
        {
            var taobenhnhancapcuu = new frm_Taobenhnhancapcuu
            {
                m_enAction = action.Insert,
                m_dtPatient = _mDtTimKiembenhNhan,
                grdList = grdList
            };
            taobenhnhancapcuu._OnActionSuccess += _Taobenhnhancapcuu__OnActionSuccess;
            taobenhnhancapcuu.ShowDialog();
        }

        void _Taobenhnhancapcuu__OnActionSuccess()
        {
            ModifyCommand();
        }
        /// <summary>
        /// hàm thực hiện việc sửa thông tin bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSuaThongTinBN_Click(object sender, EventArgs e)
        {
            var taobenhnhancapcuu = new frm_Taobenhnhancapcuu
            {
                txtMaBN = {Text = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan))},
                txtMaLankham = {Text = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham))},
                m_enAction = action.Update,
                m_dtPatient = _mDtTimKiembenhNhan,
                grdList = grdList
            };
            taobenhnhancapcuu._OnActionSuccess += _Taobenhnhancapcuu__OnActionSuccess;
            taobenhnhancapcuu.ShowDialog();
        }
        /// <summary>
        /// hàm thực hiện việc ký quĩ thông tin 
        /// </summary>
        private bool isValidData_ChuyenKhoa()
        {
           string  maluotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
           int idBenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
           KcbLuotkham kcbLuotkham = Utility.getKcbLuotkham(idBenhnhan, maluotkham);
           if (kcbLuotkham ==null)
           {
               Utility.ShowMsg("Không lấy được thông tin Bệnh nhân. Đề nghị bạn cần chọn ít nhất 1 Bệnh nhân trên lưới");
               grdList.Focus();
               return false;
           }
           if (Utility.Int32Dbnull(kcbLuotkham.TrangthaiNoitru, -1) <= 0)
           {
               Utility.ShowMsg("Bệnh nhân chưa vào viện, Bạn không thể thực hiện chức năng chuyển khoa", "Thông báo", MessageBoxIcon.Warning);
               grdList.Focus();
               return false;
           }
           if (Utility.Int32Dbnull(kcbLuotkham.TrangthaiNoitru, -1) == 4)
           {
               Utility.ShowMsg("Bệnh nhân đã được xác nhận dữ liệu nội trú để chuyển thanh toán nên không thể Hủy giường. Đề nghị bạn kiểm tra lại");
               grdList.Focus();
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
            return true;
        }
        private bool isValidData_ChuyenGiuong()
        {
            string maluotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
            int idBenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
            KcbLuotkham kcbLuotkham = Utility.getKcbLuotkham(idBenhnhan, maluotkham);
            if (kcbLuotkham == null)
            {
                Utility.ShowMsg("Không lấy được thông tin Bệnh nhân. Đề nghị bạn cần chọn ít nhất 1 Bệnh nhân trên lưới");
                grdList.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(kcbLuotkham.TrangthaiNoitru, -1) <= 0)
            {
                Utility.ShowMsg("Bệnh nhân chưa vào viện nên không thể chuyển giường", "Thông báo", MessageBoxIcon.Warning);
                grdList.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(kcbLuotkham.TrangthaiNoitru, -1) == 4)
            {
                Utility.ShowMsg("Bệnh nhân đã được xác nhận dữ liệu nội trú để chuyển thanh toán nên không thể Hủy giường. Đề nghị bạn kiểm tra lại");
                grdList.Focus();
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
            int id = Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.Id));
            var noitruPhanbuonggiuong = new Select().From<NoitruPhanbuonggiuong>()
                .Where(NoitruPhanbuonggiuong.Columns.Id).IsEqualTo(id).ExecuteSingle<NoitruPhanbuonggiuong>();
            if (noitruPhanbuonggiuong != null && Utility.Int32Dbnull(noitruPhanbuonggiuong.IdBuong, -1) < 0)
            {
                Utility.ShowMsg("Bệnh nhân chưa phân buồng giường nên bạn không thể chuyển giường", "Thông báo", MessageBoxIcon.Warning);
                grdList.Focus();
                return false;
            }
            return true;
        }
       
        private bool isValidData_Phanbuonggiuong()
        {
            string maluotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
            int idBenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
            int idKhoanoitru = Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.IdKhoanoitru));
            KcbLuotkham kcbLuotkham = Utility.getKcbLuotkham(idBenhnhan, maluotkham);
            if (kcbLuotkham == null)
            {
                Utility.ShowMsg("Không lấy được thông tin Bệnh nhân. Đề nghị bạn cần chọn ít nhất 1 Bệnh nhân trên lưới");
                grdList.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(kcbLuotkham.TrangthaiNoitru, -1) <= 0)
            {
                Utility.ShowMsg("Bệnh nhân chưa vào viện nên không thể phân buồng giường", "Thông báo", MessageBoxIcon.Warning);
                grdList.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(kcbLuotkham.TrangthaiNoitru, -1) == 4)
            {
                Utility.ShowMsg("Bệnh nhân đã được xác nhận dữ liệu nội trú để chuyển thanh toán nên không thể Hủy giường. Đề nghị bạn kiểm tra lại");
                grdList.Focus();
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
            if (_mDtKhoanoitru == null || _mDtKhoanoitru.Rows.Count <= 0 || _mDtKhoanoitru.Select(DmucKhoaphong.Columns.IdKhoaphong + "=" + idKhoanoitru).Length <= 0)
            {
                Utility.ShowMsg("Bạn không được phân buồng giường cho Bệnh nhân của khoa khác. Đề nghị bạn kiểm tra lại");
                grdList.Focus();
                return false;
            }
            int id = Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.Id));
            _noitruPhanbuonggiuong = new Select().From<NoitruPhanbuonggiuong>()
                  .Where(NoitruPhanbuonggiuong.Columns.Id).IsEqualTo(id).ExecuteSingle<NoitruPhanbuonggiuong>();
            if (_noitruPhanbuonggiuong != null && Utility.Int32Dbnull(_noitruPhanbuonggiuong.TrangThai, -1) == 1)
            {
                Utility.ShowMsg("Bạn không được phép phân buồng giường cho trạng thái đã chuyển khoa hoặc chuyển buồng giường", "Thông báo", MessageBoxIcon.Warning);
                grdList.Focus();
                return false;
            }
            return true;
        }
        
        /// <summary>
        /// hàm thực hiện việc chuyển khoa cho bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdChuyenKhoa_Click(object sender, EventArgs e)
        {
            try
            {
                if(!isValidData_ChuyenKhoa())return;
                frm_ChuyenKhoa frm = new frm_ChuyenKhoa();
                frm.IDBuonggiuong = Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.Id));
                frm.p_DanhSachPhanBuongGiuong = _mDtTimKiembenhNhan;
               // frm.m_enAction = action.Insert;
                frm.b_CallParent = true;
                
                frm.txtMaLanKham.Text = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
                frm.txtPatient_ID.Text = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
                frm.grdList = grdList;
                frm.ShowDialog();
                if (!frm.b_Cancel)
                {
                    int newid=Utility.Int32Dbnull(frm.txtPatientDept_ID.Text);
                    if (newid > 0)
                    {
                        DataTable dtTemp = SPs.NoitruTimkiembenhnhanTheoid(newid).GetDataSet().Tables[0];
                        if (dtTemp.Rows.Count > 0)
                        {
                            DataRow dr = ((DataRowView)grdList.CurrentRow.DataRow).Row;
                            Utility.CopyData(dtTemp.Rows[0], ref dr);
                            _mDtTimKiembenhNhan.AcceptChanges();
                        }
                    }
                    else//Xóa dòng hiện tại
                    {
                        DataRow dr = ((DataRowView)grdList.CurrentRow.DataRow).Row;
                        _mDtTimKiembenhNhan.Rows.Remove(dr);
                        _mDtTimKiembenhNhan.AcceptChanges();
                    }
                }
                ModifyCommand();
            }
            catch (Exception exception)
            {
                
                if(globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
                //throw;
            }
        }
        /// <summary>
        /// hàm thực hên việc chuyển giường bệnh cho bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdChuyenGiuong_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isValidData_ChuyenGiuong()) return;
                frm_Chuyengiuong frm = new frm_Chuyengiuong();
                frm.p_DanhSachPhanBuongGiuong = _mDtTimKiembenhNhan;
                frm.b_CallParent = true;
                // frm.m_enAction = action.Insert;
                frm.IDBuonggiuong = Utility.Int32Dbnull(grdBuongGiuong.GetValue(NoitruPhanbuonggiuong.Columns.Id));
                frm.txtMaLanKham.Text = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
                frm.txtPatient_ID.Text = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
                frm.grdList = grdList;
                frm.ShowDialog();
                if (!frm.b_Cancel)
                {
                    grdList_SelectionChanged(grdList, e);
                    ModifyCommand();
                }
                ModifyCommand();
            }
            catch (Exception exception)
            {

                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
                //throw;
            }
        }
        private void cmdPhanGiuong_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isValidData_Phanbuonggiuong()) return;
                int id = Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.Id));
                NoitruPhanbuonggiuong objPhanbuonggiuong = NoitruPhanbuonggiuong.FetchByID(id);
                if (objPhanbuonggiuong != null)
                {
                    var frm = new frm_phanbuonggiuong
                    {
                        PDanhSachPhanBuongGiuong = _mDtTimKiembenhNhan,
                        txtPatientDept_ID = {Text = Utility.sDbnull(objPhanbuonggiuong.Id)},
                        ObjPhanbuonggiuong = objPhanbuonggiuong
                    };
                    frm.ShowDialog();
                    if (!frm.BCancel)
                    {
                        grdList_SelectionChanged(grdList, e);
                        ModifyCommand();
                    }
                }
               
            }
            catch (Exception exception)
            {
                ModifyCommand();
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
                //throw;
            }
        }
        private bool isValidData_Huygiuong()
        {
            int id = Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.Id));
            int idKhoanoitru = Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.IdKhoanoitru));
            string maluotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
            int idBenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
            KcbLuotkham kcbLuotkham = Utility.getKcbLuotkham(idBenhnhan, maluotkham);
            if (kcbLuotkham == null)
            {
                Utility.ShowMsg("Không lấy được thông tin Bệnh nhân. Đề nghị bạn cần chọn ít nhất 1 Bệnh nhân trên lưới");
                grdList.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(kcbLuotkham.TrangthaiNoitru, -1) == 4)
            {
                Utility.ShowMsg("Bệnh nhân đã được xác nhận dữ liệu nội trú để chuyển thanh toán nên không thể Hủy giường. Đề nghị bạn kiểm tra lại");
                grdList.Focus();
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
            if (_mDtKhoanoitru == null || _mDtKhoanoitru.Rows.Count<=0 || _mDtKhoanoitru.Select(DmucKhoaphong.Columns.IdKhoaphong + "=" + idKhoanoitru).Length <= 0)
            {
                Utility.ShowMsg("Bạn không được quyền hủy giường của khoa này. Đề nghị bạn kiểm tra lại");
                grdList.Focus();
                return false;
            }
            var noitruPhieudieutri = new Select().From<NoitruPhieudieutri>()
                .Where(NoitruPhieudieutri.Columns.IdBuongGiuong).IsEqualTo(id)
                .And(NoitruPhieudieutri.Columns.MaLuotkham).IsEqualTo(maluotkham)
                .And(NoitruPhieudieutri.Columns.IdBenhnhan).IsEqualTo(idBenhnhan).ExecuteSingle<NoitruPhieudieutri>();
            if (noitruPhieudieutri != null)
            {
                Utility.ShowMsg("Đã có phiếu điều trị nội trú gắn với bệnh nhân tại buồng-giường đang chọn nên bạn không thể hủy. Đề nghị xem lại", "Thông báo", MessageBoxIcon.Warning);
                grdList.Focus();
                return false;
            }
            var noitruPhanbuonggiuong = new Select().From<NoitruPhanbuonggiuong>()
                .Where(NoitruPhanbuonggiuong.Columns.Id).IsEqualTo(id).ExecuteSingle<NoitruPhanbuonggiuong>();
            if (noitruPhanbuonggiuong != null && Utility.Int32Dbnull(noitruPhanbuonggiuong.TrangThai, -1) == 1)
            {
                Utility.ShowMsg("Bạn không được phép hủy giường cho trạng thái đã chuyển khoa hoặc chuyển buồng giường", "Thông báo", MessageBoxIcon.Warning);
                grdList.Focus();
                return false;
            }
            return true;
        }
        /// <summary>
        /// hàm thực hiện việc xóa thông tin phần buồng giường
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdHuyphangiuong_Click(object sender, EventArgs e)
        {
            if (!isValidData_Huygiuong()) return;
            if (Utility.AcceptQuestion("Bạn có muốn hủy phần buồng giường cho bệnh nhân đang chọn không?","Thông báo", true))
            {
                int id = Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.Id));
                NoitruPhanbuonggiuong objPhanbuonggiuong = NoitruPhanbuonggiuong.FetchByID(id);
                if (objPhanbuonggiuong != null)
                {
                    objPhanbuonggiuong.IdBuong = -1;
                    int idChuyen = -1;
                    objPhanbuonggiuong.IdGiuong = -1;
                    ActionResult actionResult = new noitru_nhapvien().HuyBenhNhanVaoBuongGuong(objPhanbuonggiuong, ref idChuyen);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            if (idChuyen > 0)
                            {
                                DataTable dtTemp = SPs.NoitruTimkiembenhnhanTheoid(idChuyen).GetDataSet().Tables[0];
                                if (dtTemp.Rows.Count > 0)
                                {
                                    DataRow dr = ((DataRowView)grdList.CurrentRow.DataRow).Row;
                                    Utility.CopyData(dtTemp.Rows[0], ref dr);
                                    _mDtTimKiembenhNhan.AcceptChanges();
                                }
                            }
                            else//Xóa dòng hiện tại
                            {
                                ProcessChuyenKhoa(id);
                            }
                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Lỗi trong quá trình chuyển khoa", "Thông báo", MessageBoxIcon.Error);
                            break;
                    }
                }
            }
            ModifyCommand();
        }

        private void ProcessChuyenKhoa(int id)
        {
            DataRow query = (from khoa in _mDtTimKiembenhNhan.AsEnumerable()
                where
                    Utility.Int32Dbnull(khoa[NoitruPhanbuonggiuong.Columns.Id]) ==
                    Utility.Int32Dbnull(Utility.Int32Dbnull(id))
                select khoa).FirstOrDefault();
            if (query != null)
            {
                query["id_buong"] = -1;
                query["ten_buong"] = string.Empty;
                query[NoitruDmucGiuongbenh.Columns.IdGiuong] = -1;
                query["ten_giuong"] = string.Empty;
                query.AcceptChanges();
            }
        }

        private void grdList_SelectionChanged(object sender, EventArgs e)
        {
            LayLichsuBuongGiuong();
            ModifyCommand();
        }
        DataTable _mDtBuongGiuong;
        void LayLichsuBuongGiuong()
        {
            try
            {
                //Lấy tất cả lịch sử buồng giường
                _mDtBuongGiuong =
                    new KCB_THAMKHAM().NoitruTimkiemlichsuBuonggiuong(
                        Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham)),
                        Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan)), "-1");
                Utility.SetDataSourceForDataGridEx_Basic(grdBuongGiuong, _mDtBuongGiuong, false, true, "1=1",
                    NoitruPhanbuonggiuong.Columns.NgayVaokhoa + " desc");
                grdBuongGiuong.MoveFirst();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Loi :"+ ex.Message);
            }
            finally
            {
                ShowLSuBuongGiuong();
            }
        }
        void ShowLSuBuongGiuong()
        {
            if (!Utility.isValidGrid(grdList) || grdBuongGiuong.GetDataRows().Length <= 1)
            {
                grdBuongGiuong.Width = 0;
            }
            else
            {
                grdBuongGiuong.Width = 425;
            }
        }
        
        /// <summary>
        /// hàm thưc hiện việc tìm kiếm htoong tin nhanh cho bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPatientCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadMaLanKham();
                chkByDate.Checked = false;
                cmdTimKiem.PerformClick();
            }
        }
        private void LoadMaLanKham()
        {
            MaLuotkham = Utility.sDbnull(txtPatientCode.Text.Trim());
            if (!string.IsNullOrEmpty(MaLuotkham) && txtPatientCode.Text.Length < 8)
            {
                MaLuotkham = Utility.AutoFullPatientCode(txtPatientCode.Text);
                txtPatientCode.Text = MaLuotkham;
                txtPatientCode.Select(txtPatientCode.Text.Length, txtPatientCode.Text.Length);
            }
         
        }
        /// <summary>
        /// hàm thực hiện việc phím tắt thông tin 
        /// </summary>
        private string MaLuotkham { get; set; }
        private void frm_Quanlyphanbuonggiuong_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.F3)cmdTimKiem.PerformClick();
            if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
            if (e.KeyCode == Keys.F2)
            {
                txtPatientCode.Focus();
                txtPatientCode.SelectAll();
            }
            if(e.KeyCode==Keys.N&&e.Control)cmdThemMoiBN.PerformClick();
            if(e.KeyCode==Keys.U&&e.Control)cmdSuaThongTinBN.PerformClick();
        }
        private bool isValidData_HuyKhoa()
        {
            int id = Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.Id));
            string maluotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
            int idBenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
            KcbLuotkham kcbLuotkham = Utility.getKcbLuotkham(idBenhnhan, maluotkham);
            if (kcbLuotkham == null)
            {
                Utility.ShowMsg("Không lấy được thông tin Bệnh nhân. Đề nghị bạn cần chọn ít nhất 1 Bệnh nhân trên lưới");
                grdList.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(kcbLuotkham.TrangthaiNoitru, -1) == 4)
            {
                Utility.ShowMsg("Bệnh nhân đã được xác nhận dữ liệu nội trú để chuyển thanh toán nên không thể Hủy giường. Đề nghị bạn kiểm tra lại");
                grdList.Focus();
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
            var noitruPhieudieutri = new Select().From<NoitruPhieudieutri>()
                .Where(NoitruPhieudieutri.Columns.IdBuongGiuong).IsEqualTo(id)
                .And(NoitruPhieudieutri.Columns.MaLuotkham).IsEqualTo(maluotkham)
                .And(NoitruPhieudieutri.Columns.IdBenhnhan).IsEqualTo(idBenhnhan).ExecuteSingle<NoitruPhieudieutri>();
            if (noitruPhieudieutri != null)
            {
                Utility.ShowMsg("Đã có phiếu điều trị nội trú gắn với bệnh nhân tại khoa nội trú đang chọn nên bạn không thể hủy. Đề nghị xem lại", "Thông báo", MessageBoxIcon.Warning);
                grdList.Focus();
                return false;
            }
            var noitruPhanbuonggiuong = new Select().From<NoitruPhanbuonggiuong>()
                .Where(NoitruPhanbuonggiuong.Columns.Id).IsEqualTo(id).ExecuteSingle<NoitruPhanbuonggiuong>();
            if (noitruPhanbuonggiuong != null && Utility.Int32Dbnull(noitruPhanbuonggiuong.TrangThai, -1) == 1)
            {
                Utility.ShowMsg("Bạn không được phép hủy chuyển khoa cho trạng thái đã chuyển khoa hoặc chuyển buồng giường", "Thông báo", MessageBoxIcon.Warning);
                grdList.Focus();
                return false;
            }
            return true;
        }
        private void cmdHuychuyenkhoa_Click(object sender, EventArgs e)
        {
            if (!isValidData_HuyKhoa()) return;
            if (Utility.AcceptQuestion("Bạn có chắc chắn muốn hủy chuyển khoa nội trú. Sau khi hủy, Bệnh nhân sẽ quay về trạng thái khoa-buồng-giường trước đó", "Thông báo", true))
            {
                int idChuyen = -1;
                int id = Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.Id));
                NoitruPhanbuonggiuong objPhanbuonggiuong = NoitruPhanbuonggiuong.FetchByID(id);
                if (objPhanbuonggiuong != null)
                {
                    objPhanbuonggiuong.IdBuong = -1;
                    objPhanbuonggiuong.IdGiuong = -1;
                    ActionResult actionResult = new noitru_nhapvien().HuyKhoanoitru(objPhanbuonggiuong, ref idChuyen);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            if(idChuyen>0)
                            {
                                DataTable dtTemp = SPs.NoitruTimkiembenhnhanTheoid(idChuyen).GetDataSet().Tables[0];
                                if (dtTemp.Rows.Count > 0)
                                {
                                    DataRow dr = ((DataRowView)grdList.CurrentRow.DataRow).Row;
                                    Utility.CopyData(dtTemp.Rows[0], ref dr);
                                    _mDtTimKiembenhNhan.AcceptChanges();
                                }
                            }
                            else//Xóa dòng hiện tại
                            {
                                DataRow dr = ((DataRowView)grdList.CurrentRow.DataRow).Row;
                                _mDtTimKiembenhNhan.Rows.Remove(dr);
                                _mDtTimKiembenhNhan.AcceptChanges();
                            }

                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Lỗi trong quá trình chuyển khoa", "Thông báo", MessageBoxIcon.Error);
                            break;
                    }
                }
            }
            ModifyCommand();
        }
        bool IsValidDeleteData()
        {
            try
            {
                string vMaLuotkham =
              Utility.sDbnull(
                grdList.GetValue(KcbLuotkham.Columns.MaLuotkham),
                  "");
                int vPatientId =
                     Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1);
                var lstNoitruPhanbuonggiuong=new Select().From(NoitruPhanbuonggiuong.Schema)
                    .Where(NoitruPhanbuonggiuong.Columns.IdBenhnhan).IsEqualTo(vPatientId)
                    .And(NoitruPhanbuonggiuong.Columns.MaLuotkham).IsEqualTo(vMaLuotkham)
                    .And(NoitruPhanbuonggiuong.Columns.NoiTru).IsEqualTo(1)
                    .ExecuteAsCollection<NoitruPhanbuonggiuongCollection>();
               
                if (lstNoitruPhanbuonggiuong != null && lstNoitruPhanbuonggiuong.Count > 1)
                {
                    Utility.ShowMsg( "Bệnh nhân đã chuyển khoa hoặc chuyển giường nên bạn không thể xóa thông tin");
                    return false;
                }

                var objNoitruTamung = new Select().From(NoitruTamung.Schema)
                   .Where(NoitruTamung.Columns.IdBenhnhan).IsEqualTo(vPatientId)
                   .And(NoitruTamung.Columns.MaLuotkham).IsEqualTo(vMaLuotkham)
                   .ExecuteSingle<NoitruTamung>();

                if (objNoitruTamung != null )
                {
                    Utility.ShowMsg("Bệnh nhân đã nộp tiền tạm ứng nên bạn không thể xóa thông tin");
                    return false;
                }
                var objNoitruPhieudieutri = new Select().From(NoitruPhieudieutri.Schema)
                  .Where(NoitruPhieudieutri.Columns.IdBenhnhan).IsEqualTo(vPatientId)
                  .And(NoitruPhieudieutri.Columns.MaLuotkham).IsEqualTo(vMaLuotkham)
                  .ExecuteSingle<NoitruPhieudieutri>();

                if (objNoitruPhieudieutri != null)
                {
                    Utility.ShowMsg("Bệnh nhân đã Lập phiếu điều trị nên bạn không thể xóa thông tin");
                    return false;
                }
                var objKcbDonthuoc = new Select().From(KcbDonthuoc.Schema)
                  .Where(KcbDonthuoc.Columns.IdBenhnhan).IsEqualTo(vPatientId)
                  .And(KcbDonthuoc.Columns.MaLuotkham).IsEqualTo(vMaLuotkham)
                  .ExecuteSingle<KcbDonthuoc>();

                if (objKcbDonthuoc != null)
                {
                    Utility.ShowMsg("Bệnh nhân đã được kê đơn thuốc nên bạn không thể xóa thông tin");
                    return false;
                }
                var objKcbChidinhcl = new Select().From(KcbChidinhcl.Schema)
                  .Where(KcbChidinhcl.Columns.IdBenhnhan).IsEqualTo(vPatientId)
                  .And(KcbChidinhcl.Columns.MaLuotkham).IsEqualTo(vMaLuotkham)
                  .ExecuteSingle<KcbChidinhcl>();

                if (objKcbChidinhcl != null)
                {
                    Utility.ShowMsg("Bệnh nhân đã được lập phiếu chỉ định nên bạn không thể xóa thông tin");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi kiểm tra hợp lệ dữ liệu trước khi cập nhật Bệnh nhân", ex);
                return false;
            }
        }
        private void cmdXoaBN_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn phải chọn ít nhất 1 bệnh nhân cấp cứu để xóa");
                    return;
                }
                string errMgs = "";
                string vMaLuotkham =
                   Utility.sDbnull(
                     grdList.GetValue(KcbLuotkham.Columns.MaLuotkham),
                       "");
                int vPatientId =
                     Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1);

                if (!IsValidDeleteData()) return;
                if (Utility.AcceptQuestion("Bạn có muốn xóa Bệnh nhân cấp cứu này không", "Thông báo", true))
                {
                    myTrace.FunctionID = globalVariables.FunctionID;
                    myTrace.FunctionName = globalVariables.FunctionName;
                    ActionResult actionResult = new KCB_DANGKY().PerformActionDeletePatientExam(myTrace,vMaLuotkham,
                                                                                                       vPatientId, ref errMgs);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                                grdList.CurrentRow.BeginEdit();
                                grdList.CurrentRow.Delete();
                                grdList.CurrentRow.EndEdit();
                                grdList.UpdateData();
                                grdList_SelectionChanged(grdList, e);
                            _mDtTimKiembenhNhan.AcceptChanges();
                            Utility.ShowMsg("Xóa Bệnh nhân cấp cứu thành công", "Thành công");
                            break;
                        case ActionResult.Exception:
                            if (errMgs != "")
                                Utility.ShowMsg(errMgs);
                            else
                                Utility.ShowMsg("Bệnh nhân đã có thông tin chỉ định dịch vụ hoặc đơn thuốc... /n bạn không thể xóa lần khám này", "Thông báo");
                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Có lỗi trong quá trình xóa thông tin", "Thông báo");
                            break;
                    }
                }
                ModifyCommand();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Có lỗi trong quá trình xóa thông tin" + ex.Message, "Thông báo");
            }
        }

        private void cmdConfig_Click(object sender, EventArgs e)
        {
            var properties = new frm_Properties(PropertyLib._NoitruProperties);
            properties.ShowDialog();
        }
        private NoitruPhanbuonggiuong _noitruPhanbuonggiuong;
        private void cmdsuagiuong_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:"+ exception.Message);
            }
        }

        private bool isValidData_Xoagiuong()
        {
            int id = Utility.Int32Dbnull(grdBuongGiuong.GetValue(NoitruPhanbuonggiuong.Columns.Id));
            int idKhoanoitru = Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.IdKhoanoitru));
            string maluotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
            int idBenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
            KcbLuotkham kcbLuotkham = Utility.getKcbLuotkham(idBenhnhan, maluotkham);
            if (kcbLuotkham == null)
            {
                Utility.ShowMsg("Không lấy được thông tin Bệnh nhân. Đề nghị bạn cần chọn ít nhất 1 Bệnh nhân trên lưới");
                grdList.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(kcbLuotkham.TrangthaiNoitru, -1) == 4)
            {
                Utility.ShowMsg("Bệnh nhân đã được xác nhận dữ liệu nội trú để chuyển thanh toán nên không thể Hủy giường. Đề nghị bạn kiểm tra lại");
                grdList.Focus();
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
            if (_mDtKhoanoitru == null || _mDtKhoanoitru.Rows.Count <= 0 || _mDtKhoanoitru.Select(DmucKhoaphong.Columns.IdKhoaphong + "=" + idKhoanoitru).Length <= 0)
            {
                Utility.ShowMsg("Bạn không được quyền hủy giường của khoa này. Đề nghị bạn kiểm tra lại");
                grdList.Focus();
                return false;
            }
            var noitruPhieudieutri = new Select().From<NoitruPhieudieutri>()
                .Where(NoitruPhieudieutri.Columns.IdBuongGiuong).IsEqualTo(id)
                .And(NoitruPhieudieutri.Columns.MaLuotkham).IsEqualTo(maluotkham)
                .And(NoitruPhieudieutri.Columns.IdBenhnhan).IsEqualTo(idBenhnhan).ExecuteSingle<NoitruPhieudieutri>();
            if (noitruPhieudieutri != null)
            {
                Utility.ShowMsg("Đã có phiếu điều trị nội trú gắn với bệnh nhân tại buồng-giường đang chọn nên bạn không thể hủy. Đề nghị xem lại", "Thông báo", MessageBoxIcon.Warning);
                grdList.Focus();
                return false;
            }
            //var noitruPhanbuonggiuong = new Select().From<NoitruPhanbuonggiuong>()
            //    .Where(NoitruPhanbuonggiuong.Columns.Id).IsEqualTo(id).ExecuteSingle<NoitruPhanbuonggiuong>();
            //if (noitruPhanbuonggiuong != null && Utility.Int32Dbnull(noitruPhanbuonggiuong.TrangThai, -1) == 1)
            //{
            //    Utility.ShowMsg("Bạn không được phép hủy giường cho trạng thái đã chuyển khoa hoặc chuyển buồng giường", "Thông báo", MessageBoxIcon.Warning);
            //    grdList.Focus();
            //    return false;
            //}
            return true;
        }
        private void cmdxoagiuong_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isValidData_Xoagiuong()) return;
                int id = Utility.Int32Dbnull(grdBuongGiuong.CurrentRow.Cells[NoitruPhanbuonggiuong.Columns.Id].Value);
                if (id > 0)
                {
                   new Delete().From(NoitruPhanbuonggiuong.Schema)
                  .Where(NoitruPhanbuonggiuong.Columns.Id)
                  .IsEqualTo(id)
                  .Execute();
                    Utility.ShowMsg("Xóa giường của bệnh nhân thành công!", "Thông báo",MessageBoxIcon.Information);
                    LayLichsuBuongGiuong();
                }
                else
                {
                    Utility.ShowMsg("Không tồn tại mã phân buồng giường!", "Thông báo", MessageBoxIcon.Warning);
                }
              
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi : "+ ex.Message);
            }
        }

        private void cmdsuabuonggiuong_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi: "+ ex.Message);
            }
           
        }

        private void cmdconfig2_Click(object sender, EventArgs e)
        {
            var properties = new frm_Properties(PropertyLib._NoitruProperties);
            properties.ShowDialog();
        }

        private void cmdInCamKet_Click(object sender, EventArgs e)
        {
            try
            {
                string malankham = Utility.sDbnull(grdList.CurrentRow.Cells[KcbLuotkham.Columns.MaLuotkham].Value);
                int idbenhnhan = Utility.Int32Dbnull(grdList.CurrentRow.Cells[KcbLuotkham.Columns.IdBenhnhan].Value);
                DataTable dt = SPs.NoitruLaythongtinbenhnhan(malankham, idbenhnhan).GetDataSet().Tables[0];
                noitru_baocao.InBanCamKetPhauThuat(dt,"GIẤY CAM ĐOAN PHẪU THUẬT",dtpNgayin.Value);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi: "+ ex.Message);
            }
        }

        private void cmdBienBanHoiChan_Click(object sender, EventArgs e)
        {
            try
            {
                string malankham = Utility.sDbnull(grdList.CurrentRow.Cells[KcbLuotkham.Columns.MaLuotkham].Value);
                int idbenhnhan = Utility.Int32Dbnull(grdList.CurrentRow.Cells[KcbLuotkham.Columns.IdBenhnhan].Value);
                var frm = new FrmTrichBienBanHoiChan();
                frm.idbenhnhan = idbenhnhan;
                frm.malankham = malankham;
                frm.ShowDialog();
                //DataTable dt = SPs.NoitruLaythongtinbenhnhan(malankham, idbenhnhan).GetDataSet().Tables[0];
                //noitru_baocao.InBanCamKetPhauThuat(dt, "GIẤY CAM ĐOAN PHẪU THUẬT", dtpNgayin.Value);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi: " + ex.Message);
            }
          
        }

        private void cmdInPhieuVaoVien_Click(object sender, EventArgs e)
        {
            try
            {
                string malankham = Utility.sDbnull(grdList.CurrentRow.Cells[KcbLuotkham.Columns.MaLuotkham].Value);
                int idbenhnhan = Utility.Int32Dbnull(grdList.CurrentRow.Cells[KcbLuotkham.Columns.IdBenhnhan].Value);
                DataTable dsTable =
               new noitru_nhapvien().NoitruLaythongtinInphieunhapvien(malankham, Utility.Int32Dbnull(idbenhnhan));
                if (dsTable.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy bản ghi nào\n Mời bạn xem lại", "Thông báo", MessageBoxIcon.Error);
                    return;
                }

                SqlQuery sqlQuery = new Select().From(KcbChandoanKetluan.Schema)
                    .Where(KcbChandoanKetluan.Columns.MaLuotkham).IsEqualTo(malankham)
                    .And(KcbChandoanKetluan.Columns.IdBenhnhan).IsEqualTo(idbenhnhan).OrderAsc(
                        KcbChandoanKetluan.Columns.NgayChandoan);
                var objInfoCollection = sqlQuery.ExecuteAsCollection<KcbChandoanKetluanCollection>();
                string chandoan = "";
                string mabenh = "";
                string phongkhamvaovien = "";
                string khoanoitru = "";
                foreach (KcbChandoanKetluan objDiagInfo in objInfoCollection)
                {
                    string ICD_Name = "";
                    string ICD_Code = "";
                    GetChanDoan(Utility.sDbnull(objDiagInfo.MabenhChinh, ""),
                                Utility.sDbnull(objDiagInfo.MabenhPhu, ""), ref ICD_Name, ref ICD_Code);
                    chandoan += string.IsNullOrEmpty(objDiagInfo.Chandoan)
                                    ? ICD_Name
                                    : Utility.sDbnull(objDiagInfo.Chandoan);
                    mabenh += ICD_Code;
                }

                //txtkbMa.Text = Utility.sDbnull(mabenh);

                DataSet ds = new noitru_nhapvien().KcbLaythongtinthuocKetquaCls(malankham, Utility.Int32Dbnull(idbenhnhan));
                DataTable dtThuoc = ds.Tables[0];
                DataTable dtketqua = ds.Tables[1];

                string[] query = (from thuoc in dtThuoc.AsEnumerable()
                                  let y = Utility.sDbnull(thuoc["ten_thuoc"])
                                  select y).ToArray();
                string donthuoc = string.Join(";", query);
                string[] querykq = (from kq in dtketqua.AsEnumerable()
                                    let y = Utility.sDbnull(kq["ketqua"])
                                    select y).ToArray();
                string ketquaCLS = string.Join("; ", querykq);




                //foreach (DataRow dr in dsTable.Rows)
                //{
                DataRow dr = dsTable.Rows[0];
                if (dr != null)
                {
                    dr["thuockedon"] = donthuoc;
                    dr["CHANDOAN_VAOVIEN"] = chandoan;
                    dr["KETQUA_CLS"] = ketquaCLS;
                }

                dsTable.AcceptChanges();
                VNS.HIS.UI.Baocao.noitru_baocao.Inphieunhapvien(dsTable, "PHIẾU NHẬP VIỆN", globalVariables.SysDate);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi: " + ex.Message);
            }
        }

        private void GetChanDoan(string ICD_chinh, string IDC_Phu, ref string ICD_Name, ref string ICD_Code)
        {
            try
            {
                List<string> lstICD = ICD_chinh.Split(',').ToList();
                DmucBenhCollection _list =
                    new DmucBenhController().FetchByQuery(
                        DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, lstICD));
                foreach (DmucBenh _item in _list)
                {
                    ICD_Name += _item.TenBenh + ";";
                    ICD_Code += _item.MaBenh + ";";
                }
                lstICD = IDC_Phu.Split(',').ToList();
                _list =
                    new DmucBenhController().FetchByQuery(
                        DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, lstICD));
                foreach (DmucBenh _item in _list)
                {
                    ICD_Name += _item.TenBenh + ";";
                    ICD_Code += _item.MaBenh + ";";
                }
                if (ICD_Name.Trim() != "") ICD_Name = ICD_Name.Substring(0, ICD_Name.Length - 1);
                if (ICD_Code.Trim() != "") ICD_Code = ICD_Code.Substring(0, ICD_Code.Length - 1);
            }
            catch
            {
            }
        }
     
    }
}
