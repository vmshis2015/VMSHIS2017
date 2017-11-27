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
	/// Strongly-typed collection for the TPhieutrathuocthua class.
	/// </summary>
    [Serializable]
	public partial class TPhieutrathuocthuaCollection : ActiveList<TPhieutrathuocthua, TPhieutrathuocthuaCollection>
	{	   
		public TPhieutrathuocthuaCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>TPhieutrathuocthuaCollection</returns>
		public TPhieutrathuocthuaCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                TPhieutrathuocthua o = this[i];
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
	/// This is an ActiveRecord class which wraps the t_phieutrathuocthua table.
	/// </summary>
	[Serializable]
	public partial class TPhieutrathuocthua : ActiveRecord<TPhieutrathuocthua>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public TPhieutrathuocthua()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public TPhieutrathuocthua(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public TPhieutrathuocthua(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public TPhieutrathuocthua(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("t_phieutrathuocthua", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarId = new TableSchema.TableColumn(schema);
				colvarId.ColumnName = "Id";
				colvarId.DataType = DbType.Int64;
				colvarId.MaxLength = 0;
				colvarId.AutoIncrement = true;
				colvarId.IsNullable = false;
				colvarId.IsPrimaryKey = true;
				colvarId.IsForeignKey = false;
				colvarId.IsReadOnly = false;
				colvarId.DefaultSetting = @"";
				colvarId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarId);
				
				TableSchema.TableColumn colvarMaPhieu = new TableSchema.TableColumn(schema);
				colvarMaPhieu.ColumnName = "ma_phieu";
				colvarMaPhieu.DataType = DbType.String;
				colvarMaPhieu.MaxLength = 10;
				colvarMaPhieu.AutoIncrement = false;
				colvarMaPhieu.IsNullable = false;
				colvarMaPhieu.IsPrimaryKey = false;
				colvarMaPhieu.IsForeignKey = false;
				colvarMaPhieu.IsReadOnly = false;
				colvarMaPhieu.DefaultSetting = @"";
				colvarMaPhieu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaPhieu);
				
				TableSchema.TableColumn colvarNgayLapphieu = new TableSchema.TableColumn(schema);
				colvarNgayLapphieu.ColumnName = "ngay_lapphieu";
				colvarNgayLapphieu.DataType = DbType.DateTime;
				colvarNgayLapphieu.MaxLength = 0;
				colvarNgayLapphieu.AutoIncrement = false;
				colvarNgayLapphieu.IsNullable = false;
				colvarNgayLapphieu.IsPrimaryKey = false;
				colvarNgayLapphieu.IsForeignKey = false;
				colvarNgayLapphieu.IsReadOnly = false;
				colvarNgayLapphieu.DefaultSetting = @"";
				colvarNgayLapphieu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayLapphieu);
				
				TableSchema.TableColumn colvarNguoiLapphieu = new TableSchema.TableColumn(schema);
				colvarNguoiLapphieu.ColumnName = "nguoi_lapphieu";
				colvarNguoiLapphieu.DataType = DbType.Int16;
				colvarNguoiLapphieu.MaxLength = 0;
				colvarNguoiLapphieu.AutoIncrement = false;
				colvarNguoiLapphieu.IsNullable = false;
				colvarNguoiLapphieu.IsPrimaryKey = false;
				colvarNguoiLapphieu.IsForeignKey = false;
				colvarNguoiLapphieu.IsReadOnly = false;
				colvarNguoiLapphieu.DefaultSetting = @"";
				colvarNguoiLapphieu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNguoiLapphieu);
				
				TableSchema.TableColumn colvarNgayTra = new TableSchema.TableColumn(schema);
				colvarNgayTra.ColumnName = "ngay_tra";
				colvarNgayTra.DataType = DbType.DateTime;
				colvarNgayTra.MaxLength = 0;
				colvarNgayTra.AutoIncrement = false;
				colvarNgayTra.IsNullable = true;
				colvarNgayTra.IsPrimaryKey = false;
				colvarNgayTra.IsForeignKey = false;
				colvarNgayTra.IsReadOnly = false;
				colvarNgayTra.DefaultSetting = @"";
				colvarNgayTra.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayTra);
				
				TableSchema.TableColumn colvarNguoiTra = new TableSchema.TableColumn(schema);
				colvarNguoiTra.ColumnName = "nguoi_tra";
				colvarNguoiTra.DataType = DbType.Int16;
				colvarNguoiTra.MaxLength = 0;
				colvarNguoiTra.AutoIncrement = false;
				colvarNguoiTra.IsNullable = true;
				colvarNguoiTra.IsPrimaryKey = false;
				colvarNguoiTra.IsForeignKey = false;
				colvarNguoiTra.IsReadOnly = false;
				colvarNguoiTra.DefaultSetting = @"";
				colvarNguoiTra.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNguoiTra);
				
				TableSchema.TableColumn colvarNguoiNhan = new TableSchema.TableColumn(schema);
				colvarNguoiNhan.ColumnName = "nguoi_nhan";
				colvarNguoiNhan.DataType = DbType.Int16;
				colvarNguoiNhan.MaxLength = 0;
				colvarNguoiNhan.AutoIncrement = false;
				colvarNguoiNhan.IsNullable = true;
				colvarNguoiNhan.IsPrimaryKey = false;
				colvarNguoiNhan.IsForeignKey = false;
				colvarNguoiNhan.IsReadOnly = false;
				colvarNguoiNhan.DefaultSetting = @"";
				colvarNguoiNhan.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNguoiNhan);
				
				TableSchema.TableColumn colvarIdKhoatra = new TableSchema.TableColumn(schema);
				colvarIdKhoatra.ColumnName = "id_khoatra";
				colvarIdKhoatra.DataType = DbType.Int16;
				colvarIdKhoatra.MaxLength = 0;
				colvarIdKhoatra.AutoIncrement = false;
				colvarIdKhoatra.IsNullable = false;
				colvarIdKhoatra.IsPrimaryKey = false;
				colvarIdKhoatra.IsForeignKey = false;
				colvarIdKhoatra.IsReadOnly = false;
				colvarIdKhoatra.DefaultSetting = @"";
				colvarIdKhoatra.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdKhoatra);
				
				TableSchema.TableColumn colvarIdKhonhan = new TableSchema.TableColumn(schema);
				colvarIdKhonhan.ColumnName = "id_khonhan";
				colvarIdKhonhan.DataType = DbType.Int16;
				colvarIdKhonhan.MaxLength = 0;
				colvarIdKhonhan.AutoIncrement = false;
				colvarIdKhonhan.IsNullable = false;
				colvarIdKhonhan.IsPrimaryKey = false;
				colvarIdKhonhan.IsForeignKey = false;
				colvarIdKhonhan.IsReadOnly = false;
				colvarIdKhonhan.DefaultSetting = @"";
				colvarIdKhonhan.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdKhonhan);
				
				TableSchema.TableColumn colvarKieuThuocVt = new TableSchema.TableColumn(schema);
				colvarKieuThuocVt.ColumnName = "kieu_thuoc_vt";
				colvarKieuThuocVt.DataType = DbType.String;
				colvarKieuThuocVt.MaxLength = 10;
				colvarKieuThuocVt.AutoIncrement = false;
				colvarKieuThuocVt.IsNullable = false;
				colvarKieuThuocVt.IsPrimaryKey = false;
				colvarKieuThuocVt.IsForeignKey = false;
				colvarKieuThuocVt.IsReadOnly = false;
				colvarKieuThuocVt.DefaultSetting = @"";
				colvarKieuThuocVt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarKieuThuocVt);
				
				TableSchema.TableColumn colvarTrangThai = new TableSchema.TableColumn(schema);
				colvarTrangThai.ColumnName = "trang_thai";
				colvarTrangThai.DataType = DbType.Byte;
				colvarTrangThai.MaxLength = 0;
				colvarTrangThai.AutoIncrement = false;
				colvarTrangThai.IsNullable = false;
				colvarTrangThai.IsPrimaryKey = false;
				colvarTrangThai.IsForeignKey = false;
				colvarTrangThai.IsReadOnly = false;
				colvarTrangThai.DefaultSetting = @"";
				colvarTrangThai.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTrangThai);
				
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
				DataService.Providers["ORM"].AddSchema("t_phieutrathuocthua",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("Id")]
		[Bindable(true)]
		public long Id 
		{
			get { return GetColumnValue<long>(Columns.Id); }
			set { SetColumnValue(Columns.Id, value); }
		}
		  
		[XmlAttribute("MaPhieu")]
		[Bindable(true)]
		public string MaPhieu 
		{
			get { return GetColumnValue<string>(Columns.MaPhieu); }
			set { SetColumnValue(Columns.MaPhieu, value); }
		}
		  
		[XmlAttribute("NgayLapphieu")]
		[Bindable(true)]
		public DateTime NgayLapphieu 
		{
			get { return GetColumnValue<DateTime>(Columns.NgayLapphieu); }
			set { SetColumnValue(Columns.NgayLapphieu, value); }
		}
		  
		[XmlAttribute("NguoiLapphieu")]
		[Bindable(true)]
		public short NguoiLapphieu 
		{
			get { return GetColumnValue<short>(Columns.NguoiLapphieu); }
			set { SetColumnValue(Columns.NguoiLapphieu, value); }
		}
		  
		[XmlAttribute("NgayTra")]
		[Bindable(true)]
		public DateTime? NgayTra 
		{
			get { return GetColumnValue<DateTime?>(Columns.NgayTra); }
			set { SetColumnValue(Columns.NgayTra, value); }
		}
		  
		[XmlAttribute("NguoiTra")]
		[Bindable(true)]
		public short? NguoiTra 
		{
			get { return GetColumnValue<short?>(Columns.NguoiTra); }
			set { SetColumnValue(Columns.NguoiTra, value); }
		}
		  
		[XmlAttribute("NguoiNhan")]
		[Bindable(true)]
		public short? NguoiNhan 
		{
			get { return GetColumnValue<short?>(Columns.NguoiNhan); }
			set { SetColumnValue(Columns.NguoiNhan, value); }
		}
		  
		[XmlAttribute("IdKhoatra")]
		[Bindable(true)]
		public short IdKhoatra 
		{
			get { return GetColumnValue<short>(Columns.IdKhoatra); }
			set { SetColumnValue(Columns.IdKhoatra, value); }
		}
		  
		[XmlAttribute("IdKhonhan")]
		[Bindable(true)]
		public short IdKhonhan 
		{
			get { return GetColumnValue<short>(Columns.IdKhonhan); }
			set { SetColumnValue(Columns.IdKhonhan, value); }
		}
		  
		[XmlAttribute("KieuThuocVt")]
		[Bindable(true)]
		public string KieuThuocVt 
		{
			get { return GetColumnValue<string>(Columns.KieuThuocVt); }
			set { SetColumnValue(Columns.KieuThuocVt, value); }
		}
		  
		[XmlAttribute("TrangThai")]
		[Bindable(true)]
		public byte TrangThai 
		{
			get { return GetColumnValue<byte>(Columns.TrangThai); }
			set { SetColumnValue(Columns.TrangThai, value); }
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
		public static void Insert(string varMaPhieu,DateTime varNgayLapphieu,short varNguoiLapphieu,DateTime? varNgayTra,short? varNguoiTra,short? varNguoiNhan,short varIdKhoatra,short varIdKhonhan,string varKieuThuocVt,byte varTrangThai,string varNguoiTao,DateTime varNgayTao,string varNguoiSua,DateTime? varNgaySua)
		{
			TPhieutrathuocthua item = new TPhieutrathuocthua();
			
			item.MaPhieu = varMaPhieu;
			
			item.NgayLapphieu = varNgayLapphieu;
			
			item.NguoiLapphieu = varNguoiLapphieu;
			
			item.NgayTra = varNgayTra;
			
			item.NguoiTra = varNguoiTra;
			
			item.NguoiNhan = varNguoiNhan;
			
			item.IdKhoatra = varIdKhoatra;
			
			item.IdKhonhan = varIdKhonhan;
			
			item.KieuThuocVt = varKieuThuocVt;
			
			item.TrangThai = varTrangThai;
			
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
		public static void Update(long varId,string varMaPhieu,DateTime varNgayLapphieu,short varNguoiLapphieu,DateTime? varNgayTra,short? varNguoiTra,short? varNguoiNhan,short varIdKhoatra,short varIdKhonhan,string varKieuThuocVt,byte varTrangThai,string varNguoiTao,DateTime varNgayTao,string varNguoiSua,DateTime? varNgaySua)
		{
			TPhieutrathuocthua item = new TPhieutrathuocthua();
			
				item.Id = varId;
			
				item.MaPhieu = varMaPhieu;
			
				item.NgayLapphieu = varNgayLapphieu;
			
				item.NguoiLapphieu = varNguoiLapphieu;
			
				item.NgayTra = varNgayTra;
			
				item.NguoiTra = varNguoiTra;
			
				item.NguoiNhan = varNguoiNhan;
			
				item.IdKhoatra = varIdKhoatra;
			
				item.IdKhonhan = varIdKhonhan;
			
				item.KieuThuocVt = varKieuThuocVt;
			
				item.TrangThai = varTrangThai;
			
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
        
        
        public static TableSchema.TableColumn IdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn MaPhieuColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayLapphieuColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiLapphieuColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayTraColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiTraColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiNhanColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn IdKhoatraColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn IdKhonhanColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn KieuThuocVtColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn TrangThaiColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiTaoColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayTaoColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiSuaColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn NgaySuaColumn
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Id = @"Id";
			 public static string MaPhieu = @"ma_phieu";
			 public static string NgayLapphieu = @"ngay_lapphieu";
			 public static string NguoiLapphieu = @"nguoi_lapphieu";
			 public static string NgayTra = @"ngay_tra";
			 public static string NguoiTra = @"nguoi_tra";
			 public static string NguoiNhan = @"nguoi_nhan";
			 public static string IdKhoatra = @"id_khoatra";
			 public static string IdKhonhan = @"id_khonhan";
			 public static string KieuThuocVt = @"kieu_thuoc_vt";
			 public static string TrangThai = @"trang_thai";
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