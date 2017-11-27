using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using CIS.CoreApp;
using SubSonic;
using VNS.Core.Classes;
using VNS.HIS.DAL;
using VNS.Libs;
using VNS.Libs.AppLogger;
using VNS.Libs.AppType;
using VNS.Libs.AppUI;
using VNS.Properties;
using xvhrk;

namespace VNSCore
{
    public partial class FrmLogin : Form
    {
        public bool BCancel = true;
        public bool BlnRelogin = false;
        private string _oldUid = "";

        public FrmLogin()
        {
            InitializeComponent();
            cmdSettings.Click += cmdSettings_Click;
            txtUserName.LostFocus += txtUserName_LostFocus;
            cbogiaodien.SelectedIndex = globalVariables.sMenuStyle == "MENU" ? 0 : 1;
        }

        private void cmdSettings_Click(object sender, EventArgs e)
        {
            //oldVal = PropertyLib._ConfigProperties.RunUnderWS;
            var properties = new frm_Properties(PropertyLib._ConfigProperties) {TopMost = true};
            properties.ShowDialog();
            //if (oldVal != PropertyLib._ConfigProperties.RunUnderWS)
            //{
            if (PropertyLib._ConfigProperties.RunUnderWS)
            {
                string dataBaseServer = "";
                string dataBaseName = "";
                string uid = "";
                string pwd = "";
                WS._AdminWS.GetConnectionString(ref dataBaseServer, ref dataBaseName, ref uid, ref pwd);
                PropertyLib._ConfigProperties.DataBaseServer = dataBaseServer;
                PropertyLib._ConfigProperties.DataBaseName = dataBaseName;
                PropertyLib._ConfigProperties.UID = uid;
                PropertyLib._ConfigProperties.PWD = pwd;
                globalVariables.ServerName = PropertyLib._ConfigProperties.DataBaseServer;
                globalVariables.sUName = PropertyLib._ConfigProperties.UID;
                globalVariables.sPwd = PropertyLib._ConfigProperties.PWD;
                globalVariables.sDbName = PropertyLib._ConfigProperties.DataBaseName;
            }
            else
            {
                globalVariables.ServerName = PropertyLib._ConfigProperties.DataBaseServer;
                globalVariables.sUName = PropertyLib._ConfigProperties.UID;
                globalVariables.sPwd = PropertyLib._ConfigProperties.PWD;
                globalVariables.sDbName = PropertyLib._ConfigProperties.DataBaseName;
                globalVariables.sMenuStyle = "DOCKING";

                globalVariables.MA_KHOA_THIEN = PropertyLib._ConfigProperties.MaKhoa;
                globalVariables.MA_PHONG_THIEN = PropertyLib._ConfigProperties.Maphong;
                globalVariables.SOMAYLE = PropertyLib._ConfigProperties.Somayle;
                globalVariables.MIN_STT = PropertyLib._ConfigProperties.Min;
                globalVariables.MAX_STT = PropertyLib._ConfigProperties.Max;
            }
            Utility.InitSubSonic(new ConnectionSQL().KhoiTaoKetNoi(), "ORM");
            //}
        }
        /// <summary>
        /// mã hóa
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <param name="useHashing"></param>
        /// <returns></returns>
        public string Encrypt(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);
            if (useHashing)
            {
                var hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes("hoidapit.com.vn"));
            }
            else keyArray = Encoding.UTF8.GetBytes("hoidapit.com.vn");
            var tdes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        /// <summary>
        /// Giải mã 
        /// </summary>
        /// <param name="toDecrypt"></param>
        /// <param name="useHashing"></param>
        /// <returns></returns>
        public string Decrypt(string toDecrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);
            if (useHashing)
            {
                var hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes("hoidapit.com.vn"));
            }
            else keyArray = Encoding.UTF8.GetBytes("hoidapit.com.vn");
            var tdes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Encoding.UTF8.GetString(resultArray);
        } 

        /// <summary>
        ///     hàm thực hiện việc đăng nhập thông tin của khi đăng nhập Login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Login_Load(object sender, EventArgs e)
        {
            try
            {
                globalVariables.StringLicense = GetRequestLicenseCode() + THU_VIEN_CHUNG.GetMACAddress();
                DataTable dtKhoaphong = THU_VIEN_CHUNG.Laydanhmuckhoa("ALL", 0);
                DataBinding.BindDataCombobox(cboKhoaKCB, dtKhoaphong, DmucKhoaphong.Columns.MaKhoaphong,
                    DmucKhoaphong.Columns.TenKhoaphong, "---Khoa làm việc---", false);
                PropertyLib._AppProperties = PropertyLib.GetAppPropertiess();
                PropertyLib._ConfigProperties = PropertyLib.GetConfigProperties();
                cbogiaodien.SelectedIndex = PropertyLib._AppProperties.MenuStype;
                txtUserName.Text = PropertyLib._AppProperties.UID;
                _oldUid = txtUserName.Text;
                chkRemember.Checked = PropertyLib._AppProperties.REM;
                lblMsg.Text = "";
                cboKhoaKCB.SelectedIndex = Utility.GetSelectedIndex(cboKhoaKCB, PropertyLib._ConfigProperties.MaKhoa);
                if (cboKhoaKCB.SelectedIndex <= 0)
                    cboKhoaKCB.SelectedIndex = Utility.GetSelectedIndex(cboKhoaKCB, PropertyLib._AppProperties.Makhoathien);
                if (PropertyLib._AppProperties.AutoLogin)
                {
                    txtPassWord.Text = PropertyLib._AppProperties.PWD;
                    cmdLogin_Click(cmdLogin, e);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex);
            }
        }
        public static string ConvertStringToSecureCode(string input1)
        {
            MD5 secu1 = MD5.Create();
            byte[] data1 = secu1.ComputeHash(Encoding.Default.GetBytes(input1));
            var sbd = new StringBuilder();
            for (int i = 0; i <= data1.Length - 1; i++)
            {
                sbd.Append(data1[i].ToString("x2"));
            }
            return sbd.ToString();
        }
        public static string GetRequestLicenseCode()
        {
            string hd1 = HardDiskSeriesNumber();
            string code1 = ConvertStringToSecureCode(hd1);
            string code2 = code1.Substring(24).ToUpper();
            // string s5 = FormatLicenseCode(Code2);
            return code2;
        }
        private static string HardDiskSeriesNumber()
        {
            string output = ExecuteCommandSync("vol");
            string aa = output.Split('.')[output.Split('.').Length - 1];
            string bb = aa.Split(' ')[aa.Split(' ').Length - 1];
            return bb.ToUpper();
        }
        public static string ExecuteCommandSync(object command)
        {
            try
            {
                // create the ProcessStartInfo using "cmd" as the program to be run,
                // and "/c " as the parameters.
                // Incidentally, /c tells cmd that we want it to execute the command that follows,
                // and then exit.
                var procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);

                // The following commands are needed to redirect the standard output.
                // This means that it will be redirected to the Process.StandardOutput StreamReader.
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                // Do not create the black window.
                procStartInfo.CreateNoWindow = true;
                // Now we create a process, assign its ProcessStartInfo and start it
                var proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                // Get the output into a string
                string result = proc.StandardOutput.ReadToEnd();
                // Display the command output.
                return result;
            }
            catch (Exception)
            {
                // Log the exception
                return null;
            }
        }

        /// <summary>
        ///     hàm thực hienj việc lưu lại thông itn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void txtUserName_LostFocus(object sender, EventArgs eventArgs)
        {
            try
            {
                if (_oldUid != Utility.sDbnull(txtUserName.Text))
                {
                    _oldUid = Utility.sDbnull(txtUserName.Text);
                    globalVariables.UserName = _oldUid;
                    bool isAdmin = new LoginService().isAdmin(Utility.sDbnull(_oldUid));
                    DataBinding.BindDataCombobox(cboKhoaKCB,
                        THU_VIEN_CHUNG.LaydanhsachKhoaKhidangnhap(globalVariables.UserName, Utility.Bool2byte(isAdmin)),
                        DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong,
                        "---Khoa làm việc---", false);
                    cboKhoaKCB.SelectedIndex = Utility.GetSelectedIndex(cboKhoaKCB,
                        PropertyLib._AppProperties.Makhoathien);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex);
            }
        }

        /// <summary>
        ///     hàm thực hienj viecj đang nhập
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
        }

        /// <summary>
        ///     hàm thực hiện việc đóng for
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            try
            {
                BCancel = true;
                if (!BlnRelogin)
                {
                    if (Utility.AcceptQuestion("Bạn có thực sự muốn thoát khỏi chương trình?", "Xác nhận", true))
                    {
                        Application.Exit();
                    }
                }
                //else
                //    this.Close();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex);
            }
        }

        /// <summary>
        ///     hàm thực hiện viecj đang nhập thông tin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Application.DoEvents();
                Utility.WaitNow(this);
                cmdLogin.Enabled = false;
                if (!IsValid())
                {
                    cmdLogin.Enabled = true;
                    Utility.DefaultNow(this);
                    return;
                }
                if (chkRemember.Checked)
                {
                    PropertyLib._AppProperties.UID = Utility.sDbnull(txtUserName.Text);
                    PropertyLib._AppProperties.REM = true;
                    PropertyLib._AppProperties.MenuStype = cbogiaodien.SelectedIndex;
                    PropertyLib.SaveProperty(PropertyLib._AppProperties);
                }

                PropertyLib._AppProperties.PWD = Utility.sDbnull(txtPassWord.Text);
                Close();
                Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
            finally
            {
                cmdLogin.Enabled = true;
                Utility.DefaultNow(this);
            }
        }

        /// <summary>
        ///     hàm thực hiện việc đăng nhập thông tin  kiểm tra
        ///     quyền hợp lệ
        /// </summary>
        /// <returns></returns>
        private bool IsValid()
        {
            UIAction.SetTextStatus(lblMsg, "", false);

            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                UIAction.SetTextStatus(lblMsg, "Bạn phải nhập tên đăng nhập", true);
                cmdLogin.Enabled = true;
                UIAction.FocusEditbox(txtUserName);
                return false;
            }
            if (cboKhoaKCB.Items.Count == 0 || cboKhoaKCB.SelectedValue == null ||
                cboKhoaKCB.SelectedValue.ToString() == "-1" || cboKhoaKCB.SelectedIndex < 0)
            {
                UIAction.SetTextStatus(lblMsg, "Bạn cần chọn khoa làm việc", true);
                cboKhoaKCB.Focus();
                return false;
            }
            PropertyLib._AppProperties.Makhoathien = Utility.sDbnull(cboKhoaKCB.SelectedValue, "KKB");
            globalVariables.MA_KHOA_THIEN = PropertyLib._AppProperties.Makhoathien;
            string userName = Utility.sDbnull(Utility.GetPropertyValue(txtUserName, "Text"));
            string password = Utility.sDbnull(Utility.GetPropertyValue(txtPassWord, "Text"));
            BCancel = true;
            globalVariables.LoginSuceess = new LoginService().isAdmin(Utility.sDbnull(userName),
                Utility.sDbnull(password));
            if (globalVariables.LoginSuceess) goto _Admin;
            globalVariables.LoginSuceess = new LoginService().KiemTraUserName(Utility.sDbnull(userName));
            if (!globalVariables.LoginSuceess)
            {
                UIAction.SetTextStatus(lblMsg, "Sai tên đăng nhập. Mời bạn nhập lại", true);
                globalVariables._NumberofBrlink = 0;
                UIAction.FocusEditbox(txtUserName);
                return false;
            }
            globalVariables.LoginSuceess = new LoginService().KiemTraPassword(Utility.sDbnull(userName),
                Utility.sDbnull(password));
            if (!globalVariables.LoginSuceess)
            {
                UIAction.SetTextStatus(lblMsg, "Sai mật khẩu đăng nhập. Mời bạn nhập lại mật khẩu", true);
                globalVariables._NumberofBrlink = 0;
                UIAction.FocusEditbox(txtPassWord);
                return false;
            }
            globalVariables.LoginSuceess = new LoginService().DangNhap(Utility.sDbnull(userName),
                Utility.sDbnull(password.Trim()));
            if (!globalVariables.LoginSuceess)
            {
                Utility.ShowMsg("Thông tin đăng nhập không đúng, Mời bạn nhập lại User hoặc Password", "Thông báo",
                    MessageBoxIcon.Warning);
                globalVariables._NumberofBrlink = 0;
                UIAction.FocusEditbox(txtUserName);
                return false;
            }

            _Admin:
            BCancel = false;
            globalVariables.sMenuStyle = cbogiaodien.SelectedIndex == 0 ? "MENU" : "DOCKING";
            if (!BlnRelogin && PropertyLib._ConfigProperties.HIS_AppMode != AppEnum.AppMode.Demo)
            {
                UIAction.SetTextStatus(lblMsg, "Đang kiểm tra giấy phép sử dụng phần mềm...", false);
                if (PropertyLib._ConfigProperties.RunUnderWS)
                {
                    if (!WS._AdminWS.IsValidLicense())
                    {
                        globalVariables.LoginSuceess = false;
                        UIAction.SetTextStatus(lblMsg,
                            "Phần mềm chưa đăng ký license. Đề nghị liên hệ nhà sản xuất phần mềm để được trợ giúp: 0977994312 (Mr.Ánh)",
                            true);
                        return false;
                    }
                }
                else if (!IsValidSoftKey())
                {
                    globalVariables.LoginSuceess = false;
                    Utility.ShowMsg(
                        "Phần mềm chưa đăng ký license. Đề nghị liên hệ nhà sản xuất phần mềm để được trợ giúp:  0977994312 (Mr.Ánh)");
                    UIAction.SetTextStatus(lblMsg,
                        "Phần mềm chưa đăng ký license. Đề nghị liên hệ nhà sản xuất phần mềm để được trợ giúp:  0977994312 (Mr.Ánh)",
                        true);
                    return false;
                }
            }
            LoadDataForm();
            return true;
        }

        private bool IsValidSoftKey()
        {
            try
            {
                if (globalVariables.IsValidLicense) return true;
                string sRegKey = getRegKeyBasedOnSCPLicense();
                var appKey = new MHardKey("XFW", 5, false);
                string giaima = Decrypt(sRegKey, false );

                globalVariables.IsValidLicense = sRegKey == appKey.RegKey;
                if (!globalVariables.IsValidLicense)
                {
                    LogAction.LogSCPService(
                        string.Format(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") +
                                      "-->Kiểm tra khóa mềm không hợp lệ."));
                    return false;
                }
                LogAction.LogSCPService(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "-->Kiểm tra khóa mềm hợp lệ...");
                return true;
            }
            catch (Exception ex)
            {
                LogAction.LogSCPService(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "-->Lỗi khi kiểm tra khóa mềm-->" +
                                        ex.Message);
                return false;
            }
        }

        private string getRegKeyBasedOnSCPLicense()
        {
            try
            {
                string fileName = Application.StartupPath + @"\license.lic";
                if (!File.Exists(fileName)) return "";
                using (var streamR = new StreamReader(fileName))
                {
                    return streamR.ReadLine() ;
                }
            }
            catch
            {
                return "";
            }
        }

        private void LoadDataForm()
        {
            Application.DoEvents();
            LoadList();
            Application.DoEvents();
        }

        private void Try2SaveXML()
        {
                var ds = new DataSet();
                ds.ReadXml(Application.StartupPath + @"\Config.xml");
                ds.Tables[0].Rows[0]["INTERFACEDISPLAY"] = globalVariables.sMenuStyle;
                ds.WriteXml(Application.StartupPath + @"\Config.xml", XmlWriteMode.IgnoreSchema);
        }

        public void LoadList()
        {
            try
            {
                Try2SaveXML();
                UIAction.SetTextStatus(lblMsg, "Nạp thông tin cấu hình...", false);
                Utility.LoadProperties();
                UIAction.SetTextStatus(lblMsg, "Nạp dữ liệu danh mục thuốc...", false);
                globalVariables.gv_dtDangbaoche = SPs.DmucLaydmucDangbaochethuoc().GetDataSet().Tables[0];
                globalVariables.gv_dtDanhMucThuoc = new Select().From(VDmucThuoc.Schema).ExecuteDataSet().Tables[0];
                globalVariables.gv_dtLoaiThuoc = new Select().From(DmucLoaithuoc.Schema).ExecuteDataSet().Tables[0];
                globalVariables.gv_dtDmucChung = new Select().From(DmucChung.Schema).ExecuteDataSet().Tables[0];
                globalVariables.gv_dtQheDoituongThuoc =
                new Select().From(QheDoituongThuoc.Schema).ExecuteDataSet().Tables[0];
                UIAction.SetTextStatus(lblMsg, "Nạp dữ liệu danh mục bệnh...", false);
                globalVariables.gv_dtDmucLoaibenh = THU_VIEN_CHUNG.LayDulieuDanhmucChung(new List<string> {"LOAIBENH"},
                    false);
                globalVariables.gv_dtDmucBenh = new Select().From(VDanhmucbenh.Schema).ExecuteDataSet().Tables[0];
                UIAction.SetTextStatus(lblMsg, "Nạp dữ liệu danh mục địa chính...", false);
                globalVariables.gv_dtDmucDiachinh = new Select().From(VDmucDiachinh.Schema).ExecuteDataSet().Tables[0];
                globalVariables.gv_dtDmucBenhVien = new Select().From(DmucBenhvien.Schema).ExecuteDataSet().Tables[0];
                Utility.AutoCompeleteAddress(globalVariables.gv_dtDmucDiachinh);
                UIAction.SetTextStatus(lblMsg, "Nạp dữ liệu danh mục nơi KCBBĐ...", false);
                globalVariables.gv_dtDmucNoiKCBBD = new Select().From(VDmucNoiKCBBD.Schema).ExecuteDataSet().Tables[0];
                UIAction.SetTextStatus(lblMsg, "Nạp dữ liệu dịch vụ CLS...", false);
                globalVariables.gv_dtDmucQheCamCLSChungPhieu =
                    new Select().From(QheCamchidinhChungphieu.Schema).Where(QheCamchidinhChungphieu.Columns.Loai).
                        IsEqualTo(0).ExecuteDataSet().Tables[0];
                globalVariables.gv_dtDmucDichvuCls = new Select().From(VDmucDichvucl.Schema).ExecuteDataSet().Tables[0];
                globalVariables.gv_dtDmucDichvuClsChitiet =
                    new Select().From(VDmucDichvuclsChitiet.Schema).ExecuteDataSet().Tables[0];
                globalVariables.gv_dtNhomDichVuCLS =
                    new Select().From(DmucNhomcanlamsang.Schema).ExecuteDataSet().Tables[0];
                globalVariables.gv_dtQheDoituongDichvu =
                    new Select().From(QheDoituongDichvucl.Schema).ExecuteDataSet().Tables[0];
                UIAction.SetTextStatus(lblMsg, "Nạp dữ liệu hệ thống khác...", false);
                globalVariables.gv_dtSysparams = new Select().From(SysSystemParameter.Schema).ExecuteDataSet().Tables[0];
                globalVariables.gv_dtSysTieude = new Select().From(SysTieude.Schema).ExecuteDataSet().Tables[0];
                globalVariables.gv_dtNhomInCLS = (from p in globalVariables.gv_dtDmucChung.AsEnumerable()
                    where p.Field<string>(DmucChung.Columns.Loai) == "NHOM_INPHIEU_CLS"
                    select p).CopyToDataTable();
                globalVariables.IdKhoaNhanvien = (Int16) THU_VIEN_CHUNG.LayIDPhongbanTheoUser(globalVariables.UserName);
                globalVariables.gv_dtDoituong =
                    new Select().From(DmucDoituongkcb.Schema)
                        .OrderAsc(DmucDoituongkcb.Columns.SttHthi)
                        .ExecuteDataSet()
                        .Tables[0];
                globalVariables.idKhoatheoMay =
                    (Int16) THU_VIEN_CHUNG.LayIdPhongbanTheoMay(globalVariables.MA_KHOA_THIEN);
                globalVariablesPrivate.objKhoaphong = DmucKhoaphong.FetchByID(globalVariables.idKhoatheoMay);
                globalVariablesPrivate.objNhanvien =
                    new Select().From(DmucNhanvien.Schema)
                        .Where(DmucNhanvien.Columns.UserName)
                        .IsEqualTo(globalVariables.UserName)
                        .ExecuteSingle<DmucNhanvien>();
                if (globalVariablesPrivate.objNhanvien != null)
                    globalVariablesPrivate.objKhoaphongNhanvien =
                        DmucKhoaphong.FetchByID(globalVariablesPrivate.objNhanvien.IdKhoa);

                globalVariables.gv_dtKhoaPhongNgoaiTru =
                    SPs.DmucLaydanhsachCacphongkhamTheoBacsi(globalVariables.UserName, globalVariables.idKhoatheoMay,
                        Utility.Bool2byte(
                            globalVariables.IsAdmin), 0).GetDataSet().Tables[0];
                globalVariables.g_dtMeasureUnit = THU_VIEN_CHUNG.LayDulieuDanhmucChung(new List<string> {"DONVITINH"},
                    false);
                globalVariables.gv_dtDmucPhongban = new Select().From(DmucKhoaphong.Schema).ExecuteDataSet().Tables[0];
                globalVariables.gv_dtDantoc =
                    new Select().From(DmucChung.Schema)
                        .Where(DmucChung.Columns.Loai)
                        .IsEqualTo("DAN_TOC")
                        .ExecuteDataSet()
                        .Tables[0];
                globalVariables.SysDate = THU_VIEN_CHUNG.GetSysDateTime();

                globalVariables.gv_dtDmucNhanvien = new Select().From(VDmucNhanvien.Schema).ExecuteDataSet().Tables[0];
                Utility.LoadImageLogo();
            }

            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                THU_VIEN_CHUNG.GetIP4Address();
                THU_VIEN_CHUNG.GetMACAddress();
                THU_VIEN_CHUNG.LoadThamSoHeThong();
            }
        }

        /// <summary>
        ///     hàm thực hiện việc lưu lại thông tin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkRemember_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void txtPassWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) cmdLogin_Click(cmdLogin, new EventArgs());
        }

        private void lnkHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Utility.ShowMsg("Liên hệ với quản trị phần mềm : \n Phạm Ngọc Ánh - SĐT: 0977994312");
            const string facebook = "http://www.facebook.com/HIS.QLBV";
            Utility.OpenProcess(facebook);
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            cmdSettings.Visible = txtUserName.Text.Trim() == "ADMIN";
        }

        private void cmdSettings_Click_1(object sender, EventArgs e)
        {

        }
    }
}