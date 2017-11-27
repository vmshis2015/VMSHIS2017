using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using SubSonic;
using VNS.HIS.DAL;
using VNS.Libs;

namespace VMS.HIS.KSK.Forms
{
    public partial class FrmDanhMucKhachHang : Form
    {
        public FrmDanhMucKhachHang()
        {
            InitializeComponent();
        }
        DataTable dtDmucKhachHang = new DataTable();
        private void cmdNew_Click(object sender, EventArgs e)
        {
            var frm = new FrmThemMoiKhachHang();
            frm.txtIdKhachHang.Text = @"-1";
            frm.MEnAction = action.Insert;
            frm.dt = dtDmucKhachHang;
            frm.ShowDialog();
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            var frm = new FrmThemMoiKhachHang();
            frm.txtIdKhachHang.Text = Utility.sDbnull(grdkhachhang.GetValue(KskDmucKhachhang.Columns.IdKhachHang), -1);
            frm.MEnAction = action.Update;
            frm.dt = dtDmucKhachHang;
            frm.ShowDialog();
        }

        private void cmdThoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            
            foreach (GridEXRow row in grdkhachhang.GetCheckedRows())
            {
                int idkhachhang = Utility.Int32Dbnull(row.Cells[KskDmucKhachhang.Columns.IdKhachHang].Value);
                new Delete().From(KskDmucKhachhang.Schema)
                    .Where(KskDmucKhachhang.Columns.IdKhachHang)
                    .IsEqualTo(idkhachhang)
                    .Execute();
                row.Delete();
            }
        }

        private void FrmDanhMucKhachHang_Load(object sender, EventArgs e)
        {
            try
            {
                dtDmucKhachHang = new Select().From(KskDmucKhachhang.Schema).ExecuteDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdkhachhang, dtDmucKhachHang, true, true, "", "SttHienThi");
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }

        private void FrmDanhMucKhachHang_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.N) cmdNew.PerformClick();
            if (e.Control && e.KeyCode == Keys.E) cmdEdit.PerformClick();
            if (e.Control && e.KeyCode == Keys.D) cmdDelete.PerformClick();
            if ( e.KeyCode == Keys.Escape) cmdThoat.PerformClick();
            if (e.KeyCode == Keys.F5)
            {
                dtDmucKhachHang = new Select().From(KskDmucKhachhang.Schema).ExecuteDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdkhachhang, dtDmucKhachHang, true, true, "", "SttHienThi");
            }
        }
    }
}
