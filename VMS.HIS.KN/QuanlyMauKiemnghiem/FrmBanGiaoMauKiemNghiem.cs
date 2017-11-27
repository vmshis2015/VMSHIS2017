using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using VNS.HIS.BusRule.Classes;
using VNS.HIS.DAL;
using VNS.HIS.UI.DANHMUC;
using VNS.Libs;

namespace VMS.HIS.KN.QuanlyMauKiemnghiem
{
    public partial class FrmBanGiaoMauKiemNghiem : Form
    {
        public FrmBanGiaoMauKiemNghiem()
        {
            InitializeComponent();
            txthoaly_nguoigiao._OnShowData += txthoaly_nguoigiao__OnShowData;
            txthoaly_nguoinhan._OnShowData += txthoaly_nguoinhan__OnShowData;
            txtvisinh_nguoigiao._OnShowData += txtvisinh_nguoigiao__OnShowData;
            txtvisinh_nguoinhan._OnShowData += txtvisinh_nguoinhan__OnShowData;
            txtthauphu_nguoigiao._OnShowData += txtthauphu_nguoigiao__OnShowData;
            txtthauphu_nguoinhan._OnShowData += txtthauphu_nguoinhan__OnShowData;
        }
        private DataTable _mDanhsachmauchidinh = new DataTable();
        public int Iddichvu = -1;
        public long IdChidinh = -1;
        public int Trangthai = 0;
        private void InitData()
        {
            _mDanhsachmauchidinh =
                  new Select().From(DmucDichvucl.Schema)
                      .Where(DmucDichvucl.Columns.LaDvuKiemnghiem)
                      .IsEqualTo(1)
                      .ExecuteDataSet()
                      .Tables[0];
            txtDichvuKn.Init(_mDanhsachmauchidinh,
                new List<string>()
                {
                    DmucDichvucl.Columns.IdDichvu,
                    DmucDichvucl.Columns.MaDichvu,
                    DmucDichvucl.Columns.TenDichvu
                });
            if (Iddichvu > 0)
            {
                txtDichvuKn.SetId(Iddichvu);
            }
        }

        private void GetData()
        {
            KnChidinhXn objChidinhXn =
                new Select().From(KnChidinhXn.Schema)
                    .Where(KnChidinhXn.Columns.IdChidinh)
                    .IsEqualTo(IdChidinh)
                    .ExecuteSingle<KnChidinhXn>();
            if (objChidinhXn != null)
            {
                txtThetichKhoiluong.Text = objChidinhXn.LuongmauThetich.ToString();
                txtTinhtrangmau.SetCode(objChidinhXn.TinhtrangMau);
         
                txtAssignCode.Text =Utility.sDbnull(objChidinhXn.MaChidinh);
                txtvisinh_luongmau.Text = objChidinhXn.LuongmauVisinh.ToString();
                txtvisinh_nguoigiao.Text = objChidinhXn.NguoigiaoVisinh;
                txtvisinh_nguoinhan.Text = objChidinhXn.NguoinhanVisinh;
                txthoaly_luongmau.Text = objChidinhXn.LuongmauHoaly.ToString();
                txthoaly_nguoigiao.Text = objChidinhXn.NguoigiaoHoaly;
                txthoaly_nguoinhan.Text = objChidinhXn.NguoinhanHoaly;
                txtthauphu_luongmau.Text = objChidinhXn.LuongmauThauphu.ToString();
                txtthauphu_nguoigiao.Text = objChidinhXn.NguoigiaoThauphu;
                txtthauphu_nguoinhan.Text = objChidinhXn.NguoinhanThauphu;
            }
        }
        private void FrmBanGiaoMauKiemNghiem_Load(object sender, EventArgs e)
        {
            try
            {
                txtTinhtrangmau.Init();
                txthoaly_nguoigiao.Init();
                txtvisinh_nguoigiao.Init();
                txtthauphu_nguoigiao.Init();
                txthoaly_nguoinhan.Init();
                txtvisinh_nguoinhan.Init();
                txtthauphu_nguoinhan.Init();
                InitData();
                GetData();
                lblMessge.Text = "";
            }
            catch (Exception ex)
            {

               Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
            
        }

        private void cmdBanGiao_Click(object sender, EventArgs e)
        {
            try
            {
                decimal thetich = Utility.DecimaltoDbnull(txtThetichKhoiluong.Text);
                decimal tongmau = Utility.DecimaltoDbnull(txthoaly_luongmau.Text)+
                                  Utility.DecimaltoDbnull(txtvisinh_luongmau.Text)+
                                 Utility.DecimaltoDbnull(txtthauphu_luongmau.Text);
                if (tongmau > thetich)
                {
                    if ( Utility.AcceptQuestion("Tổng lượng mẫu bàn giao lớn hơn thể tích mẫu quy định vui lòng kiểm tra lại", "Thông báo", true))
                    {
                        new Update(KnChidinhXn.Schema)
                            .Set(KnChidinhXn.Columns.TrangThai).EqualTo(1)
                            .Set(KnChidinhXn.Columns.LuongmauHoaly).EqualTo(Utility.DecimaltoDbnull(txthoaly_luongmau.Text))
                            .Set(KnChidinhXn.Columns.LuongmauVisinh).EqualTo(Utility.DecimaltoDbnull(txtvisinh_luongmau.Text))
                            .Set(KnChidinhXn.Columns.LuongmauThauphu).EqualTo(Utility.DecimaltoDbnull(txtthauphu_luongmau.Text))
                            .Set(KnChidinhXn.Columns.NguoigiaoMau).EqualTo(Utility.sDbnull(txthoaly_nguoigiao.Text))
                            .Set(KnChidinhXn.Columns.NguoigiaoVisinh).EqualTo(Utility.sDbnull(txtvisinh_nguoigiao.Text))
                            .Set(KnChidinhXn.Columns.NguoigiaoHoaly).EqualTo(Utility.sDbnull(txthoaly_nguoigiao.Text))
                            .Set(KnChidinhXn.Columns.NguoigiaoThauphu).EqualTo(Utility.sDbnull(txtthauphu_nguoigiao.Text))
                            .Set(KnChidinhXn.Columns.NguoinhanHoaly).EqualTo(Utility.sDbnull(txthoaly_nguoinhan.Text))
                            .Set(KnChidinhXn.Columns.NguoinhanVisinh).EqualTo(Utility.sDbnull(txtvisinh_nguoinhan.Text))
                            .Set(KnChidinhXn.Columns.NguoinhanThauphu).EqualTo(Utility.sDbnull(txtthauphu_nguoinhan.Text))
                            .Where(KnChidinhXn.Columns.IdChidinh).IsEqualTo(IdChidinh).Execute();
                            Trangthai = 1;
                            Utility.SetMsg(lblMessge,"Bàn giao mẫu thành công",false);
                    }
                }
                else
                {


                    new Update(KnChidinhXn.Schema).Set(KnChidinhXn.Columns.TrangThai).EqualTo(1)
                        .Set(KnChidinhXn.Columns.LuongmauHoaly).EqualTo(Utility.DecimaltoDbnull(txthoaly_luongmau.Text))
                        .Set(KnChidinhXn.Columns.LuongmauVisinh).EqualTo(Utility.DecimaltoDbnull(txtvisinh_luongmau.Text))
                        .Set(KnChidinhXn.Columns.LuongmauThauphu).EqualTo(Utility.DecimaltoDbnull(txtthauphu_luongmau.Text))
                        .Set(KnChidinhXn.Columns.NguoigiaoMau).EqualTo(Utility.sDbnull(txthoaly_nguoigiao.Text))
                        .Set(KnChidinhXn.Columns.NguoigiaoVisinh).EqualTo(Utility.sDbnull(txtvisinh_nguoigiao.Text))
                        .Set(KnChidinhXn.Columns.NguoigiaoHoaly).EqualTo(Utility.sDbnull(txthoaly_nguoigiao.Text))
                        .Set(KnChidinhXn.Columns.NguoigiaoThauphu).EqualTo(Utility.sDbnull(txtthauphu_nguoigiao.Text))
                        .Set(KnChidinhXn.Columns.NguoinhanHoaly).EqualTo(Utility.sDbnull(txthoaly_nguoinhan.Text))
                        .Set(KnChidinhXn.Columns.NguoinhanVisinh).EqualTo(Utility.sDbnull(txtvisinh_nguoinhan.Text))
                        .Set(KnChidinhXn.Columns.NguoinhanThauphu).EqualTo(Utility.sDbnull(txtthauphu_nguoinhan.Text))
                        .Where(KnChidinhXn.Columns.IdChidinh).IsEqualTo(IdChidinh).Execute();
                    Trangthai = 1;
                    Utility.SetMsg(lblMessge, "Bàn giao mẫu thành công", false);
                }
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:"+ exception.Message);
            }
        }

        private void cmdThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            Dispose();
        }

        private void txthoaly_nguoigiao__OnShowData()
        {
            var dmuchung = new DMUC_DCHUNG(txthoaly_nguoigiao.LOAI_DANHMUC);
            dmuchung.ShowDialog();
            if(!dmuchung.m_blnCancel)
            {
                string oldCode = txthoaly_nguoigiao.myCode;
                txthoaly_nguoigiao.Init();
                txthoaly_nguoigiao.SetCode(oldCode);
                txthoaly_nguoigiao.Focus();

            }
        }

        private void txtvisinh_nguoigiao__OnShowData()
        {
            var dmuchung = new DMUC_DCHUNG(txtvisinh_nguoigiao.LOAI_DANHMUC);
            dmuchung.ShowDialog();
            if (!dmuchung.m_blnCancel)
            {
                string oldCode = txtvisinh_nguoigiao.myCode;
                txtvisinh_nguoigiao.Init();
                txtvisinh_nguoigiao.SetCode(oldCode);
                txtvisinh_nguoigiao.Focus();

            }
        }

        private void txtthauphu_nguoigiao__OnShowData()
        {
            var dmuchung = new DMUC_DCHUNG(txtthauphu_nguoigiao.LOAI_DANHMUC);
            dmuchung.ShowDialog();
            if (!dmuchung.m_blnCancel)
            {
                string oldCode = txtthauphu_nguoigiao.myCode;
                txtthauphu_nguoigiao.Init();
                txtthauphu_nguoigiao.SetCode(oldCode);
                txtthauphu_nguoigiao.Focus();

            }
        }

        private void txthoaly_nguoinhan__OnShowData()
        {
            var dmuchung = new DMUC_DCHUNG(txthoaly_nguoinhan.LOAI_DANHMUC);
            dmuchung.ShowDialog();
            if (!dmuchung.m_blnCancel)
            {
                string oldCode = txthoaly_nguoinhan.myCode;
                txthoaly_nguoinhan.Init();
                txthoaly_nguoinhan.SetCode(oldCode);
                txthoaly_nguoinhan.Focus();

            }
        }

        private void txtvisinh_nguoinhan__OnShowData()
        {
            var dmuchung = new DMUC_DCHUNG(txtvisinh_nguoinhan.LOAI_DANHMUC);
            dmuchung.ShowDialog();
            if (!dmuchung.m_blnCancel)
            {
                string oldCode = txtvisinh_nguoinhan.myCode;
                txtvisinh_nguoinhan.Init();
                txtvisinh_nguoinhan.SetCode(oldCode);
                txtvisinh_nguoinhan.Focus();

            }
        }

        private void txtthauphu_nguoinhan__OnShowData()
        {
            var dmuchung = new DMUC_DCHUNG(txtthauphu_nguoinhan.LOAI_DANHMUC);
            dmuchung.ShowDialog();
            if (!dmuchung.m_blnCancel)
            {
                string oldCode = txtthauphu_nguoinhan.myCode;
                txtthauphu_nguoinhan.Init();
                txtthauphu_nguoinhan.SetCode(oldCode);
                txtthauphu_nguoinhan.Focus();

            }
        }

    }
}
