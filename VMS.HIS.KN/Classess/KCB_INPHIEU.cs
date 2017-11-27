using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Transactions;
using CrystalDecisions.CrystalReports.Engine;
using SubSonic;
using VNS.HIS.BusRule.Classes;
using VNS.HIS.DAL;
using VNS.Libs;
using VNS.Properties;

namespace VMS.HIS.KN.Classess
{
    public class KcbInphieu
    {
        public static void INPHIEU_KHAM(string maDoiTuong, DataTable mDtReport, string sTitleReport, string khoGiay)
        {
            Utility.UpdateLogotoDatatable(ref mDtReport);
            switch (maDoiTuong)
            {
                case "DV":
                    InPhieuKCB_DV(mDtReport, sTitleReport, khoGiay);
                    break;
                case "BHYT":
                    InPhieuKCB_BHYT(mDtReport, sTitleReport, khoGiay);
                    break;
                default:
                    InPhieuKCB_DV(mDtReport, sTitleReport, khoGiay);
                    break;
            }
        }

        public static void INPHIEU_HEN(DataTable mDtReport, string sTitleReport)
        {
            Utility.UpdateLogotoDatatable(ref mDtReport);
            string tieude = "", reportname = "";
            ReportDocument reportDocument = Utility.GetReport("thamkham_inphieuhen_benhnhan", ref tieude, ref reportname);
            if (reportDocument == null) return;
            ReportDocument crpt = reportDocument;

            var objForm = new frmPrintPreview(sTitleReport, crpt, true, mDtReport.Rows.Count > 0);
            try
            {
                mDtReport.AcceptChanges();
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = "thamkham_inphieuhen_benhnhan";
                crpt.SetDataSource(mDtReport);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(globalVariables.SysDate));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky",
                                                             Utility.getTrinhky(objForm.mv_sReportFileName,
                                                                                globalVariables.SysDate));
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
                //if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInPhieuKCB, PropertyLib._MayInProperties.PreviewPhieuKCB))
                //{
                //    objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInPhieuKCB, 0);
                //    objForm.ShowDialog();
                //}
                //else
                //{
                //    objForm.addTrinhKy_OnFormLoad();
                //    crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                //    crpt.PrintToPrinter(1, false, 0, 0);
                //}
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
            }
            finally
            {
                Utility.FreeMemory(crpt);
            }
        }

        public static void INMAU_CHUYENKHAM_CHUYENKHOA(DataTable mDtReport, string sTitleReport, string report,
                                                       string lydochuyen)
        {
            Utility.UpdateLogotoDatatable(ref mDtReport);
            string tieude = "", reportname = "";
            ReportDocument reportDocument = report == "PHIEUKHAM_CHUYENKHOA"
                ? Utility.GetReport("thamkham_phieukham_chuyenkhoa", ref tieude, ref reportname)
                : Utility.GetReport("thamkham_phieuxn_benhpham", ref tieude, ref reportname);
            if (reportDocument == null) return;
            ReportDocument crpt = reportDocument;

            var objForm = new frmPrintPreview(sTitleReport, crpt, true, mDtReport.Rows.Count > 0);
            try
            {
                mDtReport.AcceptChanges();
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = "thamkham_phieukham_chuyenkhoa";
                crpt.SetDataSource(mDtReport);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone + globalVariables.SOMAYLE);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(globalVariables.SysDate));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "lydochuyen", lydochuyen);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky",
                                                             Utility.getTrinhky(objForm.mv_sReportFileName,
                                                                                globalVariables.SysDate));
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
                //if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInPhieuKCB, PropertyLib._MayInProperties.PreviewPhieuKCB))
                //{
                //    objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInPhieuKCB, 0);
                //    objForm.ShowDialog();
                //}
                //else
                //{
                //    objForm.addTrinhKy_OnFormLoad();
                //    crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                //    crpt.PrintToPrinter(1, false, 0, 0);
                //}
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        public static void InPhieuKCB_DV(DataTable mDtReport, string sTitleReport, string khoGiay)
        {
            var reportDocument = new ReportDocument();
            string tieude = "", reportname = "";
            switch (khoGiay)
            {
                case "A4":
                    reportDocument = Utility.GetReport("tiepdon_PhieuKCB_Dvu_A4", ref tieude, ref reportname);
                    break;
                case "A5":
                    reportDocument = Utility.GetReport("tiepdon_PhieuKCB_Dvu_A5", ref tieude, ref reportname);
                    break;
            }
            if (reportDocument == null) return;

            ReportDocument crpt = reportDocument;
            var objForm = new frmPrintPreview(sTitleReport, crpt, true, mDtReport.Rows.Count > 0);
            try
            {
                mDtReport.AcceptChanges();
                crpt.SetDataSource(mDtReport);
                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "  PHÒNG TIẾP ĐÓN   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(globalVariables.SysDate));
                Utility.SetParameterValue(crpt, "sTitleReport", sTitleReport);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky",
                                                             Utility.getTrinhky(objForm.mv_sReportFileName,
                                                                                globalVariables.SysDate));
                objForm.crptViewer.ReportSource = crpt;
                if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInPhieuKCB,
                                           PropertyLib._MayInProperties.PreviewPhieuKCB))
                {
                    objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInPhieuKCB, 0);
                    objForm.ShowDialog();
                }
                else
                {
                    objForm.addTrinhKy_OnFormLoad();
                    crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInPhieuKCB;
                    crpt.PrintToPrinter(1, false, 0, 0);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        /// <summary>
        /// HÀM THỰC HIỆN VIỆC IN PHIẾU THÔNG TIN KHÁM BỆNH
        /// </summary>
        /// <param name="mDtReport"></param>
        /// <param name="sTitleReport"></param>
        /// <param name="khoGiay"></param>
        public static void InPhieuKCB_BHYT(DataTable mDtReport, string sTitleReport, string khoGiay)
        {
            var reportDocument = new ReportDocument();
            string tieude = "", reportname = "";
            switch (khoGiay)
            {
                case "A4":
                    reportDocument = Utility.GetReport("tiepdon_PhieuKCB_BHYT_A4", ref tieude, ref reportname);
                    break;
                case "A5":
                    reportDocument = Utility.GetReport("tiepdon_PhieuKCB_BHYT_A5", ref tieude, ref reportname);
                    break;
            }
            if (reportDocument == null) return;
            ReportDocument crpt = reportDocument;
            // VietBaIT.HISLink.Report_LaoKhoa.Report_LaoKhoa.CRPT_BAOCAO_PHIEUKHAMBENH_BAOHIEMYTE crpt = new CRPT_BAOCAO_PHIEUKHAMBENH_BAOHIEMYTE();
            var objForm = new frmPrintPreview("", crpt, true, mDtReport.Rows.Count > 0);
            try
            {
                mDtReport.AcceptChanges();
                crpt.SetDataSource(mDtReport);
                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "  PHÒNG TIẾP ĐÓN   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(globalVariables.SysDate));
                Utility.SetParameterValue(crpt, "sTitleReport", sTitleReport);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky",
                                                             Utility.getTrinhky(objForm.mv_sReportFileName,
                                                                                globalVariables.SysDate));
                objForm.crptViewer.ReportSource = crpt;
                if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInPhieuKCB,
                                           PropertyLib._MayInProperties.PreviewPhieuKCB))
                {
                    objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInPhieuKCB, 0);
                    objForm.ShowDialog();
                }
                else
                {
                    objForm.addTrinhKy_OnFormLoad();
                    crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInPhieuKCB;
                    crpt.PrintToPrinter(1, false, 0, 0);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        public static ActionResult InTachToanBoPhieuCls(int idBenhnhan, string maLuotkham, int vAssignId,
                                                        string vAssignCode, List<string> listnhomincls,
                                                        int selectedIndex, bool inTach, ref string mayin)
        {
            using (var scope = new TransactionScope())
            {
                using (new SharedDbConnectionScope())
                {
                    try
                    {
                        mayin = "";
                        DataTable dtAll =
                            new KCB_THAMKHAM().KcbThamkhamLaydulieuInphieuCls(idBenhnhan, maLuotkham, vAssignCode,
                                "ALL").Tables[0];
                        foreach (string nhomcls in listnhomincls.ToList())
                        {
                            //   KcbChidinhcl objAssignInfo = KcbChidinhcl.FetchByID(v_AssignId);
                            DataTable dt= dtAll.Select("nhom_in_cls = '" + nhomcls + "'").CopyToDataTable();
                            if (dt == null || dt.Rows.Count <= 0)
                            {
                                Utility.ShowMsg("Không có dữ liệu in. Mời bạn kiểm tra lại");
                                //return;
                            }
                            else
                            {
                                //   THU_VIEN_CHUNG.CreateXML(dt, "Thamkham_InTachToanBophieuCLS.XML");
                                Utility.UpdateLogotoDatatable(ref dt);
                                string vMachidinh = vAssignCode;
                                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("CHIDINH_BODAUCHAM_TRENMAVACH", "0", false) ==
                                    "1")
                                {
                                    vMachidinh = vAssignCode.Replace(".", "");
                                }
                                Utility.CreateBarcodeData(ref dt, vMachidinh);
                                string manhomcls = nhomcls;
                                string tieude = "";
                                ReportDocument crpt = Utility.GetReport(manhomcls, ref tieude, ref manhomcls);
                                if (crpt == null) return ActionResult.Error;
                                try
                                {
                                    var objForm = new frmPrintPreview("IN PHIẾU CHỈ ĐỊNH", crpt, true, true)
                                    {
                                        mv_sReportFileName = Path.GetFileName(manhomcls),
                                        mv_sReportCode = manhomcls
                                    };
                                    crpt.SetDataSource(dt);
                                    //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "    Nhân viên        Bác sĩ chỉ định     ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                                    Utility.SetParameterValue(crpt, "ParentBranchName",
                                        globalVariables.ParentBranch_Name);
                                    Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                                    Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                                    Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                                    Utility.SetParameterValue(crpt, "txtTrinhky",
                                        Utility.getTrinhky(objForm.mv_sReportFileName,
                                            globalVariables.SysDate));
                                    if (!inTach && selectedIndex == 0)
                                    {
                                        foreach (DataRow dr in dt.Rows)
                                            dr[VKcbChidinhcl.Columns.TenNhominphieucls] =
                                                THU_VIEN_CHUNG.Laygiatrithamsohethong("TIEUDE_PHIEUCHIDNHCLS_INCHUNG",
                                                    "PHIẾU CHỈ ĐỊNH", true);
                                    }
                                    else
                                    {
                                        Utility.SetParameterValue(crpt, "TitleReport", tieude);
                                    }
                                    Utility.SetParameterValue(crpt, "CurrentDate",
                                        Utility.FormatDateTimeWithLocation(
                                            globalVariables.SysDate, globalVariables.gv_strDiadiem));
                                    objForm.crptViewer.ReportSource = crpt;
                                    if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai,
                                        PropertyLib._MayInProperties.PreviewInCLS))
                                    {
                                        objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
                                        objForm.ShowDialog();
                                        mayin = PropertyLib._MayInProperties.TenMayInBienlai;
                                    }
                                    else
                                    {
                                        objForm.addTrinhKy_OnFormLoad();
                                        crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                                        mayin = PropertyLib._MayInProperties.TenMayInBienlai;
                                        crpt.PrintToPrinter(1, false, 0, 0);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Utility.ShowMsg("Lỗi:"+ ex.Message);
                                    // Utility.DefaultNow(this);
                                }
                                finally
                                {
                                    Utility.FreeMemory(crpt);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Utility.ShowMsg("Lỗi:" + ex.Message);
                        return ActionResult.Error;
                    }
                  
                }
                scope.Complete();
                return ActionResult.Success;
            }
        }
        public static ActionResult NoitruInTachToanBoPhieuCls(int idBenhnhan, string maLuotkham, int vAssignId,
                                                     string vAssignCode, List<string> listnhomincls,
                                                     int selectedIndex, bool inTach, DateTime ngayin, ref string mayin)
        {
            using (var scope = new TransactionScope())
            {
                using (new SharedDbConnectionScope())
                {
                    try
                    {
                        mayin = "";
                        DataTable dtAll =
                            new KCB_THAMKHAM().KcbThamkhamLaydulieuInphieuCls(idBenhnhan, maLuotkham, vAssignCode,
                                "ALL").Tables[0];
                        foreach (string nhomcls in listnhomincls.ToList())
                        {
                            //   KcbChidinhcl objAssignInfo = KcbChidinhcl.FetchByID(v_AssignId);
                            DataTable dt = dtAll.Select("nhom_in_cls = '" + nhomcls + "'").CopyToDataTable();
                            if (dt == null || dt.Rows.Count <= 0)
                            {
                                Utility.ShowMsg("Không có dữ liệu in. Mời bạn kiểm tra lại");
                                //return;
                            }
                            else
                            {
                                //   THU_VIEN_CHUNG.CreateXML(dt, "Thamkham_InTachToanBophieuCLS.XML");
                                Utility.UpdateLogotoDatatable(ref dt);
                                string vMachidinh = vAssignCode;
                                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("CHIDINH_BODAUCHAM_TRENMAVACH", "0", false) ==
                                    "1")
                                {
                                    vMachidinh = vAssignCode.Replace(".", "");
                                }
                                Utility.CreateBarcodeData(ref dt, vMachidinh);
                                string manhomcls = nhomcls;
                                string tieude = "";
                                ReportDocument crpt = Utility.GetReport(manhomcls, ref tieude, ref manhomcls);
                                if (crpt == null) return ActionResult.Error;
                                try
                                {
                                    var objForm = new frmPrintPreview("IN PHIẾU CHỈ ĐỊNH", crpt, true, true)
                                    {
                                        mv_sReportFileName = Path.GetFileName(manhomcls),
                                        mv_sReportCode = manhomcls
                                    };
                                    crpt.SetDataSource(dt);
                                    //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "    Nhân viên        Bác sĩ chỉ định     ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                                    Utility.SetParameterValue(crpt, "ParentBranchName",
                                        globalVariables.ParentBranch_Name);
                                    Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                                    Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                                    Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                                    Utility.SetParameterValue(crpt, "txtTrinhky",
                                        Utility.getTrinhky(objForm.mv_sReportFileName,
                                            globalVariables.SysDate));
                                    if (!inTach && selectedIndex == 0)
                                    {
                                        foreach (DataRow dr in dt.Rows)
                                            dr[VKcbChidinhcl.Columns.TenNhominphieucls] =
                                                THU_VIEN_CHUNG.Laygiatrithamsohethong("TIEUDE_PHIEUCHIDNHCLS_INCHUNG",
                                                    "PHIẾU CHỈ ĐỊNH", true);
                                    }
                                    else
                                    {
                                        Utility.SetParameterValue(crpt, "TitleReport", tieude);
                                    }
                                    Utility.SetParameterValue(crpt, "CurrentDate",
                                        Utility.FormatDateTimeWithLocation(
                                           ngayin, globalVariables.gv_strDiadiem));
                                    objForm.crptViewer.ReportSource = crpt;
                                    if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai,
                                        PropertyLib._MayInProperties.PreviewInCLS))
                                    {
                                        objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
                                        objForm.ShowDialog();
                                        mayin = PropertyLib._MayInProperties.TenMayInBienlai;
                                    }
                                    else
                                    {
                                        objForm.addTrinhKy_OnFormLoad();
                                        crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                                        mayin = PropertyLib._MayInProperties.TenMayInBienlai;
                                        crpt.PrintToPrinter(1, false, 0, 0);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Utility.ShowMsg("Lỗi:" + ex.Message);
                                    // Utility.DefaultNow(this);
                                }
                                finally
                                {
                                    Utility.FreeMemory(crpt);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Utility.ShowMsg("Lỗi:" + ex.Message);
                        return ActionResult.Error;
                    }

                }
                scope.Complete();
                return ActionResult.Success;
            }
        }
        public static ActionResult InphieuDangkyKiemnghiem(int vIntIdMauKn)
        {
            return ActionResult.Success;
        }
        public static ActionResult InphieuBangiaoMauKiemnghiem(int vIntIdMauKn)
        {
            return ActionResult.Success;
        }
        public static ActionResult InphieuChidinhCls(int idBenhnhan, string maLuotkham, int vAssignId,
                                                     string vAssignCode, string nhomincls, int selectedIndex,
                                                     bool inTach, ref string mayin)
        {
            using (var scope = new TransactionScope())
            {
                using (new SharedDbConnectionScope())
                {
                    try
                    {
                        mayin = "";
                        //KcbChidinhcl.FetchByID(vAssignId);

                        DataTable dt =
                            new KCB_THAMKHAM().KcbThamkhamLaydulieuInphieuCls(idBenhnhan, maLuotkham, vAssignCode,
                                nhomincls).Tables[0];
                        if (dt == null || dt.Rows.Count <= 0)
                        {
                            // Utility.ShowMsg("Không có dữ liệu in. Mời bạn kiểm tra lại");
                            return ActionResult.Error;
                        }
                        //   THU_VIEN_CHUNG.CreateXML(dt, "Thamkham_InphieuCLS.XML");
                        Utility.UpdateLogotoDatatable(ref dt);
                        string vMachidinh = vAssignCode;
                        if (THU_VIEN_CHUNG.Laygiatrithamsohethong("CHIDINH_BODAUCHAM_TRENMAVACH", "0", true) == "1")
                        {
                            vMachidinh = vAssignCode.Replace(".", "");
                        }
                        Utility.CreateBarcodeData(ref dt, vMachidinh);
                        string reportCode;
                        string khoGiay = "A5";
                        bool inchung = false;
                        string tieude = "", reportname = "";
                        if (PropertyLib._MayInProperties.CoGiayInCLS == Papersize.A4) khoGiay = "A4";
                        if (khoGiay == "A5")
                            if (inTach) //Nếu in riêng mà chọn tất
                            {
                                if (PropertyLib._ThamKhamProperties.ChophepintachCLSKhacPhieu)
                                {
                                    switch (selectedIndex)
                                    {
                                        case 0:
                                            reportCode = "thamkham_InphieuchidinhCLS_RIENG_A5";
                                            break;
                                        case 1:
                                            reportCode = "thamkham_InphieuXetNghiem_A5";
                                            break;
                                        case 2:
                                            reportCode = "thamkham_InphieuSieuAm_A5";
                                            break;
                                        case 3:
                                            reportCode = "thamkham_InphieuXQuang_A5";
                                            break;
                                        case 5:
                                            reportCode = "thamkham_InphieuNoiSoi_A5";
                                            break;
                                        case 6:
                                            reportCode = "thamkham_InphieuDienTim_A5";
                                            break;
                                        default:
                                            reportCode = "thamkham_InphieuchidinhCLS_RIENG_A5";
                                            break;
                                    }
                                }
                                else
                                {
                                    reportCode = "thamkham_InphieuchidinhCLS_RIENG_A5";
                                }
                            }
                            else
                            {
                                inchung = true;
                                reportCode = "thamkham_InphieuchidinhCLS_A5";
                            }
                        else //Khổ giấy A4
                            if (inTach && selectedIndex == 0)
                            {
                                if (PropertyLib._ThamKhamProperties.ChophepintachCLSKhacPhieu)
                                {
                                    switch (selectedIndex)
                                    {
                                        case 0:
                                            reportCode = "thamkham_InphieuchidinhCLS_RIENG_A4";
                                            break;
                                        case 1:
                                            reportCode = "thamkham_InphieuXetNghiem_A4";
                                            break;
                                        case 2:
                                            reportCode = "thamkham_InphieuSieuAm_A4";
                                            break;
                                        case 3:
                                            reportCode = "thamkham_InphieuXQuang_A4";
                                            break;
                                        case 5:
                                            reportCode = "thamkham_InphieuNoiSoi_A4";
                                            break;
                                        case 6:
                                            reportCode = "thamkham_InphieuDienTim_A4";
                                            break;
                                        default:
                                            reportCode = "thamkham_InphieuchidinhCLS_RIENG_A4";
                                            break;
                                    }
                                }
                                else
                                {
                                    reportCode = "thamkham_InphieuchidinhCLS_RIENG_A4";
                                }
                            } //Nếu in riêng mà chọn tất-->Gọi báo cáo nhóm theo nhóm in
                                //  _reportCode = "thamkham_InphieuchidinhCLS_RIENG_A4";
                            else
                            {
                                inchung = true;

                                reportCode = "thamkham_InphieuchidinhCLS_A4";
                            }
                        ReportDocument crpt = Utility.GetReport(reportCode, ref tieude, ref reportname);
                        if (crpt == null) return ActionResult.Error;
                        if (inchung)
                        {
                            List<string> lstNhominCls = (from p in dt.AsEnumerable()
                                where
                                    Utility.DoTrim(
                                        Utility.sDbnull(p.Field<string>("nhom_in_cls"))) != ""
                                select p.Field<string>("nhom_in_cls")
                                ).Distinct().ToList();
                            if (lstNhominCls.Count > 1)
                            {
                                string tenphieuchidinh = THU_VIEN_CHUNG.Laygiatrithamsohethong("CLS_TENPHIEU_INCHUNG",
                                    "PHIẾU CHỈ ĐỊNH CẬN LÂM SÀNG",
                                    true);
                                foreach (DataRow dr in dt.Rows)
                                    dr["ten_nhominphieucls"] = tenphieuchidinh;
                            }
                        }
                        var objForm = new frmPrintPreview("IN PHIẾU CHỈ ĐỊNH", crpt, true, true)
                        {
                            mv_sReportFileName = Path.GetFileName(reportname),
                            mv_sReportCode = reportCode
                        };
                        try
                        {
                            crpt.SetDataSource(dt);
                            Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                            Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                            Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                            Utility.SetParameterValue(crpt, "txtTrinhky",
                                Utility.getTrinhky(objForm.mv_sReportFileName,
                                    globalVariables.SysDate));
                            if (!inTach && selectedIndex == 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                    dr[VKcbChidinhcl.Columns.TenNhominphieucls] =
                                        THU_VIEN_CHUNG.Laygiatrithamsohethong("TIEUDE_PHIEUCHIDNHCLS_INCHUNG",
                                            "PHIẾU CHỈ ĐỊNH", true);
                            }
                            else
                            {
                                Utility.SetParameterValue(crpt, "TitleReport", tieude);
                            }
                            Utility.SetParameterValue(crpt, "CurrentDate",
                                Utility.FormatDateTimeWithLocation(globalVariables.SysDate,
                                    globalVariables.gv_strDiadiem));
                            objForm.crptViewer.ReportSource = crpt;
                            if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai,
                                PropertyLib._MayInProperties.PreviewInCLS))
                            {
                                objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
                                objForm.ShowDialog();
                                mayin = PropertyLib._MayInProperties.TenMayInBienlai;
                            }
                            else
                            {
                                objForm.addTrinhKy_OnFormLoad();
                                crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                                mayin = PropertyLib._MayInProperties.TenMayInBienlai;
                                crpt.PrintToPrinter(1, false, 0, 0);
                            }
                        }
                        catch (Exception ex)
                        {
                            Utility.ShowMsg("Lỗi:"+ ex.Message);
                            return ActionResult.Error;
                            // Utility.DefaultNow(this);
                        }
                        finally
                        {
                            Utility.FreeMemory(crpt);
                        }
                    }
                    catch (Exception ex)
                    {
                        Utility.ShowMsg("Lỗi:" + ex.Message);
                    }
                    finally
                    {
                        GC.Collect();
                    }
                }
                scope.Complete();
                return ActionResult.Success;
            }
        }
        public static ActionResult NoitruInphieuChidinhCls(int idBenhnhan, string maLuotkham, int vAssignId,
                                                   string vAssignCode, string nhomincls, int selectedIndex,
                                                   bool inTach, ref string mayin)
        {
            using (var scope = new TransactionScope())
            {
                using (new SharedDbConnectionScope())
                {
                    try
                    {
                        mayin = "";
                        //KcbChidinhcl.FetchByID(vAssignId);

                        DataTable dt =
                            new KCB_THAMKHAM().KcbThamkhamLaydulieuInphieuCls(idBenhnhan, maLuotkham, vAssignCode,
                                nhomincls).Tables[0];
                        if (dt == null || dt.Rows.Count <= 0)
                        {
                            // Utility.ShowMsg("Không có dữ liệu in. Mời bạn kiểm tra lại");
                            return ActionResult.Error;
                        }
                        //   THU_VIEN_CHUNG.CreateXML(dt, "Thamkham_InphieuCLS.XML");
                        Utility.UpdateLogotoDatatable(ref dt);
                        string vMachidinh = vAssignCode;
                        if (THU_VIEN_CHUNG.Laygiatrithamsohethong("CHIDINH_BODAUCHAM_TRENMAVACH", "0", true) == "1")
                        {
                            vMachidinh = vAssignCode.Replace(".", "");
                        }
                        Utility.CreateBarcodeData(ref dt, vMachidinh);
                        string reportCode;
                        string khoGiay = "A5";
                        bool inchung = false;
                        string tieude = "", reportname = "";
                        if (PropertyLib._MayInProperties.CoGiayInCLS == Papersize.A4) khoGiay = "A4";
                        if (khoGiay == "A5")
                            if (inTach) //Nếu in riêng mà chọn tất
                            {
                                if (PropertyLib._ThamKhamProperties.ChophepintachCLSKhacPhieu)
                                {
                                    switch (selectedIndex)
                                    {
                                        case 0:
                                            reportCode = "thamkham_InphieuchidinhCLS_RIENG_A5";
                                            break;
                                        case 1:
                                            reportCode = "thamkham_InphieuXetNghiem_A5";
                                            break;
                                        case 2:
                                            reportCode = "thamkham_InphieuSieuAm_A5";
                                            break;
                                        case 3:
                                            reportCode = "thamkham_InphieuXQuang_A5";
                                            break;
                                        case 5:
                                            reportCode = "thamkham_InphieuNoiSoi_A5";
                                            break;
                                        case 6:
                                            reportCode = "thamkham_InphieuDienTim_A5";
                                            break;
                                        default:
                                            reportCode = "thamkham_InphieuchidinhCLS_RIENG_A5";
                                            break;
                                    }
                                }
                                else
                                {
                                    reportCode = "thamkham_InphieuchidinhCLS_RIENG_A5";
                                }
                            }
                            else
                            {
                                inchung = true;
                                reportCode = "thamkham_InphieuchidinhCLS_A5";
                            }
                        else //Khổ giấy A4
                            if (inTach && selectedIndex == 0)
                            {
                                if (PropertyLib._ThamKhamProperties.ChophepintachCLSKhacPhieu)
                                {
                                    switch (selectedIndex)
                                    {
                                        case 0:
                                            reportCode = "thamkham_InphieuchidinhCLS_RIENG_A4";
                                            break;
                                        case 1:
                                            reportCode = "thamkham_InphieuXetNghiem_A4";
                                            break;
                                        case 2:
                                            reportCode = "thamkham_InphieuSieuAm_A4";
                                            break;
                                        case 3:
                                            reportCode = "thamkham_InphieuXQuang_A4";
                                            break;
                                        case 5:
                                            reportCode = "thamkham_InphieuNoiSoi_A4";
                                            break;
                                        case 6:
                                            reportCode = "thamkham_InphieuDienTim_A4";
                                            break;
                                        default:
                                            reportCode = "thamkham_InphieuchidinhCLS_RIENG_A4";
                                            break;
                                    }
                                }
                                else
                                {
                                    reportCode = "thamkham_InphieuchidinhCLS_RIENG_A4";
                                }
                            } //Nếu in riêng mà chọn tất-->Gọi báo cáo nhóm theo nhóm in
                            //  _reportCode = "thamkham_InphieuchidinhCLS_RIENG_A4";
                            else
                            {
                                inchung = true;

                                reportCode = "thamkham_InphieuchidinhCLS_A4";
                            }
                        ReportDocument crpt = Utility.GetReport(reportCode, ref tieude, ref reportname);
                        if (crpt == null) return ActionResult.Error;
                        if (inchung)
                        {
                            List<string> lstNhominCls = (from p in dt.AsEnumerable()
                                                         where
                                                             Utility.DoTrim(
                                                                 Utility.sDbnull(p.Field<string>("nhom_in_cls"))) != ""
                                                         select p.Field<string>("nhom_in_cls")
                                ).Distinct().ToList();
                            if (lstNhominCls.Count > 1)
                            {
                                string tenphieuchidinh = THU_VIEN_CHUNG.Laygiatrithamsohethong("CLS_TENPHIEU_INCHUNG",
                                    "PHIẾU CHỈ ĐỊNH CẬN LÂM SÀNG",
                                    true);
                                foreach (DataRow dr in dt.Rows)
                                    dr["ten_nhominphieucls"] = tenphieuchidinh;
                            }
                        }
                        var objForm = new frmPrintPreview("IN PHIẾU CHỈ ĐỊNH", crpt, true, true)
                        {
                            mv_sReportFileName = Path.GetFileName(reportname),
                            mv_sReportCode = reportCode
                        };
                        try
                        {
                            crpt.SetDataSource(dt);
                            Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                            Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                            Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                            Utility.SetParameterValue(crpt, "txtTrinhky",
                                Utility.getTrinhky(objForm.mv_sReportFileName,
                                    globalVariables.SysDate));
                            if (!inTach && selectedIndex == 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                    dr[VKcbChidinhcl.Columns.TenNhominphieucls] =
                                        THU_VIEN_CHUNG.Laygiatrithamsohethong("TIEUDE_PHIEUCHIDNHCLS_INCHUNG",
                                            "PHIẾU CHỈ ĐỊNH", true);
                            }
                            else
                            {
                                Utility.SetParameterValue(crpt, "TitleReport", tieude);
                            }
                            Utility.SetParameterValue(crpt, "CurrentDate",
                                Utility.FormatDateTimeWithLocation(globalVariables.SysDate,
                                    globalVariables.gv_strDiadiem));
                            objForm.crptViewer.ReportSource = crpt;
                            if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai,
                                PropertyLib._MayInProperties.PreviewInCLS))
                            {
                                objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
                                objForm.ShowDialog();
                                mayin = PropertyLib._MayInProperties.TenMayInBienlai;
                            }
                            else
                            {
                                objForm.addTrinhKy_OnFormLoad();
                                crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                                mayin = PropertyLib._MayInProperties.TenMayInBienlai;
                                crpt.PrintToPrinter(1, false, 0, 0);
                            }
                        }
                        catch (Exception ex)
                        {
                            Utility.ShowMsg("Lỗi:" + ex.Message);
                            return ActionResult.Error;
                            // Utility.DefaultNow(this);
                        }
                        finally
                        {
                            Utility.FreeMemory(crpt);
                        }
                    }
                    catch (Exception ex)
                    {
                        Utility.ShowMsg("Lỗi:" + ex.Message);
                    }
                    finally
                    {
                        GC.Collect();
                    }
                }
                scope.Complete();
                return ActionResult.Success;
            }
        }
    }
}