using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using VNS.Libs;
using VNS.HIS.DAL;
using VNS.HIS.BusRule.Classes;

namespace VMS.HIS.KN.CanLamSang
{
    public partial class frm_nhapketquaKN : Form
    {
        public delegate void OnResult(Int64 id_chitiet, byte tthai_cls);
        public event OnResult _OnResult;

        string ma_luotkham = "";
        Int64 id_benhnhan = -1;
        string MaBenhpham = "";
        string MaChidinh = "";
        int IdChitietdichvu = -1;
        int currRowIdx = -1;
        int id_chidinh = -1;
        int id_dichvu = -1;
        int co_chitiet = -1;
        KcbChidinhcl objChidinh = null;
        DataTable dtChidinh = null; 
        public frm_nhapketquaKN()
        {
            InitializeComponent();
            InitEvents();
        }
        void InitEvents()
        {
            this.Load += frm_nhapketquaKN_Load;
            this.KeyDown += frm_nhapketquaKN_KeyDown;
            cmdSearch.Click += cmdSearch_Click;
            txtidchidinh.KeyDown += txtMahoamau_KeyDown;
            grdKetqua.UpdatingCell += grdKetqua_UpdatingCell;
           // mnuCancelResult.Click += mnuCancelResult_Click;
        }
        public void AutoSearch(long id_chidinh)
        {
            txtidchidinh.Text = Utility.sDbnull(id_chidinh);
            SearchData();
        }
        void cmdConfirm_Click(object sender, EventArgs e)
        {
            Confirm(); 
        }
        void Confirm()
        {
            byte result = 3;
            try
            {
                foreach (GridEXRow row in grdKetqua.GetDataRows())
                {
                   
                }
                lblMessge.Text = "Xác nhận kết quả";
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);

            }
        }
        void mnuCancelResult_Click(object sender, EventArgs e)
        {
            foreach (GridEXRow row in grdKetqua.SelectedItems)
            {
               
            }
            if (1==1)
            {
                lblMessge.Text = "Đã hủy kết quả các xét nghiệm";
                if (_OnResult != null) _OnResult(-1,2);
            }
            else
            {
                Utility.ShowMsg("Lỗi khi thực hiện hủy kết quả xét nghiệm");
            }
        }
        //KcbChidinhclsChitiet.Trang_thai:0=Mới chỉ định;1=Đã chuyển CLS;2=Đang thực hiện;3= Đã có kết quả CLS;4=Đã xác nhận kết quả
        void grdKetqua_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
              
                int id_kq = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdKetqua,KcbKetquaCl.Columns.IdKq) ,-1);
                int IdChitietdichvu = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdKetqua, DmucDichvuclsChitiet.Columns.IdChitietdichvu), -1);
                string ketqua = Utility.sDbnull(e.Value, "");

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                
            }
        }
       
        void HienthiNhapketqua(long id_dichvu,int co_chitiet)
        {
            try
            {
                DataTable dt = SPs.KnMaukiemnghiemTimkiemchitieu(Utility.Int64Dbnull(id_dichvu)).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdKetqua, dt, true, true, "1=1", "");
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
                
            }
        }
        void txtMahoamau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SearchData();
        }

        void txtMaluotkham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SearchData();
        }

        void frm_nhapketquaKN_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F3)
            {
                txtidchidinh.SelectAll();
                txtidchidinh.Focus();
                return;
            }
            if (e.Control && e.KeyCode == Keys.S)
            {
                cmdConfirm_Click(cmdConfirm, new EventArgs());
                return;
            }
            if (e.KeyCode == Keys.F2 && grdKetqua.GetDataRows().Length > 0)
            {
                Utility.focusCell(grdKetqua, KcbKetquaCl.Columns.KetQua);
                return;
            }
            if (e.KeyCode == Keys.F5)
            {
                SearchData();
                return;
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                return;
            }
        }

        void frm_nhapketquaKN_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtidchidinh.Text))
                txtidchidinh.Focus();
            else
                Utility.focusCell(grdKetqua, KcbKetquaCl.Columns.KetQua);
            Application.DoEvents();
            chkSaveAndConfirm.Enabled = cmdConfirm.Enabled = grdKetqua.GetDataRows().Any();
            lblMessge.Text = "";
        }

        void cmdSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }
        void FillPatientData(DataRow dr)
        {
            if (dr == null)
            {
                txtPatient_Name.Clear();
                txtDiaChi.Clear();
            }
            else
            {
                txtPatient_Name.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.TenBenhnhan], "");
                txtDiaChi.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.DiaChi], "");
            }
        }
      
        void SearchData()
        {
            try
            {
                dtChidinh = SPs.KnMaukiemnghiemTimkiemchitieu(Utility.Int64Dbnull(txtidchidinh.Text)).GetDataSet().Tables[0];
                if (dtChidinh != null && dtChidinh.Rows.Count > 0)
                {
                    id_dichvu = Utility.Int32Dbnull(dtChidinh.Rows[0][VKcbChidinhcl.Columns.IdDichvu], 0);
                    IdChitietdichvu = Utility.Int32Dbnull(dtChidinh.Rows[0][VKcbChidinhcl.Columns.IdChitietdichvu], 0);
                    co_chitiet = Utility.Int32Dbnull(dtChidinh.Rows[0][VKcbChidinhcl.Columns.CoChitiet], 0);
                    id_chidinh = Utility.Int32Dbnull(dtChidinh.Rows[0]["id_chidinh"], 0);
                    ma_luotkham = Utility.sDbnull(dtChidinh.Rows[0]["ma_dangky"], "");
                    MaChidinh = Utility.sDbnull(dtChidinh.Rows[0]["ma_chidinh"], "");
                    MaBenhpham = Utility.sDbnull(dtChidinh.Rows[0]["ma_benhpham"], "");
                    txtPatient_Name.Text = Utility.sDbnull(dtChidinh.Rows[0]["ten_khachhang"], "");
                    txtDiaChi.Text = Utility.sDbnull(dtChidinh.Rows[0]["dia_chi"], "");
                    objChidinh = KcbChidinhcl.FetchByID(id_chidinh);
                    HienthiNhapketqua(id_dichvu, co_chitiet);
                }
                else
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu chỉ định kiểm nghiệm để nhập kết quả. Liên hệ IT để được trợ giúp");
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
        }

        private void grdKetqua_FormattingRow(object sender, RowLoadEventArgs e)
        {

        }
    }
}
