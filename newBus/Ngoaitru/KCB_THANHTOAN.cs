﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using NLog;
using SubSonic;
using VNS.HIS.DAL;
using VNS.HIS.NGHIEPVU.THUOC;
using VNS.Libs;
using VNS.Properties;

namespace VNS.HIS.BusRule.Classes
{
    /// <summary>
    /// </summary>
    public class KCB_THANHTOAN
    {
        private readonly Logger log;

        public KCB_THANHTOAN()
        {
            log = LogManager.GetCurrentClassLogger();
        }

        public DataTable LayDsachBenhnhanThanhtoan(int PatientID, string patient_code, string patientName,
            DateTime fromDate, DateTime toDate, string MaDoituongKcb, int BHYT, byte? noi_tru, string KieuTimKiem,
            string MAKHOATHIEN, string loaiBN)
        {
            return SPs.KcbThanhtoanLaydanhsachBenhnhanThanhtoan(-1,
                patient_code,
                patientName,
                fromDate,
                toDate,
                MaDoituongKcb, BHYT, noi_tru,
                KieuTimKiem, MAKHOATHIEN, loaiBN).GetDataSet().Tables[0];
        }

        public DataTable LaythongtininbienlaiDichvu(int? PaymentID, string MaLuotkham, int? PatientID)
        {
            return SPs.KcbThanhtoanLaythongtinInbienlaiDv(PaymentID, MaLuotkham, PatientID).GetDataSet().Tables[0];
        }

        public DataTable LaythongtininbienlaiBHYT(int? PaymentID, string MaLuotkham, int? PatientID)
        {
            return SPs.KcbThanhtoanLaythongtinInbienlaiBhyt(PaymentID, MaLuotkham, PatientID).GetDataSet().Tables[0];
        }

        public DataTable LaythongtininbienlaiDichvu(KcbThanhtoan objThanhtoan)
        {
            return
                SPs.KcbThanhtoanLaythongtinInbienlaiDv(objThanhtoan.IdThanhtoan, objThanhtoan.MaLuotkham,
                    objThanhtoan.IdBenhnhan).GetDataSet().Tables[0];
        }

        public DataTable LaythongtininbienlaiBHYT(KcbThanhtoan objThanhtoan)
        {
            return
                SPs.KcbThanhtoanLaythongtinInbienlaiBhyt(objThanhtoan.IdThanhtoan, objThanhtoan.MaLuotkham,
                    objThanhtoan.IdBenhnhan).GetDataSet().Tables[0];
        }

        public ActionResult HuyThanhtoan(KcbThanhtoan objPayment, KcbLuotkham objPatientExam, string lydohuy,
            int idHdonLog, bool huyBienlai)
        {
            try
            {
                //Kiểm tra trạng thái chốt thanh toán
                var thanhtoan = new Select().From(KcbThanhtoan.Schema).Where
                    (KcbThanhtoan.IdThanhtoanColumn.ColumnName).IsEqualTo(objPayment.IdThanhtoan)
                    .And(KcbThanhtoan.TrangthaiChotColumn.ColumnName).IsEqualTo(1).ExecuteSingle<KcbThanhtoan>();
                if (thanhtoan != null)
                {
                    Utility.ShowMsg(
                        "Thanh toán đang chọn đã được chốt nên bạn không thể hủy thanh toán. Mời bạn xem lại!");
                    return ActionResult.ExistedRecord; //Để ko hiển thị lại thông báo phía client
                }
                var objChitiet = new Select().From(KcbThanhtoanChitiet.Schema)
                    .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan).IsEqualTo(objPayment.IdThanhtoan)
                    .And(KcbThanhtoanChitiet.Columns.TrangthaiHuy).IsEqualTo(1).ExecuteSingle<KcbThanhtoanChitiet>();
                if (objChitiet != null)
                {
                    Utility.ShowMsg(
                        "Thanh toán đang chọn đã có chi tiết bị trả lại tiền nên bạn không thể hủy thanh toán. Mời bạn xem lại!");
                    return ActionResult.ExistedRecord; //Để ko hiển thị lại thông báo phía client
                }
                //int reval=0;
                //StoredProcedure spcheck = SPs.KcbThanhtoanKiemtraHuyThanhtoan(objPayment.IdThanhtoan, reval);
                //spcheck.Execute();
                //reval = Utility.Int32Dbnull(spcheck.OutputValues[0], 0);
                //if (reval == 1)
                //{
                //    Utility.ShowMsg("Lần thanh toán đang chọn có chứa dịch vụ khám đã được bác sĩ thăm khám nên bạn không được phép hủy thanh toán");
                //    return ActionResult.ExistedRecord;//Để ko hiển thị lại thông báo phía client
                //}
                //else if (reval == 2)
                //{
                //    Utility.ShowMsg("Lần thanh toán đang chọn có chứa đơn thuốc đã được duyệt cấp phát. Bạn cần liên hệ bộ phận Dược hủy duyệt đơn thuốc trước khi hủy thanh toán");
                //    return ActionResult.ExistedRecord;
                //}
                //else if (reval == 3)
                //{
                //    Utility.ShowMsg("Lần thanh toán đang chọn có chứa chỉ định dịch vụ CLS đã có kết quả thực hiện nên bạn không được phép hủy thanh toán");
                //    return ActionResult.ExistedRecord;
                //}
                DataTable dtKTra = KiemtraTrangthaidonthuocTruockhihuythanhtoan(objPayment.IdThanhtoan);
                if (!Utility.Byte2Bool(objPayment.NoiTru) && dtKTra.Rows.Count > 0)
                {
                    Utility.ShowMsg(
                        "Lần thanh toán đang chọn có chứa đơn thuốc đã được duyệt cấp phát. Bạn cần liên hệ bộ phận Dược hủy duyệt đơn thuốc trước khi hủy thanh toán");
                    return ActionResult.ExistedRecord;
                }
                return HuyThongTinLanThanhToan(objPayment, objPatientExam, lydohuy, idHdonLog, huyBienlai);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return ActionResult.Exception;
            }
        }

        public ActionResult HuyThanhtoanDonthuoctaiquay(int v_id_thanhtoan, KcbLuotkham ObjPatientExam, string lydohuy,
            int IdHdonLog, bool HuyBienlai)
        {
            if (PropertyLib._ThanhtoanProperties.Hoitruockhihuythanhtoan)
                if (
                    Utility.AcceptQuestion(
                        string.Format("Bạn có muốn hủy lần thanh toán với Mã thanh toán {0}", v_id_thanhtoan),
                        "Thông báo", true))
                {
                    return HuyThongTinLanThanhToan_Donthuoctaiquay(v_id_thanhtoan, ObjPatientExam, lydohuy, IdHdonLog,
                        HuyBienlai);
                }
                else
                {
                    return ActionResult.Cancel;
                }
            return HuyThongTinLanThanhToan_Donthuoctaiquay(v_id_thanhtoan, ObjPatientExam, lydohuy, IdHdonLog,
                HuyBienlai);
        }

        public DataTable LaythongtinBenhnhan(string MaLuotkham, int? PatientID)
        {
            return SPs.KcbThanhtoanLaythongtinBenhnhanThanhtoanTheomalankham(MaLuotkham,
                PatientID).GetDataSet().Tables[0];
        }

        public DataTable LayThongtinChuaThanhtoan(string MaLuotkham, int PatientID, int HosStatus,
            string MAKHOATHIEN, string MADOITUONG)
        {
            return SPs.KcbThanhtoanLaythongtindvuChuathanhtoan(MaLuotkham, PatientID, HosStatus,
                MAKHOATHIEN, MADOITUONG).
                GetDataSet().Tables[0];
        }

        public DataTable NoitruKcbThanhtoanLaythongtindvuChuathanhtoan(string MaLuotkham, int PatientID, int HosStatus,
            string MAKHOATHIEN, string MADOITUONG)
        {
            return SPs.NoitruKcbThanhtoanLaythongtindvuChuathanhtoan(MaLuotkham, PatientID, HosStatus,
                MAKHOATHIEN, MADOITUONG).
                GetDataSet().Tables[0];
        }

        public DataTable LayThongtinDaThanhtoan(string MaLuotkham, int PatientID, int HosStatus)
        {
            return SPs.KcbThanhtoanLaythongtindvuDathanhtoan(MaLuotkham, PatientID, HosStatus).
                GetDataSet().Tables[0];
        }

        public DataTable LayHoaDonCapPhat(string UserName)
        {
            return SPs.HoadondoLaydanhsachHoadonDacapphatTheouser(UserName).GetDataSet().Tables[0];
        }

        public DataTable LaythongtinCacLanthanhtoan(string maLuotkham, int? IdBenhnhan, int? kieuThanhToan,
            byte? noi_tru, byte Loaithanhtoan, string MA_KHOA_THIEN)
        {
            return SPs.KcbThanhtoanLaydanhsachCaclanthanhtoanTheobenhnhan(maLuotkham,
                IdBenhnhan, kieuThanhToan, noi_tru, Loaithanhtoan,
                MA_KHOA_THIEN).GetDataSet().Tables[0];
        }

        public DataTable Laythongtinhoadondo(long PaymentID)
        {
            return SPs.HoadondoLaythongtinhoadonTheothanhtoan(PaymentID).GetDataSet().Tables[0];
        }

        public DataTable KcbThanhtoanLaythongtinphieuchi(long PaymentID)
        {
            return SPs.KcbThanhtoanLaythongtinphieuchi(PaymentID).GetDataSet().Tables[0];
        }

        public DataTable KtraXnhanthuoc(int IdThanhtoan)
        {
            return null; // SPs.DonthuocKiemtraxacnhanthuocTrongdon(IdThanhtoan).GetDataSet().Tables[0];
        }

        public DataTable LaythongtinInphoiBHYT(int PaymentID, string MaLuotkham, int? PatientID, int TuTuc)
        {
            return SPs.BhytLaythongtinInphoi(PaymentID, MaLuotkham, PatientID, TuTuc).GetDataSet().Tables[0];
        }
        public DataTable NoiTruLaythongtinInphoiBHYT(int PaymentID, string MaLuotkham, int? PatientID, int TuTuc)
        {
            return SPs.NoiTruBhytLaythongtinInphoi(PaymentID, MaLuotkham, PatientID, TuTuc).GetDataSet().Tables[0];
        }
        private string LayChiKhauChiTiet()
        {
            string PTramChiTiet = "KHONG";
            SqlQuery sqlQuery = new Select().From(SysSystemParameter.Schema)
                .Where(SysSystemParameter.Columns.SName).IsEqualTo("PTRAM_CHITIET");
            var objSystemParameter = sqlQuery.ExecuteSingle<SysSystemParameter>();
            if (objSystemParameter != null) PTramChiTiet = objSystemParameter.SValue;
            return PTramChiTiet;
        }

        public void XuLyChiKhauDacBietBHYT(KcbLuotkham objLuotkham, decimal ptramBHYT)
        {
            KcbThanhtoanCollection paymentCollection =
                new KcbThanhtoanController().FetchByQuery(
                    KcbThanhtoan.CreateQuery().AddWhere(KcbThanhtoan.Columns.MaLuotkham, Comparison.Equals,
                        objLuotkham.MaLuotkham).AND(KcbThanhtoan.Columns.IdBenhnhan,
                            Comparison.Equals,
                            objLuotkham.IdBenhnhan));
            foreach (KcbThanhtoan payment in paymentCollection)
            {
                KcbThanhtoanChitietCollection paymentDetailCollection =
                    new KcbThanhtoanChitietController().FetchByQuery(
                        KcbThanhtoanChitiet.CreateQuery()
                            .AddWhere(KcbThanhtoanChitiet.Columns.IdThanhtoan, Comparison.Equals, payment.IdThanhtoan)
                            .AND(KcbThanhtoanChitiet.Columns.TuTuc, Comparison.Equals, 0));
                string IsDungTuyen = "DT";
                switch (objLuotkham.MaDoituongKcb)
                {
                    case "BHYT":
                        IsDungTuyen = Utility.Int32Dbnull(objLuotkham.DungTuyen, "0") == 1 ? "DT" : "TT";
                        break;
                    default:
                        IsDungTuyen = "KHAC";
                        break;
                }
                foreach (KcbThanhtoanChitiet PaymentDetail in paymentDetailCollection)
                {
                    SqlQuery sqlQuery = new Select().From(DmucBhytChitraDacbiet.Schema)
                        .Where(DmucBhytChitraDacbiet.Columns.IdDichvuChitiet).IsEqualTo(PaymentDetail.IdChitietdichvu)
                        .And(DmucBhytChitraDacbiet.Columns.MaLoaithanhtoan).IsEqualTo(PaymentDetail.IdLoaithanhtoan)
                        .And(DmucBhytChitraDacbiet.Columns.DungtuyenTraituyen).IsEqualTo(IsDungTuyen)
                        .And(DmucBhytChitraDacbiet.Columns.MaDoituongKcb).IsEqualTo(objLuotkham.MaDoituongKcb);
                    var objDetailDiscountRate = sqlQuery.ExecuteSingle<DmucBhytChitraDacbiet>();
                    if (objDetailDiscountRate != null)
                    {
                        log.Info("Neu trong ton tai trong bang cau hinh chi tiet chiet khau void Id_Chitiet=" +
                                 PaymentDetail.IdChitiet);
                        PaymentDetail.PtramBhyt = objDetailDiscountRate.TileGiam;
                        PaymentDetail.BhytChitra = THU_VIEN_CHUNG.TinhBhytChitra(objDetailDiscountRate.TileGiam,
                            Utility.DecimaltoDbnull(
                                PaymentDetail.DonGia, 0));
                        PaymentDetail.BnhanChitra = THU_VIEN_CHUNG.TinhBnhanChitra(objDetailDiscountRate.TileGiam,
                            Utility.DecimaltoDbnull(
                                PaymentDetail.DonGia, 0));
                    }
                    else
                    {
                        PaymentDetail.PtramBhyt = ptramBHYT;
                        PaymentDetail.BhytChitra = THU_VIEN_CHUNG.TinhBhytChitra(ptramBHYT,
                            Utility.DecimaltoDbnull(
                                PaymentDetail.DonGia, 0));
                        PaymentDetail.BnhanChitra = THU_VIEN_CHUNG.TinhBnhanChitra(ptramBHYT,
                            Utility.DecimaltoDbnull(
                                PaymentDetail.DonGia, 0));
                    }
                    log.Info("Thuc hien viec cap nhap thong tin lai gia can phai xem lại gia truoc khi thanh toan");
                }
            }
        }

        private decimal TongtienKhongTutuc(List<KcbThanhtoanChitiet> lstPaymentDetail)
        {
            decimal SumOfPaymentDetail = 0;
            foreach (KcbThanhtoanChitiet paymentDetail in lstPaymentDetail)
            {
                if (paymentDetail.TuTuc == 0 && Utility.Byte2Bool(paymentDetail.TinhChiphi))
                    SumOfPaymentDetail += (Utility.Int32Dbnull(paymentDetail.SoLuong)*
                                           Utility.DecimaltoDbnull(paymentDetail.DonGia));
            }
            return SumOfPaymentDetail;
        }

        public decimal LayThongtinPtramBHYT(decimal v_decTotalMoney, KcbLuotkham objLuotkham, ref decimal PtramBHYT)
        {
            decimal TIEN_BN = 0;
            decimal BHYT_PTRAM_LUONGCOBAN =
                Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_LUONGCOBAN", "0", false), 0);
            decimal BHYT_PTRAM_TRAITUYENNOITRU =
                Utility.DecimaltoDbnull(
                    THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", false), 0m);
            SqlQuery q;
            if (THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb.Value))
            {
                ///thực hiện xem có đúng tuyến không

                if (objLuotkham.DungTuyen == 1)
                {
                    //Các đối tượng đặc biệt hưởng 100% BHYT
                    if (Utility.Byte2Bool(objLuotkham.GiayBhyt) ||
                        globalVariables.gv_strMaQuyenLoiHuongBHYT100Phantram.Contains(objLuotkham.MaQuyenloi.ToString()))
                        // objLuotkham.MaQuyenloi.ToString() == "1" || objLuotkham.MaQuyenloi.ToString() == "2")
                    {
                        TIEN_BN = 0;
                        PtramBHYT = 100;
                        log.Info("Benh nhan tuong ung voi muc =" + objLuotkham.MaQuyenloi);
                    }
                    else
                    {
                        if (BHYT_PTRAM_LUONGCOBAN > 0)
                        {
                            if (v_decTotalMoney >= objLuotkham.LuongCoban*BHYT_PTRAM_LUONGCOBAN/100)
                            {
                                PtramBHYT = objLuotkham.TrangthaiNoitru <= 0
                                    ? Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0)
                                    : Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0);
                                TIEN_BN = v_decTotalMoney*(100 - Utility.DecimaltoDbnull(PtramBHYT, 0))/100;
                                log.Info("Tổng tiền lớn hơn lương cơ bản*% quy định -->Bệnh nhân =" +
                                         objLuotkham.MaLuotkham + " phải trả " + TIEN_BN);
                            }
                            else //Tổng tiền < lương cơ bản*% quy định-->BHYT chi trả 100%
                            {
                                PtramBHYT = 100;
                                TIEN_BN = v_decTotalMoney*(100 - Utility.DecimaltoDbnull(PtramBHYT, 0))/100; //=0
                                log.Info(
                                    "BHYT chi trả 100% tiền dịch vụ do tổng tiền nhỏ hơn lương cơ bản*% quy định. Mã khám=" +
                                    objLuotkham.MaLuotkham);
                            }
                        }
                        else //Chưa khai báo % lương cơ bản-->Tính bình thường
                        {
                            if (objLuotkham.TrangthaiNoitru <= 0)
                                PtramBHYT = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0);
                            else
                                PtramBHYT = Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0);
                            TIEN_BN = v_decTotalMoney*(100 - Utility.DecimaltoDbnull(PtramBHYT, 0))/100;
                        }
                    }
                }
                else //Trái tuyến
                {
                    if (objLuotkham.TrangthaiNoitru <= 0)
                        PtramBHYT = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0);
                    else
                        PtramBHYT = (Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0)*BHYT_PTRAM_TRAITUYENNOITRU)/
                                    100;
                    TIEN_BN = v_decTotalMoney*(100 - Utility.DecimaltoDbnull(PtramBHYT))/100;
                }
            }
            else //Đối tượng dịch vụ--> PtramBhyt=0
            {
                //thường PtramBHYT=0%
                PtramBHYT = objLuotkham.TrangthaiNoitru <= 0
                    ? Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0)
                    : Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0);
                TIEN_BN = v_decTotalMoney*(100 - Utility.Int32Dbnull(PtramBHYT, 0))/100;
                ;
            }
            return TIEN_BN;
        }

        public ActionResult ThanhtoanDonthuoctaiquay(KcbThanhtoan objThanhtoan, KcbDanhsachBenhnhan objBenhnhan,
            List<KcbThanhtoanChitiet> objArrPaymentDetail, ref int id_thanhtoan, long IdHdonLog, bool Layhoadondo)
        {
            decimal PtramBHYT = 0;
            ///tổng tiền hiện tại truyền vào của lần payment đang thực hiện
            decimal v_dblTongtienDCT = 0;
            ///tổng tiền đã thanh toán
            decimal v_TotalPaymentDetail = 0;
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        ///lấy tổng số Payment của mang truyền vào của pay ment hiện tại
                        v_dblTongtienDCT = TongtienKhongTutuc(objArrPaymentDetail);
                        KcbThanhtoanCollection paymentCollection =
                            new KcbThanhtoanController()
                                .FetchByQuery(
                                    KcbThanhtoan.CreateQuery()
                                        .AddWhere
                                        //(KcbThanhtoan.Columns.MaLuotkham, Comparison.Equals, objLuotkham.MaLuotkham).AND
                                        (KcbThanhtoan.Columns.IdBenhnhan, Comparison.Equals, objBenhnhan.IdBenhnhan)
                                        .AND(KcbThanhtoan.Columns.NoiTru, Comparison.Equals, 0)
                                        .AND(KcbThanhtoan.Columns.KieuThanhtoan, Comparison.Equals, 0)
                                );
                        //Lấy tổng tiền của các lần thanh toán trước
                        int id_donthuoc = -1;
                        foreach (KcbThanhtoan Payment in paymentCollection)
                        {
                            var paymentDetailCollection = new Select().From(KcbThanhtoanChitiet.Schema)
                                .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan).IsEqualTo(Payment.IdThanhtoan)
                                .And(KcbThanhtoanChitiet.Columns.TrangthaiHuy).IsEqualTo(0).ExecuteAsCollection
                                <KcbThanhtoanChitietCollection>();

                            foreach (KcbThanhtoanChitiet paymentDetail in paymentDetailCollection)
                            {
                                if (id_donthuoc == -1) id_donthuoc = paymentDetail.IdPhieu;
                                if (paymentDetail.TuTuc == 0)
                                    v_TotalPaymentDetail += Utility.Int32Dbnull(paymentDetail.SoLuong)*
                                                            Utility.DecimaltoDbnull(paymentDetail.DonGia);
                            }
                        }

                        //LayThongtinPtramBHYT(v_dblTongtienDCT + v_TotalPaymentDetail, objLuotkham, ref PtramBHYT);
                        objThanhtoan.MaThanhtoan =
                            THU_VIEN_CHUNG.TaoMathanhtoan(Convert.ToDateTime(objThanhtoan.NgayThanhtoan));
                        objThanhtoan.IsNew = true;
                        objThanhtoan.Save();
                        if (id_donthuoc == -1) id_donthuoc = objArrPaymentDetail[0].IdPhieu;
                        KcbDonthuoc objDonthuoc = KcbDonthuoc.FetchByID(id_donthuoc);
                        var lstChitiet =
                            new Select().From(KcbDonthuocChitiet.Schema)
                                .Where(KcbDonthuocChitiet.Columns.IdDonthuoc)
                                .IsEqualTo(id_donthuoc)
                                .ExecuteAsCollection<KcbDonthuocChitietCollection>();
                        var actionResult = ActionResult.Success;
                        if (objDonthuoc != null && lstChitiet.Count > 0)
                        {
                            if (!XuatThuoc.InValiKiemTraDonThuoc(lstChitiet, 0))
                                return ActionResult.NotEnoughDrugInStock;
                            actionResult = new XuatThuoc().LinhThuocBenhNhanTaiQuay(id_donthuoc,
                                Utility.Int16Dbnull(lstChitiet[0].IdKho, 0), globalVariables.SysDate);
                            switch (actionResult)
                            {
                                case ActionResult.Success:

                                    break;
                                case ActionResult.Error:
                                    return actionResult;
                            }
                        }
                        //Tính lại Bnhan chi trả và BHYT chi trả
                        //objArrPaymentDetail = THU_VIEN_CHUNG.TinhPhamTramBHYT(objArrPaymentDetail, PtramBHYT);
                        decimal TT_BN = 0m;
                        decimal TT_BNCT = 0m;
                        decimal TT_PT = 0m;
                        decimal TT_TT = 0m;
                        decimal TT_BHYT = 0m;
                        decimal TT_Chietkhau_Chitiet = 0m;
                        foreach (KcbThanhtoanChitiet objChitietThanhtoan in objArrPaymentDetail)
                        {
                            if (!Utility.Byte2Bool(objChitietThanhtoan.TuTuc))
                                TT_BHYT += objChitietThanhtoan.BhytChitra*Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                            TT_Chietkhau_Chitiet += Utility.DecimaltoDbnull(objChitietThanhtoan.TienChietkhau, 0);
                            TT_PT += objChitietThanhtoan.PhuThu*Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                            if (Utility.Byte2Bool(objChitietThanhtoan.TuTuc))
                                TT_TT += objChitietThanhtoan.BnhanChitra*Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                            else
                                TT_BNCT += objChitietThanhtoan.BnhanChitra*Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                            TT_BN += TT_PT + TT_BNCT + TT_TT;

                            objChitietThanhtoan.IdThanhtoan = Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1);
                            objChitietThanhtoan.IsNew = true;
                            objChitietThanhtoan.Save();
                            UpdatePaymentStatus(objThanhtoan, objChitietThanhtoan);
                        }
                        objThanhtoan.TongTien = TT_BHYT + TT_BN;

                        #region Hoadondo

                        if (Layhoadondo)
                        {
                            int record = -1;
                            if (IdHdonLog > 0)
                            {
                                record =
                                    new Delete().From(HoadonLog.Schema)
                                        .Where(HoadonLog.Columns.IdHdonLog)
                                        .IsEqualTo(IdHdonLog)
                                        .Execute();
                                if (record <= 0)
                                {
                                    Utility.ShowMsg(
                                        "Có lỗi trong quá trình xóa thông tin serie hóa đơn đã hủy để cấp lại cho lần thanh toán này.");
                                    return ActionResult.Error;
                                }
                            }
                            var obj = new HoadonLog();
                            obj.IdThanhtoan = objThanhtoan.IdThanhtoan;
                            obj.TongTien = objThanhtoan.TongTien -
                                           Utility.DecimaltoDbnull(objThanhtoan.TongtienChietkhau, 0);
                            obj.IdBenhnhan = objThanhtoan.IdBenhnhan;
                            obj.MaLuotkham = objThanhtoan.MaLuotkham;
                            obj.MauHoadon = objThanhtoan.MauHoadon;
                            obj.KiHieu = objThanhtoan.KiHieu;
                            obj.IdCapphat = objThanhtoan.IdCapphat.Value;
                            obj.MaQuyen = objThanhtoan.MaQuyen;
                            obj.Serie = objThanhtoan.Serie;
                            obj.MaNhanvien = globalVariables.UserName;
                            obj.MaLydo = "0";
                            obj.NgayIn = globalVariables.SysDate;
                            obj.TrangThai = 0;
                            obj.IsNew = true;
                            obj.Save();
                            IdHdonLog = obj.IdHdonLog; //Để update lại vào bảng thanh toán
                            new Update(HoadonCapphat.Schema).Set(HoadonCapphat.Columns.SerieHientai)
                                .EqualTo(objThanhtoan.Serie)
                                .Set(HoadonCapphat.Columns.TrangThai).EqualTo(1)
                                .Where(HoadonCapphat.Columns.IdCapphat).IsEqualTo(obj.IdCapphat)
                                .Execute();
                        }

                        #endregion

                        var objPhieuthu = new KcbPhieuthu();
                        objPhieuthu.IdBenhnhan = objThanhtoan.IdBenhnhan;
                        objPhieuthu.MaLuotkham = objThanhtoan.MaLuotkham;
                        objPhieuthu.IdThanhtoan = objThanhtoan.IdThanhtoan;
                        objPhieuthu.MaPhieuthu = THU_VIEN_CHUNG.GetMaPhieuThu(globalVariables.SysDate, 0);
                        objPhieuthu.SoluongChungtugoc = 1;
                        objPhieuthu.LoaiPhieuthu = Convert.ToByte(0);
                        objPhieuthu.NgayThuchien = globalVariables.SysDate;
                        objPhieuthu.SoTien = TT_BN - TT_Chietkhau_Chitiet;
                        objPhieuthu.SotienGoc = TT_BN;
                        objPhieuthu.MaLydoChietkhau = objThanhtoan.MaLydoChietkhau;
                        objPhieuthu.TienChietkhauchitiet = TT_Chietkhau_Chitiet;
                        objPhieuthu.TienChietkhau = objThanhtoan.TongtienChietkhau;
                        objPhieuthu.TienChietkhauhoadon = objPhieuthu.TienChietkhau - objPhieuthu.TienChietkhauchitiet;
                        objPhieuthu.NguoiNop = globalVariables.UserName;
                        objPhieuthu.TaikhoanCo = "";
                        objPhieuthu.TaikhoanNo = "";
                        objPhieuthu.LydoNop = "Thu tiền bệnh nhân";
                        objPhieuthu.IdKhoaThuchien = globalVariables.idKhoatheoMay;
                        objPhieuthu.IdNhanvien = globalVariables.gv_intIDNhanvien;
                        objPhieuthu.IsNew = true;
                        objPhieuthu.Save();

                        new Update(KcbThanhtoan.Schema)
                            .Set(KcbThanhtoan.Columns.TongTien).EqualTo(TT_BHYT + TT_BN)
                            .Set(KcbThanhtoan.Columns.BnhanChitra).EqualTo(TT_BNCT)
                            .Set(KcbThanhtoan.Columns.BhytChitra).EqualTo(TT_BHYT)
                            .Set(KcbThanhtoan.Columns.PhuThu).EqualTo(TT_PT)
                            .Set(KcbThanhtoan.Columns.TuTuc).EqualTo(TT_TT)
                            .Set(KcbThanhtoan.Columns.IdHdonLog).EqualTo(IdHdonLog)
                            .Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();
                    }
                    scope.Complete();
                    id_thanhtoan = Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1);
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                log.Error("Loi thuc hien thanh toan:" + ex);
                return ActionResult.Error;
            }
        }

        public void TinhlaitienBhyTtruocThanhtoan(DataTable mDtData, KcbThanhtoan objThanhtoan, KcbLuotkham objLuotkham,
            List<KcbThanhtoanChitiet> objArrPaymentDetail, ref decimal PtramBHYT)
        {
            if (THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb))
            {
                PtramBHYT = 0;
                ///tổng tiền hiện tại truyền vào của lần payment đang thực hiện
                decimal vDblTongtienDCT = 0;
                ///tổng tiền đã thanh toán
                decimal vTotalPaymentDetail = 0;
                vDblTongtienDCT = TongtienKhongTutuc(objArrPaymentDetail);
                KcbThanhtoanCollection paymentCollection =
                    new KcbThanhtoanController()
                        .FetchByQuery(
                            KcbThanhtoan.CreateQuery()
                                .AddWhere(KcbThanhtoan.Columns.MaLuotkham, Comparison.Equals, objLuotkham.MaLuotkham)
                                .AND(KcbThanhtoan.Columns.IdBenhnhan, Comparison.Equals, objLuotkham.IdBenhnhan)
                                .AND(KcbThanhtoan.Columns.KieuThanhtoan, Comparison.Equals, objThanhtoan.KieuThanhtoan)
                        //Chỉ lấy về các bản ghi thanh toán thường(0= thường;1= thanh toán hủy(trả lại tiền))
                        );
                //Lấy tổng tiền của các lần thanh toán trước
                var lstKcbThanhtoanChitiet = new List<KcbThanhtoanChitiet>();

                foreach (KcbThanhtoan Payment in paymentCollection)
                {
                    var paymentDetailCollection = new Select().From(KcbThanhtoanChitiet.Schema)
                        .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan).IsEqualTo(Payment.IdThanhtoan)
                        .And(KcbThanhtoanChitiet.Columns.NoiTru).IsEqualTo(objThanhtoan.NoiTru)
                        .And(KcbThanhtoanChitiet.Columns.TrangthaiHuy).IsEqualTo(0).ExecuteAsCollection
                        <KcbThanhtoanChitietCollection>();

                    foreach (KcbThanhtoanChitiet paymentDetail in paymentDetailCollection)
                    {
                        if (paymentDetail.TuTuc == 0)
                        {
                            lstKcbThanhtoanChitiet.Add(paymentDetail);
                            paymentDetail.IsNew = false;
                            paymentDetail.MarkOld();
                            vTotalPaymentDetail += Utility.Int32Dbnull(paymentDetail.SoLuong)*
                                                   Utility.DecimaltoDbnull(paymentDetail.DonGia);
                        }
                    }
                }
                List<long> lstIdThanhtoan = (from q in lstKcbThanhtoanChitiet
                    select q.IdThanhtoan).ToList<long>();
                //Tính toán lại phần trăm BHYT chủ yếu liên quan đến phần lương cơ bản. 
                //Phần trăm này có thể bị biến đổi và khác với % trong các bảng dịch vụ
                LayThongtinPtramBHYT(vDblTongtienDCT + vTotalPaymentDetail, objLuotkham, ref PtramBHYT);
                THU_VIEN_CHUNG.TinhPhamTramBHYT(objLuotkham, mDtData, PtramBHYT);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="objThanhtoan"></param>
        /// <param name="objLuotkham"></param>
        /// <param name="objArrPaymentDetail"></param>
        /// <param name="id_thanhtoan"></param>
        /// <param name="IdHdonLog"></param>
        /// <param name="Layhoadondo"></param>
        /// <param name="TongtienBNchitra"></param>
        /// <returns></returns>
        public ActionResult ThanhtoanChiphiDvuKcb(KcbThanhtoan objThanhtoan, KcbLuotkham objLuotkham,
            List<KcbThanhtoanChitiet> objArrPaymentDetail, ref int id_thanhtoan, long IdHdonLog, bool Layhoadondo,
            ref decimal TongtienBNchitra, ref string ErrMsg)
        {
            ErrMsg = "";
            decimal PtramBHYT = 0;
            //tổng tiền hiện tại truyền vào của lần payment đang thực hiện
            decimal v_dblTongtienDCT = 0;
            //tổng tiền đã thanh toán
            decimal v_TotalPaymentDetail = 0;
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        if (Utility.Byte2Bool(objThanhtoan.NoiTru))
                        {
                            if (
                                THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_TUDONGHOANUNG_KHITHANHTOANNOITRU", "0",
                                    false) == "1")
                            {
                                SPs.NoitruHoanung(objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan,
                                    objThanhtoan.NgayThanhtoan, globalVariables.gv_intIDNhanvien,
                                    globalVariables.UserName, Utility.Int16Dbnull(objLuotkham.IdKhoanoitru, 0), Utility.Int64Dbnull(objLuotkham.IdRavien, -1),
                                         Utility.Int16Dbnull(objLuotkham.IdBuong, 0), Utility.Int16Dbnull(objLuotkham.IdGiuong, 0), 1).Execute();
                            }
                        }
                        else
                        {
                            if (
                                THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_KICHHOAT_TAMUNG_NGOAITRU", "0",
                                    false) == "1")
                                if (
                                    THU_VIEN_CHUNG.Laygiatrithamsohethong(
                                        "NGOAITRU_TUDONGHOANUNG_KHITHANHTOANNGOAITRU", "0", false) == "1")
                                {
                                    SPs.NoitruHoanung(objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan,
                                        objThanhtoan.NgayThanhtoan, globalVariables.gv_intIDNhanvien,
                                        globalVariables.UserName, Utility.Int16Dbnull(objLuotkham.IdKhoanoitru,0), Utility.Int64Dbnull(objLuotkham.IdRavien,-1),
                                         Utility.Int16Dbnull(objLuotkham.IdBuong, 0), Utility.Int16Dbnull(objLuotkham.IdGiuong, 0), 0).Execute();
                                }
                        }
                        //Tính tổng tiền đồng chi trả
                        v_dblTongtienDCT = TongtienKhongTutuc(objArrPaymentDetail);


                        KcbThanhtoanCollection lstKcbThanhtoanCollection =
                            new KcbThanhtoanController()
                                .FetchByQuery(
                                    KcbThanhtoan.CreateQuery()
                                        .AddWhere(KcbThanhtoan.Columns.MaLuotkham, Comparison.Equals,
                                            objLuotkham.MaLuotkham)
                                        .AND(KcbThanhtoan.Columns.IdBenhnhan, Comparison.Equals, objLuotkham.IdBenhnhan)
                                        .AND(KcbThanhtoan.Columns.KieuThanhtoan, Comparison.Equals, 0)
                                );
                        List<long> lstIdThanhtoan = (from q in lstKcbThanhtoanCollection
                            select q.IdThanhtoan).Distinct().ToList();
                        //Biến chứa danh sách tất cả các chi tiết dùng để tính lại tổng tiền thanh toán cho thanh toán có bản ghi bị hủy
                        var lstKcbThanhtoanChitiet = new List<KcbThanhtoanChitiet>();
                        if (lstIdThanhtoan.Count > 0)
                            lstKcbThanhtoanChitiet =
                                new Select().From(KcbThanhtoanChitiet.Schema)
                                    .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan).In(lstIdThanhtoan)
                                    .ExecuteAsCollection<KcbThanhtoanChitietCollection>().ToList<KcbThanhtoanChitiet>();

                        v_TotalPaymentDetail = (from p in lstKcbThanhtoanChitiet
                            where p.TuTuc == 0
                            select p).Sum(c => Utility.DecimaltoDbnull(c.SoLuong)*c.DonGia);

                        //Tính toán lại phần trăm BHYT chủ yếu liên quan đến phần lương cơ bản. 
                        //Phần trăm này có thể bị biến đổi và khác với % trong các bảng dịch vụ
                        LayThongtinPtramBHYT(v_dblTongtienDCT + v_TotalPaymentDetail, objLuotkham, ref PtramBHYT);
                        objThanhtoan.MaThanhtoan =
                            THU_VIEN_CHUNG.TaoMathanhtoan(Convert.ToDateTime(objThanhtoan.NgayThanhtoan));
                        objThanhtoan.PtramBhyt = Utility.Int16Dbnull(PtramBHYT, 0);
                        objThanhtoan.IdLoaidoituongKcb = objLuotkham.IdLoaidoituongKcb;
                      StoredProcedure sp =   SPs.SpKcbThanhtoanInsert(objThanhtoan.IdThanhtoan,objThanhtoan.MaLuotkham, objThanhtoan.IdBenhnhan,
                            objThanhtoan.MaDoituongKcb, objThanhtoan.IdDoituongKcb, objThanhtoan.IdLoaidoituongKcb,
                            objThanhtoan.NgayThanhtoan, objThanhtoan.IdNhanvienThanhtoan, objThanhtoan.MaThanhtoan
                            , objThanhtoan.KieuThanhtoan, objThanhtoan.TrangthaiIn, objThanhtoan.NgayIn,
                            objThanhtoan.NguoiIn, objThanhtoan.NgayTonghop, objThanhtoan.NguoiTonghop,
                            objThanhtoan.MaKhoaThuchien, objThanhtoan.NgayChot, objThanhtoan.TrangthaiChot
                            , objThanhtoan.TongTien, objThanhtoan.BhytChitra, objThanhtoan.BnhanChitra,
                            objThanhtoan.PtramBhyt
                            , objThanhtoan.TileChietkhau, objThanhtoan.KieuChietkhau, objThanhtoan.TongtienChietkhau,
                            objThanhtoan.TongtienChietkhauHoadon, objThanhtoan.TongtienChietkhauChitiet,
                            objThanhtoan.MaLydoChietkhau, objThanhtoan.NguoiTao, objThanhtoan.NgayTao
                            , objThanhtoan.NguoiSua, objThanhtoan.NgaySua, objThanhtoan.IdCapphat,
                            objThanhtoan.MauHoadon, objThanhtoan.KiHieu, objThanhtoan.MaQuyen, objThanhtoan.Serie,
                            objThanhtoan.TrangthaiSeri, objThanhtoan.IdHdonLog, objThanhtoan.NoiTru
                            , objThanhtoan.MaPttt, objThanhtoan.IpMaytao, objThanhtoan.IpMaysua, objThanhtoan.TenMaytao,
                            objThanhtoan.TenMaysua, objThanhtoan.NgayRavien, objThanhtoan.PhuThu, objThanhtoan.TuTuc,
                            objThanhtoan.MaLydoHuy);
                        sp.Execute();
                        objThanhtoan.IdThanhtoan = Utility.Int64Dbnull(sp.OutputValues[0], -1);
                        //objThanhtoan.IsNew = true;
                        //objThanhtoan.Save();
                        //Tính lại Bnhan chi trả và BHYT chi trả theo % BHYT mới
                        THU_VIEN_CHUNG.TinhPhamTramBHYT(objLuotkham, ref objArrPaymentDetail, ref lstKcbThanhtoanChitiet,
                            PtramBHYT);
                        decimal TT_BN = 0m;
                        decimal TT_BNCT = 0m;
                        decimal TT_PT = 0m;
                        decimal TT_TT = 0m;
                        decimal TT_BHYT = 0m;
                        decimal TT_Chietkhau_Chitiet = 0m;

                        if (THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_TINHLAI_TOANBODICHVU", "1", false) == "1")
                        {
                            foreach (int IdThanhtoan in lstIdThanhtoan)
                            {
                                TT_BN = 0m;
                                TT_BHYT = 0m;
                                TT_PT = 0m;
                                TT_TT = 0m;
                                TT_BNCT = 0m;
                                TT_Chietkhau_Chitiet = 0m;
                                List<KcbThanhtoanChitiet> _LstChitiet = (from q in lstKcbThanhtoanChitiet
                                    where q.IdThanhtoan == IdThanhtoan
                                    select q).ToList<KcbThanhtoanChitiet>();

                                if (_LstChitiet.Count > 0)
                                {
                                    foreach (KcbThanhtoanChitiet objChitietThanhtoan in _LstChitiet)
                                    {
                                        if (!Utility.Byte2Bool(objChitietThanhtoan.TuTuc))
                                            TT_BHYT += objChitietThanhtoan.BhytChitra*Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                        TT_Chietkhau_Chitiet +=
                                            Utility.DecimaltoDbnull(objChitietThanhtoan.TienChietkhau, 0);
                                        TT_PT += objChitietThanhtoan.PhuThu*Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                        if (Utility.Byte2Bool(objChitietThanhtoan.TuTuc))
                                            TT_TT += objChitietThanhtoan.BnhanChitra*Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                        else
                                            TT_BNCT += objChitietThanhtoan.BnhanChitra*Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);

                                        objChitietThanhtoan.IsNew = false;
                                        objChitietThanhtoan.MarkOld();
                                        objChitietThanhtoan.Save();
                                    }
                                    TT_BN += TT_PT + TT_BNCT + TT_TT;
                                    //Update lại tiền thanh toán
                                    new Update(KcbThanhtoan.Schema)
                                        .Set(KcbThanhtoan.Columns.TongTien).EqualTo(TT_BHYT + TT_BN)
                                        .Set(KcbThanhtoan.Columns.BnhanChitra).EqualTo(TT_BNCT)
                                        .Set(KcbThanhtoan.Columns.BhytChitra).EqualTo(TT_BHYT)
                                        .Set(KcbThanhtoan.Columns.PhuThu).EqualTo(TT_PT)
                                        .Set(KcbThanhtoan.Columns.TuTuc).EqualTo(TT_TT)
                                        .Set(KcbThanhtoan.Columns.MaDoituongKcb).EqualTo(objLuotkham.MaDoituongKcb)
                                        .Set(KcbThanhtoan.Columns.IdDoituongKcb).EqualTo(objLuotkham.IdDoituongKcb)
                                        .Set(KcbThanhtoan.Columns.PtramBhyt).EqualTo(objLuotkham.PtramBhyt)
                                        .Set(KcbThanhtoan.Columns.IdHdonLog).EqualTo(IdHdonLog)
                                        .Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(IdThanhtoan).Execute();
                                    //Update phiếu thu
                                    new Update(KcbPhieuthu.Schema)
                                        .Set(KcbPhieuthu.Columns.SoTien).EqualTo(TT_BN - TT_Chietkhau_Chitiet)
                                        .Set(KcbPhieuthu.Columns.SotienGoc).EqualTo(TT_BN)
                                        .Where(KcbPhieuthu.Columns.IdThanhtoan).IsEqualTo(IdThanhtoan).Execute();
                                }
                            }
                        }
                        //Reset để không bị cộng dồn với các thanh toán cũ
                        TT_BN = 0m;
                        TT_BHYT = 0m;
                        TT_PT = 0m;
                        TT_TT = 0m;
                        TT_BNCT = 0m;
                        TT_Chietkhau_Chitiet = 0m;
                        int reval = -1;
                        foreach (KcbThanhtoanChitiet objChitietThanhtoan in objArrPaymentDetail)
                        {
                            if (!Utility.Byte2Bool(objChitietThanhtoan.TuTuc))
                                TT_BHYT += objChitietThanhtoan.BhytChitra*Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                            TT_Chietkhau_Chitiet += Utility.DecimaltoDbnull(objChitietThanhtoan.TienChietkhau, 0);
                            TT_PT += objChitietThanhtoan.PhuThu*Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                            if (Utility.Byte2Bool(objChitietThanhtoan.TuTuc))
                                TT_TT += objChitietThanhtoan.BnhanChitra*Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                            else
                                TT_BNCT += objChitietThanhtoan.BnhanChitra*Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);


                            objChitietThanhtoan.IdThanhtoan = Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1);
                            StoredProcedure spchitiet = SPs.SpKcbThanhtoanChitietInsert(objChitietThanhtoan.IdChitiet,
                                objChitietThanhtoan.IdThanhtoan, objChitietThanhtoan.MadoituongGia,
                                objChitietThanhtoan.PtramBhytGoc, objChitietThanhtoan.PtramBhyt,
                                objChitietThanhtoan.SoLuong, objChitietThanhtoan.DonGia, objChitietThanhtoan.BnhanChitra,
                                objChitietThanhtoan.BhytChitra, objChitietThanhtoan.PhuThu
                                , objChitietThanhtoan.TuTuc, objChitietThanhtoan.IdPhieu,
                                objChitietThanhtoan.IdPhieuChitiet, objChitietThanhtoan.IdDichvu,
                                objChitietThanhtoan.IdChitietdichvu, objChitietThanhtoan.TenChitietdichvu,
                                objChitietThanhtoan.TenBhyt, objChitietThanhtoan.DonviTinh, objChitietThanhtoan.SttIn,
                                objChitietThanhtoan.IdKhoakcb, objChitietThanhtoan.IdPhongkham,
                                objChitietThanhtoan.IdBacsiChidinh
                                , objChitietThanhtoan.IdLoaithanhtoan, objChitietThanhtoan.TenLoaithanhtoan,
                                objChitietThanhtoan.MaDoituongKcb, objChitietThanhtoan.KieuChietkhau,
                                objChitietThanhtoan.TileChietkhau, objChitietThanhtoan.TienChietkhau,
                                objChitietThanhtoan.TrangthaiHuy, objChitietThanhtoan.TrangthaiBhyt,
                                objChitietThanhtoan.TrangthaiChuyen, objChitietThanhtoan.TinhChiphi,
                                objChitietThanhtoan.NoiTru, objChitietThanhtoan.IdGoi
                                , objChitietThanhtoan.TrongGoi, objChitietThanhtoan.IdKham, objChitietThanhtoan.NguonGoc,
                                objChitietThanhtoan.IdThanhtoanhuy, objChitietThanhtoan.IdLichsuDoituongKcb,
                                objChitietThanhtoan.MatheBhyt, objChitietThanhtoan.NgayTao, objChitietThanhtoan.NguoiTao
                                );
                            spchitiet.Execute();
                            objChitietThanhtoan.IdChitiet = Utility.Int64Dbnull(sp.OutputValues[0], -1);
                            //objChitietThanhtoan.IsNew = true;
                            //objChitietThanhtoan.Save();
                            reval = UpdatePaymentStatus(objThanhtoan, objChitietThanhtoan);
                            if (reval <= 0)
                            {
                                ErrMsg =
                                    string.Format(
                                        "Dịch vụ {0} đã bị người dùng khác hủy bỏ nên bạn không thể thanh toán. Hãy nhấn nút OK và chọn lại Bệnh nhân để lấy lại các chi phí thanh toán mới nhất",
                                        objChitietThanhtoan.TenChitietdichvu);
                                return ActionResult.Cancel;
                            }
                        }
                        TT_BN += TT_PT + TT_BNCT + TT_TT;
                        TongtienBNchitra = TT_BN;
                        objThanhtoan.TongTien = TT_BN + TT_BHYT;

                        #region Hoadondo

                        if (Layhoadondo)
                        {
                            int record = -1;
                            if (IdHdonLog > 0)
                            {
                                record =
                                    new Delete().From(HoadonLog.Schema)
                                        .Where(HoadonLog.Columns.IdHdonLog)
                                        .IsEqualTo(IdHdonLog)
                                        .Execute();
                                if (record <= 0)
                                {
                                    Utility.ShowMsg(
                                        "Có lỗi trong quá trình xóa thông tin serie hóa đơn đã hủy để cấp lại cho lần thanh toán này.");
                                    return ActionResult.Error;
                                }
                            }
                            var obj = new HoadonLog();
                            obj.IdThanhtoan = objThanhtoan.IdThanhtoan;
                            obj.TongTien = objThanhtoan.TongTien -
                                           Utility.DecimaltoDbnull(objThanhtoan.TongtienChietkhau, 0);
                            obj.IdBenhnhan = objThanhtoan.IdBenhnhan;
                            obj.MaLuotkham = objThanhtoan.MaLuotkham;
                            obj.MauHoadon = objThanhtoan.MauHoadon;
                            obj.KiHieu = objThanhtoan.KiHieu;
                            obj.IdCapphat = objThanhtoan.IdCapphat.Value;
                            obj.MaQuyen = objThanhtoan.MaQuyen;
                            obj.Serie = objThanhtoan.Serie;
                            obj.MaNhanvien = globalVariables.UserName;
                            obj.MaLydo = "0";
                            obj.NgayIn = globalVariables.SysDate;
                            obj.TrangThai = 0;
                            obj.IsNew = true;
                            obj.Save();
                            IdHdonLog = obj.IdHdonLog; //Để update lại vào bảng thanh toán
                            new Update(HoadonCapphat.Schema).Set(HoadonCapphat.Columns.SerieHientai)
                                .EqualTo(objThanhtoan.Serie)
                                .Set(HoadonCapphat.Columns.TrangThai).EqualTo(1)
                                .Where(HoadonCapphat.Columns.IdCapphat).IsEqualTo(obj.IdCapphat)
                                .Execute();
                        }

                        #endregion

                        SPs.SpKcbPhieuThuInsert(THU_VIEN_CHUNG.GetMaPhieuThu(globalVariables.SysDate, 0),
                            objThanhtoan.IdThanhtoan, objThanhtoan.IdBenhnhan, objThanhtoan.MaLuotkham
                            , globalVariables.SysDate, globalVariables.UserName, "Thu tiền bệnh nhân",
                            TT_BN - TT_Chietkhau_Chitiet, TT_BN, objThanhtoan.MaLydoChietkhau, TT_Chietkhau_Chitiet
                            , objThanhtoan.TongtienChietkhau, objThanhtoan.TongtienChietkhau - TT_Chietkhau_Chitiet, 1,
                            "", "", Convert.ToByte(0), globalVariables.gv_intIDNhanvien, globalVariables.idKhoatheoMay
                            , objThanhtoan.NoiTru, "", globalVariables.UserName, globalVariables.SysDate, "",
                            globalVariables.SysDate).Execute();
                        #region PhieuThuOld
                        //KcbPhieuthu objPhieuthu = new KcbPhieuthu();
                        //objPhieuthu.IdBenhnhan = objThanhtoan.IdBenhnhan;
                        //objPhieuthu.MaLuotkham = objThanhtoan.MaLuotkham;
                        //objPhieuthu.IdThanhtoan = objThanhtoan.IdThanhtoan;
                        //objPhieuthu.MaPhieuthu = THU_VIEN_CHUNG.GetMaPhieuThu(globalVariables.SysDate, 0);
                        //objPhieuthu.SoluongChungtugoc = 1;
                        //objPhieuthu.LoaiPhieuthu = Convert.ToByte(0);
                        //objPhieuthu.NgayThuchien = globalVariables.SysDate;
                        //objPhieuthu.SoTien = TT_BN - TT_Chietkhau_Chitiet;
                        //objPhieuthu.SotienGoc = TT_BN;
                        //objPhieuthu.MaLydoChietkhau = objThanhtoan.MaLydoChietkhau;
                        //objPhieuthu.TienChietkhauchitiet = TT_Chietkhau_Chitiet;
                        //objPhieuthu.TienChietkhau = objThanhtoan.TongtienChietkhau;
                        //objPhieuthu.TienChietkhauhoadon = objPhieuthu.TienChietkhau - objPhieuthu.TienChietkhauchitiet;
                        //objPhieuthu.NguoiNop = globalVariables.UserName;
                        //objPhieuthu.TaikhoanCo = "";
                        //objPhieuthu.TaikhoanNo = "";
                        //objPhieuthu.NoiTru = (byte)objThanhtoan.NoiTru;
                        //objPhieuthu.LydoNop = "Thu tiền bệnh nhân";
                        //objPhieuthu.IdKhoaThuchien = globalVariables.idKhoatheoMay;
                        //objPhieuthu.IdNhanvien = globalVariables.gv_intIDNhanvien;
                        //objPhieuthu.IsNew = true;
                        //objPhieuthu.Save();
                        #endregion
                        new Update(KcbThanhtoan.Schema)
                            .Set(KcbThanhtoan.Columns.TongTien).EqualTo(TT_BHYT + TT_BN)
                            .Set(KcbThanhtoan.Columns.BnhanChitra).EqualTo(TT_BNCT)
                            .Set(KcbThanhtoan.Columns.BhytChitra).EqualTo(TT_BHYT)
                            .Set(KcbThanhtoan.Columns.PhuThu).EqualTo(TT_PT)
                            .Set(KcbThanhtoan.Columns.TuTuc).EqualTo(TT_TT)
                            .Set(KcbThanhtoan.Columns.MaDoituongKcb).EqualTo(objLuotkham.MaDoituongKcb)
                            .Set(KcbThanhtoan.Columns.IdDoituongKcb).EqualTo(objLuotkham.IdDoituongKcb)
                            .Set(KcbThanhtoan.Columns.PtramBhyt).EqualTo(objLuotkham.PtramBhyt)
                            .Set(KcbThanhtoan.Columns.IdHdonLog).EqualTo(IdHdonLog)
                            .Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();

                        //Tạo bản ghi trong bảng phân bổ tiền theo phương thức thanh toán
                        SPs.SpKcbThanhtoanPhanbotheoPTTTInsert(objThanhtoan.IdThanhtoan, objThanhtoan.MaPttt,
                            objThanhtoan.IdBenhnhan, objThanhtoan.MaLuotkham,
                            objThanhtoan.NoiTru, TT_BN - TT_Chietkhau_Chitiet, TT_BN - TT_Chietkhau_Chitiet,
                            objThanhtoan.NguoiTao, objThanhtoan.NgayTao, "", objThanhtoan.NgayTao).Execute();
                        //new Delete().From(KcbThanhtoanPhanbotheoPTTT.Schema)
                        //    .Where(KcbThanhtoanPhanbotheoPTTT.Columns.IdThanhtoan)
                        //    .IsEqualTo(objThanhtoan.IdThanhtoan).Execute();
                        //var objPhanbotienTT = new KcbThanhtoanPhanbotheoPTTT();
                        //objPhanbotienTT.IdThanhtoan = objThanhtoan.IdThanhtoan;
                        //objPhanbotienTT.IdBenhnhan = objThanhtoan.IdBenhnhan;
                        //objPhanbotienTT.MaLuotkham = objThanhtoan.MaLuotkham;
                        //objPhanbotienTT.MaPttt = objThanhtoan.MaPttt;
                        //objPhanbotienTT.SoTien = TT_BN - TT_Chietkhau_Chitiet;
                        //objPhanbotienTT.NoiTru = objThanhtoan.NoiTru;
                        //objPhanbotienTT.TongTien = objPhanbotienTT.SoTien;
                        //objPhanbotienTT.NguoiTao = objThanhtoan.NguoiTao;
                        //objPhanbotienTT.NgayTao = objThanhtoan.NgayTao;
                        //objPhanbotienTT.IsNew = true;
                        //objPhanbotienTT.Save();
                        int _reval =
                            SPs.SpKcbLuotkhamTrangthaithanhtoan(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham,
                                Utility.Int16Dbnull(objLuotkham.TrangthaiNoitru,0), Utility.Int16Dbnull(objThanhtoan.NoiTru,0), objThanhtoan.NgayThanhtoan,
                                objLuotkham.MaDoituongKcb).Execute();
                        if (_reval <= 0)
                        {
                            ErrMsg =
                                    string.Format(
                                        "Chưa update thành công trạng thái thanh toán của bệnh nhân {0}",
                                        objLuotkham.MaLuotkham);
                        }

                        //if (Utility.Byte2Bool(objThanhtoan.NoiTru) &&
                        //    Utility.ByteDbnull(objLuotkham.TrangthaiNoitru, 0) >= 2)
                        //    new Update(KcbLuotkham.Schema).Set(KcbLuotkham.Columns.TthaiThanhtoannoitru).EqualTo(1)
                        //        .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        //        .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        //        .Execute();

                        dbscope.Dispose();
                    }
                    scope.Complete();
                    id_thanhtoan = Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1);
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                log.Error("Loi thuc hien thanh toan:" + ex);
                return ActionResult.Error;
            }
        }

        public ActionResult ThanhtoanChiphiDVuKCB_Ao(KcbThanhtoan objThanhtoan, KcbLuotkham objLuotkham,
            List<KcbThanhtoanChitiet> objArrPaymentDetail, ref int id_thanhtoan, long IdHdonLog, bool Layhoadondo)
        {
            decimal PtramBHYT = 0;
            ///tổng tiền hiện tại truyền vào của lần payment đang thực hiện
            decimal v_dblTongtienDCT = 0;
            ///tổng tiền đã thanh toán
            decimal v_TotalPaymentDetail = 0;
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        ///lấy tổng số Payment của mang truyền vào của pay ment hiện tại
                        v_dblTongtienDCT = TongtienKhongTutuc(objArrPaymentDetail);
                        KcbThanhtoanCollection paymentCollection =
                            new KcbThanhtoanController()
                                .FetchByQuery(
                                    KcbThanhtoan.CreateQuery()
                                        .AddWhere(KcbThanhtoan.Columns.MaLuotkham, Comparison.Equals,
                                            objLuotkham.MaLuotkham)
                                        .AND(KcbThanhtoan.Columns.IdBenhnhan, Comparison.Equals, objLuotkham.IdBenhnhan)
                                        .AND(KcbThanhtoan.Columns.KieuThanhtoan, Comparison.Equals,
                                            objThanhtoan.KieuThanhtoan)
                                );
                        //Lấy tổng tiền của các lần thanh toán trước
                        var lstKcbThanhtoanChitiet = new List<KcbThanhtoanChitiet>();
                        foreach (KcbThanhtoan Payment in paymentCollection)
                        {
                            var paymentDetailCollection = new Select().From(KcbThanhtoanChitiet.Schema)
                                .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan).IsEqualTo(Payment.IdThanhtoan)
                                .And(KcbThanhtoanChitiet.Columns.TrangthaiHuy).IsEqualTo(0).ExecuteAsCollection
                                <KcbThanhtoanChitietCollection>();

                            foreach (KcbThanhtoanChitiet paymentDetail in paymentDetailCollection)
                            {
                                if (paymentDetail.TuTuc == 0)
                                {
                                    lstKcbThanhtoanChitiet.Add(paymentDetail);

                                    v_TotalPaymentDetail += Utility.Int32Dbnull(paymentDetail.SoLuong)*
                                                            Utility.DecimaltoDbnull(paymentDetail.DonGia);
                                }
                            }
                        }


                        //Tính toán lại phần trăm BHYT chủ yếu liên quan đến phần lương cơ bản. 
                        //Phần trăm này có thể bị biến đổi và khác với % trong bảng lượt khám
                        LayThongtinPtramBHYT(v_dblTongtienDCT + v_TotalPaymentDetail, objLuotkham, ref PtramBHYT);
                        objThanhtoan.MaThanhtoan =
                            THU_VIEN_CHUNG.TaoMathanhtoan(Convert.ToDateTime(objThanhtoan.NgayThanhtoan));
                        objThanhtoan.IsNew = true;
                        objThanhtoan.Save();
                        //Tính lại Bnhan chi trả và BHYT chi trả
                        THU_VIEN_CHUNG.TinhPhamTramBHYT(objLuotkham, ref objArrPaymentDetail, ref lstKcbThanhtoanChitiet,
                            PtramBHYT);
                        foreach (KcbThanhtoanChitiet objChitietThanhtoan in lstKcbThanhtoanChitiet)
                        {
                            objChitietThanhtoan.IsNew = false;
                            objChitietThanhtoan.MarkOld();
                            objChitietThanhtoan.Save();
                        }
                        decimal TT_BN = 0m;
                        decimal TT_BHYT = 0m;
                        decimal TT_Chietkhau_Chitiet = 0m;
                        foreach (KcbThanhtoanChitiet objChitietThanhtoan in objArrPaymentDetail)
                        {
                            TT_BN += (objChitietThanhtoan.BnhanChitra + objChitietThanhtoan.PhuThu)*
                                     Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                            TT_BHYT += objChitietThanhtoan.BhytChitra*Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                            TT_Chietkhau_Chitiet += Utility.DecimaltoDbnull(objChitietThanhtoan.TienChietkhau, 0);
                            objChitietThanhtoan.IdThanhtoan = Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1);
                            objChitietThanhtoan.IsNew = true;
                            objChitietThanhtoan.Save();
                            UpdatePaymentStatus(objThanhtoan, objChitietThanhtoan);
                        }

                        #region Hoadondo

                        if (Layhoadondo)
                        {
                            int record = -1;
                            if (IdHdonLog > 0)
                            {
                                record =
                                    new Delete().From(HoadonLog.Schema)
                                        .Where(HoadonLog.Columns.IdHdonLog)
                                        .IsEqualTo(IdHdonLog)
                                        .Execute();
                                if (record <= 0)
                                {
                                    Utility.ShowMsg(
                                        "Có lỗi trong quá trình xóa thông tin serie hóa đơn đã hủy để cấp lại cho lần thanh toán này.");
                                    return ActionResult.Error;
                                }
                            }
                            var obj = new HoadonLog();
                            obj.IdThanhtoan = objThanhtoan.IdThanhtoan;
                            obj.TongTien = objThanhtoan.TongTien -
                                           Utility.DecimaltoDbnull(objThanhtoan.TongtienChietkhau, 0);
                            obj.IdBenhnhan = objThanhtoan.IdBenhnhan;
                            obj.MaLuotkham = objThanhtoan.MaLuotkham;
                            obj.MauHoadon = objThanhtoan.MauHoadon;
                            obj.KiHieu = objThanhtoan.KiHieu;
                            obj.IdCapphat = objThanhtoan.IdCapphat.Value;
                            obj.MaQuyen = objThanhtoan.MaQuyen;
                            obj.Serie = objThanhtoan.Serie;
                            obj.MaNhanvien = globalVariables.UserName;
                            obj.MaLydo = "0";
                            obj.NgayIn = globalVariables.SysDate;
                            obj.TrangThai = 0;
                            obj.IsNew = true;
                            obj.Save();
                            IdHdonLog = obj.IdHdonLog; //Để update lại vào bảng thanh toán
                            new Update(HoadonCapphat.Schema).Set(HoadonCapphat.Columns.SerieHientai)
                                .EqualTo(objThanhtoan.Serie)
                                .Set(HoadonCapphat.Columns.TrangThai).EqualTo(1)
                                .Where(HoadonCapphat.Columns.IdCapphat).IsEqualTo(obj.IdCapphat)
                                .Execute();
                        }

                        #endregion

                        var objPhieuthu = new KcbPhieuthu();
                        objPhieuthu.IdBenhnhan = objThanhtoan.IdBenhnhan;
                        objPhieuthu.MaLuotkham = objThanhtoan.MaLuotkham;
                        objPhieuthu.IdThanhtoan = objThanhtoan.IdThanhtoan;
                        objPhieuthu.MaPhieuthu = THU_VIEN_CHUNG.GetMaPhieuThu(globalVariables.SysDate, 0);
                        objPhieuthu.SoluongChungtugoc = 1;
                        objPhieuthu.LoaiPhieuthu = Convert.ToByte(0);
                        objPhieuthu.NgayThuchien = globalVariables.SysDate;
                        objPhieuthu.SoTien = TT_BN - TT_Chietkhau_Chitiet;
                        objPhieuthu.SotienGoc = TT_BN;
                        objPhieuthu.MaLydoChietkhau = objThanhtoan.MaLydoChietkhau;
                        objPhieuthu.TienChietkhauchitiet = TT_Chietkhau_Chitiet;
                        objPhieuthu.TienChietkhau = objThanhtoan.TongtienChietkhau;
                        objPhieuthu.TienChietkhauhoadon = objPhieuthu.TienChietkhau - objPhieuthu.TienChietkhauchitiet;
                        objPhieuthu.NguoiNop = globalVariables.UserName;
                        objPhieuthu.TaikhoanCo = "";
                        objPhieuthu.TaikhoanNo = "";
                        objPhieuthu.NoiTru = objThanhtoan.KieuThanhtoan;

                        objPhieuthu.LydoNop = "Thu tiền bệnh nhân";
                        objPhieuthu.IdKhoaThuchien = globalVariables.idKhoatheoMay;
                        objPhieuthu.IdNhanvien = globalVariables.gv_intIDNhanvien;
                        objPhieuthu.IsNew = true;
                        objPhieuthu.Save();

                        objLuotkham.IsNew = false;
                        objLuotkham.MarkOld();
                        objLuotkham.Save();

                        new Update(KcbThanhtoan.Schema)
                            .Set(KcbThanhtoan.Columns.TongTien).EqualTo(TT_BHYT + TT_BN)
                            .Set(KcbThanhtoan.Columns.BnhanChitra).EqualTo(TT_BN)
                            .Set(KcbThanhtoan.Columns.BhytChitra).EqualTo(TT_BHYT)
                            .Set(KcbThanhtoan.Columns.MaDoituongKcb).EqualTo(objLuotkham.MaDoituongKcb)
                            .Set(KcbThanhtoan.Columns.IdDoituongKcb).EqualTo(objLuotkham.IdDoituongKcb)
                            .Set(KcbThanhtoan.Columns.PtramBhyt).EqualTo(objLuotkham.PtramBhyt)
                            .Set(KcbThanhtoan.Columns.IdHdonLog).EqualTo(IdHdonLog)
                            .Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();
                    }
                    scope.Complete();
                    id_thanhtoan = Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1);
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                log.Error("Loi thuc hien thanh toan:" + ex);
                return ActionResult.Error;
            }
        }

        public ActionResult LayHoadondo(long id_thanhtoan, string MauHoadon, string KiHieu, string MaQuyen, string Serie,
            int IdCapphat, long IdHdonLog_huy, ref long IdHdonLog)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var db = new SharedDbConnectionScope())
                    {
                        KcbThanhtoan objThanhtoan = KcbThanhtoan.FetchByID(id_thanhtoan);
                        if (objThanhtoan == null) return ActionResult.Error;

                        if (IdHdonLog_huy > 0)
                        {
                            int record =
                                new Delete().From(HoadonLog.Schema)
                                    .Where(HoadonLog.Columns.IdHdonLog)
                                    .IsEqualTo(IdHdonLog_huy)
                                    .Execute();
                        }

                        var obj = new HoadonLog();
                        obj.IdThanhtoan = objThanhtoan.IdThanhtoan;
                        obj.TongTien = objThanhtoan.TongTien -
                                       Utility.DecimaltoDbnull(objThanhtoan.TongtienChietkhau, 0);
                        obj.IdBenhnhan = objThanhtoan.IdBenhnhan;
                        obj.MaLuotkham = objThanhtoan.MaLuotkham;
                        obj.MauHoadon = MauHoadon;
                        obj.KiHieu = KiHieu;
                        obj.IdCapphat = IdCapphat;
                        obj.MaQuyen = MaQuyen;
                        obj.Serie = Serie;
                        obj.MaNhanvien = globalVariables.UserName;
                        obj.MaLydo = "0";
                        obj.NgayIn = globalVariables.SysDate;
                        obj.TrangThai = 0;
                        obj.IsNew = true;
                        obj.Save();
                        IdHdonLog = obj.IdHdonLog;
                        new Update(KcbThanhtoan.Schema)
                            .Set(KcbThanhtoan.Columns.Serie).EqualTo(Serie)
                            .Set(KcbThanhtoan.Columns.MauHoadon).EqualTo(MauHoadon)
                            .Set(KcbThanhtoan.Columns.MaQuyen).EqualTo(MaQuyen)
                            .Set(KcbThanhtoan.Columns.KiHieu).EqualTo(KiHieu)
                            .Set(KcbThanhtoan.Columns.IdHdonLog).EqualTo(obj.IdHdonLog)
                            .Set(KcbThanhtoan.Columns.IdCapphat).EqualTo(obj.IdCapphat)
                            .Set(KcbThanhtoan.Columns.TrangthaiSeri).EqualTo(0)
                            .Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(id_thanhtoan).Execute();
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch
            {
                return ActionResult.Exception;
            }
        }

        public ActionResult BoHoadondo(long IdHdonLog)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var db = new SharedDbConnectionScope())
                    {
                        new Delete().From(HoadonLog.Schema)
                            .Where(HoadonLog.Columns.IdHdonLog).IsEqualTo(IdHdonLog).Execute();
                        new Update(KcbThanhtoan.Schema)
                            .Set(KcbThanhtoan.Columns.Serie).EqualTo("")
                            .Set(KcbThanhtoan.Columns.MauHoadon).EqualTo("")
                            .Set(KcbThanhtoan.Columns.MaQuyen).EqualTo("")
                            .Set(KcbThanhtoan.Columns.KiHieu).EqualTo("")
                            .Set(KcbThanhtoan.Columns.IdHdonLog).EqualTo(-1)
                            .Set(KcbThanhtoan.Columns.IdCapphat).EqualTo(-1)
                            .Set(KcbThanhtoan.Columns.TrangthaiSeri).EqualTo(0)
                            .Where(KcbThanhtoan.Columns.IdHdonLog).IsEqualTo(IdHdonLog).Execute();
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch
            {
                return ActionResult.Exception;
            }
        }

        public ActionResult UpdatePtramBHYT(KcbLuotkham objLuotKham, int option)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var db = new SharedDbConnectionScope())
                    {
                        decimal ptramBhyt = Utility.DecimaltoDbnull(objLuotKham.PtramBhyt, 0m);
                        decimal bnhanchitra = 0m;
                        decimal bhytchitra = 0m;
                        decimal dongia = 0m;
                        if (option == 1 || option == -1)
                        {
                            var lstKcbDangkyKcb = new Select().From(KcbDangkyKcb.Schema)
                                .Where(KcbDangkyKcb.Columns.MaLuotkham).IsEqualTo(objLuotKham.MaLuotkham)
                                .And(KcbDangkyKcb.Columns.LaPhidichvukemtheo)
                                .IsNotEqualTo(1)
                                .ExecuteAsCollection<KcbDangkyKcbCollection>();
                            foreach (KcbDangkyKcb _item in lstKcbDangkyKcb)
                            {
                                dongia = _item.DonGia;
                                if (_item.TuTuc == 0)
                                {
                                    bhytchitra = THU_VIEN_CHUNG.TinhBhytChitra(ptramBhyt, dongia, 0);
                                    bnhanchitra = dongia - bhytchitra;
                                }
                                else
                                {
                                    bhytchitra = 0;
                                    bnhanchitra = dongia;
                                }
                            }
                        }
                        else if (option == 2 || option == -1)
                        {
                        }
                        else if (option == 3 || option == -1)
                        {
                        }
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch
            {
                return ActionResult.Exception;
            }
        }

        public void HUYTHONGTIN_THANHTOAN(KcbThanhtoanChitietCollection objArrPaymentDetail, KcbThanhtoan objThanhtoan)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                     var locked = (byte) (objThanhtoan.MaDoituongKcb == "DV" ? 1: 0);
                    StoredProcedure sp = SPs.SpKcbHuythanhtoan(objThanhtoan.IdThanhtoan, locked, globalVariables.SysDate,
                        globalVariables.UserName, objThanhtoan.IdBenhnhan, objThanhtoan.MaLuotkham);
                    sp.Execute();
                    #region Hủy thanh toán cũ
                    //new Update(KcbDangkySokham.Schema)
                    //    .Set(KcbDangkySokham.Columns.IdThanhtoan).EqualTo(-1)
                    //    .Set(KcbDangkySokham.Columns.NgayThanhtoan).EqualTo(null)
                    //    .Set(KcbDangkySokham.Columns.TrangthaiThanhtoan).EqualTo(0)
                    //    .Set(KcbDangkySokham.Columns.NguonThanhtoan).EqualTo(null)
                    //    .Set(KcbDangkySokham.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                    //    .Set(KcbDangkySokham.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                    //    .Where(KcbDangkySokham.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();

                    //new Update(KcbDangkyKcb.Schema)
                    //    .Set(KcbDangkyKcb.Columns.IdThanhtoan).EqualTo(-1)
                    //    .Set(KcbDangkyKcb.Columns.NgayThanhtoan).EqualTo(null)
                    //    .Set(KcbDangkyKcb.Columns.TrangthaiThanhtoan).EqualTo(0)
                    //    .Set(KcbDangkyKcb.Columns.TileChietkhau).EqualTo(0)
                    //    .Set(KcbDangkyKcb.Columns.TienChietkhau).EqualTo(0)
                    //    .Set(KcbDangkyKcb.Columns.NguonThanhtoan).EqualTo(null)
                    //    .Set(KcbDangkyKcb.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                    //    .Set(KcbDangkyKcb.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                    //    .Where(KcbDangkyKcb.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();

                    //new Update(NoitruPhanbuonggiuong.Schema)
                    //    .Set(NoitruPhanbuonggiuong.Columns.IdThanhtoan).EqualTo(-1)
                    //    .Set(NoitruPhanbuonggiuong.Columns.NgayThanhtoan).EqualTo(null)
                    //    .Set(NoitruPhanbuonggiuong.Columns.TrangthaiThanhtoan).EqualTo(0)
                    //    .Set(NoitruPhanbuonggiuong.Columns.NguonThanhtoan).EqualTo(null)
                    //    .Set(NoitruPhanbuonggiuong.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                    //    .Set(NoitruPhanbuonggiuong.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                    //    .Where(NoitruPhanbuonggiuong.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();


                    //new Update(KcbChidinhclsChitiet.Schema)
                    //    .Set(KcbChidinhclsChitiet.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                    //    .Set(KcbChidinhclsChitiet.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                    //    .Set(KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).EqualTo(0)
                    //    .Set(KcbChidinhclsChitiet.Columns.TileChietkhau).EqualTo(0)
                    //    .Set(KcbChidinhclsChitiet.Columns.TienChietkhau).EqualTo(0)
                    //    .Set(KcbChidinhclsChitiet.Columns.NguonThanhtoan).EqualTo(null)
                    //    .Set(KcbChidinhclsChitiet.Columns.NgayThanhtoan).EqualTo(null)
                    //    .Set(KcbChidinhclsChitiet.Columns.IdThanhtoan).EqualTo(-1)
                    //    .Where(KcbChidinhclsChitiet.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();


                    //new Update(KcbDonthuocChitiet.Schema)
                    //    .Set(KcbDonthuocChitiet.Columns.TrangthaiThanhtoan).EqualTo(0)
                    //    .Set(KcbDonthuocChitiet.Columns.NgayThanhtoan).EqualTo(null)
                    //    .Set(KcbDonthuocChitiet.Columns.IdThanhtoan).EqualTo(-1)
                    //    .Set(KcbDonthuocChitiet.Columns.TileChietkhau).EqualTo(0)
                    //    .Set(KcbDonthuocChitiet.Columns.NguonThanhtoan).EqualTo(null)
                    //    .Set(KcbDonthuocChitiet.Columns.TienChietkhau).EqualTo(0)
                    //    .Set(KcbDonthuocChitiet.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                    //    .Set(KcbDonthuocChitiet.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                    //    .Where(KcbDonthuocChitiet.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();


                    //new Update(TTongChiphi.Schema)
                    //    .Set(TTongChiphi.Columns.PaymentId).EqualTo(null)
                    //    .Set(TTongChiphi.Columns.PaymentStatus).EqualTo(0)
                    //    .Set(TTongChiphi.Columns.PaymentDate).EqualTo(null)
                    //    .Where(TTongChiphi.Columns.PaymentId).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();


                    //new Delete().From(KcbPhieuthu.Schema)
                    //    .Where(KcbPhieuthu.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();
                    //new Delete().From(KcbThanhtoanChitiet.Schema)
                    //    .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();
                    #endregion 
                    scope.Complete();
                }
            }
            catch (Exception exception)
            {
                log.Error(exception.ToString);
                // return ActionResult.Error;
            }
        }

        public ActionResult HuyThongTinLanThanhToan_Donthuoctaiquay(int id_thanhtoan, KcbLuotkham objLuotkham,
            string lydohuy, int IdHdonLog, bool HuyBienlai)
        {
            try
            {
                decimal v_TotalPaymentDetail = 0;
                decimal v_DiscountRate = 0;
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        if (IdHdonLog > 0)
                            if (HuyBienlai)
                                new Update(HoadonLog.Schema).Set(HoadonLog.Columns.TrangThai).EqualTo(1)
                                    .Where(HoadonLog.Columns.IdHdonLog).IsEqualTo(IdHdonLog).Execute();
                            else
                                new Delete().From(HoadonLog.Schema)
                                    .Where(HoadonLog.Columns.IdHdonLog).IsEqualTo(IdHdonLog).Execute();
                        SqlQuery sqlQuery =
                            new Select().From(KcbThanhtoanChitiet.Schema)
                                .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan)
                                .IsEqualTo(
                                    id_thanhtoan);
                        var arrPaymentDetails = sqlQuery.ExecuteAsCollection<KcbThanhtoanChitietCollection>();
                        KcbThanhtoan objThanhtoan = KcbThanhtoan.FetchByID(id_thanhtoan);
                        int id_donthuoc = -1;
                        if (arrPaymentDetails.Count > 0) id_donthuoc = arrPaymentDetails[0].IdPhieu;
                        if (objThanhtoan != null)
                            HUYTHONGTIN_THANHTOAN(arrPaymentDetails, objThanhtoan);
                        KcbDonthuoc objDonthuoc = KcbDonthuoc.FetchByID(id_donthuoc);
                        var lstChitiet =
                            new Select().From(KcbDonthuocChitiet.Schema)
                                .Where(KcbDonthuocChitiet.Columns.IdDonthuoc)
                                .IsEqualTo(id_donthuoc)
                                .ExecuteAsCollection<KcbDonthuocChitietCollection>();
                        var actionResult = ActionResult.Success;
                        if (objDonthuoc != null && lstChitiet.Count > 0)
                        {
                            actionResult = new XuatThuoc().HuyXacNhanDonThuocBNTaiQuay(id_donthuoc,
                                Utility.Int16Dbnull(lstChitiet[0].IdKho, 0), DateTime.Now, lydohuy);
                            switch (actionResult)
                            {
                                case ActionResult.Success:
                                    break;
                                case ActionResult.Error:
                                    return actionResult;
                            }
                        }
                        KcbThanhtoan.Delete(id_thanhtoan);
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi trong qua trinh huy thong tin {0}", exception.ToString());
                return ActionResult.Error;
            }
        }

        public ActionResult HuyThongTinLanThanhToan_Ao(int id_thanhtoan, KcbLuotkham objLuotkham, string lydohuy,
            int IdHdonLog, bool HuyBienlai)
        {
            try
            {
                decimal v_TotalPaymentDetail = 0;
                decimal v_DiscountRate = 0;
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        if (IdHdonLog > 0)
                            if (HuyBienlai)
                                new Update(HoadonLog.Schema).Set(HoadonLog.Columns.TrangThai).EqualTo(1)
                                    .Where(HoadonLog.Columns.IdHdonLog).IsEqualTo(IdHdonLog).Execute();
                            else
                                new Delete().From(HoadonLog.Schema)
                                    .Where(HoadonLog.Columns.IdHdonLog).IsEqualTo(IdHdonLog).Execute();
                        SqlQuery sqlQuery =
                            new Select().From(KcbThanhtoanChitiet.Schema)
                                .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan)
                                .IsEqualTo(
                                    id_thanhtoan);
                        var arrPaymentDetails = sqlQuery.ExecuteAsCollection<KcbThanhtoanChitietCollection>();
                        KcbThanhtoan objThanhtoan = KcbThanhtoan.FetchByID(id_thanhtoan);
                        if (objThanhtoan != null)
                            HUYTHONGTIN_THANHTOAN(arrPaymentDetails, objThanhtoan);
                        new Delete().From(KcbPhieuDct.Schema)
                            .Where(KcbPhieuDct.Columns.MaLuotkham).IsEqualTo(objThanhtoan.MaLuotkham)
                            .And(KcbPhieuDct.Columns.IdBenhnhan).IsEqualTo(objThanhtoan.IdBenhnhan)
                            .And(KcbPhieuDct.Columns.LoaiThanhtoan).IsEqualTo(objThanhtoan.KieuThanhtoan).Execute();
                        if (objLuotkham != null)
                        {
                            var locked = (byte) (objLuotkham.MaDoituongKcb == "DV" ? objLuotkham.Locked : 0);
                            new Update(KcbLuotkham.Schema)
                                .Set(KcbLuotkham.Columns.NgayKetthuc).EqualTo(null)
                                .Set(KcbLuotkham.Columns.NguoiKetthuc).EqualTo(string.Empty)
                                .Set(KcbLuotkham.Columns.Locked).EqualTo(locked)
                                .Set(KcbLuotkham.Columns.TrangthaiNgoaitru).EqualTo(locked)
                                .Set(KcbLuotkham.Columns.BoVien).EqualTo(0)
                                .Set(KcbLuotkham.Columns.LydoKetthuc).EqualTo("")
                                .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                                .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).Execute();
                        }
                        KcbThanhtoan.Delete(id_thanhtoan);
                        if (objLuotkham != null)
                            log.Info(
                                string.Format(
                                    "Phiếu thanh toán ID: {0} của bệnh nhân: {1} - ID Bệnh nhân: {2} đã được hủy bởi :{3} với lý do hủy :{4}",
                                    id_thanhtoan, objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan,
                                    globalVariables.UserName, lydohuy));
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi trong qua trinh huy thong tin {0}", exception.ToString());
                return ActionResult.Error;
            }
        }

        public ActionResult HuyThongTinLanThanhToan(KcbThanhtoan objThanhtoan, KcbLuotkham objLuotkham, string lydohuy,
            int idHdonLog, bool huyBienlai)
        {
            try
            {
                decimal vTotalPaymentDetail = 0;
                decimal vDiscountRate = 0;
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        if (Utility.Byte2Bool(objLuotkham.TrangthaiNoitru) && Utility.Byte2Bool(objThanhtoan.NoiTru))
                        {
                            if (
                                THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_TUDONGHOANUNG_KHITHANHTOANNOITRU", "0",
                                    false) == "1")
                                SPs.NoitruHuyhoanung(objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, 1).Execute();
                        }
                        else
                        {
                            if (
                                THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_KICHHOAT_TAMUNG_NGOAITRU", "0",
                                    false) == "1")
                                if (
                                    THU_VIEN_CHUNG.Laygiatrithamsohethong(
                                        "NGOAITRU_TUDONGHOANUNG_KHITHANHTOANNGOAITRU", "0", false) == "1")
                                    SPs.NoitruHuyhoanung(objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, 0).Execute();
                        }
                        if (idHdonLog > 0)
                            if (huyBienlai)
                                new Update(HoadonLog.Schema).Set(HoadonLog.Columns.TrangThai).EqualTo(1)
                                    .Where(HoadonLog.Columns.IdHdonLog).IsEqualTo(idHdonLog).Execute();
                            else
                                new Delete().From(HoadonLog.Schema)
                                    .Where(HoadonLog.Columns.IdHdonLog).IsEqualTo(idHdonLog).Execute();
                        SqlQuery sqlQuery =
                            new Select().From(KcbThanhtoanChitiet.Schema)
                                .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan)
                                .IsEqualTo(
                                    objThanhtoan.IdThanhtoan);
                        var arrPaymentDetails = sqlQuery.ExecuteAsCollection<KcbThanhtoanChitietCollection>();
                        var objKcbLoghuy = new KcbLoghuy();
                        objKcbLoghuy.IdBenhnhan = objThanhtoan.IdBenhnhan;
                        objKcbLoghuy.MaLuotkham = objThanhtoan.MaLuotkham;
                        objKcbLoghuy.IdNhanvien = globalVariables.gv_intIDNhanvien;
                        objKcbLoghuy.SotienHuy = objThanhtoan.TongTien;
                        objKcbLoghuy.LydoHuy = lydohuy;
                        objKcbLoghuy.NgayHuy = DateTime.Now;
                        objKcbLoghuy.NgayTao = DateTime.Now;
                        objKcbLoghuy.NguoiTao = globalVariables.UserName;
                        objKcbLoghuy.IsNew = true;
                        objKcbLoghuy.LoaiphieuHuy = Utility.ByteDbnull(objThanhtoan.KieuThanhtoan, 0);
                        objKcbLoghuy.Save();
                        HUYTHONGTIN_THANHTOAN(arrPaymentDetails, objThanhtoan);
                        //new Delete().From(KcbPhieuDct.Schema)
                        //    .Where(KcbPhieuDct.Columns.MaLuotkham).IsEqualTo(objThanhtoan.MaLuotkham)
                        //    .And(KcbPhieuDct.Columns.IdBenhnhan).IsEqualTo(objThanhtoan.IdBenhnhan)
                        //    .And(KcbPhieuDct.Columns.LoaiThanhtoan).IsEqualTo(objThanhtoan.KieuThanhtoan).Execute();
                        //if (objLuotkham != null)
                        //{
                        //    var locked = (byte) (objLuotkham.MaDoituongKcb == "DV" ? objLuotkham.Locked : 0);
                        //    new Update(KcbLuotkham.Schema)
                        //        .Set(KcbLuotkham.Columns.NgayKetthuc).EqualTo(null)
                        //        .Set(KcbLuotkham.Columns.NguoiKetthuc).EqualTo(string.Empty)
                        //        .Set(KcbLuotkham.Columns.Locked).EqualTo(locked)
                        //        .Set(KcbLuotkham.Columns.TrangthaiNgoaitru).EqualTo(locked)
                        //        .Set(KcbLuotkham.Columns.TthaiThanhtoannoitru).EqualTo(0)
                        //        .Set(KcbLuotkham.Columns.LydoKetthuc).EqualTo("")
                        //        .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        //        .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).Execute();
                        //}
                        //KcbThanhtoan.Delete(objThanhtoan.IdThanhtoan);
                        //new Delete().From(KcbThanhtoanPhanbotheoPTTT.Schema)
                        //    .Where(KcbThanhtoanPhanbotheoPTTT.Columns.IdThanhtoan)
                        //    .IsEqualTo(objThanhtoan.IdThanhtoan)
                        //    .Execute();
                        log.Info(
                            string.Format(
                                "Phiếu thanh toán ID: {0} của bệnh nhân: {1} - ID Bệnh nhân: {2} đã được hủy bởi :{3} với lý do hủy :{4}",
                                objThanhtoan.IdThanhtoan, objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan,
                                globalVariables.UserName, lydohuy));
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi trong qua trinh huy thong tin {0}", exception.ToString());
                return ActionResult.Error;
            }
        }

        public DataTable Laychitietthanhtoan(int IdThanhtoan, byte kieuthanhtoan)
        {
            return SPs.KcbThanhtoanLaythongtinchitietTheoid(IdThanhtoan, kieuthanhtoan).GetDataSet().Tables[0];
        }

        public DataTable KcbThanhtoanLaydulieuphanbothanhtoanTheoPttt(int IdThanhtoan)
        {
            return SPs.KcbThanhtoanLaydulieuphanbothanhtoanTheoPTTT(IdThanhtoan).GetDataSet().Tables[0];
        }

        public DataTable KiemtraTrangthaidonthuocTruockhihuythanhtoan(long IdThanhtoan)
        {
            return SPs.DonthuocKiemtraxacnhanthuocTrongdon(IdThanhtoan).GetDataSet().Tables[0];
        }

        public ActionResult UpdateTienphanbotheoPttt(DataTable dtData, ref string msg)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        long idThanhtoan =
                            Utility.Int64Dbnull(dtData.Select("id_thanhtoan>0")[0][KcbThanhtoan.Columns.IdThanhtoan], 0);
                        decimal tongTien = Utility.Int64Dbnull(dtData.Select("tong_tien>0")[0]["tong_tien"], 0);
                        if (idThanhtoan > 0)
                        {
                            KcbThanhtoan objThanhtoan = KcbThanhtoan.FetchByID(idThanhtoan);
                            new Delete().From(KcbThanhtoanPhanbotheoPTTT.Schema)
                                .Where(KcbThanhtoanPhanbotheoPTTT.Columns.IdThanhtoan)
                                .IsEqualTo(idThanhtoan)
                                .Execute();
                            foreach (DataRow dr in dtData.Rows)
                            {
                                if (Utility.DecimaltoDbnull(dr["so_tien"], 0) > 0)
                                {
                                    var newItem = new KcbThanhtoanPhanbotheoPTTT();
                                    newItem.IdThanhtoan = idThanhtoan;
                                    newItem.MaPttt = Utility.sDbnull(dr["ma_pttt"], "");
                                    newItem.IdBenhnhan = objThanhtoan.IdBenhnhan;
                                    newItem.MaLuotkham = objThanhtoan.MaLuotkham;
                                    newItem.TongTien = tongTien;
                                    newItem.NoiTru = objThanhtoan.NoiTru;
                                    newItem.SoTien = Utility.DecimaltoDbnull(dr["so_tien"], 0);
                                    newItem.NguoiTao = globalVariables.UserName;
                                    newItem.NgayTao = globalVariables.SysDate;
                                    //StoredProcedure sp = SPs.SpKcbThanhtoanPhanbotheoPTTTInsert(newItem.IdThanhtoan, newItem.MaPttt,
                                    //    newItem.IdBenhnhan, newItem.MaLuotkham, newItem.NoiTru, newItem.TongTien,
                                    //    newItem.SoTien
                                    //    , newItem.NguoiTao, newItem.NgayTao, newItem.NguoiSua, newItem.NgaySua);
                                    //sp.Execute();
                                    newItem.IsNew = true;
                                    newItem.Save();
                                }
                            }
                        }
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                log.Error("Loi trong qua trinh tra tien lai:{0}", ex.ToString());
                return ActionResult.Error;
            }
        }

        public ActionResult UpdateHuyInPhoiBHYT(KcbLuotkham objLuotkham, KieuThanhToan kieuThanhToan)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        new Update(KcbLuotkham.Schema)
                            //.Set(KcbLuotkham.Columns.TinhTrangRaVienStatus).EqualTo(0)
                            // .Set(KcbLuotkham.Columns.NgayKetthuc).EqualTo(null)
                            .Set(KcbLuotkham.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                            .Set(KcbLuotkham.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                            //.Set(KcbLuotkham.Columns.NguoiKetthuc).EqualTo(null)
                            //.Set(KcbLuotkham.Columns.IpMacSua).EqualTo(globalVariables.IpMacAddress)
                            //.Set(KcbLuotkham.Columns.IpMaySua).EqualTo(globalVariables.IpAddress)
                            //.Set(KcbLuotkham.Columns.ReasonBy).EqualTo("Hủy phôi bảo hiểm")
                            .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                            .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                            .Execute();
                        new Delete().From(KcbPhieuDct.Schema)
                            .Where(KcbPhieuDct.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                            .And(KcbPhieuDct.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                            .And(KcbPhieuDct.Columns.LoaiThanhtoan).IsEqualTo(kieuThanhToan).Execute();
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi trong qua trinh tra tien lai:{0}", exception.ToString());
                return ActionResult.Error;
            }
        }

        private decimal SumOfPaymentDetail(KcbThanhtoanChitiet[] objArrPaymentDetail)
        {
            decimal SumOfPaymentDetail = 0;
            foreach (KcbThanhtoanChitiet paymentDetail in objArrPaymentDetail)
            {
                if (paymentDetail.TuTuc == 0)
                    SumOfPaymentDetail += (Utility.Int32Dbnull(paymentDetail.SoLuong)*
                                           Utility.DecimaltoDbnull(paymentDetail.DonGia))
                                          +
                                          (Utility.DecimaltoDbnull(paymentDetail.PhuThu, 0)*
                                           Utility.Int32Dbnull(paymentDetail.SoLuong, 0));
            }
            return SumOfPaymentDetail;
        }

        /// <summary>
        ///     Trả lại tiền
        /// </summary>
        /// <param name="objThanhtoan"></param>
        /// <param name="objLuotkham"></param>
        /// <param name="objArrPaymentDetail"></param>
        /// <returns></returns>
        public ActionResult Tratien(KcbThanhtoan objPhieuchi, KcbLuotkham objLuotkham, List<Int64> lstIdChitiet,
            string malydohuy, string noidunghuy, string lydotratien)
        {
            decimal ptramBhyt = 0;
            ///tổng tiền hiện tại truyền vào của lần payment đang thực hiện
            decimal vDblTongtienHuy = 0;
            ///tổng tiền đã thanh toán
            decimal v_TotalPaymentDetail = 0;
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        decimal TT_BN = 0m;
                        decimal TT_BNCT = 0m;
                        decimal TT_PT = 0m;
                        decimal TT_TT = 0m;
                        decimal TT_BHYT = 0m;
                        decimal TT_Chietkhau_Chitiet = 0m;
                        List<KcbThanhtoanChitiet> arrKcbThanhtoanChitietHuy =
                            new Select().From(KcbThanhtoanChitiet.Schema)
                                .Where(KcbThanhtoanChitiet.Columns.IdChitiet).In(lstIdChitiet)
                                .ExecuteAsCollection<KcbThanhtoanChitietCollection>().ToList<KcbThanhtoanChitiet>();
                        //Thêm mới dòng thanh toán hủy
                        objPhieuchi.KieuThanhtoan = 1;
                        objPhieuchi.IdNhanvienThanhtoan = globalVariables.gv_intIDNhanvien;
                        objPhieuchi.NgayThanhtoan = globalVariables.SysDate;
                        objPhieuchi.MaThanhtoan = THU_VIEN_CHUNG.TaoMathanhtoan(globalVariables.SysDate);
                        objPhieuchi.MaLydoHuy = malydohuy;
                        objPhieuchi.IsNew = true;
                        objPhieuchi.Save();

                        //Reset và tính toán các số tiền liên quan đến các bản ghi hủy
                        TT_BN = 0m;
                        TT_BNCT = 0m;
                        TT_PT = 0m;
                        TT_TT = 0m;
                        TT_BHYT = 0m;
                        TT_Chietkhau_Chitiet = 0;
                        //Cập nhật các dòng chi tiết được chọn hủy về trạng thái hủy và các dịch vụ trong các bảng tương ứng theo id_loaithanhtoan
                        foreach (KcbThanhtoanChitiet objKcbThanhtoanChitiet in arrKcbThanhtoanChitietHuy)
                        {
                            if (!Utility.Byte2Bool(objKcbThanhtoanChitiet.TuTuc))
                                TT_BHYT += objKcbThanhtoanChitiet.BhytChitra*Utility.DecimaltoDbnull(objKcbThanhtoanChitiet.SoLuong);
                            TT_Chietkhau_Chitiet += Utility.DecimaltoDbnull(objKcbThanhtoanChitiet.TienChietkhau, 0);
                            TT_PT += objKcbThanhtoanChitiet.PhuThu*Utility.DecimaltoDbnull(objKcbThanhtoanChitiet.SoLuong);
                            if (Utility.Byte2Bool(objKcbThanhtoanChitiet.TuTuc))
                                TT_TT += objKcbThanhtoanChitiet.BnhanChitra*Utility.DecimaltoDbnull(objKcbThanhtoanChitiet.SoLuong);
                            else
                                TT_BNCT += objKcbThanhtoanChitiet.BnhanChitra*Utility.DecimaltoDbnull(objKcbThanhtoanChitiet.SoLuong);
                            ///Phí Khám chữa bệnh))
                            if (objKcbThanhtoanChitiet.IdLoaithanhtoan == 1)
                            {
                                new Update(KcbDangkyKcb.Schema)
                                    .Set(KcbDangkyKcb.Columns.TrangthaiHuy).EqualTo(1)
                                    .Where(KcbDangkyKcb.Columns.IdKham)
                                    .IsEqualTo(objKcbThanhtoanChitiet.IdPhieu)
                                    .Execute();
                            }
                            ///Dịch vụ cận lâm sàng
                            if (objKcbThanhtoanChitiet.IdLoaithanhtoan == 2)
                            {
                                KcbChidinhclsChitiet objKcbChidinhclsChitiet =
                                    KcbChidinhclsChitiet.FetchByID(objKcbThanhtoanChitiet.IdPhieuChitiet);
                                if (globalVariables.UserName != "ADMIN")
                                {
                                    if (objKcbChidinhclsChitiet != null && objKcbChidinhclsChitiet.TrangThai >= 3)
                                        //Đã có kết quả
                                    {
                                        return ActionResult.AssignIsConfirmed;
                                    }
                                }
                                new Update(KcbChidinhclsChitiet.Schema)
                                    .Set(KcbChidinhclsChitiet.Columns.TrangthaiHuy).EqualTo(1)
                                    .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh)
                                    .IsEqualTo(objKcbThanhtoanChitiet.IdPhieuChitiet)
                                    .Execute();
                            }
                            ///Thuốc
                            if (objKcbThanhtoanChitiet.IdLoaithanhtoan == 3)
                            {
                                KcbDonthuocChitiet objKcbDonthuocChitiet =
                                    KcbDonthuocChitiet.FetchByID(objKcbThanhtoanChitiet.IdPhieuChitiet);

                                if (globalVariables.UserName != "ADMIN")
                                {
                                    if (objKcbDonthuocChitiet != null &&
                                        Utility.Byte2Bool(objKcbDonthuocChitiet.TrangThai))
                                    {
                                        return ActionResult.PresIsConfirmed;
                                    }
                                }
                                new Update(KcbDonthuoc.Schema)
                                    .Set(KcbDonthuoc.Columns.TrangThai).EqualTo(0)
                                    .Where(KcbDonthuoc.Columns.IdDonthuoc)
                                    .IsEqualTo(objKcbThanhtoanChitiet.IdPhieu)
                                    .Execute();
                                new Update(KcbDonthuocChitiet.Schema)
                                    .Set(KcbDonthuocChitiet.Columns.TrangthaiHuy).EqualTo(1)
                                    .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc)
                                    .IsEqualTo(objKcbThanhtoanChitiet.IdPhieuChitiet)
                                    .Execute();
                            }
                            //Tạo dữ liệu hủy tiền

                            objKcbThanhtoanChitiet.IdThanhtoanhuy = objPhieuchi.IdThanhtoan;
                                //Để biết dòng hủy này hủy cho chi tiết thanh toán nào
                            objKcbThanhtoanChitiet.TrangthaiHuy = 1;
                            objKcbThanhtoanChitiet.IsNew = false;
                            objKcbThanhtoanChitiet.MarkOld();
                            objKcbThanhtoanChitiet.Save();
                        }
                        TT_BN += TT_PT + TT_BNCT + TT_TT;
                        //Update lại tiền thanh toán
                        new Update(KcbThanhtoan.Schema)
                            .Set(KcbThanhtoan.Columns.TongTien).EqualTo(TT_BHYT + TT_BN)
                            .Set(KcbThanhtoan.Columns.BnhanChitra).EqualTo(TT_BNCT)
                            .Set(KcbThanhtoan.Columns.BhytChitra).EqualTo(TT_BHYT)
                            .Set(KcbThanhtoan.Columns.PhuThu).EqualTo(TT_PT)
                            .Set(KcbThanhtoan.Columns.TuTuc).EqualTo(TT_TT)
                            .Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(objPhieuchi.IdThanhtoan).Execute();

                        var objPhieuthu = new KcbPhieuthu();
                        objPhieuthu.IdThanhtoan = objPhieuchi.IdThanhtoan;
                        objPhieuthu.IdBenhnhan = objPhieuchi.IdBenhnhan;
                        objPhieuthu.MaLuotkham = objPhieuchi.MaLuotkham;
                        objPhieuthu.NoiDung = noidunghuy;
                        objPhieuthu.SoluongChungtugoc = 1;
                        objPhieuthu.LoaiPhieuthu = Convert.ToByte(1); //0= phiếu thu tiền;1= phiếu chi
                        objPhieuthu.MaPhieuthu = THU_VIEN_CHUNG.GetMaPhieuThu(globalVariables.SysDate, 1);
                        objPhieuthu.NgayThuchien = globalVariables.SysDate;
                        objPhieuthu.SoTien = TT_BN - TT_Chietkhau_Chitiet;
                        objPhieuthu.SotienGoc = TT_BN;
                        objPhieuthu.MaLydoChietkhau = objPhieuchi.MaLydoChietkhau;
                        objPhieuthu.TienChietkhauchitiet = TT_Chietkhau_Chitiet;
                        objPhieuthu.TienChietkhau = objPhieuchi.TongtienChietkhau;
                        objPhieuthu.TienChietkhauhoadon = objPhieuthu.TienChietkhau - objPhieuthu.TienChietkhauchitiet;
                        objPhieuthu.NguoiNop = globalVariables.UserName;
                        objPhieuthu.TaikhoanCo = "";
                        objPhieuthu.TaikhoanNo = "";
                        objPhieuthu.NoiTru = objPhieuchi.NoiTru;
                        objPhieuthu.LydoNop = lydotratien;
                        objPhieuthu.IdKhoaThuchien = globalVariables.idKhoatheoMay;
                        objPhieuthu.IdNhanvien = globalVariables.gv_intIDNhanvien;
                        objPhieuthu.IsNew = true;
                        objPhieuthu.Save();

                        //Kết thúc tạo thông tin phiếu trả tiền(Phiếu chi)-->Kế tiếp cần tính toán lại % BHYT và tiền chênh lệch cho đối tượng BHYT
                        //Riêng đối tượng dịch vụ Giữ nguyên các giá trị thanh toán(Thực thu= Tổng thanh toán-Tổng trả lại)
                        if (THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb))
                        {
                            vDblTongtienHuy = TongtienKhongTutuc(arrKcbThanhtoanChitietHuy);
                            //Thường chỉ trả về 1 bản ghi thanh toán duy nhất vì là đối tượng BHYT
                            KcbThanhtoanCollection lstKcbThanhtoanCollection =
                                new KcbThanhtoanController()
                                    .FetchByQuery(
                                        KcbThanhtoan.CreateQuery()
                                            .AddWhere(KcbThanhtoan.Columns.MaLuotkham, Comparison.Equals,
                                                objLuotkham.MaLuotkham)
                                            .AND(KcbThanhtoan.Columns.IdBenhnhan, Comparison.Equals,
                                                objLuotkham.IdBenhnhan)
                                            .AND(KcbThanhtoan.Columns.KieuThanhtoan, Comparison.Equals, 0)
                                    );
                            List<long> lstIdThanhtoanAll = (from q in lstKcbThanhtoanCollection
                                select q.IdThanhtoan).Distinct().ToList();
                            //Biến chứa danh sách tất cả các chi tiết dùng để tính lại tổng tiền thanh toán cho thanh toán có bản ghi bị hủy
                            var lstKcbThanhtoanChitiet_Tatca = new List<KcbThanhtoanChitiet>();
                            if (lstIdThanhtoanAll.Count > 0)
                                lstKcbThanhtoanChitiet_Tatca =
                                    new Select().From(KcbThanhtoanChitiet.Schema)
                                        .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan).In(lstIdThanhtoanAll)
                                        .ExecuteAsCollection<KcbThanhtoanChitietCollection>()
                                        .ToList<KcbThanhtoanChitiet>();

                            v_TotalPaymentDetail = (from p in lstKcbThanhtoanChitiet_Tatca
                                where p.TuTuc == 0
                                select p).Sum(c => Utility.DecimaltoDbnull(c.SoLuong)*c.DonGia);

                            //Tính lại % BHYT mới sau khi đã trả lại tiền một số dịch vụ
                            LayThongtinPtramBHYT(v_TotalPaymentDetail - vDblTongtienHuy, objLuotkham, ref ptramBhyt);


                            //Tính lại thông tin BHYT,BN chi trả cho toàn bộ các chi tiết của BN đã thanh toán mà không bị hủy
                            var lsttemp = new List<KcbThanhtoanChitiet>();
                            THU_VIEN_CHUNG.TinhPhamTramBHYT(objLuotkham, ref lsttemp, ref lstKcbThanhtoanChitiet_Tatca,
                                ptramBhyt);
                            List<long> lstIdThanhtoanTinhlai = (from q in lstKcbThanhtoanChitiet_Tatca
                                select q.IdThanhtoan).Distinct().ToList();
                            //99% đặt thông số này=1
                            if (
                                THU_VIEN_CHUNG.Laygiatrithamsohethong(
                                    "KCB_THANHTOAN_TINHLAITONGTIEN_CACTHANHTOAN_BITRALAITIEN", "1", false) == "1")
                            {
                                foreach (int IdThanhtoan in lstIdThanhtoanTinhlai)
                                    //Chỉ thực hiện tính lại thanh toán có chứa các chi tiết bị thay đổi
                                {
                                    TT_BN = 0m;
                                    TT_BNCT = 0m;
                                    TT_PT = 0m;
                                    TT_TT = 0m;
                                    TT_BHYT = 0m;
                                    TT_Chietkhau_Chitiet = 0;
                                    //Lấy lại từ CSDL
                                    List<KcbThanhtoanChitiet> _LstChitiet = (from p in lstKcbThanhtoanChitiet_Tatca
                                        where p.IdThanhtoan == IdThanhtoan
                                        select p).ToList<KcbThanhtoanChitiet>();

                                    if (_LstChitiet.Count > 0)
                                    {
                                        foreach (KcbThanhtoanChitiet objChitietThanhtoan in _LstChitiet)
                                        {
                                            objChitietThanhtoan.MarkOld();
                                            objChitietThanhtoan.IsNew = false;
                                            objChitietThanhtoan.Save();
                                            if (!Utility.Byte2Bool(objChitietThanhtoan.TrangthaiHuy))
                                                //Bỏ qua các bản ghi đã bị hủy
                                            {
                                                if (!Utility.Byte2Bool(objChitietThanhtoan.TuTuc))
                                                    TT_BHYT += objChitietThanhtoan.BhytChitra*
                                                               Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                                TT_Chietkhau_Chitiet +=
                                                    Utility.DecimaltoDbnull(objChitietThanhtoan.TienChietkhau, 0);
                                                TT_PT += objChitietThanhtoan.PhuThu*Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                                if (Utility.Byte2Bool(objChitietThanhtoan.TuTuc))
                                                    TT_TT += objChitietThanhtoan.BnhanChitra*Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                                else
                                                    TT_BNCT += objChitietThanhtoan.BnhanChitra*
                                                               Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                            }
                                        }
                                        TT_BN += TT_PT + TT_BNCT + TT_TT;
                                        //Update lại tiền thanh toán
                                        new Update(KcbThanhtoan.Schema)
                                            .Set(KcbThanhtoan.Columns.TongTien).EqualTo(TT_BHYT + TT_BN)
                                            .Set(KcbThanhtoan.Columns.BnhanChitra).EqualTo(TT_BNCT)
                                            .Set(KcbThanhtoan.Columns.BhytChitra).EqualTo(TT_BHYT)
                                            .Set(KcbThanhtoan.Columns.PhuThu).EqualTo(TT_PT)
                                            .Set(KcbThanhtoan.Columns.TuTuc).EqualTo(TT_TT)
                                            .Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(IdThanhtoan).Execute();
                                        //Update phiếu thu
                                        new Update(KcbPhieuthu.Schema)
                                            .Set(KcbPhieuthu.Columns.SoTien).EqualTo(TT_BN - TT_Chietkhau_Chitiet)
                                            .Set(KcbPhieuthu.Columns.SotienGoc).EqualTo(TT_BN)
                                            .Where(KcbPhieuthu.Columns.IdThanhtoan).IsEqualTo(IdThanhtoan).Execute();
                                    }
                                }
                            }
                        }
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi trong qua trinh tra tien lai:{0}", exception.ToString());
                return ActionResult.Error;
            }
        }

        public ActionResult HuyPhieuchi(KcbThanhtoan objPhieuchi, KcbLuotkham objLuotkham, string lydohuy)
        {
            try
            {
                decimal PtramBHYT = 0;
                ///tổng tiền hiện tại truyền vào của lần payment đang thực hiện
                decimal v_dblTongtienHuy = 0;
                ///tổng tiền đã thanh toán
                decimal v_TotalPaymentDetail = 0;
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        if (objPhieuchi != null)
                        {
                            var lstKcbThanhtoanChitiet_Phieuchi =
                                new Select().From(KcbThanhtoanChitiet.Schema)
                                    .Where(KcbThanhtoanChitiet.Columns.IdThanhtoanhuy)
                                    .IsEqualTo(objPhieuchi.IdThanhtoan)
                                    .ExecuteAsCollection<KcbThanhtoanChitietCollection>();

                            List<long> lstIDThanhtoanKhoiphuc = (from p in lstKcbThanhtoanChitiet_Phieuchi
                                select Utility.Int64Dbnull(p.IdThanhtoan, -1)).ToList<long>();

                            //Khôi phục lại trạng thái hủy
                            foreach (KcbThanhtoanChitiet objKcbThanhtoanChitiet in lstKcbThanhtoanChitiet_Phieuchi)
                            {
                                objKcbThanhtoanChitiet.IsNew = false;
                                objKcbThanhtoanChitiet.MarkOld();
                                objKcbThanhtoanChitiet.TrangthaiHuy = 0;
                                objKcbThanhtoanChitiet.IdThanhtoanhuy = -1;
                                objKcbThanhtoanChitiet.Save();

                                ///thanh toán khám chữa bệnh))
                                if (objKcbThanhtoanChitiet.IdLoaithanhtoan == 1)
                                {
                                    new Update(KcbDangkyKcb.Schema)
                                        .Set(KcbDangkyKcb.Columns.TrangthaiHuy).EqualTo(0)
                                        .Where(KcbDangkyKcb.Columns.IdKham)
                                        .IsEqualTo(objKcbThanhtoanChitiet.IdPhieu)
                                        .Execute();
                                }
                                ///thah toán phần dịch vụ cận lâm sàng
                                if (objKcbThanhtoanChitiet.IdLoaithanhtoan == 2)
                                {
                                    KcbChidinhclsChitiet objKcbChidinhclsChitiet =
                                        KcbChidinhclsChitiet.FetchByID(objKcbThanhtoanChitiet.IdPhieuChitiet);
                                    if (objKcbChidinhclsChitiet != null && objKcbChidinhclsChitiet.TrangThai >= 3)
                                        //Đã có kết quả
                                    {
                                        return ActionResult.AssignIsConfirmed;
                                    }

                                    new Update(KcbChidinhclsChitiet.Schema)
                                        .Set(KcbChidinhclsChitiet.Columns.TrangthaiHuy).EqualTo(0)
                                        .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh)
                                        .IsEqualTo(objKcbThanhtoanChitiet.IdPhieuChitiet)
                                        .Execute();
                                }
                                ///thanh toán phần thuốc
                                if (objKcbThanhtoanChitiet.IdLoaithanhtoan == 3)
                                {
                                    KcbDonthuocChitiet objKcbDonthuocChitiet =
                                        KcbDonthuocChitiet.FetchByID(objKcbThanhtoanChitiet.IdPhieuChitiet);

                                    if (objKcbDonthuocChitiet != null &&
                                        Utility.Byte2Bool(objKcbDonthuocChitiet.TrangThai))
                                    {
                                        return ActionResult.PresIsConfirmed;
                                    }

                                    new Update(KcbDonthuocChitiet.Schema)
                                        .Set(KcbDonthuocChitiet.Columns.TrangthaiHuy).EqualTo(0)
                                        .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc)
                                        .IsEqualTo(objKcbThanhtoanChitiet.IdPhieuChitiet)
                                        .Execute();
                                }
                            }
                            //Ghi lại log hủy
                            var objKcbLoghuy = new KcbLoghuy();
                            objKcbLoghuy.IdBenhnhan = objPhieuchi.IdBenhnhan;
                            objKcbLoghuy.MaLuotkham = objPhieuchi.MaLuotkham;
                            objKcbLoghuy.IdNhanvien = globalVariables.gv_intIDNhanvien;
                            objKcbLoghuy.SotienHuy = objPhieuchi.TongTien;
                            objKcbLoghuy.LydoHuy = lydohuy;
                            objKcbLoghuy.NgayHuy = DateTime.Now;
                            objKcbLoghuy.NgayTao = DateTime.Now;
                            objKcbLoghuy.NguoiTao = globalVariables.UserName;
                            objKcbLoghuy.IsNew = true;
                            objKcbLoghuy.LoaiphieuHuy = Utility.ByteDbnull(objPhieuchi.KieuThanhtoan, 0);
                            objKcbLoghuy.Save();
                            //Xóa các thông tin phiếu chi
                            new Delete().From(KcbThanhtoan.Schema)
                                .Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(objPhieuchi.IdThanhtoan).Execute();
                            new Delete().From(KcbPhieuthu.Schema)
                                .Where(KcbPhieuthu.Columns.IdThanhtoan).IsEqualTo(objPhieuchi.IdThanhtoan)
                                .And(KcbPhieuthu.Columns.LoaiPhieuthu).IsEqualTo(1).Execute();
                            //Đối tượng Dịch vụ không cần khôi phục dữ liệu
                            //Đối tượng BHYT cần tính toán lại % BHYT để xác định có cần trả lại số tiền >Số tiền hủy cho BN hay không?

                            if (THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb))
                            {
                                decimal TT_BN = 0m;
                                decimal TT_BNCT = 0m;
                                decimal TT_PT = 0m;
                                decimal TT_TT = 0m;
                                decimal TT_BHYT = 0m;
                                decimal TT_Chietkhau_Chitiet = 0m;

                                //Thường chỉ trả về 1 bản ghi thanh toán duy nhất vì là đối tượng BHYT
                                KcbThanhtoanCollection lstKcbThanhtoanCollection =
                                    new KcbThanhtoanController()
                                        .FetchByQuery(
                                            KcbThanhtoan.CreateQuery()
                                                .AddWhere(KcbThanhtoan.Columns.MaLuotkham, Comparison.Equals,
                                                    objLuotkham.MaLuotkham)
                                                .AND(KcbThanhtoan.Columns.IdBenhnhan, Comparison.Equals,
                                                    objLuotkham.IdBenhnhan)
                                                .AND(KcbThanhtoan.Columns.KieuThanhtoan, Comparison.Equals, 0)
                                        );
                                List<long> lstIdThanhtoanAll = (from q in lstKcbThanhtoanCollection
                                    select q.IdThanhtoan).Distinct().ToList();
                                //Biến chứa danh sách tất cả các chi tiết dùng để tính lại tổng tiền thanh toán cho thanh toán có bản ghi bị hủy
                                var lstKcbThanhtoanChitiet_Tatca = new List<KcbThanhtoanChitiet>();
                                if (lstIdThanhtoanAll.Count > 0)
                                    lstKcbThanhtoanChitiet_Tatca =
                                        new Select().From(KcbThanhtoanChitiet.Schema)
                                            .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan).In(lstIdThanhtoanAll)
                                            .ExecuteAsCollection<KcbThanhtoanChitietCollection>()
                                            .ToList<KcbThanhtoanChitiet>();

                                v_TotalPaymentDetail = (from p in lstKcbThanhtoanChitiet_Tatca
                                    where p.TuTuc == 0
                                    select p).Sum(c => Utility.DecimaltoDbnull(c.SoLuong)*c.DonGia);

                                //Tính lại % BHYT mới sau khi đã trả lại tiền một số dịch vụ
                                LayThongtinPtramBHYT(v_TotalPaymentDetail, objLuotkham, ref PtramBHYT);


                                //Tính lại thông tin BHYT,BN chi trả cho toàn bộ các chi tiết của BN đã thanh toán mà không bị hủy
                                var lsttemp = new List<KcbThanhtoanChitiet>();
                                THU_VIEN_CHUNG.TinhPhamTramBHYT(objLuotkham, ref lsttemp,
                                    ref lstKcbThanhtoanChitiet_Tatca, PtramBHYT);
                                if (
                                    THU_VIEN_CHUNG.Laygiatrithamsohethong(
                                        "KCB_THANHTOAN_TINHLAITONGTIEN_CACTHANHTOAN_BITRALAITIEN", "1", false) == "1")
                                {
                                    foreach (int IdThanhtoan in lstIdThanhtoanAll)
                                        //Chỉ thực hiện tính lại thanh toán có chứa các chi tiết bị thay đổi
                                    {
                                        TT_BN = 0m;
                                        TT_BNCT = 0m;
                                        TT_PT = 0m;
                                        TT_TT = 0m;
                                        TT_BHYT = 0m;
                                        TT_Chietkhau_Chitiet = 0;
                                        //Lấy lại từ CSDL
                                        List<KcbThanhtoanChitiet> _LstChitiet = (from p in lstKcbThanhtoanChitiet_Tatca
                                            where p.IdThanhtoan == IdThanhtoan
                                            select p).ToList<KcbThanhtoanChitiet>();

                                        if (_LstChitiet.Count > 0)
                                        {
                                            foreach (KcbThanhtoanChitiet objChitietThanhtoan in _LstChitiet)
                                            {
                                                if (!Utility.Byte2Bool(objChitietThanhtoan.TrangthaiHuy))
                                                    //Bỏ qua các bản ghi đã bị hủy
                                                {
                                                    if (!Utility.Byte2Bool(objChitietThanhtoan.TuTuc))
                                                        TT_BHYT += objChitietThanhtoan.BhytChitra*
                                                                   Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                                    TT_Chietkhau_Chitiet +=
                                                        Utility.DecimaltoDbnull(objChitietThanhtoan.TienChietkhau, 0);
                                                    TT_PT += objChitietThanhtoan.PhuThu*Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                                    if (Utility.Byte2Bool(objChitietThanhtoan.TuTuc))
                                                        TT_TT += objChitietThanhtoan.BnhanChitra*
                                                                 Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                                    else
                                                        TT_BNCT += objChitietThanhtoan.BnhanChitra*
                                                                   Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                                }
                                            }
                                            TT_BN += TT_PT + TT_BNCT + TT_TT;
                                            //Update lại tiền thanh toán
                                            new Update(KcbThanhtoan.Schema)
                                                .Set(KcbThanhtoan.Columns.TongTien).EqualTo(TT_BHYT + TT_BN)
                                                .Set(KcbThanhtoan.Columns.BnhanChitra).EqualTo(TT_BNCT)
                                                .Set(KcbThanhtoan.Columns.BhytChitra).EqualTo(TT_BHYT)
                                                .Set(KcbThanhtoan.Columns.PhuThu).EqualTo(TT_PT)
                                                .Set(KcbThanhtoan.Columns.TuTuc).EqualTo(TT_TT)
                                                .Where(KcbThanhtoan.Columns.IdThanhtoan)
                                                .IsEqualTo(IdThanhtoan)
                                                .Execute();
                                            //Update phiếu thu
                                            new Update(KcbPhieuthu.Schema)
                                                .Set(KcbPhieuthu.Columns.SoTien).EqualTo(TT_BN - TT_Chietkhau_Chitiet)
                                                .Set(KcbPhieuthu.Columns.SotienGoc).EqualTo(TT_BN)
                                                .Where(KcbPhieuthu.Columns.IdThanhtoan).IsEqualTo(IdThanhtoan).Execute();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Ban ra loi Exception={0}", exception);
                return ActionResult.Error;
            }
        }

        public ActionResult Tratien_Old(KcbThanhtoan objThanhtoan, KcbLuotkham objLuotkham, List<Int64> lstIdChitiet,
            string malydohuy, string noidunghuy, string lydotratien)
        {
            decimal PtramBHYT = 0;
            ///tổng tiền hiện tại truyền vào của lần payment đang thực hiện
            decimal v_dblTongtienHuy = 0;
            ///tổng tiền đã thanh toán
            decimal v_TotalPaymentDetail = 0;
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        List<KcbThanhtoanChitiet> ArrKcbThanhtoanChitiet_Huy =
                            new Select().From(KcbThanhtoanChitiet.Schema)
                                .Where(KcbThanhtoanChitiet.Columns.IdChitiet).In(lstIdChitiet)
                                .ExecuteAsCollection<KcbThanhtoanChitietCollection>().ToList<KcbThanhtoanChitiet>();

                        List<long> lstIdThanhtoanTinhtoanlai = (from q in ArrKcbThanhtoanChitiet_Huy
                            select q.IdThanhtoan).ToList<long>();

                        v_dblTongtienHuy = TongtienKhongTutuc(ArrKcbThanhtoanChitiet_Huy);
                        KcbThanhtoanCollection lstKcbThanhtoanCollection =
                            new KcbThanhtoanController()
                                .FetchByQuery(
                                    KcbThanhtoan.CreateQuery()
                                        .AddWhere(KcbThanhtoan.Columns.MaLuotkham, Comparison.Equals,
                                            objLuotkham.MaLuotkham)
                                        .AND(KcbThanhtoan.Columns.IdBenhnhan, Comparison.Equals, objLuotkham.IdBenhnhan)
                                        .AND(KcbThanhtoan.Columns.KieuThanhtoan, Comparison.Equals,
                                            objThanhtoan.KieuThanhtoan)
                                ); //Chỉ lấy về các bản ghi thanh toán thường(0= thường;1= thanh toán hủy(trả lại tiền))
                        //Lấy tổng tiền của các lần thanh toán trước
                        var lstKcbThanhtoanChitiet = new List<KcbThanhtoanChitiet>();

                        foreach (KcbThanhtoan Payment in lstKcbThanhtoanCollection)
                        {
                            var lstKcbThanhtoanChitietCollection = new Select().From(KcbThanhtoanChitiet.Schema)
                                .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan).IsEqualTo(Payment.IdThanhtoan)
                                .And(KcbThanhtoanChitiet.Columns.TrangthaiHuy).IsEqualTo(0).ExecuteAsCollection
                                <KcbThanhtoanChitietCollection>();

                            foreach (KcbThanhtoanChitiet _KcbThanhtoanChitiet in lstKcbThanhtoanChitietCollection)
                            {
                                if (_KcbThanhtoanChitiet.TuTuc == 0)
                                {
                                    //Lấy các chi tiết sẽ update lại toàn bộ thông tin bhyt,bn chi trả theo % BHYT mới sau khi đã hủy một số dịch vụ
                                    //Các bản ghi hủy sẽ giữ nguyên thông tin không cần cập nhật
                                    if (!lstIdChitiet.Contains(_KcbThanhtoanChitiet.IdChitiet))
                                    {
                                        lstKcbThanhtoanChitiet.Add(_KcbThanhtoanChitiet);
                                        _KcbThanhtoanChitiet.IsNew = false;
                                        _KcbThanhtoanChitiet.MarkOld();
                                    }
                                    v_TotalPaymentDetail += Utility.Int32Dbnull(_KcbThanhtoanChitiet.SoLuong)*
                                                            Utility.DecimaltoDbnull(_KcbThanhtoanChitiet.DonGia);
                                }
                            }
                        }
                        List<long> lstIdThanhtoanCu = (from q in lstKcbThanhtoanChitiet
                            select q.IdThanhtoan).Distinct().ToList();


                        LayThongtinPtramBHYT(v_TotalPaymentDetail - v_dblTongtienHuy, objLuotkham, ref PtramBHYT);
                        //Thêm mới dòng thanh toán hủy
                        objThanhtoan.KieuThanhtoan = 1;
                        objThanhtoan.IdNhanvienThanhtoan = globalVariables.gv_intIDNhanvien;
                        objThanhtoan.NgayThanhtoan = globalVariables.SysDate;
                        objThanhtoan.MaThanhtoan = THU_VIEN_CHUNG.TaoMathanhtoan(globalVariables.SysDate);
                        objThanhtoan.MaLydoHuy = malydohuy;
                        objThanhtoan.IsNew = true;
                        objThanhtoan.Save();
                        //Tính lại thông tin BHYT,BN chi trả cho toàn bộ các chi tiết của BN đã thanh toán mà không bị hủy
                        var lsttemp = new List<KcbThanhtoanChitiet>();
                        THU_VIEN_CHUNG.TinhPhamTramBHYT(objLuotkham, ref lsttemp, ref lstKcbThanhtoanChitiet, PtramBHYT);
                        decimal TT_BN = 0m;
                        decimal TT_BHYT = 0m;
                        decimal TT_Chietkhau_Chitiet = 0m;
                        //99% đặt thông số này=1
                        if (
                            THU_VIEN_CHUNG.Laygiatrithamsohethong(
                                "KCB_THANHTOAN_TINHLAITONGTIEN_CACTHANHTOAN_BITRALAITIEN", "1", false) == "1")
                        {
                            foreach (int IdThanhtoan in lstIdThanhtoanCu)
                            {
                                TT_BN = 0m;
                                TT_BHYT = 0m;
                                TT_Chietkhau_Chitiet = 0m;
                                List<KcbThanhtoanChitiet> _LstChitiet = (from q in lstKcbThanhtoanChitiet
                                    where q.IdThanhtoan == IdThanhtoan
                                    select q).ToList<KcbThanhtoanChitiet>();

                                if (_LstChitiet.Count > 0)
                                {
                                    foreach (KcbThanhtoanChitiet objChitietThanhtoan in _LstChitiet)
                                    {
                                        TT_BN += (objChitietThanhtoan.BnhanChitra + objChitietThanhtoan.PhuThu)*
                                                 Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                        if (!Utility.Byte2Bool(objChitietThanhtoan.TuTuc))
                                            TT_BHYT += objChitietThanhtoan.BhytChitra*Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                        TT_Chietkhau_Chitiet +=
                                            Utility.DecimaltoDbnull(objChitietThanhtoan.TienChietkhau, 0);
                                        //Lưu lại các thông tin tiền đã được tính toán lại ở thủ tục THU_VIEN_CHUNG.TinhPhamTramBHYT(...)
                                        objChitietThanhtoan.IsNew = false;
                                        objChitietThanhtoan.MarkOld();
                                        objChitietThanhtoan.Save();
                                    }
                                    //Update lại tiền thanh toán
                                    new Update(KcbThanhtoan.Schema)
                                        .Set(KcbThanhtoan.Columns.TongTien).EqualTo(TT_BHYT + TT_BN)
                                        .Set(KcbThanhtoan.Columns.BnhanChitra).EqualTo(TT_BN)
                                        .Set(KcbThanhtoan.Columns.BhytChitra).EqualTo(TT_BHYT)
                                        .Set(KcbThanhtoan.Columns.MaDoituongKcb).EqualTo(objLuotkham.MaDoituongKcb)
                                        .Set(KcbThanhtoan.Columns.IdDoituongKcb).EqualTo(objLuotkham.IdDoituongKcb)
                                        .Set(KcbThanhtoan.Columns.PtramBhyt).EqualTo(objLuotkham.PtramBhyt)
                                        .Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(IdThanhtoan).Execute();
                                    //Update phiếu thu
                                    new Update(KcbPhieuthu.Schema)
                                        .Set(KcbPhieuthu.Columns.SoTien).EqualTo(TT_BN - TT_Chietkhau_Chitiet)
                                        .Set(KcbPhieuthu.Columns.SotienGoc).EqualTo(TT_BN)
                                        .Where(KcbPhieuthu.Columns.IdThanhtoan).IsEqualTo(IdThanhtoan).Execute();
                                }
                            }
                        }
                        //Reset và tính toán các số tiền liên quan đến các bản ghi hủy
                        TT_BN = 0m;
                        TT_BHYT = 0m;
                        TT_Chietkhau_Chitiet = 0m;
                        //Cập nhật các dòng chi tiết được chọn hủy về trạng thái hủy và các dịch vụ trong các bảng tương ứng theo id_loaithanhtoan
                        foreach (KcbThanhtoanChitiet objKcbThanhtoanChitiet in ArrKcbThanhtoanChitiet_Huy)
                        {
                            TT_BN += (objKcbThanhtoanChitiet.BnhanChitra + objKcbThanhtoanChitiet.PhuThu)*
                                     Utility.DecimaltoDbnull(objKcbThanhtoanChitiet.SoLuong);
                            if (!Utility.Byte2Bool(objKcbThanhtoanChitiet.TuTuc))
                                TT_BHYT += objKcbThanhtoanChitiet.BhytChitra*Utility.DecimaltoDbnull(objKcbThanhtoanChitiet.SoLuong);
                            TT_Chietkhau_Chitiet += Utility.DecimaltoDbnull(objKcbThanhtoanChitiet.TienChietkhau, 0);

                            new Update(KcbThanhtoanChitiet.Schema)
                                .Set(KcbThanhtoanChitiet.Columns.TrangthaiHuy).EqualTo(1)
                                .Where(KcbThanhtoanChitiet.Columns.IdChitiet)
                                .IsEqualTo(objKcbThanhtoanChitiet.IdChitiet)
                                .
                                Execute();
                            ///thanh toán khám chữa bệnh))
                            if (objKcbThanhtoanChitiet.IdLoaithanhtoan == 1)
                            {
                                new Update(KcbDangkyKcb.Schema)
                                    .Set(KcbDangkyKcb.Columns.TrangthaiHuy).EqualTo(1)
                                    .Where(KcbDangkyKcb.Columns.IdKham)
                                    .IsEqualTo(objKcbThanhtoanChitiet.IdPhieu)
                                    .Execute();
                            }
                            ///thah toán phần dịch vụ cận lâm sàng
                            if (objKcbThanhtoanChitiet.IdLoaithanhtoan == 2)
                            {
                                KcbChidinhclsChitiet objKcbChidinhclsChitiet =
                                    KcbChidinhclsChitiet.FetchByID(objKcbThanhtoanChitiet.IdPhieuChitiet);
                                if (globalVariables.UserName != "ADMIN")
                                {
                                    if (objKcbChidinhclsChitiet != null && objKcbChidinhclsChitiet.TrangThai >= 3)
                                        //Đã có kết quả
                                    {
                                        return ActionResult.AssignIsConfirmed;
                                    }
                                }
                                new Update(KcbChidinhclsChitiet.Schema)
                                    .Set(KcbChidinhclsChitiet.Columns.TrangthaiHuy).EqualTo(1)
                                    .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh)
                                    .IsEqualTo(objKcbThanhtoanChitiet.IdPhieuChitiet)
                                    .Execute();
                            }
                            ///thanh toán phần thuốc
                            if (objKcbThanhtoanChitiet.IdLoaithanhtoan == 3)
                            {
                                KcbDonthuocChitiet objKcbDonthuocChitiet =
                                    KcbDonthuocChitiet.FetchByID(objKcbThanhtoanChitiet.IdPhieuChitiet);

                                if (globalVariables.UserName != "ADMIN")
                                {
                                    if (objKcbDonthuocChitiet != null &&
                                        Utility.Byte2Bool(objKcbDonthuocChitiet.TrangThai))
                                    {
                                        return ActionResult.PresIsConfirmed;
                                    }
                                }
                                new Update(KcbDonthuoc.Schema)
                                    .Set(KcbDonthuoc.Columns.TrangThai).EqualTo(0)
                                    .Where(KcbDonthuoc.Columns.IdDonthuoc)
                                    .IsEqualTo(objKcbThanhtoanChitiet.IdPhieu)
                                    .Execute();
                                new Update(KcbDonthuocChitiet.Schema)
                                    .Set(KcbDonthuocChitiet.Columns.TrangthaiHuy).EqualTo(1)
                                    .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc)
                                    .IsEqualTo(objKcbThanhtoanChitiet.IdPhieuChitiet)
                                    .Execute();
                            }
                            //Tạo dữ liệu hủy tiền

                            objKcbThanhtoanChitiet.IdThanhtoanhuy = objThanhtoan.IdThanhtoan;
                                //Để biết dòng hủy này hủy cho chi tiết thanh toán nào
                            objKcbThanhtoanChitiet.TrangthaiHuy = 0;
                            objKcbThanhtoanChitiet.IsNew = true;
                            objKcbThanhtoanChitiet.Save();
                        }

                        var objPhieuthu = new KcbPhieuthu();
                        objPhieuthu.IdThanhtoan = objThanhtoan.IdThanhtoan;
                        objPhieuthu.IdBenhnhan = objThanhtoan.IdBenhnhan;
                        objPhieuthu.MaLuotkham = objThanhtoan.MaLuotkham;
                        objPhieuthu.NoiDung = noidunghuy;
                        objPhieuthu.SoluongChungtugoc = 1;
                        objPhieuthu.LoaiPhieuthu = Convert.ToByte(1); //0= phiếu thu tiền;1= phiếu chi
                        objPhieuthu.MaPhieuthu = THU_VIEN_CHUNG.GetMaPhieuThu(globalVariables.SysDate, 1);
                        objPhieuthu.NgayThuchien = globalVariables.SysDate;
                        objPhieuthu.SoTien = TT_BN - TT_Chietkhau_Chitiet;
                        objPhieuthu.SotienGoc = TT_BN;
                        objPhieuthu.MaLydoChietkhau = objThanhtoan.MaLydoChietkhau;
                        objPhieuthu.TienChietkhauchitiet = TT_Chietkhau_Chitiet;
                        objPhieuthu.TienChietkhau = objThanhtoan.TongtienChietkhau;
                        objPhieuthu.TienChietkhauhoadon = objPhieuthu.TienChietkhau - objPhieuthu.TienChietkhauchitiet;
                        objPhieuthu.NguoiNop = globalVariables.UserName;
                        objPhieuthu.TaikhoanCo = "";
                        objPhieuthu.TaikhoanNo = "";
                        objPhieuthu.NoiTru = objThanhtoan.NoiTru;
                        objPhieuthu.LydoNop = lydotratien;
                        objPhieuthu.IdKhoaThuchien = globalVariables.idKhoatheoMay;
                        objPhieuthu.IdNhanvien = globalVariables.gv_intIDNhanvien;
                        objPhieuthu.IsNew = true;
                        objPhieuthu.Save();

                        new Update(KcbThanhtoan.Schema)
                            .Set(KcbThanhtoan.Columns.TongTien).EqualTo(TT_BHYT + TT_BN)
                            .Set(KcbThanhtoan.Columns.BnhanChitra).EqualTo(TT_BN)
                            .Set(KcbThanhtoan.Columns.BhytChitra).EqualTo(TT_BHYT)
                            .Set(KcbThanhtoan.Columns.MaDoituongKcb).EqualTo(objLuotkham.MaDoituongKcb)
                            .Set(KcbThanhtoan.Columns.IdDoituongKcb).EqualTo(objLuotkham.IdDoituongKcb)
                            .Set(KcbThanhtoan.Columns.PtramBhyt).EqualTo(objLuotkham.PtramBhyt)
                            .Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi trong qua trinh tra tien lai:{0}", exception.ToString());
                return ActionResult.Error;
            }
        }

        public ActionResult UpdatePhieuDCT(KcbPhieuDct objPhieuDct, KcbLuotkham objLuotkham)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        SqlQuery sqlQuery = new Select().From<KcbPhieuDct>()
                            .Where(KcbPhieuDct.Columns.MaLuotkham)
                            .IsEqualTo(objPhieuDct.MaLuotkham)
                            .And(KcbPhieuDct.Columns.IdBenhnhan).IsEqualTo(objPhieuDct.IdBenhnhan)
                            .And(KcbPhieuDct.Columns.LoaiThanhtoan).IsEqualTo(objPhieuDct.LoaiThanhtoan);
                        if (sqlQuery.GetRecordCount() <= 0)
                        {
                            //objPhieuDct.IsNew = true;
                            //objPhieuDct.Save();
                          StoredProcedure sp =   SPs.SpKcbPhieudctInsert(objPhieuDct.MaPhieuDct, objPhieuDct.MaLuotkham,
                                objPhieuDct.IdBenhnhan, objPhieuDct.TongTien, objPhieuDct.BhytChitra,
                                objPhieuDct.BnhanChitra, objPhieuDct.NguoiTao, objPhieuDct.NgayTao, objPhieuDct.NgaySua,
                                objPhieuDct.NguoiSua, objPhieuDct.LoaiThanhtoan, objPhieuDct.MaKhoaThuchien
                                , objPhieuDct.TenLoaithanhtoan, objPhieuDct.IdLichsuDoituongKcb, objPhieuDct.MatheBhyt,
                                objPhieuDct.IpMaytao, objPhieuDct.IpMaysua, objPhieuDct.TenMaytao, objPhieuDct.TenMaysua,
                                objPhieuDct.TrangthaiXml);
                            sp.Execute();
                            objLuotkham.TrangthaiNgoaitru = 1;
                            objLuotkham.Locked = 1;
                            objLuotkham.NgayKetthuc = objPhieuDct.NgayTao;
                            new Update(KcbLuotkham.Schema)
                                .Set(KcbLuotkham.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                                .Set(KcbLuotkham.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                                .Set(KcbLuotkham.Columns.Locked).EqualTo(objLuotkham.Locked)
                                .Set(KcbLuotkham.Columns.TrangthaiNgoaitru)
                                .EqualTo(Utility.Int32Dbnull(objLuotkham.TrangthaiNgoaitru))
                                .Set(KcbLuotkham.Columns.LydoKetthuc).EqualTo("In phôi bảo hiểm")
                                .Set(KcbLuotkham.Columns.IpMaysua).EqualTo(objPhieuDct.IpMaysua)
                                .Set(KcbLuotkham.Columns.TenMaysua).EqualTo(objPhieuDct.TenMaysua)
                                .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objPhieuDct.MaLuotkham)
                                .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objPhieuDct.IdBenhnhan).Execute();
                        }
                        else
                        {
                            StoredProcedure sp = SPs.SpKcbPhieudctUpdate(objPhieuDct.MaLuotkham, objPhieuDct.IdBenhnhan,
                                objPhieuDct.TongTien, objPhieuDct.BhytChitra, objPhieuDct.BnhanChitra,
                                globalVariables.SysDate, globalVariables.UserName
                                , objPhieuDct.LoaiThanhtoan, globalVariables.gv_strIPAddress,
                                globalVariables.gv_strComputerName);
                            sp.Execute();
                            //new Update(KcbPhieuDct.Schema)
                            //    .Set(KcbPhieuDct.Columns.TongTien).EqualTo(objPhieuDct.TongTien)
                            //    .Set(KcbPhieuDct.Columns.BnhanChitra).EqualTo(objPhieuDct.BnhanChitra)
                            //    .Set(KcbPhieuDct.Columns.BhytChitra).EqualTo(objPhieuDct.BhytChitra)
                            //    .Set(KcbPhieuDct.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                            //    .Set(KcbPhieuDct.Columns.IpMaysua).EqualTo(objPhieuDct.IpMaysua)
                            //    .Set(KcbPhieuDct.Columns.TenMaysua).EqualTo(objPhieuDct.TenMaysua)
                            //    .Where(KcbPhieuDct.Columns.MaLuotkham).IsEqualTo(objPhieuDct.MaLuotkham)
                            //    .And(KcbPhieuDct.Columns.IdBenhnhan).IsEqualTo(objPhieuDct.IdBenhnhan)
                            //    .And(KcbPhieuDct.Columns.LoaiThanhtoan)
                            //    .IsEqualTo(Utility.Int32Dbnull(objPhieuDct.LoaiThanhtoan))
                            //    .Execute();
                        }
                        //}
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi trong qua trinh tra tien lai:{0}", exception.ToString());
                return ActionResult.Error;
            }
        }

        public ActionResult HuyPhieuchi_old(KcbThanhtoan objThanhtoan, KcbLuotkham objLuotkham, string lydohuy)
        {
            try
            {
                decimal PtramBHYT = 0;
                ///tổng tiền hiện tại truyền vào của lần payment đang thực hiện
                decimal v_dblTongtienHuy = 0;
                ///tổng tiền đã thanh toán
                decimal v_TotalPaymentDetail = 0;
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        if (objThanhtoan != null)
                        {
                            if (
                                THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_KHOIPHUCLAIDULIEU_KHIHUYPHIEUCHI",
                                    "0", false) == "1")
                            {
                                KcbThanhtoanCollection lstKcbThanhtoanCollection =
                                    new KcbThanhtoanController()
                                        .FetchByQuery(
                                            KcbThanhtoan.CreateQuery()
                                                .AddWhere(KcbThanhtoan.Columns.MaLuotkham, Comparison.Equals,
                                                    objThanhtoan.MaLuotkham)
                                                .AND(KcbThanhtoan.Columns.IdBenhnhan, Comparison.Equals,
                                                    objThanhtoan.IdBenhnhan)
                                                .AND(KcbThanhtoan.Columns.KieuThanhtoan, Comparison.Equals,
                                                    objThanhtoan.KieuThanhtoan)
                                        );
                                    //Chỉ lấy về các bản ghi thanh toán thường(0= thường;1= thanh toán hủy(trả lại tiền))
                                //Lấy tổng tiền của các lần thanh toán trước
                                var lstKcbThanhtoanChitiet_KhoiphucChitra = new List<KcbThanhtoanChitiet>();

                                foreach (KcbThanhtoan Payment in lstKcbThanhtoanCollection)
                                {
                                    var lstKcbThanhtoanChitietCollection = new Select().From(KcbThanhtoanChitiet.Schema)
                                        .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan).IsEqualTo(Payment.IdThanhtoan)
                                        .ExecuteAsCollection<KcbThanhtoanChitietCollection>();

                                    foreach (
                                        KcbThanhtoanChitiet _KcbThanhtoanChitiet in lstKcbThanhtoanChitietCollection)
                                    {
                                        //Tính các khoản chi tiết đồng chi trả<->Tự túc=0
                                        if (_KcbThanhtoanChitiet.TuTuc == 0)
                                        {
                                            lstKcbThanhtoanChitiet_KhoiphucChitra.Add(_KcbThanhtoanChitiet);
                                            _KcbThanhtoanChitiet.IsNew = false;
                                            _KcbThanhtoanChitiet.MarkOld();
                                            //Tính tiền các khoản có BHYT chi trả
                                            if (!Utility.Byte2Bool(_KcbThanhtoanChitiet.TrangthaiHuy))
                                                v_TotalPaymentDetail +=
                                                    Utility.Int32Dbnull(_KcbThanhtoanChitiet.SoLuong)*
                                                    Utility.DecimaltoDbnull(_KcbThanhtoanChitiet.DonGia);
                                        }
                                    }
                                }

                                var lstKcbThanhtoanChitiet_Phieuchi =
                                    new Select().From(KcbThanhtoanChitiet.Schema)
                                        .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan)
                                        .IsEqualTo(objThanhtoan.IdThanhtoan)
                                        .ExecuteAsCollection<KcbThanhtoanChitietCollection>();

                                List<long> lstIDChitiethuy = (from p in lstKcbThanhtoanChitiet_Phieuchi
                                    select Utility.Int64Dbnull(p.IdThanhtoanhuy, -1)).ToList<long>();
                                List<KcbThanhtoanChitiet> lstKcbThanhtoanChitiet_Huy =
                                    new Select().From(KcbThanhtoanChitiet.Schema)
                                        .Where(KcbThanhtoanChitiet.Columns.IdChitiet)
                                        .In(lstIDChitiethuy)
                                        .ExecuteAsCollection<KcbThanhtoanChitietCollection>()
                                        .ToList<KcbThanhtoanChitiet>();

                                v_dblTongtienHuy = TongtienKhongTutuc(lstKcbThanhtoanChitiet_Huy);
                                LayThongtinPtramBHYT(v_dblTongtienHuy + v_TotalPaymentDetail, objLuotkham, ref PtramBHYT);
                                //Tính lại thông tin BHYT,BN chi trả cho toàn bộ các chi tiết của BN đã thanh toán mà không bị hủy
                                THU_VIEN_CHUNG.TinhPhamTramBHYT(objLuotkham, ref lstKcbThanhtoanChitiet_Huy,
                                    ref lstKcbThanhtoanChitiet_KhoiphucChitra, PtramBHYT);

                                //Tính lại tổng tiền cho tất cả các lần thanh toán cũ
                                List<long> lstIdThanhtoanCu = (from q in lstKcbThanhtoanChitiet_KhoiphucChitra
                                    select Utility.Int64Dbnull(q.IdThanhtoan, -1)).Distinct().ToList();
                                decimal TT_BN = 0m;
                                decimal TT_BHYT = 0m;
                                decimal TT_Chietkhau_Chitiet = 0m;
                                foreach (int IdThanhtoan in lstIdThanhtoanCu)
                                {
                                    TT_BN = 0m;
                                    TT_BHYT = 0m;
                                    TT_Chietkhau_Chitiet = 0m;
                                    List<KcbThanhtoanChitiet> _LstChitiet =
                                        (from q in lstKcbThanhtoanChitiet_KhoiphucChitra
                                            where q.IdThanhtoan == IdThanhtoan
                                            select q).ToList<KcbThanhtoanChitiet>();

                                    if (_LstChitiet.Count > 0)
                                    {
                                        foreach (KcbThanhtoanChitiet objChitietThanhtoan in _LstChitiet)
                                        {
                                            TT_BN += (objChitietThanhtoan.BnhanChitra + objChitietThanhtoan.PhuThu)*
                                                     Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                            if (!Utility.Byte2Bool(objChitietThanhtoan.TuTuc))
                                                TT_BHYT += objChitietThanhtoan.BhytChitra*Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                            TT_Chietkhau_Chitiet +=
                                                Utility.DecimaltoDbnull(objChitietThanhtoan.TienChietkhau, 0);
                                            //Lưu lại các thông tin tiền đã được tính toán lại ở thủ tục THU_VIEN_CHUNG.TinhPhamTramBHYT(...)
                                            objChitietThanhtoan.IsNew = false;
                                            objChitietThanhtoan.MarkOld();
                                            objChitietThanhtoan.Save();
                                        }
                                        //Update lại tiền thanh toán
                                        new Update(KcbThanhtoan.Schema)
                                            .Set(KcbThanhtoan.Columns.TongTien).EqualTo(TT_BHYT + TT_BN)
                                            .Set(KcbThanhtoan.Columns.BnhanChitra).EqualTo(TT_BN)
                                            .Set(KcbThanhtoan.Columns.BhytChitra).EqualTo(TT_BHYT)
                                            .Set(KcbThanhtoan.Columns.MaDoituongKcb).EqualTo(objLuotkham.MaDoituongKcb)
                                            .Set(KcbThanhtoan.Columns.IdDoituongKcb).EqualTo(objLuotkham.IdDoituongKcb)
                                            .Set(KcbThanhtoan.Columns.PtramBhyt).EqualTo(objLuotkham.PtramBhyt)
                                            .Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(IdThanhtoan).Execute();
                                        //Update phiếu thu
                                        new Update(KcbPhieuthu.Schema)
                                            .Set(KcbPhieuthu.Columns.SoTien).EqualTo(TT_BN - TT_Chietkhau_Chitiet)
                                            .Set(KcbPhieuthu.Columns.SotienGoc).EqualTo(TT_BN)
                                            .Where(KcbPhieuthu.Columns.IdThanhtoan).IsEqualTo(IdThanhtoan).Execute();
                                    }
                                }
                                //Khôi phục lại trạng thái hủy
                                foreach (KcbThanhtoanChitiet objKcbThanhtoanChitiet in lstKcbThanhtoanChitiet_Huy)
                                {
                                    objKcbThanhtoanChitiet.IsNew = false;
                                    objKcbThanhtoanChitiet.MarkOld();
                                    objKcbThanhtoanChitiet.TrangthaiHuy = 0;
                                    objKcbThanhtoanChitiet.IdThanhtoanhuy = -1;
                                    objKcbThanhtoanChitiet.Save();

                                    ///thanh toán khám chữa bệnh))
                                    if (objKcbThanhtoanChitiet.IdLoaithanhtoan == 1)
                                    {
                                        new Update(KcbDangkyKcb.Schema)
                                            .Set(KcbDangkyKcb.Columns.TrangthaiHuy).EqualTo(0)
                                            .Where(KcbDangkyKcb.Columns.IdKham)
                                            .IsEqualTo(objKcbThanhtoanChitiet.IdPhieu)
                                            .Execute();
                                    }
                                    ///thah toán phần dịch vụ cận lâm sàng
                                    if (objKcbThanhtoanChitiet.IdLoaithanhtoan == 2)
                                    {
                                        KcbChidinhclsChitiet objKcbChidinhclsChitiet =
                                            KcbChidinhclsChitiet.FetchByID(objKcbThanhtoanChitiet.IdPhieuChitiet);
                                        if (globalVariables.UserName != "ADMIN")
                                        {
                                            if (objKcbChidinhclsChitiet != null &&
                                                objKcbChidinhclsChitiet.TrangThai >= 3) //Đã có kết quả
                                            {
                                                return ActionResult.AssignIsConfirmed;
                                            }
                                        }
                                        new Update(KcbChidinhclsChitiet.Schema)
                                            .Set(KcbChidinhclsChitiet.Columns.TrangthaiHuy).EqualTo(0)
                                            .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh)
                                            .IsEqualTo(objKcbThanhtoanChitiet.IdPhieuChitiet)
                                            .Execute();
                                    }
                                    ///thanh toán phần thuốc
                                    if (objKcbThanhtoanChitiet.IdLoaithanhtoan == 3)
                                    {
                                        KcbDonthuocChitiet objKcbDonthuocChitiet =
                                            KcbDonthuocChitiet.FetchByID(objKcbThanhtoanChitiet.IdPhieuChitiet);

                                        if (globalVariables.UserName != "ADMIN")
                                        {
                                            if (objKcbDonthuocChitiet != null &&
                                                Utility.Byte2Bool(objKcbDonthuocChitiet.TrangThai))
                                            {
                                                return ActionResult.PresIsConfirmed;
                                            }
                                        }
                                        new Update(KcbDonthuocChitiet.Schema)
                                            .Set(KcbDonthuocChitiet.Columns.TrangthaiHuy).EqualTo(0)
                                            .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc)
                                            .IsEqualTo(objKcbThanhtoanChitiet.IdPhieuChitiet)
                                            .Execute();
                                    }
                                }
                            }
                            //Ghi lại log hủy
                            var objKcbLoghuy = new KcbLoghuy();
                            objKcbLoghuy.IdBenhnhan = objThanhtoan.IdBenhnhan;
                            objKcbLoghuy.MaLuotkham = objThanhtoan.MaLuotkham;
                            objKcbLoghuy.IdNhanvien = globalVariables.gv_intIDNhanvien;
                            objKcbLoghuy.SotienHuy = objThanhtoan.TongTien;
                            objKcbLoghuy.LydoHuy = lydohuy;
                            objKcbLoghuy.NgayHuy = DateTime.Now;
                            objKcbLoghuy.NgayTao = DateTime.Now;
                            objKcbLoghuy.NguoiTao = globalVariables.UserName;
                            objKcbLoghuy.IsNew = true;
                            objKcbLoghuy.LoaiphieuHuy = Utility.ByteDbnull(objThanhtoan.KieuThanhtoan, 0);
                            objKcbLoghuy.Save();
                            //Xóa các thông tin phiếu chi
                            new Delete().From(KcbThanhtoan.Schema)
                                .Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();
                            new Delete().From(KcbThanhtoanChitiet.Schema)
                                .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan)
                                .IsEqualTo(objThanhtoan.IdThanhtoan)
                                .Execute();
                            new Delete().From(KcbPhieuthu.Schema)
                                .Where(KcbPhieuthu.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan)
                                .And(KcbPhieuthu.Columns.LoaiPhieuthu).IsEqualTo(1).Execute();
                        }
                        else
                        {
                            return ActionResult.Error;
                        }
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Ban ra loi Exception={0}", exception);
                return ActionResult.Error;
            }
        }

        public ActionResult UpdateNgayThanhtoan(KcbThanhtoan objThanhtoan)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        
                    
                        StoredProcedure sp = SPs.SpUpdateNgaythanhtoan(objThanhtoan.IdThanhtoan,
                            objThanhtoan.NgayThanhtoan);
                        sp.Execute();
                        #region update ngày thanh toán old
                        new Update(KcbThanhtoan.Schema)
                            .Set(KcbThanhtoan.Columns.NgayThanhtoan).EqualTo(objThanhtoan.NgayThanhtoan)
                            .Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();
                        new Update(KcbDangkyKcb.Schema)
                            .Set(KcbDangkyKcb.Columns.NgayThanhtoan).EqualTo(objThanhtoan.NgayThanhtoan)
                            .Where(KcbDangkyKcb.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();
                        new Update(KcbChidinhclsChitiet.Schema)
                            .Set(KcbChidinhclsChitiet.Columns.NgayThanhtoan).EqualTo(objThanhtoan.NgayThanhtoan)
                            .Where(KcbChidinhclsChitiet.Columns.IdThanhtoan)
                            .IsEqualTo(objThanhtoan.IdThanhtoan)
                            .Execute();
                        new Update(KcbDonthuocChitiet.Schema)
                            .Set(KcbDonthuocChitiet.Columns.NgayThanhtoan).EqualTo(objThanhtoan.NgayThanhtoan)
                            .Where(KcbDonthuocChitiet.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();
                        //new Update(TPatientDept.Schema)
                        //    .Set(TPatientDept.Columns.NgayThanhtoan).EqualTo(objThanhtoan.NgayThanhtoan)
                        //    .Where(TPatientDept.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();
                        new Update(TTongChiphi.Schema)
                            .Set(TTongChiphi.Columns.PaymentDate).EqualTo(objThanhtoan.NgayThanhtoan)
                            .Where(TTongChiphi.Columns.PaymentId).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();
                        //new Update(TDeposit.Schema)
                        //  .Set(TDeposit.Columns.NgayThanhtoan).EqualTo(objThanhtoan.NgayThanhtoan)
                        //  .Where(TDeposit.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();
#endregion 
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                // log.Error("Loi trong qua trinh huy thong tin {0}",exception.ToString());
                return ActionResult.Error;
            }
        }

        private int UpdatePaymentStatus(KcbThanhtoan objThanhtoan, KcbThanhtoanChitiet objChitietThanhtoan)
        {
            int reval = -1;
            using (var scope = new TransactionScope())
            {
                StoredProcedure sp = SPs.SpUpdateTrangthaithanhtoan(objChitietThanhtoan.IdLoaithanhtoan,
                    objThanhtoan.IdThanhtoan, objThanhtoan.NgayThanhtoan, objThanhtoan.KieuThanhtoan,
                    objChitietThanhtoan.KieuChietkhau, objChitietThanhtoan.TileChietkhau,
                    objChitietThanhtoan.TienChietkhau, objChitietThanhtoan.IdPhieu, objChitietThanhtoan.IdPhieuChitiet,
                    objChitietThanhtoan.NgayTao, objChitietThanhtoan.NguoiTao);
                reval = sp.Execute();
                #region updatetrangthaithanhtoan_old
        //        switch (objChitietThanhtoan.IdLoaithanhtoan)
        //        {
        //            case 1: //Phí KCB
        //                reval = new Update(KcbDangkyKcb.Schema)
        //                    .Set(KcbDangkyKcb.Columns.IdThanhtoan).EqualTo(objThanhtoan.IdThanhtoan)
        //                    .Set(KcbDangkyKcb.Columns.TrangthaiThanhtoan).EqualTo(1)
        //                    .Set(KcbDangkyKcb.Columns.NgaySua).EqualTo(globalVariables.SysDate)
        //                    .Set(KcbDangkyKcb.Columns.NguoiSua).EqualTo(globalVariables.UserName)
        //                    .Set(KcbDangkyKcb.Columns.NgayThanhtoan).EqualTo(objThanhtoan.NgayThanhtoan)
        //                    .Set(KcbDangkyKcb.Columns.NguonThanhtoan).EqualTo(objThanhtoan.KieuThanhtoan)
        //                    .Set(KcbChidinhclsChitiet.Columns.TienChietkhau).EqualTo(objChitietThanhtoan.TienChietkhau)
        //                    .Set(KcbChidinhclsChitiet.Columns.TileChietkhau).EqualTo(objChitietThanhtoan.TileChietkhau)
        //                    .Set(KcbChidinhclsChitiet.Columns.KieuChietkhau).EqualTo(objChitietThanhtoan.KieuChietkhau)
        //                    .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(objChitietThanhtoan.IdPhieu).Execute();

        //                //new Update(NoitruPhanbuonggiuong.Schema)
        //                //    .Set(NoitruPhanbuonggiuong.Columns.IdThanhtoan).EqualTo(objThanhtoan.IdThanhtoan)
        //                //    .Set(NoitruPhanbuonggiuong.Columns.TrangthaiThanhtoan).EqualTo(1)
        //                //     .Set(NoitruPhanbuonggiuong.Columns.NgaySua).EqualTo(globalVariables.SysDate)
        //                //    .Set(NoitruPhanbuonggiuong.Columns.NguoiSua).EqualTo(globalVariables.UserName)
        //                //    .Set(NoitruPhanbuonggiuong.Columns.NgayThanhtoan).EqualTo(objThanhtoan.NgayThanhtoan)
        //                //     .Set(NoitruPhanbuonggiuong.Columns.NguonThanhtoan).EqualTo(objThanhtoan.KieuThanhtoan)
        //                //    .Where(NoitruPhanbuonggiuong.Columns.IdKham).IsEqualTo(objChitietThanhtoan.IdPhieu)
        //                //    .And(NoitruPhanbuonggiuong.Columns.NoiTru).IsEqualTo(0).Execute();
        //                break;
        //            case 10: //Sổ khám
        //                reval = new Update(KcbDangkySokham.Schema)
        //                    .Set(KcbDangkySokham.Columns.IdThanhtoan).EqualTo(objThanhtoan.IdThanhtoan)
        //                    .Set(KcbDangkySokham.Columns.TrangthaiThanhtoan).EqualTo(1)
        //                    .Set(KcbDangkySokham.Columns.NgaySua).EqualTo(globalVariables.SysDate)
        //                    .Set(KcbDangkySokham.Columns.NguoiSua).EqualTo(globalVariables.UserName)
        //                    .Set(KcbDangkySokham.Columns.NgayThanhtoan).EqualTo(objThanhtoan.NgayThanhtoan)
        //                    .Set(KcbDangkySokham.Columns.NguonThanhtoan).EqualTo(objThanhtoan.KieuThanhtoan)
        //                    .Where(KcbDangkySokham.Columns.IdSokcb).IsEqualTo(objChitietThanhtoan.IdPhieu).Execute();
        //                break;
        //            case 8: //Gói dịch vụ
        //            case 11: //Công tiêm chủng
        //            case 9: //Chi phí thêm
        //            case 2: //Phí CLS
        //            case 12: //Dịch vụ kiểm nghiệm
        //                reval = new Update(KcbChidinhclsChitiet.Schema)
        //                    .Set(KcbChidinhclsChitiet.Columns.IdThanhtoan).EqualTo(objThanhtoan.IdThanhtoan)
        //                    .Set(KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).EqualTo(1)
        //                    .Set(KcbChidinhclsChitiet.Columns.TienChietkhau).EqualTo(objChitietThanhtoan.TienChietkhau)
        //                    .Set(KcbChidinhclsChitiet.Columns.TileChietkhau).EqualTo(objChitietThanhtoan.TileChietkhau)
        //                    .Set(KcbChidinhclsChitiet.Columns.KieuChietkhau).EqualTo(objChitietThanhtoan.KieuChietkhau)
        //                    .Set(KcbChidinhclsChitiet.Columns.NguonThanhtoan).EqualTo(objThanhtoan.KieuThanhtoan)
        //                    .Set(KcbChidinhclsChitiet.Columns.NgaySua).EqualTo(globalVariables.SysDate)
        //                    .Set(KcbChidinhclsChitiet.Columns.NguoiSua).EqualTo(globalVariables.UserName)
        //                    .Set(KcbChidinhclsChitiet.Columns.NgayThanhtoan).EqualTo(objThanhtoan.NgayThanhtoan)
        //                    .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh)
        //                    .IsEqualTo(objChitietThanhtoan.IdPhieuChitiet)
        //                    .Execute();
        //                //09/09/2015-->Tạm thời bỏ do ko có ý nghĩa
        //                //new Update(KcbChidinhcl.Schema)
        //                //.Set(KcbChidinhcl.Columns.TrangthaiThanhtoan).EqualTo(1)
        //                //.Set(KcbChidinhcl.Columns.NgayThanhtoan).EqualTo(objThanhtoan.NgayThanhtoan)
        //                //.Where(KcbChidinhcl.Columns.IdChidinh).IsEqualTo(objChitietThanhtoan.IdPhieu).Execute();
        //                break;
        //            case 3: //Đơn thuốc ngoại trú,nội trú
        //            case 5: //Vật tư tiêu hao
        //                reval = new Update(KcbDonthuocChitiet.Schema)
        //                    .Set(KcbDonthuocChitiet.Columns.IdThanhtoan).EqualTo(objThanhtoan.IdThanhtoan)
        //                    .Set(KcbDonthuocChitiet.Columns.TrangthaiThanhtoan).EqualTo(1)
        //                    .Set(KcbDonthuocChitiet.Columns.NgayThanhtoan).EqualTo(objThanhtoan.NgayThanhtoan)
        //                    .Set(KcbDonthuocChitiet.Columns.TienChietkhau).EqualTo(objChitietThanhtoan.TienChietkhau)
        //                    .Set(KcbDonthuocChitiet.Columns.TileChietkhau).EqualTo(objChitietThanhtoan.TileChietkhau)
        //                    .Set(KcbDonthuocChitiet.Columns.KieuChietkhau).EqualTo(objChitietThanhtoan.KieuChietkhau)
        //                    .Set(KcbDonthuocChitiet.Columns.NguonThanhtoan).EqualTo(objThanhtoan.KieuThanhtoan)
        //                    .Set(KcbDonthuocChitiet.Columns.NgaySua).EqualTo(globalVariables.SysDate)
        //                    .Set(KcbDonthuocChitiet.Columns.NguoiSua).EqualTo(globalVariables.UserName)
        //                    .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc)
        //                    .IsEqualTo(objChitietThanhtoan.IdPhieuChitiet)
        //                    .Execute();
        //                //09/09/2015-->Tạm thời bỏ do ko có ý nghĩa
        //                //new Update(KcbDonthuoc.Schema)
        //                //   .Set(KcbDonthuoc.Columns.TrangthaiThanhtoan).EqualTo(1)
        //                //   .Set(KcbDonthuoc.Columns.NgayThanhtoan).EqualTo(objThanhtoan.NgayThanhtoan)
        //                //   .Where(KcbDonthuoc.Columns.IdDonthuoc).IsEqualTo(objChitietThanhtoan.IdPhieu).Execute();
        //                break;

        //            case 4: //Giường bệnh
        //                reval = new Update(NoitruPhanbuonggiuong.Schema)
        //                    .Set(NoitruPhanbuonggiuong.Columns.IdThanhtoan).EqualTo(objThanhtoan.IdThanhtoan)
        //                    .Set(NoitruPhanbuonggiuong.Columns.TrangthaiThanhtoan).EqualTo(1)
        //                    .Set(NoitruPhanbuonggiuong.Columns.NgayThanhtoan).EqualTo(objThanhtoan.NgayThanhtoan)
        //                    .Set(NoitruPhanbuonggiuong.Columns.NgaySua).EqualTo(globalVariables.SysDate)
        //                    .Set(NoitruPhanbuonggiuong.Columns.NguoiSua).EqualTo(globalVariables.UserName)
        //                    .Set(NoitruPhanbuonggiuong.Columns.NguonThanhtoan).EqualTo(objThanhtoan.KieuThanhtoan)
        //                    .Where(NoitruPhanbuonggiuong.Columns.Id).IsEqualTo(objChitietThanhtoan.IdPhieu).Execute();
        //                break;
        //            case 0: //Phí dịch vụ kèm theo
        //                reval = new Update(KcbDangkyKcb.Schema)
        //                    .Set(KcbDangkyKcb.Columns.IdThanhtoan).EqualTo(objThanhtoan.IdThanhtoan)
        //                    .Set(KcbDangkyKcb.Columns.TrangthaiThanhtoan).EqualTo(1)
        //                    .Set(KcbDangkyKcb.Columns.NgayThanhtoan).EqualTo(objThanhtoan.NgayThanhtoan)
        //                    .Set(KcbDangkyKcb.Columns.TienChietkhau).EqualTo(objChitietThanhtoan.TienChietkhau)
        //                    .Set(KcbDangkyKcb.Columns.TileChietkhau).EqualTo(objChitietThanhtoan.TileChietkhau)
        //                    .Set(KcbDangkyKcb.Columns.KieuChietkhau).EqualTo(objChitietThanhtoan.KieuChietkhau)
        //                    .Set(KcbDangkyKcb.Columns.NguonThanhtoan).EqualTo(objThanhtoan.KieuThanhtoan)
        //                    .Set(KcbDangkyKcb.Columns.NgaySua).EqualTo(globalVariables.SysDate)
        //                    .Set(KcbDangkyKcb.Columns.NguoiSua).EqualTo(globalVariables.UserName)
        //                    .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(objChitietThanhtoan.IdPhieu)
        //                    .And(KcbDangkyKcb.Columns.LaPhidichvukemtheo).IsEqualTo(1)
        //                    .Execute();
        //                break;
                // }
#endregion 
                scope.Complete();
            }
            return reval;
        }

        public ActionResult UpdateICD10(KcbLuotkham objLuotkham, string ICDCode, string ICDTen)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        new Update(KcbLuotkham.Schema)
                            .Set(KcbLuotkham.Columns.MabenhChinh).EqualTo(ICDCode)
                            .Set(KcbLuotkham.Columns.NguoiKetthuc).EqualTo(globalVariables.UserName)
                            .Set(KcbLuotkham.Columns.NgayKetthuc).EqualTo(globalVariables.SysDate)
                            .Set(KcbLuotkham.Columns.Locked).EqualTo(1)
                            .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                            .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).Execute();
                        var objChuandoanKetluan =
                            new Select().From(KcbChandoanKetluan.Schema).Where(KcbChandoanKetluan.Columns.MaLuotkham).
                                IsEqualTo(objLuotkham.MaLuotkham).And(KcbChandoanKetluan.Columns.IdBenhnhan).IsEqualTo(
                                    objLuotkham.IdBenhnhan).ExecuteSingle<KcbChandoanKetluan>();
                        if (objChuandoanKetluan != null)
                        {
                            new Update(KcbChandoanKetluan.Schema)
                                .Set(KcbChandoanKetluan.Columns.MabenhChinh).EqualTo(ICDCode)
                                .Set(KcbChandoanKetluan.Columns.MotaBenhchinh).EqualTo(ICDTen)
                                .Where(KcbChandoanKetluan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                                .And(KcbChandoanKetluan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                                .And(KcbChandoanKetluan.Columns.Noitru).IsEqualTo(0)
                                .Execute();
                        }
                        else
                        {
                            objChuandoanKetluan = new KcbChandoanKetluan();
                            objChuandoanKetluan.IdBenhnhan = Utility.Int64Dbnull(objLuotkham.IdBenhnhan);
                            objChuandoanKetluan.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham, "");
                            objChuandoanKetluan.SongayDieutri = 1;
                            objChuandoanKetluan.MabenhChinh = ICDCode;
                            objChuandoanKetluan.MotaBenhchinh = ICDTen;
                            objChuandoanKetluan.NgayChandoan = globalVariables.SysDate;
                            objChuandoanKetluan.NgayTao = globalVariables.SysDate;
                            objChuandoanKetluan.NguoiTao = globalVariables.UserName;
                            objChuandoanKetluan.IdBacsikham = globalVariables.gv_intIDNhanvien;
                            objChuandoanKetluan.IpMaytao = globalVariables.gv_strIPAddress;
                            objChuandoanKetluan.Noitru = 0;
                            objChuandoanKetluan.IsNew = true;
                            objChuandoanKetluan.Save();
                        }
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.InfoException("Ban ra loi exception=", exception);
                return ActionResult.Error;
            }
        }

        public ActionResult Capnhattrangthaithanhtoan(long idThanhtoan)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        new Update(KcbThanhtoan.Schema)
                            .Set(KcbThanhtoan.Columns.NguoiIn).EqualTo(globalVariables.UserName)
                            .Set(KcbThanhtoan.Columns.NgayIn).EqualTo(globalVariables.SysDate)
                            .Set(KcbThanhtoan.Columns.TrangthaiIn).EqualTo(1)
                            .Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(idThanhtoan).Execute();
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.InfoException("Ban ra loi exception=", exception);
                return ActionResult.Error;
            }
        }

        public ActionResult UpdateDataPhieuThu(KcbPhieuthu objPhieuthu)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        StoredProcedure sp = SPs.KcbThanhtoanThemmoiPhieuthu(objPhieuthu.MaPhieuthu,
                            objPhieuthu.IdThanhtoan,
                            objPhieuthu.NgayThuchien,
                            objPhieuthu.NguoiNop, objPhieuthu.LydoNop,
                            objPhieuthu.SoTien, objPhieuthu.SotienGoc, objPhieuthu.TienChietkhau,
                            objPhieuthu.TienChietkhauchitiet, objPhieuthu.TienChietkhauhoadon,
                            objPhieuthu.SoluongChungtugoc, objPhieuthu.TaikhoanNo,
                            objPhieuthu.TaikhoanCo,
                            objPhieuthu.LoaiPhieuthu, globalVariables.UserName,
                            globalVariables.SysDate,
                            globalVariables.gv_intIDNhanvien,
                            globalVariables.idKhoatheoMay,
                            globalVariables.UserName, globalVariables.SysDate);
                        sp.Execute();

                        new Update(KcbThanhtoan.Schema)
                            .Set(KcbThanhtoan.Columns.NguoiIn).EqualTo(globalVariables.UserName)
                            .Set(KcbThanhtoan.Columns.NgayIn).EqualTo(globalVariables.SysDate)
                            .Set(KcbThanhtoan.Columns.TrangthaiIn).EqualTo(1)
                            .Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(objPhieuthu.IdThanhtoan).Execute();
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.InfoException("Ban ra loi exception=", exception);
                return ActionResult.Error;
            }
        }

        public ActionResult UpdateDataPhieuThu(KcbPhieuthu objPhieuthu, KcbThanhtoanChitiet[] arrPaymentDetail)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        StoredProcedure sp = SPs.KcbThanhtoanThemmoiPhieuthu(objPhieuthu.MaPhieuthu,
                            objPhieuthu.IdThanhtoan,
                            objPhieuthu.NgayThuchien,
                            objPhieuthu.NguoiNop, objPhieuthu.LydoNop,
                            objPhieuthu.SoTien, objPhieuthu.SotienGoc, objPhieuthu.TienChietkhau,
                            objPhieuthu.TienChietkhauchitiet, objPhieuthu.TienChietkhauhoadon,
                            objPhieuthu.SoluongChungtugoc, objPhieuthu.TaikhoanNo,
                            objPhieuthu.TaikhoanCo,
                            objPhieuthu.LoaiPhieuthu, globalVariables.UserName,
                            globalVariables.SysDate,
                            globalVariables.gv_intIDNhanvien,
                            globalVariables.idKhoatheoMay,
                            globalVariables.UserName, globalVariables.SysDate);
                        sp.Execute();
                        foreach (KcbThanhtoanChitiet objChitietThanhtoan in arrPaymentDetail)
                        {
                            new Update(KcbThanhtoanChitiet.Schema)
                                .Set(KcbThanhtoanChitiet.Columns.SttIn).EqualTo(objChitietThanhtoan.SttIn)
                                // .Set(KcbThanhtoanChitiet.Columns.PhuThu).EqualTo(objChitietThanhtoan.PhuThu)
                                .Where(KcbThanhtoanChitiet.Columns.IdChitiet).IsEqualTo(
                                    objChitietThanhtoan.IdChitiet).Execute();
                            log.Info("Cạp nhạp lại thong tin cua voi ma ID=" + objChitietThanhtoan.IdChitiet);
                        }
                        new Update(KcbThanhtoan.Schema)
                            .Set(KcbThanhtoan.Columns.NguoiIn).EqualTo(globalVariables.UserName)
                            .Set(KcbThanhtoan.Columns.NgayIn).EqualTo(globalVariables.SysDate)
                            .Set(KcbThanhtoan.Columns.TrangthaiIn).EqualTo(1)
                            .Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(objPhieuthu.IdThanhtoan).Execute();
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Ban ra loi Exception={0}", exception);
                return ActionResult.Error;
            }
        }

        public DataTable GetDataInphieuDichvu(KcbThanhtoan objThanhtoan)
        {
            return
                SPs.KcbThanhtoanLaythongtinInphieuDichvu(Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1),
                    objThanhtoan.MaLuotkham,
                    Utility.Int32Dbnull(objThanhtoan.IdBenhnhan)).GetDataSet().Tables[0];
        }

        public DataTable GetDataInphieuBh(KcbThanhtoan objThanhtoan, bool IsBH)
        {
            DataTable dataTable =
                SPs.BhytLaythongtinInphieubhyt(Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1),
                    objThanhtoan.MaLuotkham,
                    Utility.Int32Dbnull(objThanhtoan.IdBenhnhan)).GetDataSet().Tables[0];
            if (IsBH)
            {
                foreach (DataRow drv in dataTable.Rows)
                {
                    if (drv["TuTuc"].ToString() == "1") drv.Delete();
                }
                dataTable.AcceptChanges();
            }
            return dataTable;
        }

        public DataTable INPHIEUBH_CHOBENHNHAN(KcbThanhtoan objThanhtoan)
        {
            //DataTable dataTable =
            //    SPs.BhytLaythongtinInphieubhytChobenhnhan(Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1), objThanhtoan.MaLuotkham,
            //                          Utility.Int32Dbnull(objThanhtoan.IdBenhnhan)).GetDataSet().Tables[0];

            //return dataTable;
            return null;
        }

        public DataTable KYDONG_GetDataInphieuBH(KcbThanhtoan objThanhtoan, bool TuTuc)
        {
            return null;
            //DataTable dataTable =
            //    SPs.BhytLaythongtinInphieuTraituyen(Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1), objThanhtoan.MaLuotkham,
            //                          Utility.Int32Dbnull(objThanhtoan.IdBenhnhan)).GetDataSet().Tables[0];
            //if (!TuTuc)
            //{
            //    foreach (DataRow drv in dataTable.Rows)
            //    {
            //        if (drv["TuTuc"].ToString() == "1") drv.Delete();
            //    }
            //    dataTable.AcceptChanges();
            //}
            //return dataTable;
        }

        public DataTable KYDONG_GetDataInphieuBH_TraiTuyen(KcbThanhtoan objThanhtoan)
        {
            return null;
            //return
            //    SPs.BhytLaythongtinInphieuTraituyen(Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1), objThanhtoan.MaLuotkham,
            //                          Utility.Int32Dbnull(objThanhtoan.IdBenhnhan)).GetDataSet().Tables[0];
        }

        public void XuLyThongTinPhieu_DichVu(ref DataTable m_dtReportPhieuThu)
        {
            Utility.AddColumToDataTable(ref m_dtReportPhieuThu, "TONG_BN", typeof (decimal));
            Utility.AddColumToDataTable(ref m_dtReportPhieuThu, "PHU_THU", typeof (decimal));
            foreach (DataRow drv in m_dtReportPhieuThu.Rows)
            {
                //drv["ThanhTien"] = Utility.Int32Dbnull(drv["SoLuong"], 0) *
                //                   Utility.DecimaltoDbnull(drv["Discount_Price"], 0);
                drv["TotalSurcharge_Price"] = Utility.Int32Dbnull(drv["SoLuong"], 0)*
                                              Utility.DecimaltoDbnull(drv[KcbThanhtoanChitiet.Columns.PhuThu], 0);
            }
            m_dtReportPhieuThu.AcceptChanges();
            foreach (DataRow drv in m_dtReportPhieuThu.Rows)
            {
                drv["TONG_BN"] = Utility.Int32Dbnull(drv["SoLuong"], 0)*
                                 Utility.DecimaltoDbnull(drv["Discount_Price"], 0);
                drv["PHU_THU"] = Utility.Int32Dbnull(drv["SoLuong"], 0)*
                                 Utility.DecimaltoDbnull(drv[KcbThanhtoanChitiet.Columns.PhuThu], 0);
            }
            m_dtReportPhieuThu.AcceptChanges();
        }

        public DataTable KydongInphieuBaohiemChoBenhnhan(KcbThanhtoan objThanhtoan)
        {
            return null;
            //return SPs.BhytLaythongtinInphieubhKd(Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1),
            //                                        Utility.sDbnull(objThanhtoan.MaLuotkham),
            //                                        Utility.Int32Dbnull(objThanhtoan.IdBenhnhan, -1)).GetDataSet().Tables[0];
        }

        public DataTable KydongInPhieubaohiemTraituyen(KcbThanhtoan objThanhtoan)
        {
            return null;
            //return SPs.BhytLaythongtinInphieuTraituyen(Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1), Utility.sDbnull(objThanhtoan.MaLuotkham, ""),
            //                                      Utility.Int32Dbnull(objThanhtoan.IdBenhnhan, -1)).GetDataSet().Tables[0];
        }

        public DataTable DetmayPrintAllExtendExamPaymentDetail(KcbThanhtoan objThanhtoan)
        {
            return null;
            //return SPs.BhytLAYTHONGTInInphoibhytDm(Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1),
            //                                              objThanhtoan.MaLuotkham,
            //                                              Utility.Int32Dbnull(objThanhtoan.IdBenhnhan, -1)).GetDataSet()
            //        .Tables[0];
        }

        public DataTable DetmayInphieuBhPhuthu(KcbThanhtoan objThanhtoan)
        {
            return null;
            //return SPs.BhytLAYTHONGTInInphoibhytPhuhuDm(Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1),
            //                                              objThanhtoan.MaLuotkham,
            //                                              Utility.Int32Dbnull(objThanhtoan.IdBenhnhan, -1)).GetDataSet()
            //        .Tables[0];
        }

        public DataTable LaokhoaInbienlaiBhyt(KcbThanhtoan objThanhtoan)
        {
            return null;
            //return SPs.BhytInbienlai(Utility.Int32Dbnull(objThanhtoan.IdThanhtoan), Utility.sDbnull(objThanhtoan.MaLuotkham), Utility.Int32Dbnull(objThanhtoan.IdBenhnhan)).GetDataSet().Tables[0];
        }

        public DataTable LaokhoaInphieuBaohiemNgoaitru(KcbThanhtoan objThanhtoan)
        {
            return null;
            //return SPs.BhytLaythongtinInphoi(Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1), Utility.sDbnull(objThanhtoan.MaLuotkham, ""),
            //                             Utility.Int32Dbnull(objThanhtoan.IdBenhnhan, -1), 0).GetDataSet().Tables[0];
        }

        public ActionResult UPDATE_SOBIENLAI(HoadonLog lHoadonLog)
        {
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        int record = -1;
                        record = new Update(HoadonLog.Schema).Set(HoadonLog.Columns.MauHoadon)
                            .EqualTo(lHoadonLog.MauHoadon).Set(HoadonLog.Columns.KiHieu).EqualTo(lHoadonLog.KiHieu)
                            .Set(HoadonLog.Columns.MaQuyen).EqualTo(lHoadonLog.MaQuyen)
                            .Set(HoadonLog.Columns.Serie).EqualTo(lHoadonLog.Serie)
                            .Where(HoadonLog.Columns.IdHdonLog).IsEqualTo(lHoadonLog.IdHdonLog)
                            .Execute();
                        if (record <= 0)
                        {
                            return ActionResult.Error;
                        }
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception)
            {
                return ActionResult.Error;
            }
        }

        public ActionResult CHUYEN_DOITUONG(KcbLuotkham objLuotkham, string DOITUONG)
        {
            //try
            //{
            //    using (var Scope = new TransactionScope())
            //    {
            //        using (var dbScope = new SharedDbConnectionScope())
            //        {
            //            KcbDangkyKcbCollection TexamCollection =
            //                new Select().From(KcbDangkyKcb.Schema).Where(KcbDangkyKcb.Columns.MaLuotkham).IsEqualTo(
            //                    objLuotkham.MaLuotkham).And(KcbDangkyKcb.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
            //                    .ExecuteAsCollection<KcbDangkyKcbCollection>();
            //            if (TexamCollection.Count > 0)
            //            {
            //                //CHUYỂN GIÁ KHÁM BỆNH VÀO PHÒNG
            //                foreach (KcbDangkyKcb regExam in TexamCollection)
            //                {
            //                    if (Utility.Int32Dbnull(regExam.TrangthaiThanhtoan) == 1)
            //                    {
            //                        return ActionResult.ExistedRecord;
            //                    }
            //                    DmucDichvukcb KieuKhamCu = DmucDichvukcb.FetchByID(regExam.IdDichvuKcb);
            //                    DmucDichvukcb KieuKhamMoi =
            //                        new Select().From(DmucDichvukcb.Schema)
            //                        .Where(DmucDichvukcb.Columns.IdKieukham).IsEqualTo(KieuKhamCu.IdKieukham)
            //                        .And(DmucDichvukcb.Columns.IdKhoaphong).IsEqualTo(KieuKhamCu.IdKhoaphong)
            //                        .And(DmucDichvukcb.Columns.IdPhongkham).IsEqualTo(KieuKhamCu.IdPhongkham)
            //                        .And(DmucDichvukcb.Columns.MaDoituongKcb).IsEqualTo(DOITUONG)
            //                        .ExecuteSingle<DmucDichvukcb>();
            //                    regExam.IdDichvuKcb = Utility.Int16Dbnull(KieuKhamMoi.IdDichvukcb, -1);
            //                    if (objLuotkham.MaKhoaThuchien == "KYC")
            //                    {
            //                        regExam.DonGia = KieuKhamMoi.DonGia;
            //                        regExam.PhuThu = KieuKhamMoi.PhuthuDungtuyen;
            //                        regExam.Save();
            //                    }
            //                    else if (objLuotkham.MaKhoaThuchien == "KKB")
            //                    {
            //                        regExam.DonGia = KieuKhamMoi.DonGia;
            //                        if (Utility.sDbnull(objLuotkham.MaDoituongKcb, "DV") == "BHYT" && Utility.Int32Dbnull(objLuotkham.DungTuyen, 0) == 0)
            //                        {
            //                            regExam.PhuThu = KieuKhamMoi.PhuthuDungtuyen;
            //                        }
            //                        else
            //                        {
            //                            regExam.PhuThu = 0;
            //                        }
            //                        regExam.Save();

            //                        //THÊM CHI PHÍ DỊCH VỤ KÈM THEO KHÁM BỆNH
            //                        SqlQuery sql = new Select().From(KcbChidinhcl.Schema).Where(
            //                            KcbChidinhcl.Columns.MaLuotkham).
            //                            IsEqualTo(objLuotkham.MaLuotkham)
            //                            .And(KcbChidinhcl.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan);
            //                            //.And(KcbChidinhcl.Columns.IsPHIDvuKtheo).IsEqualTo(1);
            //                        int IdDV = -1;
            //                        string[] Ma_UuTien = globalVariables.gv_strMaUutien.Split(',');
            //                        if (!Ma_UuTien.Contains(Utility.sDbnull(objLuotkham.MaQuyenloi)))
            //                        {
            //                            if (Utility.Int32Dbnull(regExam.KhamNgoaigio, 0) == 1)
            //                            {
            //                                IdDV = Utility.Int32Dbnull(KieuKhamMoi.IdPhikemtheongoaigio, -1);
            //                            }
            //                            else
            //                            {
            //                                IdDV = Utility.Int32Dbnull(KieuKhamMoi.IdPhikemtheo, -1);
            //                            }
            //                        }
            //                        else
            //                        {
            //                            IdDV = -1;
            //                            KcbChidinhclCollection taCollection =
            //                                sql.ExecuteAsCollection<KcbChidinhclCollection>();
            //                            foreach (KcbChidinhcl assignInfo in taCollection)
            //                            {
            //                                KcbChidinhclsChitiet.Delete(KcbChidinhclsChitiet.Columns.IdChidinh, assignInfo.IdChidinh);
            //                                KcbChidinhcl.Delete(assignInfo.IdChidinh);
            //                            }
            //                        }
            //                        if (sql.GetRecordCount() <= 0)
            //                        {
            //                            //LServiceDetail lServiceDetail = LServiceDetail.FetchByID(IdDV);
            //                            //if (lServiceDetail != null)
            //                            //{
            //                            //    var objAssignInfo = new KcbChidinhcl();
            //                            //    objAssignInfo.ExamId = -1;
            //                            //    objAssignInfo.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham, "");
            //                            //    objAssignInfo.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan, "");

            //                            //    objAssignInfo.RegDate = globalVariables.SysDate;
            //                            //    objAssignInfo.DepartmentId = globalVariables.DepartmentID;
            //                            //    objAssignInfo.TrangthaiThanhtoan = 0;
            //                            //    objAssignInfo.CreatedBy = globalVariables.UserName;
            //                            //    objAssignInfo.CreateDate = globalVariables.SysDate;
            //                            //    objAssignInfo.Actived = 0;
            //                            //    objAssignInfo.MaKhoaThien = globalVariables.MA_KHOA_THIEN;
            //                            //    objAssignInfo.NoiTru = 0;
            //                            //    objAssignInfo.IdDoituongKcb = Utility.Int16Dbnull(
            //                            //        objLuotkham.IdDoituongKcb, -1);
            //                            //    objAssignInfo.DiagPerson = globalVariables.gv_intIDNhanvien;
            //                            //    objAssignInfo.DepartmentId = globalVariables.DepartmentID;
            //                            //    objAssignInfo.IsPHIDvuKtheo = 1;
            //                            //    objAssignInfo.IsNew = true;
            //                            //    objAssignInfo.Save();
            //                            //    objAssignInfo.IdChidinh =
            //                            //        Utility.Int32Dbnull(
            //                            //            KcbChidinhcl.CreateQuery().GetMax(KcbChidinhcl.Columns.IdChidinh), -1);
            //                            //    var objAssignDetail = new KcbChidinhclsChitiet();
            //                            //    objAssignDetail.ExamId = -1;
            //                            //    objAssignDetail.IdChidinh = objAssignInfo.IdChidinh;
            //                            //    objAssignDetail.ServiceId = lServiceDetail.ServiceId;
            //                            //    objAssignDetail.IdDichvuChitiet = lServiceDetail.IdDichvuChitiet;
            //                            //    objAssignDetail.DiscountPrice = 0;
            //                            //    objAssignDetail.DiscountRate = 0;
            //                            //    objAssignDetail.DiscountType = 0;
            //                            //    objAssignDetail.DonGia = Utility.DecimaltoDbnull(lServiceDetail.Price,
            //                            //                                                          0);
            //                            //    objAssignDetail.DiscountPrice = Utility.DecimaltoDbnull(
            //                            //        lServiceDetail.Price, 0);
            //                            //    objAssignDetail.PhuThu = 0;
            //                            //    objAssignDetail.UserId = globalVariables.UserName;
            //                            //    objAssignDetail.AssignTypeId = 0;
            //                            //    objAssignDetail.InputDate = globalVariables.SysDate;
            //                            //    objAssignDetail.TrangthaiThanhtoan = 0;
            //                            //    objAssignDetail.TuTuc = (byte?)(Utility.sDbnull(objLuotkham.MaDoituongKcb) == "DV" ? 0 : 1);
            //                            //    objAssignDetail.SoLuong = 1;
            //                            //    objAssignDetail.AssignDetailStatus = 0;
            //                            //    objAssignDetail.SDesc =
            //                            //        "Chi phí đi kèm thêm phòng khám khi đăng ký khám bệnh theo yêu cầu";
            //                            //    objAssignDetail.BhytStatus = 0;
            //                            //    objAssignDetail.DisplayOnReport = 1;
            //                            //    objAssignDetail.GiaBhytCt = 0;
            //                            //    objAssignDetail.GiaBnct = Utility.DecimaltoDbnull(lServiceDetail.Price, 0);
            //                            //    objAssignDetail.IpMayTao = globalVariables.IpAddress;
            //                            //    objAssignDetail.IpMacTao = globalVariables.IpMacAddress;
            //                            //    objAssignDetail.ChoPhepIn = 0;
            //                            //    objAssignDetail.AssignDetailStatus = 0;
            //                            //    objAssignDetail.DiagPerson = globalVariables.gv_intIDNhanvien;
            //                            //    objAssignDetail.IdDoituongKcb =
            //                            //        Utility.Int16Dbnull(objLuotkham.IdDoituongKcb,
            //                            //                            -1);
            //                            //    objAssignDetail.Stt = 1;
            //                            //    objAssignDetail.IsNew = true;
            //                            //    objAssignDetail.Save();
            //                            //}
            //                        }
            //                    }


            //                }
            //                //CHUYỂN GIÁ DỊCH VỤ CẬN LÂM SÀNG
            //                KcbChidinhclCollection taAssignInfoCollection = new Select().From(KcbChidinhcl.Schema).
            //                    Where(KcbChidinhcl.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
            //                    .And(KcbChidinhcl.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
            //                    .And(KcbChidinhcl.Columns.IsPHIDvuKtheo).IsEqualTo(0)
            //                    .ExecuteAsCollection<KcbChidinhclCollection>();
            //                foreach (KcbChidinhcl assignInfo in taAssignInfoCollection)
            //                {
            //                    KcbChidinhclsChitietCollection tAssignDetailCollection =
            //                        new Select().From(KcbChidinhclsChitiet.Schema)
            //                            .Where(KcbChidinhclsChitiet.Columns.IdChidinh).IsEqualTo(assignInfo.IdChidinh).
            //                            ExecuteAsCollection<KcbChidinhclsChitietCollection>();
            //                    foreach (KcbChidinhclsChitiet assignDetail in tAssignDetailCollection)
            //                    {
            //                        if (Utility.Int32Dbnull(assignDetail.TrangthaiThanhtoan) == 1)
            //                        {
            //                            return ActionResult.ExistedRecord;
            //                        }
            //                        DmucDoituongkcbService lObjectTypeService = new Select().From(DmucDoituongkcbService.Schema)
            //                            .Where(DmucDoituongkcbService.Columns.IdDichvuChitiet).IsEqualTo(assignDetail.IdDichvuChitiet)
            //                            .And(DmucDoituongkcbService.Columns.MaDtuong).IsEqualTo(objLuotkham.MaDoituongKcb)
            //                            .And(DmucDoituongkcbService.Columns.MaKhoaThien).IsEqualTo(objLuotkham.MaKhoaThien).ExecuteSingle<DmucDoituongkcbService>();
            //                        if (lObjectTypeService != null)
            //                        {
            //                            assignDetail.DiscountPrice = Utility.DecimaltoDbnull(lObjectTypeService.LastPrice, 0);
            //                            if (Utility.sDbnull(objLuotkham.MaDoituongKcb, "DV") == "BHYT" && Utility.Int32Dbnull(objLuotkham.DungTuyen, 0) == 0)
            //                            {
            //                                assignDetail.PhuThu = Utility.DecimaltoDbnull(lObjectTypeService.PhuThuTraiTuyen, 0);
            //                            }
            //                            else
            //                            {
            //                                assignDetail.PhuThu = Utility.DecimaltoDbnull(lObjectTypeService.Surcharge, 0);
            //                            }
            //                        }
            //                        else
            //                        {
            //                            if (Utility.sDbnull(objLuotkham.MaDoituongKcb) == "BHYT")
            //                            {

            //                                DmucDoituongkcbService lObjectTypeServiceDv = new Select().From(DmucDoituongkcbService.Schema)
            //                                    .Where(DmucDoituongkcbService.Columns.IdDichvuChitiet).IsEqualTo(assignDetail.IdDichvuChitiet)
            //                                    .And(DmucDoituongkcbService.Columns.MaDtuong).IsEqualTo("DV").And(DmucDoituongkcbService.Columns.MaKhoaThien).IsEqualTo(objLuotkham.MaKhoaThien).ExecuteSingle<DmucDoituongkcbService>();
            //                                if (lObjectTypeServiceDv != null)
            //                                {
            //                                    assignDetail.DiscountPrice = Utility.DecimaltoDbnull(lObjectTypeServiceDv.LastPrice, 0);
            //                                    assignDetail.PhuThu = 0;
            //                                    assignDetail.TuTuc = 1;
            //                                }
            //                                else
            //                                {
            //                                    Utility.ShowMsg("Không có giá Dịch Vụ");
            //                                    return ActionResult.Exceed;
            //                                }
            //                            }
            //                        }
            //                        assignDetail.Save();
            //                    }
            //                }
            //                objLuotkham.Save();
            //            }
            //        }
            //        Scope.Complete();
            //    }
            //    return ActionResult.Success;
            //}
            //catch (Exception)
            //{
            return ActionResult.Error;
            //}
        }

        public ActionResult Chotbaocao(DateTime NgayChot, DateTime ngayThanhToan)
        {
            try
            {
                string username = globalVariables.UserName;
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_CHOTSOLIEU_THEOTHUNGANVIEN", "0", false) == "0")
                    username = "ALL";
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        SPs.KcbThanhtoanChot(NgayChot.ToString("dd/MM/yyyy"), ngayThanhToan.ToString("dd/MM/yyyy"),
                            username).Execute();
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception)
            {
                return ActionResult.Error;
            }
        }

        public ActionResult ChotVetbaocao(DateTime NgayChot, DateTime NgayThanhToan)
        {
            try
            {
                string username = globalVariables.UserName;
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_CHOTSOLIEU_THEOTHUNGANVIEN", "0", false) == "0")
                    username = "ALL";
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        SPs.KcbThanhtoanChotvet(NgayChot.ToString("dd/MM/yyyy"), NgayThanhToan.ToString("dd/MM/yyyy"),
                            username).Execute();
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception)
            {
                return ActionResult.Error;
            }
        }
    }
}