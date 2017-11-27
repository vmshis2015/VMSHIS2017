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
	/// Strongly-typed collection for the Xml3917 class.
	/// </summary>
    [Serializable]
	public partial class Xml3917Collection : ActiveList<Xml3917, Xml3917Collection>
	{	   
		public Xml3917Collection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>Xml3917Collection</returns>
		public Xml3917Collection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                Xml3917 o = this[i];
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
	/// This is an ActiveRecord class which wraps the XML_3_917 table.
	/// </summary>
	[Serializable]
	public partial class Xml3917 : ActiveRecord<Xml3917>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public Xml3917()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public Xml3917(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public Xml3917(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public Xml3917(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("XML_3_917", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarIdXML3 = new TableSchema.TableColumn(schema);
				colvarIdXML3.ColumnName = "ID_XML3";
				colvarIdXML3.DataType = DbType.Int64;
				colvarIdXML3.MaxLength = 0;
				colvarIdXML3.AutoIncrement = true;
				colvarIdXML3.IsNullable = false;
				colvarIdXML3.IsPrimaryKey = true;
				colvarIdXML3.IsForeignKey = false;
				colvarIdXML3.IsReadOnly = false;
				colvarIdXML3.DefaultSetting = @"";
				colvarIdXML3.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdXML3);
				
				TableSchema.TableColumn colvarMaLk = new TableSchema.TableColumn(schema);
				colvarMaLk.ColumnName = "MA_LK";
				colvarMaLk.DataType = DbType.String;
				colvarMaLk.MaxLength = 20;
				colvarMaLk.AutoIncrement = false;
				colvarMaLk.IsNullable = false;
				colvarMaLk.IsPrimaryKey = false;
				colvarMaLk.IsForeignKey = false;
				colvarMaLk.IsReadOnly = false;
				colvarMaLk.DefaultSetting = @"";
				colvarMaLk.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaLk);
				
				TableSchema.TableColumn colvarStt = new TableSchema.TableColumn(schema);
				colvarStt.ColumnName = "STT";
				colvarStt.DataType = DbType.Int32;
				colvarStt.MaxLength = 0;
				colvarStt.AutoIncrement = false;
				colvarStt.IsNullable = true;
				colvarStt.IsPrimaryKey = false;
				colvarStt.IsForeignKey = false;
				colvarStt.IsReadOnly = false;
				colvarStt.DefaultSetting = @"";
				colvarStt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarStt);
				
				TableSchema.TableColumn colvarMaDichVu = new TableSchema.TableColumn(schema);
				colvarMaDichVu.ColumnName = "MA_DICH_VU";
				colvarMaDichVu.DataType = DbType.String;
				colvarMaDichVu.MaxLength = 50;
				colvarMaDichVu.AutoIncrement = false;
				colvarMaDichVu.IsNullable = true;
				colvarMaDichVu.IsPrimaryKey = false;
				colvarMaDichVu.IsForeignKey = false;
				colvarMaDichVu.IsReadOnly = false;
				colvarMaDichVu.DefaultSetting = @"";
				colvarMaDichVu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaDichVu);
				
				TableSchema.TableColumn colvarMaVatTu = new TableSchema.TableColumn(schema);
				colvarMaVatTu.ColumnName = "MA_VAT_TU";
				colvarMaVatTu.DataType = DbType.String;
				colvarMaVatTu.MaxLength = 255;
				colvarMaVatTu.AutoIncrement = false;
				colvarMaVatTu.IsNullable = true;
				colvarMaVatTu.IsPrimaryKey = false;
				colvarMaVatTu.IsForeignKey = false;
				colvarMaVatTu.IsReadOnly = false;
				colvarMaVatTu.DefaultSetting = @"";
				colvarMaVatTu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaVatTu);
				
				TableSchema.TableColumn colvarMaNhom = new TableSchema.TableColumn(schema);
				colvarMaNhom.ColumnName = "MA_NHOM";
				colvarMaNhom.DataType = DbType.String;
				colvarMaNhom.MaxLength = 20;
				colvarMaNhom.AutoIncrement = false;
				colvarMaNhom.IsNullable = true;
				colvarMaNhom.IsPrimaryKey = false;
				colvarMaNhom.IsForeignKey = false;
				colvarMaNhom.IsReadOnly = false;
				colvarMaNhom.DefaultSetting = @"";
				colvarMaNhom.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaNhom);
				
				TableSchema.TableColumn colvarTenDichVu = new TableSchema.TableColumn(schema);
				colvarTenDichVu.ColumnName = "TEN_DICH_VU";
				colvarTenDichVu.DataType = DbType.String;
				colvarTenDichVu.MaxLength = 1024;
				colvarTenDichVu.AutoIncrement = false;
				colvarTenDichVu.IsNullable = false;
				colvarTenDichVu.IsPrimaryKey = false;
				colvarTenDichVu.IsForeignKey = false;
				colvarTenDichVu.IsReadOnly = false;
				colvarTenDichVu.DefaultSetting = @"";
				colvarTenDichVu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTenDichVu);
				
				TableSchema.TableColumn colvarDonViTinh = new TableSchema.TableColumn(schema);
				colvarDonViTinh.ColumnName = "DON_VI_TINH";
				colvarDonViTinh.DataType = DbType.String;
				colvarDonViTinh.MaxLength = 50;
				colvarDonViTinh.AutoIncrement = false;
				colvarDonViTinh.IsNullable = true;
				colvarDonViTinh.IsPrimaryKey = false;
				colvarDonViTinh.IsForeignKey = false;
				colvarDonViTinh.IsReadOnly = false;
				colvarDonViTinh.DefaultSetting = @"";
				colvarDonViTinh.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDonViTinh);
				
				TableSchema.TableColumn colvarSoLuong = new TableSchema.TableColumn(schema);
				colvarSoLuong.ColumnName = "SO_LUONG";
				colvarSoLuong.DataType = DbType.Decimal;
				colvarSoLuong.MaxLength = 0;
				colvarSoLuong.AutoIncrement = false;
				colvarSoLuong.IsNullable = true;
				colvarSoLuong.IsPrimaryKey = false;
				colvarSoLuong.IsForeignKey = false;
				colvarSoLuong.IsReadOnly = false;
				colvarSoLuong.DefaultSetting = @"";
				colvarSoLuong.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoLuong);
				
				TableSchema.TableColumn colvarDonGia = new TableSchema.TableColumn(schema);
				colvarDonGia.ColumnName = "DON_GIA";
				colvarDonGia.DataType = DbType.Decimal;
				colvarDonGia.MaxLength = 0;
				colvarDonGia.AutoIncrement = false;
				colvarDonGia.IsNullable = true;
				colvarDonGia.IsPrimaryKey = false;
				colvarDonGia.IsForeignKey = false;
				colvarDonGia.IsReadOnly = false;
				colvarDonGia.DefaultSetting = @"";
				colvarDonGia.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDonGia);
				
				TableSchema.TableColumn colvarTyleTt = new TableSchema.TableColumn(schema);
				colvarTyleTt.ColumnName = "TYLE_TT";
				colvarTyleTt.DataType = DbType.Int32;
				colvarTyleTt.MaxLength = 0;
				colvarTyleTt.AutoIncrement = false;
				colvarTyleTt.IsNullable = true;
				colvarTyleTt.IsPrimaryKey = false;
				colvarTyleTt.IsForeignKey = false;
				colvarTyleTt.IsReadOnly = false;
				colvarTyleTt.DefaultSetting = @"";
				colvarTyleTt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTyleTt);
				
				TableSchema.TableColumn colvarThanhTien = new TableSchema.TableColumn(schema);
				colvarThanhTien.ColumnName = "THANH_TIEN";
				colvarThanhTien.DataType = DbType.Decimal;
				colvarThanhTien.MaxLength = 0;
				colvarThanhTien.AutoIncrement = false;
				colvarThanhTien.IsNullable = true;
				colvarThanhTien.IsPrimaryKey = false;
				colvarThanhTien.IsForeignKey = false;
				colvarThanhTien.IsReadOnly = false;
				colvarThanhTien.DefaultSetting = @"";
				colvarThanhTien.ForeignKeyTableName = "";
				schema.Columns.Add(colvarThanhTien);
				
				TableSchema.TableColumn colvarMaKhoa = new TableSchema.TableColumn(schema);
				colvarMaKhoa.ColumnName = "MA_KHOA";
				colvarMaKhoa.DataType = DbType.String;
				colvarMaKhoa.MaxLength = 50;
				colvarMaKhoa.AutoIncrement = false;
				colvarMaKhoa.IsNullable = true;
				colvarMaKhoa.IsPrimaryKey = false;
				colvarMaKhoa.IsForeignKey = false;
				colvarMaKhoa.IsReadOnly = false;
				colvarMaKhoa.DefaultSetting = @"";
				colvarMaKhoa.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaKhoa);
				
				TableSchema.TableColumn colvarMaBacSi = new TableSchema.TableColumn(schema);
				colvarMaBacSi.ColumnName = "MA_BAC_SI";
				colvarMaBacSi.DataType = DbType.String;
				colvarMaBacSi.MaxLength = 50;
				colvarMaBacSi.AutoIncrement = false;
				colvarMaBacSi.IsNullable = true;
				colvarMaBacSi.IsPrimaryKey = false;
				colvarMaBacSi.IsForeignKey = false;
				colvarMaBacSi.IsReadOnly = false;
				colvarMaBacSi.DefaultSetting = @"";
				colvarMaBacSi.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaBacSi);
				
				TableSchema.TableColumn colvarMaBenh = new TableSchema.TableColumn(schema);
				colvarMaBenh.ColumnName = "MA_BENH";
				colvarMaBenh.DataType = DbType.String;
				colvarMaBenh.MaxLength = 255;
				colvarMaBenh.AutoIncrement = false;
				colvarMaBenh.IsNullable = true;
				colvarMaBenh.IsPrimaryKey = false;
				colvarMaBenh.IsForeignKey = false;
				colvarMaBenh.IsReadOnly = false;
				colvarMaBenh.DefaultSetting = @"";
				colvarMaBenh.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaBenh);
				
				TableSchema.TableColumn colvarNgayYl = new TableSchema.TableColumn(schema);
				colvarNgayYl.ColumnName = "NGAY_YL";
				colvarNgayYl.DataType = DbType.String;
				colvarNgayYl.MaxLength = 12;
				colvarNgayYl.AutoIncrement = false;
				colvarNgayYl.IsNullable = true;
				colvarNgayYl.IsPrimaryKey = false;
				colvarNgayYl.IsForeignKey = false;
				colvarNgayYl.IsReadOnly = false;
				colvarNgayYl.DefaultSetting = @"";
				colvarNgayYl.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayYl);
				
				TableSchema.TableColumn colvarNgayKq = new TableSchema.TableColumn(schema);
				colvarNgayKq.ColumnName = "NGAY_KQ";
				colvarNgayKq.DataType = DbType.String;
				colvarNgayKq.MaxLength = 12;
				colvarNgayKq.AutoIncrement = false;
				colvarNgayKq.IsNullable = true;
				colvarNgayKq.IsPrimaryKey = false;
				colvarNgayKq.IsForeignKey = false;
				colvarNgayKq.IsReadOnly = false;
				colvarNgayKq.DefaultSetting = @"";
				colvarNgayKq.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayKq);
				
				TableSchema.TableColumn colvarMaPttt = new TableSchema.TableColumn(schema);
				colvarMaPttt.ColumnName = "MA_PTTT";
				colvarMaPttt.DataType = DbType.Int32;
				colvarMaPttt.MaxLength = 0;
				colvarMaPttt.AutoIncrement = false;
				colvarMaPttt.IsNullable = true;
				colvarMaPttt.IsPrimaryKey = false;
				colvarMaPttt.IsForeignKey = false;
				colvarMaPttt.IsReadOnly = false;
				colvarMaPttt.DefaultSetting = @"";
				colvarMaPttt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaPttt);
				
				TableSchema.TableColumn colvarGoiVtyt = new TableSchema.TableColumn(schema);
				colvarGoiVtyt.ColumnName = "GOI_VTYT";
				colvarGoiVtyt.DataType = DbType.String;
				colvarGoiVtyt.MaxLength = 5;
				colvarGoiVtyt.AutoIncrement = false;
				colvarGoiVtyt.IsNullable = true;
				colvarGoiVtyt.IsPrimaryKey = false;
				colvarGoiVtyt.IsForeignKey = false;
				colvarGoiVtyt.IsReadOnly = false;
				colvarGoiVtyt.DefaultSetting = @"";
				colvarGoiVtyt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGoiVtyt);
				
				TableSchema.TableColumn colvarTenVatTu = new TableSchema.TableColumn(schema);
				colvarTenVatTu.ColumnName = "TEN_VAT_TU";
				colvarTenVatTu.DataType = DbType.String;
				colvarTenVatTu.MaxLength = 1024;
				colvarTenVatTu.AutoIncrement = false;
				colvarTenVatTu.IsNullable = true;
				colvarTenVatTu.IsPrimaryKey = false;
				colvarTenVatTu.IsForeignKey = false;
				colvarTenVatTu.IsReadOnly = false;
				colvarTenVatTu.DefaultSetting = @"";
				colvarTenVatTu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTenVatTu);
				
				TableSchema.TableColumn colvarPhamVi = new TableSchema.TableColumn(schema);
				colvarPhamVi.ColumnName = "PHAM_VI";
				colvarPhamVi.DataType = DbType.Int32;
				colvarPhamVi.MaxLength = 0;
				colvarPhamVi.AutoIncrement = false;
				colvarPhamVi.IsNullable = true;
				colvarPhamVi.IsPrimaryKey = false;
				colvarPhamVi.IsForeignKey = false;
				colvarPhamVi.IsReadOnly = false;
				colvarPhamVi.DefaultSetting = @"";
				colvarPhamVi.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhamVi);
				
				TableSchema.TableColumn colvarTtThau = new TableSchema.TableColumn(schema);
				colvarTtThau.ColumnName = "TT_THAU";
				colvarTtThau.DataType = DbType.String;
				colvarTtThau.MaxLength = 50;
				colvarTtThau.AutoIncrement = false;
				colvarTtThau.IsNullable = true;
				colvarTtThau.IsPrimaryKey = false;
				colvarTtThau.IsForeignKey = false;
				colvarTtThau.IsReadOnly = false;
				colvarTtThau.DefaultSetting = @"";
				colvarTtThau.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTtThau);
				
				TableSchema.TableColumn colvarTTrantt = new TableSchema.TableColumn(schema);
				colvarTTrantt.ColumnName = "T_TRANTT";
				colvarTTrantt.DataType = DbType.Decimal;
				colvarTTrantt.MaxLength = 0;
				colvarTTrantt.AutoIncrement = false;
				colvarTTrantt.IsNullable = true;
				colvarTTrantt.IsPrimaryKey = false;
				colvarTTrantt.IsForeignKey = false;
				colvarTTrantt.IsReadOnly = false;
				colvarTTrantt.DefaultSetting = @"";
				colvarTTrantt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTTrantt);
				
				TableSchema.TableColumn colvarMucHuong = new TableSchema.TableColumn(schema);
				colvarMucHuong.ColumnName = "MUC_HUONG";
				colvarMucHuong.DataType = DbType.Int32;
				colvarMucHuong.MaxLength = 0;
				colvarMucHuong.AutoIncrement = false;
				colvarMucHuong.IsNullable = true;
				colvarMucHuong.IsPrimaryKey = false;
				colvarMucHuong.IsForeignKey = false;
				colvarMucHuong.IsReadOnly = false;
				colvarMucHuong.DefaultSetting = @"";
				colvarMucHuong.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMucHuong);
				
				TableSchema.TableColumn colvarTNguonkhac = new TableSchema.TableColumn(schema);
				colvarTNguonkhac.ColumnName = "T_NGUONKHAC";
				colvarTNguonkhac.DataType = DbType.String;
				colvarTNguonkhac.MaxLength = 10;
				colvarTNguonkhac.AutoIncrement = false;
				colvarTNguonkhac.IsNullable = true;
				colvarTNguonkhac.IsPrimaryKey = false;
				colvarTNguonkhac.IsForeignKey = false;
				colvarTNguonkhac.IsReadOnly = false;
				colvarTNguonkhac.DefaultSetting = @"";
				colvarTNguonkhac.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTNguonkhac);
				
				TableSchema.TableColumn colvarTBntt = new TableSchema.TableColumn(schema);
				colvarTBntt.ColumnName = "T_BNTT";
				colvarTBntt.DataType = DbType.Decimal;
				colvarTBntt.MaxLength = 0;
				colvarTBntt.AutoIncrement = false;
				colvarTBntt.IsNullable = true;
				colvarTBntt.IsPrimaryKey = false;
				colvarTBntt.IsForeignKey = false;
				colvarTBntt.IsReadOnly = false;
				colvarTBntt.DefaultSetting = @"";
				colvarTBntt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTBntt);
				
				TableSchema.TableColumn colvarTBhtt = new TableSchema.TableColumn(schema);
				colvarTBhtt.ColumnName = "T_BHTT";
				colvarTBhtt.DataType = DbType.Decimal;
				colvarTBhtt.MaxLength = 0;
				colvarTBhtt.AutoIncrement = false;
				colvarTBhtt.IsNullable = true;
				colvarTBhtt.IsPrimaryKey = false;
				colvarTBhtt.IsForeignKey = false;
				colvarTBhtt.IsReadOnly = false;
				colvarTBhtt.DefaultSetting = @"";
				colvarTBhtt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTBhtt);
				
				TableSchema.TableColumn colvarTBncct = new TableSchema.TableColumn(schema);
				colvarTBncct.ColumnName = "T_BNCCT";
				colvarTBncct.DataType = DbType.Decimal;
				colvarTBncct.MaxLength = 0;
				colvarTBncct.AutoIncrement = false;
				colvarTBncct.IsNullable = true;
				colvarTBncct.IsPrimaryKey = false;
				colvarTBncct.IsForeignKey = false;
				colvarTBncct.IsReadOnly = false;
				colvarTBncct.DefaultSetting = @"";
				colvarTBncct.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTBncct);
				
				TableSchema.TableColumn colvarTNgoaids = new TableSchema.TableColumn(schema);
				colvarTNgoaids.ColumnName = "T_NGOAIDS";
				colvarTNgoaids.DataType = DbType.Decimal;
				colvarTNgoaids.MaxLength = 0;
				colvarTNgoaids.AutoIncrement = false;
				colvarTNgoaids.IsNullable = true;
				colvarTNgoaids.IsPrimaryKey = false;
				colvarTNgoaids.IsForeignKey = false;
				colvarTNgoaids.IsReadOnly = false;
				colvarTNgoaids.DefaultSetting = @"";
				colvarTNgoaids.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTNgoaids);
				
				TableSchema.TableColumn colvarMaGiuong = new TableSchema.TableColumn(schema);
				colvarMaGiuong.ColumnName = "MA_GIUONG";
				colvarMaGiuong.DataType = DbType.String;
				colvarMaGiuong.MaxLength = 50;
				colvarMaGiuong.AutoIncrement = false;
				colvarMaGiuong.IsNullable = true;
				colvarMaGiuong.IsPrimaryKey = false;
				colvarMaGiuong.IsForeignKey = false;
				colvarMaGiuong.IsReadOnly = false;
				colvarMaGiuong.DefaultSetting = @"";
				colvarMaGiuong.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaGiuong);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("XML_3_917",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("IdXML3")]
		[Bindable(true)]
		public long IdXML3 
		{
			get { return GetColumnValue<long>(Columns.IdXML3); }
			set { SetColumnValue(Columns.IdXML3, value); }
		}
		  
		[XmlAttribute("MaLk")]
		[Bindable(true)]
		public string MaLk 
		{
			get { return GetColumnValue<string>(Columns.MaLk); }
			set { SetColumnValue(Columns.MaLk, value); }
		}
		  
		[XmlAttribute("Stt")]
		[Bindable(true)]
		public int? Stt 
		{
			get { return GetColumnValue<int?>(Columns.Stt); }
			set { SetColumnValue(Columns.Stt, value); }
		}
		  
		[XmlAttribute("MaDichVu")]
		[Bindable(true)]
		public string MaDichVu 
		{
			get { return GetColumnValue<string>(Columns.MaDichVu); }
			set { SetColumnValue(Columns.MaDichVu, value); }
		}
		  
		[XmlAttribute("MaVatTu")]
		[Bindable(true)]
		public string MaVatTu 
		{
			get { return GetColumnValue<string>(Columns.MaVatTu); }
			set { SetColumnValue(Columns.MaVatTu, value); }
		}
		  
		[XmlAttribute("MaNhom")]
		[Bindable(true)]
		public string MaNhom 
		{
			get { return GetColumnValue<string>(Columns.MaNhom); }
			set { SetColumnValue(Columns.MaNhom, value); }
		}
		  
		[XmlAttribute("TenDichVu")]
		[Bindable(true)]
		public string TenDichVu 
		{
			get { return GetColumnValue<string>(Columns.TenDichVu); }
			set { SetColumnValue(Columns.TenDichVu, value); }
		}
		  
		[XmlAttribute("DonViTinh")]
		[Bindable(true)]
		public string DonViTinh 
		{
			get { return GetColumnValue<string>(Columns.DonViTinh); }
			set { SetColumnValue(Columns.DonViTinh, value); }
		}
		  
		[XmlAttribute("SoLuong")]
		[Bindable(true)]
		public decimal? SoLuong 
		{
			get { return GetColumnValue<decimal?>(Columns.SoLuong); }
			set { SetColumnValue(Columns.SoLuong, value); }
		}
		  
		[XmlAttribute("DonGia")]
		[Bindable(true)]
		public decimal? DonGia 
		{
			get { return GetColumnValue<decimal?>(Columns.DonGia); }
			set { SetColumnValue(Columns.DonGia, value); }
		}
		  
		[XmlAttribute("TyleTt")]
		[Bindable(true)]
		public int? TyleTt 
		{
			get { return GetColumnValue<int?>(Columns.TyleTt); }
			set { SetColumnValue(Columns.TyleTt, value); }
		}
		  
		[XmlAttribute("ThanhTien")]
		[Bindable(true)]
		public decimal? ThanhTien 
		{
			get { return GetColumnValue<decimal?>(Columns.ThanhTien); }
			set { SetColumnValue(Columns.ThanhTien, value); }
		}
		  
		[XmlAttribute("MaKhoa")]
		[Bindable(true)]
		public string MaKhoa 
		{
			get { return GetColumnValue<string>(Columns.MaKhoa); }
			set { SetColumnValue(Columns.MaKhoa, value); }
		}
		  
		[XmlAttribute("MaBacSi")]
		[Bindable(true)]
		public string MaBacSi 
		{
			get { return GetColumnValue<string>(Columns.MaBacSi); }
			set { SetColumnValue(Columns.MaBacSi, value); }
		}
		  
		[XmlAttribute("MaBenh")]
		[Bindable(true)]
		public string MaBenh 
		{
			get { return GetColumnValue<string>(Columns.MaBenh); }
			set { SetColumnValue(Columns.MaBenh, value); }
		}
		  
		[XmlAttribute("NgayYl")]
		[Bindable(true)]
		public string NgayYl 
		{
			get { return GetColumnValue<string>(Columns.NgayYl); }
			set { SetColumnValue(Columns.NgayYl, value); }
		}
		  
		[XmlAttribute("NgayKq")]
		[Bindable(true)]
		public string NgayKq 
		{
			get { return GetColumnValue<string>(Columns.NgayKq); }
			set { SetColumnValue(Columns.NgayKq, value); }
		}
		  
		[XmlAttribute("MaPttt")]
		[Bindable(true)]
		public int? MaPttt 
		{
			get { return GetColumnValue<int?>(Columns.MaPttt); }
			set { SetColumnValue(Columns.MaPttt, value); }
		}
		  
		[XmlAttribute("GoiVtyt")]
		[Bindable(true)]
		public string GoiVtyt 
		{
			get { return GetColumnValue<string>(Columns.GoiVtyt); }
			set { SetColumnValue(Columns.GoiVtyt, value); }
		}
		  
		[XmlAttribute("TenVatTu")]
		[Bindable(true)]
		public string TenVatTu 
		{
			get { return GetColumnValue<string>(Columns.TenVatTu); }
			set { SetColumnValue(Columns.TenVatTu, value); }
		}
		  
		[XmlAttribute("PhamVi")]
		[Bindable(true)]
		public int? PhamVi 
		{
			get { return GetColumnValue<int?>(Columns.PhamVi); }
			set { SetColumnValue(Columns.PhamVi, value); }
		}
		  
		[XmlAttribute("TtThau")]
		[Bindable(true)]
		public string TtThau 
		{
			get { return GetColumnValue<string>(Columns.TtThau); }
			set { SetColumnValue(Columns.TtThau, value); }
		}
		  
		[XmlAttribute("TTrantt")]
		[Bindable(true)]
		public decimal? TTrantt 
		{
			get { return GetColumnValue<decimal?>(Columns.TTrantt); }
			set { SetColumnValue(Columns.TTrantt, value); }
		}
		  
		[XmlAttribute("MucHuong")]
		[Bindable(true)]
		public int? MucHuong 
		{
			get { return GetColumnValue<int?>(Columns.MucHuong); }
			set { SetColumnValue(Columns.MucHuong, value); }
		}
		  
		[XmlAttribute("TNguonkhac")]
		[Bindable(true)]
		public string TNguonkhac 
		{
			get { return GetColumnValue<string>(Columns.TNguonkhac); }
			set { SetColumnValue(Columns.TNguonkhac, value); }
		}
		  
		[XmlAttribute("TBntt")]
		[Bindable(true)]
		public decimal? TBntt 
		{
			get { return GetColumnValue<decimal?>(Columns.TBntt); }
			set { SetColumnValue(Columns.TBntt, value); }
		}
		  
		[XmlAttribute("TBhtt")]
		[Bindable(true)]
		public decimal? TBhtt 
		{
			get { return GetColumnValue<decimal?>(Columns.TBhtt); }
			set { SetColumnValue(Columns.TBhtt, value); }
		}
		  
		[XmlAttribute("TBncct")]
		[Bindable(true)]
		public decimal? TBncct 
		{
			get { return GetColumnValue<decimal?>(Columns.TBncct); }
			set { SetColumnValue(Columns.TBncct, value); }
		}
		  
		[XmlAttribute("TNgoaids")]
		[Bindable(true)]
		public decimal? TNgoaids 
		{
			get { return GetColumnValue<decimal?>(Columns.TNgoaids); }
			set { SetColumnValue(Columns.TNgoaids, value); }
		}
		  
		[XmlAttribute("MaGiuong")]
		[Bindable(true)]
		public string MaGiuong 
		{
			get { return GetColumnValue<string>(Columns.MaGiuong); }
			set { SetColumnValue(Columns.MaGiuong, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varMaLk,int? varStt,string varMaDichVu,string varMaVatTu,string varMaNhom,string varTenDichVu,string varDonViTinh,decimal? varSoLuong,decimal? varDonGia,int? varTyleTt,decimal? varThanhTien,string varMaKhoa,string varMaBacSi,string varMaBenh,string varNgayYl,string varNgayKq,int? varMaPttt,string varGoiVtyt,string varTenVatTu,int? varPhamVi,string varTtThau,decimal? varTTrantt,int? varMucHuong,string varTNguonkhac,decimal? varTBntt,decimal? varTBhtt,decimal? varTBncct,decimal? varTNgoaids,string varMaGiuong)
		{
			Xml3917 item = new Xml3917();
			
			item.MaLk = varMaLk;
			
			item.Stt = varStt;
			
			item.MaDichVu = varMaDichVu;
			
			item.MaVatTu = varMaVatTu;
			
			item.MaNhom = varMaNhom;
			
			item.TenDichVu = varTenDichVu;
			
			item.DonViTinh = varDonViTinh;
			
			item.SoLuong = varSoLuong;
			
			item.DonGia = varDonGia;
			
			item.TyleTt = varTyleTt;
			
			item.ThanhTien = varThanhTien;
			
			item.MaKhoa = varMaKhoa;
			
			item.MaBacSi = varMaBacSi;
			
			item.MaBenh = varMaBenh;
			
			item.NgayYl = varNgayYl;
			
			item.NgayKq = varNgayKq;
			
			item.MaPttt = varMaPttt;
			
			item.GoiVtyt = varGoiVtyt;
			
			item.TenVatTu = varTenVatTu;
			
			item.PhamVi = varPhamVi;
			
			item.TtThau = varTtThau;
			
			item.TTrantt = varTTrantt;
			
			item.MucHuong = varMucHuong;
			
			item.TNguonkhac = varTNguonkhac;
			
			item.TBntt = varTBntt;
			
			item.TBhtt = varTBhtt;
			
			item.TBncct = varTBncct;
			
			item.TNgoaids = varTNgoaids;
			
			item.MaGiuong = varMaGiuong;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(long varIdXML3,string varMaLk,int? varStt,string varMaDichVu,string varMaVatTu,string varMaNhom,string varTenDichVu,string varDonViTinh,decimal? varSoLuong,decimal? varDonGia,int? varTyleTt,decimal? varThanhTien,string varMaKhoa,string varMaBacSi,string varMaBenh,string varNgayYl,string varNgayKq,int? varMaPttt,string varGoiVtyt,string varTenVatTu,int? varPhamVi,string varTtThau,decimal? varTTrantt,int? varMucHuong,string varTNguonkhac,decimal? varTBntt,decimal? varTBhtt,decimal? varTBncct,decimal? varTNgoaids,string varMaGiuong)
		{
			Xml3917 item = new Xml3917();
			
				item.IdXML3 = varIdXML3;
			
				item.MaLk = varMaLk;
			
				item.Stt = varStt;
			
				item.MaDichVu = varMaDichVu;
			
				item.MaVatTu = varMaVatTu;
			
				item.MaNhom = varMaNhom;
			
				item.TenDichVu = varTenDichVu;
			
				item.DonViTinh = varDonViTinh;
			
				item.SoLuong = varSoLuong;
			
				item.DonGia = varDonGia;
			
				item.TyleTt = varTyleTt;
			
				item.ThanhTien = varThanhTien;
			
				item.MaKhoa = varMaKhoa;
			
				item.MaBacSi = varMaBacSi;
			
				item.MaBenh = varMaBenh;
			
				item.NgayYl = varNgayYl;
			
				item.NgayKq = varNgayKq;
			
				item.MaPttt = varMaPttt;
			
				item.GoiVtyt = varGoiVtyt;
			
				item.TenVatTu = varTenVatTu;
			
				item.PhamVi = varPhamVi;
			
				item.TtThau = varTtThau;
			
				item.TTrantt = varTTrantt;
			
				item.MucHuong = varMucHuong;
			
				item.TNguonkhac = varTNguonkhac;
			
				item.TBntt = varTBntt;
			
				item.TBhtt = varTBhtt;
			
				item.TBncct = varTBncct;
			
				item.TNgoaids = varTNgoaids;
			
				item.MaGiuong = varMaGiuong;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdXML3Column
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn MaLkColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn SttColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn MaDichVuColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn MaVatTuColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn MaNhomColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn TenDichVuColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn DonViTinhColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn SoLuongColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn DonGiaColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn TyleTtColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn ThanhTienColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn MaKhoaColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn MaBacSiColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn MaBenhColumn
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayYlColumn
        {
            get { return Schema.Columns[15]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayKqColumn
        {
            get { return Schema.Columns[16]; }
        }
        
        
        
        public static TableSchema.TableColumn MaPtttColumn
        {
            get { return Schema.Columns[17]; }
        }
        
        
        
        public static TableSchema.TableColumn GoiVtytColumn
        {
            get { return Schema.Columns[18]; }
        }
        
        
        
        public static TableSchema.TableColumn TenVatTuColumn
        {
            get { return Schema.Columns[19]; }
        }
        
        
        
        public static TableSchema.TableColumn PhamViColumn
        {
            get { return Schema.Columns[20]; }
        }
        
        
        
        public static TableSchema.TableColumn TtThauColumn
        {
            get { return Schema.Columns[21]; }
        }
        
        
        
        public static TableSchema.TableColumn TTranttColumn
        {
            get { return Schema.Columns[22]; }
        }
        
        
        
        public static TableSchema.TableColumn MucHuongColumn
        {
            get { return Schema.Columns[23]; }
        }
        
        
        
        public static TableSchema.TableColumn TNguonkhacColumn
        {
            get { return Schema.Columns[24]; }
        }
        
        
        
        public static TableSchema.TableColumn TBnttColumn
        {
            get { return Schema.Columns[25]; }
        }
        
        
        
        public static TableSchema.TableColumn TBhttColumn
        {
            get { return Schema.Columns[26]; }
        }
        
        
        
        public static TableSchema.TableColumn TBncctColumn
        {
            get { return Schema.Columns[27]; }
        }
        
        
        
        public static TableSchema.TableColumn TNgoaidsColumn
        {
            get { return Schema.Columns[28]; }
        }
        
        
        
        public static TableSchema.TableColumn MaGiuongColumn
        {
            get { return Schema.Columns[29]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string IdXML3 = @"ID_XML3";
			 public static string MaLk = @"MA_LK";
			 public static string Stt = @"STT";
			 public static string MaDichVu = @"MA_DICH_VU";
			 public static string MaVatTu = @"MA_VAT_TU";
			 public static string MaNhom = @"MA_NHOM";
			 public static string TenDichVu = @"TEN_DICH_VU";
			 public static string DonViTinh = @"DON_VI_TINH";
			 public static string SoLuong = @"SO_LUONG";
			 public static string DonGia = @"DON_GIA";
			 public static string TyleTt = @"TYLE_TT";
			 public static string ThanhTien = @"THANH_TIEN";
			 public static string MaKhoa = @"MA_KHOA";
			 public static string MaBacSi = @"MA_BAC_SI";
			 public static string MaBenh = @"MA_BENH";
			 public static string NgayYl = @"NGAY_YL";
			 public static string NgayKq = @"NGAY_KQ";
			 public static string MaPttt = @"MA_PTTT";
			 public static string GoiVtyt = @"GOI_VTYT";
			 public static string TenVatTu = @"TEN_VAT_TU";
			 public static string PhamVi = @"PHAM_VI";
			 public static string TtThau = @"TT_THAU";
			 public static string TTrantt = @"T_TRANTT";
			 public static string MucHuong = @"MUC_HUONG";
			 public static string TNguonkhac = @"T_NGUONKHAC";
			 public static string TBntt = @"T_BNTT";
			 public static string TBhtt = @"T_BHTT";
			 public static string TBncct = @"T_BNCCT";
			 public static string TNgoaids = @"T_NGOAIDS";
			 public static string MaGiuong = @"MA_GIUONG";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
