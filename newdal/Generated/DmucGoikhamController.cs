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
    /// Controller class for dmuc_goikham
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class DmucGoikhamController
    {
        // Preload our schema..
        DmucGoikham thisSchemaLoad = new DmucGoikham();
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
        public DmucGoikhamCollection FetchAll()
        {
            DmucGoikhamCollection coll = new DmucGoikhamCollection();
            Query qry = new Query(DmucGoikham.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DmucGoikhamCollection FetchByID(object IdGoi)
        {
            DmucGoikhamCollection coll = new DmucGoikhamCollection().Where("id_goi", IdGoi).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public DmucGoikhamCollection FetchByQuery(Query qry)
        {
            DmucGoikhamCollection coll = new DmucGoikhamCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object IdGoi)
        {
            return (DmucGoikham.Delete(IdGoi) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object IdGoi)
        {
            return (DmucGoikham.Destroy(IdGoi) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string MaGoi,string TenGoi,decimal DonGia,string Maloaigoi,byte Noitru,short? SongaySudung,byte LagoiMaukiemnghiem,string Motathem,string NguoiTao,DateTime NgayTao,string NguoiSua,DateTime? NgaySua)
	    {
		    DmucGoikham item = new DmucGoikham();
		    
            item.MaGoi = MaGoi;
            
            item.TenGoi = TenGoi;
            
            item.DonGia = DonGia;
            
            item.Maloaigoi = Maloaigoi;
            
            item.Noitru = Noitru;
            
            item.SongaySudung = SongaySudung;
            
            item.LagoiMaukiemnghiem = LagoiMaukiemnghiem;
            
            item.Motathem = Motathem;
            
            item.NguoiTao = NguoiTao;
            
            item.NgayTao = NgayTao;
            
            item.NguoiSua = NguoiSua;
            
            item.NgaySua = NgaySua;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int IdGoi,string MaGoi,string TenGoi,decimal DonGia,string Maloaigoi,byte Noitru,short? SongaySudung,byte LagoiMaukiemnghiem,string Motathem,string NguoiTao,DateTime NgayTao,string NguoiSua,DateTime? NgaySua)
	    {
		    DmucGoikham item = new DmucGoikham();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.IdGoi = IdGoi;
				
			item.MaGoi = MaGoi;
				
			item.TenGoi = TenGoi;
				
			item.DonGia = DonGia;
				
			item.Maloaigoi = Maloaigoi;
				
			item.Noitru = Noitru;
				
			item.SongaySudung = SongaySudung;
				
			item.LagoiMaukiemnghiem = LagoiMaukiemnghiem;
				
			item.Motathem = Motathem;
				
			item.NguoiTao = NguoiTao;
				
			item.NgayTao = NgayTao;
				
			item.NguoiSua = NguoiSua;
				
			item.NgaySua = NgaySua;
				
	        item.Save(UserName);
	    }
    }
}