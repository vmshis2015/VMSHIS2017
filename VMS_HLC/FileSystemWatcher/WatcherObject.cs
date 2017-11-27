using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Permissions;
using System.Timers;
using NLog;
using VNS.Libs;

namespace VMS.FSW
{
    public class WatcherObject
    {
        #region Attributes

        private readonly Timer _myTimer = new Timer(15000);

        /// <summary>
        /// </summary>
        private readonly DirectoryInfo _watcherPathInfo; // = new DirectoryInfo(WatchedPath);

        public Dictionary<string, FileSystemEventArgs> ListEvent = new Dictionary<string, FileSystemEventArgs>();
        public Dictionary<string, object> ListObject = new Dictionary<string, object>();
        private FileSystemWatcher _fileWatcher;
        private DateTime _lastModifitime = DateTime.Now;
        public Logger MyLog;
        public string WatchedPath { get; set; }

        public event OnChangeHandler Change;
        public event OnRenameHandler Rename;

        #endregion

        #region Contructor

        public WatcherObject(string resultPath)
        {
            WatchedPath = resultPath;
            _watcherPathInfo = new DirectoryInfo(resultPath);
            _myTimer.Elapsed += _myTimer_Elapsed;
        }

        public WatcherObject(string resultPath, int intervalTime)
        {
            WatchedPath = resultPath;
            _watcherPathInfo = new DirectoryInfo(resultPath);
            _myTimer.Interval = intervalTime;
            _myTimer.Elapsed += _myTimer_Elapsed;
        }

        public WatcherObject()
        {
            _myTimer.Elapsed += _myTimer_Elapsed;
        }

        #endregion

        #region Public Method

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public void StartWatch()
        {
            try
            {
                bool writeNotExists = false;
                MyLog.Trace("Start Watching...");
                if (Utility.Laygiatrithamsohethong("ASTM_SECURITY", "0", false) == "1")
                {
                    var theNetworkCredential =
                        new NetworkCredential(Utility.Laygiatrithamsohethong("ASTM_UID", "UserName", false),
                            Utility.Laygiatrithamsohethong("ASTM_PWD", "PassWord", false));
                    var theNetcache = new CredentialCache();
                    theNetcache.Add(new Uri(WatchedPath), "Basic", theNetworkCredential);
                }
                //while (!Directory.Exists(WatchedPath))
                ////while (!CheckPath(WatchedPath))
                //{
                //    myLog.Trace(string.Format("WatchedPath {0} is not existed", WatchedPath));
                //    if (!writeNotExists)
                //    {
                //        writeNotExists = true;
                //    }
                //    //Nếu chưa tìm thấy thư mục cần theo dõi thì lặp lại liên tục
                //    Thread.Sleep(2000);
                //}
                _lastModifitime = GetlastModifiedTime();
                // Create a new FileSystemWatcher and set its properties.
                //_fileWatcher = new FileSystemWatcher
                //{
                //    Path = WatchedPath,
                //    NotifyFilter = NotifyFilters.LastWrite
                //};
                // Lấy về thời gian thay đổi cuối cùng của thư mục

                _myTimer.Start();
            }
            catch (Exception ex)
            {
                MyLog.Error(string.Format("StartWatch.Exception-->{0}", ex.Message));
            }
        }

        #endregion

        #region Private Method

        /// <summary>
        ///     Hàm kiểm tra file mới thay đổi và phát sinh sự kiện
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _myTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _myTimer.Stop();
            // Lấy ra tên file mới
            try
            {
                var fileList = new List<string>();
                MyLog.Trace(string.Format("Veryfying NetworkCredential..."));
                if (Utility.Laygiatrithamsohethong("ASTM_SECURITY", "0", false) == "1")
                {
                    var theNetworkCredential =
                        new NetworkCredential(Utility.Laygiatrithamsohethong("ASTM_UID", "UserName", false),
                            Utility.Laygiatrithamsohethong("ASTM_PWD", "PassWord", false));
                    var theNetcache = new CredentialCache();
                    theNetcache.Add(new Uri(WatchedPath), "Basic", theNetworkCredential);
                }
                MyLog.Trace(string.Format("Gettings result files..."));
                using (
                    new NetworkConnection(WatchedPath,
                        Utility.CreateCredentials(Utility.Laygiatrithamsohethong("ASTM_UID", "UserName", false),
                            Utility.Laygiatrithamsohethong("ASTM_PWD", "PassWord", false))))
                {
                    fileList = Directory.GetFiles(WatchedPath, "*.txt", SearchOption.TopDirectoryOnly).ToList();
                }
                // fileList.OrderBy(c => c.LastWriteTime);
                var fileQuery = new List<string>();
                foreach (string f in fileList)
                    //if (f.LastWriteTime.Ticks > _lastModifitime.Ticks)//Tạm bỏ đi do sẽ di chuyển file đã phân tích sang thư mục khác+ đề phòng file đọc bị lỗi lại không được đưa vào danh sách để đọc lại
                    fileQuery.Add(f);
                foreach (string s in fileQuery)
                {
                    MyLog.Trace(string.Format("Invoking Analysis() method"));
                    // Change.Invoke(_fileWatcher, new FileSystemEventArgs(WatcherChangeTypes.Changed, WatchedPath, s.Substring(s.LastIndexOf('\\') + 1)));
                    Change.Invoke(null,
                        new FileSystemEventArgs(WatcherChangeTypes.Changed, WatchedPath,
                            s.Substring(s.LastIndexOf('\\') + 1)));
                    _lastModifitime = new FileInfo(s).LastWriteTime;
                }
            }
            catch (Exception ex)
            {
                MyLog.Error(string.Format("StartWatch.Exception-->{0}", ex.Message));
            }
            finally
            {
                _myTimer.Start();
            }
        }

        /// <summary>
        ///     Lấy về thời gian thay đổi file cuối cùng
        /// </summary>
        /// <returns></returns>
        private DateTime GetlastModifiedTime()
        {
            try
            {
                IEnumerable<FileInfo> fileList = null;
                if (Utility.Laygiatrithamsohethong("ASTM_SECURITY", "0", false) == "1")
                {
                    //using (new NetworkConnection(_watcherPathInfo, Utility.CreateCredentials(Utility.Laygiatrithamsohethong("ASTM_UID", "UserName", false), Utility.Laygiatrithamsohethong("ASTM_PWD", "PassWord", false))))
                    var theNetworkCredential =
                        new NetworkCredential(Utility.Laygiatrithamsohethong("ASTM_UID", "UserName", false),
                            Utility.Laygiatrithamsohethong("ASTM_PWD", "PassWord", false));
                    var theNetcache = new CredentialCache();
                    theNetcache.Add(new Uri(WatchedPath), "Basic", theNetworkCredential);
                }
                using (
                    new NetworkConnection(WatchedPath,
                        Utility.CreateCredentials(Utility.Laygiatrithamsohethong("ASTM_UID", "UserName", false),
                            Utility.Laygiatrithamsohethong("ASTM_PWD", "PassWord", false))))
                {
                    fileList = _watcherPathInfo.GetFiles("*.txt",
                        SearchOption.TopDirectoryOnly);
                }
                // Nếu không tồn tại file nào trả về thời gian hiện tại
                if (!fileList.Any()) return DateTime.Now;
                // Nếu có file trả về thời gian thay đổi của file cuối cùng
                return (from file in fileList
                    orderby file.LastWriteTime descending
                    select file.LastWriteTime).FirstOrDefault();
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
        }

        #endregion
    }
}