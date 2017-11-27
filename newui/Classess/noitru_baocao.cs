using System;
using System.Data;
using System.IO;
using System.Linq;
using CrystalDecisions.CrystalReports.Engine;
using VNS.HIS.DAL;
using VNS.Libs;

namespace VNS.HIS.UI.Baocao
{
    public class noitru_baocao
    {
        public static void Inphieunhapvien(DataTable mDtReport, string sTitleReport, DateTime ngayIn)
        {
            string tieude = "", reportname = "";
            ReportDocument crpt = Utility.GetReport("noitru_phieunhapvien", ref tieude, ref reportname);
            if (crpt == null) return;
            THU_VIEN_CHUNG.CreateXML(mDtReport, "noitru_phieunhapvien.xml");
            var moneyByLetter = new MoneyByLetter();
            var objForm = new frmPrintPreview(sTitleReport, crpt, true, mDtReport.Rows.Count > 0);
            // string tinhtong = TinhTong(m_dtReport);
            Utility.UpdateLogotoDatatable(ref mDtReport);
            try
            {
                crpt.SetDataSource(mDtReport);


                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = "noitru_phieunhapvien";
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(ngayIn));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception ex)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(ex.ToString());
                }
            }
        }

        public static void InBanCamKetPhauThuat(DataTable mDtReport, string sTitleReport, DateTime ngayIn)
        {
            string tieude = "", reportname = "";
            ReportDocument crpt = Utility.GetReport("noitru_giaycamketPT_A5", ref tieude, ref reportname);
            if (crpt == null) return;
            THU_VIEN_CHUNG.CreateXML(mDtReport, "noitru_giaycamketPT_A5.xml");
            var moneyByLetter = new MoneyByLetter();
            var objForm = new frmPrintPreview(sTitleReport, crpt, true, mDtReport.Rows.Count > 0);
            // string tinhtong = TinhTong(m_dtReport);
            Utility.UpdateLogotoDatatable(ref mDtReport);
            try
            {
                crpt.SetDataSource(mDtReport);


                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = "noitru_giaycamketPT_A5";
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTimeWithThanhPho(ngayIn));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception ex)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(ex.ToString());
                }
            }
        }

        private static string TinhTong(DataTable dataTable)
        {
            string sumoftotal = Utility.sDbnull(dataTable.AsEnumerable().Sum(
                c => c.Field<decimal>(TPhieuNhapxuatthuocChitiet.ThanhTienColumn.ColumnName)));
            return sumoftotal;
        }

        private static string TinhTong(DataTable dataTable, string colName)
        {
            string sumoftotal = Utility.sDbnull(dataTable.AsEnumerable().Sum(
                c => c.Field<decimal>(colName)));
            return sumoftotal;
        }
        public static void InTricbienbanhoichan(DataTable mDtReport, string sTitleReport, DateTime ngayIn)
        {
            string tieude = "", reportname = "";
            ReportDocument crpt = Utility.GetReport("noitru_trichbienbanhoichan_A4", ref tieude, ref reportname);
            if (crpt == null) return;
            THU_VIEN_CHUNG.CreateXML(mDtReport, "noitru_trichbienbanhoichan_A4.xml");
            var moneyByLetter = new MoneyByLetter();
            var objForm = new frmPrintPreview(sTitleReport, crpt, true, mDtReport.Rows.Count > 0);
            // string tinhtong = TinhTong(m_dtReport);
            Utility.UpdateLogotoDatatable(ref mDtReport);
            try
            {
                crpt.SetDataSource(mDtReport);


                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = "noitru_trichbienbanhoichan_A4";
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTimeWithThanhPho(ngayIn));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception ex)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(ex.ToString());
                }
            }
        }
    }
}