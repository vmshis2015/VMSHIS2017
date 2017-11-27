using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using Microsoft.VisualBasic;
using NLog;
using VNS.HIS.DAL;
using VNS.Libs;

namespace VMS.HIS.HLC.ASTM
{
    public class RocheCommunication
    {
        public static  Logger MyLog = LogManager.GetLogger("ASTM_AnalysisService");
        public static DataTable ReadOrders(string filePath)
        {
            var dt = new DataTable("Order");
            dt.Columns.AddRange(new[]
            {
                new DataColumn("Header", typeof (string)),
                new DataColumn("P", typeof (string)),
                new DataColumn("O", typeof (string)),
                new DataColumn("MA", typeof (string)),
                new DataColumn("KQ", typeof (string))
            });

            var lstLines = new List<string>();
            try
            {
                string folder = Path.GetDirectoryName(filePath);
                if (Utility.Laygiatrithamsohethong("ASTM_SECURITY", "0", false) == "1")
                {
                    //using (new NetworkConnection(_folder, Utility.CreateCredentials(Utility.Laygiatrithamsohethong("ASTM_UID", "UserName", false), Utility.Laygiatrithamsohethong("ASTM_PWD", "PassWord", false))))
                    var theNetworkCredential =
                        new NetworkCredential(Utility.Laygiatrithamsohethong("ASTM_UID", "UserName", false),
                            Utility.Laygiatrithamsohethong("ASTM_PWD", "PassWord", false));
                    var theNetcache = new CredentialCache();
                    if (folder != null) theNetcache.Add(new Uri(folder), "Basic", theNetworkCredential);
                }
                using (var reader = new StreamReader(filePath))
                {
                    while (reader.Peek() > -1)
                    {
                        lstLines.Add(reader.ReadLine());
                    }
                    reader.BaseStream.Flush();
                    reader.Close();
                }
                IEnumerable<string> patientinfor = from p in lstLines.AsEnumerable()
                    where p.StartsWith("P")
                    select p;
                IEnumerable<string> headerInfor = from p in lstLines.AsEnumerable()
                    where p.StartsWith("H")
                    select p;
                List<string> lstOLines = (from p in lstLines.AsEnumerable()
                    where p.StartsWith("O")
                    select p).ToList<string>();
                foreach (string ol in lstOLines)
                {
                    string[] arrValues = ol.Split('|');
                    string maChidinh = arrValues[2];
                    string ngayChidinh = arrValues[6];
                    string[] lstMaXN = arrValues[4].Replace("^", "").Split('\\');
                    foreach (string ma in lstMaXN)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Header"] = headerInfor.FirstOrDefault();
                        dr["P"] = patientinfor.FirstOrDefault();
                        dr["O"] = "O|1|" + maChidinh + "|ALL|||" + ngayChidinh + "|||||||||||||||||||F|";
                        dr["MA"] = ma;
                        dr["KQ"] = "";
                        dt.Rows.Add(dr);
                    }
                }
                dt.AcceptChanges();
                return dt;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return null;
            }
        }

        public static int WriteResultMessage(string resultFolderPath, DataTable dtData)
        {
            string ResultFolderPath_org = resultFolderPath;
            if (!Directory.Exists(resultFolderPath)) return -1;
            if (dtData == null || dtData.Rows.Count <= 0) return -2;
            string Header = dtData.Rows[0]["Header"].ToString();
            string patientInfor = dtData.Rows[0]["P"].ToString();
            List<string> LstOrderItems = (from p in dtData.AsEnumerable()
                select p["O"].ToString()).Distinct().ToList();
            string Footer = "L|1|N";
            string CommentLine = "C|1||Test 11150 comment|";
            string orderfileName = Utility.FixedFolder(resultFolderPath) + "Result_Message.txt";
            if (Utility.Laygiatrithamsohethong("ASTM_SECURITY", "0", false) == "1")
            {
                //using (new NetworkConnection(ResultFolderPath_org, Utility.CreateCredentials(Utility.Laygiatrithamsohethong("ASTM_UID", "UserName", false), Utility.Laygiatrithamsohethong("ASTM_PWD", "PassWord", false))))
                var theNetworkCredential =
                    new NetworkCredential(Utility.Laygiatrithamsohethong("ASTM_UID", "UserName", false),
                        Utility.Laygiatrithamsohethong("ASTM_PWD", "PassWord", false));
                var theNetcache = new CredentialCache();
                theNetcache.Add(new Uri(ResultFolderPath_org), "Basic", theNetworkCredential);
            }
            using (var _writer = new StreamWriter(orderfileName, false))
            {
                _writer.WriteLine(Header);
                _writer.WriteLine(patientInfor);
                foreach (string orderitems in LstOrderItems)
                {
                    _writer.WriteLine(orderitems);
                    DataRow[] arrDr = dtData.Select("O='" + orderitems + "'");
                    int idx = 1;
                    foreach (DataRow dr in arrDr)
                    {
                        string _R = "R|" + idx + "|^^^" + dr["MA"] + "^^^^|" + dr["KQ"] +
                                    "|mmol/L||||F||||20121031115507||";
                        _writer.WriteLine(_R);
                        _writer.WriteLine(CommentLine);
                        idx++;
                    }
                }
                _writer.WriteLine(Footer);
                _writer.Flush();
                _writer.Close();
            }
            return 0;
        }

        public static int WriteOrderMessage(string orderFolderPath, DataTable dtData)
        {
            MyLog.Trace(string.Format("---Begin sent order to Rocher----"));
            string orderFolderPathOrg = orderFolderPath;
            if (!Directory.Exists(orderFolderPath))
            {
                MyLog.Trace(string.Format("--- Path does not exist  ----"));
                return -1;
            }
            if (dtData == null || dtData.Rows.Count <= 0)
            {
                MyLog.Trace(string.Format("--- Object does not exist  ----"));
                return -2;
            }
          
            string header = @"H|\^&|||VMS-HIS|||||PSM||P||" + Utility.GetYYYYMMDDHHMMSS(globalVariables.SysDate);
            string patientInfor = "";
            var lstOrderItems = new List<string>();
            string Footer = "L|1|F";
            string maLuotkham = Utility.sDbnull(dtData.Rows[0][KcbLuotkham.Columns.MaLuotkham], "");
            MyLog.Trace(string.Format("---Begin sent order to Rocher of maluotkham: {0}  ----",maLuotkham));
            string tenBenhnhan =
                Utility.DoTrim(Utility.sDbnull(dtData.Rows[0][KcbDanhsachBenhnhan.Columns.TenBenhnhan], ""));
            string diaChi = Utility.DoTrim(Utility.sDbnull(dtData.Rows[0][KcbDanhsachBenhnhan.Columns.DiaChi], ""));
            string sodienthoai =
                Utility.DoTrim(Utility.sDbnull(dtData.Rows[0][KcbDanhsachBenhnhan.Columns.DienThoai], ""));
            string sobhyt = Utility.DoTrim(Utility.sDbnull(dtData.Rows[0][KcbLuotkham.Columns.MatheBhyt], ""));
            // string ten_benhnhan = Utility.DoTrim(Utility.UnSignedCharacter(Utility.sDbnull(dtData.Rows[0][KcbDanhsachBenhnhan.Columns.TenBenhnhan], ""))).ToUpper();
            string[] arrValues = tenBenhnhan.Split(' ');
            string ho = arrValues[0];
            string ten = tenBenhnhan.Substring(tenBenhnhan.IndexOf(' ') + 1);
            tenBenhnhan = ho + "^" + ten;
            string[] arrValuesDiaChi = diaChi.Split(',');
            string xaphuong = arrValuesDiaChi[0];
            string huyentinh = diaChi.Substring(diaChi.IndexOf(',') + 1);
            diaChi = xaphuong + "^" + huyentinh;
            patientInfor = "P|1||" + maLuotkham + "||" + tenBenhnhan + "||" +
                           Utility.sDbnull(dtData.Rows[0]["sngay_sinh"]) + "|" +
                           Utility.sDbnull(dtData.Rows[0]["sgioi_tinh"]) + "|" + diaChi + "|" + sodienthoai + "|" +
                           sobhyt + "|";
            List<string> q = (from p in dtData.AsEnumerable()
                select Utility.sDbnull(p["ma_chidinh"])).Distinct().ToList();
            int i = 1;

            foreach (string maChidinh in q)
            {
                MyLog.Trace(string.Format("---Sent order to Rocher of machidinh: {0}  ----", maChidinh));
                DataRow[] arrChitiet = dtData.Select("ma_chidinh='" + maChidinh + "'");
                string orderItems = "O|" + i + "|" + maChidinh + "||";
                string ngayChidinh = "";
                string maKhoaChidinh = "";
                string tenBacsychidinh = "";
                string tenKhoaphong = "";
                foreach (DataRow drchitiet in arrChitiet)
                {
                    if (ngayChidinh == "")
                    {
                        var ngaycd = (DateTime) drchitiet["ngay_chidinh"];
                        ngayChidinh = Utility.GetYYYYMMDDHHMMSS(ngaycd);
                    }
                    if (maKhoaChidinh == "")
                        maKhoaChidinh = Utility.sDbnull(drchitiet["ma_khoa_chidinh"], "KKB");
                    if (tenBacsychidinh == "") tenBacsychidinh = Utility.sDbnull(drchitiet["ten_nhanvien"], "");
                    if (tenKhoaphong == "") tenKhoaphong = Utility.sDbnull(drchitiet["id_phong_chidinh"], "");
                    string maXetnghiem = "^^^" + Utility.sDbnull(drchitiet["ma_xetnghiem"]) + @"\";
                    orderItems = orderItems + maXetnghiem;
                }
                //Loại bỏ dấu \ cuối cùng
                orderItems = orderItems.Substring(0, orderItems.Length - 1);
                orderItems += "|R|" + ngayChidinh + "|||||A|||||" + tenBacsychidinh + "||" + tenKhoaphong +
                              "||||||||O";
                lstOrderItems.Add(orderItems);
                MyLog.Trace(string.Format("---End order to Rocher of machidinh: {0}  ----", maChidinh));
            }
            if (!orderFolderPath.EndsWith(@"\")) orderFolderPath += @"\";
            string seqNum = Utility.GetSequence().ToString(CultureInfo.InvariantCulture);
            if (seqNum == "-1") seqNum = Strings.Right("000000" + dtData.Rows[0][KcbChidinhcl.Columns.IdChidinh], 6);
            else
                seqNum = Strings.Right("000000" + seqNum, 6);
            string orderfileName = orderFolderPath + "Order" + seqNum + ".txt";
            if (Utility.Laygiatrithamsohethong("ASTM_SECURITY", "0", false) == "1")
            {
                //using (new NetworkConnection(orderFolderPath_org, Utility.CreateCredentials(Utility.Laygiatrithamsohethong("ASTM_UID", "UserName", false), Utility.Laygiatrithamsohethong("ASTM_PWD", "PassWord", false))))
                var theNetworkCredential =
                    new NetworkCredential(Utility.Laygiatrithamsohethong("ASTM_UID", "UserName", false),
                        Utility.Laygiatrithamsohethong("ASTM_PWD", "PassWord", false));
                var theNetcache = new CredentialCache();
                theNetcache.Add(new Uri(orderFolderPathOrg), "Basic", theNetworkCredential);
            }
            using (var writer = new StreamWriter(orderfileName, false))
            {
                writer.WriteLine(header);
                writer.WriteLine(patientInfor);
                foreach (string orderitems in lstOrderItems)
                    writer.WriteLine(orderitems);
                writer.WriteLine(Footer);
                writer.Flush();
                writer.Close();
            }
            MyLog.Trace(string.Format("---End sent order to Rocher of maluotkham: {0}  ----", maLuotkham));
            return 0;
        }
    }

    public class DeviceHelper
    {
        #region Fields

        private static readonly string[] VietnameseSigns =
        {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
        };

        private static readonly string _barcodeType = string.Empty;

        #endregion

        static DeviceHelper()
        {
            new DeviceHelper();
        }

        #region Constant

        public const char NULL = (char) 0;
        public const char STX = (char) 2;
        public const char ETX = (char) 3;
        public const char EOT = (char) 4;
        public const char ENQ = (char) 5;
        public const char ACK = (char) 6;
        public const char CR = (char) 13;
        public const char LF = (char) 10;
        public const char VT = (char) 11;
        public const char NAK = (char) 21;
        public const char ETB = (char) 23;
        public const char FS = (char) 28;
        public const char GS = (char) 29;
        public const char RS = (char) 30;
        public const char SOH = (char) 1;
        public const char SYN = (char) 22;
        public const char DC1 = (char) 17;
        public const char DC2 = (char) 18;
        public const char DC3 = (char) 19;
        public const char DC4 = (char) 20;
        public static readonly string CRLF = String.Format("{0}{1}", CR, LF);

        #endregion
    }
}