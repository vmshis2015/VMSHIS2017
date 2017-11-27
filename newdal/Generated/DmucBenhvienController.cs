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
    /// Controller class for dmuc_benhvien
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class DmucBenhvienController
    {
        // Preload our schema..
        DmucBenhvien thisSchemaLoad = new DmucBenhvien();
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
        public DmucBenhvienCollection FetchAll()
        {
            DmucBenhvienCollection coll = new DmucBenhvienCollection();
            Query qry = new Query(DmucBenhvien.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DmucBenhvienCollection FetchByID(object IdBenhvien)
        {
            DmucBenhvienCollection coll = new DmucBenhvienCollection().Where("id_benhvien", IdBenhvien).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public DmucBenhvienCollection FetchByQuery(Query qry)
        {
            DmucBenhvienCollection coll = new DmucBenhvienCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object IdBenhvien)
        {
            return (DmucBenhvien.Delete(IdBenhvien) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object IdBenhvien)
        {
            return (DmucBenhvien.Destroy(IdBenhvien) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string MaBenhvien,string TenBenhvien,string MaThanhpho,short? SttHthi,string Tuyen,string Ghichu)
	    {
		    DmucBenhvien item = new DmucBenhvien();
		    
            item.MaBenhvien = MaBenhvien;
            
            item.TenBenhvien = TenBenhvien;
            
            item.MaThanhpho = MaThanhpho;
            
            item.SttHthi = SttHthi;
            
            item.Tuyen = Tuyen;
            
            item.Ghichu = Ghichu;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int IdBenhvien,string MaBenhvien,string TenBenhvien,string MaThanhpho,short? SttHthi,string Tuyen,string Ghichu)
	    {
		    DmucBenhvien item = new DmucBenhvien();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.IdBenhvien = IdBenhvien;
				
			item.MaBenhvien = MaBenhvien;
				
			item.TenBenhvien = TenBenhvien;
				
			item.MaThanhpho = MaThanhpho;
				
			item.SttHthi = SttHthi;
				
			item.Tuyen = Tuyen;
				
			item.Ghichu = Ghichu;
				
	        item.Save(UserName);
	    }
    }
}
