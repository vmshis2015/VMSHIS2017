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
	/// Strongly-typed collection for the DmucChung class.
	/// </summary>
    [Serializable]
	public partial class DmucChungCollection : ActiveList<DmucChung, DmucChungCollection>
	{	   
		public DmucChungCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>DmucChungCollection</returns>
		public DmucChungCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                DmucChung o = this[i];
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
	/// This is an ActiveRecord class which wraps the dmuc_chung table.
	/// </summary>
	[Serializable]
	public partial class DmucChung : ActiveRecord<DmucChung>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public DmucChung()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public DmucChung(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public DmucChung(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public DmucChung(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("dmuc_chung", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarMa = new TableSchema.TableColumn(schema);
				colvarMa.ColumnName = "MA";
				colvarMa.DataType = DbType.String;
				colvarMa.MaxLength = 50;
				colvarMa.AutoIncrement = false;
				colvarMa.IsNullable = false;
				colvarMa.IsPrimaryKey = true;
				colvarMa.IsForeignKey = false;
				colvarMa.IsReadOnly = false;
				colvarMa.DefaultSetting = @"";
				colvarMa.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMa);
				
				TableSchema.TableColumn colvarLoai = new TableSchema.TableColumn(schema);
				colvarLoai.ColumnName = "LOAI";
				colvarLoai.DataType = DbType.String;
				colvarLoai.MaxLength = 50;
				colvarLoai.AutoIncrement = false;
				colvarLoai.IsNullable = false;
				colvarLoai.IsPrimaryKey = true;
				colvarLoai.IsForeignKey = false;
				colvarLoai.IsReadOnly = false;
				colvarLoai.DefaultSetting = @"";
				colvarLoai.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLoai);
				
				TableSchema.TableColumn colvarTen = new TableSchema.TableColumn(schema);
				colvarTen.ColumnName = "TEN";
				colvarTen.DataType = DbType.String;
				colvarTen.MaxLength = 255;
				colvarTen.AutoIncrement = false;
				colvarTen.IsNullable = false;
				colvarTen.IsPrimaryKey = false;
				colvarTen.IsForeignKey = false;
				colvarTen.IsReadOnly = false;
				colvarTen.DefaultSetting = @"";
				colvarTen.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTen);
				
				TableSchema.TableColumn colvarSttHthi = new TableSchema.TableColumn(schema);
				colvarSttHthi.ColumnName = "STT_HTHI";
				colvarSttHthi.DataType = DbType.Int32;
				colvarSttHthi.MaxLength = 0;
				colvarSttHthi.AutoIncrement = false;
				colvarSttHthi.IsNullable = false;
				colvarSttHthi.IsPrimaryKey = false;
				colvarSttHthi.IsForeignKey = false;
				colvarSttHthi.IsReadOnly = false;
				colvarSttHthi.DefaultSetting = @"";
				colvarSttHthi.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSttHthi);
				
				TableSchema.TableColumn colvarTrangThai = new TableSchema.TableColumn(schema);
				colvarTrangThai.ColumnName = "TRANG_THAI";
				colvarTrangThai.DataType = DbType.Int32;
				colvarTrangThai.MaxLength = 0;
				colvarTrangThai.AutoIncrement = false;
				colvarTrangThai.IsNullable = false;
				colvarTrangThai.IsPrimaryKey = false;
				colvarTrangThai.IsForeignKey = false;
				colvarTrangThai.IsReadOnly = false;
				colvarTrangThai.DefaultSetting = @"";
				colvarTrangThai.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTrangThai);
				
				TableSchema.TableColumn colvarTrangthaiMacdinh = new TableSchema.TableColumn(schema);
				colvarTrangthaiMacdinh.ColumnName = "TRANGTHAI_MACDINH";
				colvarTrangthaiMacdinh.DataType = DbType.Byte;
				colvarTrangthaiMacdinh.MaxLength = 0;
				colvarTrangthaiMacdinh.AutoIncrement = false;
				colvarTrangthaiMacdinh.IsNullable = true;
				colvarTrangthaiMacdinh.IsPrimaryKey = false;
				colvarTrangthaiMacdinh.IsForeignKey = false;
				colvarTrangthaiMacdinh.IsReadOnly = false;
				colvarTrangthaiMacdinh.DefaultSetting = @"";
				colvarTrangthaiMacdinh.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTrangthaiMacdinh);
				
				TableSchema.TableColumn colvarVietTat = new TableSchema.TableColumn(schema);
				colvarVietTat.ColumnName = "VIET_TAT";
				colvarVietTat.DataType = DbType.String;
				colvarVietTat.MaxLength = 10;
				colvarVietTat.AutoIncrement = false;
				colvarVietTat.IsNullable = true;
				colvarVietTat.IsPrimaryKey = false;
				colvarVietTat.IsForeignKey = false;
				colvarVietTat.IsReadOnly = false;
				colvarVietTat.DefaultSetting = @"";
				colvarVietTat.ForeignKeyTableName = "";
				schema.Columns.Add(colvarVietTat);
				
				TableSchema.TableColumn colvarMotaThem = new TableSchema.TableColumn(schema);
				colvarMotaThem.ColumnName = "MOTA_THEM";
				colvarMotaThem.DataType = DbType.String;
				colvarMotaThem.MaxLength = 255;
				colvarMotaThem.AutoIncrement = false;
				colvarMotaThem.IsNullable = true;
				colvarMotaThem.IsPrimaryKey = false;
				colvarMotaThem.IsForeignKey = false;
				colvarMotaThem.IsReadOnly = false;
				colvarMotaThem.DefaultSetting = @"";
				colvarMotaThem.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMotaThem);
				
				TableSchema.TableColumn colvarNguoiTao = new TableSchema.TableColumn(schema);
				colvarNguoiTao.ColumnName = "NGUOI_TAO";
				colvarNguoiTao.DataType = DbType.String;
				colvarNguoiTao.MaxLength = 30;
				colvarNguoiTao.AutoIncrement = false;
				colvarNguoiTao.IsNullable = true;
				colvarNguoiTao.IsPrimaryKey = false;
				colvarNguoiTao.IsForeignKey = false;
				colvarNguoiTao.IsReadOnly = false;
				colvarNguoiTao.DefaultSetting = @"";
				colvarNguoiTao.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNguoiTao);
				
				TableSchema.TableColumn colvarNgayTao = new TableSchema.TableColumn(schema);
				colvarNgayTao.ColumnName = "NGAY_TAO";
				colvarNgayTao.DataType = DbType.DateTime;
				colvarNgayTao.MaxLength = 0;
				colvarNgayTao.AutoIncrement = false;
				colvarNgayTao.IsNullable = true;
				colvarNgayTao.IsPrimaryKey = false;
				colvarNgayTao.IsForeignKey = false;
				colvarNgayTao.IsReadOnly = false;
				colvarNgayTao.DefaultSetting = @"";
				colvarNgayTao.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayTao);
				
				TableSchema.TableColumn colvarNguoiSua = new TableSchema.TableColumn(schema);
				colvarNguoiSua.ColumnName = "NGUOI_SUA";
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
				colvarNgaySua.ColumnName = "NGAY_SUA";
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
				DataService.Providers["ORM"].AddSchema("dmuc_chung",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("Ma")]
		[Bindable(true)]
		public string Ma 
		{
			get { return GetColumnValue<string>(Columns.Ma); }
			set { SetColumnValue(Columns.Ma, value); }
		}
		  
		[XmlAttribute("Loai")]
		[Bindable(true)]
		public string Loai 
		{
			get { return GetColumnValue<string>(Columns.Loai); }
			set { SetColumnValue(Columns.Loai, value); }
		}
		  
		[XmlAttribute("Ten")]
		[Bindable(true)]
		public string Ten 
		{
			get { return GetColumnValue<string>(Columns.Ten); }
			set { SetColumnValue(Columns.Ten, value); }
		}
		  
		[XmlAttribute("SttHthi")]
		[Bindable(true)]
		public int SttHthi 
		{
			get { return GetColumnValue<int>(Columns.SttHthi); }
			set { SetColumnValue(Columns.SttHthi, value); }
		}
		  
		[XmlAttribute("TrangThai")]
		[Bindable(true)]
		public int TrangThai 
		{
			get { return GetColumnValue<int>(Columns.TrangThai); }
			set { SetColumnValue(Columns.TrangThai, value); }
		}
		  
		[XmlAttribute("TrangthaiMacdinh")]
		[Bindable(true)]
		public byte? TrangthaiMacdinh 
		{
			get { return GetColumnValue<byte?>(Columns.TrangthaiMacdinh); }
			set { SetColumnValue(Columns.TrangthaiMacdinh, value); }
		}
		  
		[XmlAttribute("VietTat")]
		[Bindable(true)]
		public string VietTat 
		{
			get { return GetColumnValue<string>(Columns.VietTat); }
			set { SetColumnValue(Columns.VietTat, value); }
		}
		  
		[XmlAttribute("MotaThem")]
		[Bindable(true)]
		public string MotaThem 
		{
			get { return GetColumnValue<string>(Columns.MotaThem); }
			set { SetColumnValue(Columns.MotaThem, value); }
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
		public DateTime? NgayTao 
		{
			get { return GetColumnValue<DateTime?>(Columns.NgayTao); }
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
		public static void Insert(string varMa,string varLoai,string varTen,int varSttHthi,int varTrangThai,byte? varTrangthaiMacdinh,string varVietTat,string varMotaThem,string varNguoiTao,DateTime? varNgayTao,string varNguoiSua,DateTime? varNgaySua)
		{
			DmucChung item = new DmucChung();
			
			item.Ma = varMa;
			
			item.Loai = varLoai;
			
			item.Ten = varTen;
			
			item.SttHthi = varSttHthi;
			
			item.TrangThai = varTrangThai;
			
			item.TrangthaiMacdinh = varTrangthaiMacdinh;
			
			item.VietTat = varVietTat;
			
			item.MotaThem = varMotaThem;
			
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
		public static void Update(string varMa,string varLoai,string varTen,int varSttHthi,int varTrangThai,byte? varTrangthaiMacdinh,string varVietTat,string varMotaThem,string varNguoiTao,DateTime? varNgayTao,string varNguoiSua,DateTime? varNgaySua)
		{
			DmucChung item = new DmucChung();
			
				item.Ma = varMa;
			
				item.Loai = varLoai;
			
				item.Ten = varTen;
			
				item.SttHthi = varSttHthi;
			
				item.TrangThai = varTrangThai;
			
				item.TrangthaiMacdinh = varTrangthaiMacdinh;
			
				item.VietTat = varVietTat;
			
				item.MotaThem = varMotaThem;
			
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
        
        
        public static TableSchema.TableColumn MaColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn LoaiColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn TenColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn SttHthiColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn TrangThaiColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn TrangthaiMacdinhColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn VietTatColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn MotaThemColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiTaoColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayTaoColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiSuaColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn NgaySuaColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Ma = @"MA";
			 public static string Loai = @"LOAI";
			 public static string Ten = @"TEN";
			 public static string SttHthi = @"STT_HTHI";
			 public static string TrangThai = @"TRANG_THAI";
			 public static string TrangthaiMacdinh = @"TRANGTHAI_MACDINH";
			 public static string VietTat = @"VIET_TAT";
			 public static string MotaThem = @"MOTA_THEM";
			 public static string NguoiTao = @"NGUOI_TAO";
			 public static string NgayTao = @"NGAY_TAO";
			 public static string NguoiSua = @"NGUOI_SUA";
			 public static string NgaySua = @"NGAY_SUA";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
