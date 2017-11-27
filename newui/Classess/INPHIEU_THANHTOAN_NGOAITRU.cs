﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using SubSonic;
using VNS.HIS.DAL;
using VNS.Libs;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;
using System.IO;
namespace VNS.HIS.Classes
{
    /// <summary>
    /// 05-11-2013
    ///  </summary>
   public class INPHIEU_THANHTOAN_NGOAITRU
    {
       DateTime NGAYINPHIEU;
       private  decimal SumOfTotal(DataTable dataTable)
       {
           return Utility.DecimaltoDbnull(dataTable.Compute("SUM(TONG_BN)+sum(PHU_THU)", "1=1"), 0);
       }
       public INPHIEU_THANHTOAN_NGOAITRU(DateTime NGAYINPHIEU)
       {
           this.NGAYINPHIEU = NGAYINPHIEU;
       }
       public INPHIEU_THANHTOAN_NGOAITRU()
       {
       }
       
       private decimal TinhTongBienLai(DataTable dataTable)
       {

           decimal tong = dataTable.AsEnumerable().Sum(c => c.Field<decimal>("tongtien_bnhan"));
           return tong;
       }
       public void INBIENLAI_QUAYTHUOC(DataTable m_dtReportPhieuThu, DateTime NgayInPhieu, string khogiay)
       {
           Utility.UpdateLogotoDatatable(ref m_dtReportPhieuThu);
           ReportDocument reportDocument = new ReportDocument();
           string tieude = "", reportname = "",reportcode="";
           switch (khogiay)
           {
               case "A4":
                   reportcode = "quaythuoc_bienlaithanhtoan_A4";
                   reportDocument = Utility.GetReport("quaythuoc_bienlaithanhtoan_A4", ref tieude, ref reportname);
                   break;
               case "A5":
                   reportcode = "quaythuoc_bienlaithanhtoan_A5";
                   reportDocument = Utility.GetReport("quaythuoc_bienlaithanhtoan_A5", ref tieude, ref reportname);
                   break;

           }
           if (reportDocument == null) return;
           var crpt = reportDocument;
           var p = (from q in m_dtReportPhieuThu.AsEnumerable()
                    group q by q.Field<long>(KcbThanhtoan.Columns.IdThanhtoan) into r
                    select new
                    {
                        _key = r.Key,
                        tongtien_chietkhau_hoadon = r.Min(g => g.Field<decimal>("tongtien_chietkhau_hoadon")),
                        tongtien_chietkhau_chitiet = r.Min(g => g.Field<decimal>("tongtien_chietkhau_chitiet")),
                        tongtien_chietkhau = r.Min(g => g.Field<decimal>("tongtien_chietkhau"))
                    }).ToList();

           decimal tong = m_dtReportPhieuThu.AsEnumerable().Sum(c => c.Field<decimal>("TT_BN"));
           decimal tong_ck_hoadon = p.Sum(c => c.tongtien_chietkhau_hoadon);
           decimal tong_ck = p.Sum(c => c.tongtien_chietkhau);
           tong = tong - tong_ck;
           var objForm = new frmPrintPreview("", crpt, true, true);
           objForm.mv_sReportFileName = Path.GetFileName(reportname);
           objForm.mv_sReportCode = reportcode;
           //try
           //{
           crpt.SetDataSource(m_dtReportPhieuThu.DefaultView);
           //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "                                                                      ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
           Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
           Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
           Utility.SetParameterValue(crpt, "Telephone", globalVariables.Branch_Phone);
           Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);

           Utility.SetParameterValue(crpt, "tienmiengiam_hdon", tong_ck_hoadon);
           Utility.SetParameterValue(crpt, "tong_miengiam", tong_ck);
           Utility.SetParameterValue(crpt, "tongtien_bn", tong);

           //  Utility.SetParameterValue(crpt,"DateTime", Utility.FormatDateTime(dtCreateDate.Value));
           Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(NgayInPhieu));
           Utility.SetParameterValue(crpt, "sTitleReport", tieude);
           Utility.SetParameterValue(crpt, "sMoneyCharacter",
                                  new MoneyByLetter().sMoneyToLetter(Utility.sDbnull(tong)));
           Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
           objForm.crptViewer.ReportSource = crpt;
           if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai, PropertyLib._MayInProperties.PreviewInBienlai))
           {
               objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
               objForm.ShowDialog();
           }
           else
           {
               objForm.addTrinhKy_OnFormLoad();
               crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
               crpt.PrintToPrinter(1, false, 0, 0);
           }
       }

     
       /// <summary>
       /// hàm thực hiện việc in phiếu dịch vụ
       /// </summary>
       /// <param name="m_dtReportPhieuThu"></param>
       /// <param name="NgayInPhieu"></param>
       /// <param name="sTitleReport"></param>
       public void LAOKHOA_INPHIEU_DICHVU(DataTable m_dtReportPhieuThu, DateTime NgayInPhieu, string sTitleReport)
       {
           Utility.UpdateLogotoDatatable(ref m_dtReportPhieuThu);
            string tieude="", reportname = "";
           var crpt = Utility.GetReport("thanhtoan_crpt_PhieuDV_A5",ref tieude,ref reportname);
           if (crpt == null) return;
           var objForm = new frmPrintPreview("", crpt, true, true);
           //try
           //{
           objForm.mv_sReportFileName = Path.GetFileName(reportname);
           objForm.mv_sReportCode = "thanhtoan_crpt_PhieuDV_A5";
           crpt.SetDataSource(m_dtReportPhieuThu);
           //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "                                                                      ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
           Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
           Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
           Utility.SetParameterValue(crpt,"Telephone", globalVariables.Branch_Phone);
           Utility.SetParameterValue(crpt,"Address", globalVariables.Branch_Address);
           //  Utility.SetParameterValue(crpt,"DateTime", Utility.FormatDateTime(dtCreateDate.Value));
           Utility.SetParameterValue(crpt,"CurrentDate", Utility.FormatDateTime(NgayInPhieu));
           Utility.SetParameterValue(crpt,"sTitleReport", sTitleReport);
           Utility.SetParameterValue(crpt,"sMoneyCharacter",
                                  new MoneyByLetter().sMoneyToLetter(SumOfTotal(m_dtReportPhieuThu).ToString()));
           Utility.SetParameterValue(crpt,"BottomCondition", THU_VIEN_CHUNG.BottomCondition());
           objForm.crptViewer.ReportSource = crpt;
           objForm.ShowDialog();
       }
       /// <summary>
       /// hàm thưc hiện việc in phiếu thông tin biên lai của bhyt cho bệnh nhân
       /// </summary>
       /// <param name="m_dtReportPhieuThu"></param>
       /// <param name="NgayInPhieu"></param>
       /// <param name="sTitleReport"></param>
       public void LAOKHOA_INPHIEU_BIENLAI_BHYT_CHO_BN(DataTable m_dtReportPhieuThu, DateTime NgayInPhieu, string sTitleReport)
       {
           Utility.UpdateLogotoDatatable(ref m_dtReportPhieuThu);
            string tieude="", reportname = "";
           var crpt = Utility.GetReport("thanhtoan_crpt_PhieuThu_BHYT_Cho_BN_A5",ref tieude,ref reportname);
           if (crpt == null) return;
           var objForm = new frmPrintPreview("", crpt, true, true);
           //try
           //{
           objForm.mv_sReportFileName = Path.GetFileName(reportname);
           objForm.mv_sReportCode = "thanhtoan_crpt_PhieuThu_BHYT_Cho_BN_A5";
           crpt.SetDataSource(m_dtReportPhieuThu);
           //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "                                                                      ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
           Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
           Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
           Utility.SetParameterValue(crpt,"Telephone", globalVariables.Branch_Phone);
           Utility.SetParameterValue(crpt,"Address", globalVariables.Branch_Address);
           //  Utility.SetParameterValue(crpt,"DateTime", Utility.FormatDateTime(dtCreateDate.Value));
           Utility.SetParameterValue(crpt,"CurrentDate", Utility.FormatDateTime(NgayInPhieu));
           Utility.SetParameterValue(crpt,"sTitleReport", sTitleReport);
           Utility.SetParameterValue(crpt,"sMoneyCharacter",
                                  new MoneyByLetter().sMoneyToLetter(SumOfTotal(m_dtReportPhieuThu).ToString()));
           Utility.SetParameterValue(crpt,"BottomCondition", THU_VIEN_CHUNG.BottomCondition());
           objForm.crptViewer.ReportSource = crpt;
           objForm.ShowDialog();
       }
       /// <summary>
       /// hàm thưc hiện viêc in thông tin của phiếu thu đồng chi trả
       /// </summary>
       /// <param name="m_dtReportPhieuThu"></param>
       /// <param name="NgayInPhieu"></param>
       /// <param name="sTitleReport"></param>
       public void INPHIEU_DONGCHITRA(DataTable m_dtReportPhieuThu, DateTime NgayInPhieu, string sTitleReport)
       {
           Utility.UpdateLogotoDatatable(ref m_dtReportPhieuThu);
            string tieude="", reportname = "";
           var crpt = Utility.GetReport("thanhtoan_PHIEUTHU_DONGCHITRA",ref tieude,ref reportname);
           if (crpt == null) return;
           var objForm = new frmPrintPreview("", crpt, true, true);
           //try
           //{
           objForm.mv_sReportFileName = Path.GetFileName(reportname);
           objForm.mv_sReportCode = "thanhtoan_PHIEUTHU_DONGCHITRA";
           crpt.SetDataSource(m_dtReportPhieuThu);
           //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "                                                                      ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
           //Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
           Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
           Utility.SetParameterValue(crpt,"Phone", globalVariables.Branch_Phone);
           Utility.SetParameterValue(crpt,"Address", globalVariables.Branch_Address);
           //  Utility.SetParameterValue(crpt,"DateTime", Utility.FormatDateTime(dtCreateDate.Value));
           Utility.SetParameterValue(crpt,"CurrentDate", Utility.FormatDateTime(NgayInPhieu));
           Utility.SetParameterValue(crpt,"sTitleReport", sTitleReport);
           Utility.SetParameterValue(crpt,"sMoneyLetter",
                                  new MoneyByLetter().sMoneyToLetter(SumOfTotal(m_dtReportPhieuThu,"SO_TIEN").ToString()));
           Utility.SetParameterValue(crpt,"BottomCondition", THU_VIEN_CHUNG.BottomCondition());
           objForm.crptViewer.ReportSource = crpt;
           if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai, PropertyLib._MayInProperties.PreviewInBienlai))
           {
               objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
               objForm.ShowDialog();
           }
           else
           {
               objForm.addTrinhKy_OnFormLoad();
               crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
               crpt.PrintToPrinter(1, false, 0, 0);
           }
       }
       private  MoneyByLetter sMoneyByLetter = new MoneyByLetter();
       /// <summary>
       /// hàm thực hiện in phiếu bảo hiểm đúng tuyến
       /// </summary>
       /// <param name="sTitleReport"></param>
       public void LAOKHOA_INPHIEU_BAOHIEM_NGOAITRU(DataTable m_dtReportPhieuThu, string sTitleReport,DateTime ngayIn)
       {
           Utility.UpdateLogotoDatatable(ref m_dtReportPhieuThu);
           THU_VIEN_CHUNG.Sapxepthutuin(ref m_dtReportPhieuThu,true);
           m_dtReportPhieuThu.DefaultView.Sort = "THU_TU ASC";
           m_dtReportPhieuThu.AcceptChanges();
            string tieude="", reportname = "";
           var crpt = Utility.GetReport("BHYT_InPhoi",ref tieude,ref reportname);
           if (crpt == null) return;
           var objForm = new frmPrintPreview("", crpt, true, true);
           //try
           //{
           objForm.mv_sReportFileName = Path.GetFileName(reportname);
           objForm.mv_sReportCode = "BHYT_InPhoi";
           crpt.SetDataSource(m_dtReportPhieuThu.DefaultView);
           //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "    Nhân viên                                                                   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
           // Utility.SetParameterValue(crpt,"Telephone", globalVariables.Branch_Phone);
           // Utility.SetParameterValue(crpt,"Address", globalVariables.Branch_Address);
           Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
           Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
           Utility.SetParameterValue(crpt,"CurrentDate", Utility.FormatDateTime(ngayIn));
           Utility.SetParameterValue(crpt,"sTitleReport", sTitleReport);
           Utility.SetParameterValue(crpt,"sMoneyCharacter_Thanhtien",
                                  sMoneyByLetter.sMoneyToLetter(
                                      SumOfTotal_BH(m_dtReportPhieuThu, "THANH_TIEN").ToString()));
           Utility.SetParameterValue(crpt,"sMoneyCharacter_Thanhtien_BH",
                                  sMoneyByLetter.sMoneyToLetter(
                                      SumOfTotal_BH(m_dtReportPhieuThu, "BHCT").ToString()));
           Utility.SetParameterValue(crpt,"sMoneyCharacter_Thanhtien_BN",
                                  sMoneyByLetter.sMoneyToLetter(
                                      SumOfTotal_BH(m_dtReportPhieuThu, "BNCT").ToString()));
           Utility.SetParameterValue(crpt,"sMoneyCharacter_Thanhtien_Khac",
                                  sMoneyByLetter.sMoneyToLetter(
                                      SumOfTotal_BH(m_dtReportPhieuThu, "PHU_THU").ToString()));
           objForm.crptViewer.ReportSource = crpt;
           objForm.addTrinhKy_OnFormLoad();
           PrintDialog frmPrint = new PrintDialog();
           if(frmPrint.ShowDialog() == DialogResult.OK)
           {
               crpt.PrintOptions.PrinterName = frmPrint.PrinterSettings.PrinterName;
               crpt.PrintToPrinter(frmPrint.PrinterSettings.Copies,frmPrint.PrinterSettings.Collate,frmPrint.PrinterSettings.FromPage,frmPrint.PrinterSettings.ToPage);             
           }
           //objForm.ShowDialog();
          
           //}
           //catch (Exception ex)
           //{
           //   
           //}
       }

       public void INPHOI_BHYT(DataTable m_dtReportPhieuThu,  DateTime ngayIn, KcbLuotkham objPatientExam)
       {
           Utility.UpdateLogotoDatatable(ref m_dtReportPhieuThu);
           if (objPatientExam.Noitru == 1)
           {
               THU_VIEN_CHUNG.NoiTru_Sapxepthutuin(ref m_dtReportPhieuThu, true);
               
           }
           else
           {
               THU_VIEN_CHUNG.Sapxepthutuin(ref m_dtReportPhieuThu, true);
           }
           m_dtReportPhieuThu.DefaultView.Sort = "stt_in,stt_hthi_dichvu,stt_hthi_chitiet,stt_in_thuoc,ten_chitietdichvu";
           m_dtReportPhieuThu.AcceptChanges();

            string tieude="", reportname = "";
           var crpt = Utility.GetReport("BHYT_InPhoi" ,ref tieude,ref reportname);
          // VMS.HISLink.Report.Report.BHYT_InPhoi crpt = new VMS.HISLink.Report.Report.BHYT_InPhoi();
          // tieude = "BẢNG KÊ CHI PHÍ KHÁM BỆNH, CHỮA BỆNH NGOẠI TRÚ";
           //reportname = "BHYT_InPhoi.RPT".ToUpper();
           if (crpt == null) return;
           frmPrintPreview objForm = new frmPrintPreview(tieude, crpt, true, true);
           objForm.NGAY = NGAYINPHIEU;
           try
           {
               objForm.mv_sReportFileName = Path.GetFileName(reportname);
               objForm.mv_sReportCode = "BHYT_InPhoi";
               crpt.SetDataSource(m_dtReportPhieuThu.DefaultView);
               //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "    Nhân viên                                                                   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
               Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
               Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
               Utility.SetParameterValue(crpt,"CurrentDate", Utility.FormatDateTime(ngayIn));
               Utility.SetParameterValue(crpt, "sTitleReport", tieude);
               Utility.SetParameterValue(crpt,"sMoneyCharacter_Thanhtien",
                                      sMoneyByLetter.sMoneyToLetter(
                                          SumOfTotal_BH(m_dtReportPhieuThu, "TT_KHONG_PHUTHU").ToString()));
               Utility.SetParameterValue(crpt,"sMoneyCharacter_Thanhtien_BH",
                                      sMoneyByLetter.sMoneyToLetter(
                                          SumOfTotal_BH(m_dtReportPhieuThu, "TT_BHYT").ToString()));
               Utility.SetParameterValue(crpt,"sMoneyCharacter_Thanhtien_BN",
                                      sMoneyByLetter.sMoneyToLetter(
                                          SumOfTotal_BH(m_dtReportPhieuThu, "TT_BN").ToString()));
               Utility.SetParameterValue(crpt,"sMoneyCharacter_Thanhtien_Khac",
                                      sMoneyByLetter.sMoneyToLetter(
                                          SumOfTotal_BH(m_dtReportPhieuThu, "TT_PHUTHU").ToString()));
               Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, NGAYINPHIEU));
               objForm.crptViewer.ReportSource = crpt;
               

               if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai, PropertyLib._MayInProperties.PreviewInPhoiBHYT))
               {
                   objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
                   if (objForm.ShowDialog() == DialogResult.OK)
                   {
                       //Tự động khóa BN để kết thúc
                       new Update(KcbLuotkham.Schema)
                           .Set(KcbLuotkham.Columns.NgayKetthuc).EqualTo(globalVariables.SysDate)
                           .Set(KcbLuotkham.Columns.NguoiKetthuc).EqualTo(globalVariables.UserName)
                           .Set(KcbLuotkham.Columns.Locked).EqualTo(1)
                           .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objPatientExam.MaLuotkham)
                           .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objPatientExam.IdBenhnhan).Execute();
                   }
               }
               else
               {
                   //Tự động khóa BN để kết thúc
                   new Update(KcbLuotkham.Schema)
                       .Set(KcbLuotkham.Columns.NgayKetthuc).EqualTo(globalVariables.SysDate)
                       .Set(KcbLuotkham.Columns.NguoiKetthuc).EqualTo(globalVariables.UserName)
                       .Set(KcbLuotkham.Columns.Locked).EqualTo(1)
                       .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objPatientExam.MaLuotkham)
                       .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objPatientExam.IdBenhnhan).Execute();
                   objForm.addTrinhKy_OnFormLoad();
                   crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                   crpt.PrintToPrinter(1, false, 0, 0);
                   
               }
               
           }
           catch (Exception ex)
           {
               Utility.CatchException(ex);
           }
           finally
           {
               Utility.FreeMemory(crpt);
           }
       }
       private  decimal SumOfTotal(DataTable dataTable, string FiledName)
       {
           return Utility.DecimaltoDbnull(dataTable.AsEnumerable().Sum(c => c.Field<decimal>(FiledName)));
       }
       public void INPHIEU_DICHVU(DataTable m_dtReportPhieuThu, DateTime NgayInPhieu, string khogiay)
       {
           Utility.UpdateLogotoDatatable(ref m_dtReportPhieuThu);
           m_dtReportPhieuThu.DefaultView.Sort = "stt_in ASC";
           m_dtReportPhieuThu.AcceptChanges();
           ReportDocument reportDocument = new ReportDocument();
           string tieude = "", reportname = "", reportCode = "";
           switch (khogiay)
           {
               case "A4":
                   reportCode = "thanhtoan_Bienlai_Dichvu_A4";
                   reportDocument = Utility.GetReport("thanhtoan_Bienlai_Dichvu_A4", ref tieude, ref reportname);
                   break;
               case "A5":
                   reportCode = "thanhtoan_Bienlai_Dichvu_A5";
                   reportDocument = Utility.GetReport("thanhtoan_Bienlai_Dichvu_A5", ref tieude, ref reportname);
                   break;

           }
           if (reportDocument == null) return;
           var crpt = reportDocument;
           var p = (from q in m_dtReportPhieuThu.AsEnumerable()
                    group q by q.Field<long>(KcbThanhtoan.Columns.IdThanhtoan) into r
                    select new
                    {
                        _key = r.Key,
                        tongtien_chietkhau_hoadon = r.Min(g => g.Field<decimal>("tongtien_chietkhau_hoadon")),
                        tongtien_chietkhau_chitiet = r.Min(g => g.Field<decimal>("tongtien_chietkhau_chitiet")),
                        tongtien_chietkhau = r.Min(g => g.Field<decimal>("tongtien_chietkhau"))
                    }).ToList();

           decimal tong = m_dtReportPhieuThu.AsEnumerable().Sum(c => c.Field<decimal>("TT_BN"));
           decimal tong_ck_hoadon = p.Sum(c => c.tongtien_chietkhau_hoadon);
           decimal tong_ck = p.Sum(c => c.tongtien_chietkhau);
           tong = tong - tong_ck;
           var objForm = new frmPrintPreview("", crpt, true, true);
           objForm.mv_sReportFileName = Path.GetFileName(reportname);
           objForm.mv_sReportCode = reportCode;
           //try
           //{
           crpt.SetDataSource(m_dtReportPhieuThu.DefaultView);
           //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "                                                                      ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
           Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
           Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
           Utility.SetParameterValue(crpt, "Telephone", globalVariables.Branch_Phone);
           Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);

           Utility.SetParameterValue(crpt, "tienmiengiam_hdon", tong_ck_hoadon);
           Utility.SetParameterValue(crpt, "tong_miengiam", tong_ck);
           Utility.SetParameterValue(crpt, "tongtien_bn", tong);

           //  Utility.SetParameterValue(crpt,"DateTime", Utility.FormatDateTime(dtCreateDate.Value));
           Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(NgayInPhieu));
           Utility.SetParameterValue(crpt, "sTitleReport", tieude);
           Utility.SetParameterValue(crpt, "sMoneyCharacter",
                                  new MoneyByLetter().sMoneyToLetter(Utility.sDbnull(tong)));
           Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
           Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, NGAYINPHIEU));
           objForm.crptViewer.ReportSource = crpt;
           if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai, PropertyLib._MayInProperties.PreviewInBienlai))
           {
               objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
               objForm.ShowDialog();
           }
           else
           {
               objForm.addTrinhKy_OnFormLoad();
               crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
               crpt.PrintToPrinter(1, false, 0, 0);
           }
       }
       MoneyByLetter _moneyByLetter=new MoneyByLetter();
       /// <summary>
       /// HÀM THỰC HIỆN VIỆC IN PHIẾU BIÊN LAI CHO BẢO HIỂM Y TẾ
       /// </summary>
       /// <param name="m_dtReportPhieuThu"></param>
       /// <param name="sTitleReport"></param>
       /// <param name="ngayIn"></param>
       public void INPHIEU_BIENLAI_BHYT(DataTable m_dtReportPhieuThu,DateTime ngayIn, string khogiay)
       {
          
           if(m_dtReportPhieuThu.Rows.Count<=0)
           {
               Utility.ShowMsg("Không tìm thấy bản ghi nào","Thông báo",MessageBoxIcon.Warning);
               return;
           }
           Utility.UpdateLogotoDatatable(ref m_dtReportPhieuThu);
          // THU_VIEN_CHUNG.Sapxepthutuin(ref m_dtReportPhieuThu,true);
           m_dtReportPhieuThu.DefaultView.Sort = "stt_in ASC";
           m_dtReportPhieuThu.AcceptChanges();
           decimal tong = m_dtReportPhieuThu.AsEnumerable().Sum(c => c.Field<decimal>("TT_BN"));
           string tieude = "", reportname = "", reportcode = "thanhtoan_Bienlai_BHYT_A4_new";
           ReportDocument crpt = Utility.GetReport("thanhtoan_Bienlai_BHYT_A4_new" ,ref tieude,ref reportname);
           switch (khogiay)
           {
               case "A4":
                   reportcode = "thanhtoan_Bienlai_BHYT_A4_new";
                   crpt = Utility.GetReport("thanhtoan_Bienlai_BHYT_A4_new" ,ref tieude,ref reportname);
                   break;
               case "A5":
                   reportcode = "thanhtoan_Bienlai_BHYT_A5_new";
                   crpt = Utility.GetReport("thanhtoan_Bienlai_BHYT_A5_new" ,ref tieude,ref reportname);
                   break;

           }
           if (crpt == null) return;
           var objForm = new frmPrintPreview(tieude, crpt, true, true);
           //try
           //{
           var p = (from q in m_dtReportPhieuThu.AsEnumerable()
                    group q by q.Field<long>(KcbThanhtoan.Columns.IdThanhtoan) into r
                    select new
                    {
                        _key = r.Key,
                        tongtien_chietkhau_hoadon = r.Min(g => g.Field<decimal>("tongtien_chietkhau_hoadon")),
                        tongtien_chietkhau_chitiet = r.Min(g => g.Field<decimal>("tongtien_chietkhau_chitiet")),
                        tongtien_chietkhau = r.Min(g => g.Field<decimal>("tongtien_chietkhau"))
                    }).ToList();

           
           decimal tong_ck_hoadon = p.Sum(c => c.tongtien_chietkhau_hoadon);
           decimal tong_ck = p.Sum(c => c.tongtien_chietkhau);
           tong = tong - tong_ck;
           objForm.mv_sReportFileName = Path.GetFileName(reportname);
           objForm.mv_sReportCode = reportcode;
           crpt.SetDataSource(m_dtReportPhieuThu.DefaultView);
           //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "                                                                      ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
           Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
           Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
           Utility.SetParameterValue(crpt,"Telephone", globalVariables.Branch_Phone);
           Utility.SetParameterValue(crpt,"Address", globalVariables.Branch_Address);

           Utility.SetParameterValue(crpt, "tienmiengiam_hdon", tong_ck_hoadon);
           Utility.SetParameterValue(crpt, "tong_miengiam", tong_ck);
           Utility.SetParameterValue(crpt, "tongtien_bn", tong);


           //  Utility.SetParameterValue(crpt,"DateTime", Utility.FormatDateTime(dtCreateDate.Value));
           Utility.SetParameterValue(crpt,"CurrentDate", Utility.FormatDateTime(globalVariables.SysDate));
           Utility.SetParameterValue(crpt, "sTitleReport", tieude);
           Utility.SetParameterValue(crpt,"sMoneyCharacter",
                                  new MoneyByLetter().sMoneyToLetter(Utility.sDbnull(tong)));
           Utility.SetParameterValue(crpt,"BottomCondition", THU_VIEN_CHUNG.BottomCondition());
           Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, NGAYINPHIEU));
           objForm.crptViewer.ReportSource = crpt;
           
           if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai, PropertyLib._MayInProperties.PreviewInBienlai))
           {
               objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
               objForm.ShowDialog();
           }
           else
           {
               objForm.addTrinhKy_OnFormLoad();
               crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
               crpt.PrintToPrinter(1, false, 0, 0);
           }
       }
       private decimal SumOfTotal_BH(DataTable dataTable, string sFildName)
       {
           return Utility.DecimaltoDbnull(dataTable.Compute("SUM(" + sFildName + ")", "1=1"), 0);
       }
       public void InBienlai(bool IsTongHop, int _Payment_ID, KcbLuotkham objLuotkham)
       {
          
           try
           {
               ActionResult actionResult = new KCB_THANHTOAN().Capnhattrangthaithanhtoan(_Payment_ID);
               if (actionResult == ActionResult.Success)
               {
                   switch (objLuotkham.MaDoituongKcb)
                   {
                       case "DV":
                           Inbienlai_Dichvu(_Payment_ID, IsTongHop);
                           break;
                       case "BHYT":
                           Inbienlai_BHYT(_Payment_ID, IsTongHop);
                           break;
                       default:
                           Inbienlai_Dichvu(_Payment_ID, IsTongHop);
                           break;
                   }
               }
           }
           catch (Exception ex)
           {
               Utility.ShowMsg(string.Format("Lỗi trong quá trình in phiếu dịch vụ ={0}", ex.ToString()));
              
           }
           finally
           {
              
           }

       }
       public void InBienlaiQuaythuoc(bool IsTongHop, int _Payment_ID)
       {

           try
           {
               ActionResult actionResult = new KCB_THANHTOAN().Capnhattrangthaithanhtoan(_Payment_ID);
               if (actionResult == ActionResult.Success)
               {

                   Inbienlai_quaythuoc(_Payment_ID, IsTongHop);
                           
               }
           }
           catch (Exception ex)
           {
               Utility.ShowMsg(string.Format("Lỗi trong quá trình in phiếu dịch vụ ={0}", ex.ToString()));

           }
           finally
           {

           }
       }
        void Inbienlai_Dichvu(int payment_id, bool IsTongHop)
       {
           try
           {
               KcbThanhtoan objPayment = KcbThanhtoan.FetchByID(payment_id);
               if (IsTongHop) objPayment.IdThanhtoan = -1;
               ///lấy thông tin vào phiếu thu
              DataTable m_dtReportPhieuThu = new KCB_THANHTOAN().LaythongtininbienlaiDichvu(objPayment);
               THU_VIEN_CHUNG.Sapxepthutuin(ref m_dtReportPhieuThu, false);
               m_dtReportPhieuThu.DefaultView.Sort = "stt_in ,stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu";

               THU_VIEN_CHUNG.CreateXML(m_dtReportPhieuThu, Application.StartupPath + @"\Xml4Reports\Thanhtoan_InBienLai_DV.XML");
               if (m_dtReportPhieuThu.Rows.Count <= 0)
               {
                   Utility.ShowMsg("Không tìm thấy dữ liệu in phiếu (KCB_THANHTOAN_LAYTHONGTIN_INPHIEU_DICHVU)", "Thông báo");
                   return;
               }
              INPHIEU_DICHVU(m_dtReportPhieuThu, globalVariables.SysDate,  PropertyLib._MayInProperties.CoGiayInBienlai == Papersize.A4 ? "A4" : "A5");
              
           }
           catch
           {
           }
       }
        void Inbienlai_quaythuoc(int payment_id, bool IsTongHop)
        {
            try
            {
                KcbThanhtoan objPayment = KcbThanhtoan.FetchByID(payment_id);
                if (IsTongHop) objPayment.IdThanhtoan = -1;
                ///lấy thông tin vào phiếu thu
                DataTable m_dtReportPhieuThu = new KCB_THANHTOAN().LaythongtininbienlaiDichvu(objPayment);
                THU_VIEN_CHUNG.Sapxepthutuin(ref m_dtReportPhieuThu, false);
                m_dtReportPhieuThu.DefaultView.Sort = "stt_in ,stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu";

                THU_VIEN_CHUNG.CreateXML(m_dtReportPhieuThu, Application.StartupPath + @"\Xml4Reports\Thanhtoan_InBienLai_DV.XML");
                if (m_dtReportPhieuThu.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu in phiếu (KCB_THANHTOAN_LAYTHONGTIN_INPHIEU_DICHVU)", "Thông báo");
                    return;
                }
                INBIENLAI_QUAYTHUOC(m_dtReportPhieuThu, globalVariables.SysDate, PropertyLib._MayInProperties.CoGiayInBienlai == Papersize.A4 ? "A4" : "A5");

            }
            catch
            {
            }
        }
       private void Inbienlai_BHYT(int payment_id, bool IsTongHop)
       {
           try
           {

               KcbThanhtoan objPayment = KcbThanhtoan.FetchByID(payment_id);
               if (IsTongHop) objPayment.IdThanhtoan = -1;
               ///lấy thông tin vào phiếu thu
              DataTable m_dtReportPhieuThu =
                   new KCB_THANHTOAN().LaythongtininbienlaiDichvu(objPayment);
               THU_VIEN_CHUNG.Sapxepthutuin(ref m_dtReportPhieuThu, true);
               m_dtReportPhieuThu.DefaultView.Sort = "stt_in ,stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu";
               if (m_dtReportPhieuThu.Rows.Count <= 0)
               {
                   Utility.ShowMsg("Không tìm thấy bản ghi ", "Thông báo");
                   return;
               }
               ///hàm thực hiện xử lý thôgn tin của phiếu dịch vụ
              INPHIEU_BIENLAI_BHYT(m_dtReportPhieuThu, globalVariables.SysDate, PropertyLib._MayInProperties.CoGiayInBienlai == Papersize.A5 ? "A5" : "A4");
              
           }
           catch (Exception ex)
           {
               Utility.ShowMsg("Lỗi:"+ ex.Message);
           }

       }
       public void IN_HOADON(int payment_ID)
       {
           string LyDoIn = "0";
           try
           {
               DataTable dtPatientPayment = new KCB_THANHTOAN().Laythongtinhoadondo(payment_ID);
               THU_VIEN_CHUNG.CreateXML(dtPatientPayment, "thanhtoan_Hoadondo.xml");
               Utility.UpdateLogotoDatatable(ref dtPatientPayment);
               dtPatientPayment.Rows[0]["sotien_bangchu"] =
                   new MoneyByLetter().sMoneyToLetter(Utility.sDbnull(dtPatientPayment.Rows[0]["TONG_TIEN"]));
               string tieude = "", reportname = "";
               ReportDocument report = Utility.GetReport("thanhtoan_Hoadondo", ref tieude, ref reportname);
               if (report == null) return;
               var objForm = new frmPrintPreview("", report, true, true);
               //objForm.AutoClose = true;
               objForm.mv_sReportFileName = Path.GetFileName(reportname);
               objForm.mv_sReportCode = "thanhtoan_Hoadondo";
               report.SetDataSource(dtPatientPayment);
               Utility.SetParameterValue(report,"NGUOIIN", Utility.sDbnull(globalVariables.gv_strTenNhanvien, ""));

               Utility.SetParameterValue(report,"ParentBranchName", globalVariables.ParentBranch_Name);
               Utility.SetParameterValue(report,"BranchName", globalVariables.Branch_Name);
               Utility.SetParameterValue(report, "DateTime", Utility.FormatDateTime(Convert.ToDateTime(dtPatientPayment.Rows[0]["ngay_thanhtoan"])));
               Utility.SetParameterValue(report, "CurrentDate", Utility.FormatDateTimeWithLocation(Convert.ToDateTime(dtPatientPayment.Rows[0]["ngay_thanhtoan"]), globalVariables.gv_strDiadiem));
               Utility.SetParameterValue(report, "sTitleReport", tieude);
              // Utility.SetParameterValue(report, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName,globalVariables.SysDate));
               Utility.SetParameterValue(report, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, Convert.ToDateTime(dtPatientPayment.Rows[0]["ngay_thanhtoan"])));
               objForm.crptViewer.ReportSource = report;
              
               if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInHoadon, PropertyLib._MayInProperties.PreviewInHoadon))
               {
                   objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInHoadon, 1);
                   objForm.ShowDialog();
               }
               else
               {
                   objForm.addTrinhKy_OnFormLoad();
                   report.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInHoadon;
                   report.PrintToPrinter(1, false, 0, 0);
               }
              
           }
           catch (Exception ex1)
           {
               Utility.CatchException(ex1);
           }

       }
       public void InPhieuchi(long payment_ID)
       {
           string LyDoIn = "0";
           try
           {
               DataTable dtPatientPayment = new KCB_THANHTOAN().KcbThanhtoanLaythongtinphieuchi(payment_ID);
               THU_VIEN_CHUNG.CreateXML(dtPatientPayment, "thanhtoan_phieuchi.xml");
               dtPatientPayment.Rows[0]["sotien_bangchu"] =
                   new MoneyByLetter().sMoneyToLetter(Utility.sDbnull(dtPatientPayment.Rows[0]["SO_TIEN"]));
               string tieude = "", reportname = "";
               ReportDocument report = Utility.GetReport("thanhtoan_phieuchi", ref tieude, ref reportname);
               if (report == null) return;
               var objForm = new frmPrintPreview("", report, true, true);
               objForm.mv_sReportFileName = Path.GetFileName(reportname);
               objForm.mv_sReportCode = "thanhtoan_phieuchi";
               report.SetDataSource(dtPatientPayment);
               Utility.SetParameterValue(report,"NGUOIIN", Utility.sDbnull(globalVariables.gv_strTenNhanvien, ""));

               Utility.SetParameterValue(report,"ParentBranchName", globalVariables.ParentBranch_Name);
               Utility.SetParameterValue(report,"BranchName", globalVariables.Branch_Name);
               Utility.SetParameterValue(report,"DateTime", Utility.FormatDateTime(globalVariables.SysDate));
               Utility.SetParameterValue(report,"sCurrentDate", Utility.FormatDateTimeWithLocation(globalVariables.SysDate, globalVariables.gv_strDiadiem));
               Utility.SetParameterValue(report, "sTitleReport", tieude);
               //Utility.SetParameterValue(report,"CharacterMoney", new MoneyByLetter().sMoneyToLetter(TONG_TIEN.ToString()));
               objForm.crptViewer.ReportSource = report;

               if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInHoadon, PropertyLib._MayInProperties.PreviewInHoadon))
               {
                   objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInHoadon, 1);
                   objForm.ShowDialog();
               }
               else
               {
                   objForm.addTrinhKy_OnFormLoad();
                   report.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInHoadon;
                   report.PrintToPrinter(1, false, 0, 0);
               }
           }
           catch (Exception ex1)
           {
               Utility.ShowMsg("Lỗi khi thực hiện in hóa đơn mẫu. Liên hệ IT để được trợ giúp-->" +
                               ex1.Message);
           }

       }
       public void InphieuDCT_Benhnhan(KcbLuotkham objLuotkham, DataTable dtDataPayment)
       {
           try
           {
               if (objLuotkham != null)
               {

                  
                   ActionResult actionResult = ActionResult.Success;
                   if (PropertyLib._ThanhtoanProperties.TaodulieuDCTkhiInphieuDCT)
                   {
                       KcbPhieuDct objPhieuDct = CreatePhieuDongChiTra(objLuotkham, dtDataPayment);
                       actionResult = new KCB_THANHTOAN().UpdatePhieuDCT(objPhieuDct, objLuotkham);
                   }
                   
                   switch (actionResult)
                   {
                       case ActionResult.Success:
                           DataTable dtData =    SPs.BhytLaydulieuinphieudct(objLuotkham.MaLuotkham,
                                                Utility.Int32Dbnull(objLuotkham.IdBenhnhan)).GetDataSet().Tables[
                                                    0];
                           if (dtData.Rows.Count <= 0)
                           {
                               Utility.ShowMsg("Không tìm thấy thông tin phiếu đồng chi trả. Bạn cần kiểm tra xem đã in phôi BHYT chưa?", "Thông báo", MessageBoxIcon.Warning);
                               return;
                           }
                           if(Utility.DecimaltoDbnull( dtData.Compute("SUM(so_tien)","1=1"),0)>0)
                           new VNS.HIS.Classes.INPHIEU_THANHTOAN_NGOAITRU().
                               INPHIEU_DONGCHITRA(dtData, globalVariables.SysDate, "PHIẾU THU ĐỒNG CHI TRẢ");
                           else
                               Utility.ShowMsg("Bệnh nhân này đã được BHYT chi trả 100% nên không cần in phiếu đồng chi trả", "Thông báo lỗi", MessageBoxIcon.Error);
                           break;
                       case ActionResult.Error:
                           Utility.ShowMsg("Lỗi trong quá trình cập nhập thông tin đồng  chi trả", "Thông báo lỗi", MessageBoxIcon.Error);
                           break;
                   }


               }

           }
           catch (Exception exception)
           {

           }
       }

       private void GetChanDoanPhu( string IDC_Phu, ref string ICD_Name, ref string ICD_Code)
       {
           try
           {
               List<string> lstICD = IDC_Phu.Split(',').ToList();
               //DmucBenhCollection _list =
               //    new DmucBenhController().FetchByQuery(
               //        DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, lstICD));
               // Cách này cho hạn chế phi vào DB 
               foreach (var row in lstICD)
               {
                   var item = (from p in globalVariables.gv_dtDmucBenh.AsEnumerable()
                       where row != null && p[DmucBenh.Columns.MaBenh].Equals(row)
                       select p).FirstOrDefault();
                   if (item != null)
                   {
                       ICD_Name += item["ten_benh"].ToString() + ";";
                       ICD_Code += item["ma_benh"].ToString() + ";";
                   }
               }
               //foreach (DmucBenh _item in _list)
               //{
               //    ICD_Name += _item.TenBenh + ";";
               //    ICD_Code += _item.MaBenh + ";";
               //}
               //_list =
               //    new DmucBenhController().FetchByQuery(
               //        DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, lstICD));
               //foreach (DmucBenh _item in _list)
               //{
               //    ICD_Name += _item.TenBenh + ";";
               //    ICD_Code += _item.MaBenh + ";";
               //}
               if (ICD_Name.Trim() != "") ICD_Name = ICD_Name.Substring(0, ICD_Name.Length - 1);
               if (ICD_Code.Trim() != "") ICD_Code = ICD_Code.Substring(0, ICD_Code.Length - 1);
           }
           catch
           {
           }
       }
       public bool InPhoiBHYT(KcbLuotkham objLuotkham, DataTable m_dtPayment,DateTime ngayIn)
       {
           try
           {
               KcbPhieuDct objPhieuDct = CreatePhieuDongChiTra(objLuotkham, m_dtPayment);
               objPhieuDct.NgayTao = ngayIn;
               ActionResult actionResult =new KCB_THANHTOAN().UpdatePhieuDCT(objPhieuDct, objLuotkham);
               if (actionResult == ActionResult.Success) //Tránh trường hợp in ra phôi mà ko đẩy vào CSDL
               {
                  DataTable mDtReportPhieuThu =
                       new KCB_THANHTOAN().LaythongtinInphoiBHYT(-1, Utility.sDbnull(objLuotkham.MaLuotkham, ""),
                           Utility.Int32Dbnull(objLuotkham.IdBenhnhan, -1), 0);
                   THU_VIEN_CHUNG.CreateXML(mDtReportPhieuThu);
                   ///load thông tin của việc lấy dữ liệu vào datatable trong phiếu thanh toán)

                   if (mDtReportPhieuThu.Rows.Count <= 0)
                   {
                       Utility.ShowMsg("Không tìm thấy dữ liệu để in phôi BHYT ", "Thông báo");
                       return false;
                   }
                   string ICD_Name = "";
                   string ICD_Code = "";
                   if (mDtReportPhieuThu.Rows.Count > 0)
                       GetChanDoanPhu(Utility.sDbnull(mDtReportPhieuThu.Rows[0]["ma_benhphu"], ""), ref ICD_Name, ref ICD_Code);
                   //foreach (DataRow dr in m_dtReportPhieuThu.Rows)
                   //{
                   //    dr["chuan_doanphu"] = Utility.sDbnull(dr["chuan_doanphu"]).Trim() == ""
                   //                          ? ICD_Name
                   //                          : Utility.sDbnull(dr["chuan_doanphu"]) + ";" + ICD_Name;
                   //    //dr[DmucBenh.Columns.MaBenh] = ICD_Code;
                   //   // dr["ma_icd"] = ICD_Code;
                   //}
                   //
                   foreach (DataRow drv in mDtReportPhieuThu.Rows)
                   {

                       drv["chuan_doanphu"] = Utility.sDbnull(drv["chuan_doanphu"]).Trim() == ""
                                         ? ICD_Name
                                         : Utility.sDbnull(drv["chuan_doanphu"]) + ";" + ICD_Name;
                       if (drv[KcbThanhtoanChitiet.Columns.IdLoaithanhtoan].ToString() == "1"//Chi phí KCB
                           || drv[KcbThanhtoanChitiet.Columns.IdLoaithanhtoan].ToString() == "0"//Phí KCB kèm theo
                           || drv[KcbThanhtoanChitiet.Columns.IdLoaithanhtoan].ToString() == "4"//Buồng giường
                           || drv[KcbThanhtoanChitiet.Columns.IdLoaithanhtoan].ToString() == "8"//Gói dịch vụ
                           )
                       {

                           drv["ten_loaidichvu"] = string.Empty;
                           drv["STT"] = 1;
                           drv["id_loaidichvu"] = -1;
                       }
                       else if (drv[KcbThanhtoanChitiet.Columns.IdLoaithanhtoan].ToString() == "2")
                       {
                           string maLoaidichvu = Utility.sDbnull(drv["id_loaidichvu"], -1);
                            //drv["id_loaidichvu"]-->Được xác định trong câu truy vấn
                           var objService = (from p in globalVariables.gv_dtDmucChung.AsEnumerable()
                               where
                                   p[DmucChung.Columns.Loai].Equals("LOAIDICHVUCLS") &&
                                   p[DmucChung.Columns.Ma].Equals(maLoaidichvu)
                               select p).FirstOrDefault();
                          // DmucChung objService = THU_VIEN_CHUNG.LaydoituongDmucChung("LOAIDICHVUCLS", maLoaidichvu);
                           if (objService != null)
                           {
                               drv["ten_loaidichvu"] = Utility.sDbnull(objService["ten"].ToString());
                               drv["STT"] = Utility.sDbnull(objService["stt_hthi"]);
                           }

                       }
                       else if (drv[KcbThanhtoanChitiet.Columns.IdLoaithanhtoan].ToString() == "3")
                       {
                           int drugId = Utility.Int32Dbnull(drv["id_dichvu"], -1);
                           var objDrug = (from p in globalVariables.gv_dtDanhMucThuoc.AsEnumerable()
                               where p[DmucThuoc.Columns.IdThuoc].Equals(drugId)
                               select p).FirstOrDefault();
                           var objLoaithuoc = (from p in globalVariables.gv_dtLoaiThuoc.AsEnumerable()
                               where objDrug != null && p[DmucLoaithuoc.Columns.IdLoaithuoc].Equals(objDrug["id_loaithuoc"].ToString())
                               select p).FirstOrDefault();
                           //DmucThuoc objDrug = DmucThuoc.FetchByID(drugId);
                         //  DmucLoaithuoc objLoaithuoc = DmucLoaithuoc.FetchByID(objDrug.IdLoaithuoc);
                           if (objLoaithuoc != null)
                               if (objDrug != null) objDrug["kieu_thuocvattu"] = objLoaithuoc["kieu_thuocvattu"];
                           //LDrugType objLDrugType = LDrugType.FetchByID(objDrug.DrugTypeId);
                           if (objDrug != null && objDrug["kieu_thuocvattu"].ToString() == "THUOC")
                           {
                               drv["id_loaidichvu"] = "THUOC";
                               drv["STT"] = 1;
                               drv["ten_loaidichvu"] = "3.1 Trong danh mục BHYT";
                           }
                           else
                           {
                               drv["id_loaidichvu"] = "VTTH";
                               drv["STT"] = 2;
                               drv["ten_loaidichvu"] = "Vật tư tiêu hao";
                           }
                       }
                       else if (drv[KcbThanhtoanChitiet.Columns.IdLoaithanhtoan].ToString() == "5")
                       {
                           drv["id_loaidichvu"] = "VTTH";
                           drv["STT"] = 2;
                           drv["ten_loaidichvu"] = "Vật tư tiêu hao";
                       }
                       if (drv[KcbThanhtoanChitiet.Columns.IdLoaithanhtoan].ToString() == "11")
                       {
                           drv["id_loaidichvu"] = 1;
                           drv["STT"] = 1;
                           drv["ten_loaidichvu"] = "Chi phí thêm  ";
                       }
                   }
                   mDtReportPhieuThu.AcceptChanges();
                   new INPHIEU_THANHTOAN_NGOAITRU(objPhieuDct.NgayTao).INPHOI_BHYT(
                       mDtReportPhieuThu, ngayIn, objLuotkham);
                   
               }
               return actionResult == ActionResult.Success;
           }
           catch(Exception ex)
           {
               Utility.ShowMsg("Lỗi khi thực hiện in phôi BHYT\n"+ex.Message);
               return false;
           }
       }
       private KcbPhieuDct CreatePhieuDongChiTra(KcbLuotkham objLuotkham, DataTable m_dtPayment)
       {
           KcbPhieuDct objPhieuDct = new KcbPhieuDct();
           objPhieuDct.MaPhieuDct = Utility.sDbnull(THU_VIEN_CHUNG.TaomaDongChiTra(globalVariables.SysDate));
           objPhieuDct.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
           objPhieuDct.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan);
           objPhieuDct.LoaiThanhtoan = 0;
           objPhieuDct.NguoiTao = globalVariables.UserName;
           objPhieuDct.TrangthaiXml = 0;
           objPhieuDct.NgayTao = globalVariables.SysDate;
           objPhieuDct.IpMaytao = globalVariables.gv_strIPAddress;
           objPhieuDct.MatheBhyt = Utility.sDbnull(objLuotkham.MatheBhyt,"");
           objPhieuDct.MaKhoaThuchien = Utility.sDbnull(objLuotkham.MaKhoaThuchien,"");
           objPhieuDct.TenMaytao = globalVariables.gv_strComputerName;
           objPhieuDct.TongTien = (decimal)m_dtPayment.Compute("SUM(TT_DCT)", "1=1");// Utility.DecimaltoDbnull(txtSoTienGoc.Text);
           objPhieuDct.BnhanChitra = (decimal)m_dtPayment.Compute("SUM(BN_CT)", "1=1"); //Utility.DecimaltoDbnull(txtTienBNCT.Text);
           objPhieuDct.BhytChitra = (decimal)m_dtPayment.Compute("SUM(BHYT_CT)", "1=1"); //Utility.DecimaltoDbnull(txtTienBHCT.Text);
           objPhieuDct.TrangthaiXml = 0;
           return objPhieuDct;

       }
    }
}
