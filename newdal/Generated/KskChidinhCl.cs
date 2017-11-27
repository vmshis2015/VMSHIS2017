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
	/// Strongly-typed collection for the KskChidinhCl class.
	/// </summary>
    [Serializable]
	public partial class KskChidinhClCollection : ActiveList<KskChidinhCl, KskChidinhClCollection>
	{	   
		public KskChidinhClCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>KskChidinhClCollection</returns>
		public KskChidinhClCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                KskChidinhCl o = this[i];
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
	/// This is an ActiveRecord class which wraps the ksk_chidinh_cls table.
	/// </summary>
	[Serializable]
	public partial class KskChidinhCl : ActiveRecord<KskChidinhCl>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public KskChidinhCl()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public KskChidinhCl(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public KskChidinhCl(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public KskChidinhCl(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("ksk_chidinh_cls", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarIdChiDinh = new TableSchema.TableColumn(schema);
				colvarIdChiDinh.ColumnName = "IdChiDinh";
				colvarIdChiDinh.DataType = DbType.Int64;
				colvarIdChiDinh.MaxLength = 0;
				colvarIdChiDinh.AutoIncrement = true;
				colvarIdChiDinh.IsNullable = false;
				colvarIdChiDinh.IsPrimaryKey = true;
				colvarIdChiDinh.IsForeignKey = false;
				colvarIdChiDinh.IsReadOnly = false;
				colvarIdChiDinh.DefaultSetting = @"";
				colvarIdChiDinh.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdChiDinh);
				
				TableSchema.TableColumn colvarMaChiDinh = new TableSchema.TableColumn(schema);
				colvarMaChiDinh.ColumnName = "MaChiDinh";
				colvarMaChiDinh.DataType = DbType.String;
				colvarMaChiDinh.MaxLength = 20;
				colvarMaChiDinh.AutoIncrement = false;
				colvarMaChiDinh.IsNullable = false;
				colvarMaChiDinh.IsPrimaryKey = false;
				colvarMaChiDinh.IsForeignKey = false;
				colvarMaChiDinh.IsReadOnly = false;
				colvarMaChiDinh.DefaultSetting = @"";
				colvarMaChiDinh.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaChiDinh);
				
				TableSchema.TableColumn colvarIdNhanVien = new TableSchema.TableColumn(schema);
				colvarIdNhanVien.ColumnName = "IdNhanVien";
				colvarIdNhanVien.DataType = DbType.Int64;
				colvarIdNhanVien.MaxLength = 0;
				colvarIdNhanVien.AutoIncrement = false;
				colvarIdNhanVien.IsNullable = false;
				colvarIdNhanVien.IsPrimaryKey = false;
				colvarIdNhanVien.IsForeignKey = false;
				colvarIdNhanVien.IsReadOnly = false;
				colvarIdNhanVien.DefaultSetting = @"";
				colvarIdNhanVien.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdNhanVien);
				
				TableSchema.TableColumn colvarMaLuotkham = new TableSchema.TableColumn(schema);
				colvarMaLuotkham.ColumnName = "MaLuotkham";
				colvarMaLuotkham.DataType = DbType.String;
				colvarMaLuotkham.MaxLength = 20;
				colvarMaLuotkham.AutoIncrement = false;
				colvarMaLuotkham.IsNullable = false;
				colvarMaLuotkham.IsPrimaryKey = false;
				colvarMaLuotkham.IsForeignKey = false;
				colvarMaLuotkham.IsReadOnly = false;
				colvarMaLuotkham.DefaultSetting = @"";
				colvarMaLuotkham.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaLuotkham);
				
				TableSchema.TableColumn colvarSoLo = new TableSchema.TableColumn(schema);
				colvarSoLo.ColumnName = "SoLo";
				colvarSoLo.DataType = DbType.String;
				colvarSoLo.MaxLength = 20;
				colvarSoLo.AutoIncrement = false;
				colvarSoLo.IsNullable = false;
				colvarSoLo.IsPrimaryKey = false;
				colvarSoLo.IsForeignKey = false;
				colvarSoLo.IsReadOnly = false;
				colvarSoLo.DefaultSetting = @"";
				colvarSoLo.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoLo);
				
				TableSchema.TableColumn colvarMaDangKy = new TableSchema.TableColumn(schema);
				colvarMaDangKy.ColumnName = "MaDangKy";
				colvarMaDangKy.DataType = DbType.String;
				colvarMaDangKy.MaxLength = 20;
				colvarMaDangKy.AutoIncrement = false;
				colvarMaDangKy.IsNullable = false;
				colvarMaDangKy.IsPrimaryKey = false;
				colvarMaDangKy.IsForeignKey = false;
				colvarMaDangKy.IsReadOnly = false;
				colvarMaDangKy.DefaultSetting = @"";
				colvarMaDangKy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaDangKy);
				
				TableSchema.TableColumn colvarTrangThai = new TableSchema.TableColumn(schema);
				colvarTrangThai.ColumnName = "TrangThai";
				colvarTrangThai.DataType = DbType.Byte;
				colvarTrangThai.MaxLength = 0;
				colvarTrangThai.AutoIncrement = false;
				colvarTrangThai.IsNullable = true;
				colvarTrangThai.IsPrimaryKey = false;
				colvarTrangThai.IsForeignKey = false;
				colvarTrangThai.IsReadOnly = false;
				colvarTrangThai.DefaultSetting = @"";
				colvarTrangThai.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTrangThai);
				
				TableSchema.TableColumn colvarBarCode = new TableSchema.TableColumn(schema);
				colvarBarCode.ColumnName = "BarCode";
				colvarBarCode.DataType = DbType.String;
				colvarBarCode.MaxLength = 20;
				colvarBarCode.AutoIncrement = false;
				colvarBarCode.IsNullable = true;
				colvarBarCode.IsPrimaryKey = false;
				colvarBarCode.IsForeignKey = false;
				colvarBarCode.IsReadOnly = false;
				colvarBarCode.DefaultSetting = @"";
				colvarBarCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarBarCode);
				
				TableSchema.TableColumn colvarNgayChiDinh = new TableSchema.TableColumn(schema);
				colvarNgayChiDinh.ColumnName = "NgayChiDinh";
				colvarNgayChiDinh.DataType = DbType.DateTime;
				colvarNgayChiDinh.MaxLength = 0;
				colvarNgayChiDinh.AutoIncrement = false;
				colvarNgayChiDinh.IsNullable = true;
				colvarNgayChiDinh.IsPrimaryKey = false;
				colvarNgayChiDinh.IsForeignKey = false;
				colvarNgayChiDinh.IsReadOnly = false;
				colvarNgayChiDinh.DefaultSetting = @"";
				colvarNgayChiDinh.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayChiDinh);
				
				TableSchema.TableColumn colvarNgayTraKetQua = new TableSchema.TableColumn(schema);
				colvarNgayTraKetQua.ColumnName = "NgayTraKetQua";
				colvarNgayTraKetQua.DataType = DbType.DateTime;
				colvarNgayTraKetQua.MaxLength = 0;
				colvarNgayTraKetQua.AutoIncrement = false;
				colvarNgayTraKetQua.IsNullable = true;
				colvarNgayTraKetQua.IsPrimaryKey = false;
				colvarNgayTraKetQua.IsForeignKey = false;
				colvarNgayTraKetQua.IsReadOnly = false;
				colvarNgayTraKetQua.DefaultSetting = @"";
				colvarNgayTraKetQua.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayTraKetQua);
				
				TableSchema.TableColumn colvarNguoiChiDinh = new TableSchema.TableColumn(schema);
				colvarNguoiChiDinh.ColumnName = "NguoiChiDinh";
				colvarNguoiChiDinh.DataType = DbType.String;
				colvarNguoiChiDinh.MaxLength = 10;
				colvarNguoiChiDinh.AutoIncrement = false;
				colvarNguoiChiDinh.IsNullable = true;
				colvarNguoiChiDinh.IsPrimaryKey = false;
				colvarNguoiChiDinh.IsForeignKey = false;
				colvarNguoiChiDinh.IsReadOnly = false;
				colvarNguoiChiDinh.DefaultSetting = @"";
				colvarNguoiChiDinh.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNguoiChiDinh);
				
				TableSchema.TableColumn colvarMoTaThem = new TableSchema.TableColumn(schema);
				colvarMoTaThem.ColumnName = "MoTaThem";
				colvarMoTaThem.DataType = DbType.String;
				colvarMoTaThem.MaxLength = 500;
				colvarMoTaThem.AutoIncrement = false;
				colvarMoTaThem.IsNullable = true;
				colvarMoTaThem.IsPrimaryKey = false;
				colvarMoTaThem.IsForeignKey = false;
				colvarMoTaThem.IsReadOnly = false;
				colvarMoTaThem.DefaultSetting = @"";
				colvarMoTaThem.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMoTaThem);
				
				TableSchema.TableColumn colvarGhiChu = new TableSchema.TableColumn(schema);
				colvarGhiChu.ColumnName = "GhiChu";
				colvarGhiChu.DataType = DbType.String;
				colvarGhiChu.MaxLength = 500;
				colvarGhiChu.AutoIncrement = false;
				colvarGhiChu.IsNullable = true;
				colvarGhiChu.IsPrimaryKey = false;
				colvarGhiChu.IsForeignKey = false;
				colvarGhiChu.IsReadOnly = false;
				colvarGhiChu.DefaultSetting = @"";
				colvarGhiChu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGhiChu);
				
				TableSchema.TableColumn colvarNguoiTao = new TableSchema.TableColumn(schema);
				colvarNguoiTao.ColumnName = "NguoiTao";
				colvarNguoiTao.DataType = DbType.String;
				colvarNguoiTao.MaxLength = 10;
				colvarNguoiTao.AutoIncrement = false;
				colvarNguoiTao.IsNullable = true;
				colvarNguoiTao.IsPrimaryKey = false;
				colvarNguoiTao.IsForeignKey = false;
				colvarNguoiTao.IsReadOnly = false;
				colvarNguoiTao.DefaultSetting = @"";
				colvarNguoiTao.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNguoiTao);
				
				TableSchema.TableColumn colvarNgayTao = new TableSchema.TableColumn(schema);
				colvarNgayTao.ColumnName = "NgayTao";
				colvarNgayTao.DataType = DbType.String;
				colvarNgayTao.MaxLength = 10;
				colvarNgayTao.AutoIncrement = false;
				colvarNgayTao.IsNullable = true;
				colvarNgayTao.IsPrimaryKey = false;
				colvarNgayTao.IsForeignKey = false;
				colvarNgayTao.IsReadOnly = false;
				colvarNgayTao.DefaultSetting = @"";
				colvarNgayTao.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayTao);
				
				TableSchema.TableColumn colvarNguoiSua = new TableSchema.TableColumn(schema);
				colvarNguoiSua.ColumnName = "NguoiSua";
				colvarNguoiSua.DataType = DbType.String;
				colvarNguoiSua.MaxLength = 10;
				colvarNguoiSua.AutoIncrement = false;
				colvarNguoiSua.IsNullable = true;
				colvarNguoiSua.IsPrimaryKey = false;
				colvarNguoiSua.IsForeignKey = false;
				colvarNguoiSua.IsReadOnly = false;
				colvarNguoiSua.DefaultSetting = @"";
				colvarNguoiSua.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNguoiSua);
				
				TableSchema.TableColumn colvarNgaySua = new TableSchema.TableColumn(schema);
				colvarNgaySua.ColumnName = "NgaySua";
				colvarNgaySua.DataType = DbType.String;
				colvarNgaySua.MaxLength = 10;
				colvarNgaySua.AutoIncrement = false;
				colvarNgaySua.IsNullable = true;
				colvarNgaySua.IsPrimaryKey = false;
				colvarNgaySua.IsForeignKey = false;
				colvarNgaySua.IsReadOnly = false;
				colvarNgaySua.DefaultSetting = @"";
				colvarNgaySua.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgaySua);
				
				TableSchema.TableColumn colvarIpMayTao = new TableSchema.TableColumn(schema);
				colvarIpMayTao.ColumnName = "IpMayTao";
				colvarIpMayTao.DataType = DbType.String;
				colvarIpMayTao.MaxLength = 10;
				colvarIpMayTao.AutoIncrement = false;
				colvarIpMayTao.IsNullable = true;
				colvarIpMayTao.IsPrimaryKey = false;
				colvarIpMayTao.IsForeignKey = false;
				colvarIpMayTao.IsReadOnly = false;
				colvarIpMayTao.DefaultSetting = @"";
				colvarIpMayTao.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIpMayTao);
				
				TableSchema.TableColumn colvarIpMaySua = new TableSchema.TableColumn(schema);
				colvarIpMaySua.ColumnName = "IpMaySua";
				colvarIpMaySua.DataType = DbType.String;
				colvarIpMaySua.MaxLength = 10;
				colvarIpMaySua.AutoIncrement = false;
				colvarIpMaySua.IsNullable = true;
				colvarIpMaySua.IsPrimaryKey = false;
				colvarIpMaySua.IsForeignKey = false;
				colvarIpMaySua.IsReadOnly = false;
				colvarIpMaySua.DefaultSetting = @"";
				colvarIpMaySua.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIpMaySua);
				
				TableSchema.TableColumn colvarMacMayTao = new TableSchema.TableColumn(schema);
				colvarMacMayTao.ColumnName = "MacMayTao";
				colvarMacMayTao.DataType = DbType.String;
				colvarMacMayTao.MaxLength = 10;
				colvarMacMayTao.AutoIncrement = false;
				colvarMacMayTao.IsNullable = true;
				colvarMacMayTao.IsPrimaryKey = false;
				colvarMacMayTao.IsForeignKey = false;
				colvarMacMayTao.IsReadOnly = false;
				colvarMacMayTao.DefaultSetting = @"";
				colvarMacMayTao.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMacMayTao);
				
				TableSchema.TableColumn colvarMacMaySua = new TableSchema.TableColumn(schema);
				colvarMacMaySua.ColumnName = "MacMaySua";
				colvarMacMaySua.DataType = DbType.String;
				colvarMacMaySua.MaxLength = 10;
				colvarMacMaySua.AutoIncrement = false;
				colvarMacMaySua.IsNullable = true;
				colvarMacMaySua.IsPrimaryKey = false;
				colvarMacMaySua.IsForeignKey = false;
				colvarMacMaySua.IsReadOnly = false;
				colvarMacMaySua.DefaultSetting = @"";
				colvarMacMaySua.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMacMaySua);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("ksk_chidinh_cls",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("IdChiDinh")]
		[Bindable(true)]
		public long IdChiDinh 
		{
			get { return GetColumnValue<long>(Columns.IdChiDinh); }
			set { SetColumnValue(Columns.IdChiDinh, value); }
		}
		  
		[XmlAttribute("MaChiDinh")]
		[Bindable(true)]
		public string MaChiDinh 
		{
			get { return GetColumnValue<string>(Columns.MaChiDinh); }
			set { SetColumnValue(Columns.MaChiDinh, value); }
		}
		  
		[XmlAttribute("IdNhanVien")]
		[Bindable(true)]
		public long IdNhanVien 
		{
			get { return GetColumnValue<long>(Columns.IdNhanVien); }
			set { SetColumnValue(Columns.IdNhanVien, value); }
		}
		  
		[XmlAttribute("MaLuotkham")]
		[Bindable(true)]
		public string MaLuotkham 
		{
			get { return GetColumnValue<string>(Columns.MaLuotkham); }
			set { SetColumnValue(Columns.MaLuotkham, value); }
		}
		  
		[XmlAttribute("SoLo")]
		[Bindable(true)]
		public string SoLo 
		{
			get { return GetColumnValue<string>(Columns.SoLo); }
			set { SetColumnValue(Columns.SoLo, value); }
		}
		  
		[XmlAttribute("MaDangKy")]
		[Bindable(true)]
		public string MaDangKy 
		{
			get { return GetColumnValue<string>(Columns.MaDangKy); }
			set { SetColumnValue(Columns.MaDangKy, value); }
		}
		  
		[XmlAttribute("TrangThai")]
		[Bindable(true)]
		public byte? TrangThai 
		{
			get { return GetColumnValue<byte?>(Columns.TrangThai); }
			set { SetColumnValue(Columns.TrangThai, value); }
		}
		  
		[XmlAttribute("BarCode")]
		[Bindable(true)]
		public string BarCode 
		{
			get { return GetColumnValue<string>(Columns.BarCode); }
			set { SetColumnValue(Columns.BarCode, value); }
		}
		  
		[XmlAttribute("NgayChiDinh")]
		[Bindable(true)]
		public DateTime? NgayChiDinh 
		{
			get { return GetColumnValue<DateTime?>(Columns.NgayChiDinh); }
			set { SetColumnValue(Columns.NgayChiDinh, value); }
		}
		  
		[XmlAttribute("NgayTraKetQua")]
		[Bindable(true)]
		public DateTime? NgayTraKetQua 
		{
			get { return GetColumnValue<DateTime?>(Columns.NgayTraKetQua); }
			set { SetColumnValue(Columns.NgayTraKetQua, value); }
		}
		  
		[XmlAttribute("NguoiChiDinh")]
		[Bindable(true)]
		public string NguoiChiDinh 
		{
			get { return GetColumnValue<string>(Columns.NguoiChiDinh); }
			set { SetColumnValue(Columns.NguoiChiDinh, value); }
		}
		  
		[XmlAttribute("MoTaThem")]
		[Bindable(true)]
		public string MoTaThem 
		{
			get { return GetColumnValue<string>(Columns.MoTaThem); }
			set { SetColumnValue(Columns.MoTaThem, value); }
		}
		  
		[XmlAttribute("GhiChu")]
		[Bindable(true)]
		public string GhiChu 
		{
			get { return GetColumnValue<string>(Columns.GhiChu); }
			set { SetColumnValue(Columns.GhiChu, value); }
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
		public string NgayTao 
		{
			get { return GetColumnValue<string>(Columns.NgayTao); }
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
		public string NgaySua 
		{
			get { return GetColumnValue<string>(Columns.NgaySua); }
			set { SetColumnValue(Columns.NgaySua, value); }
		}
		  
		[XmlAttribute("IpMayTao")]
		[Bindable(true)]
		public string IpMayTao 
		{
			get { return GetColumnValue<string>(Columns.IpMayTao); }
			set { SetColumnValue(Columns.IpMayTao, value); }
		}
		  
		[XmlAttribute("IpMaySua")]
		[Bindable(true)]
		public string IpMaySua 
		{
			get { return GetColumnValue<string>(Columns.IpMaySua); }
			set { SetColumnValue(Columns.IpMaySua, value); }
		}
		  
		[XmlAttribute("MacMayTao")]
		[Bindable(true)]
		public string MacMayTao 
		{
			get { return GetColumnValue<string>(Columns.MacMayTao); }
			set { SetColumnValue(Columns.MacMayTao, value); }
		}
		  
		[XmlAttribute("MacMaySua")]
		[Bindable(true)]
		public string MacMaySua 
		{
			get { return GetColumnValue<string>(Columns.MacMaySua); }
			set { SetColumnValue(Columns.MacMaySua, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varMaChiDinh,long varIdNhanVien,string varMaLuotkham,string varSoLo,string varMaDangKy,byte? varTrangThai,string varBarCode,DateTime? varNgayChiDinh,DateTime? varNgayTraKetQua,string varNguoiChiDinh,string varMoTaThem,string varGhiChu,string varNguoiTao,string varNgayTao,string varNguoiSua,string varNgaySua,string varIpMayTao,string varIpMaySua,string varMacMayTao,string varMacMaySua)
		{
			KskChidinhCl item = new KskChidinhCl();
			
			item.MaChiDinh = varMaChiDinh;
			
			item.IdNhanVien = varIdNhanVien;
			
			item.MaLuotkham = varMaLuotkham;
			
			item.SoLo = varSoLo;
			
			item.MaDangKy = varMaDangKy;
			
			item.TrangThai = varTrangThai;
			
			item.BarCode = varBarCode;
			
			item.NgayChiDinh = varNgayChiDinh;
			
			item.NgayTraKetQua = varNgayTraKetQua;
			
			item.NguoiChiDinh = varNguoiChiDinh;
			
			item.MoTaThem = varMoTaThem;
			
			item.GhiChu = varGhiChu;
			
			item.NguoiTao = varNguoiTao;
			
			item.NgayTao = varNgayTao;
			
			item.NguoiSua = varNguoiSua;
			
			item.NgaySua = varNgaySua;
			
			item.IpMayTao = varIpMayTao;
			
			item.IpMaySua = varIpMaySua;
			
			item.MacMayTao = varMacMayTao;
			
			item.MacMaySua = varMacMaySua;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(long varIdChiDinh,string varMaChiDinh,long varIdNhanVien,string varMaLuotkham,string varSoLo,string varMaDangKy,byte? varTrangThai,string varBarCode,DateTime? varNgayChiDinh,DateTime? varNgayTraKetQua,string varNguoiChiDinh,string varMoTaThem,string varGhiChu,string varNguoiTao,string varNgayTao,string varNguoiSua,string varNgaySua,string varIpMayTao,string varIpMaySua,string varMacMayTao,string varMacMaySua)
		{
			KskChidinhCl item = new KskChidinhCl();
			
				item.IdChiDinh = varIdChiDinh;
			
				item.MaChiDinh = varMaChiDinh;
			
				item.IdNhanVien = varIdNhanVien;
			
				item.MaLuotkham = varMaLuotkham;
			
				item.SoLo = varSoLo;
			
				item.MaDangKy = varMaDangKy;
			
				item.TrangThai = varTrangThai;
			
				item.BarCode = varBarCode;
			
				item.NgayChiDinh = varNgayChiDinh;
			
				item.NgayTraKetQua = varNgayTraKetQua;
			
				item.NguoiChiDinh = varNguoiChiDinh;
			
				item.MoTaThem = varMoTaThem;
			
				item.GhiChu = varGhiChu;
			
				item.NguoiTao = varNguoiTao;
			
				item.NgayTao = varNgayTao;
			
				item.NguoiSua = varNguoiSua;
			
				item.NgaySua = varNgaySua;
			
				item.IpMayTao = varIpMayTao;
			
				item.IpMaySua = varIpMaySua;
			
				item.MacMayTao = varMacMayTao;
			
				item.MacMaySua = varMacMaySua;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdChiDinhColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn MaChiDinhColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn IdNhanVienColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn MaLuotkhamColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn SoLoColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn MaDangKyColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn TrangThaiColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn BarCodeColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayChiDinhColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayTraKetQuaColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiChiDinhColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn MoTaThemColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn GhiChuColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiTaoColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayTaoColumn
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiSuaColumn
        {
            get { return Schema.Columns[15]; }
        }
        
        
        
        public static TableSchema.TableColumn NgaySuaColumn
        {
            get { return Schema.Columns[16]; }
        }
        
        
        
        public static TableSchema.TableColumn IpMayTaoColumn
        {
            get { return Schema.Columns[17]; }
        }
        
        
        
        public static TableSchema.TableColumn IpMaySuaColumn
        {
            get { return Schema.Columns[18]; }
        }
        
        
        
        public static TableSchema.TableColumn MacMayTaoColumn
        {
            get { return Schema.Columns[19]; }
        }
        
        
        
        public static TableSchema.TableColumn MacMaySuaColumn
        {
            get { return Schema.Columns[20]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string IdChiDinh = @"IdChiDinh";
			 public static string MaChiDinh = @"MaChiDinh";
			 public static string IdNhanVien = @"IdNhanVien";
			 public static string MaLuotkham = @"MaLuotkham";
			 public static string SoLo = @"SoLo";
			 public static string MaDangKy = @"MaDangKy";
			 public static string TrangThai = @"TrangThai";
			 public static string BarCode = @"BarCode";
			 public static string NgayChiDinh = @"NgayChiDinh";
			 public static string NgayTraKetQua = @"NgayTraKetQua";
			 public static string NguoiChiDinh = @"NguoiChiDinh";
			 public static string MoTaThem = @"MoTaThem";
			 public static string GhiChu = @"GhiChu";
			 public static string NguoiTao = @"NguoiTao";
			 public static string NgayTao = @"NgayTao";
			 public static string NguoiSua = @"NguoiSua";
			 public static string NgaySua = @"NgaySua";
			 public static string IpMayTao = @"IpMayTao";
			 public static string IpMaySua = @"IpMaySua";
			 public static string MacMayTao = @"MacMayTao";
			 public static string MacMaySua = @"MacMaySua";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}