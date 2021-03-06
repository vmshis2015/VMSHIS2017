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
    /// Controller class for noitru_phieuravien
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class NoitruPhieuravienController
    {
        // Preload our schema..
        NoitruPhieuravien thisSchemaLoad = new NoitruPhieuravien();
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
        public NoitruPhieuravienCollection FetchAll()
        {
            NoitruPhieuravienCollection coll = new NoitruPhieuravienCollection();
            Query qry = new Query(NoitruPhieuravien.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public NoitruPhieuravienCollection FetchByID(object IdRavien)
        {
            NoitruPhieuravienCollection coll = new NoitruPhieuravienCollection().Where("id_ravien", IdRavien).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public NoitruPhieuravienCollection FetchByQuery(Query qry)
        {
            NoitruPhieuravienCollection coll = new NoitruPhieuravienCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object IdRavien)
        {
            return (NoitruPhieuravien.Delete(IdRavien) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object IdRavien)
        {
            return (NoitruPhieuravien.Destroy(IdRavien) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string SophieuRavien,long? IdBenhnhan,string MaLuotkham,int? SoBenhAn,int? IdKhoaravien,int? IdKhoanoitru,byte? TrangThai,int TongsongayDieutri,string PhuongphapDieutri,string YkienDexuat,string LoidanBacsi,string MaKquaDieutri,string MaKieuchuyenvien,string MaTinhtrangravien,short? IdBenhvienDi,byte TrangthaiChuyenvien,short? IdBacsiChuyenvien,string MabenhChinh,string MabenhPhu,string MabenhBienchung,string MabenhNguyennhan,string MabenhGiaiphau,byte PhuhopChandoanlamsang,byte? TthaiIn,DateTime? NgayCapgiayravien,DateTime? NgayTronvien,string MaLydotronvien,DateTime NgayRavien,string NguoiTao,DateTime NgayTao,DateTime? NgaySua,string NguoiSua,string MotaBenhchinh)
	    {
		    NoitruPhieuravien item = new NoitruPhieuravien();
		    
            item.SophieuRavien = SophieuRavien;
            
            item.IdBenhnhan = IdBenhnhan;
            
            item.MaLuotkham = MaLuotkham;
            
            item.SoBenhAn = SoBenhAn;
            
            item.IdKhoaravien = IdKhoaravien;
            
            item.IdKhoanoitru = IdKhoanoitru;
            
            item.TrangThai = TrangThai;
            
            item.TongsongayDieutri = TongsongayDieutri;
            
            item.PhuongphapDieutri = PhuongphapDieutri;
            
            item.YkienDexuat = YkienDexuat;
            
            item.LoidanBacsi = LoidanBacsi;
            
            item.MaKquaDieutri = MaKquaDieutri;
            
            item.MaKieuchuyenvien = MaKieuchuyenvien;
            
            item.MaTinhtrangravien = MaTinhtrangravien;
            
            item.IdBenhvienDi = IdBenhvienDi;
            
            item.TrangthaiChuyenvien = TrangthaiChuyenvien;
            
            item.IdBacsiChuyenvien = IdBacsiChuyenvien;
            
            item.MabenhChinh = MabenhChinh;
            
            item.MabenhPhu = MabenhPhu;
            
            item.MabenhBienchung = MabenhBienchung;
            
            item.MabenhNguyennhan = MabenhNguyennhan;
            
            item.MabenhGiaiphau = MabenhGiaiphau;
            
            item.PhuhopChandoanlamsang = PhuhopChandoanlamsang;
            
            item.TthaiIn = TthaiIn;
            
            item.NgayCapgiayravien = NgayCapgiayravien;
            
            item.NgayTronvien = NgayTronvien;
            
            item.MaLydotronvien = MaLydotronvien;
            
            item.NgayRavien = NgayRavien;
            
            item.NguoiTao = NguoiTao;
            
            item.NgayTao = NgayTao;
            
            item.NgaySua = NgaySua;
            
            item.NguoiSua = NguoiSua;
            
            item.MotaBenhchinh = MotaBenhchinh;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(long IdRavien,string SophieuRavien,long? IdBenhnhan,string MaLuotkham,int? SoBenhAn,int? IdKhoaravien,int? IdKhoanoitru,byte? TrangThai,int TongsongayDieutri,string PhuongphapDieutri,string YkienDexuat,string LoidanBacsi,string MaKquaDieutri,string MaKieuchuyenvien,string MaTinhtrangravien,short? IdBenhvienDi,byte TrangthaiChuyenvien,short? IdBacsiChuyenvien,string MabenhChinh,string MabenhPhu,string MabenhBienchung,string MabenhNguyennhan,string MabenhGiaiphau,byte PhuhopChandoanlamsang,byte? TthaiIn,DateTime? NgayCapgiayravien,DateTime? NgayTronvien,string MaLydotronvien,DateTime NgayRavien,string NguoiTao,DateTime NgayTao,DateTime? NgaySua,string NguoiSua,string MotaBenhchinh)
	    {
		    NoitruPhieuravien item = new NoitruPhieuravien();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.IdRavien = IdRavien;
				
			item.SophieuRavien = SophieuRavien;
				
			item.IdBenhnhan = IdBenhnhan;
				
			item.MaLuotkham = MaLuotkham;
				
			item.SoBenhAn = SoBenhAn;
				
			item.IdKhoaravien = IdKhoaravien;
				
			item.IdKhoanoitru = IdKhoanoitru;
				
			item.TrangThai = TrangThai;
				
			item.TongsongayDieutri = TongsongayDieutri;
				
			item.PhuongphapDieutri = PhuongphapDieutri;
				
			item.YkienDexuat = YkienDexuat;
				
			item.LoidanBacsi = LoidanBacsi;
				
			item.MaKquaDieutri = MaKquaDieutri;
				
			item.MaKieuchuyenvien = MaKieuchuyenvien;
				
			item.MaTinhtrangravien = MaTinhtrangravien;
				
			item.IdBenhvienDi = IdBenhvienDi;
				
			item.TrangthaiChuyenvien = TrangthaiChuyenvien;
				
			item.IdBacsiChuyenvien = IdBacsiChuyenvien;
				
			item.MabenhChinh = MabenhChinh;
				
			item.MabenhPhu = MabenhPhu;
				
			item.MabenhBienchung = MabenhBienchung;
				
			item.MabenhNguyennhan = MabenhNguyennhan;
				
			item.MabenhGiaiphau = MabenhGiaiphau;
				
			item.PhuhopChandoanlamsang = PhuhopChandoanlamsang;
				
			item.TthaiIn = TthaiIn;
				
			item.NgayCapgiayravien = NgayCapgiayravien;
				
			item.NgayTronvien = NgayTronvien;
				
			item.MaLydotronvien = MaLydotronvien;
				
			item.NgayRavien = NgayRavien;
				
			item.NguoiTao = NguoiTao;
				
			item.NgayTao = NgayTao;
				
			item.NgaySua = NgaySua;
				
			item.NguoiSua = NguoiSua;
				
			item.MotaBenhchinh = MotaBenhchinh;
				
	        item.Save(UserName);
	    }
    }
}
