using System; 
using System.Text; 
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration; 
using System.Xml; 
using System.Xml.Serialization;
using SubSonic; 
using SubSonic.Utilities;
// <auto-generated />
namespace VNS.HIS.DAL
{
    /// <summary>
    /// Controller class for XML_3_917
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class Xml3917Controller
    {
        // Preload our schema..
        Xml3917 thisSchemaLoad = new Xml3917();
        private string userName = String.Empty;
        protected string UserName
        {
            get
            {
				if (userName.Length == 0) 
				{
    				if (System.Web.HttpContext.Current != null)
    				{
						userName=System.Web.HttpContext.Current.User.Identity.Name;
					}
					else
					{
						userName=System.Threading.Thread.CurrentPrincipal.Identity.Name;
					}
				}
				return userName;
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public Xml3917Collection FetchAll()
        {
            Xml3917Collection coll = new Xml3917Collection();
            Query qry = new Query(Xml3917.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Xml3917Collection FetchByID(object IdXML3)
        {
            Xml3917Collection coll = new Xml3917Collection().Where("ID_XML3", IdXML3).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public Xml3917Collection FetchByQuery(Query qry)
        {
            Xml3917Collection coll = new Xml3917Collection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object IdXML3)
        {
            return (Xml3917.Delete(IdXML3) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object IdXML3)
        {
            return (Xml3917.Destroy(IdXML3) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string MaLk,int? Stt,string MaDichVu,string MaVatTu,string MaNhom,string TenDichVu,string DonViTinh,decimal? SoLuong,decimal? DonGia,int? TyleTt,decimal? ThanhTien,string MaKhoa,string MaBacSi,string MaBenh,string NgayYl,string NgayKq,int? MaPttt,string GoiVtyt,string TenVatTu,int? PhamVi,string TtThau,decimal? TTrantt,int? MucHuong,string TNguonkhac,decimal? TBntt,decimal? TBhtt,decimal? TBncct,decimal? TNgoaids,string MaGiuong)
	    {
		    Xml3917 item = new Xml3917();
		    
            item.MaLk = MaLk;
            
            item.Stt = Stt;
            
            item.MaDichVu = MaDichVu;
            
            item.MaVatTu = MaVatTu;
            
            item.MaNhom = MaNhom;
            
            item.TenDichVu = TenDichVu;
            
            item.DonViTinh = DonViTinh;
            
            item.SoLuong = SoLuong;
            
            item.DonGia = DonGia;
            
            item.TyleTt = TyleTt;
            
            item.ThanhTien = ThanhTien;
            
            item.MaKhoa = MaKhoa;
            
            item.MaBacSi = MaBacSi;
            
            item.MaBenh = MaBenh;
            
            item.NgayYl = NgayYl;
            
            item.NgayKq = NgayKq;
            
            item.MaPttt = MaPttt;
            
            item.GoiVtyt = GoiVtyt;
            
            item.TenVatTu = TenVatTu;
            
            item.PhamVi = PhamVi;
            
            item.TtThau = TtThau;
            
            item.TTrantt = TTrantt;
            
            item.MucHuong = MucHuong;
            
            item.TNguonkhac = TNguonkhac;
            
            item.TBntt = TBntt;
            
            item.TBhtt = TBhtt;
            
            item.TBncct = TBncct;
            
            item.TNgoaids = TNgoaids;
            
            item.MaGiuong = MaGiuong;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(long IdXML3,string MaLk,int? Stt,string MaDichVu,string MaVatTu,string MaNhom,string TenDichVu,string DonViTinh,decimal? SoLuong,decimal? DonGia,int? TyleTt,decimal? ThanhTien,string MaKhoa,string MaBacSi,string MaBenh,string NgayYl,string NgayKq,int? MaPttt,string GoiVtyt,string TenVatTu,int? PhamVi,string TtThau,decimal? TTrantt,int? MucHuong,string TNguonkhac,decimal? TBntt,decimal? TBhtt,decimal? TBncct,decimal? TNgoaids,string MaGiuong)
	    {
		    Xml3917 item = new Xml3917();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.IdXML3 = IdXML3;
				
			item.MaLk = MaLk;
				
			item.Stt = Stt;
				
			item.MaDichVu = MaDichVu;
				
			item.MaVatTu = MaVatTu;
				
			item.MaNhom = MaNhom;
				
			item.TenDichVu = TenDichVu;
				
			item.DonViTinh = DonViTinh;
				
			item.SoLuong = SoLuong;
				
			item.DonGia = DonGia;
				
			item.TyleTt = TyleTt;
				
			item.ThanhTien = ThanhTien;
				
			item.MaKhoa = MaKhoa;
				
			item.MaBacSi = MaBacSi;
				
			item.MaBenh = MaBenh;
				
			item.NgayYl = NgayYl;
				
			item.NgayKq = NgayKq;
				
			item.MaPttt = MaPttt;
				
			item.GoiVtyt = GoiVtyt;
				
			item.TenVatTu = TenVatTu;
				
			item.PhamVi = PhamVi;
				
			item.TtThau = TtThau;
				
			item.TTrantt = TTrantt;
				
			item.MucHuong = MucHuong;
				
			item.TNguonkhac = TNguonkhac;
				
			item.TBntt = TBntt;
				
			item.TBhtt = TBhtt;
				
			item.TBncct = TBncct;
				
			item.TNgoaids = TNgoaids;
				
			item.MaGiuong = MaGiuong;
				
	        item.Save(UserName);
	    }
    }
}
