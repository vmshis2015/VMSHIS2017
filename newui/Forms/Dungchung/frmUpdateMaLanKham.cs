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
using SubSonic;

namespace VNS.HIS.UI.Forms.Dungchung
{
    public partial class frmUpdateMaLanKham : Form
    {
        public frmUpdateMaLanKham()
        {
            InitializeComponent();
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                SqlQuery sqlkt = new Select().From(KcbLuotkham.Schema).Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(txtmabenhnhanmoi.Text);
                if(sqlkt.GetRecordCount()>0)
                {
                    Utility.ShowMsg("Mã lần khám này đang được sử dụng cho bệnh nhân khác! Bạn cần check lại mã lượt khám khác");
                    txtmabenhnhanmoi.Focus();
                    return;
                }
                var sp = SPs.SpUpdateMaLuotKham(Utility.sDbnull(txtmabenhnhanmoi.Text), Utility.sDbnull(txtmabenhnhancu.Text));
                sp.Execute();
                THU_VIEN_CHUNG.Log(Name, globalVariables.UserName,
                          string.Format("Thực hiện update mã lần khám thành công! Từ mã lần khám  {0} sang mã lần khám {1}", Utility.sDbnull(txtmabenhnhanmoi.Text), Utility.sDbnull(txtmabenhnhancu.Text)), action.Update, "");
                Utility.ShowMsg("Bạn đã update lần khám thành công!");
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
           
        }

        private void txtmalankhammoi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && Utility.DoTrim(txtmabenhnhanmoi.Text) != "")
            {
                string _maluotkham  = Utility.AutoFullPatientCode(txtmabenhnhanmoi.Text);
                txtmabenhnhanmoi.Text = _maluotkham;
            }
        }

        private void cmdThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void frmUpdateMaLanKham_Load(object sender, EventArgs e)
        {
            if (Utility.Int64Dbnull(txtidbenhnhancu.Text) > 0)
            {
                KcbDanhsachBenhnhan objBenhNhan =
                    new Select().From(KcbDanhsachBenhnhan.Schema)
                        .Where(KcbDanhsachBenhnhan.Columns.IdBenhnhan)
                        .IsEqualTo(Utility.Int64Dbnull(txtidbenhnhancu.Text))
                        .ExecuteSingle<KcbDanhsachBenhnhan>();
                if (objBenhNhan != null)
                {
                    txttenbenhnhancu.Text = Utility.sDbnull(objBenhNhan.TenBenhnhan);
                    txtnamsinhcu.Text = Utility.sDbnull(objBenhNhan.NamSinh);
                }
            }
        }
    }
}
