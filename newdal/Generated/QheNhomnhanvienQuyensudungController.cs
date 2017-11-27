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
    /// Controller class for qhe_nhomnhanvien_quyensudung
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class QheNhomnhanvienQuyensudungController
    {
        // Preload our schema..
        QheNhomnhanvienQuyensudung thisSchemaLoad = new QheNhomnhanvienQuyensudung();
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
        public QheNhomnhanvienQuyensudungCollection FetchAll()
        {
            QheNhomnhanvienQuyensudungCollection coll = new QheNhomnhanvienQuyensudungCollection();
            Query qry = new Query(QheNhomnhanvienQuyensudung.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public QheNhomnhanvienQuyensudungCollection FetchByID(object MaNhom)
        {
            QheNhomnhanvienQuyensudungCollection coll = new QheNhomnhanvienQuyensudungCollection().Where("ma_nhom", MaNhom).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public QheNhomnhanvienQuyensudungCollection FetchByQuery(Query qry)
        {
            QheNhomnhanvienQuyensudungCollection coll = new QheNhomnhanvienQuyensudungCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object MaNhom)
        {
            return (QheNhomnhanvienQuyensudung.Delete(MaNhom) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object MaNhom)
        {
            return (QheNhomnhanvienQuyensudung.Destroy(MaNhom) == 1);
        }
        
        
        
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(string MaNhom,string MaQuyen)
        {
            Query qry = new Query(QheNhomnhanvienQuyensudung.Schema);
            qry.QueryType = QueryType.Delete;
            qry.AddWhere("MaNhom", MaNhom).AND("MaQuyen", MaQuyen);
            qry.Execute();
            return (true);
        }        
       
    	
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string MaNhom,string MaQuyen)
	    {
		    QheNhomnhanvienQuyensudung item = new QheNhomnhanvienQuyensudung();
		    
            item.MaNhom = MaNhom;
            
            item.MaQuyen = MaQuyen;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(string MaNhom,string MaQuyen)
	    {
		    QheNhomnhanvienQuyensudung item = new QheNhomnhanvienQuyensudung();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.MaNhom = MaNhom;
				
			item.MaQuyen = MaQuyen;
				
	        item.Save(UserName);
	    }
    }
}
