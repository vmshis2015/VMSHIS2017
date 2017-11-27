﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
using SubSonic;
using VNS.HIS.DAL;
using System.Data.OleDb;
using System.Transactions;
using Janus.Windows.GridEX;
using System.IO;
using Excel;
namespace VNS.HIS.UI.Forms.DUOC
{
    public partial class frm_IE_Excel : Form
    {
        public bool m_blnCancel = true;
        public frm_IE_Excel()
        {
            InitializeComponent();
            InitEvents();
        }
        void InitEvents()
        {
            this.Load += new EventHandler(frm_IE_Excel_Load);
            this.KeyDown += new KeyEventHandler(frm_IE_Excel_KeyDown);
            cmdExport.Click += new EventHandler(cmdExport_Click);
            cmdImport.Click += new EventHandler(cmdImport_Click);
            cmdLoadExcel.Click += new EventHandler(cmdLoadExcel_Click);
        }

        void frm_IE_Excel_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        void frm_IE_Excel_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable m_dtObjectType = new Select().From(DmucDoituongkcb.Schema).OrderAsc(DmucDoituongkcb.Columns.SttHthi).ExecuteDataSet().Tables[0];
                grdObjectTypeList.DataSource = m_dtObjectType;
                Try2LoadHelp();
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi khởi tạo chức năng", ex);

            }
        }
        void Try2LoadHelp()
        {
            try
            {
                string filehelp = Application.StartupPath + @"\Help\Nhap_xuat_exel_thuoc.rtf";
                if (File.Exists(filehelp))
                {
                    lblNofile.SendToBack();
                    txtHelp.LoadFile(filehelp);
                }
                else
                {
                    lblNofile.BringToFront();
                    Utility.SetMsg(lblNofile, "Không tìm thấy file hướng dẫn tại địa chỉ " + filehelp, true);
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi khởi tạo chức năng", ex);

            }
        }
        void cmdLoadExcel_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog _OpenFileDialog = new OpenFileDialog();
                if (_OpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    LoadExcel(_OpenFileDialog.FileName);
                    //LoadDataFromFileExcelToGrid(_OpenFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi mở file Excel", ex);
            } 
        }
        void LoadExcel(string filePath)
        {
             IExcelDataReader excelReader = null;
             try
             {
                 FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);

                 //1. Reading from a binary Excel file ('97-2003 format; *.xls)


                 if (optOffice2003.Checked) excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                 else excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                 //...
                 //2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
                 //IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                 //...
                 //3. DataSet - The result of each spreadsheet will be created in the result.Tables
                 excelReader.IsFirstRowAsColumnNames = true;
                 DataSet ods = excelReader.AsDataSet();
                 if (ods != null && ods.Tables.Count > 0 && ods.Tables[0].Rows.Count > 0)
                 {
                     if (!CheckColumn(ods.Tables[0]))
                     {

                         Utility.ShowMsg("File excel của bạn không đúng định dạng. Nhấn OK để hệ thống sinh file mẫu và mời bạn đọc lại hướng dẫn nạp dữ liệu từ file Excel");
                         Save2Excel(false);
                         tabControl1.SelectedTab = tabControl1.TabPages[1];
                         return;
                     }
                 }
                 else
                 {
                     Utility.ShowMsg("Không có dữ liệu danh mục thuốc từ file Excel");
                     return;
                 }
                 if (!ods.Tables[0].Columns.Contains("Error")) ods.Tables[0].Columns.Add(new DataColumn("Error", typeof(int)));
                 if (!ods.Tables[0].Columns.Contains("Success")) ods.Tables[0].Columns.Add(new DataColumn("Success", typeof(int)));
                 foreach (DataRow dr in ods.Tables[0].Rows)
                 {
                     dr["Error"] = 0;
                     dr["Success"] = 1;
                 }
                 Utility.SetDataSourceForDataGridEx(grdList, ods.Tables[0], true, true, "Success=1", "ten_thuoc");
                 grdList.CheckAllRecords();
                 //...
                 ////4. DataSet - Create column names from first row
                 //excelReader.IsFirstRowAsColumnNames = true;
                 //DataSet result = excelReader.AsDataSet();

                 ////5. Data Reader methods
                 //while (excelReader.Read())
                 //{
                 //    //excelReader.GetInt32(0);
                 //}

                 //6. Free resources (IExcelDataReader is IDisposable)
             }
             catch (Exception ex)
             {
                 Utility.CatchException("Lỗi khi nạp danh mục thuốc từ file Excel", ex);
             }
            finally
            {
                excelReader.Close();
            }
        }
        private void LoadDataFromFileExcelToGrid(string Path)
        {
             Utility.SetDataSourceForDataGridEx(grdList, null, true, true, "1=1", "ten_thuoc");
            OleDbConnection odbConnection = null;
            if (optOffice2003.Checked) odbConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Path + ";" + "Extended Properties=Excel 8.0;");
            else
                odbConnection = new OleDbConnection("provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Path + ";Extended Properties=Excel 12.0;");
            string sheetName = "";
            try
            {
                odbConnection.Open();
                DataTable dt = odbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                sheetName = dt.Rows[1]["TABLE_NAME"].ToString();
                OleDbDataAdapter oldbAdapter = new OleDbDataAdapter("Select * from ["+sheetName+"$] ", odbConnection);
                DataSet ods = new DataSet();
                oldbAdapter.Fill(ods);
                if (ods != null && ods.Tables.Count > 0 && ods.Tables[0].Rows.Count > 0)
                {
                    if (!CheckColumn(ods.Tables[0]))
                    {

                        Utility.ShowMsg("File excel của bạn không đúng định dạng. Nhấn OK để hệ thống sinh file mẫu và mời bạn đọc lại hướng dẫn nạp dữ liệu từ file Excel");
                        Save2Excel(false);
                        tabControl1.SelectedTab = tabControl1.TabPages[1];
                        return;
                    }
                }
                else
                {
                    Utility.ShowMsg("Không có dữ liệu danh mục thuốc từ file Excel");
                    return;
                }
                if (!ods.Tables[0].Columns.Contains("Error")) ods.Tables[0].Columns.Add(new DataColumn("Error", typeof(int)));
                if (!ods.Tables[0].Columns.Contains("Success")) ods.Tables[0].Columns.Add(new DataColumn("Success", typeof(int)));
                foreach (DataRow dr in ods.Tables[0].Rows)
                {
                    dr["Error"] = 0;
                    dr["Success"] = 1;
                }
                Utility.SetDataSourceForDataGridEx(grdList, ods.Tables[0], true, true, "Success=1", "ten_thuoc");
                grdList.CheckAllRecords();
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi nạp danh mục thuốc từ file Excel",ex);
            }
            finally
            {
                odbConnection.Close();
            }
        }
        bool CheckColumn(DataTable dt)
        {
            return dt.Columns.Contains(DmucThuoc.Columns.DangBaoche)
                && dt.Columns.Contains(DmucThuoc.Columns.DonGia)
                && dt.Columns.Contains(DmucThuoc.Columns.GiaBhyt)
                && dt.Columns.Contains(DmucThuoc.Columns.HamLuong)
                && dt.Columns.Contains(DmucThuoc.Columns.HangSanxuat)
                && dt.Columns.Contains(DmucThuoc.Columns.HoatChat)
                && dt.Columns.Contains(DmucThuoc.Columns.IdLoaithuoc)
                && dt.Columns.Contains(DmucThuoc.Columns.KieuThuocvattu)
                && dt.Columns.Contains(DmucThuoc.Columns.MaDonvitinh)
                && dt.Columns.Contains(DmucThuoc.Columns.MaThuoc)
                && dt.Columns.Contains(DmucThuoc.Columns.MotaThem)
                && dt.Columns.Contains(DmucThuoc.Columns.NoitruNgoaitru)
                && dt.Columns.Contains(DmucThuoc.Columns.NuocSanxuat)
                && dt.Columns.Contains(DmucThuoc.Columns.PhuthuDungtuyen)
                && dt.Columns.Contains(DmucThuoc.Columns.PhuthuTraituyen)
                && dt.Columns.Contains(DmucThuoc.Columns.QD31)
                && dt.Columns.Contains(DmucThuoc.Columns.SoDangky)
                && dt.Columns.Contains(DmucThuoc.Columns.TenBhyt)
                && dt.Columns.Contains(DmucThuoc.Columns.TenThuoc)
                && dt.Columns.Contains(DmucThuoc.Columns.TinhChat)
                && dt.Columns.Contains(DmucThuoc.Columns.TrangThai)
                && dt.Columns.Contains(DmucThuoc.Columns.TuTuc)
                ;

        }
        void cmdImport_Click(object sender, EventArgs e)
        {
            if (grdList.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn cần chọn ít nhất một thuốc để thực hiện đẩy vào hệ thống");
                return;
            }
            if (Utility.AcceptQuestion("Bạn có chắc chắn muốn thay thế dữ liệu thuốc trong hệ thống bằng dữ liệu từ file excel vừa nạp hay không?\nChú ý: Nếu bạn đồng ý thực hiện, toàn bộ dữ liệu liên quan đến thuốc sẽ bị xóa để khởi tạo lại", "Cảnh báo", true))
                if (Utility.AcceptQuestion("Toàn bộ dữ liệu biến động thuốc(Nhập-xuất-tồn) sẽ bị xóa để khởi tạo lại. Bạn có chắc chắn?", "Cảnh báo", true))
                    if (Utility.AcceptQuestion("Toàn bộ dữ liệu kê đơn thuốc, cấp phát thuốc sẽ bị xóa để khởi tạo lại. Bạn có chắc chắn", "Cảnh báo", true))
                        ImportFromExcel(chkQuanhe.Checked);
        }
         void ImportFromExcel(bool taoquanhe)
        {
            bool hasError = false;
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        
                        //new Delete().From(DmucThuoc.Schema).Execute();
                        //if (taoquanhe)
                        //{
                        //    List<int> lstIdDoituongKCB = (from p in grdObjectTypeList.GetCheckedRows()
                        //                                  select Utility.Int32Dbnull(p.Cells[DmucDoituongkcb.Columns.IdDoituongKcb].Value, 0)
                        //                                     ).ToList<int>();
                        //    new Delete().From(QheDoituongThuoc.Schema).Where(QheDoituongThuoc.Columns.IdDoituongKcb).In(lstIdDoituongKCB).Execute();
                        //}
                        SPs.ResetDuocAll().Execute();
                        int idx = 0;
                        List<string> lstNoitruNgoaitru = new List<string>() { "ALL", "NOI", "NGOAI" };
                        List<string> lstThuoc_VT = new List<string>() { "THUOC", "VT" };
                        progressBar1.Visible = true;
                        lblCount.Visible = true;
                        progressBar1.Minimum = 0;
                        progressBar1.Maximum = grdList.GetCheckedRows().Length;
                        progressBar1.Value = 0;
                        foreach (GridEXRow row in grdList.GetCheckedRows())
                        {
                            idx++;
                            if (progressBar1.Value + 1 <= progressBar1.Maximum) progressBar1.Value += 1;
                            lblCount.Text = progressBar1.Value.ToString() + " / " + progressBar1.Maximum.ToString();

                            try
                            {
                                DmucThuoc _newDmucThuoc = new DmucThuoc();
                                _newDmucThuoc.DangBaoche = Utility.sDbnull(row.Cells[DmucThuoc.Columns.DangBaoche].Value, "");
                                _newDmucThuoc.DonGia = Utility.DecimaltoDbnull(row.Cells[DmucThuoc.Columns.DonGia].Value, 0);
                                _newDmucThuoc.GiaBhyt = Utility.DecimaltoDbnull(row.Cells[DmucThuoc.Columns.GiaBhyt].Value, 0);
                                _newDmucThuoc.HamLuong = Utility.sDbnull(row.Cells[DmucThuoc.Columns.HamLuong].Value, "");
                                _newDmucThuoc.HangSanxuat = Utility.sDbnull(row.Cells[DmucThuoc.Columns.HangSanxuat].Value, "");
                                _newDmucThuoc.HoatChat = Utility.sDbnull(row.Cells[DmucThuoc.Columns.HoatChat].Value, "");
                                _newDmucThuoc.IdLoaithuoc = Utility.Int16Dbnull(row.Cells[DmucThuoc.Columns.IdLoaithuoc].Value, -1);
                                _newDmucThuoc.IdThuoc = -1;
                                string KieuThuocvattu = Utility.DoTrim(Utility.sDbnull(row.Cells[DmucThuoc.Columns.KieuThuocvattu].Value, "THUOC"));
                                _newDmucThuoc.KieuThuocvattu = KieuThuocvattu == "" || !lstThuoc_VT.Contains(KieuThuocvattu) ? "THUOC" : KieuThuocvattu;
                                _newDmucThuoc.MaDonvitinh = Utility.sDbnull(row.Cells[DmucThuoc.Columns.MaDonvitinh].Value, "");
                                string ma_thuoc = Utility.DoTrim(Utility.sDbnull(row.Cells[DmucThuoc.Columns.MaThuoc].Value, ""));
                                _newDmucThuoc.MaThuoc = ma_thuoc == "" ? "T" + idx.ToString() : ma_thuoc;
                                _newDmucThuoc.MotaThem = Utility.sDbnull(row.Cells[DmucThuoc.Columns.MotaThem].Value, "");
                                string NoitruNgoaitru = Utility.DoTrim(Utility.sDbnull(row.Cells[DmucThuoc.Columns.NoitruNgoaitru].Value, ""));
                                _newDmucThuoc.NoitruNgoaitru = NoitruNgoaitru == "" || !lstNoitruNgoaitru.Contains(NoitruNgoaitru) ? "ALL" : NoitruNgoaitru;
                                _newDmucThuoc.NuocSanxuat = Utility.sDbnull(row.Cells[DmucThuoc.Columns.NuocSanxuat].Value, "");
                                _newDmucThuoc.PhuthuDungtuyen = Utility.DecimaltoDbnull(row.Cells[DmucThuoc.Columns.PhuthuDungtuyen].Value, 0);
                                _newDmucThuoc.PhuthuTraituyen = Utility.DecimaltoDbnull(row.Cells[DmucThuoc.Columns.PhuthuTraituyen].Value, 0);
                                _newDmucThuoc.QD31 = Utility.sDbnull(row.Cells[DmucThuoc.Columns.QD31].Value, "");
                                _newDmucThuoc.SoDangky = Utility.sDbnull(row.Cells[DmucThuoc.Columns.SoDangky].Value, "");
                                _newDmucThuoc.TenBhyt = Utility.sDbnull(row.Cells[DmucThuoc.Columns.TenBhyt].Value, "");
                                _newDmucThuoc.TenThuoc = Utility.sDbnull(row.Cells[DmucThuoc.Columns.TenThuoc].Value, "");
                                _newDmucThuoc.TinhChat = Utility.ByteDbnull(row.Cells[DmucThuoc.Columns.TinhChat].Value, 0);
                                _newDmucThuoc.TrangThai = 1;
                                _newDmucThuoc.TuTuc = Utility.ByteDbnull(row.Cells[DmucThuoc.Columns.TuTuc].Value, 0);

                                _newDmucThuoc.IsNew = true;
                                _newDmucThuoc.Save();
                                if (taoquanhe)
                                {
                                    foreach (GridEXRow rowdoituong in grdObjectTypeList.GetCheckedRows())
                                    {
                                        DmucDoituongkcb _DmucDoituongkcb = DmucDoituongkcb.FetchByID(Utility.Int32Dbnull(rowdoituong.Cells[DmucDoituongkcb.Columns.IdDoituongKcb].Value, -1));
                                        QheDoituongThuoc _QheDoituongThuoc = new QheDoituongThuoc();

                                        _QheDoituongThuoc.IdDoituongKcb = _DmucDoituongkcb.IdDoituongKcb;
                                        _QheDoituongThuoc.IdLoaithuoc = _newDmucThuoc.IdLoaithuoc;
                                        _QheDoituongThuoc.IdThuoc = _newDmucThuoc.IdThuoc;
                                        _QheDoituongThuoc.TyleGiamgia = 0;
                                        _QheDoituongThuoc.KieuGiamgia = "%";
                                        _QheDoituongThuoc.DonGia = (THU_VIEN_CHUNG.IsBaoHiem(_DmucDoituongkcb.IdLoaidoituongKcb) ? Utility.DecimaltoDbnull(_newDmucThuoc.GiaBhyt, 0) : Utility.DecimaltoDbnull(_newDmucThuoc.DonGia, 0));
                                        _QheDoituongThuoc.PhuthuDungtuyen = (THU_VIEN_CHUNG.IsBaoHiem(_DmucDoituongkcb.IdLoaidoituongKcb) ? Utility.DecimaltoDbnull(_newDmucThuoc.PhuthuDungtuyen, 0) : 0m);
                                        _QheDoituongThuoc.PhuthuTraituyen = (THU_VIEN_CHUNG.IsBaoHiem(_DmucDoituongkcb.IdLoaidoituongKcb) ? Utility.DecimaltoDbnull(_newDmucThuoc.PhuthuTraituyen, 0) : 0m);
                                        _QheDoituongThuoc.IdLoaidoituongKcb = _DmucDoituongkcb.IdLoaidoituongKcb;

                                        _QheDoituongThuoc.MaDoituongKcb = _DmucDoituongkcb.MaDoituongKcb;
                                        _QheDoituongThuoc.MaKhoaThuchien = globalVariables.MA_KHOA_THIEN;
                                        _QheDoituongThuoc.NgayTao = globalVariables.SysDate;
                                        _QheDoituongThuoc.NguoiTao = globalVariables.UserName;
                                        _QheDoituongThuoc.IsNew = true;
                                        _QheDoituongThuoc.Save();
                                    }
                                }

                            }
                            catch
                            {
                                hasError = true;
                                row.BeginEdit();
                                row.Cells["Error"].Value = 1;
                                row.Cells["Success"].Value = 0;
                                row.EndEdit();
                            }
                            finally
                            {
                                Application.DoEvents();
                            }
                        }


                    }
                    if (hasError)
                    {
                        if (Utility.AcceptQuestion("Có lỗi trong quá trình đẩy dữ liệu thuốc từ file excel vào hệ thống. Bạn có muốn chấp nhận các dữ liệu đã đẩy thành công hay không?\nChú ý: Với các dữ liệu lỗi bạn có thể liên hệ để được trợ giúp để khắc phục"))
                        {
                            Scope.Complete();
                            m_blnCancel = false;
                        }
                    }
                    else
                    {
                        Scope.Complete();
                        Utility.ShowMsg("Đã nhập liệu thành công. Nhấn OK để kết thúc");
                        m_blnCancel = false;
                    }

                }
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi xuất thuốc ra file Excel", ex);

            }
            finally
            {
                progressBar1.Visible = false;
                lblCount.Visible = false;
            }
        }
        void cmdExport_Click(object sender, EventArgs e)
        {
            Save2Excel(true);
        }
        void Save2Excel(bool allData)
        {
            try
            {
                SaveFileDialog _SaveFileDialog = new SaveFileDialog();
                if (_SaveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    DataTable dtdata = SPs.DmucLaydanhmucthuocXuatexcel(allData?1:-1).GetDataSet().Tables[0];
                    ExportExcel.exportToExcel(dtdata, _SaveFileDialog.FileName, "Danhmuc_thuoc");
                    Utility.OpenProcess(_SaveFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi xuất thuốc ra file Excel", ex);
            }
        }
    }
}
