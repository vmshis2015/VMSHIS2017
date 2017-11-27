using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Test
{

    [DataContract]
    public class ApiToken
    {
        [DataMember]
        public string username { get; set; }
        [DataMember]
        public string password { get; set; }
    }

    public class KQPhienLamViec
    {
        public string maKetQua { get; set; }
        public ApiKey APIKey { get; set; }
    }

    public class ApiKey
    {
        [DataMember]
        public string access_token { get; set; }
        [DataMember]
        public string id_token { get; set; }
        [DataMember]
        public string token_type { get; set; }
        [DataMember]
        public string username { get; set; }
        [DataMember]
        public DateTime expires_in { get; set; }
    }

    public class KQGuiHoSoGiamDinh
    {
        public string maKetQua { get; set; }
        public string maGiaoDich { get; set; }
    }

    public class KQGuiHoSoTongHop
    {
        public string maKetQua { get; set; }
        public string maGiaoDich { get; set; }
    }

    public class KQGuiHoSoDanhMuc
    {
        public string maKetQua { get; set; }
        public string maGiaoDich { get; set; }
    }
    public class KQGuiHoSoChuyenTuyen
    {
        public string maKetQua { get; set; }
        public string maGiaoDich { get; set; }
    }

    [System.Runtime.Serialization.DataContractAttribute]
    public class ApiThe
    {
        [DataMember]
        public string ma_the { get; set; }
        [DataMember]
        public string ho_ten { get; set; }
        [DataMember]
        public string ngay_sinh { get; set; }
        [DataMember]
        public short gioi_tinh { get; set; }
        [DataMember]
        public string maCSKCB { get; set; }
        [DataMember]
        public string ngay_bd { get; set; }
        [DataMember]
        public string ngay_kt { get; set; }
    }

    [System.Runtime.Serialization.DataContractAttribute]
    public class ApiTheBHYT
    {
        [DataMember]
        public string maThe { get; set; }
        [DataMember]
        public string hoTen { get; set; }
        [DataMember]
        public string ngaySinh { get; set; }
        [DataMember]
        public short gioiTinh { get; set; }
        [DataMember]
        public string maCSKCB { get; set; }
        [DataMember]
        public string ngayBD { get; set; }
        [DataMember]
        public string ngayKT { get; set; }
    }

    [System.Runtime.Serialization.DataContractAttribute]
    public class ApiContent
    {
        public string token { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public short loaiHoSo { get; set; }
        public string maTinh { get; set; }
        public string maCSKCB { get; set; }
        public byte[] fileHS { get; set; }
    }
    [System.Runtime.Serialization.DataContractAttribute]

    public class KQNhanHoSoChuyenTuyen
    {
        public string maKetQua { get; set; }
        public string fileHoSoChuyenTuyen { get; set; }
    }

    [System.Runtime.Serialization.DataContractAttribute]
    public class ApiTheGuiHSCT
    {
        [DataMember]
        public string ma_the { get; set; }
        [DataMember]
        public string ho_ten { get; set; }
        [DataMember]
        public string ngay_sinh { get; set; }
        [DataMember]
        public short gioi_tinh { get; set; }
        [DataMember]
        public string cskcb_di { get; set; }
        [DataMember]
        public string cskcb_den { get; set; }
        [DataMember]
        public byte[] fileHS { get; set; }
    }
    [System.Runtime.Serialization.DataContractAttribute]
    public class ApiTheNhanHSCT
    {
        [DataMember]
        public string ma_the { get; set; }
        [DataMember]
        public string ho_ten { get; set; }
        [DataMember]
        public string ngay_sinh { get; set; }
        [DataMember]
        public short gioi_tinh { get; set; }
        [DataMember]
        public string cskcb_di { get; set; }
        [DataMember]
        public string cskcb_den { get; set; }
    }

    public class KQNhanLichSuKCB
    {
        public string maKetQua { get; set; }
        public List<LichSuKCB> dsLichSuKCB { get; set; }
    }
    public class LichSuKCB
    {
        public long maHoSo { get; set; }
        public string maCSKCB { get; set; }
        public string tuNgay { get; set; }
        public string denNgay { get; set; }
        public string tenBenh { get; set; }
        public string tinhTrang { get; set; }
        public string kqDieuTri { get; set; }
    }

    public class KQNhanHoSoKCBChiTiet
    {
        public string maKetQua { get; set; }
        public HoSoKCBChiTiet hoSoKCB { get; set; }

    }
    public class HoSoKCBChiTiet
    {
        public Xml19324 xml1 { get; set; }
        public IList<Xml29324> dsXml2 { get; set; }
        public IList<Xml39324> dsXml3 { get; set; }
        public IList<Xml49324> dsXml4 { get; set; }
        public IList<Xml59324> dsXml5 { get; set; }
    }


    public class Xml19324
    {

        [SolrUniqueKey("id")]
        public long Id { get; set; }
        [SolrField("MA_LK")]
        public string MaLk { get; set; }
        [SolrField("STT")]
        public int? Stt { get; set; }
        [SolrField("Ma_BN")]
        public string MaBn { get; set; }
        [SolrField("HO_TEN")]
        public string HoTen { get; set; }
        [SolrField("NGAY_SINH")]
        public DateTime? NgaySinh { get; set; }
        [SolrField("GIOI_TINH")]
        public int? GioiTinh { get; set; }
        [SolrField("DIA_CHI")]
        public string DiaChi { get; set; }
        [SolrField("MA_THE")]
        public string MaThe { get; set; }
        [SolrField("MA_DKBD")]
        public string MaDkbd { get; set; }
        [SolrField("GT_THE_TU")]
        public DateTime? GtTheTu { get; set; }
        [SolrField("GT_THE_DEN")]
        public DateTime? GtTheDen { get; set; }
        [SolrField("TEN_BENH")]
        public string TenBenh { get; set; }
        [SolrField("MA_BENH")]
        public string MaBenh { get; set; }
        [SolrField("MA_BENHKHAC")]
        public string MaBenhkhac { get; set; }
        [SolrField("MA_LYDO_VVIEN")]
        public int? MaLydoVvien { get; set; }
        [SolrField("MA_NOI_CHUYEN")]
        public string MaNoiChuyen { get; set; }
        [SolrField("MA_TAI_NAN")]
        public string MaTaiNan { get; set; }
        [SolrField("NGAY_VAO")]
        public DateTime? NgayVao { get; set; }
        [SolrField("NGAY_RA")]
        public DateTime? NgayRa { get; set; }
        [SolrField("SO_NGAY_DTRI")]
        public int? SoNgayDtri { get; set; }
        [SolrField("KET_QUA_DTRI")]
        public int? KetQuaDtri { get; set; }
        [SolrField("TINH_TRANG_RV")]
        public int? TinhTrangRv { get; set; }
        [SolrField("NGAY_TTOAN")]
        public string NgayTtoan { get; set; }
        [SolrField("MUC_HUONG")]
        public float? MucHuong { get; set; }
        [SolrField("T_THUOC")]
        public long? TThuoc { get; set; }
        [SolrField("T_VTYT")]
        public long? TVtyt { get; set; }
        [SolrField("T_TONGCHI")]
        public long? TTongchi { get; set; }
        [SolrField("T_BNTT")]
        public long? TBntt { get; set; }
        [SolrField("T_BHTT")]
        public long? TBhtt { get; set; }
        [SolrField("T_NGUONKHAC")]
        public long? TNguonkhac { get; set; }
        [SolrField("T_NGOAIDS")]
        public long? TNgoaids { get; set; }
        [SolrField("NAM_QT")]
        public int? NamQt { get; set; }
        [SolrField("THANG_QT")]
        public int? ThangQt { get; set; }
        [SolrField("MA_LOAI_KCB")]
        public int? MaLoaiKcb { get; set; }
        [SolrField("MA_KHOA")]
        public string MaKhoa { get; set; }
        [SolrField("MA_CSKCB")]
        public string MaCskcb { get; set; }
        [SolrField("MA_KHUVUC")]
        public string MaKhuvuc { get; set; }
        [SolrField("MA_PTTT_QT")]
        public string MaPtttQt { get; set; }
        [SolrField("CAN_NANG")]
        public float? CanNang { get; set; }
        [SolrField("COSOKCB_ID")]
        public int CosokcbId { get; set; }
        [SolrField("TINHTHANH_ID")]
        public int? TinhthanhId { get; set; }
        [SolrField("TRANGTHAI")]
        public int Trangthai { get; set; }
        [SolrField("HOSO_ID")]
        public long? HosoId { get; set; }
        [SolrField("KYGIAMDINH_ID")]
        public long? KygiamdinhId { get; set; }
        [SolrField("MIEUTA")]
        public string Mieuta { get; set; }
        [SolrField("STATUS")]
        public int? Status { get; set; }
        [SolrField("RESULTCHUNG")]
        public string Resultchung { get; set; }
        [SolrField("RESULTTHUOC")]
        public string Resultthuoc { get; set; }
        [SolrField("RESULTDICHVU")]
        public string Resultdichvu { get; set; }
        [SolrField("XUATTOAN")]
        public long? Xuattoan { get; set; }
        [SolrField("NGUOIGIAMDINH_ID")]
        public int? NguoigiamdinhId { get; set; }
        [SolrField("NGAYTHANHTOAN")]
        public DateTime? Ngaythanhtoan { get; set; }
        public long? DotgiamdinhId { get; set; }
        public int? Trongmau { get; set; }
        [SolrField("LOAI_BN")]
        public int? LoaiBn { get; set; }
        [SolrField("K_XETNGHIEM")]
        public long? KXetnghiem { get; set; }
        [SolrField("K_CDHATDCN")]
        public long? KCdhatdcn { get; set; }
        [SolrField("K_THUOC")]
        public long? KThuoc { get; set; }
        [SolrField("K_MAU")]
        public long? KMau { get; set; }
        [SolrField("K_THUTHUATPHAUTHUAT")]
        public long? KPttt { get; set; }
        [SolrField("K_VTYT")]
        public long? KVtyt { get; set; }
        [SolrField("H_DVKT")]
        public long? HDvkt { get; set; }
        [SolrField("H_THUOC")]
        public long? HThuoc { get; set; }
        [SolrField("H_VTYT")]
        public long? HVtyt { get; set; }
        [SolrField("TIENKHAM")]
        public long? Tienkham { get; set; }
        [SolrField("VANCHUYEN")]
        public long? Vanchuyen { get; set; }
        [SolrField("TIENGIUONG")]
        public long? Tiengiuong { get; set; }
        public DateTime? Ngaynhan { get; set; }
        public string ResultchungTd { get; set; }
        public string ResultthuocTd { get; set; }
        public string ResultdichvuTd { get; set; }
        public int? TrangthaiTd { get; set; }
        public string Ghichuhs { get; set; }
        public short? Theodoi { get; set; }
        public short? Giamdinhtiep { get; set; }
        public short? Ketqua { get; set; }
        public int? Sttmau { get; set; }
        public short? KetquaTd { get; set; }
        public int? MaLydoVvienTd { get; set; }
        public float? MucHuongTd { get; set; }
        public long? KXetnghiemQt { get; set; }
        public long? KCdhatdcnQt { get; set; }
        public long? KThuocQt { get; set; }
        public long? KMauQt { get; set; }
        public long? KPtttQt { get; set; }
        public long? KVtytQt { get; set; }
        public long? HDvktQt { get; set; }
        public long? HThuocQt { get; set; }
        public long? HVtytQt { get; set; }
        public long? VanchuyenQt { get; set; }
        public long? TiengiuongQt { get; set; }
        public long? TienkhamQt { get; set; }
        //public  IList<Xml29324> Xml2 { get; set; }
        //public  IList<Xml39324> Xml3 { get; set; }
        //public  IList<Xml49324> Xml4 { get; set; }
        //public  IList<Xml59324> Xml5 { get; set; }
        //public  short? LoaiBn { get; set; }
        //public  long? KXetnghiem { get; set; }
        //public  long? KCdhatdcn { get; set; }

        //public  long? KThuoc { get; set; }
        //public  long? KMau { get; set; }
        //public  long? KPttt { get; set; }
        //public  long? KVtyt { get; set; }
        //public  long? HDvkt { get; set; }
        //public  long? HThuoc { get; set; }
        //public  long? HVtyt { get; set; }
        //public  long? Tienkham { get; set; }
        //public  long? Vanchuyen { get; set; }
        //public  long? Tiengiuong { get; set; }
        //public  DateTime? Ngaynhan { get; set; }
    }
    public class Xml29324
    {
        public long Id { get; set; }
        public string MaLk { get; set; }
        public int? Stt { get; set; }
        public string MaThuoc { get; set; }
        public string MaNhom { get; set; }
        public string TenThuoc { get; set; }
        public string DonViTinh { get; set; }
        public string HamLuong { get; set; }
        public string DuongDung { get; set; }
        public string LieuDung { get; set; }
        public string SoDangKy { get; set; }
        public double? SoLuong { get; set; }
        public long? DonGia { get; set; }
        public double? TyLe { get; set; }
        public long? ThanhTien { get; set; }
        public string MaKhoa { get; set; }
        public string MaBacSi { get; set; }
        public string MaBenh { get; set; }
        public DateTime? NgayYl { get; set; }
        public short? MaPttt { get; set; }
        public int CosokcbId { get; set; }
        public int? TinhthanhId { get; set; }
        public short Trangthai { get; set; }
        public long? HosoId { get; set; }
        public long? Xml19324Id { get; set; }
        public long? KygiamdinhId { get; set; }
        public string Mieuta { get; set; }
        public short? Status { get; set; }
        public int? ThuocId { get; set; }
        public bool isXuatToanTP { get; set; }
        public long xuattoan { get; set; }
    }
    public class Xml39324
    {
        public long Id { get; set; }
        public string MaLk { get; set; }
        public int? Stt { get; set; }
        public string MaDichVu { get; set; }
        public string MaVatTu { get; set; }
        public string MaNhom { get; set; }
        public string TenDichVu { get; set; }
        public string DonViTinh { get; set; }
        public double? SoLuong { get; set; }
        public long? DonGia { get; set; }
        public double? TyleTt { get; set; }
        public long? ThanhTien { get; set; }
        public string MaKhoa { get; set; }
        public string MaBacSi { get; set; }
        public string MaBenh { get; set; }
        public DateTime? NgayYl { get; set; }
        public DateTime? NgayKq { get; set; }
        public short? MaPttt { get; set; }
        public int CosokcbId { get; set; }
        public int? TinhthanhId { get; set; }
        public short Trangthai { get; set; }
        public string Mieuta { get; set; }
        public short? Status { get; set; }
        public long? HosoId { get; set; }
        public long? Xml19324 { get; set; }
        public long? KygiamdinhId { get; set; }
        public bool isXuatToanTP { get; set; }
        public long xuattoan { get; set; }
    }
    public class Xml49324
    {
        public long Id { get; set; }
        public string MaLk { get; set; }
        public int? Stt { get; set; }
        public string MaDichVu { get; set; }
        public string MaChiSo { get; set; }
        public string TenChiSo { get; set; }
        public string GiaTri { get; set; }
        public string MaMay { get; set; }
        public string MoTa { get; set; }
        public string KetLuan { get; set; }
        public string NgayKq { get; set; }
        public long? HosoId { get; set; }
        public long? KygiamdinhId { get; set; }
        public int? CosokcbId { get; set; }
        public int? TinhthanhId { get; set; }
        public short? Trangthai { get; set; }
        public string Mieuta { get; set; }
        public short? Status { get; set; }
        public long? Xml19324Id { get; set; }
        public bool isXuatToanTP { get; set; }
        public long xuattoan { get; set; }
    }
    public class Xml59324
    {
        public long Id { get; set; }
        public string MaLk { get; set; }
        public int? Stt { get; set; }
        public string DienBien { get; set; }
        public string HoiChan { get; set; }
        public string PhauThuat { get; set; }
        public string NgayYl { get; set; }
        public long? HosoId { get; set; }
        public long? KygiamdinhId { get; set; }
        public int? CosokcbId { get; set; }
        public int? TinhthanhId { get; set; }
        public short Trangthai { get; set; }
        public string Mieuta { get; set; }
        public short? Status { get; set; }
        public long? Xml19324Id { get; set; }
        public bool isXuatToanTP { get; set; }
        public long xuattoan { get; set; }
    }
    public class KQNhanDSGDDanhMucThang
    {
        [DataMember]
        public string maKetQua { get; set; }
        [DataMember]
        public List<GDDanhMuc> dsGDDanhMuc { get; set; }
    }
    public class GDDanhMuc
    {
        public string tenFile { get; set; }
        public string ngayTao { get; set; }
        public decimal dungLuong { get; set; }
    }
    [System.Runtime.Serialization.DataContractAttribute]
    public class KQNhanKQGDDanhMuc
    {
        [DataMember]
        public string maKetQua { get; set; }
        [DataMember]
        public string fileKQ { get; set; }
    }

    public class KQNhanKQTiepNhanHS
    {
        public string maKetQua { get; set; }
        public List<KQGuiHosoNgay> dsKQGuiHosoNgay { get; set; }
    }

    public class KQGuiHosoNgay
    {
        public string ngayGui { get; set; }
        public int tongSo { get; set; }
        public int soHSThanhCong { get; set; }
        public int soHSLoi { get; set; }
        public decimal tongTien { get; set; }
    }
    public class KQNhanChiTietHSLoiNgay
    {
        public string maKetQua { get; set; }
        public List<ThongTinHSLoi> dsHSLoi { get; set; }
    }
    public class ThongTinHSLoi
    {
        public string maGiaoDich { get; set; }
        public string mieuTa { get; set; }
        public string ngayGui { get; set; }
    }

    public class KQNhanChiTietLoiHS
    {
        public string maKetQua { get; set; }
        public List<ChiTietLoiHS> dsLoi { get; set; }
    }
    public class ChiTietLoiHS
    {
        public string maLoi { get; set; }
        public string moTaLoi { get; set; }
    }
    public class KQNhanDSDotGDThang
    {
        public string maKetQua { get; set; }
        public List<ChiTietDotGD> dsDotGD { get; set; }
    }

    public class ChiTietDotGD
    {
        public string tenDotGD { get; set; }
        public string maDotGD { get; set; }
        public short thangGD { get; set; }
        public short namGD { get; set; }
        public string loaiGD { get; set; }
        public string ngayTao { get; set; }
    }

    public class KQNhanKQGDHoSo
    {
        public string maKetQua { get; set; }
        public string fileBase64String { get; set; }
    }
    public class KQNhanQuyetToanThangQuy
    {
        public string maKetQua { get; set; }
        public string fileBase64String { get; set; }
    }

  

}

