using System;
using System.Collections.Generic;
using System.Data;
using System.Transactions;
using NLog;
using SubSonic;
using VNS.HIS.DAL;
using VNS.Libs;
using IsolationLevel = System.Transactions.IsolationLevel;

namespace VMS.HIS.KSK.Classess
{
  public class BussinessKSK
    {
        public delegate void OnDoing(int value);
        public event OnDoing _OnDoing;
        public static ActionResult Thembenhnhan(KcbDanhsachBenhnhan objdanhsachbenhnhan, KcbLuotkham objluotkham,
                                                KcbChidinhcl objchidinh, KcbDangkyKcb objDangkyKcb,
                                                KcbChidinhclsChitiet[] objchidinhchitiet, 
                                                ref string errMsg)
        {
            Logger log = LogManager.GetCurrentClassLogger();
            try
            {
                var option = new TransactionOptions
                {IsolationLevel = IsolationLevel.Snapshot, Timeout = TimeSpan.FromMinutes(5)};
                using (var trans = new TransactionScope())
                {
                    using (var shs = new SharedDbConnectionScope())
                    {
                        log.Trace("1.Bắt đầu thêm mới bệnh nhân " + objdanhsachbenhnhan.TenBenhnhan + "");
                        string PatientCode =
                            THU_VIEN_CHUNG.KCB_SINH_MALANKHAM(
                                (byte) (Utility.Int16Dbnull(objdanhsachbenhnhan.KieuBenhnhan, 1)));

                        // Them moi thong tin benh nhan
                        SqlQuery objdbenhnhan = new Select().From(KcbDanhsachBenhnhan.Schema)
                            .Where(KcbDanhsachBenhnhan.Columns.IdBenhnhan).IsEqualTo(objdanhsachbenhnhan.IdBenhnhan);
                        if (objdbenhnhan.GetRecordCount() > 0)
                        {
                            objdanhsachbenhnhan.MarkOld();
                            objdanhsachbenhnhan.IsLoaded = true;
                        }
                        else
                        {
                            objdanhsachbenhnhan.IsNew = true;
                        }
                        objdanhsachbenhnhan.Save();
                        log.Trace("2. Đã thêm mới Bệnh nhân");
                        // them moi lan kham cho benh nhan
                        objluotkham.MaLuotkham = Utility.sDbnull(PatientCode, "");
                        SqlQuery sqlPatientExam = new Select().From(KcbLuotkham.Schema).Where(
                            KcbLuotkham.Columns.MaLuotkham)
                            .IsEqualTo(objluotkham.MaLuotkham);
                        if (sqlPatientExam.GetRecordCount() > 0)
                        {
                            objluotkham.MarkOld();
                            objluotkham.IsLoaded = true;
                        }
                        else
                        {
                            objluotkham.IsNew = true;
                            objluotkham.MaLuotkham = PatientCode;
                            objluotkham.IdBenhnhan = objdanhsachbenhnhan.IdBenhnhan;
                        }

                        objluotkham.Save();

                        log.Trace("3. Đã thêm mới Lượt khám Bệnh nhân");
                        DataTable dtCheck =
                            SPs.SpKcbKiemtraTrungMaLuotkham(objluotkham.IdBenhnhan, objluotkham.MaLuotkham).
                                GetDataSet().Tables[0];
                        if (dtCheck != null && dtCheck.Rows.Count > 0)
                        {
                            log.Trace("3.1 Đã phát hiện trùng mã Bệnh nhân-->Lấy lại mã mới");
                            string patientCode =
                                THU_VIEN_CHUNG.KCB_SINH_MALANKHAM(
                                    (byte) (Utility.Int16Dbnull(objdanhsachbenhnhan.KieuBenhnhan, 1)));
                            SPs.SpKcbCapnhatLuotkhamMaluotkham(patientCode, objluotkham.MaLuotkham,
                                objluotkham.IdBenhnhan).Execute();
                            log.Trace("3.2 Cập  nhập mã lượt khám này được sử dụng rồi");
                            //  SPs.SpKcbCapnhatMaluotkhamLichsudoituongKcb(patientCode, objLichsuKcb.IdLichsuDoituongKcb).Execute();
                            //  log.Trace("3.2 Đã Cập nhật lại mã lượt khám mới");
                            objluotkham.MaLuotkham = patientCode;
                        }
                        SPs.SpKcbCapnhatDmucLuotkham(objluotkham.MaLuotkham,
                            (byte) (Utility.Int16Dbnull(objdanhsachbenhnhan.KieuBenhnhan, 1)), 1, 2,
                            globalVariables.UserName).Execute();

                        log.Trace("4. Đã đánh dấu mã lượt khám đã được sử dụng trong hệ thống");
                        // Them moi thong tin chi dinh cho benh nhan 
                        if (objDangkyKcb != null) 
                        {
                            SqlQuery sqldangkykcb = new Select().From(KcbDangkyKcb.Schema).Where(
                           KcbDangkyKcb.Columns.IdKham)
                           .IsEqualTo(Utility.Int64Dbnull(objDangkyKcb.IdKham, -1));
                            if (sqldangkykcb.GetRecordCount() > 0)
                            {
                                objDangkyKcb.MarkOld();
                                objDangkyKcb.IsLoaded = true;
                            }
                            else
                            {
                                objDangkyKcb.IsNew = true;
                                objDangkyKcb.MaLuotkham = PatientCode;
                                objDangkyKcb.IdBenhnhan = objdanhsachbenhnhan.IdBenhnhan;
                            }
                            objDangkyKcb.Save();

                        }
                        
                        SqlQuery sqlAssignInfo = new Select().From(KcbChidinhcl.Schema)
                            .Where(KcbChidinhcl.Columns.IdChidinh).IsEqualTo(objchidinh.IdChidinh);
                        if (objDangkyKcb != null) objchidinh.IdKham = objDangkyKcb.IdKham;
                        objchidinh.IdDoituongKcb = 3;
                        objchidinh.MaDoituongKcb = "KSK";
                        objchidinh.IdLoaidoituongKcb = 2;
                        if (sqlAssignInfo.GetRecordCount() > 0)
                        {
                         
                            objchidinh.MarkOld();
                            objchidinh.IsLoaded = true;
                        }
                        else
                        {
                            objchidinh.IsNew = true;
                            objchidinh.MaChidinh = THU_VIEN_CHUNG.SinhMaChidinhCLSKSK();
                        }
                        objchidinh.IdBenhnhan = objluotkham.IdBenhnhan;
                        objchidinh.MaLuotkham = objluotkham.MaLuotkham;
                        objchidinh.NguoiSua = globalVariables.UserName;
                        objchidinh.NgaySua = DateTime.Now;
                        if (string.IsNullOrEmpty(objchidinh.MaKhoaChidinh))
                            objchidinh.MaKhoaChidinh = Utility.sDbnull(objluotkham.MaKhoaThuchien);
                        objchidinh.Save();
                        log.Trace("5. Đã thêm mới phiếu chỉ định cho bệnh nhân");
                        if (Utility.Int32Dbnull(objchidinh.IdChidinh) > 0)
                        {
                            foreach (KcbChidinhclsChitiet objAssignDetail in objchidinhchitiet)
                            {
                                // Them chi tiet chi dinh cho benh nhan 
                                SqlQuery sqlAssignDetail = new Select().From(KcbChidinhclsChitiet.Schema)
                                    .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(
                                        objAssignDetail.IdChitietchidinh);
                                if (sqlAssignDetail.GetRecordCount() > 0)
                                {
                                    objAssignDetail.MarkOld();
                                    objAssignDetail.IsLoaded = true;
                                }
                                else
                                {
                                    objAssignDetail.IsNew = true;
                                }
                                
                                objAssignDetail.IdDoituongKcb = Utility.Int16Dbnull(objluotkham.IdDoituongKcb);
                                objAssignDetail.MaLuotkham = objchidinh.MaLuotkham;
                                objAssignDetail.IdBenhnhan = objchidinh.IdBenhnhan;
                                objAssignDetail.IdKham = objchidinh.IdKham;
                                objAssignDetail.IdChidinh = Utility.Int32Dbnull(objchidinh.IdChidinh);
                            //    objAssignDetail.IdKham = -1;
                                objAssignDetail.IpMaytao = globalVariables.gv_strIPAddress;
                                objAssignDetail.PtramBhyt =
                                    Utility.DecimaltoDbnull(objluotkham.PtramBhyt);
                                objAssignDetail.IdBacsiThuchien = globalVariables.IdKhoaNhanvien;
                               // objAssignDetail.IsNew = true;
                                objAssignDetail.Save();
                                log.Info(
                                    "Them moi thong tin cua phieu chi dinh chi tiet voi ma phieu Assign_ID=" +
                                    objchidinh.IdChidinh);
                            }
                            log.Trace("6. Đã thêm mới chi tiết phiếu chỉ định cho bệnh nhân");
                        }
                        else
                        {
                            log.Trace("7. Lỗi thêm bệnh nhân vào hệ thống");
                            return ActionResult.Error;

                        }
                    }

                    trans.Complete();
                    log.Trace("8. Thêm bệnh nhân có ID_Benhnhan " + objdanhsachbenhnhan.IdBenhnhan +
                              " vào hệ thống thành công");
                    errMsg = @"Thêm mới thành công bệnh nhân";
                    return ActionResult.Success;
                   
                }
            }
            catch (Exception ex)
            {

                errMsg = "Lỗi thêm mới bệnh nhân : " +  ex.Message;
                log.Error(ex.Message);
                return ActionResult.Error;
            }
        }
        public bool DeleteData(List<int> lstId, bool checkassignstatus, ref string eMessage)
        {
            Logger log = LogManager.GetCurrentClassLogger();
            try
            {
                int value = 0;
                using (TransactionScope trans = new TransactionScope())
                {
                    using (SharedDbConnectionScope shs = new SubSonic.SharedDbConnectionScope())
                    {
                        foreach (int _id in lstId)
                        {
                            bool allowdeleted = true;
                            if (checkassignstatus)
                            {
                                KcbChidinhclsChitietCollection lst =
                                    new Select().From(KcbChidinhclsChitiet.Schema).Where(
                                        KcbChidinhclsChitiet.Columns.TrangThai).IsGreaterThan(1).And(
                                            KcbChidinhclsChitiet.Columns.IdChidinh).In(
                                                new Select(KcbChidinhcl.Columns.IdChidinh).From(KcbChidinhcl.Schema).Where(
                                                    KcbChidinhcl.Columns.IdBenhnhan).IsEqualTo(_id)).ExecuteAsCollection
                                        <KcbChidinhclsChitietCollection>();
                                allowdeleted = lst.Count <= 0;
                            }
                            if (allowdeleted)
                            {
                                new Delete().From(KcbChidinhclsChitiet.Schema).Where(KcbChidinhclsChitiet.Columns.IdChidinh)
                                    .In(new Select(KcbChidinhcl.Columns.IdChidinh).From(KcbChidinhcl.Schema)
                                    .Where(KcbChidinhcl.Columns.IdBenhnhan).IsEqualTo(_id)).Execute();
                                new Delete().From(KcbChidinhcl.Schema)
                                    .Where(KcbChidinhcl.Columns.IdBenhnhan)
                                    .IsEqualTo(_id)
                                    .Execute();
                                new Delete().From(KcbDangkyKcb.Schema)
                                    .Where(KcbDangkyKcb.Columns.IdBenhnhan)
                                    .IsEqualTo(_id)
                                    .Execute();
                                new Delete().From(KcbLuotkham.Schema)
                                    .Where(KcbLuotkham.Columns.IdBenhnhan)
                                    .IsEqualTo(_id)
                                    .Execute();
                                new Delete().From(KcbDanhsachBenhnhan.Schema)
                                    .Where(KcbDanhsachBenhnhan.Columns.IdBenhnhan)
                                    .IsEqualTo(_id)
                                    .Execute();
                            }
                            value += 1;
                            _OnDoing(value);
                        }
                    }
                    trans.Complete();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return false;
            }
            return true;
        }
    }
}