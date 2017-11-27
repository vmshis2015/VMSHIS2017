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
	/// Strongly-typed collection for the TThuockho class.
	/// </summary>
    [Serializable]
	public partial class TThuockhoCollection : ActiveList<TThuockho, TThuockhoCollection>
	{	   
		public TThuockhoCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>TThuockhoCollection</returns>
		public TThuockhoCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                TThuockho o = this[i];
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
	/// This is an ActiveRecord class which wraps the t_thuockho table.
	/// </summary>
	[Serializable]
	public partial class TThuockho : ActiveRecord<TThuockho>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public TThuockho()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public TThuockho(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public TThuockho(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public TThuockho(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("t_thuockho", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarIdThuockho = new TableSchema.TableColumn(schema);
				colvarIdThuockho.ColumnName = "id_thuockho";
				colvarIdThuockho.DataType = DbType.Int64;
				colvarIdThuockho.MaxLength = 0;
				colvarIdThuockho.AutoIncrement = true;
				colvarIdThuockho.IsNullable = false;
				colvarIdThuockho.IsPrimaryKey = true;
				colvarIdThuockho.IsForeignKey = false;
				colvarIdThuockho.IsReadOnly = false;
				colvarIdThuockho.DefaultSetting = @"";
				colvarIdThuockho.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdThuockho);
				
				TableSchema.TableColumn colvarIdKho = new TableSchema.TableColumn(schema);
				colvarIdKho.ColumnName = "id_kho";
				colvarIdKho.DataType = DbType.Int32;
				colvarIdKho.MaxLength = 0;
				colvarIdKho.AutoIncrement = false;
				colvarIdKho.IsNullable = false;
				colvarIdKho.IsPrimaryKey = false;
				colvarIdKho.IsForeignKey = false;
				colvarIdKho.IsReadOnly = false;
				colvarIdKho.DefaultSetting = @"";
				colvarIdKho.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdKho);
				
				TableSchema.TableColumn colvarIdThuoc = new TableSchema.TableColumn(schema);
				colvarIdThuoc.ColumnName = "id_thuoc";
				colvarIdThuoc.DataType = DbType.Int32;
				colvarIdThuoc.MaxLength = 0;
				colvarIdThuoc.AutoIncrement = false;
				colvarIdThuoc.IsNullable = false;
				colvarIdThuoc.IsPrimaryKey = false;
				colvarIdThuoc.IsForeignKey = false;
				colvarIdThuoc.IsReadOnly = false;
				colvarIdThuoc.DefaultSetting = @"";
				colvarIdThuoc.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdThuoc);
				
				TableSchema.TableColumn colvarNgayHethan = new TableSchema.TableColumn(schema);
				colvarNgayHethan.ColumnName = "ngay_hethan";
				colvarNgayHethan.DataType = DbType.DateTime;
				colvarNgayHethan.MaxLength = 0;
				colvarNgayHethan.AutoIncrement = false;
				colvarNgayHethan.IsNullable = false;
				colvarNgayHethan.IsPrimaryKey = false;
				colvarNgayHethan.IsForeignKey = false;
				colvarNgayHethan.IsReadOnly = false;
				colvarNgayHethan.DefaultSetting = @"";
				colvarNgayHethan.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayHethan);
				
				TableSchema.TableColumn colvarGiaNhap = new TableSchema.TableColumn(schema);
				colvarGiaNhap.ColumnName = "gia_nhap";
				colvarGiaNhap.DataType = DbType.Decimal;
				colvarGiaNhap.MaxLength = 0;
				colvarGiaNhap.AutoIncrement = false;
				colvarGiaNhap.IsNullable = false;
				colvarGiaNhap.IsPrimaryKey = false;
				colvarGiaNhap.IsForeignKey = false;
				colvarGiaNhap.IsReadOnly = false;
				colvarGiaNhap.DefaultSetting = @"";
				colvarGiaNhap.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGiaNhap);
				
				TableSchema.TableColumn colvarGiaBan = new TableSchema.TableColumn(schema);
				colvarGiaBan.ColumnName = "gia_ban";
				colvarGiaBan.DataType = DbType.Decimal;
				colvarGiaBan.MaxLength = 0;
				colvarGiaBan.AutoIncrement = false;
				colvarGiaBan.IsNullable = false;
				colvarGiaBan.IsPrimaryKey = false;
				colvarGiaBan.IsForeignKey = false;
				colvarGiaBan.IsReadOnly = false;
				
						colvarGiaBan.DefaultSetting = @"((0))";
				colvarGiaBan.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGiaBan);
				
				TableSchema.TableColumn colvarSoLuong = new TableSchema.TableColumn(schema);
				colvarSoLuong.ColumnName = "so_luong";
				colvarSoLuong.DataType = DbType.Decimal;
				colvarSoLuong.MaxLength = 0;
				colvarSoLuong.AutoIncrement = false;
				colvarSoLuong.IsNullable = false;
				colvarSoLuong.IsPrimaryKey = false;
				colvarSoLuong.IsForeignKey = false;
				colvarSoLuong.IsReadOnly = false;
				
						colvarSoLuong.DefaultSetting = @"((0))";
				colvarSoLuong.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoLuong);
				
				TableSchema.TableColumn colvarVat = new TableSchema.TableColumn(schema);
				colvarVat.ColumnName = "vat";
				colvarVat.DataType = DbType.Decimal;
				colvarVat.MaxLength = 0;
				colvarVat.AutoIncrement = false;
				colvarVat.IsNullable = false;
				colvarVat.IsPrimaryKey = false;
				colvarVat.IsForeignKey = false;
				colvarVat.IsReadOnly = false;
				colvarVat.DefaultSetting = @"";
				colvarVat.ForeignKeyTableName = "";
				schema.Columns.Add(colvarVat);
				
				TableSchema.TableColumn colvarSoLo = new TableSchema.TableColumn(schema);
				colvarSoLo.ColumnName = "so_lo";
				colvarSoLo.DataType = DbType.String;
				colvarSoLo.MaxLength = 20;
				colvarSoLo.AutoIncrement = false;
				colvarSoLo.IsNullable = true;
				colvarSoLo.IsPrimaryKey = false;
				colvarSoLo.IsForeignKey = false;
				colvarSoLo.IsReadOnly = false;
				colvarSoLo.DefaultSetting = @"";
				colvarSoLo.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoLo);
				
				TableSchema.TableColumn colvarMaNhacungcap = new TableSchema.TableColumn(schema);
				colvarMaNhacungcap.ColumnName = "ma_nhacungcap";
				colvarMaNhacungcap.DataType = DbType.String;
				colvarMaNhacungcap.MaxLength = 20;
				colvarMaNhacungcap.AutoIncrement = false;
				colvarMaNhacungcap.IsNullable = true;
				colvarMaNhacungcap.IsPrimaryKey = false;
				colvarMaNhacungcap.IsForeignKey = false;
				colvarMaNhacungcap.IsReadOnly = false;
				colvarMaNhacungcap.DefaultSetting = @"";
				colvarMaNhacungcap.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaNhacungcap);
				
				TableSchema.TableColumn colvarSttBan = new TableSchema.TableColumn(schema);
				colvarSttBan.ColumnName = "stt_ban";
				colvarSttBan.DataType = DbType.Int32;
				colvarSttBan.MaxLength = 0;
				colvarSttBan.AutoIncrement = false;
				colvarSttBan.IsNullable = true;
				colvarSttBan.IsPrimaryKey = false;
				colvarSttBan.IsForeignKey = false;
				colvarSttBan.IsReadOnly = false;
				colvarSttBan.DefaultSetting = @"";
				colvarSttBan.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSttBan);
				
				TableSchema.TableColumn colvarIdChuyen = new TableSchema.TableColumn(schema);
				colvarIdChuyen.ColumnName = "id_chuyen";
				colvarIdChuyen.DataType = DbType.Int64;
				colvarIdChuyen.MaxLength = 0;
				colvarIdChuyen.AutoIncrement = false;
				colvarIdChuyen.IsNullable = true;
				colvarIdChuyen.IsPrimaryKey = false;
				colvarIdChuyen.IsForeignKey = false;
				colvarIdChuyen.IsReadOnly = false;
				colvarIdChuyen.DefaultSetting = @"";
				colvarIdChuyen.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdChuyen);
				
				TableSchema.TableColumn colvarNgayNhap = new TableSchema.TableColumn(schema);
				colvarNgayNhap.ColumnName = "ngay_nhap";
				colvarNgayNhap.DataType = DbType.DateTime;
				colvarNgayNhap.MaxLength = 0;
				colvarNgayNhap.AutoIncrement = false;
				colvarNgayNhap.IsNullable = true;
				colvarNgayNhap.IsPrimaryKey = false;
				colvarNgayNhap.IsForeignKey = false;
				colvarNgayNhap.IsReadOnly = false;
				colvarNgayNhap.DefaultSetting = @"";
				colvarNgayNhap.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayNhap);
				
				TableSchema.TableColumn colvarGiaBhyt = new TableSchema.TableColumn(schema);
				colvarGiaBhyt.ColumnName = "gia_bhyt";
				colvarGiaBhyt.DataType = DbType.Decimal;
				colvarGiaBhyt.MaxLength = 0;
				colvarGiaBhyt.AutoIncrement = false;
				colvarGiaBhyt.IsNullable = true;
				colvarGiaBhyt.IsPrimaryKey = false;
				colvarGiaBhyt.IsForeignKey = false;
				colvarGiaBhyt.IsReadOnly = false;
				colvarGiaBhyt.DefaultSetting = @"";
				colvarGiaBhyt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGiaBhyt);
				
				TableSchema.TableColumn colvarPhuthuDungtuyen = new TableSchema.TableColumn(schema);
				colvarPhuthuDungtuyen.ColumnName = "phuthu_dungtuyen";
				colvarPhuthuDungtuyen.DataType = DbType.Decimal;
				colvarPhuthuDungtuyen.MaxLength = 0;
				colvarPhuthuDungtuyen.AutoIncrement = false;
				colvarPhuthuDungtuyen.IsNullable = true;
				colvarPhuthuDungtuyen.IsPrimaryKey = false;
				colvarPhuthuDungtuyen.IsForeignKey = false;
				colvarPhuthuDungtuyen.IsReadOnly = false;
				colvarPhuthuDungtuyen.DefaultSetting = @"";
				colvarPhuthuDungtuyen.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhuthuDungtuyen);
				
				TableSchema.TableColumn colvarPhuthuTraituyen = new TableSchema.TableColumn(schema);
				colvarPhuthuTraituyen.ColumnName = "phuthu_traituyen";
				colvarPhuthuTraituyen.DataType = DbType.Decimal;
				colvarPhuthuTraituyen.MaxLength = 0;
				colvarPhuthuTraituyen.AutoIncrement = false;
				colvarPhuthuTraituyen.IsNullable = true;
				colvarPhuthuTraituyen.IsPrimaryKey = false;
				colvarPhuthuTraituyen.IsForeignKey = false;
				colvarPhuthuTraituyen.IsReadOnly = false;
				colvarPhuthuTraituyen.DefaultSetting = @"";
				colvarPhuthuTraituyen.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhuthuTraituyen);
				
				TableSchema.TableColumn colvarChophepKetutruc = new TableSchema.TableColumn(schema);
				colvarChophepKetutruc.ColumnName = "chophep_ketutruc";
				colvarChophepKetutruc.DataType = DbType.Byte;
				colvarChophepKetutruc.MaxLength = 0;
				colvarChophepKetutruc.AutoIncrement = false;
				colvarChophepKetutruc.IsNullable = true;
				colvarChophepKetutruc.IsPrimaryKey = false;
				colvarChophepKetutruc.IsForeignKey = false;
				colvarChophepKetutruc.IsReadOnly = false;
				colvarChophepKetutruc.DefaultSetting = @"";
				colvarChophepKetutruc.ForeignKeyTableName = "";
				schema.Columns.Add(colvarChophepKetutruc);
				
				TableSchema.TableColumn colvarChophepKedon = new TableSchema.TableColumn(schema);
				colvarChophepKedon.ColumnName = "chophep_kedon";
				colvarChophepKedon.DataType = DbType.Byte;
				colvarChophepKedon.MaxLength = 0;
				colvarChophepKedon.AutoIncrement = false;
				colvarChophepKedon.IsNullable = true;
				colvarChophepKedon.IsPrimaryKey = false;
				colvarChophepKedon.IsForeignKey = false;
				colvarChophepKedon.IsReadOnly = false;
				colvarChophepKedon.DefaultSetting = @"";
				colvarChophepKedon.ForeignKeyTableName = "";
				schema.Columns.Add(colvarChophepKedon);
				
				TableSchema.TableColumn colvarKieuThuocvattu = new TableSchema.TableColumn(schema);
				colvarKieuThuocvattu.ColumnName = "kieu_thuocvattu";
				colvarKieuThuocvattu.DataType = DbType.String;
				colvarKieuThuocvattu.MaxLength = 10;
				colvarKieuThuocvattu.AutoIncrement = false;
				colvarKieuThuocvattu.IsNullable = true;
				colvarKieuThuocvattu.IsPrimaryKey = false;
				colvarKieuThuocvattu.IsForeignKey = false;
				colvarKieuThuocvattu.IsReadOnly = false;
				colvarKieuThuocvattu.DefaultSetting = @"";
				colvarKieuThuocvattu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarKieuThuocvattu);
				
				TableSchema.TableColumn colvarSoDky = new TableSchema.TableColumn(schema);
				colvarSoDky.ColumnName = "so_dky";
				colvarSoDky.DataType = DbType.String;
				colvarSoDky.MaxLength = 30;
				colvarSoDky.AutoIncrement = false;
				colvarSoDky.IsNullable = true;
				colvarSoDky.IsPrimaryKey = false;
				colvarSoDky.IsForeignKey = false;
				colvarSoDky.IsReadOnly = false;
				colvarSoDky.DefaultSetting = @"";
				colvarSoDky.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoDky);
				
				TableSchema.TableColumn colvarSoQdinhthau = new TableSchema.TableColumn(schema);
				colvarSoQdinhthau.ColumnName = "so_qdinhthau";
				colvarSoQdinhthau.DataType = DbType.String;
				colvarSoQdinhthau.MaxLength = 30;
				colvarSoQdinhthau.AutoIncrement = false;
				colvarSoQdinhthau.IsNullable = true;
				colvarSoQdinhthau.IsPrimaryKey = false;
				colvarSoQdinhthau.IsForeignKey = false;
				colvarSoQdinhthau.IsReadOnly = false;
				colvarSoQdinhthau.DefaultSetting = @"";
				colvarSoQdinhthau.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoQdinhthau);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("t_thuockho",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("IdThuockho")]
		[Bindable(true)]
		public long IdThuockho 
		{
			get { return GetColumnValue<long>(Columns.IdThuockho); }
			set { SetColumnValue(Columns.IdThuockho, value); }
		}
		  
		[XmlAttribute("IdKho")]
		[Bindable(true)]
		public int IdKho 
		{
			get { return GetColumnValue<int>(Columns.IdKho); }
			set { SetColumnValue(Columns.IdKho, value); }
		}
		  
		[XmlAttribute("IdThuoc")]
		[Bindable(true)]
		public int IdThuoc 
		{
			get { return GetColumnValue<int>(Columns.IdThuoc); }
			set { SetColumnValue(Columns.IdThuoc, value); }
		}
		  
		[XmlAttribute("NgayHethan")]
		[Bindable(true)]
		public DateTime NgayHethan 
		{
			get { return GetColumnValue<DateTime>(Columns.NgayHethan); }
			set { SetColumnValue(Columns.NgayHethan, value); }
		}
		  
		[XmlAttribute("GiaNhap")]
		[Bindable(true)]
		public decimal GiaNhap 
		{
			get { return GetColumnValue<decimal>(Columns.GiaNhap); }
			set { SetColumnValue(Columns.GiaNhap, value); }
		}
		  
		[XmlAttribute("GiaBan")]
		[Bindable(true)]
		public decimal GiaBan 
		{
			get { return GetColumnValue<decimal>(Columns.GiaBan); }
			set { SetColumnValue(Columns.GiaBan, value); }
		}
		  
		[XmlAttribute("SoLuong")]
		[Bindable(true)]
		public decimal SoLuong 
		{
			get { return GetColumnValue<decimal>(Columns.SoLuong); }
			set { SetColumnValue(Columns.SoLuong, value); }
		}
		  
		[XmlAttribute("Vat")]
		[Bindable(true)]
		public decimal Vat 
		{
			get { return GetColumnValue<decimal>(Columns.Vat); }
			set { SetColumnValue(Columns.Vat, value); }
		}
		  
		[XmlAttribute("SoLo")]
		[Bindable(true)]
		public string SoLo 
		{
			get { return GetColumnValue<string>(Columns.SoLo); }
			set { SetColumnValue(Columns.SoLo, value); }
		}
		  
		[XmlAttribute("MaNhacungcap")]
		[Bindable(true)]
		public string MaNhacungcap 
		{
			get { return GetColumnValue<string>(Columns.MaNhacungcap); }
			set { SetColumnValue(Columns.MaNhacungcap, value); }
		}
		  
		[XmlAttribute("SttBan")]
		[Bindable(true)]
		public int? SttBan 
		{
			get { return GetColumnValue<int?>(Columns.SttBan); }
			set { SetColumnValue(Columns.SttBan, value); }
		}
		  
		[XmlAttribute("IdChuyen")]
		[Bindable(true)]
		public long? IdChuyen 
		{
			get { return GetColumnValue<long?>(Columns.IdChuyen); }
			set { SetColumnValue(Columns.IdChuyen, value); }
		}
		  
		[XmlAttribute("NgayNhap")]
		[Bindable(true)]
		public DateTime? NgayNhap 
		{
			get { return GetColumnValue<DateTime?>(Columns.NgayNhap); }
			set { SetColumnValue(Columns.NgayNhap, value); }
		}
		  
		[XmlAttribute("GiaBhyt")]
		[Bindable(true)]
		public decimal? GiaBhyt 
		{
			get { return GetColumnValue<decimal?>(Columns.GiaBhyt); }
			set { SetColumnValue(Columns.GiaBhyt, value); }
		}
		  
		[XmlAttribute("PhuthuDungtuyen")]
		[Bindable(true)]
		public decimal? PhuthuDungtuyen 
		{
			get { return GetColumnValue<decimal?>(Columns.PhuthuDungtuyen); }
			set { SetColumnValue(Columns.PhuthuDungtuyen, value); }
		}
		  
		[XmlAttribute("PhuthuTraituyen")]
		[Bindable(true)]
		public decimal? PhuthuTraituyen 
		{
			get { return GetColumnValue<decimal?>(Columns.PhuthuTraituyen); }
			set { SetColumnValue(Columns.PhuthuTraituyen, value); }
		}
		  
		[XmlAttribute("ChophepKetutruc")]
		[Bindable(true)]
		public byte? ChophepKetutruc 
		{
			get { return GetColumnValue<byte?>(Columns.ChophepKetutruc); }
			set { SetColumnValue(Columns.ChophepKetutruc, value); }
		}
		  
		[XmlAttribute("ChophepKedon")]
		[Bindable(true)]
		public byte? ChophepKedon 
		{
			get { return GetColumnValue<byte?>(Columns.ChophepKedon); }
			set { SetColumnValue(Columns.ChophepKedon, value); }
		}
		  
		[XmlAttribute("KieuThuocvattu")]
		[Bindable(true)]
		public string KieuThuocvattu 
		{
			get { return GetColumnValue<string>(Columns.KieuThuocvattu); }
			set { SetColumnValue(Columns.KieuThuocvattu, value); }
		}
		  
		[XmlAttribute("SoDky")]
		[Bindable(true)]
		public string SoDky 
		{
			get { return GetColumnValue<string>(Columns.SoDky); }
			set { SetColumnValue(Columns.SoDky, value); }
		}
		  
		[XmlAttribute("SoQdinhthau")]
		[Bindable(true)]
		public string SoQdinhthau 
		{
			get { return GetColumnValue<string>(Columns.SoQdinhthau); }
			set { SetColumnValue(Columns.SoQdinhthau, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(int varIdKho,int varIdThuoc,DateTime varNgayHethan,decimal varGiaNhap,decimal varGiaBan,decimal varSoLuong,decimal varVat,string varSoLo,string varMaNhacungcap,int? varSttBan,long? varIdChuyen,DateTime? varNgayNhap,decimal? varGiaBhyt,decimal? varPhuthuDungtuyen,decimal? varPhuthuTraituyen,byte? varChophepKetutruc,byte? varChophepKedon,string varKieuThuocvattu,string varSoDky,string varSoQdinhthau)
		{
			TThuockho item = new TThuockho();
			
			item.IdKho = varIdKho;
			
			item.IdThuoc = varIdThuoc;
			
			item.NgayHethan = varNgayHethan;
			
			item.GiaNhap = varGiaNhap;
			
			item.GiaBan = varGiaBan;
			
			item.SoLuong = varSoLuong;
			
			item.Vat = varVat;
			
			item.SoLo = varSoLo;
			
			item.MaNhacungcap = varMaNhacungcap;
			
			item.SttBan = varSttBan;
			
			item.IdChuyen = varIdChuyen;
			
			item.NgayNhap = varNgayNhap;
			
			item.GiaBhyt = varGiaBhyt;
			
			item.PhuthuDungtuyen = varPhuthuDungtuyen;
			
			item.PhuthuTraituyen = varPhuthuTraituyen;
			
			item.ChophepKetutruc = varChophepKetutruc;
			
			item.ChophepKedon = varChophepKedon;
			
			item.KieuThuocvattu = varKieuThuocvattu;
			
			item.SoDky = varSoDky;
			
			item.SoQdinhthau = varSoQdinhthau;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(long varIdThuockho,int varIdKho,int varIdThuoc,DateTime varNgayHethan,decimal varGiaNhap,decimal varGiaBan,decimal varSoLuong,decimal varVat,string varSoLo,string varMaNhacungcap,int? varSttBan,long? varIdChuyen,DateTime? varNgayNhap,decimal? varGiaBhyt,decimal? varPhuthuDungtuyen,decimal? varPhuthuTraituyen,byte? varChophepKetutruc,byte? varChophepKedon,string varKieuThuocvattu,string varSoDky,string varSoQdinhthau)
		{
			TThuockho item = new TThuockho();
			
				item.IdThuockho = varIdThuockho;
			
				item.IdKho = varIdKho;
			
				item.IdThuoc = varIdThuoc;
			
				item.NgayHethan = varNgayHethan;
			
				item.GiaNhap = varGiaNhap;
			
				item.GiaBan = varGiaBan;
			
				item.SoLuong = varSoLuong;
			
				item.Vat = varVat;
			
				item.SoLo = varSoLo;
			
				item.MaNhacungcap = varMaNhacungcap;
			
				item.SttBan = varSttBan;
			
				item.IdChuyen = varIdChuyen;
			
				item.NgayNhap = varNgayNhap;
			
				item.GiaBhyt = varGiaBhyt;
			
				item.PhuthuDungtuyen = varPhuthuDungtuyen;
			
				item.PhuthuTraituyen = varPhuthuTraituyen;
			
				item.ChophepKetutruc = varChophepKetutruc;
			
				item.ChophepKedon = varChophepKedon;
			
				item.KieuThuocvattu = varKieuThuocvattu;
			
				item.SoDky = varSoDky;
			
				item.SoQdinhthau = varSoQdinhthau;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdThuockhoColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn IdKhoColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn IdThuocColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayHethanColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn GiaNhapColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn GiaBanColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn SoLuongColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn VatColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn SoLoColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn MaNhacungcapColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn SttBanColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn IdChuyenColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayNhapColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn GiaBhytColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn PhuthuDungtuyenColumn
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        public static TableSchema.TableColumn PhuthuTraituyenColumn
        {
            get { return Schema.Columns[15]; }
        }
        
        
        
        public static TableSchema.TableColumn ChophepKetutrucColumn
        {
            get { return Schema.Columns[16]; }
        }
        
        
        
        public static TableSchema.TableColumn ChophepKedonColumn
        {
            get { return Schema.Columns[17]; }
        }
        
        
        
        public static TableSchema.TableColumn KieuThuocvattuColumn
        {
            get { return Schema.Columns[18]; }
        }
        
        
        
        public static TableSchema.TableColumn SoDkyColumn
        {
            get { return Schema.Columns[19]; }
        }
        
        
        
        public static TableSchema.TableColumn SoQdinhthauColumn
        {
            get { return Schema.Columns[20]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string IdThuockho = @"id_thuockho";
			 public static string IdKho = @"id_kho";
			 public static string IdThuoc = @"id_thuoc";
			 public static string NgayHethan = @"ngay_hethan";
			 public static string GiaNhap = @"gia_nhap";
			 public static string GiaBan = @"gia_ban";
			 public static string SoLuong = @"so_luong";
			 public static string Vat = @"vat";
			 public static string SoLo = @"so_lo";
			 public static string MaNhacungcap = @"ma_nhacungcap";
			 public static string SttBan = @"stt_ban";
			 public static string IdChuyen = @"id_chuyen";
			 public static string NgayNhap = @"ngay_nhap";
			 public static string GiaBhyt = @"gia_bhyt";
			 public static string PhuthuDungtuyen = @"phuthu_dungtuyen";
			 public static string PhuthuTraituyen = @"phuthu_traituyen";
			 public static string ChophepKetutruc = @"chophep_ketutruc";
			 public static string ChophepKedon = @"chophep_kedon";
			 public static string KieuThuocvattu = @"kieu_thuocvattu";
			 public static string SoDky = @"so_dky";
			 public static string SoQdinhthau = @"so_qdinhthau";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}