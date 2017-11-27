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
	/// Strongly-typed collection for the TPhieuCapphatNoitru class.
	/// </summary>
    [Serializable]
	public partial class TPhieuCapphatNoitruCollection : ActiveList<TPhieuCapphatNoitru, TPhieuCapphatNoitruCollection>
	{	   
		public TPhieuCapphatNoitruCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>TPhieuCapphatNoitruCollection</returns>
		public TPhieuCapphatNoitruCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                TPhieuCapphatNoitru o = this[i];
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
	/// This is an ActiveRecord class which wraps the t_phieu_capphat_noitru table.
	/// </summary>
	[Serializable]
	public partial class TPhieuCapphatNoitru : ActiveRecord<TPhieuCapphatNoitru>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public TPhieuCapphatNoitru()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public TPhieuCapphatNoitru(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public TPhieuCapphatNoitru(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public TPhieuCapphatNoitru(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("t_phieu_capphat_noitru", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarIdCapphat = new TableSchema.TableColumn(schema);
				colvarIdCapphat.ColumnName = "id_capphat";
				colvarIdCapphat.DataType = DbType.Int32;
				colvarIdCapphat.MaxLength = 0;
				colvarIdCapphat.AutoIncrement = true;
				colvarIdCapphat.IsNullable = false;
				colvarIdCapphat.IsPrimaryKey = true;
				colvarIdCapphat.IsForeignKey = false;
				colvarIdCapphat.IsReadOnly = false;
				colvarIdCapphat.DefaultSetting = @"";
				colvarIdCapphat.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdCapphat);
				
				TableSchema.TableColumn colvarTuNgay = new TableSchema.TableColumn(schema);
				colvarTuNgay.ColumnName = "tu_ngay";
				colvarTuNgay.DataType = DbType.DateTime;
				colvarTuNgay.MaxLength = 0;
				colvarTuNgay.AutoIncrement = false;
				colvarTuNgay.IsNullable = false;
				colvarTuNgay.IsPrimaryKey = false;
				colvarTuNgay.IsForeignKey = false;
				colvarTuNgay.IsReadOnly = false;
				colvarTuNgay.DefaultSetting = @"";
				colvarTuNgay.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTuNgay);
				
				TableSchema.TableColumn colvarDenNgay = new TableSchema.TableColumn(schema);
				colvarDenNgay.ColumnName = "den_ngay";
				colvarDenNgay.DataType = DbType.DateTime;
				colvarDenNgay.MaxLength = 0;
				colvarDenNgay.AutoIncrement = false;
				colvarDenNgay.IsNullable = false;
				colvarDenNgay.IsPrimaryKey = false;
				colvarDenNgay.IsForeignKey = false;
				colvarDenNgay.IsReadOnly = false;
				colvarDenNgay.DefaultSetting = @"";
				colvarDenNgay.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDenNgay);
				
				TableSchema.TableColumn colvarIdKhoaLinh = new TableSchema.TableColumn(schema);
				colvarIdKhoaLinh.ColumnName = "id_khoa_linh";
				colvarIdKhoaLinh.DataType = DbType.Int32;
				colvarIdKhoaLinh.MaxLength = 0;
				colvarIdKhoaLinh.AutoIncrement = false;
				colvarIdKhoaLinh.IsNullable = false;
				colvarIdKhoaLinh.IsPrimaryKey = false;
				colvarIdKhoaLinh.IsForeignKey = false;
				colvarIdKhoaLinh.IsReadOnly = false;
				colvarIdKhoaLinh.DefaultSetting = @"";
				colvarIdKhoaLinh.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdKhoaLinh);
				
				TableSchema.TableColumn colvarIdKhoXuat = new TableSchema.TableColumn(schema);
				colvarIdKhoXuat.ColumnName = "id_kho_xuat";
				colvarIdKhoXuat.DataType = DbType.Int32;
				colvarIdKhoXuat.MaxLength = 0;
				colvarIdKhoXuat.AutoIncrement = false;
				colvarIdKhoXuat.IsNullable = false;
				colvarIdKhoXuat.IsPrimaryKey = false;
				colvarIdKhoXuat.IsForeignKey = false;
				colvarIdKhoXuat.IsReadOnly = false;
				colvarIdKhoXuat.DefaultSetting = @"";
				colvarIdKhoXuat.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdKhoXuat);
				
				TableSchema.TableColumn colvarIdNhanvien = new TableSchema.TableColumn(schema);
				colvarIdNhanvien.ColumnName = "id_nhanvien";
				colvarIdNhanvien.DataType = DbType.Int32;
				colvarIdNhanvien.MaxLength = 0;
				colvarIdNhanvien.AutoIncrement = false;
				colvarIdNhanvien.IsNullable = false;
				colvarIdNhanvien.IsPrimaryKey = false;
				colvarIdNhanvien.IsForeignKey = false;
				colvarIdNhanvien.IsReadOnly = false;
				colvarIdNhanvien.DefaultSetting = @"";
				colvarIdNhanvien.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdNhanvien);
				
				TableSchema.TableColumn colvarIdNhanviencapphat = new TableSchema.TableColumn(schema);
				colvarIdNhanviencapphat.ColumnName = "id_nhanviencapphat";
				colvarIdNhanviencapphat.DataType = DbType.Int32;
				colvarIdNhanviencapphat.MaxLength = 0;
				colvarIdNhanviencapphat.AutoIncrement = false;
				colvarIdNhanviencapphat.IsNullable = true;
				colvarIdNhanviencapphat.IsPrimaryKey = false;
				colvarIdNhanviencapphat.IsForeignKey = false;
				colvarIdNhanviencapphat.IsReadOnly = false;
				colvarIdNhanviencapphat.DefaultSetting = @"";
				colvarIdNhanviencapphat.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdNhanviencapphat);
				
				TableSchema.TableColumn colvarNgayNhap = new TableSchema.TableColumn(schema);
				colvarNgayNhap.ColumnName = "ngay_nhap";
				colvarNgayNhap.DataType = DbType.DateTime;
				colvarNgayNhap.MaxLength = 0;
				colvarNgayNhap.AutoIncrement = false;
				colvarNgayNhap.IsNullable = false;
				colvarNgayNhap.IsPrimaryKey = false;
				colvarNgayNhap.IsForeignKey = false;
				colvarNgayNhap.IsReadOnly = false;
				colvarNgayNhap.DefaultSetting = @"";
				colvarNgayNhap.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayNhap);
				
				TableSchema.TableColumn colvarLoaiPhieu = new TableSchema.TableColumn(schema);
				colvarLoaiPhieu.ColumnName = "loai_phieu";
				colvarLoaiPhieu.DataType = DbType.Byte;
				colvarLoaiPhieu.MaxLength = 0;
				colvarLoaiPhieu.AutoIncrement = false;
				colvarLoaiPhieu.IsNullable = true;
				colvarLoaiPhieu.IsPrimaryKey = false;
				colvarLoaiPhieu.IsForeignKey = false;
				colvarLoaiPhieu.IsReadOnly = false;
				colvarLoaiPhieu.DefaultSetting = @"";
				colvarLoaiPhieu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLoaiPhieu);
				
				TableSchema.TableColumn colvarKieuThuocVt = new TableSchema.TableColumn(schema);
				colvarKieuThuocVt.ColumnName = "kieu_thuoc_vt";
				colvarKieuThuocVt.DataType = DbType.String;
				colvarKieuThuocVt.MaxLength = 5;
				colvarKieuThuocVt.AutoIncrement = false;
				colvarKieuThuocVt.IsNullable = true;
				colvarKieuThuocVt.IsPrimaryKey = false;
				colvarKieuThuocVt.IsForeignKey = false;
				colvarKieuThuocVt.IsReadOnly = false;
				colvarKieuThuocVt.DefaultSetting = @"";
				colvarKieuThuocVt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarKieuThuocVt);
				
				TableSchema.TableColumn colvarSoPhieu = new TableSchema.TableColumn(schema);
				colvarSoPhieu.ColumnName = "so_phieu";
				colvarSoPhieu.DataType = DbType.String;
				colvarSoPhieu.MaxLength = 15;
				colvarSoPhieu.AutoIncrement = false;
				colvarSoPhieu.IsNullable = true;
				colvarSoPhieu.IsPrimaryKey = false;
				colvarSoPhieu.IsForeignKey = false;
				colvarSoPhieu.IsReadOnly = false;
				colvarSoPhieu.DefaultSetting = @"";
				colvarSoPhieu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSoPhieu);
				
				TableSchema.TableColumn colvarNgayXacnhan = new TableSchema.TableColumn(schema);
				colvarNgayXacnhan.ColumnName = "ngay_xacnhan";
				colvarNgayXacnhan.DataType = DbType.DateTime;
				colvarNgayXacnhan.MaxLength = 0;
				colvarNgayXacnhan.AutoIncrement = false;
				colvarNgayXacnhan.IsNullable = true;
				colvarNgayXacnhan.IsPrimaryKey = false;
				colvarNgayXacnhan.IsForeignKey = false;
				colvarNgayXacnhan.IsReadOnly = false;
				colvarNgayXacnhan.DefaultSetting = @"";
				colvarNgayXacnhan.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayXacnhan);
				
				TableSchema.TableColumn colvarNguoiXacnhan = new TableSchema.TableColumn(schema);
				colvarNguoiXacnhan.ColumnName = "nguoi_xacnhan";
				colvarNguoiXacnhan.DataType = DbType.String;
				colvarNguoiXacnhan.MaxLength = 30;
				colvarNguoiXacnhan.AutoIncrement = false;
				colvarNguoiXacnhan.IsNullable = true;
				colvarNguoiXacnhan.IsPrimaryKey = false;
				colvarNguoiXacnhan.IsForeignKey = false;
				colvarNguoiXacnhan.IsReadOnly = false;
				colvarNguoiXacnhan.DefaultSetting = @"";
				colvarNguoiXacnhan.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNguoiXacnhan);
				
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
				
				TableSchema.TableColumn colvarTrangThai = new TableSchema.TableColumn(schema);
				colvarTrangThai.ColumnName = "trang_thai";
				colvarTrangThai.DataType = DbType.Int16;
				colvarTrangThai.MaxLength = 0;
				colvarTrangThai.AutoIncrement = false;
				colvarTrangThai.IsNullable = false;
				colvarTrangThai.IsPrimaryKey = false;
				colvarTrangThai.IsForeignKey = false;
				colvarTrangThai.IsReadOnly = false;
				
						colvarTrangThai.DefaultSetting = @"((0))";
				colvarTrangThai.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTrangThai);
				
				TableSchema.TableColumn colvarMotaThem = new TableSchema.TableColumn(schema);
				colvarMotaThem.ColumnName = "mota_them";
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
				
				TableSchema.TableColumn colvarIdChot = new TableSchema.TableColumn(schema);
				colvarIdChot.ColumnName = "id_chot";
				colvarIdChot.DataType = DbType.Int32;
				colvarIdChot.MaxLength = 0;
				colvarIdChot.AutoIncrement = false;
				colvarIdChot.IsNullable = true;
				colvarIdChot.IsPrimaryKey = false;
				colvarIdChot.IsForeignKey = false;
				colvarIdChot.IsReadOnly = false;
				colvarIdChot.DefaultSetting = @"";
				colvarIdChot.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdChot);
				
				TableSchema.TableColumn colvarNgayChot = new TableSchema.TableColumn(schema);
				colvarNgayChot.ColumnName = "ngay_chot";
				colvarNgayChot.DataType = DbType.DateTime;
				colvarNgayChot.MaxLength = 0;
				colvarNgayChot.AutoIncrement = false;
				colvarNgayChot.IsNullable = true;
				colvarNgayChot.IsPrimaryKey = false;
				colvarNgayChot.IsForeignKey = false;
				colvarNgayChot.IsReadOnly = false;
				colvarNgayChot.DefaultSetting = @"";
				colvarNgayChot.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayChot);
				
				TableSchema.TableColumn colvarNguoiChot = new TableSchema.TableColumn(schema);
				colvarNguoiChot.ColumnName = "nguoi_chot";
				colvarNguoiChot.DataType = DbType.Int32;
				colvarNguoiChot.MaxLength = 0;
				colvarNguoiChot.AutoIncrement = false;
				colvarNguoiChot.IsNullable = true;
				colvarNguoiChot.IsPrimaryKey = false;
				colvarNguoiChot.IsForeignKey = false;
				colvarNguoiChot.IsReadOnly = false;
				colvarNguoiChot.DefaultSetting = @"";
				colvarNguoiChot.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNguoiChot);
				
				TableSchema.TableColumn colvarTrangthaiChot = new TableSchema.TableColumn(schema);
				colvarTrangthaiChot.ColumnName = "trangthai_chot";
				colvarTrangthaiChot.DataType = DbType.Byte;
				colvarTrangthaiChot.MaxLength = 0;
				colvarTrangthaiChot.AutoIncrement = false;
				colvarTrangthaiChot.IsNullable = true;
				colvarTrangthaiChot.IsPrimaryKey = false;
				colvarTrangthaiChot.IsForeignKey = false;
				colvarTrangthaiChot.IsReadOnly = false;
				colvarTrangthaiChot.DefaultSetting = @"";
				colvarTrangthaiChot.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTrangthaiChot);
				
				TableSchema.TableColumn colvarNguoiHuychot = new TableSchema.TableColumn(schema);
				colvarNguoiHuychot.ColumnName = "nguoi_huychot";
				colvarNguoiHuychot.DataType = DbType.Int32;
				colvarNguoiHuychot.MaxLength = 0;
				colvarNguoiHuychot.AutoIncrement = false;
				colvarNguoiHuychot.IsNullable = true;
				colvarNguoiHuychot.IsPrimaryKey = false;
				colvarNguoiHuychot.IsForeignKey = false;
				colvarNguoiHuychot.IsReadOnly = false;
				colvarNguoiHuychot.DefaultSetting = @"";
				colvarNguoiHuychot.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNguoiHuychot);
				
				TableSchema.TableColumn colvarNgayHuychot = new TableSchema.TableColumn(schema);
				colvarNgayHuychot.ColumnName = "ngay_huychot";
				colvarNgayHuychot.DataType = DbType.DateTime;
				colvarNgayHuychot.MaxLength = 0;
				colvarNgayHuychot.AutoIncrement = false;
				colvarNgayHuychot.IsNullable = true;
				colvarNgayHuychot.IsPrimaryKey = false;
				colvarNgayHuychot.IsForeignKey = false;
				colvarNgayHuychot.IsReadOnly = false;
				colvarNgayHuychot.DefaultSetting = @"";
				colvarNgayHuychot.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayHuychot);
				
				TableSchema.TableColumn colvarLydoHuychot = new TableSchema.TableColumn(schema);
				colvarLydoHuychot.ColumnName = "lydo_huychot";
				colvarLydoHuychot.DataType = DbType.String;
				colvarLydoHuychot.MaxLength = 255;
				colvarLydoHuychot.AutoIncrement = false;
				colvarLydoHuychot.IsNullable = true;
				colvarLydoHuychot.IsPrimaryKey = false;
				colvarLydoHuychot.IsForeignKey = false;
				colvarLydoHuychot.IsReadOnly = false;
				colvarLydoHuychot.DefaultSetting = @"";
				colvarLydoHuychot.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLydoHuychot);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("t_phieu_capphat_noitru",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("IdCapphat")]
		[Bindable(true)]
		public int IdCapphat 
		{
			get { return GetColumnValue<int>(Columns.IdCapphat); }
			set { SetColumnValue(Columns.IdCapphat, value); }
		}
		  
		[XmlAttribute("TuNgay")]
		[Bindable(true)]
		public DateTime TuNgay 
		{
			get { return GetColumnValue<DateTime>(Columns.TuNgay); }
			set { SetColumnValue(Columns.TuNgay, value); }
		}
		  
		[XmlAttribute("DenNgay")]
		[Bindable(true)]
		public DateTime DenNgay 
		{
			get { return GetColumnValue<DateTime>(Columns.DenNgay); }
			set { SetColumnValue(Columns.DenNgay, value); }
		}
		  
		[XmlAttribute("IdKhoaLinh")]
		[Bindable(true)]
		public int IdKhoaLinh 
		{
			get { return GetColumnValue<int>(Columns.IdKhoaLinh); }
			set { SetColumnValue(Columns.IdKhoaLinh, value); }
		}
		  
		[XmlAttribute("IdKhoXuat")]
		[Bindable(true)]
		public int IdKhoXuat 
		{
			get { return GetColumnValue<int>(Columns.IdKhoXuat); }
			set { SetColumnValue(Columns.IdKhoXuat, value); }
		}
		  
		[XmlAttribute("IdNhanvien")]
		[Bindable(true)]
		public int IdNhanvien 
		{
			get { return GetColumnValue<int>(Columns.IdNhanvien); }
			set { SetColumnValue(Columns.IdNhanvien, value); }
		}
		  
		[XmlAttribute("IdNhanviencapphat")]
		[Bindable(true)]
		public int? IdNhanviencapphat 
		{
			get { return GetColumnValue<int?>(Columns.IdNhanviencapphat); }
			set { SetColumnValue(Columns.IdNhanviencapphat, value); }
		}
		  
		[XmlAttribute("NgayNhap")]
		[Bindable(true)]
		public DateTime NgayNhap 
		{
			get { return GetColumnValue<DateTime>(Columns.NgayNhap); }
			set { SetColumnValue(Columns.NgayNhap, value); }
		}
		  
		[XmlAttribute("LoaiPhieu")]
		[Bindable(true)]
		public byte? LoaiPhieu 
		{
			get { return GetColumnValue<byte?>(Columns.LoaiPhieu); }
			set { SetColumnValue(Columns.LoaiPhieu, value); }
		}
		  
		[XmlAttribute("KieuThuocVt")]
		[Bindable(true)]
		public string KieuThuocVt 
		{
			get { return GetColumnValue<string>(Columns.KieuThuocVt); }
			set { SetColumnValue(Columns.KieuThuocVt, value); }
		}
		  
		[XmlAttribute("SoPhieu")]
		[Bindable(true)]
		public string SoPhieu 
		{
			get { return GetColumnValue<string>(Columns.SoPhieu); }
			set { SetColumnValue(Columns.SoPhieu, value); }
		}
		  
		[XmlAttribute("NgayXacnhan")]
		[Bindable(true)]
		public DateTime? NgayXacnhan 
		{
			get { return GetColumnValue<DateTime?>(Columns.NgayXacnhan); }
			set { SetColumnValue(Columns.NgayXacnhan, value); }
		}
		  
		[XmlAttribute("NguoiXacnhan")]
		[Bindable(true)]
		public string NguoiXacnhan 
		{
			get { return GetColumnValue<string>(Columns.NguoiXacnhan); }
			set { SetColumnValue(Columns.NguoiXacnhan, value); }
		}
		  
		[XmlAttribute("NgayTao")]
		[Bindable(true)]
		public DateTime NgayTao 
		{
			get { return GetColumnValue<DateTime>(Columns.NgayTao); }
			set { SetColumnValue(Columns.NgayTao, value); }
		}
		  
		[XmlAttribute("NguoiTao")]
		[Bindable(true)]
		public string NguoiTao 
		{
			get { return GetColumnValue<string>(Columns.NguoiTao); }
			set { SetColumnValue(Columns.NguoiTao, value); }
		}
		  
		[XmlAttribute("NgaySua")]
		[Bindable(true)]
		public DateTime? NgaySua 
		{
			get { return GetColumnValue<DateTime?>(Columns.NgaySua); }
			set { SetColumnValue(Columns.NgaySua, value); }
		}
		  
		[XmlAttribute("NguoiSua")]
		[Bindable(true)]
		public string NguoiSua 
		{
			get { return GetColumnValue<string>(Columns.NguoiSua); }
			set { SetColumnValue(Columns.NguoiSua, value); }
		}
		  
		[XmlAttribute("TrangThai")]
		[Bindable(true)]
		public short TrangThai 
		{
			get { return GetColumnValue<short>(Columns.TrangThai); }
			set { SetColumnValue(Columns.TrangThai, value); }
		}
		  
		[XmlAttribute("MotaThem")]
		[Bindable(true)]
		public string MotaThem 
		{
			get { return GetColumnValue<string>(Columns.MotaThem); }
			set { SetColumnValue(Columns.MotaThem, value); }
		}
		  
		[XmlAttribute("IdChot")]
		[Bindable(true)]
		public int? IdChot 
		{
			get { return GetColumnValue<int?>(Columns.IdChot); }
			set { SetColumnValue(Columns.IdChot, value); }
		}
		  
		[XmlAttribute("NgayChot")]
		[Bindable(true)]
		public DateTime? NgayChot 
		{
			get { return GetColumnValue<DateTime?>(Columns.NgayChot); }
			set { SetColumnValue(Columns.NgayChot, value); }
		}
		  
		[XmlAttribute("NguoiChot")]
		[Bindable(true)]
		public int? NguoiChot 
		{
			get { return GetColumnValue<int?>(Columns.NguoiChot); }
			set { SetColumnValue(Columns.NguoiChot, value); }
		}
		  
		[XmlAttribute("TrangthaiChot")]
		[Bindable(true)]
		public byte? TrangthaiChot 
		{
			get { return GetColumnValue<byte?>(Columns.TrangthaiChot); }
			set { SetColumnValue(Columns.TrangthaiChot, value); }
		}
		  
		[XmlAttribute("NguoiHuychot")]
		[Bindable(true)]
		public int? NguoiHuychot 
		{
			get { return GetColumnValue<int?>(Columns.NguoiHuychot); }
			set { SetColumnValue(Columns.NguoiHuychot, value); }
		}
		  
		[XmlAttribute("NgayHuychot")]
		[Bindable(true)]
		public DateTime? NgayHuychot 
		{
			get { return GetColumnValue<DateTime?>(Columns.NgayHuychot); }
			set { SetColumnValue(Columns.NgayHuychot, value); }
		}
		  
		[XmlAttribute("LydoHuychot")]
		[Bindable(true)]
		public string LydoHuychot 
		{
			get { return GetColumnValue<string>(Columns.LydoHuychot); }
			set { SetColumnValue(Columns.LydoHuychot, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(DateTime varTuNgay,DateTime varDenNgay,int varIdKhoaLinh,int varIdKhoXuat,int varIdNhanvien,int? varIdNhanviencapphat,DateTime varNgayNhap,byte? varLoaiPhieu,string varKieuThuocVt,string varSoPhieu,DateTime? varNgayXacnhan,string varNguoiXacnhan,DateTime varNgayTao,string varNguoiTao,DateTime? varNgaySua,string varNguoiSua,short varTrangThai,string varMotaThem,int? varIdChot,DateTime? varNgayChot,int? varNguoiChot,byte? varTrangthaiChot,int? varNguoiHuychot,DateTime? varNgayHuychot,string varLydoHuychot)
		{
			TPhieuCapphatNoitru item = new TPhieuCapphatNoitru();
			
			item.TuNgay = varTuNgay;
			
			item.DenNgay = varDenNgay;
			
			item.IdKhoaLinh = varIdKhoaLinh;
			
			item.IdKhoXuat = varIdKhoXuat;
			
			item.IdNhanvien = varIdNhanvien;
			
			item.IdNhanviencapphat = varIdNhanviencapphat;
			
			item.NgayNhap = varNgayNhap;
			
			item.LoaiPhieu = varLoaiPhieu;
			
			item.KieuThuocVt = varKieuThuocVt;
			
			item.SoPhieu = varSoPhieu;
			
			item.NgayXacnhan = varNgayXacnhan;
			
			item.NguoiXacnhan = varNguoiXacnhan;
			
			item.NgayTao = varNgayTao;
			
			item.NguoiTao = varNguoiTao;
			
			item.NgaySua = varNgaySua;
			
			item.NguoiSua = varNguoiSua;
			
			item.TrangThai = varTrangThai;
			
			item.MotaThem = varMotaThem;
			
			item.IdChot = varIdChot;
			
			item.NgayChot = varNgayChot;
			
			item.NguoiChot = varNguoiChot;
			
			item.TrangthaiChot = varTrangthaiChot;
			
			item.NguoiHuychot = varNguoiHuychot;
			
			item.NgayHuychot = varNgayHuychot;
			
			item.LydoHuychot = varLydoHuychot;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varIdCapphat,DateTime varTuNgay,DateTime varDenNgay,int varIdKhoaLinh,int varIdKhoXuat,int varIdNhanvien,int? varIdNhanviencapphat,DateTime varNgayNhap,byte? varLoaiPhieu,string varKieuThuocVt,string varSoPhieu,DateTime? varNgayXacnhan,string varNguoiXacnhan,DateTime varNgayTao,string varNguoiTao,DateTime? varNgaySua,string varNguoiSua,short varTrangThai,string varMotaThem,int? varIdChot,DateTime? varNgayChot,int? varNguoiChot,byte? varTrangthaiChot,int? varNguoiHuychot,DateTime? varNgayHuychot,string varLydoHuychot)
		{
			TPhieuCapphatNoitru item = new TPhieuCapphatNoitru();
			
				item.IdCapphat = varIdCapphat;
			
				item.TuNgay = varTuNgay;
			
				item.DenNgay = varDenNgay;
			
				item.IdKhoaLinh = varIdKhoaLinh;
			
				item.IdKhoXuat = varIdKhoXuat;
			
				item.IdNhanvien = varIdNhanvien;
			
				item.IdNhanviencapphat = varIdNhanviencapphat;
			
				item.NgayNhap = varNgayNhap;
			
				item.LoaiPhieu = varLoaiPhieu;
			
				item.KieuThuocVt = varKieuThuocVt;
			
				item.SoPhieu = varSoPhieu;
			
				item.NgayXacnhan = varNgayXacnhan;
			
				item.NguoiXacnhan = varNguoiXacnhan;
			
				item.NgayTao = varNgayTao;
			
				item.NguoiTao = varNguoiTao;
			
				item.NgaySua = varNgaySua;
			
				item.NguoiSua = varNguoiSua;
			
				item.TrangThai = varTrangThai;
			
				item.MotaThem = varMotaThem;
			
				item.IdChot = varIdChot;
			
				item.NgayChot = varNgayChot;
			
				item.NguoiChot = varNguoiChot;
			
				item.TrangthaiChot = varTrangthaiChot;
			
				item.NguoiHuychot = varNguoiHuychot;
			
				item.NgayHuychot = varNgayHuychot;
			
				item.LydoHuychot = varLydoHuychot;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdCapphatColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn TuNgayColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn DenNgayColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn IdKhoaLinhColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn IdKhoXuatColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn IdNhanvienColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn IdNhanviencapphatColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayNhapColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn LoaiPhieuColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn KieuThuocVtColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn SoPhieuColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayXacnhanColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiXacnhanColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayTaoColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiTaoColumn
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        public static TableSchema.TableColumn NgaySuaColumn
        {
            get { return Schema.Columns[15]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiSuaColumn
        {
            get { return Schema.Columns[16]; }
        }
        
        
        
        public static TableSchema.TableColumn TrangThaiColumn
        {
            get { return Schema.Columns[17]; }
        }
        
        
        
        public static TableSchema.TableColumn MotaThemColumn
        {
            get { return Schema.Columns[18]; }
        }
        
        
        
        public static TableSchema.TableColumn IdChotColumn
        {
            get { return Schema.Columns[19]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayChotColumn
        {
            get { return Schema.Columns[20]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiChotColumn
        {
            get { return Schema.Columns[21]; }
        }
        
        
        
        public static TableSchema.TableColumn TrangthaiChotColumn
        {
            get { return Schema.Columns[22]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiHuychotColumn
        {
            get { return Schema.Columns[23]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayHuychotColumn
        {
            get { return Schema.Columns[24]; }
        }
        
        
        
        public static TableSchema.TableColumn LydoHuychotColumn
        {
            get { return Schema.Columns[25]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string IdCapphat = @"id_capphat";
			 public static string TuNgay = @"tu_ngay";
			 public static string DenNgay = @"den_ngay";
			 public static string IdKhoaLinh = @"id_khoa_linh";
			 public static string IdKhoXuat = @"id_kho_xuat";
			 public static string IdNhanvien = @"id_nhanvien";
			 public static string IdNhanviencapphat = @"id_nhanviencapphat";
			 public static string NgayNhap = @"ngay_nhap";
			 public static string LoaiPhieu = @"loai_phieu";
			 public static string KieuThuocVt = @"kieu_thuoc_vt";
			 public static string SoPhieu = @"so_phieu";
			 public static string NgayXacnhan = @"ngay_xacnhan";
			 public static string NguoiXacnhan = @"nguoi_xacnhan";
			 public static string NgayTao = @"ngay_tao";
			 public static string NguoiTao = @"nguoi_tao";
			 public static string NgaySua = @"ngay_sua";
			 public static string NguoiSua = @"nguoi_sua";
			 public static string TrangThai = @"trang_thai";
			 public static string MotaThem = @"mota_them";
			 public static string IdChot = @"id_chot";
			 public static string NgayChot = @"ngay_chot";
			 public static string NguoiChot = @"nguoi_chot";
			 public static string TrangthaiChot = @"trangthai_chot";
			 public static string NguoiHuychot = @"nguoi_huychot";
			 public static string NgayHuychot = @"ngay_huychot";
			 public static string LydoHuychot = @"lydo_huychot";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
