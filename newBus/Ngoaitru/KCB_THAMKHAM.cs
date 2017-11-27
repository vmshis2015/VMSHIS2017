﻿using System;
using System.Data;
using System.Transactions;
using System.Linq;
using SubSonic;
using VNS.Libs;
using VNS.HIS.DAL;
using NLog;

namespace VNS.HIS.BusRule.Classes
{
    /// <summary>
    /// 
    /// </summary>
    public class KCB_THAMKHAM
    {
        private NLog.Logger log;
        public KCB_THAMKHAM()
        {
            log = LogManager.GetCurrentClassLogger();
        }
        public DataTable KcbTiemchungInbangketruocTiemchung(long? IdBenhnhan, string maluotkham, long? Idkham, long? Iddonthuoc, int? Idthuoc)
        {
            return SPs.KcbTiemchungInbangketruocTiemchung(IdBenhnhan, maluotkham, Idkham, Iddonthuoc, Idthuoc).GetDataSet().Tables[0];
        }
        public ActionResult DanhdautrangthaiTiem(KcbDonthuocChitiet objChitiet, long _IdKham, bool Da_tiem)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {

                        if (objChitiet != null)
                        {
                            objChitiet.IsNew = false;
                            objChitiet.DaDung = Utility.Bool2byte(Da_tiem);
                            objChitiet.MarkOld();
                            objChitiet.Save();
                        }
                        else
                        {
                            new Update(KcbDonthuocChitiet.Schema)
                                .Set(KcbDonthuocChitiet.Columns.DaDung).EqualTo(Utility.Bool2byte(Da_tiem))
                                .Where(KcbDonthuocChitiet.Columns.IdKham).IsEqualTo(_IdKham)
                                .Execute();
                        }
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi trong qua trinh chuyen vien khoi noi tru {0}", exception);
                return ActionResult.Error;
            }
        }
        public ActionResult LuuHoibenhvaChandoan(KcbChandoanKetluan objDiagInfo,KcbDonthuocChitiet objChitiet,bool luudulieutiemchung)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {

                        if (objChitiet != null)
                        {
                            objChitiet.IsNew = false;
                            if (objDiagInfo != null)
                            {
                                objChitiet.PhanungSautiem = objDiagInfo.PhanungSautiemchung;
                                objChitiet.Xutri = objDiagInfo.HuongDieutri;
                                objChitiet.KetQua = objDiagInfo.Ketluan;
                                objChitiet.KetluanNguyennhan = objDiagInfo.KetluanNguyennhan;
                            }
                            objChitiet.MarkOld();
                            objChitiet.Save();
                        }
                        if (objDiagInfo != null)
                        {
                            log.Trace("1. Bắt đầu lưu chẩn đoán của bệnh nhân: " + objDiagInfo.MaLuotkham);
                            if (objDiagInfo.IsNew)
                            {

                              //  objDiagInfo.Save();
                                SPs.SpKcbThemmoiChandoanKetluan(objDiagInfo.IdChandoan, objDiagInfo.IdKham,
                                    objDiagInfo.IdBenhnhan,
                                    objDiagInfo.MaLuotkham, objDiagInfo.IdBacsikham, objDiagInfo.NgayChandoan,
                                    objDiagInfo.NguoiTao,
                                    objDiagInfo.NgayTao, objDiagInfo.IdKhoanoitru, objDiagInfo.IdBuonggiuong,
                                    objDiagInfo.IdBuong, objDiagInfo.IdGiuong
                                    , objDiagInfo.IdPhieudieutri, objDiagInfo.Noitru, objDiagInfo.IdPhongkham,
                                    objDiagInfo.TenPhongkham, objDiagInfo.Mach, objDiagInfo.Nhietdo, objDiagInfo.Huyetap
                                    , objDiagInfo.Nhiptim, objDiagInfo.Nhiptho, objDiagInfo.Cannang,
                                    objDiagInfo.Chieucao, objDiagInfo.Nhommau, objDiagInfo.Ketluan,objDiagInfo.ChedoDinhduong,
                                    objDiagInfo.HuongDieutri, objDiagInfo.SongayDieutri, objDiagInfo.TrieuchungBandau
                                    , objDiagInfo.Chandoan, objDiagInfo.ChandoanKemtheo, objDiagInfo.MabenhChinh,
                                    objDiagInfo.MabenhPhu,objDiagInfo.MotaBenhchinh, objDiagInfo.IpMaytao, objDiagInfo.TenMaytao,
                                    objDiagInfo.PhanungSautiemchung, objDiagInfo.KPL1
                                    , objDiagInfo.KPL2, objDiagInfo.KPL3, objDiagInfo.KPL4, objDiagInfo.KPL5,
                                    objDiagInfo.KPL6, objDiagInfo.KPL7, objDiagInfo.KPL8, objDiagInfo.KL1,
                                    objDiagInfo.KL2, objDiagInfo.KL3, objDiagInfo.KetluanNguyennhan, objDiagInfo.NhanXet
                                    , objDiagInfo.ChongchidinhKhac, objDiagInfo.SoNgayhen,objDiagInfo.ChisoIbm, objDiagInfo.ThilucMp, objDiagInfo.ThilucMt, objDiagInfo.NhanapMp, objDiagInfo.NhanapMt
                                    ).Execute();
                                log.Trace("1.1 Thêm mới lưu chẩn đoán của bệnh nhân: " + objDiagInfo.MaLuotkham);
                            }
                            else
                            {
                                SPs.SpKcbCapnhatChandoanKetluan(objDiagInfo.IdChandoan, objDiagInfo.IdBacsikham,
                                    objDiagInfo.NgayChandoan, objDiagInfo.NguoiSua, objDiagInfo.NgaySua,
                                    objDiagInfo.IdPhieudieutri
                                    , objDiagInfo.Noitru, objDiagInfo.IdPhongkham, objDiagInfo.TenPhongkham,
                                    objDiagInfo.Mach, objDiagInfo.Nhietdo, objDiagInfo.Huyetap, objDiagInfo.Nhiptim,
                                    objDiagInfo.Nhiptho, objDiagInfo.Cannang, objDiagInfo.Chieucao
                                    , objDiagInfo.Nhommau, objDiagInfo.Ketluan, objDiagInfo.ChedoDinhduong, objDiagInfo.HuongDieutri,
                                    objDiagInfo.SongayDieutri, objDiagInfo.TrieuchungBandau, objDiagInfo.Chandoan,
                                    objDiagInfo.ChandoanKemtheo, objDiagInfo.MabenhChinh
                                    , objDiagInfo.MabenhPhu,objDiagInfo.MotaBenhchinh, objDiagInfo.IpMaysua, objDiagInfo.TenMaysua,
                                    objDiagInfo.PhanungSautiemchung, objDiagInfo.KPL1
                                    , objDiagInfo.KPL2, objDiagInfo.KPL3, objDiagInfo.KPL4, objDiagInfo.KPL5,
                                    objDiagInfo.KPL6, objDiagInfo.KPL7, objDiagInfo.KPL8, objDiagInfo.KL1,
                                    objDiagInfo.KL2, objDiagInfo.KL3, objDiagInfo.KetluanNguyennhan, objDiagInfo.NhanXet,
                                    objDiagInfo.ChongchidinhKhac, objDiagInfo.SoNgayhen, objDiagInfo.ChisoIbm, objDiagInfo.ThilucMp, objDiagInfo.ThilucMt, objDiagInfo.NhanapMp, objDiagInfo.NhanapMt).Execute();
                                log.Trace("1.2 Cập nhật chẩn đoán của bệnh nhân: " + objDiagInfo.MaLuotkham);
                                // objDiagInfo.MarkOld();

                                //  objDiagInfo.Save();
                            }
                            if (luudulieutiemchung && objChitiet == null)
                            {
                                new Update(KcbDonthuocChitiet.Schema)
                                    .Set(KcbDonthuocChitiet.Columns.PhanungSautiem).EqualTo(objDiagInfo.PhanungSautiemchung)
                                    .Set(KcbDonthuocChitiet.Columns.Xutri).EqualTo(objDiagInfo.HuongDieutri)
                                    .Set(KcbDonthuocChitiet.Columns.KetQua).EqualTo(objDiagInfo.Ketluan)
                                    .Set(KcbDonthuocChitiet.Columns.KetluanNguyennhan).EqualTo(objDiagInfo.KetluanNguyennhan)
                                    .Where(KcbDonthuocChitiet.Columns.IdKham).IsEqualTo(objDiagInfo.IdKham)
                                    .Execute();
                            }
                            log.Trace("2.Kết thúc lưu chẩn đoán của bệnh nhân: " + objDiagInfo.MaLuotkham);
                        }
                        log.Trace("");
                        sh.Dispose();
                    }

                    scope.Complete();
                    //  Reg_ID = Utility.Int32Dbnull(objRegExam.IdKham, -1);
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi trong qua trinh chuyen vien khoi noi tru {0}", exception);
                return ActionResult.Error;
            }
        }
        /// <summary>
        /// hàm thực hiện việc update thông tin xác nhận gói
        /// </summary>
        /// <param name="objThongtinGoiDvuBnhan"></param>
        /// <returns></returns>
     

        public ActionResult UpdateExamInfo(KcbChandoanKetluan objDiagInfo, KcbDangkyKcb objRegExam,
                                           KcbLuotkham objPatientExam)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                       
                        if (objDiagInfo != null)
                        {
                            log.Trace("1.1 Bat dau ket thuc benh nhan: " + objDiagInfo.MaLuotkham);
                            if (objDiagInfo.IsNew)
                            {
                                //  objDiagInfo.Save();
                                SPs.SpKcbThemmoiChandoanKetluan(objDiagInfo.IdChandoan, objDiagInfo.IdKham,
                                    objDiagInfo.IdBenhnhan,
                                    objDiagInfo.MaLuotkham, objDiagInfo.IdBacsikham, objDiagInfo.NgayChandoan,
                                    objDiagInfo.NguoiTao,
                                    objDiagInfo.NgayTao, objDiagInfo.IdKhoanoitru, objDiagInfo.IdBuonggiuong,
                                    objDiagInfo.IdBuong, objDiagInfo.IdGiuong
                                    , objDiagInfo.IdPhieudieutri, objDiagInfo.Noitru, objDiagInfo.IdPhongkham,
                                    objDiagInfo.TenPhongkham, objDiagInfo.Mach, objDiagInfo.Nhietdo, objDiagInfo.Huyetap
                                    , objDiagInfo.Nhiptim, objDiagInfo.Nhiptho, objDiagInfo.Cannang,
                                    objDiagInfo.Chieucao, objDiagInfo.Nhommau, objDiagInfo.Ketluan, objDiagInfo.ChedoDinhduong, 
                                    objDiagInfo.HuongDieutri, objDiagInfo.SongayDieutri, objDiagInfo.TrieuchungBandau
                                    , objDiagInfo.Chandoan, objDiagInfo.ChandoanKemtheo, objDiagInfo.MabenhChinh,
                                    objDiagInfo.MabenhPhu,objDiagInfo.MotaBenhchinh, objDiagInfo.IpMaytao, objDiagInfo.TenMaytao,
                                    objDiagInfo.PhanungSautiemchung, objDiagInfo.KPL1
                                    , objDiagInfo.KPL2, objDiagInfo.KPL3, objDiagInfo.KPL4, objDiagInfo.KPL5,
                                    objDiagInfo.KPL6, objDiagInfo.KPL7, objDiagInfo.KPL8, objDiagInfo.KL1,
                                    objDiagInfo.KL2, objDiagInfo.KL3, objDiagInfo.KetluanNguyennhan, objDiagInfo.NhanXet
                                    , objDiagInfo.ChongchidinhKhac, objDiagInfo.SoNgayhen, objDiagInfo.ChisoIbm, objDiagInfo.ThilucMp, objDiagInfo.ThilucMt, objDiagInfo.NhanapMp, objDiagInfo.NhanapMt
                                    ).Execute();

                            }
                            else
                            {
                                SPs.SpKcbCapnhatChandoanKetluan(objDiagInfo.IdChandoan, objDiagInfo.IdBacsikham,
                                    objDiagInfo.NgayChandoan, objDiagInfo.NguoiSua, objDiagInfo.NgaySua,
                                    objDiagInfo.IdPhieudieutri
                                    , objDiagInfo.Noitru, objDiagInfo.IdPhongkham, objDiagInfo.TenPhongkham,
                                    objDiagInfo.Mach, objDiagInfo.Nhietdo, objDiagInfo.Huyetap, objDiagInfo.Nhiptim,
                                    objDiagInfo.Nhiptho, objDiagInfo.Cannang, objDiagInfo.Chieucao
                                    , objDiagInfo.Nhommau, objDiagInfo.Ketluan, objDiagInfo.ChedoDinhduong, objDiagInfo.HuongDieutri,
                                    objDiagInfo.SongayDieutri, objDiagInfo.TrieuchungBandau, objDiagInfo.Chandoan,
                                    objDiagInfo.ChandoanKemtheo, objDiagInfo.MabenhChinh
                                    , objDiagInfo.MabenhPhu,objDiagInfo.MotaBenhchinh, objDiagInfo.IpMaysua, objDiagInfo.TenMaysua,
                                    objDiagInfo.PhanungSautiemchung, objDiagInfo.KPL1
                                    , objDiagInfo.KPL2, objDiagInfo.KPL3, objDiagInfo.KPL4, objDiagInfo.KPL5,
                                    objDiagInfo.KPL6, objDiagInfo.KPL7, objDiagInfo.KPL8, objDiagInfo.KL1,
                                    objDiagInfo.KL2, objDiagInfo.KL3, objDiagInfo.KetluanNguyennhan, objDiagInfo.NhanXet,
                                    objDiagInfo.ChongchidinhKhac, objDiagInfo.SoNgayhen, objDiagInfo.ChisoIbm, objDiagInfo.ThilucMp, objDiagInfo.ThilucMt, objDiagInfo.NhanapMp, objDiagInfo.NhanapMt).Execute();
                            }

                        }
                        log.Trace("1.2 Luu xong chan doan cua benh nhan: " + objDiagInfo.MaLuotkham);
                        SqlQuery sqlQuery = new Select().From(
                                                     KcbChandoanKetluan.Schema)
                               .Where(KcbChandoanKetluan.Columns.MaLuotkham).IsEqualTo(objPatientExam.MaLuotkham)
                               .And(KcbChandoanKetluan.Columns.IdBenhnhan).IsEqualTo(objPatientExam.IdBenhnhan)
                               .And(KcbChandoanKetluan.Columns.IdKham).IsEqualTo(objRegExam.IdKham).OrderAsc(
                                   KcbChandoanKetluan.Columns.NgayChandoan);

                        KcbChandoanKetluanCollection objInfoCollection = sqlQuery.ExecuteAsCollection<KcbChandoanKetluanCollection>();

                        var query = (from chandoan in objInfoCollection.AsEnumerable()
                                     let y = Utility.sDbnull(chandoan.Chandoan)
                                     where (y != "")
                                     select y).ToArray();
                        string cdchinh = string.Join(";", query);
                        //KcbChandoanKetluanCollection objInfoCollection = sqlQuery.ExecuteAsCollection<KcbChandoanKetluanCollection>();
                        var querychandoanphu = (from chandoan in objInfoCollection.AsEnumerable()
                                                let y = Utility.sDbnull(chandoan.ChandoanKemtheo)
                                                where (y != "")
                                                select y).ToArray();
                        string cdphu = string.Join(";", querychandoanphu);
                        var querybenhchinh = (from benhchinh in objInfoCollection.AsEnumerable()
                                              let y = Utility.sDbnull(benhchinh.MabenhChinh)
                                              where (y != "")
                                              select y).ToArray();
                        string mabenhchinh = string.Join(";", querybenhchinh);

                        var querybenhphu = (from benhphu in objInfoCollection.AsEnumerable()
                                            let y = Utility.sDbnull(benhphu.MabenhPhu)
                                            where (y != "")
                                            select y).ToArray();
                        string mabenhphu = string.Join(";", querybenhphu);
                        SPs.KcbLuotkhamTrangthaiketthuckham(objPatientExam.IdBenhnhan,
                            objPatientExam.MaLuotkham, objPatientExam.TrangthaiNgoaitru, objPatientExam.Noitru,
                            mabenhchinh, mabenhphu, objPatientExam.KetLuan, objPatientExam.HuongDieutri, cdchinh
                            , cdphu, objPatientExam.Locked, objPatientExam.SongayDieutri, objPatientExam.TrieuChung,
                            globalVariables.UserName, globalVariables.SysDate, globalVariables.gv_strIPAddress,
                            globalVariables.gv_strComputerName, objDiagInfo.IdBacsikham, objRegExam.TrangThai,
                            objRegExam.IdKham).Execute();

                        log.Trace("1.3 Update thanh cong trang thai cua benh nhan: " + objDiagInfo.MaLuotkham);

                        //new Update(KcbLuotkham.Schema)
                        //    .Set(KcbLuotkham.Columns.MabenhChinh).EqualTo(mabenhchinh)
                        //    .Set(KcbLuotkham.Columns.MabenhPhu).EqualTo(mabenhphu)
                        //    .Set(KcbLuotkham.Columns.KetLuan).EqualTo(objPatientExam.KetLuan)
                        //    .Set(KcbLuotkham.Columns.HuongDieutri).EqualTo(objPatientExam.HuongDieutri)
                        //    .Set(KcbLuotkham.Columns.ChanDoan).EqualTo(cdchinh)
                        //    .Set(KcbLuotkham.Columns.SongayDieutri).EqualTo(objPatientExam.SongayDieutri)
                        //    .Set(KcbLuotkham.Columns.ChandoanKemtheo).EqualTo(cdphu)
                        //    .Set(KcbLuotkham.Columns.TrieuChung).EqualTo(objPatientExam.TrieuChung)
                        //    .Set(KcbLuotkham.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                        //    .Set(KcbLuotkham.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                        //    .Set(KcbLuotkham.Columns.Locked).EqualTo(objPatientExam.Locked)
                        //    .Set(KcbLuotkham.Columns.NguoiKetthuc).EqualTo(objPatientExam.NguoiKetthuc)
                        //    .Set(KcbLuotkham.Columns.NgayKetthuc).EqualTo(objPatientExam.NgayKetthuc)
                        //    .Set(KcbLuotkham.Columns.TrangthaiNgoaitru).EqualTo(objPatientExam.TrangthaiNgoaitru)
                        //    .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objPatientExam.MaLuotkham)
                        //    .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objPatientExam.IdBenhnhan).Execute();
                        ////Tạm bỏ tránh việc bị cập nhật sai bác sĩ chỉ định nếu bác sĩ đó chỉ lưu thông tin kết luận
                        ////SPs.KcbThamkhamCappnhatBsyKham(Utility.Int32Dbnull(objRegExam.IdKham, -1), objPatientExam.MaLuotkham,
                        ////                            Utility.Int32Dbnull(objPatientExam.IdBenhnhan, -1),
                        ////                            Utility.Int32Dbnull(objDiagInfo.DoctorId, -1)).Execute();

                        //if (objRegExam != null)
                        //{
                        //    new Update(KcbDangkyKcb.Schema)
                        //        .Set(KcbDangkyKcb.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                        //        .Set(KcbDangkyKcb.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                        //        .Set(KcbDangkyKcb.Columns.IpMaysua).EqualTo(globalVariables.gv_strIPAddress)
                        //        .Set(KcbDangkyKcb.Columns.TenMaysua).EqualTo(globalVariables.gv_strComputerName)
                        //        .Set(KcbDangkyKcb.Columns.IdBacsikham).EqualTo(objDiagInfo.IdBacsikham)
                        //        .Set(KcbDangkyKcb.Columns.TrangThai).EqualTo(objRegExam.TrangThai)
                        //        .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(Utility.Int32Dbnull(objRegExam.IdKham, -1)).
                        //        Execute();
                        //}
                        sh.Dispose();
                    }

                    scope.Complete();
                    //  Reg_ID = Utility.Int32Dbnull(objRegExam.IdKham, -1);
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi trong qua trinh ket thuc benh nhan", exception);
                return ActionResult.Error;
            }
        }
       
        public ActionResult LockExamInfo(KcbLuotkham objPatientExam)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        new Update(KcbLuotkham.Schema)
                            .Set(KcbLuotkham.Columns.Locked).EqualTo(objPatientExam.Locked)
                            .Set(KcbLuotkham.Columns.NguoiKetthuc).EqualTo(objPatientExam.NguoiKetthuc)
                            .Set(KcbLuotkham.Columns.NgayKetthuc).EqualTo(objPatientExam.NgayKetthuc)
                            .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objPatientExam.MaLuotkham)
                            .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objPatientExam.IdBenhnhan).Execute();

                    }

                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi trong qua trinh lưu phiếu điều trị {0}", exception);
                return ActionResult.Error;
            }
        }
        public DataTable NoitruTimkiembenhnhan(string regFrom, string regTo, string patientName, Int16 capcuu, string maluotkham, int DepartmentID, int tthaiBuonggiuong, int chuyenkhoa)
        {
            return SPs.NoitruTimkiembenhnhan(DepartmentID, maluotkham, 1, regFrom, regTo, patientName, capcuu, tthaiBuonggiuong, chuyenkhoa).
                    GetDataSet().Tables[0];
        }

        public DataTable LayDSachBnhanThamkham(DateTime regFrom, DateTime regTo, string patientName, int Status, int SoPhieu, int DepartmentID,string LoaiBN, string MaKhoaThucHien)
        {
            return SPs.KcbThamkhamLaydanhsachBnhanChokham(regFrom, regTo, patientName, Status,
                                                      SoPhieu,
                                                     DepartmentID,LoaiBN,
                                                      MaKhoaThucHien).
                    GetDataSet().Tables[0];
        }

        public DataTable LayDSachBnhanThamkhamTiemchung(DateTime regFrom, DateTime regTo, string patientName, int Status, int SoPhieu, int DepartmentID, string LoaiBN, string MaKhoaThucHien)
        {
            return SPs.KcbThamkhamLaydanhsachBnhanTiemchungChokham(regFrom, regTo, patientName, Status,
                                                      SoPhieu,
                                                     DepartmentID, LoaiBN,
                                                      MaKhoaThucHien).
                    GetDataSet().Tables[0];
        }
        public DataTable LayThongtinBenhnhanKCB(string PatientCode, int PatientID, int RegID)
        {
            return SPs.KcbThamkhamLaythongtinBenhnhankcb(PatientCode, PatientID, RegID).
                                GetDataSet().Tables[0];
        }
        public DataTable NoitruLaythongtinBenhnhan(string PatientCode, int PatientID)
        {
            return SPs.NoitruLaythongtinbenhnhan(PatientCode, PatientID).
                                GetDataSet().Tables[0];
        }
        public DataTable NoitruTimkiemphieudieutriTheoluotkham( byte IsAdmin,string ngaylapphieu, string PatientCode, int PatientID, string idkhoanoitru, int songayhienthi)
        {
            return SPs.NoitruTimkiemphieudieutriTheoluotkham(globalVariables.UserName,IsAdmin,  ngaylapphieu, PatientCode, PatientID, idkhoanoitru, songayhienthi).
                                GetDataSet().Tables[0];
        }
        public DataTable NoitruTimkiemlichsuBuonggiuong(string PatientCode, int PatientID, string idKhoanoitru)
        {
            return SPs.NoitruTimkiemlichsuBuonggiuong( PatientCode, PatientID,idKhoanoitru).GetDataSet().Tables[0];
        }
        public DataTable NoitruTimkiemlichsuNoptientamung(string PatientCode, int PatientID, short kieutamung, int idkhoanoitru, byte noitru)
        {
            return SPs.NoitruTimkiemlichsuNoptientamung(PatientCode, PatientID,kieutamung,idkhoanoitru,noitru).GetDataSet().Tables[0];
        }
        public DataTable TimkiemBenhnhan(string PatientCode, int DepartmentId,byte noitru, int Locked)
        {
            return SPs.KcbThamkhamTimkiembenhnhan(PatientCode, DepartmentId,noitru, Locked).
                            GetDataSet().Tables[0];
        }
        public DataTable KcbLichsuKcbTimkiemBenhnhan(DateTime tungay, DateTime denngay, string maluotkham, int? idbenhnhan, string tenBenhnhan, string matheBhyt, int idbacsikham, string loaiBn)
        {
            return SPs.KcbLichsukcbTimkiembenhnhan(tungay, denngay, maluotkham, idbenhnhan, tenBenhnhan, matheBhyt, idbacsikham,loaiBn).GetDataSet().Tables[0];
        }
        public DataTable KcbLichsuKcbLuotkham(int? idbenhnhan)
        {
            return SPs.KcbLichsukcbLuotkham(idbenhnhan).GetDataSet().Tables[0];
        }
        public DataTable KcbLichsuKcbTimkiemphongkham(string Maluotkham)
        {
            return SPs.KcbLichsukcbTimkiemphongkham(Maluotkham).GetDataSet().Tables[0];
        }

        public DataTable TimkiemThongtinBenhnhansaukhigoMaBN(string PatientCODE, int DepartmentId,string makhoathien)
        {
            return SPs.KcbThamkhamTimkiemBnhanSaukhinhapmabn(PatientCODE, DepartmentId, makhoathien)
                            .GetDataSet().Tables[0];
        }
        public DataTable NoitruTimkiemThongtinBenhnhansaukhigoMaBN(string PatientCODE, int DepartmentId, string makhoathien)
        {
            return SPs.NoitruTimkiemBnhanSaukhinhapmabn(PatientCODE, DepartmentId, makhoathien)
                            .GetDataSet().Tables[0];
        }
        public DataSet LaythongtinInphieuTtatDtriNgoaitru(long IdKham, string ma_luotkham)
        {
            return SPs.KcbThamkhamLaydulieuInphieuTtatDtriNgoaitru(IdKham,ma_luotkham).GetDataSet();
        }
        public DataSet LaythongtinCLSVaThuoc(int PatientID, string PatientCode, int ExamID)
        {
            return SPs.KcbThamkhamLaythongtinclsThuocTheolankham(PatientID, PatientCode, ExamID).GetDataSet();
        }
        public DataSet NoitruLaythongtinclsThuocTheophieudieutri(int PatientID, string PatientCode, int idPhieudieutri, byte layca_dulieu_ngoaitru_chuathanhtoan, string idKhoanoitru)
        {
            return SPs.NoitruLaythongtinclsThuocTheophieudieutri(PatientID, PatientCode, idPhieudieutri, layca_dulieu_ngoaitru_chuathanhtoan,idKhoanoitru).GetDataSet();
        }
        public DataSet NoitruLayDanhsachVtthGoidichvu(int PatientID, string PatientCode)
        {
            return SPs.NoitruLaydanhsachVtthGoidichvu(PatientID, PatientCode).GetDataSet();
        }

        public DataSet InMauPhieuChuyenKhoa(string maluotkham, long idbenhnhan)
        {
            return SPs.ThamkhamInmauphieuChuyenkhoa(maluotkham, idbenhnhan).GetDataSet();
        }
        public DataSet NoitruLaythongtinVTTHTrongoi(int PatientID, string PatientCode, int idPhieudieutri, int Idgoi)
        {
            return SPs.NoitruLaythongtinvtthTrongoi(PatientID, PatientCode, idPhieudieutri,Idgoi).GetDataSet();
        }
        public DataSet KcbThamkhamLaydulieuInphieuCls(int PatientID, string PatientCode, string Machidinh, string nhomincls)
        {
            return SPs.KcbThamkhamLaydulieuInphieuCls(Machidinh, nhomincls, PatientCode, PatientID).GetDataSet();
        }
        public DataTable ClsLaokhoaInphieuChidinhCls(string AssignCode, string PatientCode, int PatientID)
        {
            return SPs.KcbThamkhamLaythongtinclsInphieuTach(AssignCode, PatientCode,
                                                            PatientID).GetDataSet().
                    Tables[0];
        }
        public DataTable LaydanhsachBenh()
        {
            return new Select().From(DmucBenh.Schema).ExecuteDataSet().Tables[0];
        }
        public DataTable LayThongtinInphieuCLS(int AssingID, int ServicePrintType)
        {
            return SPs.KcbThamkhamLaythongtinclsInphieuChung(AssingID, ServicePrintType).GetDataSet().Tables[0];
        }
        
    }
}
