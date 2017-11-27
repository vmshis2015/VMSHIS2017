﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using VNS.HIS.UI.NGOAITRU;
using VNS.Libs;
using Janus.Windows.GridEX.EditControls;
using VNS.HIS.DAL;
using VNS.HIS.BusRule.Classes;
using SubSonic;
using VNS.HIS.UI.DANHMUC;
using System.Transactions;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using VNS.HIS.UI.Forms.Cauhinh;
namespace VNS.HIS.UI.Forms.NGOAITRU
{
    public partial class frm_chuyenvien : Form
    {
        bool AllowTextChanged = false;
        KcbPhieuchuyenvien objPhieuchuyenvien = null;
        KcbLuotkham objLuotkham = null;
        action m_enAct = action.Insert;
        public byte noitru = 0;
        public frm_chuyenvien()
        {
            InitializeComponent();
            InitEvents();
        }
        void InitEvents()
        {
            txtMaluotkham.KeyDown += new KeyEventHandler(txtMaluotkham_KeyDown);
            this.KeyDown += new KeyEventHandler(frm_chuyenvien_KeyDown);
            this.Load += new EventHandler(frm_chuyenvien_Load);

            txtphuongtienvc._OnSaveAs += new UCs.AutoCompleteTextbox_Danhmucchung.OnSaveAs(txtphuongtienvc__OnSaveAs);
            txtphuongtienvc._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtphuongtienvc__OnShowData);

            txtTrangthainguoibenh._OnSaveAs += new UCs.AutoCompleteTextbox_Danhmucchung.OnSaveAs(txtTrangthainguoibenh__OnSaveAs);
            txtTrangthainguoibenh._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtTrangthainguoibenh__OnShowData);

            txtdauhieucls._OnSaveAs += new UCs.AutoCompleteTextbox_Danhmucchung.OnSaveAs(txtdauhieucls__OnSaveAs);
            txtdauhieucls._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtdauhieucls__OnShowData);
            txtMaBenhChinh.KeyDown += txtMaBenhChinh_KeyDown;
            txtMaBenhChinh.TextChanged += txtMaBenhChinh_TextChanged;
            txtHuongdieutri._OnSaveAs += new UCs.AutoCompleteTextbox_Danhmucchung.OnSaveAs(txtHuongdieutri__OnSaveAs);
            txtHuongdieutri._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtHuongdieutri__OnShowData);

            cmdExit.Click += new EventHandler(cmdExit_Click);
            cmdChuyen.Click += new EventHandler(cmdChuyen_Click);
            cmdHuy.Click += new EventHandler(cmdHuy_Click);

            cmdgetPatient.Click += new EventHandler(cmdgetPatient_Click);
            cmdGetBV.Click += new EventHandler(cmdGetBV_Click);
            cmdPrint.Click += new EventHandler(cmdPrint_Click);
        }

        void cmdPrint_Click(object sender, EventArgs e)
        {
              Utility.WaitNow(this);
                DataTable dtData =
                                 SPs.KcbThamkhamPhieuchuyenvien(Utility.DoTrim(txtMaluotkham.Text)).GetDataSet().Tables[0];

                if (dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu cho báo cáo", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                THU_VIEN_CHUNG.CreateXML(dtData, "thamkham_phieuchuyenvien.XML");
                Utility.UpdateLogotoDatatable(ref dtData);
                string StaffName = globalVariables.gv_strTenNhanvien;
                if (string.IsNullOrEmpty(globalVariables.gv_strTenNhanvien)) StaffName = globalVariables.UserName;

                string tieude = "", reportname = "";
                ReportDocument crpt = Utility.GetReport("thamkham_phieuchuyenvien", ref tieude, ref reportname);
                if (crpt == null) return;
                try
                {
             
                frmPrintPreview objForm = new frmPrintPreview("PHIẾU CHUYỂN TUYẾN", crpt, true, dtData.Rows.Count <= 0 ? false : true);
                crpt.SetDataSource(dtData);
               
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = "thamkham_phieuchuyenvien";
                Utility.SetParameterValue(crpt, "StaffName", StaffName);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTimeWithThanhPho(dtpNgayin.Value));
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, globalVariables.SysDate));
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                Utility.DefaultNow(this);
                 GC.Collect();
                 Utility.FreeMemory(crpt);
            }
        }
        void cmdGetBV_Click(object sender, EventArgs e)
        {
            frm_danhsachbenhvien _danhsachbenhvien = new frm_danhsachbenhvien();
            if (_danhsachbenhvien.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtNoichuyenden.SetId(_danhsachbenhvien.idBenhvien);
            }
            
        }

        void cmdgetPatient_Click(object sender, EventArgs e)
        {
            frm_DSACH_BN_TKIEM _DSACH_BN_TKIEM = new frm_DSACH_BN_TKIEM("ALL");
            if (_DSACH_BN_TKIEM.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtMaluotkham.Text = _DSACH_BN_TKIEM.MaLuotkham;
                txtMaluotkham_KeyDown(txtMaluotkham, new KeyEventArgs(Keys.Enter));
            }
        }

        void cmdHuy_Click(object sender, EventArgs e)
        {
            if(objLuotkham==null)
            {
                Utility.SetMsg(lblMsg,"Bạn cần chọn bệnh nhân trước khi thực hiện hủy chuyển viện",true);
                return;
            }
            if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn hủy chuyển viện cho bệnh nhân {0} hay không?", txtTenBN.Text), "Xác nhận hủy chuyển viện", true))
            {
                try
                {
                    using (var scope = new TransactionScope())
                    {
                        using (var dbscope = new SharedDbConnectionScope())
                        {
                            objLuotkham.TthaiChuyendi=0;
                            objLuotkham.IdBenhvienDi=-1;
                            objLuotkham.IdBacsiChuyenvien = -1;
                            objLuotkham.NgayRavien = null;
                            objLuotkham.IsNew=false;
                            objLuotkham.MarkOld();
                            objLuotkham.Save();
                            new Delete().From(KcbPhieuchuyenvien.Schema).Where(KcbPhieuchuyenvien.Columns.IdPhieu).IsEqualTo(Utility.Int32Dbnull(txtId.Text, -1)).Execute();
                           
                        }
                        scope.Complete();
                        Utility.SetMsg(lblMsg,string.Format( "Hủy chuyển viện cho bệnh nhân {0} thành công",txtTenBN.Text), true);
                    }
                }
                catch (Exception ex)
                {
                    Utility.CatchException(ex);
                }
            }
        }

        void cmdChuyen_Click(object sender, EventArgs e)
        {
            Utility.SetMsg(lblMsg, "", false);
            if (txtNoichuyenden.MyCode == "-1")
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin nơi chuyển đến", true);
                txtNoichuyenden.Focus();
                return;
            }
            if (Utility.DoTrim(txtdauhieucls.Text) == "")
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin dấu hiệu lâm sàng", true);
                txtdauhieucls.Focus();
                return;
            }
            //if (Utility.DoTrim(txtketquaCls.Text) == "")
            //{
            //    Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin kết quả xét nghiệm, cận lâm sàng", true);
            //    txtketquaCls.Focus();
            //    return;
            //}
            if (Utility.DoTrim(txtChandoan.Text) == "" && Utility.DoTrim(txtMaBenhChinh.Text) =="")
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin chẩn đoán", true);
                txtMaBenhChinh.Focus();
                return;
            }
            //if (Utility.DoTrim(txtThuocsudung.Text) == "")
            //{
            //    Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin Phương pháp, thủ thuật, kỹ thuật, thuốc đã sử dụng trong điều trị:", true);
            //    txtThuocsudung.Focus();
            //    return;
            //}
            if (Utility.DoTrim(txtTrangthainguoibenh.Text) == "")
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin trạng thái người bệnh", true);
                txtTrangthainguoibenh.Focus();
                return;
            }
            if (Utility.DoTrim(txtHuongdieutri.Text) == "")
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin hướng điều trị", true);
                txtHuongdieutri.Focus();
                return;
            }
            //if (Utility.DoTrim(txtphuongtienvc.Text) == "")
            //{
            //    Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin phương tiện vận chuyển", true);
            //    txtphuongtienvc.Focus();
            //    return;
            //}
            //if (Utility.DoTrim(txtNguoivanchuyen.Text) == "")
            //{
            //    Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin người vận chuyển", true);
            //    txtNguoivanchuyen.Focus();
            //    return;
            //}

            try
            {
                KcbPhieuchuyenvien _phieuchuyenvien = null;
                SqlQuery sqlkt = new Select().From(KcbPhieuchuyenvien.Schema).Where(KcbPhieuchuyenvien.Columns.MaLuotkham).IsEqualTo(Utility.sDbnull(txtMaluotkham.Text));
                if (m_enAct == action.Insert && sqlkt.GetRecordCount()<=0)
                {
                    _phieuchuyenvien = new KcbPhieuchuyenvien();
                    _phieuchuyenvien.IsNew = true;
                    _phieuchuyenvien.NgayTao = globalVariables.SysDate;
                    _phieuchuyenvien.NguoiTao = globalVariables.UserName;
                    _phieuchuyenvien.SoChuyentuyen = Utility.Int32Dbnull(GetmaxSoChuyenVien());
                }
                else
                {
                    _phieuchuyenvien = KcbPhieuchuyenvien.FetchByID(Utility.Int32Dbnull(txtId.Text));
                    _phieuchuyenvien.IsNew = false;
                    _phieuchuyenvien.MarkOld();
                    _phieuchuyenvien.NguoiSua = globalVariables.UserName;
                    _phieuchuyenvien.NgaySua = globalVariables.SysDate;
                    _phieuchuyenvien.SoChuyentuyen = Utility.Int32Dbnull(txtsochuyenvien.Text, -1);
                }
                _phieuchuyenvien.IdBenhnhan = objLuotkham.IdBenhnhan;
                _phieuchuyenvien.MaLuotkham = objLuotkham.MaLuotkham;
                _phieuchuyenvien.IdBenhvienChuyenden =Utility.Int16Dbnull( txtNoichuyenden.MyID,-1);
                _phieuchuyenvien.DauhieuCls = Utility.DoTrim(txtdauhieucls.Text);
                _phieuchuyenvien.KetquaXnCls = Utility.DoTrim(txtketquaCls.Text);
                _phieuchuyenvien.ChanDoan = Utility.DoTrim(txtChandoan.Text);
                _phieuchuyenvien.Mabenh = Utility.DoTrim(txtMaBenhChinh.Text);
                _phieuchuyenvien.ThuocSudung = Utility.DoTrim(txtThuocsudung.Text);
                _phieuchuyenvien.TrangthaiBenhnhan = Utility.DoTrim(txtTrangthainguoibenh.Text);
                _phieuchuyenvien.HuongDieutri = Utility.DoTrim(txtHuongdieutri.Text);
                _phieuchuyenvien.LydoChuyen = Utility.sDbnull(radDuDieukien.Checked ? "1" : "0");
                _phieuchuyenvien.PhuongtienChuyen = Utility.DoTrim(txtphuongtienvc.Text);
                _phieuchuyenvien.NgayChuyenvien = dtNgaychuyenvien.Value;
                _phieuchuyenvien.IdBacsiChuyenvien = Utility.Int16Dbnull(cboDoctorAssign.SelectedValue, -1);
                _phieuchuyenvien.TenNguoichuyen = Utility.DoTrim(txtNguoivanchuyen.Text);
                _phieuchuyenvien.NoiTru = noitru;
                _phieuchuyenvien.IdRavien = Utility.Int32Dbnull(txtIdravien.Text,-1);
                _phieuchuyenvien.IdKhoanoitru = Utility.Int32Dbnull(txtIdkhoanoitru.Text, -1);
                _phieuchuyenvien.IdBuong = Utility.Int32Dbnull(txtidBuong.Text, -1);
                _phieuchuyenvien.IdGiuong = Utility.Int32Dbnull(txtidgiuong.Text, -1);
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        _phieuchuyenvien.Save();
                        objLuotkham.TthaiChuyendi = 1;
                        objLuotkham.IdBacsiChuyenvien = _phieuchuyenvien.IdBacsiChuyenvien;
                        objLuotkham.MabenhChinh = _phieuchuyenvien.Mabenh;
                        objLuotkham.NgayKetthuc = _phieuchuyenvien.NgayChuyenvien;
                        objLuotkham.NguoiKetthuc = _phieuchuyenvien.NguoiTao;
                        objLuotkham.NgayRavien = _phieuchuyenvien.NgayChuyenvien;
                        objLuotkham.KetLuan = "Chuyển viện";
                        objLuotkham.HuongDieutri = "Chuyển viện";
                        objLuotkham.IdBenhvienDi = Utility.Int16Dbnull(txtNoichuyenden.MyID,-1);
                        objLuotkham.IsNew = false;
                        objLuotkham.MarkOld();
                        objLuotkham.Save();
                        KcbChandoanKetluan objChuandoanKetluan =
                          new Select().From(KcbChandoanKetluan.Schema).Where(KcbChandoanKetluan.Columns.MaLuotkham).
                              IsEqualTo(objLuotkham.MaLuotkham).And(KcbChandoanKetluan.Columns.IdBenhnhan).IsEqualTo(
                                  objLuotkham.IdBenhnhan).ExecuteSingle<KcbChandoanKetluan>();
                        if (objChuandoanKetluan != null)
                        {
                            new Update(KcbChandoanKetluan.Schema)
                                .Set(KcbChandoanKetluan.Columns.MabenhChinh).EqualTo(objLuotkham.MabenhChinh)
                         .Where(KcbChandoanKetluan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                         .And(KcbChandoanKetluan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                         .And(KcbChandoanKetluan.Columns.Noitru).IsEqualTo(0)
                         .Execute();

                        }
                        else
                        {
                            objChuandoanKetluan = new KcbChandoanKetluan();
                            objChuandoanKetluan.IdBenhnhan = Utility.Int64Dbnull(objLuotkham.IdBenhnhan);
                            objChuandoanKetluan.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham, "");
                            objChuandoanKetluan.SongayDieutri = 1;
                            objChuandoanKetluan.MabenhChinh = objLuotkham.MabenhChinh;
                            objChuandoanKetluan.NgayChandoan = globalVariables.SysDate;
                            objChuandoanKetluan.NguoiTao = globalVariables.UserName;
                            objChuandoanKetluan.IdBacsikham = globalVariables.gv_intIDNhanvien;
                            objChuandoanKetluan.IpMaytao = globalVariables.gv_strIPAddress;
                            objChuandoanKetluan.Noitru = 0;
                            objChuandoanKetluan.IsNew = true;
                            objChuandoanKetluan.Save();
                        }
                        new Update(KcbDangkyKcb.Schema).Set(KcbDangkyKcb.Columns.TrangThai).EqualTo(1).Where(
                            KcbDangkyKcb.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).And(
                                KcbDangkyKcb.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).Execute();
                    }
                    scope.Complete();
                }
                Utility.SetMsg(lblMsg, "Cập nhật phiếu chuyển viện thành công", false);
                if (m_enAct == action.Insert)
                    cmdPrint.Enabled = true;
                m_enAct = action.Update;
                txtId.Text = _phieuchuyenvien.IdPhieu.ToString();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);   
            }
        }

        void cmdExit_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        void txtHuongdieutri__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtphuongtienvc.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtphuongtienvc.Text;
                txtphuongtienvc.Init();
                txtphuongtienvc._Text = oldCode;
                txtphuongtienvc.Focus();
            }    
        }

        void txtHuongdieutri__OnSaveAs()
        {
            if (Utility.DoTrim(txtHuongdieutri.Text) == "") return;
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtphuongtienvc.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtphuongtienvc.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtphuongtienvc.Text;
                txtphuongtienvc.Init();
                txtphuongtienvc._Text = oldCode;
                txtphuongtienvc.Focus();
            }   
        }

        void txtdauhieucls__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtdauhieucls.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtdauhieucls.Text;
                txtdauhieucls.Init();
                txtdauhieucls._Text = oldCode;
                txtdauhieucls.Focus();
            }   
        }

        void txtdauhieucls__OnSaveAs()
        {
            if (Utility.DoTrim(txtdauhieucls.Text) == "") return;
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtdauhieucls.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtdauhieucls.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtdauhieucls.Text;
                txtdauhieucls.Init();
                txtdauhieucls._Text = oldCode;
                txtdauhieucls.Focus();
            }    
        }

        void txtTrangthainguoibenh__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtTrangthainguoibenh.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtTrangthainguoibenh.Text;
                txtTrangthainguoibenh.Init();
                txtTrangthainguoibenh._Text = oldCode;
                txtTrangthainguoibenh.Focus();
            }   
        }

        void txtTrangthainguoibenh__OnSaveAs()
        {
            if (Utility.DoTrim(txtTrangthainguoibenh.Text) == "") return;
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtTrangthainguoibenh.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtTrangthainguoibenh.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtTrangthainguoibenh.Text;
                txtTrangthainguoibenh.Init();
                txtTrangthainguoibenh._Text = oldCode;
                txtTrangthainguoibenh.Focus();
            }   
        }

        void txtphuongtienvc__OnShowData()
        {

            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtphuongtienvc.LOAI_DANHMUC);
          
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtphuongtienvc.Text;
                txtphuongtienvc.Init();
                txtphuongtienvc._Text = oldCode;
                txtphuongtienvc.Focus();
            }    
        }

        void txtphuongtienvc__OnSaveAs()
        {
            if (Utility.DoTrim(txtHuongdieutri.Text) == "") return;
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtphuongtienvc.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtphuongtienvc.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtphuongtienvc.Text;
                txtphuongtienvc.Init();
                txtphuongtienvc._Text = oldCode;
                txtphuongtienvc.Focus();
            }    
        }
        private DataTable dt_ICD = new DataTable();
        private readonly KCB_THAMKHAM _KCB_THAMKHAM = new KCB_THAMKHAM();
        private void Load_DSach_ICD()
        {
            try
            {
                dt_ICD = _KCB_THAMKHAM.LaydanhsachBenh();
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy danh sách ICD");
            }
        }
        void frm_chuyenvien_Load(object sender, EventArgs e)
        {
            try
            {
                LaydanhsachBacsi();
                AutocompleteBenhvien();
                txtphuongtienvc.Init();
                txtTrangthainguoibenh.Init();
                txtHuongdieutri.Init();
                txtdauhieucls.Init();
                dtNgaychuyenvien.Value = DateTime.Now;
                dtpNgayin.Value = DateTime.Now;
                Load_DSach_ICD();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        private bool hasMorethanOne = true;
        private bool isLike = true;
        private void txtMaBenhChinh_TextChanged(object sender, EventArgs e)
        {
            try
            {
                hasMorethanOne = true;
                DataRow[] arrDr;
                if (isLike)
                    arrDr =
                        globalVariables.gv_dtDmucBenh.Select(DmucBenh.Columns.MaBenh + " like '" +
                                                             Utility.sDbnull(txtMaBenhChinh.Text, "") +
                                                             "%'");
                else
                    arrDr =
                        globalVariables.gv_dtDmucBenh.Select(DmucBenh.Columns.MaBenh + " = '" +
                                                             Utility.sDbnull(txtMaBenhChinh.Text, "") +
                                                             "'");
                if (!string.IsNullOrEmpty(txtMaBenhChinh.Text))
                {
                    if (arrDr.GetLength(0) == 1)
                    {
                        hasMorethanOne = false;
                        txtMaBenhChinh.Text = arrDr[0][DmucBenh.Columns.MaBenh].ToString();
                        txtChandoan.Text = Utility.sDbnull(arrDr[0][DmucBenh.Columns.TenBenh], "");
                        //  txtDisease_ID.Text = Utility.sDbnull(arrDr[0][DmucBenh.Columns.DiseaseId], "-1");
                    }
                    else
                    {
                        //txtDisease_ID.Text = "-1";
                        txtChandoan.Text = "";
                    }
                }
                else
                {
                    //  txtDisease_ID.Text = "-1";

                    txtChandoan.Text = "";
                    //cmdSearchBenhChinh.PerformClick();
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                //cboChonBenhAn.Visible = Utility.DoTrim(txtMaBenhChinh.Text) != "" &&
                //                        THU_VIEN_CHUNG.Laygiatrithamsohethong("BENH_AN", "0", false) == "1" &&
                //                        (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_ICD_BENH_AN_NGOAI_TRU", "ALL", false) ==
                //                         "ALL" ||
                //                         THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_ICD_BENH_AN_NGOAI_TRU", "ALL", false)
                //                             .Contains(Utility.DoTrim(txtMaBenhChinh.Text)));
                //lblBenhan.Visible = cboChonBenhAn.Visible;
                //setChanDoan();
            }
        }
        private void txtMaBenhChinh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               DSACH_ICD(txtMaBenhChinh, DmucChung.Columns.Ma, 0);
               hasMorethanOne = false;
            }
        }

        private void DSACH_ICD(EditBox tEditBox, string LOAITIMKIEM, int CP)
        {
            try
            {
                string sFillter = "";
                if (LOAITIMKIEM.ToUpper() == DmucChung.Columns.Ten)
                {
                    sFillter = " Disease_Name like '%" + tEditBox.Text + "%' OR FirstChar LIKE '%" + tEditBox.Text +
                               "%'";
                }
                else if (LOAITIMKIEM == DmucChung.Columns.Ma)
                {
                    sFillter = DmucBenh.Columns.MaBenh + " LIKE '%" + tEditBox.Text + "%'";
                }
                DataRow[] dataRows;
                dataRows = dt_ICD.Select(sFillter);
                if (dataRows.Length == 1)
                {
                    if (CP == 0)
                    {
                        txtMaBenhChinh.Text = "";
                        txtMaBenhChinh.Text = Utility.sDbnull(dataRows[0][DmucBenh.Columns.MaBenh], "");
                        hasMorethanOne = false;
                        txtMaBenhChinh_TextChanged(txtMaBenhChinh, new EventArgs());
                        txtMaBenhChinh.Focus();
                    }
                }
                else if (dataRows.Length > 1)
                {
                    var frmDanhSachIcd = new frm_DanhSach_ICD(CP);
                    frmDanhSachIcd.dt_ICD = dataRows.CopyToDataTable();
                    frmDanhSachIcd.ShowDialog();
                    if (!frmDanhSachIcd.has_Cancel)
                    {
                        List<GridEXRow> lstSelectedRows = frmDanhSachIcd.lstSelectedRows;
                        if (CP == 0)
                        {
                            isLike = false;
                            txtMaBenhChinh.Text = "";
                            txtMaBenhChinh.Text =
                                Utility.sDbnull(lstSelectedRows[0].Cells[DmucBenh.Columns.MaBenh].Value, "");
                            hasMorethanOne = false;
                            txtMaBenhChinh_TextChanged(txtMaBenhChinh, new EventArgs());
                           // txtMaBenhChinh_KeyDown(txtMaBenhChinh, new KeyEventArgs(Keys.Enter));
                        }
                      //  tEditBox.Focus();
                    }
                    else
                    {
                        hasMorethanOne = true;
                      //  tEditBox.Focus();
                    }
                }
                else
                {
                    hasMorethanOne = true;
                    //tEditBox.SelectAll();
                }
            }
            catch
            {
            }
            finally
            {
                isLike = true;
                hasMorethanOne = true;
            }
        }
        private void LaydanhsachBacsi()
        {
            try
            {
                DataTable dtBacsi = THU_VIEN_CHUNG.LaydanhsachBacsi(-1, 0);
                DataBinding.BindDataCombox(cboDoctorAssign, dtBacsi, DmucNhanvien.Columns.IdNhanvien,
                                           DmucNhanvien.Columns.TenNhanvien, "---Bác sỹ chỉ định---", true);
                if (globalVariables.gv_intIDNhanvien <= 0)
                {
                    if (cboDoctorAssign.Items.Count > 0)
                        cboDoctorAssign.SelectedIndex = 0;
                }
                else
                {
                    if (cboDoctorAssign.Items.Count > 0)
                        cboDoctorAssign.SelectedIndex = Utility.GetSelectedIndex(cboDoctorAssign,
                                                                                 globalVariables.gv_intIDNhanvien.ToString());
                }
            }
            catch (Exception exception)
            {
                // throw;
            }

        }
        void frm_chuyenvien_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.Control && e.KeyCode == Keys.S) cmdChuyen.PerformClick();
            if (e.Control && e.KeyCode == Keys.P) cmdPrint.PerformClick();
        }
        public static  int GetmaxSoChuyenVien()
        {
            int sochuyenvien = -1;
            sochuyenvien = new
        Select(Aggregate.Max(KcbPhieuchuyenvien.SoChuyentuyenColumn))
        .From(KcbPhieuchuyenvien.Schema)
        .ExecuteScalar<int>();
            return sochuyenvien + 1;
        }
        public void txtMaluotkham_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && Utility.DoTrim(txtMaluotkham.Text) != "")
                {
                    var dtPatient = new DataTable();
                        objLuotkham = null;
                        string _patient_Code = Utility.AutoFullPatientCode(txtMaluotkham.Text);
                        ClearControls();

                        //dtPatient = new KCB_THAMKHAM().TimkiemBenhnhan(txtMaluotkham.Text,
                        //                               -1,0, 0);

                        //DataRow[] arrPatients = dtPatient.Select(KcbLuotkham.Columns.MaLuotkham + "='" + _patient_Code + "'");
                        //if (arrPatients.GetLength(0) <= 0)
                        //{
                        //    if (dtPatient.Rows.Count > 1)
                        //    {
                        //        var frm = new frm_DSACH_BN_TKIEM();
                        //        frm.MaLuotkham = txtMaluotkham.Text;
                        //        frm.dtPatient = dtPatient;
                        //        frm.ShowDialog();
                        //        if (!frm.has_Cancel)
                        //        {
                        //            txtMaluotkham.Text = frm.MaLuotkham;
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    txtMaluotkham.Text = _patient_Code;
                        //}
                        txtMaluotkham.Text = _patient_Code;
                        DataTable dt_Patient = new KCB_THAMKHAM().TimkiemThongtinBenhnhansaukhigoMaBN(txtMaluotkham.Text, -1, globalVariables.MA_KHOA_THIEN);
                        if (dt_Patient != null && dt_Patient.Rows.Count > 0)
                        {
                            
                            txtIdBn.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan], "");
                            objLuotkham = new Select().From(KcbLuotkham.Schema).Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(txtIdBn.Text)
                                .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(txtMaluotkham.Text)
                                .ExecuteSingle<KcbLuotkham>();
                            txtTenBN.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbDanhsachBenhnhan.Columns.TenBenhnhan], "");
                            txttuoi.Text = Utility.sDbnull(dt_Patient.Rows[0]["Tuoi"], "");
                            txtgioitinh.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbDanhsachBenhnhan.Columns.GioiTinh], "");
                            txtDiaChi.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbDanhsachBenhnhan.Columns.DiaChi], "");
                            txtmatheBhyt.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.MatheBhyt], "");
                            txtKhoanoitru.Text = Utility.sDbnull(dt_Patient.Rows[0]["ten_khoaphong_noitru"], "");
                            txtBuong.Text = Utility.sDbnull(dt_Patient.Rows[0][NoitruDmucBuong.Columns.TenBuong], "");
                            txtGiuong.Text = Utility.sDbnull(dt_Patient.Rows[0][NoitruDmucGiuongbenh.Columns.TenGiuong], "");
                            txtIdkhoanoitru.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.IdKhoanoitru], "-1");
                            txtIdravien.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.IdRavien], "-1");
                            txtidBuong.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.IdBuong], "-1");
                            txtidgiuong.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.IdGiuong], "-1");
                            txtThuocsudung.Text = Utility.sDbnull(dt_Patient.Rows[0]["thuoc_sudung"], "");
                            txtMaBenhChinh.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.MabenhChinh],"");
                            txtChandoan.Text = Utility.sDbnull(dt_Patient.Rows[0]["chan_doan"],"");
                            txtketquaCls.Text = Utility.sDbnull(dt_Patient.Rows[0]["ketqua_cls"], "");
                            objPhieuchuyenvien = new Select().From(KcbPhieuchuyenvien.Schema)
                               .Where(KcbPhieuchuyenvien.Columns.IdBenhnhan).IsEqualTo(txtIdBn.Text)
                               .And(KcbPhieuchuyenvien.Columns.MaLuotkham).IsEqualTo(txtMaluotkham.Text)
                               .ExecuteSingle<KcbPhieuchuyenvien>();
                            if (objPhieuchuyenvien != null)
                            {
                                txtId.Text = objPhieuchuyenvien.IdPhieu.ToString();
                                txtsochuyenvien.Text = Utility.sDbnull(objPhieuchuyenvien.SoChuyentuyen);
                                txtNoichuyenden.SetId(objPhieuchuyenvien.IdBenhvienChuyenden);
                                txtdauhieucls._Text = objPhieuchuyenvien.DauhieuCls;
                                txtketquaCls.Text = objPhieuchuyenvien.KetquaXnCls;
                                txtChandoan.Text = Utility.sDbnull(objPhieuchuyenvien.ChanDoan,"");
                                txtMaBenhChinh.Text = Utility.sDbnull(objPhieuchuyenvien.Mabenh,"");
                                txtThuocsudung.Text = objPhieuchuyenvien.ThuocSudung;
                                txtTrangthainguoibenh._Text = objPhieuchuyenvien.TrangthaiBenhnhan;
                                txtHuongdieutri._Text = objPhieuchuyenvien.HuongDieutri;
                                txtphuongtienvc._Text = objPhieuchuyenvien.PhuongtienChuyen;
                                txtNguoivanchuyen.Text = objPhieuchuyenvien.TenNguoichuyen;
                                cboDoctorAssign.SelectedIndex = Utility.GetSelectedIndex(cboDoctorAssign,Utility.sDbnull( objPhieuchuyenvien.IdBacsiChuyenvien,"-1"));
                                cmdPrint.Enabled = true;
                                cmdHuy.Enabled = true;
                            }
                            else
                            {
                                txtsochuyenvien.Text = Utility.sDbnull(GetmaxSoChuyenVien());
                                cmdPrint.Enabled = false;
                                cmdHuy.Enabled = false;
                            }
                            m_enAct = objPhieuchuyenvien == null ? action.Insert : action.Update;
                            if (m_enAct == action.Insert)
                                cmdPrint.Enabled = false;
                            else
                                cmdPrint.Enabled = true;
                            dtNgaychuyenvien.Focus();
                        }
                   
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi: " + ex.Message);
            }
            finally
            {
                
                AllowTextChanged = true;
            }
        }
        private void AutocompleteBenhvien()
        {
            DataTable m_dtBenhvien = new Select().From(DmucBenhvien.Schema).OrderAsc(DmucBenhvien.Columns.SttHthi).ExecuteDataSet().Tables[0];
            try
            {
                if (m_dtBenhvien == null) return;
                if (!m_dtBenhvien.Columns.Contains("ShortCut"))
                    m_dtBenhvien.Columns.Add(new DataColumn("ShortCut", typeof(string)));
                foreach (DataRow dr in m_dtBenhvien.Rows)
                {
                    string shortcut = "";
                    string realName = dr[DmucBenhvien.Columns.TenBenhvien].ToString().Trim() + " " +
                                      Utility.Bodau(dr[DmucBenhvien.Columns.TenBenhvien].ToString().Trim());
                    shortcut = dr[DmucBenhvien.Columns.MaBenhvien].ToString().Trim();
                    string[] arrWords = realName.ToLower().Split(' ');
                    string _space = "";
                    string _Nospace = "";
                    foreach (string word in arrWords)
                    {
                        if (word.Trim() != "")
                        {
                            _space += word + " ";
                            //_Nospace += word;
                        }
                    }
                    shortcut += _space; // +_Nospace;
                    foreach (string word in arrWords)
                    {
                        if (word.Trim() != "")
                            shortcut += word.Substring(0, 1);
                    }
                    dr["ShortCut"] = shortcut;
                }
                var source = new List<string>();
                var query = from p in m_dtBenhvien.AsEnumerable()
                            select p[DmucBenhvien.Columns.IdBenhvien].ToString() + "#" + p[DmucBenhvien.Columns.MaBenhvien].ToString() + "@" + p[DmucBenhvien.Columns.TenBenhvien].ToString() + "@" + p["shortcut"].ToString();
                source = query.ToList();
                this.txtNoichuyenden.AutoCompleteList = source;
                this.txtNoichuyenden.TextAlign = HorizontalAlignment.Left;
                this.txtNoichuyenden.CaseSensitive = false;
                this.txtNoichuyenden.MinTypedCharacters = 1;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
              

            }
        }
        private void ClearControls()
        {
            try
            {
                cmdPrint.Enabled = false;
                cmdHuy.Enabled = false;

                foreach (Control control in pnlTop.Controls)
                {
                    if (control is EditBox)
                    {
                        ((EditBox)(control)).Clear();
                    }
                    else if (control is MaskedEditBox)
                    {
                        control.Text = "";
                    }
                    else if (control is VNS.HIS.UCs.AutoCompleteTextbox)
                    {
                        ((VNS.HIS.UCs.AutoCompleteTextbox)control)._Text = "";
                    }
                    else if (control is TextBox)
                    {
                        ((TextBox)(control)).Clear();
                    }
                }
                foreach (Control control in pnlFill.Controls)
                {
                    if (control is EditBox)
                    {
                        ((EditBox)(control)).Clear();
                    }
                    else if (control is MaskedEditBox)
                    {
                        control.Text = "";
                    }
                    else if (control is VNS.HIS.UCs.AutoCompleteTextbox)
                    {
                        ((VNS.HIS.UCs.AutoCompleteTextbox)control)._Text = "";
                    }
                    else if (control is TextBox)
                    {
                        ((TextBox)(control)).Clear();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void cmdSearchBenhChinh_Click(object sender, EventArgs e)
        {
            ShowDiseaseList();
        }
        /// <summary>
        /// hàm thực hiện hsow thông tin cua bệnh
        /// </summary>
        private void ShowDiseaseList()
        {
            try
            {
                var frm = new frm_QuickSearchDiseases();
                frm.DiseaseType_ID = -1;
                frm.ShowDialog();
                if (frm.m_blnCancel)
                {
                    txtMaBenhChinh.Text = frm.v_DiseasesCode;
                    txtMaBenhChinh.Focus();
                    txtMaBenhChinh.SelectAll();
                }
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
            }
        }

        private void cmdPrint_Click_1(object sender, EventArgs e)
        {

        }
    }
}
