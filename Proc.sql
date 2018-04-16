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
dbo.UpDateTietHocTheoNgayNghi @ngayNghi = '2018-04-19', -- date
    @ghiChu = N'' -- nvarchar(300)



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
