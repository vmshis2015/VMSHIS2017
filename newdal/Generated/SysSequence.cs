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
	/// Strongly-typed collection for the SysSequence class.
	/// </summary>
    [Serializable]
	public partial class SysSequenceCollection : ActiveList<SysSequence, SysSequenceCollection>
	{	   
		public SysSequenceCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>SysSequenceCollection</returns>
		public SysSequenceCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                SysSequence o = this[i];
                foreach (SubSonic.Where w in this.wheres)
                {
                    bool remove = false;
                    System.Reflection.PropertyInfo pi = o.GetType().GetProperty(w.ColumnName);
                    if (pi.CanRead)
                    {
                        object val = pi.GetValue(o, null);
                        switch (w.Comparison)
                        {
                            case SubSonic.Comparison.Equals:
                                if (!val.Equals(w.ParameterValue))
                                {
                                    remove = true;
                                }
                                break;
                        }
                    }
                    if (remove)
                    {
                        this.Remove(o);
                        break;
                    }
                }
            }
            return this;
        }
		
		
	}
	/// <summary>
	/// This is an ActiveRecord class which wraps the sys_Sequence table.
	/// </summary>
	[Serializable]
	public partial class SysSequence : ActiveRecord<SysSequence>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public SysSequence()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public SysSequence(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public SysSequence(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public SysSequence(string columnName, object columnValue)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByParam(columnName,columnValue);
		}
		
		protected static void SetSQLProps() { GetTableSchema(); }
		
		#endregion
		
		#region Schema and Query Accessor	
		public static Query CreateQuery() { return new Query(Schema); }
		public static TableSchema.Table Schema
		{
			get
			{
				if (BaseSchema == null)
					SetSQLProps();
				return BaseSchema;
			}
		}
		
		private static void GetTableSchema() 
		{
			if(!IsSchemaInitialized)
			{
				//Schema declaration
				TableSchema.Table schema = new TableSchema.Table("sys_Sequence", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarSeqId = new TableSchema.TableColumn(schema);
				colvarSeqId.ColumnName = "seq_id";
				colvarSeqId.DataType = DbType.Int64;
				colvarSeqId.MaxLength = 0;
				colvarSeqId.AutoIncrement = true;
				colvarSeqId.IsNullable = false;
				colvarSeqId.IsPrimaryKey = true;
				colvarSeqId.IsForeignKey = false;
				colvarSeqId.IsReadOnly = false;
				colvarSeqId.DefaultSetting = @"";
				colvarSeqId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSeqId);
				
				TableSchema.TableColumn colvarTestvalue = new TableSchema.TableColumn(schema);
				colvarTestvalue.ColumnName = "testvalue";
				colvarTestvalue.DataType = DbType.String;
				colvarTestvalue.MaxLength = 10;
				colvarTestvalue.AutoIncrement = false;
				colvarTestvalue.IsNullable = true;
				colvarTestvalue.IsPrimaryKey = false;
				colvarTestvalue.IsForeignKey = false;
				colvarTestvalue.IsReadOnly = false;
				colvarTestvalue.DefaultSetting = @"";
				colvarTestvalue.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTestvalue);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("sys_Sequence",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("SeqId")]
		[Bindable(true)]
		public long SeqId 
		{
			get { return GetColumnValue<long>(Columns.SeqId); }
			set { SetColumnValue(Columns.SeqId, value); }
		}
		  
		[XmlAttribute("Testvalue")]
		[Bindable(true)]
		public string Testvalue 
		{
			get { return GetColumnValue<string>(Columns.Testvalue); }
			set { SetColumnValue(Columns.Testvalue, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varTestvalue)
		{
			SysSequence item = new SysSequence();
			
			item.Testvalue = varTestvalue;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(long varSeqId,string varTestvalue)
		{
			SysSequence item = new SysSequence();
			
				item.SeqId = varSeqId;
			
				item.Testvalue = varTestvalue;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn SeqIdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn TestvalueColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string SeqId = @"seq_id";
			 public static string Testvalue = @"testvalue";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
