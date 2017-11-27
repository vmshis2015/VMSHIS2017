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
    /// Controller class for SysConfigRadio
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class SysConfigRadioController
    {
        // Preload our schema..
        SysConfigRadio thisSchemaLoad = new SysConfigRadio();
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
        public SysConfigRadioCollection FetchAll()
        {
            SysConfigRadioCollection coll = new SysConfigRadioCollection();
            Query qry = new Query(SysConfigRadio.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public SysConfigRadioCollection FetchByID(object SysId)
        {
            SysConfigRadioCollection coll = new SysConfigRadioCollection().Where("Sys_ID", SysId).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public SysConfigRadioCollection FetchByQuery(Query qry)
        {
            SysConfigRadioCollection coll = new SysConfigRadioCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object SysId)
        {
            return (SysConfigRadio.Delete(SysId) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object SysId)
        {
            return (SysConfigRadio.Destroy(SysId) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string UserName,string PassWord,string Domain,string PathUNC)
	    {
		    SysConfigRadio item = new SysConfigRadio();
		    
            item.UserName = UserName;
            
            item.PassWord = PassWord;
            
            item.Domain = Domain;
            
            item.PathUNC = PathUNC;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int SysId,string UserName,string PassWord,string Domain,string PathUNC)
	    {
		    SysConfigRadio item = new SysConfigRadio();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.SysId = SysId;
				
			item.UserName = UserName;
				
			item.PassWord = PassWord;
				
			item.Domain = Domain;
				
			item.PathUNC = PathUNC;
				
	        item.Save(UserName);
	    }
    }
}