﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Data.SqlClient;
using System.Threading;
using System.Xml.Serialization;
using VNS.Properties;

namespace UpdateVersion
{
    public partial class UpdateVersion : Form
    {
        string sDbnull(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return string.Empty;
            }
            else
            {
                return DoTrim(obj.ToString());
            }
            // Int32Dbnull()
        }
       string sDbnull(object obj, object DefaultVal)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return DefaultVal.ToString();
            }
            else
            {
                return DoTrim(obj.ToString());
            }
        }
       string DoTrim(string value)
        {
            return value.TrimStart().TrimEnd();
        }
       string AppName = Application.StartupPath + @"\Core.exe";
        public UpdateVersion()
        {
           
            InitializeComponent();
            //Tự động tắt phần mềm HIS
            this.Shown += new EventHandler(UpdateVersion_Shown);
            this.KeyDown += new KeyEventHandler(UpdateVersion_KeyDown);
            Try2ReadApp();
            KillProcess(AppName);
           
            ReadConfig();
        }

        void UpdateVersion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                OpenProcess(AppName);
                Try2ExitApp();
            }
        }

        void UpdateVersion_Shown(object sender, EventArgs e)
        {
            //if (!Debugger.IsAttached)
            //{
                Thread.Sleep(1);
                Application.DoEvents();
                StartUpdateVersion();
            //}
        }
        void Try2ReadApp()
        {
            try
            {
                string StartupFile = Application.StartupPath + @"\Startup.txt";
                if (File.Exists(StartupFile))
                    AppName = Application.StartupPath + @"\" + GetFirstValueFromFile(StartupFile);
            }
            catch
            {
                AppName = Application.StartupPath + @"\Core.exe";
            }
        }
        string GetFirstValueFromFile(string fileName)
        {
            try
            {
                if (!File.Exists(fileName)) return "";
                using (StreamReader _Reader = new StreamReader(fileName))
                {
                    object obj = _Reader.ReadLine();
                    if (obj == null) return "";
                    return obj.ToString().Trim();
                }
                return "";
            }
            catch
            {
                return "";
            }
        }
        void Try2ExitApp()
        {
            try
            {
                //return;
                this.Close();
                this.Dispose();
                Application.Exit();
            }
            catch
            {
            }
        }
        private void UpdateVersion_Load(object sender, EventArgs e)
        {
           
        }
        DataTable getVersion()
        {
            try
            {
                DataTable dt = new DataTable("SysVersions");
                using (SqlConnection conn = new SqlConnection(sqlConnectionString))
                {
                    conn.Open();
                    SqlCommand sqlcmd = new SqlCommand();
                    sqlcmd.Connection = conn;
                    sqlcmd.CommandText = "sys_getVersions";
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter sqlDa = new SqlDataAdapter(sqlcmd);
                    sqlDa.Fill(dt);
                    conn.Close();
                }
                return dt;
            }
            catch
            {
                return null;
            }
        }
        bool Syserr = false;
        void StartUpdateVersion()
        {
            try
            {
                Syserr = false;
                Cursor = Cursors.WaitCursor;

                //Lấy dữ liệu phiên bản
                DataTable dtData = getVersion();
                if (dtData == null || dtData.Rows.Count <= 0)
                {
                    
                    return;
                }
                DataTable dtLastVersion = dtData.Clone();
                foreach (DataRow dr in dtData.Rows)
                {
                    string fullfilePath = "";
                    if (sDbnull(dr["sFolder"], "") != "")
                        fullfilePath = Application.StartupPath + @"\" + sDbnull(dr["sFolder"], "") + @"\" + sDbnull(dr["sFileName"], "");
                    else
                        fullfilePath = Application.StartupPath + @"\" + sDbnull(dr["sFileName"], "");
                    if (!File.Exists(fullfilePath))
                    {
                        InsertNewRow(dr, dtData, ref dtLastVersion);
                        //Nếu tồn tại thì kiểm tra xem Version có khác nhau không?
                    }
                    else
                    {
                        FileVersionInfo _FileVersionInfo = FileVersionInfo.GetVersionInfo(fullfilePath);
                        System.IO.FileInfo fI = new FileInfo(fullfilePath);
                        long ticks = fI.LastWriteTime.Ticks;
                        string sVersion = sDbnull(_FileVersionInfo.ProductVersion, "");

                        if (!sVersion.Equals(dr["sVersion"]) || Int64Dbnull(dr["tick"], -1) > ticks)
                        {
                            InsertNewRow(dr, dtData, ref dtLastVersion);
                        }

                    }
                }
                dtLastVersion.AcceptChanges();
                Application.DoEvents();

                if (dtLastVersion.Rows.Count > 0)
                {
                    Tientrinh.Maximum = dtLastVersion.Rows.Count;
                    Tientrinh.Minimum = 0;
                    Tientrinh.Step = 1;
                    Tientrinh.Value = 1;
                    foreach (DataRow dr in dtLastVersion.Rows)
                    {
                        string fullfilePath = "";
                        string fullfilePath_rar = "";
                        string trueFile = "";
                        if (sDbnull(dr["sFolder"], "") != "")
                        {
                            fullfilePath = Application.StartupPath + "\\" + sDbnull(dr["sFolder"], "") + @"\" + sDbnull(dr["sFileName"], "");
                            fullfilePath_rar = Application.StartupPath + "\\" + sDbnull(dr["sFolder"], "") + @"\" + sDbnull(dr["sRarFileName"], "");
                        }
                        else
                        {
                            fullfilePath = Application.StartupPath + "\\" + sDbnull(dr["sFileName"], "");
                            fullfilePath_rar = Application.StartupPath + "\\" + sDbnull(dr["sRarFileName"], "");
                        }
                        trueFile=Int32Dbnull(dr["intRar"], 0) == 0 ? fullfilePath : fullfilePath_rar;
                        if (Tientrinh.Value + 1 > Tientrinh.Maximum)
                            Tientrinh.Value = Tientrinh.Maximum;
                        else
                            Tientrinh.Value += 1;
                        Application.DoEvents();
                        lblStatus.Visible = true;
                        lblStatus.Text = "Đang cập nhật: " + fullfilePath + "...";
                        Application.DoEvents();
                        if (!Directory.Exists(Path.GetDirectoryName(trueFile)))
                            Directory.CreateDirectory(Path.GetDirectoryName(trueFile));
                        SaveOldDLL(fullfilePath);
                        byte[] objData = dr["objData"] as byte[];
                        MemoryStream ms = new MemoryStream(objData);

                        FileStream fs = new FileStream( trueFile, FileMode.Create, FileAccess.Write);
                        ms.WriteTo(fs);
                        ms.Flush();
                        fs.Close();
                        try
                        {
                            if (Int32Dbnull(dr["intRar"], 0) == 1)
                            {
                                if (!File.Exists(Application.StartupPath + "\\WinRAR\\WinRAR.exe"))
                                {
                                    MessageBox.Show("Bạn cần copy chương trình giải nén file vào tại đường dẫn sau " + Application.StartupPath + "\\WinRAR\\WinRAR.exe");
                                    Syserr = true;
                                    return;
                                }
                                string pStartupPath = trueFile;
                                ProcessStartInfo info = new ProcessStartInfo();
                                lblStatus.Text = "Giải nén:" + sDbnull(dr["sFileName"]).ToString() + "...";
                                info.FileName = Application.StartupPath + "\\WinRAR\\WinRAR.exe";
                                info.Arguments = "e -pSYSMAN -o+ " + Strings.Chr(34) + pStartupPath + Strings.Chr(34) + " " + Strings.Chr(34) + Path.GetDirectoryName(trueFile) + Strings.Chr(34);
                                info.WindowStyle = ProcessWindowStyle.Hidden;
                                Process pro = System.Diagnostics.Process.Start(info);
                                pro.WaitForExit();
                                //
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi khi thực hiện giải nén file-->"+ex.Message);
                            Syserr = true;
                        }

                    }
                    lblStatus.Text = "Đã cập nhật xong. Xin vui lòng chờ trong giây lát...";
                    if (Debugger.IsAttached)
                    {
                        cmdUpdate.Visible = true;
                        cmdExit.Visible = true;
                    }
                    else
                    {
                        //OpenProcess(AppName);
                        //Try2ExitApp();
                    }
                }
                else
                {
                    lblStatus.Visible = true;
                    cmdUpdate.Visible = true;
                    lblStatus.Text = "Các phiên bản bạn đang dùng là mới nhất";
                    //OpenProcess(AppName);
                    //Try2ExitApp();
                }
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                //OpenProcess(AppName);
                //Try2ExitApp();
            }
            finally
            {
                lblStatus.Visible = true;
                cmdUpdate.Visible = true;
                lblStatus.Text = "Các phiên bản bạn đang dùng là mới nhất";
                if (!Syserr) OpenProcess(AppName);
                else
                {
                }
                Try2ExitApp();
            }
        }
        string sqlConnectionString = "";
        public void ReadConfig()
        {
            try
            {
                HIS.WS.LoginWS _LoginWS = new HIS.WS.LoginWS();

                string _path = Application.StartupPath + @"\Properties";

                ConfigProperties _ConfigProperties = PropertyLib.GetConfigProperties(_path);
                string ServerName = _ConfigProperties.DataBaseServer;
                string sUName = _ConfigProperties.UID;
                string sPwd = _ConfigProperties.PWD;
                string sDbName = _ConfigProperties.DataBaseName;
                _LoginWS.Url = _ConfigProperties.WSURL;
                if (_ConfigProperties.RunUnderWS)
                {
                    string DataBaseServer = "";
                    string DataBaseName = "";
                    string UID = "";
                    string PWD = "";
                    _LoginWS.GetConnectionString(ref ServerName, ref sDbName, ref sUName, ref sPwd);
                }
                sqlConnectionString = "workstation id=" + ServerName + ";packet size=4096;data source=" + ServerName + ";persist security info=False;initial catalog=" + sDbName + ";uid=" + sUName + ";pwd=" + sPwd;
            }
            catch (Exception ex)
            {
                sqlConnectionString = "";

            }
        }
        bool IsNumeric(object Value)
        {
            return Microsoft.VisualBasic.Information.IsNumeric(Value);
        }
        Int32 Int32Dbnull(object obj, object DefaultVal)
        {
            if (obj == null || obj == DBNull.Value || !IsNumeric(obj))
            {
                return Convert.ToInt32(DefaultVal);
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        long Int64Dbnull(object obj, object DefaultVal)
        {
            if (obj == null || obj == DBNull.Value || !IsNumeric(obj))
            {
                return Convert.ToInt64(DefaultVal);
            }
            else
            {
                return Convert.ToInt64(obj);
            }
        }
        public void SaveOldDLL(string pv_sFileName)
        {
            try
            {
                if (!Directory.Exists(Application.StartupPath + "\\OldVersions"))
                {
                    Directory.CreateDirectory(Application.StartupPath + "\\OldVersions");
                }
                if (!Directory.Exists(Path.GetDirectoryName( Application.StartupPath +@"\OldVersions\" + pv_sFileName)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(Application.StartupPath + @"\OldVersions\" + pv_sFileName));
                }
                if (File.Exists(Application.StartupPath + "\\" + pv_sFileName))
                {
                    File.Copy(Application.StartupPath + "\\" + pv_sFileName, Application.StartupPath + "\\OldVersions\\" + pv_sFileName, true);
                }

            }
            catch (Exception ex)
            {
            }
        }
        public void InsertNewRow(DataRow dr, DataTable pv_SourceTable, ref DataTable pv_DS)
        {
            try
            {
                DataRow DRLastestV = pv_DS.NewRow();
                foreach (DataColumn col in pv_SourceTable.Columns)
                {
                    DRLastestV[col.ColumnName] = dr[col.ColumnName];
                }
                pv_DS.Rows.Add(DRLastestV);

            }
            catch (Exception ex)
            {
            }
        }
         void OpenProcess(string appName)
        {
            try
            {
                System.Diagnostics.Process.Start(appName);
            }
            catch
            {
            }
        }

         void KillProcess(string appName)
        {
            try
            {
                appName = Path.GetFileNameWithoutExtension(appName);
                System.Diagnostics.Process[] arrProcess = System.Diagnostics.Process.GetProcessesByName(appName);
                if (arrProcess.Length > 0) arrProcess[0].Kill();
            }
            catch
            {
            }
        }

        bool ExistsProcess(string processName)
        {
            try
            {
                System.Diagnostics.Process[] arrProcess = System.Diagnostics.Process.GetProcessesByName(processName);
                return arrProcess.Length > 0;
            }
            catch
            {
                return false;
            }
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            OpenProcess(AppName);
            Try2ExitApp();
        }

        private void cmdUpdate_Click_1(object sender, EventArgs e)
        {
            StartUpdateVersion();
        }
    }
}
namespace VNS.Properties
{
    public class PropertyLib
    {
        public static ConfigProperties _ConfigProperties = new ConfigProperties();
        public static ConfigProperties GetConfigProperties(string _path)
        {
            try
            {
                if (!System.IO.Directory.Exists(_path))
                {
                    Directory.CreateDirectory(_path);
                }
                var myProperty = new ConfigProperties();
                string filePath = string.Format(@"{0}\{1}.xml", _path, myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (ConfigProperties)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new ConfigProperties();
            }
        }
    }
    public class ConfigProperties
    {

        public ConfigProperties()
        {
            DataBaseServer = "192.168.1.254";
            DataBaseName = "PACS";
            UID = "sa";
            PWD = "123456";
            ORM = "ORM";
            MaKhoa = "KKB";
            Maphong = "101";
            Somayle = "12345678";
            MaDvi = "HIS";
            Min = 0;
            Max = 1000;
            RunUnderWS = true;
            WSURL = "http://localhost:1695/AdminWS.asmx";
        }
        [Browsable(true), ReadOnly(false), Category("Webservice settings"),
Description("Địa chỉ Webservice"),
DisplayName("Địa chỉ Webservice")]
        public string WSURL { get; set; }
        [Browsable(true), ReadOnly(false), Category("Webservice settings"),
 Description("true=Kết nối qua Webservice để nhận chuỗi kết nối chung và kiểm tra giấy phép sử dụng trên máy chủ CSDL. False = Từng máy đăng ký và tự cấu hình vào CSDL"),
 DisplayName("Kết nối qua Webservice")]
        public bool RunUnderWS { get; set; }

       
        public int Min { get; set; }
        [Browsable(true), ReadOnly(false), Category("Department Settings"),
   Description("Số mã bệnh phẩm lớn nhất khi bác sĩ chỉ định CLS"),
   DisplayName("Số mã bệnh phẩm lớn nhất")]
        public int Max { get; set; }

        [Browsable(true), ReadOnly(false), Category("Department Settings"),
    Description("Mã đơn vị(Bệnh viện) sử dụng phần mềm"),
    DisplayName("Mã đơn vị thực hiện")]
        public string MaDvi { get; set; }

        [Browsable(true), ReadOnly(false), Category("Department Settings"),
     Description("Mã khoa đang sử dụng hệ thống phần mềm"),
     DisplayName("Mã khoa thực hiện")]
        public string MaKhoa { get; set; }

        [Browsable(true), ReadOnly(false), Category("Department Settings"),
     Description("Mã phòng đang sử dụng hệ thống phần mềm"),
     DisplayName("Mã phòng thực hiện")]
        public string Maphong { get; set; }

        [Browsable(true), ReadOnly(false), Category("Department Settings"),
     Description("Số máy lẻ của khoa sử dụng"),
     DisplayName("Số máy lẻ khoa sử dụng")]
        public string Somayle { get; set; }

        [Browsable(true), ReadOnly(false), Category("DataBase Settings"),
     Description("DataBase Server"),
     DisplayName("DataBase Server")]
        public string DataBaseServer { get; set; }

        [Browsable(true), ReadOnly(false), Category("DataBase Settings"),
       Description("DataBase Name"),
       DisplayName("DataBase Name")]
        public string DataBaseName { get; set; }

        [Browsable(true), ReadOnly(false), Category("DataBase Settings"),
     Description("DataBase User"),
     DisplayName("DataBase User")]
        public string UID { get; set; }

        [Browsable(true), ReadOnly(false), Category("DataBase Settings"),
     Description("DataBase Password"),
     DisplayName("DataBase Password")]
        public string PWD { get; set; }

        [Browsable(true), ReadOnly(false), Category("DataBase Settings"),
    Description("ProviderName"),
    DisplayName("ProviderName")]
        public string ORM { get; set; }

    }
}
