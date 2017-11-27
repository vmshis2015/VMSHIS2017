using System;
using System.Globalization;
using System.IO;
using System.ServiceProcess;
using System.Xml;
using NLog;
using NLog.Config;
using NLog.Targets;
using SubSonic;
using VNS.Libs;

namespace VMS.FSW
{
    public partial class FSWService : ServiceBase
    {
         HLCFileWatcherServer HLCFW = null;
        int _interval = 600000;
        public FSWService()
        {
            InitializeComponent();
            LogConfig();
            InitSubSonic(GetConnectionString(@"C:\VMSConfig\HISconfig"), "ORM");
            string intervalFile = AppDomain.CurrentDomain.BaseDirectory + @"\timerInterval.txt";
            if (File.Exists(intervalFile))
            {
                _interval = Convert.ToInt32(File.ReadAllText(intervalFile));

            }
            else
            {
                File.WriteAllText(intervalFile, _interval.ToString(CultureInfo.InvariantCulture));
            }
            HLCFW = new HLCFileWatcherServer();
            HLCFW.MyLog.Trace(string.Format("Add SystemFileWatcher for the Folder {0}", Utility.Laygiatrithamsohethong("ASTM_RESULTS_FOLDER", @"\\192.168.1.254\Results\", false)));
            HLCFW.AddWatcher(Utility.Laygiatrithamsohethong("ASTM_RESULTS_FOLDER", @"\\192.168.1.254\Results\", false), _interval);
            
        }
        public static void LogConfig()
        {
            try
            {
               
                var config = new LoggingConfiguration();
                var fileTarget = new FileTarget
                {
                    FileName =
                        "${basedir}/AppLogs/${date:format=yy}${date:format=MM}${date:format=dd}/${logger}.log",
                    //"${basedir}/AppLogs/${date:format=yyyy}/${date:format=MM}/${date:format=dd}/${logger}.log",
                    Layout =
                        "${date:format=dd/MM/yyy HH\\:mm\\:ss}|${threadid}|${level}|${logger}|${message}",
                    ArchiveAboveSize = 5242880
                };
                config.AddTarget("file", fileTarget);
                config.LoggingRules.Add(new LoggingRule("*", LogLevel.Trace, fileTarget));
                LogManager.Configuration = config;
            }
            catch (Exception ex)
            {
               // HLCFW.MyLog.Trace("LogConfig.Exception-->" + ex.Message);
            }
        }
        private static string GetConnectionString(string configDir)
        {
            string _defaultConnString =
                "workstation id=192.168.0.168; Data Source=192.168.0.168; Initial catalog=PACS;uid=sa;pwd=123456";
            try
            {
                if (configDir.Trim() == "") configDir = @"C:\VMSConfig\HISconfig";
                string filePath = configDir + @"\Servers.xml";
                if (File.Exists(filePath))
                {
                    var xmLdoc = new XmlDocument();
                    xmLdoc.Load(filePath);
                    XmlNode xmlNode = xmLdoc.SelectSingleNode(@"NewDataSet/SQLSERVER/CONNECTSTRING");
                    if (xmlNode != null)
                        return xmlNode.InnerText;
                }
                return _defaultConnString;
            }
            catch
            {
                return _defaultConnString;
            }
        }
        internal class CustomSqlProvider : SqlDataProvider
        {
            public CustomSqlProvider(string connectionString)
            {
                DefaultConnectionString = connectionString;
            }

            public override string Name
            {
                get { return "ORM"; }
            }
        }
        public static void InitSubSonic(string sqlConnstr, string providerName)
        {
            //MyLog.Trace("\r\n-----------------------------------------------------------------------");
            //MyLog.Trace(string.Format("Connection String: {0}", SqlConnstr));
            DataService.Providers = new DataProviderCollection();
            var myProvider = new CustomSqlProvider(sqlConnstr);
            if (DataService.Providers[providerName] == null)
            {
                DataService.Providers.Add(myProvider);
                DataService.Provider = myProvider;
            }
            else
            {
                DataService.Provider.DefaultConnectionString = sqlConnstr;
            }
        }
        protected override void OnStart(string[] args)
        {
            try
            {
                HLCFW.MyLog.Trace("Start Server..........");
                HLCFW.StartServer();

            }
            catch (Exception ex)
            {
               HLCFW.MyLog.Trace("OnStart.Exception-->" + ex.Message);
            }
        }

        protected override void OnStop()
        {
        }

       
       
    }
}


