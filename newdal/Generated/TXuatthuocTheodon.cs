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
	/// Strongly-typed collection for the TXuatthuocTheodon class.
	/// </summary>
    [Serializable]
	public partial class TXuatthuocTheodonCollection : ActiveList<TXuatthuocTheodon, TXuatthuocTheodonCollection>
	{	   
		public TXuatthuocTheodonCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>TXuatthuocTheodonCollection</returns>
		public TXuatthuocTheodonCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                TXuatthuocTheodon o = this[i];
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
	/// This is an ActiveRecord class which wraps the t_xuatthuoc_theodon table.
	/// </summary>
	[Serializable]
	public partial class TXuatthuocTheodon : ActiveRecord<TXuatthuocTheodon>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public TXuatthuocTheodon()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public TXuatthuocTheodon(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public TXuatthuocTheodon(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public TXuatthuocTheodon(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("t_xuatthuoc_theodon", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarIdPhieu = new TableSchema.TableColumn(schema);
				colvarIdPhieu.ColumnName = "id_phieu";
				colvarIdPhieu.DataType = DbType.Int32;
				colvarIdPhieu.MaxLength = 0;
				colvarIdPhieu.AutoIncrement = true;
				colvarIdPhieu.IsNullable = false;
				colvarIdPhieu.IsPrimaryKey = true;
				colvarIdPhieu.IsForeignKey = false;
				colvarIdPhieu.IsReadOnly = false;
				colvarIdPhieu.DefaultSetting = @"";
				colvarIdPhieu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdPhieu);
				
				TableSchema.TableColumn colvarIdPhieuXuat = new TableSchema.TableColumn(schema);
				colvarIdPhieuXuat.ColumnName = "id_phieu_xuat";
				colvarIdPhieuXuat.DataType = DbType.Int32;
				colvarIdPhieuXuat.MaxLength = 0;
				colvarIdPhieuXuat.AutoIncrement = false;
				colvarIdPhieuXuat.IsNullable = false;
				colvarIdPhieuXuat.IsPrimaryKey = false;
				colvarIdPhieuXuat.IsForeignKey = false;
				colvarIdPhieuXuat.IsReadOnly = false;
				colvarIdPhieuXuat.DefaultSetting = @"";
				colvarIdPhieuXuat.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdPhieuXuat);
				
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
				
				TableSchema.TableColumn colvarSoLuong = new TableSchema.TableColumn(schema);
				colvarSoLuong.ColumnName = "so_luong";
				colvarSoLuong.DataType = DbType.Decimal;
				colvarSoLuong.MaxLength = 0;
				colvarSoLuong.AutoIncrement = false;
				colvarSoLuong.IsNullable = false;
				colvarSoLuong.IsPrimaryKey = false;
				colvarSoLuong.IsForeignKey = false;
				colvarSoLuong.IsReadOnly = false;
				colvarSoLuong.DefaultSetting = @"";
				colvarSoLuong.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoLuong);
				
				TableSchema.TableColumn colvarDonGia = new TableSchema.TableColumn(schema);
				colvarDonGia.ColumnName = "don_gia";
				colvarDonGia.DataType = DbType.Decimal;
				colvarDonGia.MaxLength = 0;
				colvarDonGia.AutoIncrement = false;
				colvarDonGia.IsNullable = false;
				colvarDonGia.IsPrimaryKey = false;
				colvarDonGia.IsForeignKey = false;
				colvarDonGia.IsReadOnly = false;
				colvarDonGia.DefaultSetting = @"";
				colvarDonGia.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDonGia);
				
				TableSchema.TableColumn colvarPhuThu = new TableSchema.TableColumn(schema);
				colvarPhuThu.ColumnName = "phu_thu";
				colvarPhuThu.DataType = DbType.Decimal;
				colvarPhuThu.MaxLength = 0;
				colvarPhuThu.AutoIncrement = false;
				colvarPhuThu.IsNullable = false;
				colvarPhuThu.IsPrimaryKey = false;
				colvarPhuThu.IsForeignKey = false;
				colvarPhuThu.IsReadOnly = false;
				colvarPhuThu.DefaultSetting = @"";
				colvarPhuThu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhuThu);
				
				TableSchema.TableColumn colvarBnhanChitra = new TableSchema.TableColumn(schema);
				colvarBnhanChitra.ColumnName = "bnhan_chitra";
				colvarBnhanChitra.DataType = DbType.Decimal;
				colvarBnhanChitra.MaxLength = 0;
				colvarBnhanChitra.AutoIncrement = false;
				colvarBnhanChitra.IsNullable = false;
				colvarBnhanChitra.IsPrimaryKey = false;
				colvarBnhanChitra.IsForeignKey = false;
				colvarBnhanChitra.IsReadOnly = false;
				colvarBnhanChitra.DefaultSetting = @"";
				colvarBnhanChitra.ForeignKeyTableName = "";
				schema.Columns.Add(colvarBnhanChitra);
				
				TableSchema.TableColumn colvarBhytChitra = new TableSchema.TableColumn(schema);
				colvarBhytChitra.ColumnName = "bhyt_chitra";
				colvarBhytChitra.DataType = DbType.Decimal;
				colvarBhytChitra.MaxLength = 0;
				colvarBhytChitra.AutoIncrement = false;
				colvarBhytChitra.IsNullable = false;
				colvarBhytChitra.IsPrimaryKey = false;
				colvarBhytChitra.IsForeignKey = false;
				colvarBhytChitra.IsReadOnly = false;
				colvarBhytChitra.DefaultSetting = @"";
				colvarBhytChitra.ForeignKeyTableName = "";
				schema.Columns.Add(colvarBhytChitra);
				
				TableSchema.TableColumn colvarPtramBhyt = new TableSchema.TableColumn(schema);
				colvarPtramBhyt.ColumnName = "ptram_bhyt";
				colvarPtramBhyt.DataType = DbType.Int32;
				colvarPtramBhyt.MaxLength = 0;
				colvarPtramBhyt.AutoIncrement = false;
				colvarPtramBhyt.IsNullable = true;
				colvarPtramBhyt.IsPrimaryKey = false;
				colvarPtramBhyt.IsForeignKey = false;
				colvarPtramBhyt.IsReadOnly = false;
				colvarPtramBhyt.DefaultSetting = @"";
				colvarPtramBhyt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPtramBhyt);
				
				TableSchema.TableColumn colvarChiDan = new TableSchema.TableColumn(schema);
				colvarChiDan.ColumnName = "chi_dan";
				colvarChiDan.DataType = DbType.String;
				colvarChiDan.MaxLength = 500;
				colvarChiDan.AutoIncrement = false;
				colvarChiDan.IsNullable = true;
				colvarChiDan.IsPrimaryKey = false;
				colvarChiDan.IsForeignKey = false;
				colvarChiDan.IsReadOnly = false;
				colvarChiDan.DefaultSetting = @"";
				colvarChiDan.ForeignKeyTableName = "";
				schema.Columns.Add(colvarChiDan);
				
				TableSchema.TableColumn colvarCachDung = new TableSchema.TableColumn(schema);
				colvarCachDung.ColumnName = "cach_dung";
				colvarCachDung.DataType = DbType.String;
				colvarCachDung.MaxLength = 255;
				colvarCachDung.AutoIncrement = false;
				colvarCachDung.IsNullable = true;
				colvarCachDung.IsPrimaryKey = false;
				colvarCachDung.IsForeignKey = false;
				colvarCachDung.IsReadOnly = false;
				colvarCachDung.DefaultSetting = @"";
				colvarCachDung.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCachDung);
				
				TableSchema.TableColumn colvarChidanThem = new TableSchema.TableColumn(schema);
				colvarChidanThem.ColumnName = "chidan_them";
				colvarChidanThem.DataType = DbType.String;
				colvarChidanThem.MaxLength = 500;
				colvarChidanThem.AutoIncrement = false;
				colvarChidanThem.IsNullable = true;
				colvarChidanThem.IsPrimaryKey = false;
				colvarChidanThem.IsForeignKey = false;
				colvarChidanThem.IsReadOnly = false;
				colvarChidanThem.DefaultSetting = @"";
				colvarChidanThem.ForeignKeyTableName = "";
				schema.Columns.Add(colvarChidanThem);
				
				TableSchema.TableColumn colvarSolanDung = new TableSchema.TableColumn(schema);
				colvarSolanDung.ColumnName = "solan_dung";
				colvarSolanDung.DataType = DbType.String;
				colvarSolanDung.MaxLength = 10;
				colvarSolanDung.AutoIncrement = false;
				colvarSolanDung.IsNullable = true;
				colvarSolanDung.IsPrimaryKey = false;
				colvarSolanDung.IsForeignKey = false;
				colvarSolanDung.IsReadOnly = false;
				colvarSolanDung.DefaultSetting = @"";
				colvarSolanDung.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSolanDung);
				
				TableSchema.TableColumn colvarSoluongDung = new TableSchema.TableColumn(schema);
				colvarSoluongDung.ColumnName = "soluong_dung";
				colvarSoluongDung.DataType = DbType.String;
				colvarSoluongDung.MaxLength = 10;
				colvarSoluongDung.AutoIncrement = false;
				colvarSoluongDung.IsNullable = true;
				colvarSoluongDung.IsPrimaryKey = false;
				colvarSoluongDung.IsForeignKey = false;
				colvarSoluongDung.IsReadOnly = false;
				colvarSoluongDung.DefaultSetting = @"";
				colvarSoluongDung.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoluongDung);
				
				TableSchema.TableColumn colvarNgayTao = new TableSchema.TableColumn(schema);
				colvarNgayTao.ColumnName = "ngay_tao";
				colvarNgayTao.DataType = DbType.DateTime;
				colvarNgayTao.MaxLength = 0;
				colvarNgayTao.AutoIncrement = false;
				colvarNgayTao.IsNullable = true;
				colvarNgayTao.IsPrimaryKey = false;
				colvarNgayTao.IsForeignKey = false;
				colvarNgayTao.IsReadOnly = false;
				
						colvarNgayTao.DefaultSetting = @"(getdate())";
				colvarNgayTao.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayTao);
				
				TableSchema.TableColumn colvarNguoiTao = new TableSchema.TableColumn(schema);
				colvarNguoiTao.ColumnName = "nguoi_tao";
				colvarNguoiTao.DataType = DbType.String;
				colvarNguoiTao.MaxLength = 50;
				colvarNguoiTao.AutoIncrement = false;
				colvarNguoiTao.IsNullable = true;
				colvarNguoiTao.IsPrimaryKey = false;
				colvarNguoiTao.IsForeignKey = false;
				colvarNguoiTao.IsReadOnly = false;
				colvarNguoiTao.DefaultSetting = @"";
				colvarNguoiTao.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNguoiTao);
				
				TableSchema.TableColumn colvarIdChitietdonthuoc = new TableSchema.TableColumn(schema);
				colvarIdChitietdonthuoc.ColumnName = "id_chitietdonthuoc";
				colvarIdChitietdonthuoc.DataType = DbType.Int32;
				colvarIdChitietdonthuoc.MaxLength = 0;
				colvarIdChitietdonthuoc.AutoIncrement = false;
				colvarIdChitietdonthuoc.IsNullable = true;
				colvarIdChitietdonthuoc.IsPrimaryKey = false;
				colvarIdChitietdonthuoc.IsForeignKey = false;
				colvarIdChitietdonthuoc.IsReadOnly = false;
				colvarIdChitietdonthuoc.DefaultSetting = @"";
				colvarIdChitietdonthuoc.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdChitietdonthuoc);
				
				TableSchema.TableColumn colvarIdDonthuoc = new TableSchema.TableColumn(schema);
				colvarIdDonthuoc.ColumnName = "id_donthuoc";
				colvarIdDonthuoc.DataType = DbType.Int32;
				colvarIdDonthuoc.MaxLength = 0;
				colvarIdDonthuoc.AutoIncrement = false;
				colvarIdDonthuoc.IsNullable = true;
				colvarIdDonthuoc.IsPrimaryKey = false;
				colvarIdDonthuoc.IsForeignKey = false;
				colvarIdDonthuoc.IsReadOnly = false;
				colvarIdDonthuoc.DefaultSetting = @"";
				colvarIdDonthuoc.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdDonthuoc);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("t_xuatthuoc_theodon",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("IdPhieu")]
		[Bindable(true)]
		public int IdPhieu 
		{
			get { return GetColumnValue<int>(Columns.IdPhieu); }
			set { SetColumnValue(Columns.IdPhieu, value); }
		}
		  
		[XmlAttribute("IdPhieuXuat")]
		[Bindable(true)]
		public int IdPhieuXuat 
		{
			get { return GetColumnValue<int>(Columns.IdPhieuXuat); }
			set { SetColumnValue(Columns.IdPhieuXuat, value); }
		}
		  
		[XmlAttribute("IdThuoc")]
		[Bindable(true)]
		public int IdThuoc 
		{
			get { return GetColumnValue<int>(Columns.IdThuoc); }
			set { SetColumnValue(Columns.IdThuoc, value); }
		}
		  
		[XmlAttribute("SoLuong")]
		[Bindable(true)]
		public decimal SoLuong 
		{
			get { return GetColumnValue<decimal>(Columns.SoLuong); }
			set { SetColumnValue(Columns.SoLuong, value); }
		}
		  
		[XmlAttribute("DonGia")]
		[Bindable(true)]
		public decimal DonGia 
		{
			get { return GetColumnValue<decimal>(Columns.DonGia); }
			set { SetColumnValue(Columns.DonGia, value); }
		}
		  
		[XmlAttribute("PhuThu")]
		[Bindable(true)]
		public decimal PhuThu 
		{
			get { return GetColumnValue<decimal>(Columns.PhuThu); }
			set { SetColumnValue(Columns.PhuThu, value); }
		}
		  
		[XmlAttribute("BnhanChitra")]
		[Bindable(true)]
		public decimal BnhanChitra 
		{
			get { return GetColumnValue<decimal>(Columns.BnhanChitra); }
			set { SetColumnValue(Columns.BnhanChitra, value); }
		}
		  
		[XmlAttribute("BhytChitra")]
		[Bindable(true)]
		public decimal BhytChitra 
		{
			get { return GetColumnValue<decimal>(Columns.BhytChitra); }
			set { SetColumnValue(Columns.BhytChitra, value); }
		}
		  
		[XmlAttribute("PtramBhyt")]
		[Bindable(true)]
		public int? PtramBhyt 
		{
			get { return GetColumnValue<int?>(Columns.PtramBhyt); }
			set { SetColumnValue(Columns.PtramBhyt, value); }
		}
		  
		[XmlAttribute("ChiDan")]
		[Bindable(true)]
		public string ChiDan 
		{
			get { return GetColumnValue<string>(Columns.ChiDan); }
			set { SetColumnValue(Columns.ChiDan, value); }
		}
		  
		[XmlAttribute("CachDung")]
		[Bindable(true)]
		public string CachDung 
		{
			get { return GetColumnValue<string>(Columns.CachDung); }
			set { SetColumnValue(Columns.CachDung, value); }
		}
		  
		[XmlAttribute("ChidanThem")]
		[Bindable(true)]
		public string ChidanThem 
		{
			get { return GetColumnValue<string>(Columns.ChidanThem); }
			set { SetColumnValue(Columns.ChidanThem, value); }
		}
		  
		[XmlAttribute("SolanDung")]
		[Bindable(true)]
		public string SolanDung 
		{
			get { return GetColumnValue<string>(Columns.SolanDung); }
			set { SetColumnValue(Columns.SolanDung, value); }
		}
		  
		[XmlAttribute("SoluongDung")]
		[Bindable(true)]
		public string SoluongDung 
		{
			get { return GetColumnValue<string>(Columns.SoluongDung); }
			set { SetColumnValue(Columns.SoluongDung, value); }
		}
		  
		[XmlAttribute("NgayTao")]
		[Bindable(true)]
		public DateTime? NgayTao 
		{
			get { return GetColumnValue<DateTime?>(Columns.NgayTao); }
			set { SetColumnValue(Columns.NgayTao, value); }
		}
		  
		[XmlAttribute("NguoiTao")]
		[Bindable(true)]
		public string NguoiTao 
		{
			get { return GetColumnValue<string>(Columns.NguoiTao); }
			set { SetColumnValue(Columns.NguoiTao, value); }
		}
		  
		[XmlAttribute("IdChitietdonthuoc")]
		[Bindable(true)]
		public int? IdChitietdonthuoc 
		{
			get { return GetColumnValue<int?>(Columns.IdChitietdonthuoc); }
			set { SetColumnValue(Columns.IdChitietdonthuoc, value); }
		}
		  
		[XmlAttribute("IdDonthuoc")]
		[Bindable(true)]
		public int? IdDonthuoc 
		{
			get { return GetColumnValue<int?>(Columns.IdDonthuoc); }
			set { SetColumnValue(Columns.IdDonthuoc, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(int varIdPhieuXuat,int varIdThuoc,decimal varSoLuong,decimal varDonGia,decimal varPhuThu,decimal varBnhanChitra,decimal varBhytChitra,int? varPtramBhyt,string varChiDan,string varCachDung,string varChidanThem,string varSolanDung,string varSoluongDung,DateTime? varNgayTao,string varNguoiTao,int? varIdChitietdonthuoc,int? varIdDonthuoc)
		{
			TXuatthuocTheodon item = new TXuatthuocTheodon();
			
			item.IdPhieuXuat = varIdPhieuXuat;
			
			item.IdThuoc = varIdThuoc;
			
			item.SoLuong = varSoLuong;
			
			item.DonGia = varDonGia;
			
			item.PhuThu = varPhuThu;
			
			item.BnhanChitra = varBnhanChitra;
			
			item.BhytChitra = varBhytChitra;
			
			item.PtramBhyt = varPtramBhyt;
			
			item.ChiDan = varChiDan;
			
			item.CachDung = varCachDung;
			
			item.ChidanThem = varChidanThem;
			
			item.SolanDung = varSolanDung;
			
			item.SoluongDung = varSoluongDung;
			
			item.NgayTao = varNgayTao;
			
			item.NguoiTao = varNguoiTao;
			
			item.IdChitietdonthuoc = varIdChitietdonthuoc;
			
			item.IdDonthuoc = varIdDonthuoc;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varIdPhieu,int varIdPhieuXuat,int varIdThuoc,decimal varSoLuong,decimal varDonGia,decimal varPhuThu,decimal varBnhanChitra,decimal varBhytChitra,int? varPtramBhyt,string varChiDan,string varCachDung,string varChidanThem,string varSolanDung,string varSoluongDung,DateTime? varNgayTao,string varNguoiTao,int? varIdChitietdonthuoc,int? varIdDonthuoc)
		{
			TXuatthuocTheodon item = new TXuatthuocTheodon();
			
				item.IdPhieu = varIdPhieu;
			
				item.IdPhieuXuat = varIdPhieuXuat;
			
				item.IdThuoc = varIdThuoc;
			
				item.SoLuong = varSoLuong;
			
				item.DonGia = varDonGia;
			
				item.PhuThu = varPhuThu;
			
				item.BnhanChitra = varBnhanChitra;
			
				item.BhytChitra = varBhytChitra;
			
				item.PtramBhyt = varPtramBhyt;
			
				item.ChiDan = varChiDan;
			
				item.CachDung = varCachDung;
			
				item.ChidanThem = varChidanThem;
			
				item.SolanDung = varSolanDung;
			
				item.SoluongDung = varSoluongDung;
			
				item.NgayTao = varNgayTao;
			
				item.NguoiTao = varNguoiTao;
			
				item.IdChitietdonthuoc = varIdChitietdonthuoc;
			
				item.IdDonthuoc = varIdDonthuoc;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdPhieuColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn IdPhieuXuatColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn IdThuocColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn SoLuongColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn DonGiaColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn PhuThuColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn BnhanChitraColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn BhytChitraColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn PtramBhytColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn ChiDanColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn CachDungColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn ChidanThemColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn SolanDungColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn SoluongDungColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayTaoColumn
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiTaoColumn
        {
            get { return Schema.Columns[15]; }
        }
        
        
        
        public static TableSchema.TableColumn IdChitietdonthuocColumn
        {
            get { return Schema.Columns[16]; }
        }
        
        
        
        public static TableSchema.TableColumn IdDonthuocColumn
        {
            get { return Schema.Columns[17]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string IdPhieu = @"id_phieu";
			 public static string IdPhieuXuat = @"id_phieu_xuat";
			 public static string IdThuoc = @"id_thuoc";
			 public static string SoLuong = @"so_luong";
			 public static string DonGia = @"don_gia";
			 public static string PhuThu = @"phu_thu";
			 public static string BnhanChitra = @"bnhan_chitra";
			 public static string BhytChitra = @"bhyt_chitra";
			 public static string PtramBhyt = @"ptram_bhyt";
			 public static string ChiDan = @"chi_dan";
			 public static string CachDung = @"cach_dung";
			 public static string ChidanThem = @"chidan_them";
			 public static string SolanDung = @"solan_dung";
			 public static string SoluongDung = @"soluong_dung";
			 public static string NgayTao = @"ngay_tao";
			 public static string NguoiTao = @"nguoi_tao";
			 public static string IdChitietdonthuoc = @"id_chitietdonthuoc";
			 public static string IdDonthuoc = @"id_donthuoc";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
