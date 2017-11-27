using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using VNS.HIS.BusRule.Classes;
using VNS.HIS.Classes;
using VNS.HIS.DAL;
using VMS.HIS.KN.Classess;
using VMS.HIS.KN.CanLamSang;
using VNS.Libs;

namespace VMS.HIS.KN.QuanlyMauKiemnghiem
{
    public partial class FrmQuanlyKetquaMaukiemnghiem : Form
    {
        readonly KCB_CHIDINH_CANLAMSANG _kcbChidinhCanlamsang = new KCB_CHIDINH_CANLAMSANG();
        private DataTable _mDtDichvuKn=new DataTable();
        public FrmQuanlyKetquaMaukiemnghiem()
        {
            InitializeComponent();
            //this.InitTrace();
            this.KeyPreview = true;
            dtmFrom.Value = globalVariables.SysDate;
            dtmTo.Value = globalVariables.SysDate;
            
            InitEvents();
        }
        void InitEvents()
        {
            this.txtPatientCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatientCode_KeyDown);
            this.txtPatient_ID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatient_ID_KeyDown);
            this.cmdTimKiem.Click += new System.EventHandler(this.cmdTimKiem_Click);

            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);

            this.chkByDate.CheckedChanged += new System.EventHandler(this.chkByDate_CheckedChanged);

            this.Load += new System.EventHandler(this.frm_Quanly_Ketqua_Maukiemnghiem_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_Quanly_Ketqua_Maukiemnghiem_KeyDown);
            grdDetail.SelectionChanged += grdDetail_SelectionChanged;
            cmdNhanmau.Click += cmdNhanmau_Click;
            cmdNhapKQ.Click += cmdNhapKQ_Click;
            cmdXacnhanKQ.Click += cmdXacnhanKQ_Click;
        }
        void cmdXacnhanKQ_Click(object sender, EventArgs e)
        {
            XacnhanKq(cmdXacnhanKQ.Tag.ToString() == "0");
        }
        void XacnhanKq(bool xacnhan)
        {
            bool _isValid = Utility.isValidGrid(grdDetail);
            bool _autoChecked = false;
            try
            {
                if (grdDetail.GetCheckedRows().Count() <= 0)
                {
                    if (!_isValid)
                    {
                        Utility.ShowMsg("Bạn cần chọn ít nhất một mẫu kiểm nghiệm để xác nhận/hủy xác nhận kết quả");
                        return;
                    }
                    else
                    {
                        _autoChecked = true;
                        grdDetail.CurrentRow.BeginEdit();
                        grdDetail.CurrentRow.IsChecked = true;
                        grdDetail.CurrentRow.EndEdit();
                    }
                }
                List<string> lstIdchidinhchitiet = (from chidinh in grdDetail.GetCheckedRows().AsEnumerable()
                                                    let x = Utility.sDbnull(chidinh.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value)
                                                    select x).ToList<string>();
                string _IdChitietchidinh = string.Join(",", lstIdchidinhchitiet.ToArray());
                string Question = string.Format("Bạn có muốn xác nhận kết quả các mẫu đang chọn hay không?\nChú ý: Chỉ các mẫu có kết quả và chưa xác nhận mới được xác nhận");
                byte trangthaicu = 3;
                byte trangthaimoi = 4;
                if (!xacnhan)
                {
                    trangthaicu = 4;
                    trangthaimoi = 3;
                    Question = string.Format("Bạn có muốn hủy xác nhận kết quả các mẫu đang chọn hay không?\nChú ý: Chỉ các mẫu có kết quả và chưa in mới được hủy xác nhận");
                }
                if (Utility.AcceptQuestion(Question, "Thông báo", true))
                {
                    cmdXacnhanKQ.Text = xacnhan ? "Hủy xác nhận kết quả" : "Xác nhận kết quả";
                    cmdXacnhanKQ.Tag = xacnhan ? 1 : 0;
                    _kcbChidinhCanlamsang.MaukiemnghiemCapnhattrangthai(_IdChitietchidinh, trangthaicu, trangthaimoi);
                    _mDtDichvuKn.AsEnumerable()
                                    .Where(c => Utility.ByteDbnull(c[KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan], 0) == 1 && Utility.sDbnull(c["trangthai_chuyencls"], "") == trangthaicu.ToString() && lstIdchidinhchitiet.Contains(Utility.sDbnull(c.Field<long>(KcbChidinhclsChitiet.Columns.IdChitietchidinh))))
                                    .ToList<DataRow>()
                                    .ForEach(c1 => { c1["trangthai_chuyencls"] = trangthaimoi; c1["ten_trangthai_chuyencls"] = trangthaimoi == 4 ? "Đã xác nhận kết quả" : "Đã có kết quả"; });
                    _mDtDichvuKn.AcceptChanges();
                    ModifyCommand();
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            if (_autoChecked)
            {
                grdDetail.CurrentRow.BeginEdit();
                grdDetail.CurrentRow.IsChecked = false;
                grdDetail.CurrentRow.EndEdit();
            }
        }
        void cmdNhapKQ_Click(object sender, EventArgs e)
        {
            frm_nhapketquaKN _nhapketquaKN = new frm_nhapketquaKN();
            long idchidinh = Utility.Int64Dbnull( Utility.GetValueFromGridColumn(grdDetail, "id_chidinh"));
            _nhapketquaKN._OnResult += _nhapketquaKN__OnResult;
            _nhapketquaKN.AutoSearch(idchidinh);
            _nhapketquaKN.ShowDialog(); 
        }
        void _nhapketquaKN__OnResult(long id_chitiet, byte tthai_cls)
        {
            try
            {
                 _mDtDichvuKn.AsEnumerable()
                                    .Where(c =>Utility.Int64Dbnull(c.Field<long>(KcbChidinhclsChitiet.Columns.IdChitietchidinh))==id_chitiet)
                                    .ToList<DataRow>()
                                    .ForEach(c1 => { c1["trangthai_chuyencls"] = tthai_cls; c1["ten_trangthai_chuyencls"] = (tthai_cls == 4 ? "Đã xác nhận kết quả" : (tthai_cls == 2?"Đang nhập kết quả":"Đã có kết quả")); });
                    _mDtDichvuKn.AcceptChanges();
            }
            catch (Exception ex)
            {
                
                
            }
        }
        void Nhanmau(bool nhanmau)
        {
             bool isValid = Utility.isValidGrid(grdDetail);
                bool autoChecked = false;
            try
            {
                if (!grdDetail.GetCheckedRows().Any())
                {
                    if (!isValid)
                    {
                        Utility.ShowMsg("Bạn cần chọn ít nhất một mẫu kiểm nghiệm để bàn giao");
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
                string question = string.Format("Bạn có muốn nhận các mẫu đang chọn hay không?");
                byte trangthaicu = 1;
                byte trangthaimoi = 2;
                if (!nhanmau)
                {
                    trangthaicu = 2;
                    trangthaimoi = 1;
                    question = string.Format("Bạn có muốn hủy nhận các mẫu đang chọn hay không?");
                }
                if (Utility.AcceptQuestion(question, "Thông báo", true))
                {
                    cmdNhanmau.Text = nhanmau ? "Hủy nhận mẫu" : "Nhận mẫu";
                    cmdNhanmau.Tag = nhanmau ? 1 : 0;
                    _kcbChidinhCanlamsang.MaukiemnghiemCapnhattrangthai(idChitietchidinh, trangthaicu, trangthaimoi);
                    _mDtDichvuKn.AsEnumerable()
                        .Where(
                            c =>
                                Utility.ByteDbnull(c[KnChidinhChitiet.Columns.TrangthaiThanhtoan], 0) == 1 &&
                                Utility.sDbnull(c["trangthai_chuyencls"], "") == trangthaicu.ToString() &&
                                lstIdchidinhchitiet.Contains(
                                    Utility.sDbnull(c.Field<long>(KcbChidinhclsChitiet.Columns.IdChitietchidinh))))
                        .ToList()
                        .ForEach(c1 =>
                        {
                            c1["trangthai_chuyencls"] = trangthaimoi;
                            c1["ten_trangthai_chuyencls"] = trangthaimoi == 1 ? "Đã bàn giao" : "Đang nhập kết quả";
                        });
                    _mDtDichvuKn.AcceptChanges();
                    ModifyCommand();
                }
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
        void cmdNhanmau_Click(object sender, EventArgs e)
        {
            Nhanmau(cmdNhanmau.Tag.ToString() == "0");
        }
        void grdDetail_SelectionChanged(object sender, EventArgs e)
        {
            ModifyCommand();
        }
        void cmdPrintAssign_Click(object sender, EventArgs e)
        {
            try
            {
                string  mayin="";
                int v_AssignId = Utility.Int32Dbnull(grdDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), -1);
                string v_AssignCode = Utility.sDbnull(grdDetail.GetValue(KcbChidinhcl.Columns.MaChidinh), -1);
                string nhomincls = "ALL";

                KcbInphieu.InphieuDangkyKiemnghiem(v_AssignId);
            }
            catch(Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtmTo.Enabled = dtmFrom.Enabled = chkByDate.Checked;
        }
        void TimKiemThongTin(bool theongay)
        {
            try
            {
                byte trangThai = 10;
                if (optChuacoKQ.Checked) trangThai = 1;
                if (optDanhapKQ.Checked) trangThai = 2;
                if (optDaXacnhanKQ.Checked) trangThai = 4;
                _mDtDichvuKn =
                    _kcbChidinhCanlamsang.MaukiemnghiemLayChitietDangkyKiemnghiem(
                        theongay
                            ? (chkByDate.Checked ? dtmFrom.Value.ToString("dd/MM/yyyy") : "01/01/1900")
                            : "01/01/1900",
                        theongay
                            ? (chkByDate.Checked ? dtmTo.Value.ToString("dd/MM/yyyy") : "01/01/1900")
                            : "01/01/1900",
                        Utility.sDbnull(txtPatientName.Text),
                        Utility.Int32Dbnull(txtPatient_ID.Text, -1),
                        Utility.sDbnull(txtPatientCode.Text), Utility.Int32Dbnull(txtDichvuKn.MyID, -1),
                        Utility.Int32Dbnull(txtMauKn.MyID, -1), trangThai);
                Utility.SetDataSourceForDataGridEx(grdDetail, _mDtDichvuKn, true, true, "1=1","");
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Loi:"+ exception.Message);
            }
            finally
            {
                ModifyCommand();
            }
        }
        private void cmdTimKiem_Click(object sender, EventArgs e)
        {
            TimKiemThongTin(true);
        }
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frm_Quanly_Ketqua_Maukiemnghiem_Load(object sender, EventArgs e)
        {
            ModifyCommand();
            TimKiemThongTin(true);
        }
        private void ModifyCommand()
        {
            bool isValid = Utility.isValidGrid(grdDetail);
            int trangThai = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdDetail, "trangthai_chuyencls"), 0);
            cmdNhapKQ.Enabled = isValid && (trangThai == 2 || trangThai == 3);
            cmdXacnhanKQ.Enabled = isValid && (trangThai == 3 || trangThai == 4);
            cmdXacnhanKQ.Text = trangThai == 3 ? "Xác nhận kết quả" : "Hủy xác nhận kết quả";
            cmdXacnhanKQ.Tag = trangThai == 3 ? 0 : 1;
            cmdNhanmau.Enabled = isValid && (trangThai == 1 || trangThai == 2);
            cmdNhanmau.Text = trangThai == 1 ? "Nhận mẫu" : "Hủy nhận mẫu";
            cmdNhanmau.Tag = trangThai == 1 ? 0 : 1;
        }
        private void frm_Quanly_Ketqua_Maukiemnghiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11) Utility.ShowMsg(this.ActiveControl.Name);      
            if (e.KeyCode == Keys.F3) cmdTimKiem.PerformClick();
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
           
            if (e.KeyCode == Keys.N && e.Control) cmdNhapKQ.PerformClick();
            if (e.KeyCode == Keys.X && e.Control) cmdXacnhanKQ.PerformClick();
        }
        
        private void txtPatientCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                errorProvider1.Clear();
                if (e.KeyCode == Keys.Enter && Utility.DoTrim(txtPatientCode.Text.Trim()) != "") 
                {
                    string _ID = txtPatient_ID.Text.Trim();
                    string _Code ="KN"+ Utility.GetYY(DateTime.Now) + Utility.FormatNumberToString(Utility.Int32Dbnull(txtPatientCode.Text, 0), "000000");
                    txtPatient_ID.Clear();
                    txtPatientCode.Text = _Code;
                    TimKiemThongTin(false);
                    if (grdDetail.RowCount == 1) grdDetail_SelectionChanged(grdDetail, new EventArgs());
                    txtPatient_ID.Text = _ID;
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy thông tin khách hàng");
            }
        }

        private void txtPatient_ID_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                errorProvider1.Clear();
                var dtPatient = new DataTable();
                if (e.KeyCode == Keys.Enter && Utility.DoTrim(txtPatient_ID.Text.Trim())!="") 
                {
                    string _code = txtPatientCode.Text.Trim();
                    txtPatientCode.Clear();
                    TimKiemThongTin(false);
                    if ( grdDetail.RowCount == 1) grdDetail_SelectionChanged(grdDetail, new EventArgs());
                    txtPatientCode.Text = _code;
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy thông tin khách hàng");
            }
        }

    }
}
