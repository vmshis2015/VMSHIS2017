﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Transactions;
using SubSonic;
using VNS.HIS.DAL;
using VNS.Libs;

namespace VMS.FSW
{
    internal class HLCFileWatcherServer : FileWatcherServer
    {
        #region Attributies

        #endregion

        #region Contructor

        #endregion

        public override void OnChanged(object source, FileSystemEventArgs e)
        {
            try
            {
                if (e.ChangeType == WatcherChangeTypes.Deleted) return;
                StartAnalysisFile(e.FullPath);
            }
            catch (Exception ex)
            {
                MyLog.Error(string.Format(ex.Message));
            }
        }

        private void StartAnalysisFile(string fullpath)
        {
            MyLog.Trace(string.Format("------------------Begin Analysing file {0}------------------", fullpath));
            var lstLines = new List<string>();
            try
            {
                if (Utility.Laygiatrithamsohethong("ASTM_SECURITY", "0", false) == "1")
                {
                    //using (new NetworkConnection(_watcherPathInfo, Utility.CreateCredentials(Utility.Laygiatrithamsohethong("ASTM_UID", "UserName", false), Utility.Laygiatrithamsohethong("ASTM_PWD", "PassWord", false))))
                    var theNetworkCredential =
                        new NetworkCredential(Utility.Laygiatrithamsohethong("ASTM_UID", "UserName", false),
                            Utility.Laygiatrithamsohethong("ASTM_PWD", "PassWord", false));
                    var theNetcache = new CredentialCache();
                    theNetcache.Add(new Uri(Path.GetDirectoryName(fullpath)), "Basic", theNetworkCredential);
                }
                using (
                    new NetworkConnection(Path.GetDirectoryName(fullpath),
                        Utility.CreateCredentials(Utility.Laygiatrithamsohethong("ASTM_UID", "UserName", false),
                            Utility.Laygiatrithamsohethong("ASTM_PWD", "PassWord", false))))
                {
                    using (var _reader = new StreamReader(fullpath))
                    {
                        while (_reader.Peek() > -1)
                        {
                            lstLines.Add(_reader.ReadLine());
                        }
                        _reader.BaseStream.Flush();
                        _reader.Close();
                    }

                    MyLog.Trace(string.Format("Read All lines"));
                    IEnumerable<string> patientinfor = from p in lstLines.AsEnumerable()
                        where p.StartsWith("P")
                        select p;
                    IEnumerable<string> Orderinfor = from p in lstLines.AsEnumerable()
                        where p.StartsWith("O")
                        select p;
                    var lstmachidinh = new List<string>();
                    foreach (string line in lstLines)
                    {
                        if (line.StartsWith("O")) //Chỉ định
                            if (!lstmachidinh.Contains(line.Split('|')[2]))
                                lstmachidinh.Add(line.Split('|')[2]);
                    }
                    bool isOK = false;
                    if (patientinfor.Any() && Orderinfor.Any())
                    {
                        MyLog.Trace(string.Format("File format is valid"));
                        MyLog.Trace(string.Format("Number of AssignCode {0}", lstmachidinh.Count));
                        var lstKq = new KcbKetquaClCollection();
                        MyLog.Trace(string.Format("GetData base on PID {0} and AssignCode List {1} ",
                            patientinfor.FirstOrDefault().Split('|')[2], string.Join(",", lstmachidinh.ToArray())));
                        DataTable dtData =
                            SPs.HisLisLaydulieuCapnhatketquatuLis(patientinfor.FirstOrDefault().Split('|')[2],
                                string.Join(",", lstmachidinh.ToArray())).GetDataSet().Tables[0];

                        if (dtData != null && dtData.Rows.Count > 0)
                        {
                            using (var scope = new TransactionScope())
                            {
                                using (var dbscope = new SharedDbConnectionScope())
                                {
                                    MyLog.Trace(string.Format("Number of DataRow:={0}", dtData.Rows.Count));
                                    string ma_chidinh = "";
                                    int idx = 0;
                                    foreach (string line in lstLines)
                                    {
                                        if (line.StartsWith("O")) //Order
                                            ma_chidinh = line.Split('|')[2];
                                        if (line.StartsWith("R")) //Result
                                        {
                                            idx++;
                                            MyLog.Trace(string.Format("Begin Analysing Line {0}...", idx));
                                            string[] arrValues = line.Split('|');
                                            string ma_xetnghiem = arrValues[2].Replace("^", "");
                                            string ketqua = arrValues[3];
                                            string dvt = arrValues[4];
                                            string ngaytraketqua = arrValues[12];
                                            MyLog.Trace(
                                                string.Format(
                                                    "Line Data-->ma_chidinh={0} ma_xetnghiem={1}, ketqua={2}, DVT={3},ngaytraketqua={4} ",
                                                    ma_chidinh, ma_xetnghiem, ketqua, dvt, ngaytraketqua));
                                            DataRow[] arrCt =
                                                dtData.Select("ma_xetnghiem='" + ma_xetnghiem + "' AND ma_chidinh='" +
                                                              ma_chidinh + "'");

                                            if (arrCt.Length > 0)
                                            {
                                                MyLog.Trace(
                                                    string.Format(
                                                        "Row Data-->ma_chidinh={0} ma_xetnghiem={1}, id_chidinh={2}, id_chitietchidinh={3},id_chitietdichvu={4} ",
                                                        arrCt[0]["ma_chidinh"],
                                                        arrCt[0]["ma_xetnghiem"], arrCt[0]["id_chidinh"],
                                                        arrCt[0]["id_chitietchidinh"], arrCt[0]["id_chitietdichvu"]));
                                                //Nếu không có chi tiết thì update vào cả bảng chỉ định cận lâm sàng chi tiết

                                                KcbChidinhclsChitiet objChitiet =
                                                    KcbChidinhclsChitiet.FetchByID(
                                                        Utility.Int64Dbnull(arrCt[0]["id_chitietchidinh"]));
                                                if (objChitiet != null)
                                                {
                                                    if (
                                                        !Utility.Byte2Bool(Utility.ByteDbnull(arrCt[0]["co_chitiet"], 0)))
                                                    {
                                                        objChitiet.KetQua = ketqua;
                                                    }
                                                    objChitiet.IsNew = false;
                                                    objChitiet.TrangThai = 4;
                                                    objChitiet.MarkOld();
                                                    objChitiet.Save();
                                                }
                                                var kq = new KcbKetquaCl();
                                                kq = new Select().From(KcbKetquaCl.Schema)
                                                    .Where(KcbKetquaCl.Columns.IdChidinh)
                                                    .IsEqualTo(Utility.Int64Dbnull(arrCt[0]["id_chidinh"]))
                                                    .And(KcbKetquaCl.Columns.IdChitietchidinh)
                                                    .IsEqualTo(Utility.Int64Dbnull(arrCt[0]["id_chitietchidinh"]))
                                                    .And(KcbKetquaCl.Columns.IdDichvuchitiet)
                                                    .IsEqualTo(Utility.Int64Dbnull(arrCt[0]["id_chitietdichvu"]))
                                                    .ExecuteSingle<KcbKetquaCl>();
                                                if (kq == null)
                                                {
                                                    MyLog.Trace(
                                                        string.Format("ma_xetnghiem={0}-->insert into KcbKetquaCl",
                                                            ma_xetnghiem));
                                                    kq = new KcbKetquaCl();
                                                    kq.IsNew = true;
                                                }
                                                else
                                                {
                                                    MyLog.Trace(
                                                        string.Format("ma_xetnghiem={0}-->updated from KcbKetquaCl",
                                                            ma_xetnghiem));
                                                    kq.IsNew = false;
                                                    kq.MarkOld();
                                                }
                                                DmucDichvuclsChitiet objDvuchitiet =
                                                    DmucDichvuclsChitiet.FetchByID(
                                                        Utility.Int64Dbnull(arrCt[0]["id_chitietdichvu"]));
                                                if (objDvuchitiet != null)
                                                {
                                                    kq.TenThongso = objDvuchitiet.TenChitietdichvu;
                                                }
                                                kq.IdChidinh = Utility.Int64Dbnull(arrCt[0]["id_chidinh"]);
                                                kq.IdChitietchidinh = Utility.Int64Dbnull(arrCt[0]["id_chitietchidinh"]);
                                                kq.IdDichvu = Utility.Int32Dbnull(arrCt[0]["id_dichvu"]);
                                                kq.IdDichvuchitiet = Utility.Int32Dbnull(arrCt[0]["id_chitietdichvu"]);
                                                kq.IdBenhnhan = Utility.Int64Dbnull(arrCt[0]["id_benhnhan"]);
                                                kq.MaLuotkham = Utility.sDbnull(arrCt[0]["ma_luotkham"]);
                                                kq.MaChidinh = Utility.sDbnull(arrCt[0]["ma_chidinh"]);
                                                kq.MaBenhpham = Utility.sDbnull(arrCt[0]["ma_chidinh"]);
                                                kq.IdLab = -1;
                                                kq.IdChitietLab = -1;
                                                kq.Barcode = "";
                                                kq.SttIn = 0;
                                                kq.KetQua = ketqua;
                                                kq.BtNam = "";
                                                kq.BtNu = "";
                                                kq.TenDonvitinh = "";
                                                kq.TenThongso = "";
                                                kq.TenKq = "";
                                                kq.LoaiKq = 0;
                                                kq.ChophepHienthi = 1;
                                                kq.ChophepIn = 1;
                                                kq.MotaThem = "";
                                                kq.NguoiTao = "WIN_SERVICE";
                                                kq.NgayTao = DateTime.Now;
                                                kq.NguoiSua = "WIN_SERVICE";
                                                kq.NgaySua = DateTime.Now;
                                                kq.TrangThai = 4;
                                                kq.NgayXacnhan = DateTime.Now;
                                                kq.NguoiXacnhan = "WIN_SERVICE";
                                                kq.NguoiXacnhan = "WIN_SERVICE";
                                                kq.Save();
                                                //lstKq.Add(kq);
                                            }
                                            else
                                            {
                                                isOK = false;
                                                MyLog.Error(
                                                    string.Format(
                                                        "No data found with ma_xetnghiem={0}.Please check again",
                                                        ma_xetnghiem));
                                            }
                                        }
                                    }
                                    //lstKq.SaveAll();
                                    isOK = true;
                                }
                                scope.Complete();
                            }
                        }
                        else
                        {
                            isOK = false;
                            MyLog.Error(string.Format("Could not get Data from HIS with PID {0} and AssignCode {1}",
                                patientinfor.FirstOrDefault().Split('|')[3], string.Join(",", lstmachidinh.ToArray())));
                        }
                    }
                    else //Invalid file structure
                    {
                        isOK = false;
                        MyLog.Error(
                            string.Format(
                                "FileFormat is not valid(No Patient or Order tags found in this file). Pls check result file again!"));
                    }
                    if (isOK)
                    {
                        string newDestinationFolder =
                            Utility.FixedFolder(Utility.Laygiatrithamsohethong("ASTM_BACKUP_FOLDER",
                                @"\\192.168.1.254\Backup", false));
                        newDestinationFolder += Path.GetFileName(fullpath);
                        string errMsg = Utility.MoveFile(fullpath, newDestinationFolder);
                        if (Utility.DoTrim(errMsg) == "")
                            MyLog.Trace(string.Format("Move file from {0} to {1} succedded", fullpath,
                                newDestinationFolder));
                        else
                            MyLog.Error(string.Format("Move file from {0} to {1} error-->{2}", fullpath,
                                newDestinationFolder, errMsg));
                    }
                    MyLog.Trace(string.Format("------------------Finish Analysing file {0}------------------", fullpath));
                }
            }
            catch (Exception ex)
            {
                MyLog.Error(string.Format("StartAnalysisFile.Exception-->{0}", ex.Message));
            }
        }

        public override void OnRenamed(object source, RenamedEventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}