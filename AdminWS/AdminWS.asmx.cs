using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml.Serialization;
using AppType;
using System.Management;

using System.Security.Cryptography;
using System.Security;
using System.Collections;
using System.Text;

namespace AdminWS
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class LoginWS : System.Web.Services.WebService
    {

        [WebMethod]
        public string GetConnectionString(ref string DataBaseServer, ref  string DataBaseName, ref  string UID, ref  string PWD)
        {
            try
            {
                VNS.Properties.PropertyLib._ConfigProperties = VNS.Properties.PropertyLib.GetConfigProperties(@"C:\Configs\HISConfig\Properties");
                string strSQL =
                string.Format(
                    "workstation id={0};packet size=4096;data source={0};persist security info=False;initial catalog={1};uid={2};pwd={3}",
                    VNS.Properties.PropertyLib._ConfigProperties.DataBaseServer, VNS.Properties.PropertyLib._ConfigProperties.DataBaseName,
                    VNS.Properties.PropertyLib._ConfigProperties.UID, VNS.Properties.PropertyLib._ConfigProperties.PWD);
                DataBaseServer = VNS.Properties.PropertyLib._ConfigProperties.DataBaseServer;
                DataBaseName = VNS.Properties.PropertyLib._ConfigProperties.DataBaseName;
                UID = VNS.Properties.PropertyLib._ConfigProperties.UID;
                PWD = VNS.Properties.PropertyLib._ConfigProperties.PWD;
                return strSQL;

            }
            catch (Exception ex)
            {
                return "GetConnectionString:" + ex.Message;
            }
        }
        [WebMethod]
        public bool IsValidLicense()
        {
            return isValidSoftKey();
        }

        string getRegKeyBasedOnSCPLicense()
        {
            try
            {
                string fileName = @"C:\Configs\HISConfig\license.lic";
                if (!File.Exists(fileName)) return "";
                using (StreamReader _streamR = new StreamReader(fileName))
                {
                    return _streamR.ReadLine();
                }
            }
            catch
            {
                return "";
            }
        }
        bool isValidSoftKey()
        {
            try
            {
                string sRegKey = "";
                sRegKey = getRegKeyBasedOnSCPLicense();
                string GenKey = Security.HardWare.Value("XFW");
                string RegKey = Security.HardWare.GetKey(GenKey);
                bool isValidLicense = sRegKey == RegKey;
                if (!isValidLicense)
                {
                    AppLogger.LogAction.LogSCPService(string.Format(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "-->Kiểm tra khóa mềm không hợp lệ."));
                    return false;
                }
                else//Khóa check OK
                {
                    AppLogger.LogAction.LogSCPService(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "-->Kiểm tra khóa mềm hợp lệ...");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                AppLogger.LogAction.LogSCPService(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "-->Lỗi khi kiểm tra khóa mềm-->" + ex.Message);
                return false;
            }
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
            HIS_AppMode = AppEnum.AppMode.License;
            HIS_HardKeyType = AppEnum.HardKeyType.SOFTKEY;
            RunUnderWS = true;
        }
        [Browsable(true), ReadOnly(false), Category("PACS License settings"),
 Description("true=Kết nối qua Webservice để nhận chuỗi kết nối chung và kiểm tra giấy phép sử dụng trên máy chủ CSDL. False = Từng máy đăng ký và tự cấu hình vào CSDL"),
 DisplayName("Kết nối qua Webservice")]
        public bool RunUnderWS { get; set; }

        [Browsable(true), ReadOnly(false), Category("PACS License settings"),
  Description("Kiểu giấy phép"),
  DisplayName("HIS License Type")]
        public AppEnum.HardKeyType HIS_HardKeyType { get; set; }
        [Browsable(true), ReadOnly(false), Category("PACS License settings"),
Description("Kiểu ứng dụng"),
DisplayName("HIS Application Mode")]
        public AppEnum.AppMode HIS_AppMode { get; set; }
        [Browsable(true), ReadOnly(false), Category("Department Settings"),
   Description("Số mã bệnh phẩm nhỏ nhất khi bác sĩ chỉ định CLS"),
   DisplayName("số mã bệnh phẩm nhỏ nhất")]
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
namespace AppType
{
    public class AppEnum
    {
        public enum InsertReturn
        {
            Exists = 0, Error = 1, Success = 2
        }
        public enum WORKLISTSEARCHTYPE { ModalityWorklistQuery = 0, PatientRootQueryStudy = 1 }
        public enum HardKeyType { SOFTKEY = 0, USBDONGLE = 1, MIXEDMODE = 2, SOFTKEY_EXP = 3 }
        public enum AppName { XFILM = 0, XVIEW = 1, RISLINK = 2, DICOMVIEWER = 3 }
        public enum HIDReaderType { Internal = 0, FromFile = 1 }
        public enum DataBaseType { SQLSERVER = 0, SQLCE = 1, OLEDB = 2, ORACLE = 3 }
        public enum Navigation { None = 0, Next = 1, Back = 2 };
        public enum TabMode { WorkList = 0, Acq = 1, StudyList = 2, System = 3, Viewer = 4, Print = 5, Option = 6, List = 7, Other = 8, Suspending = 9 };
        public enum DoubleMode { WorkList = 0, StudyList = 2 };
        public enum AppMode { Demo = 0, License = 1, Developer = 2 };
        public enum ViewState { Ready = 0, Capture = 1 };
        public enum FPDMode { SingleMode = 0, DualMode = 1, Other = 2 };
        public enum LoginMode { None = -1, Login = 0, Logout = 1 };
        public enum ClickMode { Click = 0, DoubleClick = 1 };
        public enum ToolMode { None = 0, WindowLevel = 1, Zoom = 2, Magnify = 3, Crop = 4, Annotation = 5, Select = 6 };
        public enum AnnType { None = 0, Arrow = 1, Square = 2, Ellipse = 3, Pen = 4, Angle = 5, Ruler = 6 };
    }
}
namespace Security
{
    public class HardwareController
    {
        public static string GetCombinedKey()
        {
            return GetCPUId() + "-" + GetMotherBoardID() + "-" + GetHDDSerial();
        }
        public static string GetHDDSerial()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");

                foreach (ManagementObject wmi_HD in searcher.Get())
                {
                    // get the hardware serial no.
                    if (wmi_HD["SerialNumber"] != null)
                        return wmi_HD["SerialNumber"].ToString().Trim().Replace('-', '#');
                }

                return "SRLNBR";
            }
            catch
            {
                return "SRLNBR";
            }
        }
        public static string GetMotherBoardID()
        {
            string mbInfo = String.Empty;
            ManagementScope scope = new ManagementScope("\\\\" + Environment.MachineName + "\\root\\cimv2");
            scope.Connect();
            ManagementObject wmiClass = new ManagementObject(scope, new ManagementPath("Win32_BaseBoard.Tag=\"Base Board\""), new ObjectGetOptions());

            foreach (PropertyData propData in wmiClass.Properties)
            {
                if (propData.Name == "SerialNumber")
                    mbInfo = String.Format("{0}", Convert.ToString(propData.Value).Trim()).Replace('-', '#'); ;
            }

            return mbInfo;
        }
        public static String GetCPUId()
        {
            String processorID = "";

            ManagementObjectSearcher searcher = new ManagementObjectSearcher(
            "Select * FROM WIN32_Processor");

            ManagementObjectCollection mObject = searcher.Get();

            foreach (ManagementObject obj in mObject)
            {
                processorID = obj["ProcessorId"].ToString().Trim().Replace('-', '#'); ;
            }

            return processorID;
        }
    }

    /// <summary>
    /// Generates a 16 byte Unique Identification code of a computer
    /// Example: 4876-8DB5-EE85-69D3-FE52-8CF7-395D-2EA9
    /// </summary>
    public class HardWare
    {
        private static string fingerPrint = string.Empty;
        public static string Value(string AppName)
        {
            fingerPrint = string.Empty;
            if (string.IsNullOrEmpty(fingerPrint))
            {
                string _cpuIdVal = cpuId();
                string _biosIdVal = biosId();
                string _baseIdVal = baseId();
                string _combineVal = AppName + ">>AppName" + "\nCPU >> " + _cpuIdVal + "\nBIOS >> " + _biosIdVal + "\nBASE >> " + _baseIdVal;
                fingerPrint = GetHash(_combineVal
                    //+"\nDISK >> "+ diskId() + "\nVIDEO >> " + videoId() +"\nMAC >> "+ macId()
                                     );
            }
            return fingerPrint.Replace("-", "");
        }
        public static string GetKey(string Value)
        {
            string reval = "YOURHARDKEY:" + Value;
            return GetHash(reval);
        }

        private static string GetHash(string s)
        {
            MD5 sec = new MD5CryptoServiceProvider();
            ASCIIEncoding enc = new ASCIIEncoding();
            byte[] bt = enc.GetBytes(s);
            return GetHexString(sec.ComputeHash(bt));
        }
        private static string GetHexString(byte[] bt)
        {
            string s = string.Empty;
            for (int i = 0; i < bt.Length; i++)
            {
                byte b = bt[i];
                int n, n1, n2;
                n = (int)b;
                n1 = n & 15;
                n2 = (n >> 4) & 15;
                if (n2 > 9)
                    s += ((char)(n2 - 10 + (int)'A')).ToString();
                else
                    s += n2.ToString();
                if (n1 > 9)
                    s += ((char)(n1 - 10 + (int)'A')).ToString();
                else
                    s += n1.ToString();
                if ((i + 1) != bt.Length && (i + 1) % 2 == 0) s += "-";
            }
            return s;
        }


        #region Original Device ID Getting Code
        //Return a hardware identifier
        private static string identifier(string wmiClass, string wmiProperty, string wmiMustBeTrue)
        {
            string result = "";
            System.Management.ManagementClass mc = new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {
                if (mo[wmiMustBeTrue].ToString() == "True")
                {
                    //Only get the first one
                    if (result == "")
                    {
                        try
                        {
                            result = mo[wmiProperty].ToString();
                            break;
                        }
                        catch
                        {
                        }
                    }
                }
            }
            return result;
        }
        //Return a hardware identifier
        private static string identifier(string wmiClass, string wmiProperty)
        {
            string result = "";
            System.Management.ManagementClass mc = new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {
                //Only get the first one
                if (result == "")
                {
                    try
                    {
                        result = mo[wmiProperty].ToString();
                        break;
                    }
                    catch
                    {
                    }
                }
            }
            return result;
        }
        private static string cpuId()
        {
            //Uses first CPU identifier available in order of preference
            //Don't get all identifiers, as very time consuming
            string retVal = identifier("Win32_Processor", "UniqueId");
            if (retVal == "") //If no UniqueID, use ProcessorID
            {
                retVal = identifier("Win32_Processor", "ProcessorId");
                if (retVal == "") //If no ProcessorId, use Name
                {
                    retVal = identifier("Win32_Processor", "Name");
                    if (retVal == "") //If no Name, use Manufacturer
                    {
                        retVal = identifier("Win32_Processor", "Manufacturer");
                    }
                    //Add clock speed for extra security
                    retVal += identifier("Win32_Processor", "MaxClockSpeed");
                }
            }
            return retVal;
        }
        //BIOS Identifier
        private static string biosId()
        {
            return identifier("Win32_BIOS", "Manufacturer")
            + identifier("Win32_BIOS", "SMBIOSBIOSVersion")
            + identifier("Win32_BIOS", "IdentificationCode")
            + identifier("Win32_BIOS", "SerialNumber")
            + identifier("Win32_BIOS", "ReleaseDate")
            + identifier("Win32_BIOS", "Version");
        }
        //Main physical hard drive ID
        private static string diskId()
        {
            return identifier("Win32_DiskDrive", "Model")
            + identifier("Win32_DiskDrive", "Manufacturer")
            + identifier("Win32_DiskDrive", "Signature")
            + identifier("Win32_DiskDrive", "TotalHeads");
        }
        //Motherboard ID
        private static string baseId()
        {
            return identifier("Win32_BaseBoard", "Model")
            + identifier("Win32_BaseBoard", "Manufacturer")
            + identifier("Win32_BaseBoard", "Name")
            + identifier("Win32_BaseBoard", "SerialNumber");
        }
        //Primary video controller ID
        private static string videoId()
        {
            return identifier("Win32_VideoController", "DriverVersion")
            + identifier("Win32_VideoController", "Name");
        }
        //First enabled network card ID
        private static string macId()
        {
            return identifier("Win32_NetworkAdapterConfiguration", "MACAddress", "IPEnabled");
        }
        #endregion
    }

}
