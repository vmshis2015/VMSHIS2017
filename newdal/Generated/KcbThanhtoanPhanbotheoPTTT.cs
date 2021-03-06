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
	/// Strongly-typed collection for the KcbThanhtoanPhanbotheoPTTT class.
	/// </summary>
    [Serializable]
	public partial class KcbThanhtoanPhanbotheoPTTTCollection : ActiveList<KcbThanhtoanPhanbotheoPTTT, KcbThanhtoanPhanbotheoPTTTCollection>
	{	   
		public KcbThanhtoanPhanbotheoPTTTCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>KcbThanhtoanPhanbotheoPTTTCollection</returns>
		public KcbThanhtoanPhanbotheoPTTTCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                KcbThanhtoanPhanbotheoPTTT o = this[i];
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
	/// This is an ActiveRecord class which wraps the kcb_thanhtoan_phanbotheoPTTT table.
	/// </summary>
	[Serializable]
	public partial class KcbThanhtoanPhanbotheoPTTT : ActiveRecord<KcbThanhtoanPhanbotheoPTTT>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public KcbThanhtoanPhanbotheoPTTT()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public KcbThanhtoanPhanbotheoPTTT(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public KcbThanhtoanPhanbotheoPTTT(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public KcbThanhtoanPhanbotheoPTTT(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("kcb_thanhtoan_phanbotheoPTTT", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarIdThanhtoan = new TableSchema.TableColumn(schema);
				colvarIdThanhtoan.ColumnName = "id_thanhtoan";
				colvarIdThanhtoan.DataType = DbType.Int64;
				colvarIdThanhtoan.MaxLength = 0;
				colvarIdThanhtoan.AutoIncrement = false;
				colvarIdThanhtoan.IsNullable = false;
				colvarIdThanhtoan.IsPrimaryKey = true;
				colvarIdThanhtoan.IsForeignKey = false;
				colvarIdThanhtoan.IsReadOnly = false;
				colvarIdThanhtoan.DefaultSetting = @"";
				colvarIdThanhtoan.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdThanhtoan);
				
				TableSchema.TableColumn colvarMaPttt = new TableSchema.TableColumn(schema);
				colvarMaPttt.ColumnName = "ma_pttt";
				colvarMaPttt.DataType = DbType.String;
				colvarMaPttt.MaxLength = 20;
				colvarMaPttt.AutoIncrement = false;
				colvarMaPttt.IsNullable = false;
				colvarMaPttt.IsPrimaryKey = true;
				colvarMaPttt.IsForeignKey = false;
				colvarMaPttt.IsReadOnly = false;
				colvarMaPttt.DefaultSetting = @"";
				colvarMaPttt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaPttt);
				
				TableSchema.TableColumn colvarIdBenhnhan = new TableSchema.TableColumn(schema);
				colvarIdBenhnhan.ColumnName = "id_benhnhan";
				colvarIdBenhnhan.DataType = DbType.Int64;
				colvarIdBenhnhan.MaxLength = 0;
				colvarIdBenhnhan.AutoIncrement = false;
				colvarIdBenhnhan.IsNullable = false;
				colvarIdBenhnhan.IsPrimaryKey = false;
				colvarIdBenhnhan.IsForeignKey = false;
				colvarIdBenhnhan.IsReadOnly = false;
				colvarIdBenhnhan.DefaultSetting = @"";
				colvarIdBenhnhan.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdBenhnhan);
				
				TableSchema.TableColumn colvarMaLuotkham = new TableSchema.TableColumn(schema);
				colvarMaLuotkham.ColumnName = "ma_luotkham";
				colvarMaLuotkham.DataType = DbType.String;
				colvarMaLuotkham.MaxLength = 10;
				colvarMaLuotkham.AutoIncrement = false;
				colvarMaLuotkham.IsNullable = false;
				colvarMaLuotkham.IsPrimaryKey = false;
				colvarMaLuotkham.IsForeignKey = false;
				colvarMaLuotkham.IsReadOnly = false;
				colvarMaLuotkham.DefaultSetting = @"";
				colvarMaLuotkham.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaLuotkham);
				
				TableSchema.TableColumn colvarNoiTru = new TableSchema.TableColumn(schema);
				colvarNoiTru.ColumnName = "noi_tru";
				colvarNoiTru.DataType = DbType.Byte;
				colvarNoiTru.MaxLength = 0;
				colvarNoiTru.AutoIncrement = false;
				colvarNoiTru.IsNullable = false;
				colvarNoiTru.IsPrimaryKey = false;
				colvarNoiTru.IsForeignKey = false;
				colvarNoiTru.IsReadOnly = false;
				colvarNoiTru.DefaultSetting = @"";
				colvarNoiTru.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNoiTru);
				
				TableSchema.TableColumn colvarTongTien = new TableSchema.TableColumn(schema);
				colvarTongTien.ColumnName = "tong_tien";
				colvarTongTien.DataType = DbType.Decimal;
				colvarTongTien.MaxLength = 0;
				colvarTongTien.AutoIncrement = false;
				colvarTongTien.IsNullable = false;
				colvarTongTien.IsPrimaryKey = false;
				colvarTongTien.IsForeignKey = false;
				colvarTongTien.IsReadOnly = false;
				colvarTongTien.DefaultSetting = @"";
				colvarTongTien.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTongTien);
				
				TableSchema.TableColumn colvarSoTien = new TableSchema.TableColumn(schema);
				colvarSoTien.ColumnName = "so_tien";
				colvarSoTien.DataType = DbType.Decimal;
				colvarSoTien.MaxLength = 0;
				colvarSoTien.AutoIncrement = false;
				colvarSoTien.IsNullable = false;
				colvarSoTien.IsPrimaryKey = false;
				colvarSoTien.IsForeignKey = false;
				colvarSoTien.IsReadOnly = false;
				colvarSoTien.DefaultSetting = @"";
				colvarSoTien.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoTien);
				
				TableSchema.TableColumn colvarNguoiTao = new TableSchema.TableColumn(schema);
				colvarNguoiTao.ColumnName = "nguoi_tao";
				colvarNguoiTao.DataType = DbType.String;
				colvarNguoiTao.MaxLength = 30;
				colvarNguoiTao.AutoIncrement = false;
				colvarNguoiTao.IsNullable = false;
				colvarNguoiTao.IsPrimaryKey = false;
				colvarNguoiTao.IsForeignKey = false;
				colvarNguoiTao.IsReadOnly = false;
				colvarNguoiTao.DefaultSetting = @"";
				colvarNguoiTao.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNguoiTao);
				
				TableSchema.TableColumn colvarNgayTao = new TableSchema.TableColumn(schema);
				colvarNgayTao.ColumnName = "ngay_tao";
				colvarNgayTao.DataType = DbType.DateTime;
				colvarNgayTao.MaxLength = 0;
				colvarNgayTao.AutoIncrement = false;
				colvarNgayTao.IsNullable = false;
				colvarNgayTao.IsPrimaryKey = false;
				colvarNgayTao.IsForeignKey = false;
				colvarNgayTao.IsReadOnly = false;
				colvarNgayTao.DefaultSetting = @"";
				colvarNgayTao.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayTao);
				
				TableSchema.TableColumn colvarNguoiSua = new TableSchema.TableColumn(schema);
				colvarNguoiSua.ColumnName = "nguoi_sua";
				colvarNguoiSua.DataType = DbType.String;
				colvarNguoiSua.MaxLength = 30;
				colvarNguoiSua.AutoIncrement = false;
				colvarNguoiSua.IsNullable = true;
				colvarNguoiSua.IsPrimaryKey = false;
				colvarNguoiSua.IsForeignKey = false;
				colvarNguoiSua.IsReadOnly = false;
				colvarNguoiSua.DefaultSetting = @"";
				colvarNguoiSua.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNguoiSua);
				
				TableSchema.TableColumn colvarNgaySua = new TableSchema.TableColumn(schema);
				colvarNgaySua.ColumnName = "ngay_sua";
				colvarNgaySua.DataType = DbType.DateTime;
				colvarNgaySua.MaxLength = 0;
				colvarNgaySua.AutoIncrement = false;
				colvarNgaySua.IsNullable = true;
				colvarNgaySua.IsPrimaryKey = false;
				colvarNgaySua.IsForeignKey = false;
				colvarNgaySua.IsReadOnly = false;
				colvarNgaySua.DefaultSetting = @"";
				colvarNgaySua.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgaySua);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("kcb_thanhtoan_phanbotheoPTTT",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("IdThanhtoan")]
		[Bindable(true)]
		public long IdThanhtoan 
		{
			get { return GetColumnValue<long>(Columns.IdThanhtoan); }
			set { SetColumnValue(Columns.IdThanhtoan, value); }
		}
		  
		[XmlAttribute("MaPttt")]
		[Bindable(true)]
		public string MaPttt 
		{
			get { return GetColumnValue<string>(Columns.MaPttt); }
			set { SetColumnValue(Columns.MaPttt, value); }
		}
		  
		[XmlAttribute("IdBenhnhan")]
		[Bindable(true)]
		public long IdBenhnhan 
		{
			get { return GetColumnValue<long>(Columns.IdBenhnhan); }
			set { SetColumnValue(Columns.IdBenhnhan, value); }
		}
		  
		[XmlAttribute("MaLuotkham")]
		[Bindable(true)]
		public string MaLuotkham 
		{
			get { return GetColumnValue<string>(Columns.MaLuotkham); }
			set { SetColumnValue(Columns.MaLuotkham, value); }
		}
		  
		[XmlAttribute("NoiTru")]
		[Bindable(true)]
		public byte NoiTru 
		{
			get { return GetColumnValue<byte>(Columns.NoiTru); }
			set { SetColumnValue(Columns.NoiTru, value); }
		}
		  
		[XmlAttribute("TongTien")]
		[Bindable(true)]
		public decimal TongTien 
		{
			get { return GetColumnValue<decimal>(Columns.TongTien); }
			set { SetColumnValue(Columns.TongTien, value); }
		}
		  
		[XmlAttribute("SoTien")]
		[Bindable(true)]
		public decimal SoTien 
		{
			get { return GetColumnValue<decimal>(Columns.SoTien); }
			set { SetColumnValue(Columns.SoTien, value); }
		}
		  
		[XmlAttribute("NguoiTao")]
		[Bindable(true)]
		public string NguoiTao 
		{
			get { return GetColumnValue<string>(Columns.NguoiTao); }
			set { SetColumnValue(Columns.NguoiTao, value); }
		}
		  
		[XmlAttribute("NgayTao")]
		[Bindable(true)]
		public DateTime NgayTao 
		{
			get { return GetColumnValue<DateTime>(Columns.NgayTao); }
			set { SetColumnValue(Columns.NgayTao, value); }
		}
		  
		[XmlAttribute("NguoiSua")]
		[Bindable(true)]
		public string NguoiSua 
		{
			get { return GetColumnValue<string>(Columns.NguoiSua); }
			set { SetColumnValue(Columns.NguoiSua, value); }
		}
		  
		[XmlAttribute("NgaySua")]
		[Bindable(true)]
		public DateTime? NgaySua 
		{
			get { return GetColumnValue<DateTime?>(Columns.NgaySua); }
			set { SetColumnValue(Columns.NgaySua, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(long varIdThanhtoan,string varMaPttt,long varIdBenhnhan,string varMaLuotkham,byte varNoiTru,decimal varTongTien,decimal varSoTien,string varNguoiTao,DateTime varNgayTao,string varNguoiSua,DateTime? varNgaySua)
		{
			KcbThanhtoanPhanbotheoPTTT item = new KcbThanhtoanPhanbotheoPTTT();
			
			item.IdThanhtoan = varIdThanhtoan;
			
			item.MaPttt = varMaPttt;
			
			item.IdBenhnhan = varIdBenhnhan;
			
			item.MaLuotkham = varMaLuotkham;
			
			item.NoiTru = varNoiTru;
			
			item.TongTien = varTongTien;
			
			item.SoTien = varSoTien;
			
			item.NguoiTao = varNguoiTao;
			
			item.NgayTao = varNgayTao;
			
			item.NguoiSua = varNguoiSua;
			
			item.NgaySua = varNgaySua;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(long varIdThanhtoan,string varMaPttt,long varIdBenhnhan,string varMaLuotkham,byte varNoiTru,decimal varTongTien,decimal varSoTien,string varNguoiTao,DateTime varNgayTao,string varNguoiSua,DateTime? varNgaySua)
		{
			KcbThanhtoanPhanbotheoPTTT item = new KcbThanhtoanPhanbotheoPTTT();
			
				item.IdThanhtoan = varIdThanhtoan;
			
				item.MaPttt = varMaPttt;
			
				item.IdBenhnhan = varIdBenhnhan;
			
				item.MaLuotkham = varMaLuotkham;
			
				item.NoiTru = varNoiTru;
			
				item.TongTien = varTongTien;
			
				item.SoTien = varSoTien;
			
				item.NguoiTao = varNguoiTao;
			
				item.NgayTao = varNgayTao;
			
				item.NguoiSua = varNguoiSua;
			
				item.NgaySua = varNgaySua;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdThanhtoanColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn MaPtttColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn IdBenhnhanColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn MaLuotkhamColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn NoiTruColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn TongTienColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn SoTienColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiTaoColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayTaoColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiSuaColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn NgaySuaColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string IdThanhtoan = @"id_thanhtoan";
			 public static string MaPttt = @"ma_pttt";
			 public static string IdBenhnhan = @"id_benhnhan";
			 public static string MaLuotkham = @"ma_luotkham";
			 public static string NoiTru = @"noi_tru";
			 public static string TongTien = @"tong_tien";
			 public static string SoTien = @"so_tien";
			 public static string NguoiTao = @"nguoi_tao";
			 public static string NgayTao = @"ngay_tao";
			 public static string NguoiSua = @"nguoi_sua";
			 public static string NgaySua = @"ngay_sua";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
