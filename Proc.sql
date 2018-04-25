USE QUANLYGIANGVIEN
GO



CREATE PROCEDURE LayGiangVienTheoTen
@ten NVARCHAR(40)
AS
	SELECT * FROM dbo.GiangVien WHERE tenGiangVien like '%' + replace(@ten, '%', '[%]') + '%'
GO

CREATE PROCEDURE LayMonHocTheoTen
@ten NVARCHAR(40)
AS
	SELECT * FROM dbo.MonHoc WHERE tenMonHoc like '%' + replace(@ten, '%', '[%]') + '%'
GO
CREATE PROCEDURE SuaBuoiHocBu
	@idTietHoc int,
	@maLopHoc INT,
	@tietBatDau int,
	@tenPhong nvarchar(10),
	@ngayHoc date,
	@trangThai int,
	@ghiChu nvarchar(300)
	AS
	IF @ngayHoc	 != (SELECT TOP 1 ngayHoc FROM dbo.TietHocBu WHERE idTietHoc = @idTietHoc)
	BEGIN
	   DELETE FROM dbo.TietHocBu WHERE idTietHoc = @idTietHoc
	   INSERT INTO dbo.TietHocBu
	           ( maLopHoc ,
	             idTietHoc ,
	             tenPhong ,
	             ngayHoc ,
	             tietBatDau ,
	             trangThai ,
	             ghiChu
	           )
	   VALUES  ( @maLopHoc , -- maLopHoc - int
	             @idTietHoc , -- idTietHoc - int
	             @tenPhong , -- tenPhong - nvarchar(10)
	             @ngayHoc , -- ngayHoc - date
	             @tietBatDau, -- tietBatDau - int
	             @trangThai , -- trangThai - int
	             @ghiChu  -- ghiChu - nvarchar(300)
	           )
	END
    ELSE 
	BEGIN 
		UPDATE dbo.TietHocBu SET tenPhong = @tenPhong ,tietBatDau = @tietBatDau,trangThai = @trangThai,ghiChu =@ghiChu
	END
    
		GO
		
CREATE PROCEDURE SuaBuoiHoc
	@id int,
	
	@tenPhong nvarchar(10),
	
	@trangThai int,
	@ghiChu nvarchar(300)
	AS
	    UPDATE dbo.TietHoc SET tenPhong = @tenPhong,trangThai =@trangThai,ghiChu =@ghiChu WHERE idTietHoc = @id 
		GO
		

		
		
	
			
            
CREATE PROCEDURE UpDateTietHocTheoTiet_NgayNghi
@ngayNghi DATE,
@tietBatDau INT,
@tietKetThuc INT,
@ghiChu nvarchar(300)
AS

UPDATE dbo.TietHoc	SET  trangThai = 0 ,ghiChu = @ghiChu
WHERE ngayHoc = @ngayNghi AND maLopHoc IN (SELECT maLopHoc FROM dbo.LopHoc WHERE (tietBatDau+soTiet -1) >= @tietBatDau AND tietBatDau <= @tietKetThuc ) AND trangThai != 0 
GO

CREATE PROCEDURE UpDateTietHocTheoNgayNghi
@ngayNghi DATE,
@ghiChu NVARCHAR(300)
as

UPDATE dbo.TietHoc	SET  trangThai = 0 ,ghiChu = @ghiChu
WHERE ngayHoc = @ngayNghi AND trangThai != 0 ;
GO




CREATE PROCEDURE xoaDuLieu
AS
DELETE dbo.TietHocBu
DELETE dbo.TietHoc
DELETE dbo.LopHoc
DELETE dbo.GiangVien
DELETE dbo.MonHoc
DELETE dbo.ThongTinHoc
GO


CREATE PROCEDURE luuThongTinHoc
@ngayBatDau DATE,
@ngayKetThuc DATE,
@soTiet INT
AS
INSERT INTO dbo.ThongTinHoc
        ( ngayNhapHoc, ngayKetThuc , soTietHoc )
VALUES  ( @ngayBatDau, -- ngayNhapHoc - date
          @ngayKetThuc,  -- ngayKetThuc - date
          @soTiet
		  )
GO
CREATE PROCEDURE themGiangVien 
@maGiangVien CHAR(5),
@hoLotGiangVien NVARCHAR(60),
@tenGiangVien NVARCHAR(30)
AS
BEGIN
INSERT INTO dbo.GiangVien
        ( hoLotGiangVien ,
          tenGiangVien ,
          maGiangVien
        )
VALUES  ( @hoLotGiangVien , -- hoLotGiangVien - nvarchar(60)
          @tenGiangVien , -- tenGiangVien - nvarchar(30)
          @maGiangVien  -- maGiangVien - char(5)
        )
END
GO
CREATE PROCEDURE themLopHoc
@maLopHoc INT,
@thu INT,
@tietBatDau INT,
@soTiet INT,
@phong NVARCHAR(10),
@maLop NVARCHAR(10),
@maGiangVien CHAR(5),
@maMonHoc CHAR(7)
AS
BEGIN
INSERT INTO dbo.LopHoc
        ( maLopHoc ,
          thu ,
          tietBatDau ,
          soTiet ,
          phong ,
          maLop ,
          maGiangVien ,
          maMonHoc
        )
VALUES  ( @maLopHoc , -- maLopHoc - int
          @thu , -- thu - int
          @tietBatDau , -- tietBatDau - int
          @soTiet , -- soTiet - int
          @phong , -- phong - nvarchar(10)
          @maLop , -- maLop - nvarchar(10)
          @maGiangVien , -- maGiangVien - char(5)
          @maMonHoc  -- maMonHoc - char(7)
        )
END
GO

CREATE PROCEDURE themMonHoc
@tenMonHoc NVARCHAR(100),
@maMonHoc CHAR(7)
AS
BEGIN
	INSERT INTO dbo.MonHoc
	        ( tenMonHoc, maMonHoc )
	VALUES  ( @tenMonHoc, -- tenMonHoc - nvarchar(100)
	          @maMonHoc  -- maMonHoc - char(7)
	          )
END	
GO
CREATE PROCEDURE themTietHoc
@maLopHoc INT,
@tenPhong NVARCHAR(10),
@ngayHoc DATE,
@trangThai INT,
@ghiChu NVARCHAR(300)
AS
BEGIN
INSERT INTO dbo.TietHoc
        ( maLopHoc ,
          tenPhong ,
          ngayHoc ,
          trangThai ,
          ghiChu
        )
VALUES  ( @maLopHoc , -- maLopHoc - int
          @tenPhong , -- tenPhong - nvarchar(10)
          @ngayHoc , -- ngayHoc - date
          @trangThai , -- trangThai - int
          @ghiChu  -- ghiChu - nvarchar(300)
        )
END	
GO
CREATE PROCEDURE themTietHocBu
@maLopHoc INT,
@idTietHoc INT,
@tenPhong NVARCHAR(10),
@ngayHoc DATE,
@tietBatDau INT,
@trangThai INT,
@ghiChu NVARCHAR(300)
AS
BEGIN
 INSERT INTO dbo.TietHocBu
         ( maLopHoc ,
           idTietHoc ,
           tenPhong ,
           ngayHoc ,
           tietBatDau ,
           trangThai ,
           ghiChu
         )
 VALUES  ( @maLopHoc , -- maLopHoc - int
           @idTietHoc , -- idTietHoc - int
           @tenPhong , -- tenPhong - nvarchar(10)
           @ngayHoc , -- ngayHoc - date
           @tietBatDau , -- tietBatDau - int
           @trangThai , -- trangThai - int
           @ghiChu  -- ghiChu - nvarchar(300)
         )
END
GO
SELECT * FROM dbo.GiangVien
SELECT * FROM dbo.MonHoc


