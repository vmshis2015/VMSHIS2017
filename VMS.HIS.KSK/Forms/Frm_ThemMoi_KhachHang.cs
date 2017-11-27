using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using VNS.HIS.DAL;
using VNS.Libs;

namespace VMS.HIS.KSK.Forms
{
    public partial class FrmThemMoiKhachHang : Form
    {
        public FrmThemMoiKhachHang()
        {
            InitializeComponent();
        }
        //private readonly Query _mQuery = KskDmucKhachhang.CreateQuery();
        public action MEnAction = action.Insert;
        public  DataTable dt = new DataTable();
        private void GetData()
        {
            var objDmucKhachhang = new KskDmucKhachhang();
            objDmucKhachhang =
                new Select().From(KskDmucKhachhang.Schema)
                    .Where(KskDmucKhachhang.Columns.IdKhachHang)
                    .IsEqualTo(Utility.Int32Dbnull(txtIdKhachHang.Text))
                    .ExecuteSingle<KskDmucKhachhang>();
            if (objDmucKhachhang != null)
            {
                txtMaKhachHang.Text = Utility.sDbnull(objDmucKhachhang.MaKhachHang);
                txtTenKhachHang.Text = Utility.sDbnull(objDmucKhachhang.TenKhachHang);
                txtDiaChi.Text = Utility.sDbnull(objDmucKhachhang.DiaChi);
                txtSoDienThoai.Text = Utility.sDbnull(objDmucKhachhang.SoDienThoai);
                txtNguoiDaiDien.Text = Utility.sDbnull(objDmucKhachhang.NguoiDaiDien);
                txtMaSoThue.Text = Utility.sDbnull(objDmucKhachhang.MaSoThue);
                txtSohopDong.Text = Utility.sDbnull(objDmucKhachhang.SoHopDong);
                chkTrangThai.Checked = Utility.Byte2Bool(objDmucKhachhang.TrangThai);
                txtStthienThi.Text = Utility.sDbnull(objDmucKhachhang.SttHienThi);
                dtpNgayHopDong.Value = Convert.ToDateTime(objDmucKhachhang.NgayHopDong);
            }
        }

        private void ModifyCommand()
        {
            if (MEnAction == action.Insert)
            {
                
            }
            else
            {
                txtMaKhachHang.Enabled = false;
            }
        }
        private void FrmThemMoiKhachHang_Load(object sender, EventArgs e)
        {
            txtMaKhachHang.Focus();
            dtpNgayHopDong.Value = DateTime.Now;
            if (MEnAction == action.Update) GetData();
            if (string.IsNullOrEmpty(txtIdKhachHang.Text))
                txtStthienThi.Text =
                    Utility.sDbnull(
                        new Select(Aggregate.Max(KskDmucKhachhang.Columns.SttHienThi)).From(KskDmucKhachhang.Schema).
                            ExecuteScalar<int>() + 1);
            ModifyCommand();
        }
        private void FrmThemMoiKhachHang_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.Control && e.KeyCode == Keys.S) cmdSave.PerformClick();
            if (e.Control && e.KeyCode == Keys.N) cmdNew.PerformClick();
        }

        private bool Isvalidata()
        {
            if (string.IsNullOrEmpty(txtMaKhachHang.Text))
            {
                Utility.ShowMsg("Mã khách hàng không được bỏ trống");
                txtMaKhachHang.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtTenKhachHang.Text))
            {
                Utility.ShowMsg("Tên khách hàng không được bỏ trống");
                txtTenKhachHang.Focus();
                return false;
            }
            SqlQuery sqlktmakhachhang = new Select().From(KskDmucKhachhang.Schema).Where(KskDmucKhachhang.Columns.MaKhachHang).IsEqualTo(Utility.sDbnull(txtMaKhachHang.Text));
            if (sqlktmakhachhang.GetRecordCount() > 0 && MEnAction == action.Insert)
            {
                Utility.ShowMsg("Đã tồn tại mã khách hàng trong hệ thống");
                txtMaKhachHang.Focus();
                return false;
            }
            SqlQuery sqlkttenkhachhang = new Select().From(KskDmucKhachhang.Schema).Where(KskDmucKhachhang.Columns.TenKhachHang).IsEqualTo(Utility.sDbnull(txtTenKhachHang.Text));
            if (sqlkttenkhachhang.GetRecordCount() > 0 && MEnAction == action.Insert)
            {
                Utility.ShowMsg("Đã tồn tại tên khách hàng trong hệ thống");
                txtTenKhachHang.Focus();
                return false;
            }
            return true;
        }
        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (!Isvalidata()) return;
            var objDmucKhachhang = new KskDmucKhachhang();
            if (MEnAction == action.Insert)
            {
                objDmucKhachhang.IsNew = true;
                objDmucKhachhang.NgayTao = globalVariables.SysDate;
                objDmucKhachhang.NguoiTao = globalVariables.UserName;
            }
            else
            {
                objDmucKhachhang.MarkOld();
                objDmucKhachhang.IsLoaded = true;
                objDmucKhachhang.NgaySua = globalVariables.SysDate;
                objDmucKhachhang.NguoiSua = globalVariables.UserName;
            }
            objDmucKhachhang.MaKhachHang = Utility.sDbnull(txtMaKhachHang.Text, "");
            objDmucKhachhang.TenKhachHang = Utility.sDbnull(txtTenKhachHang.Text);
            objDmucKhachhang.DiaChi = Utility.sDbnull(txtDiaChi.Text,"");
            objDmucKhachhang.MaSoThue = Utility.sDbnull(txtMaSoThue.Text, "");
            objDmucKhachhang.NguoiDaiDien = Utility.sDbnull(txtNguoiDaiDien.Text, "");
            objDmucKhachhang.SoDienThoai = Utility.sDbnull(txtSoDienThoai.Text, "");
            objDmucKhachhang.SoHopDong = Utility.sDbnull(txtSohopDong.Text, "");
            objDmucKhachhang.NgayHopDong = dtpNgayHopDong.Value;
            objDmucKhachhang.TrangThai = Utility.ByteDbnull(chkTrangThai.Checked);
            objDmucKhachhang.SttHienThi = Utility.Int16Dbnull(txtStthienThi.Text,100);
            objDmucKhachhang.Save();
            txtIdKhachHang.Text = Utility.sDbnull(objDmucKhachhang.IdKhachHang);
            if (MEnAction == action.Insert)
            {
                ThemMoiLenLuoi();
            }
            else
            {
                UpdateLenLuoi();
            }
            if (chkThemMoiLienTuc.Checked)
            {
                ClearControl();
                MEnAction = action.Insert;
                lblMessage.Text = MEnAction == action.Insert ? @"Thêm mới thành công" : @"Cập nhật thành công";
               
            }
            else
            {
                MEnAction = action.Update;
                lblMessage.Text = MEnAction == action.Insert ? @"Thêm mới thành công" : @"Cập nhật thành công";
            }
            ModifyCommand();
          
        }
        private void UpdateLenLuoi()
        {
            DataRow[] dr = dt.Select(KskDmucKhachhang.Columns.IdKhachHang + "="+ Utility.Int32Dbnull(txtIdKhachHang.Text));
            if (dr.GetLength(0) > 0)
            {
                dr[0][KskDmucKhachhang.Columns.IdKhachHang] = Utility.Int64Dbnull(txtIdKhachHang.Text);
                dr[0][KskDmucKhachhang.Columns.MaKhachHang] = txtMaKhachHang.Text;
                dr[0][KskDmucKhachhang.Columns.TenKhachHang] = txtTenKhachHang.Text;
                dr[0][KskDmucKhachhang.Columns.DiaChi] = txtDiaChi.Text;
                dr[0][KskDmucKhachhang.Columns.MaSoThue] = txtMaSoThue.Text;
                dr[0][KskDmucKhachhang.Columns.SoHopDong] = txtSohopDong.Text;
                dr[0][KskDmucKhachhang.Columns.SoDienThoai] = txtSoDienThoai.Text;
                dr[0][KskDmucKhachhang.Columns.NgayHopDong] = Convert.ToDateTime(dtpNgayHopDong.Value);
                dr[0][KskDmucKhachhang.Columns.TrangThai] = chkTrangThai.Checked ? 1 : 0;
                dr[0][KskDmucKhachhang.Columns.SttHienThi] = Utility.Int32Dbnull(txtStthienThi.Text);
            }
            dt.AcceptChanges();
        }
        private void ThemMoiLenLuoi()
        {
            DataRow dr = dt.NewRow();
            dr[KskDmucKhachhang.Columns.IdKhachHang] = Utility.Int64Dbnull(txtIdKhachHang.Text);
            dr[KskDmucKhachhang.Columns.MaKhachHang] = txtMaKhachHang.Text;
            dr[KskDmucKhachhang.Columns.TenKhachHang] = txtTenKhachHang.Text;
            dr[KskDmucKhachhang.Columns.DiaChi] = txtDiaChi.Text;
            dr[KskDmucKhachhang.Columns.MaSoThue] = txtMaSoThue.Text;
            dr[KskDmucKhachhang.Columns.SoHopDong] = txtSohopDong.Text;
            dr[KskDmucKhachhang.Columns.SoDienThoai] = txtSoDienThoai.Text;
            dr[KskDmucKhachhang.Columns.NgayHopDong] = Convert.ToDateTime(dtpNgayHopDong.Value);
            dr[KskDmucKhachhang.Columns.TrangThai] = chkTrangThai.Checked ? 1 : 0;
            dr[KskDmucKhachhang.Columns.SttHienThi] = Utility.Int32Dbnull(txtStthienThi.Text);
            dt.Rows.Add(dr);
            dt.AcceptChanges();
        }
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ClearControl()
        {
            txtIdKhachHang.Clear();
            txtMaKhachHang.Clear();
            txtTenKhachHang.Clear();
            txtDiaChi.Clear();
            txtSoDienThoai.Clear();
            txtNguoiDaiDien.Clear();
            txtSohopDong.Clear();
            txtMaSoThue.Clear();
            dtpNgayHopDong.Value = DateTime.Now;
            txtMaKhachHang.Focus();
        }
        private void cmdNew_Click(object sender, EventArgs e)
        {
            ClearControl();
            lblMessage.Text = "";
            MEnAction = action.Insert;
        }
    }
}
