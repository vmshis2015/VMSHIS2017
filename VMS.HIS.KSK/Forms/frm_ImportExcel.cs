using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using Microsoft.Office.Interop.Excel;
using NLog;
using SubSonic;
using VMS.HIS.HLC.ASTM;
using VMS.HIS.KSK.Classess;
using VNS.HIS.BusRule.Classes;
using VNS.HIS.DAL;

using VNS.Libs;
using VNS.Properties;
using Application = Microsoft.Office.Interop.Excel.Application;
using DataTable = System.Data.DataTable;
using Font = System.Drawing.Font;
using SortOrder = Janus.Windows.GridEX.SortOrder;

namespace VMS.HIS.KSK.Forms
{
    public partial class frm_ImportExcel : Form
    {
        public delegate void AddLog(string logText, Color sActionColor);

        private const string SNewline = "\r\n";
        private static Application _oXl;
        private static Workbook _mWorkBook;
        private static Sheets _mWorkSheets;
        private static Worksheet _mWSheet1;
        private readonly KCB_CHIDINH_CANLAMSANG _chidinhCanlamsang = new KCB_CHIDINH_CANLAMSANG();
        private readonly Logger log = LogManager.GetCurrentClassLogger();
        private bool _allowTextChanged;
        public bool AutoAddAfterCheck = false;
        private bool AutoLoad;
        private KSKProperties HisKSKProperties;
        private string _maDtuong = "DV";
        public action MEnAction = action.Insert;
        private Int16 _IdDoituongKcb = 1;
        private string _MaDoituongKcb = "DV";
        private BussinessKSK _bussinessImportExcel = new BussinessKSK();
        private DataTable _dtPhongKhamExcel;
        private DataTable _dtServiceExxcel;
        private DataTable _mDtChitietPhieuCls = new DataTable();
        private DataTable _mDtPhongkham = new DataTable();
        private string _rowFilter = "1=1";
        private int _value;
        private DataTable dtDmucKhachHang = new DataTable();
        private DataTable dtKSK_CSDL = new DataTable();
        private DataTable dt_Excel = new DataTable();
        private DataTable m_PhongKham = new DataTable();
        private DataTable m_dtDangkyPhongkham = new DataTable();
        public DataTable m_dtDanhsachDichvuCLS = new DataTable();
        public DataTable m_dtDanhsachDichvuCLS_org = new DataTable();
        private DataTable m_dtDanhsachDichvuKCB = new DataTable();
        private DataTable m_dtqheCamchidinhCLSChungphieu = new DataTable();
        public action m_enAction = action.Insert;
        private DataTable m_kieuKham;
        private Application myExcelApplication;
        private Worksheet myExcelWorkSheet;
        private Workbook myExcelWorkbook;
        private string nhomchidinh = "";
        private KcbDanhsachBenhnhan objBenhnhan;
        private KcbLuotkham objLuotkham;
        private KcbChidinhcl objphieuchidinh;
        private string rowFilter = "1=1";
        private int rowNumber = 1; // define first row number to enter data in excel
        private int v_AssignId = -1;

        public frm_ImportExcel()
        {
            // HisKSKProperties = hisKskProperties;
            InitializeComponent();
        }

        public int Rownumber
        {
            get { return rowNumber; }
            set { rowNumber = value; }
        }

        private void cmdConfig_Click(object sender, EventArgs e)
        {
            var frm = new frm_Properties(PropertyLib._KCBProperties);
            frm.ShowDialog();
            //  CauHinhKCB();
        }

        private DataTable ProcessObjects(object[,] valueArray)
        {
            try
            {
                var dt = new DataTable();

                // Get the COLUMN names

                for (int k = 1; k <= valueArray.GetLength(1); k++)
                {
                    dt.Columns.Add((string) valueArray[1, k]); //add columns to the data table.
                }
                // Load Excel SHEET DATA into data table
                var singleDValue = new object[valueArray.GetLength(1)];
                //value array first row contains column names. so loop starts from 2 instead of 1
                for (int i = 2; i <= valueArray.GetLength(0); i++)
                {
                    for (int j = 0; j < valueArray.GetLength(1); j++)
                    {
                        if (valueArray[i, j + 1] != null)
                        {
                            singleDValue[j] = valueArray[i, j + 1].ToString();
                        }
                        else
                        {
                            singleDValue[j] = valueArray[i, j + 1];
                        }
                    }
                    dt.LoadDataRow(singleDValue, LoadOption.PreserveChanges);
                }
                return (dt);
            }
            catch (Exception ex)
            {
                if (globalVariables.IsAdmin)
                    Utility.ShowMsg("Lỗi:" + ex.Message);
                return null;
            }
        }

        private void LoadDataFromFileExcelToGrid(string Path)
        {
            Application excelApp = null;
            Workbook workbook = null;
            var dtaTable = new DataTable();
            try
            {
                excelApp = new Application();
                workbook = excelApp.Workbooks.Open(Path, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                    Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                    Missing.Value, Missing.Value,
                    Missing.Value,
                    Missing.Value, Missing.Value, Missing.Value);

                var ws =
                    (Worksheet) workbook.Sheets.get_Item("Data");
                Range excelRange = ws.UsedRange; //gives the used cells in sheet
                ws = null; // now No need of this so should expire.
                //Reading Excel file.               
                var valueArray = (object[,]) excelRange.get_Value(XlRangeValueDataType.xlRangeValueDefault);
                dt_Excel = ProcessObjects(valueArray);
                Utility.AddColumToDataTable(ref dt_Excel, "chon", typeof (bool));
                Utility.AddColumToDataTable(ref dt_Excel, "_Error", typeof (int));
                Utility.AddColumToDataTable(ref dt_Excel, "id_benhnhan", typeof (int));
                Utility.AddColumToDataTable(ref dt_Excel, "ten_benhnhan", typeof (string));
                Utility.AddColumToDataTable(ref dt_Excel, "ma_luotkham", typeof (string));
                Utility.AddColumToDataTable(ref dt_Excel, "id_chidinh", typeof (int));
                Utility.AddColumToDataTable(ref dt_Excel, "ma_chidinh", typeof (string));
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
            finally
            {
                #region Clean Up

                if (workbook != null)
                {
                    #region Clean Up Close the workbook and release all the memory.

                    workbook.Close(false, Path, Missing.Value);
                    Marshal.ReleaseComObject(workbook);

                    #endregion
                }
                workbook = null;

                if (excelApp != null)
                {
                    excelApp.Quit();
                }
                excelApp = null;

                #endregion
            }
        }

        private void cmdChooseFile_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Excel File (*.xls;*.xlsx)|*.xls;*.xlsx|All Files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var m_chidinhbandau = new DataTable();
                dtKSK_CSDL = new DataTable();
                dt_Excel = new DataTable();
                txtSourceFile.Text = ofd.FileName;
                LoadDataFromFileExcelToGrid(ofd.FileName);
                //LoadChiDinh();
                ////----Lấy chỉ định từ File Excel -----
                //if (!m_dtDanhsachDichvuCLS.Columns.Contains("don_gia"))
                //    m_dtDanhsachDichvuCLS.Columns.Add("don_gia", typeof (decimal));
                //if (!m_dtDanhsachDichvuCLS.Columns.Contains("thanh_tien"))
                //    m_dtDanhsachDichvuCLS.Columns.Add("thanh_tien", typeof (decimal));
                //if (_dtServiceExxcel.Rows.Count > 0)
                //{
                //    if (_dtServiceExxcel.Rows.Count > 0)
                //    {
                //        DataTable chidinh = (from excel in _dtServiceExxcel.AsEnumerable()
                //                             join servicedetail in m_dtDanhsachDichvuCLS.AsEnumerable()
                //                                 on Utility.Int16Dbnull(excel["id_chitietdichvu"]) equals
                //                                 Utility.Int16Dbnull(servicedetail["id_chitietdichvu"])
                //                             select servicedetail).CopyToDataTable();

                //        foreach (DataRow row in chidinh.AsEnumerable())
                //        {
                //            foreach (DataRow row1 in _dtServiceExxcel.AsEnumerable())
                //            {
                //                if (row["id_chitietdichvu"].ToString() == row1["id_chitietdichvu"].ToString())
                //                {
                //                    decimal giacongty = Utility.DecimaltoDbnull(row["don_gia"].ToString());
                //                    row["don_gia"] = giacongty.ToString();
                //                    row["thanh_tien"] = giacongty.ToString();
                //                }
                //            }
                //        }
                //        if (chidinh.Rows.Count > 0)
                //        {
                //            grdChiDinh.DataSource = null;
                //            Utility.SetDataSourceForDataGridEx(grdChiDinh, chidinh, false, true, "", "");
                //        }
                //    }
                //    else
                //    {
                //        Utility.ShowMsg("Không có chỉ định từ File Excel!");
                //        return;
                //    }


                //}

                ////-----Lấy chỉ định phòng khám từ File Excel -------
                //if (!_mDtPhongkham.Columns.Contains("Gia_kham"))
                //    _mDtPhongkham.Columns.Add("Gia_kham", typeof (decimal));
                //if (!_mDtPhongkham.Columns.Contains("Thanhtien"))
                //    _mDtPhongkham.Columns.Add("Thanhtien", typeof (decimal));
                //if (_mDtPhongkham.Rows.Count > 0)
                //{
                //    if (_dtPhongKhamExcel.Rows.Count > 0)
                //    {
                //        DataTable phongkham = (from pkexcel in _dtPhongKhamExcel.AsEnumerable()
                //                               join pk in _mDtPhongkham.AsEnumerable()
                //                                   on Utility.Int16Dbnull(pkexcel["id_khoaphong"]) equals
                //                                   Utility.Int16Dbnull(pk["id_phongkham"])
                //                               select pk).CopyToDataTable();

                //        foreach (DataRow row in phongkham.AsEnumerable())
                //        {
                //            foreach (DataRow row1 in _dtPhongKhamExcel.AsEnumerable())
                //            {
                //                if (row["id_khoaphong"].ToString() == row1["id_khoaphong"].ToString())
                //                {
                //                    decimal giakham = Utility.DecimaltoDbnull(row["Gia_kham"].ToString());
                //                    row["Gia_kham"] = giakham.ToString();
                //                    row["Thanhtien"] = giakham.ToString();
                //                }
                //            }
                //        }
                //        if (phongkham.Rows.Count > 0)
                //        {
                //            grdPhongkham.DataSource = null;
                //            Utility.SetDataSourceForDataGridEx(grdPhongkham, phongkham, false, true, "", "");
                //        }
                //    }
                //}


                //if (string.IsNullOrEmpty(txtSolo.Text))
                //{
                //    Utility.ShowMsg("Chưa có số lô. Đề nghị cập nhật file excel");
                //    m_chidinhbandau.Clear();
                //    m_chidinhbandau.AcceptChanges();
                //    return;
                //}


                dtKSK_CSDL = KSK_LayThongTinBenhNhanTheoSoLo(txtSolo.Text).GetDataSet().Tables[0];
                GhepDuLieuExcel();
                cmdDeletebyLo.Enabled = false;
                grdList.DataSource = dt_Excel;
            }
        }

        private void GhepDuLieuExcel()
        {
            try
            {
                foreach (DataRow drExcel in dt_Excel.Rows)
                {
                    DataRow drCSDL = (from dr in dtKSK_CSDL.AsEnumerable()
                        where
                            Utility.sDbnull(dr.Field<object>("manv")) ==
                            Utility.sDbnull(drExcel["manv"])
                            && Utility.sDbnull(dr.Field<object>("manv")) != ""
                        select dr).FirstOrDefault();
                    if (drCSDL != null)
                    {
                        drExcel["id_benhnhan"] = drCSDL["id_benhnhan"];
                        drExcel["ma_luotkham"] = drCSDL["ma_luotkham"];
                        drExcel["id_chidinh"] = drCSDL["id_chidinh"];
                        drExcel["ma_chidinh"] = drCSDL["ma_chidinh"];
                        if (drCSDL["Barcode"] != null)
                            drExcel["Barcode"] = drCSDL["Barcode"];
                    }
                }
                dt_Excel.AcceptChanges();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        public static StoredProcedure KSK_LayThongTinBenhNhanTheoSoLo(string SoLo)
        {
            var sp = new StoredProcedure("KSK_LayThongTinBenhNhan_TheoSoLo", DataService.GetInstance("ORM"), "dbo");

            sp.Command.AddParameter("@SoLo", SoLo, DbType.String, null, null);

            return sp;
        }

        private void LaydanhsachdangkyKcb()
        {
            m_dtDangkyPhongkham = SPs.SpLaythongtinkhamTheosolo(Utility.sDbnull(txtSolo.Text)).GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdPhongkham, m_dtDangkyPhongkham, false, true, "", "Id_kham desc");
            //if (grdRegExam.RowCount > 0)
            //{
            //    txtTongChiPhiKham.Text = (m_dtDangkyPhongkham.Compute("SUM(" + KcbDangkyKcb.Columns.RegFee +  ")","1=1")
            //        +m_dtDangkyPhongkham.Compute("SUM("  + KcbDangkyKcb.Columns.SurchargePrice + ")","1=1").ToString()).ToString();
            //}
            //else
            //{
            //    txtTongChiPhiKham.Text = "0";
            //}
        }

        private void LayThongTin_Chitiet_CLS()
        {
            _mDtChitietPhieuCls =
                SPs.SpLayThongTinChiDinhTheoSoLo(Utility.sDbnull(txtSolo.Text, "")).GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdChiDinh, _mDtChitietPhieuCls, false, true, "1=1",
                "stt_hthi_dichvu,stt_hthi_chitiet," + DmucDichvuclsChitiet.Columns.TenChitietdichvu);
            grdChiDinh.MoveFirst();
            // ModifyCommand();
        }

        private void frm_ImportExcel_Load(object sender, EventArgs e)
        {
            try
            {
                dtpNgayNhap.Value = globalVariables.SysDate;
                LoadDanhmucchidinh();
                NapThongtinDichvuKcb();
                LaydanhsachdangkyKcb();
                LayThongTin_Chitiet_CLS();
                dtDmucKhachHang = new Select().From(KskDmucKhachhang.Schema).ExecuteDataSet().Tables[0];
                txtDoanhNghiep.Init(dtDmucKhachHang,
                    new List<string>
                    {
                        KskDmucKhachhang.Columns.IdKhachHang,
                        KskDmucKhachhang.Columns.MaKhachHang,
                        KskDmucKhachhang.Columns.TenKhachHang
                    });
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }

        private void LoadChiDinh()
        {
            try
            {
                _dtServiceExxcel = new DataTable();
                _dtPhongKhamExcel = new DataTable();
                if (_dtServiceExxcel.Columns.Count <= 0)
                {
                    _dtServiceExxcel.Columns.Add("id_chitietdichvu", typeof (int));
                    _dtServiceExxcel.Columns.Add("don_gia", typeof (int));
                }
                if (_dtPhongKhamExcel.Columns.Count <= 0)
                {
                    _dtPhongKhamExcel.Columns.Add("id_khoaphong", typeof (int));
                    _dtPhongKhamExcel.Columns.Add("ten_phongkham", typeof (string));
                    _dtPhongKhamExcel.Columns.Add("gia_kham", typeof (decimal));
                }
                string path = txtSourceFile.Text.Trim();
                _oXl = new Application();
                _oXl.Visible = false;
                _oXl.DisplayAlerts = false;
                _mWorkBook = _oXl.Workbooks.Open(path, 0, false, 5, "", "", false, XlPlatform.xlWindows, "", true, false,
                    0, true, false, false);

                //Get all the sheets in the workbook
                _mWorkSheets = _mWorkBook.Worksheets;
                //Get the allready exists sheet
                _mWSheet1 = (Worksheet) _mWorkSheets.Item["chidinh"];

                int rowIndex = 2;
                while (Utility.Int16Dbnull((_mWSheet1.Cells[rowIndex, 1] as Range).Value) > 0)
                {
                    int serviceDetailID = Utility.Int16Dbnull((_mWSheet1.Cells[rowIndex, 1] as Range).Value);
                    decimal Price = Utility.DecimaltoDbnull((_mWSheet1.Cells[rowIndex, 3] as Range).Value);
                    string ten_phongkham = Utility.sDbnull((_mWSheet1.Cells[rowIndex, 2] as Range).Value);
                    DataRow row_servicedetail = _dtServiceExxcel.NewRow();
                    row_servicedetail["id_chitietdichvu"] = serviceDetailID.ToString();
                    row_servicedetail["don_gia"] = Price.ToString();

                    _dtServiceExxcel.Rows.Add(row_servicedetail);
                    rowIndex++;
                }
                int rowindexpk = 2;
                while (Utility.Int16Dbnull((_mWSheet1.Cells[rowindexpk, 5] as Range).Value) > 0)
                {
                    int departmentID = Utility.Int16Dbnull((_mWSheet1.Cells[rowindexpk, 5] as Range).Value);
                    decimal giakham = Utility.DecimaltoDbnull((_mWSheet1.Cells[rowindexpk, 7] as Range).Value);
                    string ten_phongkham = Utility.sDbnull((_mWSheet1.Cells[rowindexpk, 6] as Range).Value);
                    DataRow row_phongkham = _dtPhongKhamExcel.NewRow();
                    row_phongkham["id_khoaphong"] = departmentID.ToString();
                    row_phongkham["ten_phongkham"] = ten_phongkham;
                    row_phongkham["gia_kham"] = giakham.ToString();
                    _dtPhongKhamExcel.Rows.Add(row_phongkham);
                    rowindexpk++;
                }
                txtSolo.Text = ExcellCellToString(_mWSheet1.Cells[2, 4]);
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:" + exception.Message);
            }
            finally
            {
                _mWorkBook.Close();
                _oXl.Quit();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }

        private string ExcellCellToString(object objCell)
        {
            try
            {
                var range = objCell as Range;
                return range.Value.ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }


        private string ChuanHoaChuoi(string sValue)
        {
            sValue = sValue.Trim();
            sValue = sValue.Replace("'", "");
            return sValue;
        }

        private KcbDanhsachBenhnhan TaoBenhnhan(GridEXRow row)
        {
            int namsinh = DateTime.Now.Year;
            DateTime ngay_sinh = DateTime.Now;
            objBenhnhan = new KcbDanhsachBenhnhan();
            objBenhnhan.IdBenhnhan = Utility.Int64Dbnull(row.Cells["id_benhnhan"].Value, -1);
            objBenhnhan.TenBenhnhan = Utility.sDbnull(row.Cells["ten_benhnhan"].Value, "");
            objBenhnhan.DiaChi = Utility.sDbnull(row.Cells["DIACHI"].Value, "");
            objBenhnhan.DiachiBhyt = Utility.sDbnull(row.Cells["DIACHI"].Value, "");
            objBenhnhan.DienThoai = Utility.sDbnull(row.Cells["SDT"].Value, "").Trim();
            objBenhnhan.Email = "";
            objBenhnhan.SoTiemchungQg = "";
            objBenhnhan.NguoiLienhe = "";
            objBenhnhan.NgayTao = globalVariables.SysDate;
            objBenhnhan.NguoiTao = globalVariables.UserName;
            objBenhnhan.NguonGoc = "KSK";
            objBenhnhan.Cmt = Utility.sDbnull(row.Cells["CMT"].Value, "");
            objBenhnhan.CoQuan = Utility.sDbnull(row.Cells["CO_QUAN"].Value, "");
            objBenhnhan.NgheNghiep = "";
            objBenhnhan.GioiTinh = Utility.sDbnull(row.Cells["GIOI_TINH"].Value, "").Trim() == "F" ? "Nữ" : "Nam";
            objBenhnhan.IdGioitinh =
                Utility.ByteDbnull(Utility.sDbnull(row.Cells["GIOI_TINH"].Value, "") == "Nam" ? 1 : 0);
            objBenhnhan.KieuBenhnhan = 3;
            //0= Bệnh nhân thường đến khám chữa bệnh;1= Người gửi mẫu kiểm nghiệm cá nhân;2= Tổ chức gửi mẫu kiểm nghiệm;3 = Bệnh nhân khám sức khỏe công ty
            string sNgaySinh = Utility.sDbnull(row.Cells["nam_sinh"].Value, "");
            objBenhnhan.SoLo = Utility.sDbnull(txtSolo.Text.Trim());
            objBenhnhan.MaBenhnhan = Utility.sDbnull(row.Cells["MaNV"].Value, "");
            if (!string.IsNullOrEmpty(sNgaySinh))
            {
                if (sNgaySinh.TrimStart().TrimEnd() == "")
                {
                    namsinh = globalVariables.SysDate.Year;
                    ngay_sinh = new DateTime(namsinh, 1, 1);
                }
                if (sNgaySinh.TrimStart().TrimEnd().Length == 4)
                {
                    namsinh = Utility.Int32Dbnull(sNgaySinh, globalVariables.SysDate.Year);
                    ngay_sinh = new DateTime(namsinh, 1, 1);
                }
                else
                {
                    ngay_sinh = Convert.ToDateTime(sNgaySinh);
                    namsinh = ngay_sinh.Year;
                }
            }
            objBenhnhan.NgaySinh = ngay_sinh;
            objBenhnhan.NamSinh = Utility.Int16Dbnull(namsinh);
            if (m_enAction == action.Insert)
            {
                objBenhnhan.NgayTiepdon = dtpNgayNhap.Value;
                objBenhnhan.NguoiTao = globalVariables.UserName;
                objBenhnhan.IpMaytao = globalVariables.gv_strIPAddress;
                objBenhnhan.TenMaytao = globalVariables.gv_strComputerName;
            }
            if (m_enAction == action.Update)
            {
                objBenhnhan.NgaySua = globalVariables.SysDate;
                objBenhnhan.NguoiSua = globalVariables.UserName;
                objBenhnhan.NgayTiepdon = dtpNgayNhap.Value;

                objBenhnhan.IpMaysua = globalVariables.gv_strIPAddress;
                objBenhnhan.TenMaysua = globalVariables.gv_strComputerName;
            }
            objBenhnhan.DanToc = "01";
            return objBenhnhan;
        }

        private KcbLuotkham TaoLuotkham(GridEXRow row)
        {
            try
            {
                objLuotkham = new KcbLuotkham();
                objLuotkham.MaLuotkham = Utility.sDbnull(row.Cells["ma_luotkham"].Value, "");
                objLuotkham.KieuKham = "KDN";
                objLuotkham.MaKhoaThuchien = globalVariables.MA_KHOA_THIEN;
                objLuotkham.Noitru = 0;
                objLuotkham.IdDoituongKcb = 3;
                objLuotkham.IdLoaidoituongKcb = 2;
                objLuotkham.MaDoituongKcb = "KSK";
                objLuotkham.Locked = 0;
                objLuotkham.HienthiBaocao = 1;
                objLuotkham.TrangthaiCapcuu = 0;
                objLuotkham.IdKhoatiepnhan = globalVariables.idKhoatheoMay;
                objLuotkham.NguoiTao = globalVariables.UserName;
                objLuotkham.NgayTao = globalVariables.SysDate;
                objLuotkham.Cmt = Utility.sDbnull(row.Cells["CMT"].Value, "").Trim();
                objLuotkham.DiaChi = Utility.sDbnull(row.Cells["DIACHI"].Value, "").Trim();
                objLuotkham.CachTao = 0;
                objLuotkham.SoBenhAn = "";
                objLuotkham.Email = "";
                objLuotkham.NoiGioithieu = Utility.sDbnull(txtDoanhNghiep.Text);
                objLuotkham.NhomBenhnhan = "KSK";
                objLuotkham.GiayBhyt = 0;
                objLuotkham.DungTuyen = 0;
                objLuotkham.IdBenhvienDen = -1;
                objLuotkham.TthaiChuyenden = 1;
                objLuotkham.SolanKham = 1;
                objLuotkham.TrieuChung = "";
                objLuotkham.MaLuotkham = Utility.sDbnull(row.Cells["ma_luotkham"].Value, "");
                objLuotkham.IdBenhnhan = Utility.Int64Dbnull(objBenhnhan.IdBenhnhan, -1);
                DmucDoituongkcb objectType = DmucDoituongkcb.FetchByID(objLuotkham.IdDoituongKcb);
                if (objectType != null)
                {
                    objLuotkham.MaDoituongKcb = Utility.sDbnull(objectType.MaDoituongKcb, "");
                }
                if (m_enAction == action.Update)
                {
                    objLuotkham.NgayTiepdon = dtpNgayNhap.Value;
                    objLuotkham.NguoiSua = globalVariables.UserName;
                    objLuotkham.NgaySua = globalVariables.SysDate;
                    objLuotkham.IpMaysua = globalVariables.gv_strIPAddress;
                    objLuotkham.TenMaysua = globalVariables.gv_strComputerName;
                }
                if (m_enAction == action.Add || m_enAction == action.Insert)
                {
                    objLuotkham.NgayTiepdon = dtpNgayNhap.Value;
                    objLuotkham.NguoiTiepdon = globalVariables.UserName;

                    objLuotkham.IpMaytao = globalVariables.gv_strIPAddress;
                    objLuotkham.TenMaytao = globalVariables.gv_strComputerName;
                }
                objLuotkham.PtramBhytGoc = 0; // Utility.DecimaltoDbnull(txtptramDauthe.Text, 0);
                objLuotkham.PtramBhyt = 0;
                // Utility.DecimaltoDbnull(txtPtramBHYT.Text, 0);//chkTraiTuyen.Visible ?Utility.DecimaltoDbnull(txtPtramBHYT.Text, 0):(objLuotkham.DungTuyen == 0 ? 0 : Utility.DecimaltoDbnull(txtPtramBHYT.Text, 0));
                return objLuotkham;
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi tạo thông tin lượt khám", ex);
                return null;
            }
        }

        private KcbChidinhcl Taophieuchidinh(GridEXRow row)
        {
            objphieuchidinh = new KcbChidinhcl();
            objphieuchidinh.IdChidinh = Utility.Int32Dbnull(row.Cells["id_chidinh"].Value.ToString());
            objphieuchidinh.MaChidinh = Utility.sDbnull(row.Cells["ma_chidinh"].Value.ToString());
            objphieuchidinh.IdKham = -1;
            objphieuchidinh.IdBuongGiuong = -1;
            objphieuchidinh.IdDieutri = -1;
            objphieuchidinh.MaLuotkham = objLuotkham.MaLuotkham;
            objphieuchidinh.IdBenhnhan = objLuotkham.IdBenhnhan;
            objphieuchidinh.Barcode = row.Cells["Barcode"].Value.ToString();
            objphieuchidinh.NgayChidinh = dtpNgayNhap.Value;
            objphieuchidinh.TrangthaiThanhtoan = 0;
            objphieuchidinh.NguoiTao = globalVariables.UserName;
            objphieuchidinh.NgayTao = globalVariables.SysDate;
            objphieuchidinh.IpMaytao = globalVariables.gv_strIPAddress;
            objphieuchidinh.IdDoituongKcb = objLuotkham.IdDoituongKcb;
            objphieuchidinh.MaDoituongKcb = objLuotkham.MaDoituongKcb;
            objphieuchidinh.MaKhoaChidinh = objLuotkham.MaKhoaThuchien;
            objphieuchidinh.TrangThai = 0;
            objphieuchidinh.Noitru = 0;
            objphieuchidinh.KieuChidinh = 5;
            return objphieuchidinh;
        }

        private KcbChidinhclsChitiet[] TaoChitietchidinh(GridEXRow row)
        {
            int i = 0;
            foreach (GridEXRow gridExRow in grdChiDinh.GetDataRows())
            {
                if (gridExRow.RowType == RowType.Record) i++;
            }
            int idx = 0;
            var arrAssignDetail = new KcbChidinhclsChitiet[i];
            foreach (GridEXRow gridExRow in grdChiDinh.GetDataRows())
            {
                if (gridExRow.RowType == RowType.Record)
                {
                    arrAssignDetail[idx] = new KcbChidinhclsChitiet();
                    if (Utility.Int64Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value, -1) >
                        0)
                    {
                        arrAssignDetail[idx].IsLoaded = true;
                        arrAssignDetail[idx].IsNew = false;
                        arrAssignDetail[idx].MarkOld();
                        arrAssignDetail[idx].IdChitietchidinh =
                            Utility.Int64Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value, -1);
                    }
                    else
                    {
                        arrAssignDetail[idx].IsNew = true;
                    }
                    arrAssignDetail[idx].IdChidinh = Utility.Int32Dbnull(row.Cells["id_chidinh"], -1);
                    arrAssignDetail[idx].NguoiTao = globalVariables.UserName;
                    arrAssignDetail[idx].IdDichvu =
                        Utility.Int16Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdDichvu].Value, -1);
                    arrAssignDetail[idx].IdChitietdichvu =
                        Utility.Int16Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietdichvu].Value, -1);

                    arrAssignDetail[idx].SoLuong =
                        Utility.Int16Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.SoLuong].Value, 1);
                    arrAssignDetail[idx].DonGia = Utility.DecimaltoDbnull(
                        gridExRow.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0);
                    arrAssignDetail[idx].PhuThu =
                        Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.PhuThu].Value, 0);
                    arrAssignDetail[idx].HienthiBaocao = 0;
                    arrAssignDetail[idx].TrangthaiBhyt = 0;
                    arrAssignDetail[idx].TrangthaiHuy = 0;
                    arrAssignDetail[idx].IdLoaichidinh = 0;
                    arrAssignDetail[idx].LoaiChietkhau = 0;
                    arrAssignDetail[idx].TrangThai = 0;
                    arrAssignDetail[idx].TrangthaiThanhtoan =
                        Utility.ByteDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan].Value, 0);
                    arrAssignDetail[idx].NgayTao = globalVariables.SysDate;
                    arrAssignDetail[idx].TuTuc =
                        Utility.ByteDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.TuTuc].Value, 0);
                    arrAssignDetail[idx].MadoituongGia =
                        Utility.sDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.MadoituongGia].Value,
                            objLuotkham.MaDoituongKcb);
                    arrAssignDetail[idx].IdThanhtoan =
                        Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdThanhtoan].Value, -1);
                    arrAssignDetail[idx].PtramBhyt = 0;
                    arrAssignDetail[idx].PtramBhytGoc = 0;
                    arrAssignDetail[idx].BhytChitra = 0;
                    arrAssignDetail[idx].BnhanChitra =
                        Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0);
                    arrAssignDetail[idx].IpMaysua = globalVariables.gv_strIPAddress;
                    arrAssignDetail[idx].TenMaysua = globalVariables.gv_strComputerName;

                    arrAssignDetail[idx].IpMaytao = globalVariables.gv_strIPAddress;
                    arrAssignDetail[idx].TenMaytao = globalVariables.gv_strComputerName;
                    idx++;
                }
            }
            return arrAssignDetail;
        }

        private bool CheckValiData()
        {
            bool bRet = true;
            try
            {
                int count = 0;
                List<string> lstBarCode =
                    grdList.GetCheckedRows()
                        .Select(e => e.Cells[KcbChidinhcl.Columns.Barcode].Value.ToString())
                        .ToList();
                string sBarcode = "";
                int iRow = 1;
                if (grdList.RowCount == 0)
                {
                    Utility.ShowMsg("Không có thông tin bệnh nhân", "Thông báo");
                    tabKSK.SelectedIndex = 0;
                    txtSourceFile.Focus();
                    bRet = false;
                }
                else if (grdChiDinh.RowCount == 0)
                {
                    Utility.ShowMsg("Không có thông tin chỉ định. Kiểm tra lại");
                    tabKSK.SelectedIndex = 1;
                    bRet = false;
                }
                if (grdList.GetCheckedRows().Length == 0)
                {
                    Utility.ShowMsg("Bạn cần chọn các bệnh nhân đẩy vào hệ thống", "Thông báo");
                    tabKSK.SelectedIndex = 0;
                    grdList.Focus();
                    bRet = false;
                }
                SqlQuery solo =
                    new Select().From(KcbDanhsachBenhnhan.Schema)
                        .Where(KcbDanhsachBenhnhan.Columns.SoLo)
                        .IsEqualTo(txtSolo.Text.Trim());
                if (txtSolo.Text.Trim() == "")
                {
                    Utility.ShowMsg("Bạn cần chọn các bệnh nhân đẩy vào hệ thống", "Thông báo");
                    tabKSK.SelectedIndex = 0;
                    grdList.Focus();
                    bRet = false;
                }
            }
            catch (Exception)
            {
                bRet = false;
            }

            return bRet;
        }

        private KcbDangkyKcb TaoDangkyKCB()
        {
            var objRegExam = new KcbDangkyKcb();
            DmucDichvukcb objDichvuKcb =
                DmucDichvukcb.FetchByID(Utility.Int32Dbnull(cboKieuKham.Value, -1));
            var objdepartment =
                new Select().From(DmucKhoaphong.Schema)
                    .Where(DmucKhoaphong.IdKhoaphongColumn)
                    .IsEqualTo(Utility.Int32Dbnull(txtIDPkham, -1))
                    .ExecuteSingle<DmucKhoaphong>();
            if (objDichvuKcb != null)
            {
                objRegExam.IdDichvuKcb = Utility.Int16Dbnull(objDichvuKcb.IdDichvukcb, -1);
                objRegExam.IdKieukham = 10;
                objRegExam.NhomBaocao = "KSK";
                objRegExam.DonGia = Utility.DecimaltoDbnull(objDichvuKcb.DonGia, 0);
                objRegExam.MadoituongGia = "KSK";
                objRegExam.NguoiTao = globalVariables.UserName;
                objRegExam.LaPhidichvukemtheo = 0;
                objRegExam.SttKham = -1;
                objRegExam.SttKham = THU_VIEN_CHUNG.LaySothutuKCB(Utility.Int32Dbnull(objDichvuKcb.IdPhongkham, -1));
                objRegExam.IdCha = -1;
                if (objdepartment != null)
                {
                    objRegExam.IdKhoakcb = objdepartment.IdKhoaphong;
                    objRegExam.MaPhongStt = objdepartment.MaPhongStt;
                }

                objRegExam.IdLoaidoituongkcb = 3;
                objRegExam.MaDoituongkcb = "KSK";
                objRegExam.IdDoituongkcb = 3;
                if (Utility.Int16Dbnull(objDichvuKcb.IdPhongkham, -1) > -1)
                    objRegExam.IdPhongkham = Utility.Int16Dbnull(objDichvuKcb.IdPhongkham, -1);
                else
                    objRegExam.IdPhongkham = Utility.Int16Dbnull(txtIDPkham.Text, -1);

                objRegExam.PhuThu = 0;

                if (!THU_VIEN_CHUNG.IsBaoHiem(objRegExam.IdLoaidoituongkcb))
                    objRegExam.PhuThu = 0;
                objRegExam.NgayDangky = globalVariables.SysDate;
                objRegExam.IdBenhnhan = Utility.Int64Dbnull(objLuotkham.IdBenhnhan);
                objRegExam.TrangthaiThanhtoan = 0;
                objRegExam.TrangthaiHuy = 0;
                objRegExam.Noitru = 0;
                objRegExam.TrangthaiIn = 0;
                objRegExam.TrangThai = 0;
                objRegExam.IpMaytao = globalVariables.gv_strIPAddress;
                objRegExam.TenMaytao = globalVariables.gv_strComputerName;
                objRegExam.TuTuc = 0;
                objRegExam.MaKhoaThuchien = globalVariables.MA_KHOA_THIEN;
                objRegExam.TenDichvuKcb = objDichvuKcb.TenDichvukcb;
                objRegExam.NgayTiepdon = globalVariables.SysDate;
                objRegExam.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham, "");
                //Bỏ đi do sinh lại ở mục business
                if (THU_VIEN_CHUNG.IsNgoaiGio())
                {
                    objRegExam.KhamNgoaigio = 1;
                    objRegExam.DonGia = Utility.DecimaltoDbnull(objDichvuKcb.DongiaNgoaigio, 0);
                    objRegExam.PhuThu = 0;
                }
                else
                {
                    objRegExam.KhamNgoaigio = 0;
                }
            }
            else
            {
                objRegExam = null;
            }
            //  THU_VIEN_CHUNG.TinhlaiGiaChiphiKCB(m_dtDangkyPhongkham, ref objRegExam);
            return objRegExam;
        }

        public void LogText(string sLogText, Color sActionColor)
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new AddLog(LogText), new object[] {sLogText, sActionColor});
                }
                else
                {
                    AddAction(sLogText, sActionColor);
                    //rtxtLogs.AppendText(sLogText);
                    rtxtLogs.AppendText(SNewline);
                    //TextBoxTraceListener.SendMessage(_richTextBoxLog.Handle, TextBoxTraceListener.WM_VSCROLL, TextBoxTraceListener.SB_BOTTOM, 0);
                }
            }
            catch
            {
            }
        }

        private void AddAction(string sLogText, Color color)
        {
            if (sLogText.Length > 0)
            {
                Color oldColor = rtxtLogs.SelectionColor;
                rtxtLogs.SelectionLength = 0;
                rtxtLogs.SelectionStart = rtxtLogs.Text.Length;
                rtxtLogs.SelectionColor = color;
                rtxtLogs.SelectionFont = new Font(rtxtLogs.SelectionFont, FontStyle.Bold);
                rtxtLogs.AppendText(sLogText);
                rtxtLogs.SelectionColor = oldColor;
            }
        }

        private void cmdImportToHis_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckValiData()) return;
                tabKSK.SelectedIndex = 0;
                string errMsg = "";

                Utility.ResetProgressBar(prgBar, grdList.GetCheckedRows().Count(), true);
                foreach (GridEXRow  row in grdList.GetCheckedRows())
                {
                    if (Utility.sDbnull(row.Cells["ma_luotkham"].Value, "") != "")
                    {
                        LogText(
                            string.Format("{0}. Thông tin bệnh nhân {1} đã có trong hệ thống!", (row.RowIndex + 1),
                                Utility.sDbnull(row.Cells["ma_luotkham"].Value, "")), Color.Red);
                    }
                    else
                    {
                        m_enAction = action.Insert;
                        KcbDanhsachBenhnhan objdanhsachbenhnhan = TaoBenhnhan(row);
                        KcbLuotkham objluotkham = TaoLuotkham(row);
                        KcbChidinhcl objchidinh = Taophieuchidinh(row);
                        KcbChidinhclsChitiet[] objchidinhchitiet = TaoChitietchidinh(row);
                        KcbDangkyKcb objDangkyKcb = TaoDangkyKCB();
                        ActionResult actionResult = BussinessKSK.Thembenhnhan(objdanhsachbenhnhan, objluotkham,
                            objchidinh, objDangkyKcb,
                            objchidinhchitiet,
                            ref errMsg);
                        switch (actionResult)
                        {
                            case ActionResult.Success:
                                if (objchidinh != null)
                                {
                                    row.BeginEdit();
                                    row.Cells["id_chidinh"].Value = Utility.sDbnull(objchidinh.IdChidinh);
                                    row.Cells["ma_chidinh"].Value = Utility.sDbnull(objchidinh.MaChidinh);
                                    row.Cells["ma_luotkham"].Value = Utility.sDbnull(objchidinh.MaLuotkham);
                                    row.Cells["id_benhnhan"].Value = Utility.sDbnull(objchidinh.IdBenhnhan);
                                    //grdList.UpdateData();
                                    row.EndEdit();
                                }
                                LogText(
                                    string.Format(
                                        "{0}. {1}   - - - Có mã lần khám là {2}  - - -   có mã chỉ định là: {3}",
                                        row.RowIndex + 1, errMsg, Utility.sDbnull(objchidinh.MaLuotkham),
                                        Utility.sDbnull(objchidinh.MaChidinh)), Color.DarkBlue);
                                //  Utility.SetMessage(lblMsg, "Bạn import BN thành công", true);
                                //lblMsg.ForeColor = Color.Thistle;
                                break;
                            case ActionResult.Error:
                                LogText(errMsg, Color.Red);
                                //  Utility.ShowMsg("Lỗi trong quá trình thêm mới thông tin", "Thông báo", MessageBoxIcon.Error);
                                break;
                        }
                    }
                    SetValue4Prg(prgBar, 1);
                    System.Windows.Forms.Application.DoEvents();
                    row.IsChecked = false;
                    //grdList.UnCheckAllRecords();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Import dữ liệu không thành công" + ex.Message);
            }
        }

        private void SetValue4Prg(ProgressBar Prg, int _Value)
        {
            try
            {
                if (Prg.InvokeRequired)
                {
                    Prg.Invoke(new SetPrgValue(SetValue4Prg), new object[] {Prg, _Value});
                }
                else
                {
                    if (Prg.Value + _Value <= Prg.Maximum) Prg.Value += _Value;
                    Prg.Refresh();
                    Thread.Sleep(10);
                }
            }
            catch (Exception ex)
            {
                if (globalVariables.IsAdmin)
                    Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }

        private void cmdSentToLis_Click(object sender, EventArgs e)
        {
            try
            {
                var lstIdchidinhchitiet = new List<string>();
                Utility.ResetProgressBar(prgBar, grdList.GetCheckedRows().Count(), true);
                foreach (GridEXRow row in grdList.GetDataRows())
                {
                    int result = new Update(KcbChidinhclsChitiet.Schema)
                        .Set(KcbChidinhclsChitiet.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                        .Set(KcbChidinhclsChitiet.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                        .Set(KcbChidinhclsChitiet.Columns.TrangThai).EqualTo(1)
                        .Where(KcbChidinhclsChitiet.Columns.TrangThai).In(0, null)
                        .And(KcbChidinhclsChitiet.Columns.IdBenhnhan).IsEqualTo(row.Cells["id_benhnhan"].Value)
                        .And(KcbChidinhclsChitiet.Columns.MaLuotkham).IsEqualTo(row.Cells["ma_luotkham"].Value)
                        .And(KcbChidinhclsChitiet.Columns.IdChidinh).IsEqualTo(row.Cells["id_chidinh"].Value)
                        .Execute();
                    log.Trace("");
                    DataSet dsData =
                        SPs.HisLisLaydulieuchuyensangLisKsk(dtpNgayNhap.Value.ToString("dd/MM/yyyy"),
                            Utility.Int64Dbnull(row.Cells["id_benhnhan"].Value),
                            Utility.sDbnull(row.Cells["ma_luotkham"].Value)).
                            GetDataSet();
                    DataTable dt2Lis = dsData.Tables[1].Copy();

                    int recoder = RocheCommunication.WriteOrderMessage(
                        THU_VIEN_CHUNG.Laygiatrithamsohethong("ASTM_ORDERS_FOLDER", @"\\192.168.1.254\Orders", false),
                        dt2Lis);
                    if (recoder == 0) //Thành công
                    {
                        log.Trace(" Thuc hien day thanh cong cho benh nhan co ma luot kham:" +
                                  row.Cells["ma_luotkham"].Value + "voi barcode la :" + row.Cells["Barcode"].Value);
                    }
                    SetValue4Prg(prgBar, 1);
                    lstIdchidinhchitiet = (from p in dsData.Tables[1].AsEnumerable()
                        select
                            Utility.sDbnull(
                                p[KcbChidinhclsChitiet.Columns.IdChitietchidinh], 0)).ToList();
                    SPs.HisLisCapnhatdulieuchuyensangLis(
                        string.Join(",", lstIdchidinhchitiet.ToArray()), 2, 1).Execute();
                }
            }
            catch (Exception ex)
            {
                log.Trace("Thuc hien day khong thanh cong chi dinh cho benh nhan");
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }

        private void _BussinessImportExcel__OnDoing(int value)
        {
            SetValue4Prg(prgBar, 1);
        }

        private void cmdDeletebyLo_Click(object sender, EventArgs e)
        {
            try
            {
                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa dữ liệu lô vừa chọn", "Cảnh báo", true))
                {
                    string eMessage = "";

                    log.Trace("Bắt đầu xóa dữ liệu của lô: " + txtSolo.Text);
                    _bussinessImportExcel = new BussinessKSK();
                    _bussinessImportExcel._OnDoing += _BussinessImportExcel__OnDoing;
                    _value = 0;
                    var lstId = new List<int>();
                    prgBar.Maximum = grdList.GetCheckedRows().Length;
                    prgBar.Value = 0;
                    foreach (GridEXRow row in grdList.GetCheckedRows())
                    {
                        int idBenhnhan = Utility.Int32Dbnull(row.Cells["id_benhnhan"].Value, -1);
                        lstId.Add(idBenhnhan);
                        row.Delete();
                    }
                    bool _reval = _bussinessImportExcel.DeleteData(lstId, true, ref eMessage);
                    if (_reval)
                    {
                        log.Trace("Xóa thành công dữ liệu của lô:" + txtSolo.Text);
                        LogText("Xóa thành công", Color.DarkBlue);
                        THU_VIEN_CHUNG.Log(Name, globalVariables.UserName,
                            string.Format("Thực hiện xóa lô khám sức khỏe có số lô {0}", txtSolo.Text), action.Delete);
                        Utility.ShowMsg("Đã xóa xong dữ liệu. Nhấn OK để kết thúc");
                    }
                    else
                    {
                        LogText("Xóa không thành công", Color.Red);
                    }
                }
            }
            catch (Exception ex)
            {
                LogText("Xóa không thành công", Color.Red);
                Utility.ShowMsg("Lỗi trong quá trình xóa thông tin theo số lô: " + ex.Message);
            }
        }

        private void cmdSeachByLo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSolo.Text.Trim()))
            {
                Utility.ShowMsg("Đề nghị nhập số lô");
                return;
            }
            dt_Excel = KSK_GetPatientListByBatch(txtSolo.Text).GetDataSet().Tables[0];
            grdList.DataSource = dt_Excel;
            LayThongTin_Chitiet_CLS();
            LaydanhsachdangkyKcb();
            grdList.CheckAllRecords();
            cmdDeletebyLo.Enabled = true;
        }

        public static StoredProcedure KSK_GetPatientListByBatch(string SoLo)
        {
            var sp = new StoredProcedure("KSK_LayThongTinBenhNhan_TheoSoLo", DataService.GetInstance("ORM"), "dbo");

            sp.Command.AddParameter("@SoLo", SoLo, DbType.String, null, null);

            return sp;
        }

        private void mnuDelLog_Click(object sender, EventArgs e)
        {
            rtxtLogs.Clear();
        }

        private void cmdThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void grdServiceDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (grdServiceDetail.Focused)
                if (e.KeyCode == Keys.Enter)
                {
                    cmdAddChiDinh.PerformClick();
                    txtFilterName.SelectAll();
                    txtFilterName.Focus();
                }
                else if (e.KeyCode == Keys.Space && grdServiceDetail.CurrentColumn != null &&
                         Utility.sDbnull(grdServiceDetail.CurrentColumn.Key, "") != "colCHON")
                {
                    grdServiceDetail.CurrentRow.IsChecked = !grdServiceDetail.CurrentRow.IsChecked;
                    if (chkChiDinhNhanh.Checked)
                    {
                        if (grdServiceDetail.CurrentRow.IsChecked)
                        {
                            AddOneRow_ServiceDetail();
                        }
                    }
                }
        }

        private void cmdAddChiDinh_Click(object sender, EventArgs e)
        {
            AddOneRow_ServiceDetail();
        }

        private void grdServiceDetail_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
        }

        private void cmdThemMoiKH_Click(object sender, EventArgs e)
        {
            var frm = new FrmDanhMucKhachHang();
            frm.ShowDialog();
            dtDmucKhachHang = new Select().From(KskDmucKhachhang.Schema).ExecuteDataSet().Tables[0];
            txtDoanhNghiep.Init(dtDmucKhachHang,
                new List<string>
                {
                    KskDmucKhachhang.Columns.IdKhachHang,
                    KskDmucKhachhang.Columns.MaKhachHang,
                    KskDmucKhachhang.Columns.TenKhachHang
                });
        }

        #region Nested type: SetPrgValue

        private delegate void SetPrgValue(ProgressBar Prg, int _Value);

        #endregion

        #region Load danh muc từ hệ thống

        private void AddOneRow_ServiceDetail()
        {
            try
            {
                string errMsg = string.Empty;
                string errMsg_temp = string.Empty;
               // GridEXRow gridExRow = grdServiceDetail.CurrentRow;
                   GridEXRow[] arrCheckList = grdServiceDetail.GetCheckedRows();
                foreach (GridEXRow gridExRow in arrCheckList)
                {
                    resetNewItem();
                    Int32 idChitietdichvu =
                        Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietdichvu].Value, -1);
                    EnumerableRowCollection<DataRow> query =
                        from loz in _mDtChitietPhieuCls.AsEnumerable().Cast<DataRow>()
                        where
                            Utility.Int32Dbnull(
                                loz[KcbChidinhclsChitiet.Columns.IdChitietdichvu], -1) ==
                            idChitietdichvu
                        select loz;
                    if (query.Count() <= 0)
                    {
                        DataRow newDr = _mDtChitietPhieuCls.NewRow();
                        newDr[KcbChidinhclsChitiet.Columns.IdChitietchidinh] = -1;
                        newDr["stt_hthi_dichvu"] =
                            Utility.Int32Dbnull(gridExRow.Cells["stt_hthi_dichvu"].Value, -1);
                        newDr["stt_hthi_chitiet"] =
                            Utility.Int32Dbnull(gridExRow.Cells["stt_hthi_chitiet"].Value, -1);
                        newDr[KcbChidinhclsChitiet.Columns.IdChidinh] = v_AssignId;
                        newDr[KcbChidinhclsChitiet.Columns.IdDichvu] =
                            Utility.Int32Dbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.IdDichvu].Value, -1);
                        newDr[KcbChidinhclsChitiet.Columns.IdChitietdichvu] =
                            Utility.Int32Dbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.IdChitietdichvu].Value, -1);
                        newDr[KcbChidinhclsChitiet.Columns.PtramBhyt] = 0;
                        //Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.PtramBhyt].Value, 0);
                        newDr[KcbChidinhclsChitiet.Columns.DonGia] =
                            Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0);
                        newDr[KcbChidinhclsChitiet.Columns.GiaDanhmuc] =
                            Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.GiaDanhmuc].Value, 0);
                        newDr[KcbChidinhclsChitiet.Columns.LoaiChietkhau] =
                            Utility.ByteDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.LoaiChietkhau].Value);
                        newDr[DmucDichvuclsChitiet.Columns.TenChitietdichvu] =
                            Utility.sDbnull(gridExRow.Cells[DmucDichvuclsChitiet.Columns.TenChitietdichvu].Value, "");
                        newDr["IsNew"] = 1;
                        newDr["IsLocked"] = 0;
                        newDr["NoSave"] = 1;
                        newDr[DmucDoituongkcb.Columns.IdDoituongKcb] = 1;
                        newDr[KcbChidinhclsChitiet.Columns.HienthiBaocao] = 1;
                        newDr[KcbChidinhclsChitiet.Columns.SoLuong] =
                            Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.SoLuong].Value, 1);
                        newDr[KcbChidinhclsChitiet.Columns.TuTuc] = 0;
                        newDr[DmucDichvucl.Columns.TenDichvu] =
                            Utility.sDbnull(gridExRow.Cells[DmucDichvucl.Columns.TenDichvu].Value, "");
                        newDr[KcbChidinhclsChitiet.Columns.PhuThu] =
                            Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.PhuThu].Value, 0);
                        errMsg_temp =
                            KiemtraCamchidinhchungphieu(
                                Utility.Int32Dbnull(newDr[KcbChidinhclsChitiet.Columns.IdChitietdichvu], 0),
                                Utility.sDbnull(newDr[DmucDichvuclsChitiet.Columns.TenChitietdichvu], ""));
                        if (errMsg_temp != string.Empty)
                        {
                            errMsg += errMsg_temp;
                        }
                        else
                        {
                            _mDtChitietPhieuCls.Rows.Add(newDr);
                            Utility.GonewRowJanus(grdDanhSachCLS, KcbChidinhclsChitiet.Columns.IdChitietdichvu,
                                Utility.sDbnull(newDr[KcbChidinhclsChitiet.Columns.IdChitietdichvu], "0"));
                            m_dtDanhsachDichvuCLS.AcceptChanges();
                        }
                       
                    }
                }
                if (errMsg != string.Empty)
                {
                    if (errMsg.Contains("Single-Service:"))
                    {
                        Utility.ShowMsg(
                            "Dịch vụ sau được đánh dấu không được phép kê chung với bất kỳ dịch vụ nào. Đề nghị bạn kiểm tra lại:\n" +
                            Utility.DoTrim(errMsg.Replace("Single-Service:", "")));
                    }
                    else
                        Utility.ShowMsg(
                            "Các cặp dịch vụ sau đã được thiết lập chống chỉ định chung phiếu. Đề nghị bạn kiểm tra lại:\n" +
                            errMsg);
                }
                m_dtDanhsachDichvuCLS.AcceptChanges();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }

        private void Get_KIEUKHAM(string MA_DTUONG)
        {
            m_kieuKham = THU_VIEN_CHUNG.Get_KIEUKHAM(MA_DTUONG, -1);
        }

        private void Get_PHONGKHAM(string MA_DTUONG)
        {
            m_PhongKham = THU_VIEN_CHUNG.Get_PHONGKHAM(MA_DTUONG);
        }

        private void NapThongtinDichvuKcb()
        {
            bool oldStatus = _allowTextChanged;
            try
            {
                cboKieuKham.DataSource = null;
                //Khởi tạo danh mục Loại khám
                string objecttype_code = "KSK";
                DmucDoituongkcb objectType = DmucDoituongkcb.FetchByID(3);
                if (objectType != null)
                {
                    objecttype_code = Utility.sDbnull(objectType.MaDoituongKcb);
                }
                _maDtuong = objecttype_code;
                m_dtDanhsachDichvuKCB = THU_VIEN_CHUNG.LayDsach_Dvu_KCB(objecttype_code, "KSK", -1);
                Get_KIEUKHAM(objecttype_code);
                Get_PHONGKHAM(objecttype_code);
                //AutocompleteMaDvu();
                //AutocompletePhongKham();
                //AutocompleteKieuKham();
                m_dtDanhsachDichvuKCB.AcceptChanges();
                cboKieuKham.DataSource = m_dtDanhsachDichvuKCB;
                cboKieuKham.DataMember = DmucDichvukcb.Columns.IdDichvukcb;
                cboKieuKham.ValueMember = DmucDichvukcb.Columns.IdDichvukcb;
                cboKieuKham.DisplayMember = DmucDichvukcb.Columns.TenDichvukcb;
                //cboKieuKham.ValueChanged += new EventHandler(cboKieuKham_ValueChanged);
                //  cboKieuKham.Visible = globalVariables.UserName == "ADMIN";
                if (m_dtDanhsachDichvuKCB == null || m_dtDanhsachDichvuKCB.Columns.Count <= 0) return;
                _allowTextChanged = true;
                if (m_dtDanhsachDichvuKCB.Rows.Count == 1 && MEnAction != action.Update)
                {
                    cboKieuKham.SelectedIndex = 0;
                    DataRow idKieukham = (from s in m_dtDanhsachDichvuKCB.AsEnumerable()
                        select s).FirstOrDefault();
                    if (idKieukham != null)
                    {
                        txtIDPkham.Text = Utility.sDbnull(idKieukham["id_phongkham"]);
                        txtIDKieuKham.Text = Utility.sDbnull(idKieukham["id_kieukham"]);
                    }
                    // txtIDKieuKham.Text = Utility.sDbnull(txtExamtypeCode.MyID);
                    // txtIDKieuKham.Text = Utility.sDbnull(cboKieuKham.Value);
                    // cboKieuKham.Value = txtExamtypeCode.txtMyID;
                    // txtKieuKham.Text = cboKieuKham.Text;
                    AutoLoadKieuKham();
                }
                _allowTextChanged = oldStatus;
            }
            catch
            {
                _allowTextChanged = oldStatus;
            }
        }

        private void AutoLoadKieuKham()
        {
            try
            {
                if (Utility.Int32Dbnull(txtIDKieuKham.Text, -1) == -1 || Utility.Int32Dbnull(txtIDPkham.Text, -1) == -1)
                {
                    cboKieuKham.Text = "CHỌN PHÒNG KHÁM";
                    cboKieuKham.SelectedIndex = -1;
                    return;
                }
                DataRow[] arrDr =
                    m_dtDanhsachDichvuKCB.Select("(ma_doituong_kcb='ALL' OR ma_doituong_kcb='" + _maDtuong +
                                                 "') AND id_kieukham=" +
                                                 txtIDKieuKham.Text.Trim() + " AND  id_phongkham=" +
                                                 txtIDPkham.Text.Trim());
                //nếu ko có đích danh phòng thì lấy dịch vụ bất kỳ theo kiểu khám và đối tượng
                if (arrDr.Length <= 0)
                    arrDr =
                        m_dtDanhsachDichvuKCB.Select("(ma_doituong_kcb='ALL' OR ma_doituong_kcb='" + _maDtuong +
                                                     "') AND id_kieukham=" +
                                                     txtIDKieuKham.Text.Trim() + " AND id_phongkham=-1 ");
                if (arrDr.Length <= 0)
                {
                    cboKieuKham.Text = "CHỌN PHÒNG KHÁM";
                    cboKieuKham.SelectedIndex = -1;
                }
                else
                {
                    cboKieuKham.Text = arrDr[0][DmucDichvukcb.Columns.TenDichvukcb].ToString();
                    txtIDPkham.Text = arrDr[0][DmucDichvukcb.Columns.IdPhongkham].ToString();
                }
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:" + exception.Message);
            }
            finally
            {
                AutoLoad = false;
            }
        }

        private string KiemtraCamchidinhchungphieu(int idDichvuchitiet, string tenChitiet)
        {
            string reval = "";
            string tempt;
            var lstKey = new List<string>();
            string key = "";
            //Kiểm tra dịch vụ đang thêm có phải là dạng Single-Service hay không?
            DataRow[] arrSingle =
                m_dtDanhsachDichvuCLS.Select(DmucDichvuclsChitiet.Columns.SingleService + "=1 AND " +
                                             DmucDichvuclsChitiet.Columns.IdChitietdichvu + "=" + idDichvuchitiet);
            if (arrSingle.Length > 0 &&
                _mDtChitietPhieuCls.Select(KcbChidinhclsChitiet.Columns.IdChitietdichvu + "<>" +
                                           idDichvuchitiet).Length > 0)
            {
                return string.Format("Single-Service: {0}", tenChitiet);
            }
            //Kiểm tra các dịch vụ đã thêm có cái nào là Single-Service hay không?
            List<int> lstID =
                _mDtChitietPhieuCls.AsEnumerable().Select(
                    c => Utility.Int32Dbnull(c[KcbChidinhclsChitiet.Columns.IdChitietdichvu], 0)).Distinct().ToList();
            EnumerableRowCollection<DataRow> q = from p in m_dtDanhsachDichvuCLS.AsEnumerable()
                where
                    Utility.ByteDbnull(p[DmucDichvuclsChitiet.Columns.SingleService], 0) ==
                    1
                    &&
                    lstID.Contains(
                        Utility.Int32Dbnull(
                            p[DmucDichvuclsChitiet.Columns.IdChitietdichvu], 0))
                select p;
            if (q.Any())
            {
                return string.Format("Single-Service: {0}",
                    Utility.sDbnull(q.FirstOrDefault()[DmucDichvuclsChitiet.Columns.TenChitietdichvu],
                        ""));
            }
            //Lấy các cặp cấm chỉ định chung cùng nhau
            DataRow[] arrDr =
                m_dtqheCamchidinhCLSChungphieu.Select(QheCamchidinhChungphieu.Columns.IdDichvu + "=" + idDichvuchitiet);
            DataRow[] arrDr1 =
                m_dtqheCamchidinhCLSChungphieu.Select(QheCamchidinhChungphieu.Columns.IdDichvuCamchidinhchung + "=" +
                                                      idDichvuchitiet);
            foreach (DataRow dr in arrDr)
            {
                DataRow[] arrtemp =
                    _mDtChitietPhieuCls.Select(KcbChidinhclsChitiet.Columns.IdChitietdichvu + "=" +
                                               Utility.sDbnull(
                                                   dr[QheCamchidinhChungphieu.Columns.IdDichvuCamchidinhchung]));
                if (arrtemp.Length > 0)
                {
                    foreach (DataRow dr1 in arrtemp)
                    {
                        tempt = string.Empty;
                        key = idDichvuchitiet + "-" +
                              Utility.sDbnull(dr1[KcbChidinhclsChitiet.Columns.IdChitietdichvu], "");
                        if (!lstKey.Contains(key))
                        {
                            lstKey.Add(key);
                            tempt = string.Format("{0} - {1}", tenChitiet,
                                Utility.sDbnull(dr1[DmucDichvuclsChitiet.Columns.TenChitietdichvu],
                                    ""));
                        }
                        if (tempt != string.Empty)
                            reval += tempt + "\n";
                    }
                }
            }
            foreach (DataRow dr in arrDr1)
            {
                DataRow[] arrtemp =
                    _mDtChitietPhieuCls.Select(KcbChidinhclsChitiet.Columns.IdChitietdichvu + "=" +
                                               Utility.sDbnull(dr[QheCamchidinhChungphieu.Columns.IdDichvu]));
                if (arrtemp.Length > 0)
                {
                    foreach (DataRow dr1 in arrtemp)
                    {
                        tempt = string.Empty;
                        key = idDichvuchitiet + "-" +
                              Utility.sDbnull(dr1[KcbChidinhclsChitiet.Columns.IdChitietdichvu], "");
                        if (!lstKey.Contains(key))
                        {
                            lstKey.Add(key);
                            tempt = string.Format("{0} - {1}", tenChitiet,
                                Utility.sDbnull(dr1[DmucDichvuclsChitiet.Columns.TenChitietdichvu],
                                    ""));
                        }
                        if (tempt != string.Empty)
                            reval += tempt + "\n";
                    }
                }
            }
            return reval;
        }

        private void txtFilterName_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Down))
            {
                if (grdDanhSachCLS.GetDataRows().Length == 1)
                {
                    foreach (GridEXRow _row in grdDanhSachCLS.GetDataRows())
                    {
                        if (_row.RowType == RowType.Record)
                        {
                            _row.IsChecked = true;
                            if (_row.IsChecked)
                            {
                                Utility.focusCell(grdDanhSachCLS, DmucDichvucl.Columns.TenDichvu);
                                AddOneRow_ServiceDetail();
                                txtFilterName.SelectAll();
                                txtFilterName.Focus();
                            }
                        }
                    }
                }
                else
                    Utility.focusCell(grdDanhSachCLS, DmucDichvucl.Columns.TenDichvu);
            }
        }

        private void LoadDanhmucchidinh()
        {
            try
            {
                string MA_KHOA_THIEN = globalVariables.MA_KHOA_THIEN;
                nhomchidinh = "-GOI,-TIEN,-CHIPHITHEM";
                _mDtPhongkham = new Select("*").From(DmucDichvukcb.Schema).ExecuteDataSet().Tables[0];
                m_dtqheCamchidinhCLSChungphieu =
                    new Select().From(QheCamchidinhChungphieu.Schema).Where(QheCamchidinhChungphieu.Columns.Loai).
                        IsEqualTo(0).ExecuteDataSet().Tables[0];
                m_dtDanhsachDichvuCLS = _chidinhCanlamsang.LaydanhsachCLS_chidinh("DV", 0, 0, -1, 0, MA_KHOA_THIEN,
                    nhomchidinh);
                if (!m_dtDanhsachDichvuCLS.Columns.Contains(KcbChidinhclsChitiet.Columns.SoLuong))
                    m_dtDanhsachDichvuCLS.Columns.Add(KcbChidinhclsChitiet.Columns.SoLuong, typeof (int));
                if (!m_dtDanhsachDichvuCLS.Columns.Contains("ten_donvitinh"))
                    m_dtDanhsachDichvuCLS.Columns.Add("ten_donvitinh", typeof (string));
                m_dtDanhsachDichvuCLS.AcceptChanges();
                Utility.SetDataSourceForDataGridEx(grdServiceDetail, m_dtDanhsachDichvuCLS, false, true, "", "");
                GridEXColumn gridExColumnGroupIntOrder = grdServiceDetail.RootTable.Columns["stt_hthi_dichvu"];
                GridEXColumn gridExColumnIntOrder = grdServiceDetail.RootTable.Columns["stt_hthi_chitiet"];
                Utility.SetGridEXSortKey(grdServiceDetail, gridExColumnGroupIntOrder, SortOrder.Ascending);
                Utility.SetGridEXSortKey(grdServiceDetail, gridExColumnIntOrder, SortOrder.Ascending);
                m_dtDanhsachDichvuCLS_org = m_dtDanhsachDichvuCLS.Copy();
                txtFilterName.Focus();
                txtFilterName.SelectAll();
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi trong quá trình load thông tin :" + exception.Message);
            }
        }

        private void cmdTaonhom_Click(object sender, EventArgs e)
        {
            var _quanlynhomchidinh_cls = new VNS.HIS.UI.NGOAITRU.frm_quanlynhomchidinh_cls(nhomchidinh, 0);
            _quanlynhomchidinh_cls.ShowDialog();
            txtNhomCD.Init(
                new Select().From(DmucNhomcanlamsang.Schema).Where(DmucNhomcanlamsang.Columns.NguoiTao).IsEqualTo(
                    globalVariables.UserName).Or(globalVariables.UserName).IsEqualTo("ADMIN").ExecuteDataSet().Tables[0],
                new List<string>
                {
                    DmucNhomcanlamsang.Columns.Id,
                    DmucNhomcanlamsang.Columns.MaNhom,
                    DmucNhomcanlamsang.Columns.TenNhom
                });
        }

        private void txtFilterName_TextChanged(object sender, EventArgs e)
        {
            FilterCLS();
        }

        private void FilterCLS()
        {
            try
            {
                //log.Trace("Filtering...");
                rowFilter = "1=1";
                if (!string.IsNullOrEmpty(txtFilterName.Text))
                {
                    string _value = Utility.DoTrim(txtFilterName.Text.ToUpper());
                    int rowcount = 0;
                    rowcount =
                        m_dtDanhsachDichvuCLS.Select(DmucDichvucl.Columns.MaDichvu + " ='" + _value +
                                                     "'").GetLength(0);
                    if (rowcount > 0)
                    {
                        rowFilter = DmucDichvucl.Columns.MaDichvu + " ='" + _value + "'";
                    }
                    else
                    {
                        rowFilter = DmucDichvuclsChitiet.Columns.TenChitietdichvu + " like '%" + _value +
                                    "%'  OR  " + DmucDichvuclsChitiet.Columns.MaChitietdichvu + " like '" + _value +
                                    "%'";
                    }
                }
                //m_dtDanhsachDichvuCLS.DefaultView.RowFilter = "1=1";
                m_dtDanhsachDichvuCLS.DefaultView.RowFilter = rowFilter;
                grdDanhSachCLS.Refresh();
                System.Windows.Forms.Application.DoEvents();
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:" + exception.Message);
                //log.ErrorException("Filter.exception-->", exception);
                // log.Error("loi trong qua trinh loc thong tin ", exception);
                rowFilter = "1=2";
            }
            finally
            {
                //log.Trace("Filtered");
            }
        }

        private void resetNewItem()
        {
            if (_mDtChitietPhieuCls == null || !_mDtChitietPhieuCls.Columns.Contains("isnew")) return;
            foreach (DataRow dr in _mDtChitietPhieuCls.Rows)
                dr["isnew"] = 0;
            _mDtChitietPhieuCls.AcceptChanges();
        }

        //private void LayThongTin_Chitiet_CLS()
        //{
        //    try
        //    {
        //        m_dtChitietPhieuCLS = CHIDINH_CANLAMSANG.LaythongtinCLS_Thuoc(Utility.Int32Dbnull(, -1), "DICHVU");
        //        Utility.SetDataSourceForDataGridEx(grdAssignDetail, m_dtChitietPhieuCLS, false, true, "1=1",
        //                                           "stt_hthi_dichvu,stt_hthi_chitiet," + DmucDichvuclsChitiet.Columns.TenChitietdichvu);
        //        grdAssignDetail.MoveFirst();
        //        ModifyCommand();
        //    }
        //    catch (Exception ex)
        //    {
        //        Utility.ShowMsg("Lỗi khi lấy dữ liệu CLS chi tiết\n" + ex.Message);
        //    }
        //}

        private void grdDanhSachCLS_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
            if (grdDanhSachCLS.CurrentRow.IsChecked)
            {
                AddOneRow_ServiceDetail();
            }
        }

        private void gridEX2_FormattingRow(object sender, RowLoadEventArgs e)
        {
        }

        #endregion
    }
}