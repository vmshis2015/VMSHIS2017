-- Synchronization script for .\sql2k14.VMS_Standard
-- Generated by SQL Delta on 01/12/2017 2:26:26 CH
-- Please backup VMS_Standard before executing this script
-- 
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON
GO
SET NUMERIC_ROUNDABORT OFF
GO
SET XACT_ABORT ON
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO
BEGIN TRANSACTION
GO
PRINT N'Altering Procedure [dbo].[Baocao_Chidinhcls_Chitiet]'
GO
ALTER PROCEDURE [dbo].[Baocao_Chidinhcls_Chitiet]
	@FromDate DATETIME,
	@ToDate DATETIME,
	@MaDoiTuong NVARCHAR(50),
	@Loaidichvu NVARCHAR(100),
	@CreateBy NVARCHAR(50),
	@MA_KHOA_THIEN NVARCHAR(50),
	@has_exam INT,
	@KieuBenhNhan NVARCHAR(20),
	@BacSyChiDinh NVARCHAR(500),
	@IdChiTietDichVu INT
WITH RECOMPILE
AS
	DECLARE @sfromdate NVARCHAR(10)
	SET @sfromdate = CONVERT(NVARCHAR(10), @FromDate, 103)
	SET @FromDate = dbo.trunc(@FromDate)
	SET @ToDate = dbo.trunc(@ToDate)
	BEGIN
		IF OBJECT_ID('tempdb..#tien_khoa') IS NOT NULL
		    DROP TABLE #tien_khoa
		
		SELECT THANH_TIEN,
		       PHU_THU,
		       don_gia,
		       so_luong,
		       ten_chitietdichvu,
		       username,
		       ten_nguoichidinh,
		       P.ma_doituong_kcb,
		       P.tu_tuc,
		       P.id_dichvu
		       INTO #tien_khoa
		       -- Ma_DV
		FROM   (
		           SELECT (ISNULL(tpd.don_gia, 0) * ISNULL(tpd.so_luong, 0)) AS 
		                  THANH_TIEN,
		                  tpd.so_luong,
		                  id_loaithanhtoan,
		                  (
		                      CASE id_loaithanhtoan
		                           WHEN 1 THEN 'Cong kham'
		                           WHEN 2 THEN tpd.ten_chitietdichvu
		                           ELSE tpd.ten_chitietdichvu
		                      END
		                  ) AS ten_chitietdichvu,
		                  --tpd.ten_chitietdichvu,
		                  (ISNULL(tpd.phu_thu, 0) * ISNULL(tpd.so_luong, 0)) AS 
		                  PHU_THU,
		                  TP.ma_khoa_thuchien,
		                  ISNULL(
		                      (
		                          SELECT TOP 1 [USER_NAME]
		                          FROM   dmuc_nhanvien dn
		                          WHERE  dn.id_nhanvien = tpd.id_bacsi_chidinh
		                      ),
		                      'ADMIN'
		                  ) AS username,
		                  ISNULL(
		                      (
		                          SELECT TOP 1 dn.ten_nhanvien
		                          FROM   dmuc_nhanvien dn
		                          WHERE  dn.id_nhanvien = tpd.id_bacsi_chidinh
		                      ),
		                      N'ADMIN'
		                  ) AS ten_nguoichidinh,
		                  tp.ma_doituong_kcb,
		                  tpd.tu_tuc,
		                  (
		                      CASE id_loaithanhtoan
		                           WHEN 1 THEN -100
		                           WHEN 2 THEN tpd.id_dichvu
		                           ELSE tpd.id_dichvu
		                      END
		                  ) AS id_dichvu,
		                  --tpd.id_dichvu,
		                  tpd.don_gia
		           FROM   kcb_thanhtoan_chitiet tpd
		                  JOIN (
		                           SELECT *
		                           FROM   kcb_thanhtoan tp
		                           WHERE  ISNULL(tp.noi_tru, 0) = 0
		                                  --  AND ISNULL(tp.kieu_thanhtoan, 0) = 0--0= thanh toan thu?ng. 1= b?n ghi thanh toan h?y(tr? l?i ti?n);2= Thanh toan b? vi?n
		                                  AND ( dbo.trunc(tp.ngay_thanhtoan) 
		                                             BETWEEN @FromDate AND @ToDate
		                                      )
		                       ) tp
		                       ON  tpd.id_thanhtoan = tp.id_thanhtoan
		                  LEFT  JOIN kcb_luotkham kl
		                       ON  tp.ma_luotkham = kl.ma_luotkham
		                       AND kl.id_benhnhan = tp.id_benhnhan
		                  LEFT JOIN kcb_danhsach_benhnhan kdb
		                       ON  kdb.id_benhnhan = kl.id_benhnhan
		           WHERE  ISNULL(tpd.trangthai_huy, 0) = 0
		                  AND ISNULL(tp.kieu_thanhtoan, 0) = 0
		                  AND 
		                  tpd.id_loaithanhtoan IN ( 2)--Ch? b?c ti?n kham va d?ch v? CLS
		                      
		                      --  AND (tpd.ma_doituong_kcb = @MaDoiTuong OR @MaDoiTuong = '-1')
		                  AND (
		                          @has_exam = -1
		                          OR (@has_exam = 0 AND ISNULL(tpd.id_kham, -1) = -1)
		                          OR (@has_exam = 1 AND ISNULL(tpd.id_kham, -1) > 0)
		                      )
		                  AND (
		                          @Loaidichvu = '-1'
		                          OR tpd.id_dichvu IN (SELECT *
		                                               FROM   dbo.fromStringintoIntTable(@Loaidichvu) 
		                                                      fsit)
		                      )
		                  AND (
		                          @MA_KHOA_THIEN = '-1'
		                          OR kl.ma_khoa_thuchien = @MA_KHOA_THIEN
		                      )
							    AND (@BacSyChiDinh='-1' OR
		                           Exists ( SELECT 1 from dmuc_nhanvien where id_nhanvien= tpd.id_bacsi_chidinh
								   and ma_nhanvien in (select s.items
		                                           FROM   dbo.[Split](@BacSyChiDinh, ';') 
		                                                  s))
		                      )
		                  AND (@KieuBenhNhan='ALL' OR
		                          kl.kieu_kham IN (SELECT s.items
		                                           FROM   dbo.[Split](@KieuBenhNhan, ';') 
		                                                  s)
		                      )
							  
		                  AND (
		                          @IdChiTietDichVu = -1
		                          OR (
		                                 tpd.id_chitietdichvu = @IdChiTietDichVu
		                                 AND tpd.id_loaithanhtoan IN (1, 2)
		                             )
		                      )
		       ) AS P
		
		IF @MaDoiTuong = '-1'
		BEGIN
		    SELECT (SUM(THANH_TIEN) + SUM(PHU_THU)) AS THANH_TIEN,
		           SUM(so_luong) AS so_luong,
		           ten_chitietdichvu AS ten_dichvuchitiet,
		           id_dichvu,
		           don_gia,
		           (
		               CASE id_dichvu
		                    WHEN -100 THEN N'Cong kham'
		                    ELSE (
		                             SELECT TOP 1 dd.ten_dichvu
		                             FROM   dmuc_dichvucls dd
		                             WHERE  dd.id_dichvu = P.id_dichvu
		                         )
		               END
		           ) AS ten_dichvu
		    FROM   #tien_khoa AS P
		    WHERE  (P.username = @CreateBy OR @CreateBy = '-1')
		    GROUP BY
		           ten_chitietdichvu,
		           id_dichvu,
		           don_gia
		    ORDER BY
		           id_dichvu
		END
		ELSE
		BEGIN
		    IF @MaDoiTuong = 'BHYT'
		    BEGIN
		        SELECT (SUM(THANH_TIEN) + SUM(PHU_THU)) AS THANH_TIEN,
		               SUM(so_luong) AS so_luong,
		               ten_chitietdichvu AS ten_dichvuchitiet,
		               id_dichvu,
		               don_gia,
		               (
		                   CASE id_dichvu
		                        WHEN -100 THEN 'Cong kham'
		                        ELSE (
		                                 SELECT TOP 1 dd.ten_dichvu
		                                 FROM   dmuc_dichvucls dd
		                                 WHERE  dd.id_dichvu = P.id_dichvu
		                             )
		                   END
		               ) AS ten_dichvu
		        FROM   #tien_khoa AS P
		        WHERE  (P.username = @CreateBy OR @CreateBy = '-1')
		               AND P.ma_doituong_kcb = @MaDoiTuong
		                    AND P.tu_tuc = 0
		        GROUP BY
		               ten_chitietdichvu,
		               id_dichvu,
		               don_gia
		        ORDER BY
		               id_dichvu
		    END
		    ELSE
		    BEGIN
		        SELECT (SUM(THANH_TIEN) + SUM(PHU_THU)) AS THANH_TIEN,
		               SUM(so_luong) AS so_luong,
		               ten_chitietdichvu AS ten_dichvuchitiet,
		               id_dichvu,
		               don_gia,
		               (
		                   CASE id_dichvu
		                        WHEN -100 THEN 'Cong kham'
		                        ELSE (
		                                 SELECT TOP 1 dd.ten_dichvu
		                                 FROM   dmuc_dichvucls dd
		                                 WHERE  dd.id_dichvu = P.id_dichvu
		                             )
		                   END
		               ) AS ten_dichvu
		        FROM   #tien_khoa AS P
		        WHERE  (P.username = @CreateBy OR @CreateBy = '-1')
		               AND (P.ma_doituong_kcb = @MaDoiTuong OR P.tu_tuc = 1)
		        GROUP BY
		               ten_chitietdichvu,
		               id_dichvu,
		               don_gia
		        ORDER BY
		               id_dichvu
		    END
		END
	END
GO
IF (@@TRANCOUNT > 0)
  IF (@@ERROR <> 0) ROLLBACK TRANSACTION ELSE COMMIT TRANSACTION
GO
IF (@@TRANCOUNT = 0) BEGIN TRANSACTION
GO
PRINT N'Altering Procedure [dbo].[Baocao_Tiepdonbenhnhan_Chitiet]'
GO

ALTER PROCEDURE [dbo].[Baocao_Tiepdonbenhnhan_Chitiet]
	@Object_Type INT,
	@FromDate [datetime],
	@ToDate [datetime],
	@nguoi_tao NVARCHAR(30),
	@Deparment_CODE NVARCHAR(10),
	@loaiBN NVARCHAR(20),
	@IdPhong INT
WITH  

 EXECUTE AS CALLER
AS
DECLARE @sfromDate NVARCHAR(10)
SET @sfromDate = CONVERT(NVARCHAR(10), @FromDate, 103)
SELECT tpe.ma_luotkham,
       tpi.ten_benhnhan,
       tpi.nam_sinh,
       (YEAR(GETDATE()) -tpi.nam_sinh) AS Tuoi,
       gioi_tinh,
       CONVERT(NVARCHAR(10), tpe.ngay_tiepdon, 103) AS ngay_tiepdon,
       tpe.nguoi_tiepdon, 
       (SELECT TOP 1 dn.ten_nhanvien
          FROM dmuc_nhanvien dn WHERE dn.ma_nhanvien = tpe.nguoi_tiepdon) AS ten_nhanvien,
       lot.id_doituong_kcb,
       lot.ten_doituong_kcb,
       tpe.id_benhnhan,
       tpe.mathe_bhyt,
       tpi.dia_chi
FROM   kcb_luotkham tpe
       --JOIN kcb_thanhtoan kt ON kt.ma_luotkham = tpe.ma_luotkham AND kt.id_benhnhan = tpe.id_benhnhan
       INNER JOIN kcb_danhsach_benhnhan tpi
            ON  tpi.id_benhnhan = tpe.id_benhnhan
       INNER JOIN dmuc_doituongkcb lot
            ON  lot.id_doituong_kcb = tpe.id_doituong_kcb
            --INNER JOIN kcb_dangky_kcb kdk ON kdk.ma_luotkham = kt.ma_luotkham
WHERE  (tpe.id_doituong_kcb = @Object_Type OR @Object_Type = -1)
       AND (tpe.kieu_kham = @loaiBN )
       AND (
               tpe.ma_khoa_thuchien = @Deparment_CODE
               OR @Deparment_CODE = N'-1'
           )
       AND (
               @sfromDate = '01/01/1900'
               OR dbo.trunc(tpe.ngay_tiepdon) BETWEEN dbo.trunc(@FromDate) AND 
                  dbo.trunc(@ToDate)
           )
       AND (@nguoi_tao = '-1' OR tpe.nguoi_tiepdon = @nguoi_tao)
       --AND (@IdPhong = -1 OR kdk.id_phongkham = @IdPhong)
GO
IF (@@TRANCOUNT > 0)
  IF (@@ERROR <> 0) ROLLBACK TRANSACTION ELSE COMMIT TRANSACTION
GO
IF (@@TRANCOUNT = 0) BEGIN TRANSACTION
GO
PRINT N'Altering Procedure [dbo].[Kcb_Thamkham_Laydulieu_Inphieu_Cls]'
GO


ALTER PROCEDURE [dbo].[Kcb_Thamkham_Laydulieu_Inphieu_Cls]
	@ma_chidinh NVARCHAR(50),
	@NhomInCls NVARCHAR(20),
	@PatientCode NVARCHAR(50),
	@Patient_ID INT
WITH RECOMPILE
AS
	UPDATE kcb_chidinhcls
	SET    tinhtrang_in = 1
	WHERE  ma_chidinh = @ma_chidinh
	DECLARE @CHIDINH_BODAUCHAM_TRENMAVACH NVARCHAR
	set @CHIDINH_BODAUCHAM_TRENMAVACH=isnull((SELECT TOP 1 sValue
	                                            FROM Sys_SystemParameters
	WHERE sName='CHIDINH_BODAUCHAM_TRENMAVACH'),'0')
	IF OBJECT_ID('tempdb..#chidinhcls') IS NOT NULL
	    DROP TABLE #chidinhcls
	
	--DECLARE @ChanDoan NVARCHAR(500);
	--SET @ChanDoan = dbo.LayThongTinChanDoan(@PatientCode, @Patient_ID)
	
	SELECT p.*,
	       0 AS CHON,
	       (
	           CASE 
	                WHEN ISNULL(p.noitru, 0) = 1 THEN c.ptram_bhyt_goc
	                ELSE c.ptram_bhyt
	           END
	       ) AS ptram_bhyt_noitru,
	       (
	           (ISNULL(c.don_gia, 0) + ISNULL(c.phu_thu, 0)) * ISNULL(c.so_luong, 0)
	       ) AS TT,
	       (
	           (ISNULL(c.bnhan_chitra, 0) + ISNULL(c.phu_thu, 0)) * ISNULL(c.so_luong, 0)
	       ) AS TT_BN,
	       (ISNULL(c.bnhan_chitra, 0) * ISNULL(c.so_luong, 0)) AS 
	       TT_BN_KHONG_PHUTHU,
	       (ISNULL(c.bhyt_chitra, 0) * ISNULL(c.so_luong, 0)) AS TT_BHYT,
	       (ISNULL(c.don_gia, 0) * ISNULL(c.so_luong, 0)) AS TT_KHONG_PHUTHU,
	       (ISNULL(c.phu_thu, 0) * ISNULL(c.so_luong, 0)) AS TT_PHUTHU,
	       (
	           SELECT TOP 1 ten_khoaphong
	           FROM   dmuc_khoaphong WITH(NOLOCK)
	           WHERE  id_khoaphong = p.id_phong_chidinh
	       ) AS ten_phongchidinh,
	       (
	           SELECT TOP 1ten_khoaphong
	           FROM   dmuc_khoaphong WITH(NOLOCK)
	           WHERE  id_khoaphong = p.id_khoa_chidinh
	       ) AS ten_khoachidinh,
	       (
	           SELECT TOP 1 ten_nhanvien
	           FROM   dmuc_nhanvien WITH(NOLOCK)
	           WHERE  id_nhanvien = p.id_bacsi_chidinh
	       ) AS ten_bacsi_chidinh,
	       (
	           SELECT TOP 1 ten_doituong_kcb
	           FROM   dmuc_doituongkcb WITH(NOLOCK)
	           WHERE  id_doituong_kcb = p.id_doituong_kcb
	       ) AS ten_doituong_kcb,
	       c.id_chitietchidinh,
	       c.id_chitietdichvu,
	       c.id_dichvu,
	       c.ptram_bhyt,
	       c.ptram_bhyt_goc,
	       c.gia_danhmuc,
	       c.don_gia,
	       c.phu_thu,
	       c.bhyt_chitra,
	       c.bnhan_chitra,
	       c.id_loaichidinh,
	       c.trangthai_thanhtoan AS tinhtrang_thanhtoan_chitiet,
	       c.ngay_thanhtoan AS ngay_thanhtoan_chitiet,
	       c.trangthai_huy,
	       c.tu_tuc,
	       c.so_luong,
	       c.trang_thai AS trangthai_chitiet,
	       c.trangthai_bhyt,
	       c.hienthi_baocao,
	       c.id_thanhtoan,
	       c.id_khoa_thuchien,
	       c.id_phong_thuchien,
	       c.FTPImage,
	       d.NHOM_CLS,
	       d.ten_khoa_thuchien,
	       d.ten_khoa_thuchien_chitiet,
	       d.ten_phong_thuchien,
	       d.ten_phong_thuchien_chitiet,
	       d.chi_dan,
	       d.chidan_dichvu,
	       d.ma_chitietdichvu,
	       d.ma_chitietdichvu_bhyt,
	       d.ten_chitietdichvu_bhyt AS ten_chitietdichvu,
	       d.ten_chitietdichvu_bhyt,
	       d.ma_dichvu,
	       d.ma_bhyt,
	       d.ten_dichvu,
	       d.ten_bhyt,
	       d.ma_loaidichvu,
	       d.ten_loaidichvu,
	       d.stt_hthi_dichvu,
	       d.stt_hthi_chitiet,
	       d.co_chitiet,
	       d.ten_nhombaocao_chitiet,
	       d.ten_donvitinh,
	       d.idvungkhaosat,
	       d.id_vungkhaosat,
	       d.tenvungkhaosat,
	       d.ten_vungkhaosat_chitiet,
	       d.ten_nhombaocao_dichvu,
	       d.ten_nhominphieucls,
	       ISNULL(d.nhom_in_cls, 'ALL') AS nhom_in_cls,
	       d.nhom_baocao_dichvu,
	       d.stt_hthi_nhominphieucls,
	       d.binhthuong_nam,
	       d.binhthuong_nu,
	       c.id_bacsi_thuchien,
	       c.nguoi_thuchien,
	       c.ngay_thuchien,
	       c.imgPath1,
	       c.imgPath2,
	       c.imgPath3,
	       c.imgPath4,
	       c.id_goi,
	       ISNULL(c.trong_goi, 0) AS trong_goi,
	       ISNULL(CONVERT(NVARCHAR(1000), c.ket_qua), '') AS ket_qua
	       INTO #chidinhcls
	FROM   (
	           SELECT *
	           FROM   kcb_chidinhcls kc WITH(NOLOCK)
	           WHERE  kc.id_benhnhan = @Patient_ID
	                  AND kc.ma_luotkham = @PatientCode
	                  AND kc.ma_chidinh = @ma_chidinh
	       ) p
	       INNER JOIN (
	                SELECT *
	                FROM   kcb_chidinhcls_chitiet kcc WITH(NOLOCK)
	                WHERE  kcc.id_benhnhan = @Patient_ID
	                       AND kcc.ma_luotkham = @PatientCode
	            ) AS c
	            ON  p.id_chidinh = c.id_chidinh
	       INNER JOIN (SELECT * FROM v_dmuc_dichvucls_chitiet d WHERE  @NhomInCls = 'ALL'
	           OR (d.nhom_in_cls = 'ALL' OR d.nhom_in_cls = @NhomInCls)) d 
	            ON  d.id_chitietdichvu = c.id_chitietdichvu
	
	
	SELECT p.id_dichvu,
	       p.id_chitietdichvu,
	       p.ma_dichvu,
	       (case @CHIDINH_BODAUCHAM_TRENMAVACH WHEN '0' then p.ma_chidinh ELSE REPLACE(p.ma_chidinh,'.','') END )as ma_chidinh,
	       p.stt_hthi_dichvu,
	       p.stt_hthi_chitiet,
	       p.ten_dichvu,
	       p.ten_chitietdichvu,
	       p.ten_loaidichvu,
	       p.id_chidinh,
	       p.id_chitietchidinh,
	       p.ma_chidinh,	--p.barcode,
	       p.so_luong,
	       p.don_gia,
	       p.bhyt_chitra,
	       p.bnhan_chitra,
	       p.gia_danhmuc,
	       p.phu_thu,
	       p.ptram_bhyt,
	       p.hienthi_baocao,
	       p.id_bacsi_chidinh,
	       tpi.id_benhnhan,
	       tpi.ten_benhnhan,
	       tpi.dia_chi,
	       tpi.diachi_bhyt,
	       tpi.gioi_tinh,
	       (YEAR(GETDATE()) - tpi.nam_sinh) AS TUOI,
	       tpi.ngay_sinh,
	       tpi.nam_sinh,
	       tpi.dan_toc,
	       tpi.CMT,
	       tpi.dien_thoai,
	       tpi.nghe_nghiep,
	       tpi.ton_giao,
	       tpi.ngay_tiepdon,
	       (
	           SELECT TOP 1 kck.chandoan
	           FROM   kcb_chandoan_ketluan kck WITH(NOLOCK)
	           WHERE  tpe.ma_luotkham = kck.ma_luotkham
	       ) AS chan_doan,
	       tpe.chandoan_kemtheo,
	       tpe.dung_tuyen,
	       tpe.huong_dieutri,
	       tpe.ma_luotkham,
	       tpe.ngaybatdau_bhyt,
	       ngayketthuc_bhyt,
	       noi_dongtruso_kcbbd,
	       noicap_bhyt,
	       tpe.mathe_bhyt,
	       ma_quyenloi,
	       mabenh_chinh,
	       mabenh_phu,
	       ma_doituong_bhyt,
	       tpe.ma_doituong_kcb,
	       tpe.id_doituong_kcb,
	       p.id_kham,
	       (
	           SELECT TOP 1 ten_doituong_kcb
	           FROM   dmuc_doituongkcb WITH(NOLOCK)
	           WHERE  id_doituong_kcb = tpe.id_doituong_kcb
	       ) AS ten_doituong_kcb,
	       p.ten_khoa_thuchien,
	       p.ten_khoa_thuchien_chitiet,
	       p.ten_phong_thuchien,
	       p.ten_phong_thuchien_chitiet,
	       (p.ten_khoa_thuchien + '-' + p.ten_phong_thuchien) AS 
	       noithuchien_dichvu,
	       (
	           p.ten_khoa_thuchien_chitiet + '-' + p.ten_phong_thuchien_chitiet
	       ) AS noithuchien_dichvu_chitiet,
	       p.chi_dan AS chidan_chitiet,
	       p.chidan_dichvu,
	       P.ten_bacsi_chidinh,
	       P.ten_khoachidinh,
	       P.ten_phongchidinh,
	       P.ten_nhombaocao_chitiet,
	       P.ten_donvitinh,
	       P.ten_nhombaocao_dichvu,
	       P.ten_nhominphieucls,
	       P.nhom_in_cls,
	       P.nhom_baocao_dichvu,
	       (
	           CASE 
	                WHEN LEN(chi_dan) > 0 THEN chi_dan
	                ELSE chidan_dichvu
	           END
	       ) AS chi_dan
	FROM   (
	           SELECT *
	           FROM   #chidinhcls cd 
	           WHERE  cd.ma_chidinh = @ma_chidinh
	       ) P
	       JOIN (
	                SELECT *
	                FROM   kcb_danhsach_benhnhan kdb WITH(NOLOCK)
	                WHERE  kdb.id_benhnhan = @Patient_ID
	            ) AS tpi
	            ON  tpi.id_benhnhan = P.id_benhnhan
	       JOIN (
	                SELECT *
	                FROM   kcb_luotkham kl WITH(NOLOCK)
	                WHERE  kl.id_benhnhan = @Patient_ID
	                       AND kl.ma_luotkham = @PatientCode
	            ) AS tpe
	            ON  tpe.ma_luotkham = P.ma_luotkham
	WHERE  (
	           @NhomInCls = 'ALL'
	           OR (P.nhom_in_cls = 'ALL' OR P.nhom_in_cls = @NhomInCls)
	       )
	ORDER BY
	       P.stt_hthi_nhominphieucls,
	       stt_hthi_dichvu,
	       P.stt_hthi_chitiet,
	       p.ten_chitietdichvu
GO
IF (@@TRANCOUNT > 0)
  IF (@@ERROR <> 0) ROLLBACK TRANSACTION ELSE COMMIT TRANSACTION
GO
IF (@@TRANCOUNT = 0) BEGIN TRANSACTION
GO
PRINT N'Altering Procedure [dbo].[reset_kcb]'
GO
ALTER PROCEDURE [dbo].[reset_kcb]
AS
BEGIN
	TRUNCATE TABLE hoadon_logs


TRUNCATE TABLE kcb_benh_an
TRUNCATE TABLE kcb_chandoan_ketluan
TRUNCATE TABLE kcb_chidinhcls
TRUNCATE TABLE kcb_chidinhcls_chitiet
TRUNCATE TABLE kcb_dangky_kcb
TRUNCATE TABLE kcb_danhsach_benhnhan
TRUNCATE TABLE kcb_donthuoc
TRUNCATE TABLE kcb_donthuoc_chitiet
TRUNCATE TABLE tbl_kedonthuoc_tempt
TRUNCATE TABLE kcb_ketqua_cls
TRUNCATE TABLE kcb_lichsu_inphoi_bhyt
TRUNCATE TABLE kcb_luotkham
TRUNCATE TABLE kcb_lichsu_doituong_kcb
TRUNCATE TABLE kcb_dangky_sokham
TRUNCATE TABLE kcb_phieu_dct
TRUNCATE TABLE kcb_phieuthu
TRUNCATE TABLE kcb_qms
TRUNCATE TABLE kcb_thanhtoan
TRUNCATE TABLE kcb_thanhtoan_chitiet
TRUNCATE TABLE kcb_dmuc_luotkham
TRUNCATE TABLE kcb_phieuchuyenvien
TRUNCATE TABLE kcb_loghuy
TRUNCATE TABLE kcb_thanhtoan_phanbotheoPTTT
TRUNCATE TABLE kcb_ketqua_ha
TRUNCATE TABLE t_log


TRUNCATE TABLE noitru_phanbuonggiuong
TRUNCATE TABLE noitru_phieudieutri
TRUNCATE TABLE noitru_tamung

TRUNCATE TABLE noitru_phieuravien
TRUNCATE TABLE noitru_phieudinhduong
--Truncate cac bang lien quan den cap phat thuoc noi tru
TRUNCATE TABLE t_thuoc_capphat_chitiet
TRUNCATE TABLE t_phieu_capphat_noitru
TRUNCATE TABLE t_phieu_capphat_chitiet

TRUNCATE TABLE t_phieu_xuatthuoc_benhnhan
TRUNCATE TABLE t_phieu_xuatthuoc_benhnhan_chitiet
TRUNCATE TABLE t_xuatthuoc_theodon

TRUNCATE TABLE t_phieu_capphat_noitru
TRUNCATE TABLE t_phieu_capphat_chitiet
TRUNCATE TABLE t_thuoc_capphat_chitiet

UPDATE noitru_dmuc_giuongbenh
SET
	dang_sudung=0

END
GO
IF (@@TRANCOUNT > 0)
  IF (@@ERROR <> 0) ROLLBACK TRANSACTION ELSE COMMIT TRANSACTION
GO
PRINT 'Script deployment completed'
GO

