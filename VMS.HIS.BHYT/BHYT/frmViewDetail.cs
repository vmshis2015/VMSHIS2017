using System;
using System.Data;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using SubSonic;
using VNS.HIS.DAL;
using VNS.Libs;

namespace VMS.HIS.BHYT
{
    public partial class frmViewDetail : Form
    {
        private readonly string PatientCode = "";

        private DataSet _dtXml = new DataSet();

        public frmViewDetail(string maLk)
        {
            InitializeComponent();
            PatientCode = maLk;
        }

        private void frmViewDetail_Load(object sender, EventArgs e)
        {
            _dtXml = SPs.SpXmlThongTuBHYT917GetData(Utility.sDbnull(PatientCode)).GetDataSet();
            if (_dtXml.Tables[0].Rows.Count > 0)
            {
                grdList.DataSource = _dtXml.Tables[0];
            }
            if (_dtXml.Tables[1].Rows.Count > 0)
            {
                gridThuoc.DataSource = _dtXml.Tables[1];
            }
            if (_dtXml.Tables[2].Rows.Count > 0)
            {
                gridDVKT.DataSource = _dtXml.Tables[2];
            }
        }

        private void grdList_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            try
            {
                if (grdList.CurrentRow == null) return;
                if (Utility.sDbnull(grdList.GetValue("MA_LK"), "") != "")
                {
                    new Update(Xml1917.Schema)
                        .Set(Xml1917.Columns.Stt).EqualTo(grdList.GetValue(Xml1917.Columns.Stt))
                        .Set(Xml1917.Columns.MaBn).EqualTo(grdList.GetValue(Xml1917.Columns.MaBn))
                        .Set(Xml1917.Columns.HoTen).EqualTo(grdList.GetValue(Xml1917.Columns.HoTen))
                        .Set(Xml1917.Columns.NgaySinh).EqualTo(grdList.GetValue(Xml1917.Columns.NgaySinh))
                        .Set(Xml1917.Columns.DiaChi).EqualTo(grdList.GetValue(Xml1917.Columns.DiaChi))
                        .Set(Xml1917.Columns.MaThe).EqualTo(grdList.GetValue(Xml1917.Columns.MaThe))
                        .Set(Xml1917.Columns.MaDkbd).EqualTo(grdList.GetValue(Xml1917.Columns.MaDkbd))
                        .Set(Xml1917.Columns.GtTheTu).EqualTo(grdList.GetValue(Xml1917.Columns.GtTheTu))
                        .Set(Xml1917.Columns.GtTheDen).EqualTo(grdList.GetValue(Xml1917.Columns.GtTheDen))
                        .Set(Xml1917.Columns.MienCungCt).EqualTo(grdList.GetValue(Xml1917.Columns.MienCungCt))
                        .Set(Xml1917.Columns.TenBenh).EqualTo(grdList.GetValue(Xml1917.Columns.TenBenh))
                        .Set(Xml1917.Columns.MaBenh).EqualTo(grdList.GetValue(Xml1917.Columns.MaBenh))
                        .Set(Xml1917.Columns.MaBenhkhac).EqualTo(grdList.GetValue(Xml1917.Columns.MaBenhkhac))
                        .Set(Xml1917.Columns.MaLydoVvien).EqualTo(grdList.GetValue(Xml1917.Columns.MaLydoVvien))
                        .Set(Xml1917.Columns.MaNoiChuyen).EqualTo(grdList.GetValue(Xml1917.Columns.MaNoiChuyen))
                        .Set(Xml1917.Columns.MaTaiNan).EqualTo(grdList.GetValue(Xml1917.Columns.MaTaiNan))
                        .Set(Xml1917.Columns.NgayRa).EqualTo(grdList.GetValue(Xml1917.Columns.NgayRa))
                        .Set(Xml1917.Columns.NgayVao).EqualTo(grdList.GetValue(Xml1917.Columns.NgayVao))
                        .Set(Xml1917.Columns.SoNgayDtri).EqualTo(grdList.GetValue(Xml1917.Columns.SoNgayDtri))
                        .Set(Xml1917.Columns.KetQuaDtri).EqualTo(grdList.GetValue(Xml1917.Columns.KetQuaDtri))
                        .Set(Xml1917.Columns.TinhTrangRv).EqualTo(grdList.GetValue(Xml1917.Columns.TinhTrangRv))
                        .Set(Xml1917.Columns.NgayTtoan).EqualTo(grdList.GetValue(Xml1917.Columns.NgayTtoan))
                        .Set(Xml1917.Columns.MucHuong).EqualTo(grdList.GetValue(Xml1917.Columns.MucHuong))
                        .Set(Xml1917.Columns.TThuoc).EqualTo(grdList.GetValue(Xml1917.Columns.TThuoc))
                        .Set(Xml1917.Columns.TVtyt).EqualTo(grdList.GetValue(Xml1917.Columns.TVtyt))
                        .Set(Xml1917.Columns.TTongchi).EqualTo(grdList.GetValue(Xml1917.Columns.TTongchi))
                        .Set(Xml1917.Columns.TBntt).EqualTo(grdList.GetValue(Xml1917.Columns.TBntt))
                        .Set(Xml1917.Columns.TBncct).EqualTo(grdList.GetValue(Xml1917.Columns.TBncct))
                        .Set(Xml1917.Columns.TBhtt).EqualTo(grdList.GetValue(Xml1917.Columns.TBhtt))
                        .Set(Xml1917.Columns.TNguonkhac).EqualTo(grdList.GetValue(Xml1917.Columns.TNguonkhac))
                        .Set(Xml1917.Columns.TNgoaids).EqualTo(grdList.GetValue(Xml1917.Columns.TNgoaids))
                        .Set(Xml1917.Columns.NamQt).EqualTo(grdList.GetValue(Xml1917.Columns.NamQt))
                        .Set(Xml1917.Columns.ThangQt).EqualTo(grdList.GetValue(Xml1917.Columns.ThangQt))
                        .Set(Xml1917.Columns.MaLoaiKcb).EqualTo(grdList.GetValue(Xml1917.Columns.MaLoaiKcb))
                        .Set(Xml1917.Columns.MaKhoa).EqualTo(grdList.GetValue(Xml1917.Columns.MaKhoa))
                        .Set(Xml1917.Columns.MaCskcb).EqualTo(grdList.GetValue(Xml1917.Columns.MaCskcb))
                        .Set(Xml1917.Columns.MaKhuvuc).EqualTo(grdList.GetValue(Xml1917.Columns.MaKhuvuc))
                        .Set(Xml1917.Columns.MaPtttQt).EqualTo(grdList.GetValue(Xml1917.Columns.MaPtttQt))
                        .Set(Xml1917.Columns.CanNang).EqualTo(grdList.GetValue(Xml1917.Columns.CanNang))
                        .Where(Xml1917.Columns.MaLk).IsEqualTo(grdList.GetValue(Xml1917.Columns.MaLk)).Execute();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void gridThuoc_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            try
            {
                if (Utility.sDbnull(gridThuoc.GetValue("ID_XML2"), "") != "")
                {
                    new Update(Xml2917.Schema)
                        .Set(Xml2917.Columns.Stt).EqualTo(gridThuoc.GetValue(Xml2917.Columns.Stt))
                        .Set(Xml2917.Columns.MaThuoc).EqualTo(gridThuoc.GetValue(Xml2917.Columns.MaThuoc))
                        .Set(Xml2917.Columns.MaNhom).EqualTo(gridThuoc.GetValue(Xml2917.Columns.MaNhom))
                        .Set(Xml2917.Columns.TenThuoc).EqualTo(gridThuoc.GetValue(Xml2917.Columns.TenThuoc))
                        .Set(Xml2917.Columns.DonViTinh).EqualTo(gridThuoc.GetValue(Xml2917.Columns.DonViTinh))
                        .Set(Xml2917.Columns.HamLuong).EqualTo(gridThuoc.GetValue(Xml2917.Columns.HamLuong))
                        .Set(Xml2917.Columns.LieuDung).EqualTo(gridThuoc.GetValue(Xml2917.Columns.LieuDung))
                        .Set(Xml2917.Columns.SoDangKy).EqualTo(gridThuoc.GetValue(Xml2917.Columns.SoDangKy))
                        .Set(Xml2917.Columns.SoLuong).EqualTo(gridThuoc.GetValue(Xml2917.Columns.SoLuong))
                        .Set(Xml2917.Columns.DonGia).EqualTo(gridThuoc.GetValue(Xml2917.Columns.DonGia))
                        .Set(Xml2917.Columns.TyleTt).EqualTo(gridThuoc.GetValue(Xml2917.Columns.TyleTt))
                        .Set(Xml2917.Columns.ThanhTien).EqualTo(gridThuoc.GetValue(Xml2917.Columns.ThanhTien))
                        .Set(Xml2917.Columns.MaKhoa).EqualTo(gridThuoc.GetValue(Xml2917.Columns.MaKhoa))
                        .Set(Xml2917.Columns.MaBacSi).EqualTo(gridThuoc.GetValue(Xml2917.Columns.MaBacSi))
                        .Set(Xml2917.Columns.MaBenh).EqualTo(gridThuoc.GetValue(Xml2917.Columns.MaBenh))
                        .Set(Xml2917.Columns.NgayYl).EqualTo(gridThuoc.GetValue(Xml2917.Columns.NgayYl))
                        .Set(Xml2917.Columns.MaPttt).EqualTo(gridThuoc.GetValue(Xml2917.Columns.MaPttt))
                        .Set(Xml2917.Columns.TtThau).EqualTo(gridThuoc.GetValue(Xml2917.Columns.TtThau))
                        .Set(Xml2917.Columns.PhamVi).EqualTo(gridThuoc.GetValue(Xml2917.Columns.PhamVi))
                        .Set(Xml2917.Columns.MucHuong).EqualTo(gridThuoc.GetValue(Xml2917.Columns.MucHuong))
                        .Set(Xml2917.Columns.TNguonkhac).EqualTo(gridThuoc.GetValue(Xml2917.Columns.TNguonkhac))
                        .Set(Xml2917.Columns.TBntt).EqualTo(gridThuoc.GetValue(Xml2917.Columns.TBntt))
                        .Set(Xml2917.Columns.TBncct).EqualTo(gridThuoc.GetValue(Xml2917.Columns.TBncct))
                        .Set(Xml2917.Columns.TBhtt).EqualTo(gridThuoc.GetValue(Xml2917.Columns.TBhtt))
                        .Set(Xml2917.Columns.TNgoaids).EqualTo(gridThuoc.GetValue(Xml2917.Columns.TNgoaids))
                        .Where(Xml2917.Columns.IdXML2).IsEqualTo(gridThuoc.GetValue(Xml2917.Columns.IdXML2)).Execute();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void gridDVKT_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            try
            {
                if (Utility.sDbnull(gridDVKT.GetValue("ID_XML3"), "") != "")
                {
                    new Update(Xml3917.Schema)
                        .Set(Xml3917.Columns.Stt).EqualTo(gridDVKT.GetValue(Xml3917.Columns.Stt))
                        .Set(Xml3917.Columns.MaDichVu).EqualTo(gridDVKT.GetValue(Xml3917.Columns.MaDichVu))
                        .Set(Xml3917.Columns.MaVatTu).EqualTo(gridDVKT.GetValue(Xml3917.Columns.MaVatTu))
                        .Set(Xml3917.Columns.TenDichVu).EqualTo(gridDVKT.GetValue(Xml3917.Columns.TenDichVu))
                        .Set(Xml3917.Columns.TenVatTu).EqualTo(gridDVKT.GetValue(Xml3917.Columns.TenVatTu))
                        .Set(Xml3917.Columns.DonViTinh).EqualTo(gridDVKT.GetValue(Xml3917.Columns.DonViTinh))
                        .Set(Xml3917.Columns.NgayKq).EqualTo(gridDVKT.GetValue(Xml3917.Columns.NgayKq))
                        .Set(Xml3917.Columns.SoLuong).EqualTo(gridDVKT.GetValue(Xml3917.Columns.SoLuong))
                        .Set(Xml3917.Columns.DonGia).EqualTo(gridDVKT.GetValue(Xml3917.Columns.DonGia))
                        .Set(Xml3917.Columns.TyleTt).EqualTo(gridDVKT.GetValue(Xml3917.Columns.TyleTt))
                        .Set(Xml3917.Columns.ThanhTien).EqualTo(gridDVKT.GetValue(Xml3917.Columns.ThanhTien))
                        .Set(Xml3917.Columns.MaKhoa).EqualTo(gridDVKT.GetValue(Xml3917.Columns.MaKhoa))
                        .Set(Xml3917.Columns.MaBacSi).EqualTo(gridDVKT.GetValue(Xml3917.Columns.MaBacSi))
                        .Set(Xml3917.Columns.MaBenh).EqualTo(gridDVKT.GetValue(Xml3917.Columns.MaBenh))
                        .Set(Xml3917.Columns.NgayYl).EqualTo(gridDVKT.GetValue(Xml3917.Columns.NgayYl))
                        .Set(Xml3917.Columns.MaPttt).EqualTo(gridDVKT.GetValue(Xml3917.Columns.MaPttt))
                        .Set(Xml3917.Columns.TtThau).EqualTo(gridDVKT.GetValue(Xml3917.Columns.TtThau))
                        .Set(Xml3917.Columns.PhamVi).EqualTo(gridDVKT.GetValue(Xml3917.Columns.PhamVi))
                        .Set(Xml3917.Columns.MucHuong).EqualTo(gridDVKT.GetValue(Xml3917.Columns.MucHuong))
                        .Set(Xml3917.Columns.TNguonkhac).EqualTo(gridDVKT.GetValue(Xml3917.Columns.TNguonkhac))
                        .Set(Xml3917.Columns.TBntt).EqualTo(gridDVKT.GetValue(Xml3917.Columns.TBntt))
                        .Set(Xml3917.Columns.TBncct).EqualTo(gridDVKT.GetValue(Xml3917.Columns.TBncct))
                        .Set(Xml3917.Columns.TBhtt).EqualTo(gridDVKT.GetValue(Xml3917.Columns.TBhtt))
                        .Set(Xml3917.Columns.TNgoaids).EqualTo(gridDVKT.GetValue(Xml3917.Columns.TNgoaids))
                        .Set(Xml3917.Columns.GoiVtyt).EqualTo(gridDVKT.GetValue(Xml3917.Columns.GoiVtyt))
                        .Set(Xml3917.Columns.MaGiuong).EqualTo(gridDVKT.GetValue(Xml3917.Columns.MaGiuong))
                        .Set(Xml3917.Columns.TTrantt).EqualTo(gridDVKT.GetValue(Xml3917.Columns.TTrantt))
                        .Where(Xml3917.Columns.IdXML3).IsEqualTo(gridDVKT.GetValue(Xml3917.Columns.IdXML3)).Execute();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
    }
}