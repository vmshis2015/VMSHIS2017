using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.HIS.DAL;
using VNS.Libs;

namespace VNS.HIS.UI.Forms.Dungchung
{
    public partial class frm_LogInfo : Form
    {
        public frm_LogInfo()
        {
            InitializeComponent();
        }
        private void LaydanhsachNhanvien()
        {
            try
            {
                DataTable data = THU_VIEN_CHUNG.LaydanhsachBacsi(-1, -1);
                txtNhanvien.Init(data,
                                 new List<string>()
                                     {
                                         DmucNhanvien.Columns.IdNhanvien,
                                         DmucNhanvien.Columns.MaNhanvien,
                                         DmucNhanvien.Columns.TenNhanvien
                                     });
                if (globalVariables.gv_intIDNhanvien <= 0)
                {
                    txtNhanvien.SetId(-1);
                }
                else
                {
                    txtNhanvien.SetId(globalVariables.gv_intIDNhanvien);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }

        private void frm_LogInfo_Load(object sender, EventArgs e)
        {
            try
            {
                LaydanhsachNhanvien();
                dtpFromDate.Value = DateTime.Now;
                dtpToDate.Value = DateTime.Now;
                cboLogAction.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                    Utility.ShowMsg("Lỗi:"+ ex.Message);                
            }
        }

        private void cmdSearchLog_Click(object sender, EventArgs e)
        {
            try
            {
                grdLogInfomation.DataSource = null;
                DataTable dtTLog =
                    SPs.SpGetTLogInfomation(dtpFromDate.Value, dtpToDate.Value,
                                            Utility.Int32Dbnull(cboLogAction.SelectedValue,-1), Utility.sDbnull(txtNhanvien.MyCode,"-1")).
                        GetDataSet().Tables[0];
                if(dtTLog.Rows.Count>0)
                {
                    grdLogInfomation.DataSource = dtTLog;
                }
            }
            catch(Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ex.Message);
            }
            finally
            {
                GC.Collect();
            }
        }

        private void frm_LogInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode ==Keys.F3) cmdSearchLog.PerformClick();
            if(e.KeyCode == Keys.F4) cmdViewLog.PerformClick();
            if(e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdViewLog_Click(object sender, EventArgs e)
        {
            ExcelUtlity.ExportGridEx(grdLogInfomation);
        }
    }
}
