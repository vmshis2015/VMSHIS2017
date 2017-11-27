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
    /// Controller class for t_dutru_thuoc
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class TDutruThuocController
    {
        // Preload our schema..
        TDutruThuoc thisSchemaLoad = new TDutruThuoc();
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
        public TDutruThuocCollection FetchAll()
        {
            TDutruThuocCollection coll = new TDutruThuocCollection();
            Query qry = new Query(TDutruThuoc.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public TDutruThuocCollection FetchByID(object IdThuoc)
        {
            TDutruThuocCollection coll = new TDutruThuocCollection().Where("id_thuoc", IdThuoc).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public TDutruThuocCollection FetchByQuery(Query qry)
        {
            TDutruThuocCollection coll = new TDutruThuocCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object IdThuoc)
        {
            return (TDutruThuoc.Delete(IdThuoc) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object IdThuoc)
        {
            return (TDutruThuoc.Destroy(IdThuoc) == 1);
        }
        
        
        
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(int IdThuoc,short IdKho,string KieuThuocVt)
        {
            Query qry = new Query(TDutruThuoc.Schema);
            qry.QueryType = QueryType.Delete;
            qry.AddWhere("IdThuoc", IdThuoc).AND("IdKho", IdKho).AND("KieuThuocVt", KieuThuocVt);
            qry.Execute();
            return (true);
        }        
       
    	
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int IdThuoc,short IdKho,string KieuThuocVt,int? SoluongDutru,short IdKhonhan,int IdThuockho)
	    {
		    TDutruThuoc item = new TDutruThuoc();
		    
            item.IdThuoc = IdThuoc;
            
            item.IdKho = IdKho;
            
            item.KieuThuocVt = KieuThuocVt;
            
            item.SoluongDutru = SoluongDutru;
            
            item.IdKhonhan = IdKhonhan;
            
            item.IdThuockho = IdThuockho;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int IdThuoc,short IdKho,string KieuThuocVt,int? SoluongDutru,short IdKhonhan,int IdThuockho)
	    {
		    TDutruThuoc item = new TDutruThuoc();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.IdThuoc = IdThuoc;
				
			item.IdKho = IdKho;
				
			item.KieuThuocVt = KieuThuocVt;
				
			item.SoluongDutru = SoluongDutru;
				
			item.IdKhonhan = IdKhonhan;
				
			item.IdThuockho = IdThuockho;
				
	        item.Save(UserName);
	    }
    }
}
