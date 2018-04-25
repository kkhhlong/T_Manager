CREATE FUNCTION fNgayTrong (@maLopHoc INT )
  RETURNS date
  AS
  BEGIN
  DECLARE @ngayHoc DATE = (SELECT MAX(ngayHoc) FROM dbo.TietHoc WHERE maLopHoc = @maLopHoc)
  WHILE @ngayHoc< (SELECT TOP 1 ngayKetThuc FROM dbo.ThongTinHoc)
  BEGIN
	SET @ngayHoc = DATEADD(DAY,7,@ngayHoc)
	IF (SELECT COUNT(*) FROM dbo.TietHocBu WHERE maLopHoc = @maLopHoc AND ngayHoc = @ngayHoc) = 0
		RETURN @ngayHoc;

  END;
  
  RETURN NULL 
 
  END
  GO

  CREATE FUNCTION fHocBuDefault (@maLopHoc INT , @buoiHoc DATE )
  RETURNS bit
  AS
  BEGIN
  DECLARE @ngayHoc DATE = (SELECT MAX(ngayHoc) FROM dbo.TietHoc WHERE maLopHoc = @maLopHoc)
  WHILE @ngayHoc< (SELECT TOP 1 ngayKetThuc FROM dbo.ThongTinHoc)
  BEGIN
	SET @ngayHoc = DATEADD(DAY,7,@ngayHoc)
	IF (@buoiHoc = @ngayHoc)
		RETURN 1;

  END;
  
  RETURN 0;
 
  END
  GO
  


CREATE TRIGGER ThemBuoiHocBu ON  dbo.TietHoc
FOR UPDATE
AS
DECLARE TrigTempUpdate_Cursor CURSOR FOR

SELECT Inserted.maLopHoc,Inserted.trangThai,Inserted.idTietHoc FROM Inserted

DECLARE @maLopHoc int, @trangThai INT , @id INT 

OPEN TrigTempUpdate_Cursor;

FETCH NEXT FROM TrigTempUpdate_Cursor INTO @maLopHoc, @trangThai, @id

WHILE @@FETCH_STATUS = 0
BEGIN
	DECLARE @ngayHoc DATE = dbo.fNgayTrong(@maLopHoc)
	DECLARE @trangThaiTietHocMoi INT
	DECLARE @tenPhong NVARCHAR(10) = (SELECT phong FROM dbo.LopHoc WHERE maLopHoc = @maLopHoc )
	DECLARE @tietBatDau INT =  (SELECT tietBatDau FROM dbo.LopHoc WHERE maLopHoc = @maLopHoc)
	
	DECLARE @tietHocBu INT = (SELECT COUNT(*) FROM dbo.TietHocBu WHERE idTietHoc =@id)

	IF (@trangThai = -1  OR @trangThai = 2)AND @tietHocBu = 1
	BEGIN
	DELETE dbo.TietHocBu WHERE idTietHoc = @id
    END
    
	
	
	IF (@trangThai = 0 OR @trangThai = 1) AND @tietHocBu=0
	BEGIN
	IF @ngayHoc < (SELECT TOP 1 NgayKetThuc FROM dbo.ThongTinHoc)
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
		          @id , -- idTietHoc - int
		          @tenPhong , -- tenPhong - nvarchar(10)
		          @ngayHoc , -- ngayHoc - date
		          @tietBatDau , -- tietBatDau - int
		          -1 , -- trangThai - int
		          N''  -- ghiChu - nvarchar(300)
		        )
				END
        ELSE
        BEGIN
		INSERT INTO dbo.TietHocBu
		        ( maLopHoc ,
		          idTietHoc ,
		          tenPhong ,
		          ngayHoc,
		          tietBatDau ,
		          trangThai ,
		          ghiChu
		        )
		VALUES  ( @maLopHoc , -- maLopHoc - int
		          @id , -- idTietHoc - int
		          @tenPhong , -- tenPhong - nvarchar(10)
		          null , -- ngayHoc - date
		          null , -- tietBatDau - int
		          -1 , -- trangThai - int
		          N'Cần thêm ngày học'  -- ghiChu - nvarchar(300)
		        )
        END
                
			END 
FETCH NEXT FROM TrigTempUpdate_Cursor INTO @maLopHoc, @trangThai,@id

		END;
		CLOSE TrigTempUpdate_Cursor;

		DEALLOCATE TrigTempUpdate_Cursor;
        GO



CREATE TRIGGER TriggerSuaBuoiHocBu ON  dbo.TietHocBu
FOR DELETE
AS
DECLARE @ngayHocDeleted  DATE = (SELECT TOP 1 Deleted.ngayHoc FROM Deleted)
DECLARE @ngayHocPre  DATE = (SELECT TOP 1 Deleted.ngayHoc FROM Deleted)
DECLARE @tietBatDau INT = (SELECT TOP 1 Deleted.tietBatDau FROM Deleted)
DECLARE @maLopHoc INT = (SELECT TOP 1 Deleted.maLopHoc FROM Deleted)
DECLARE Trig CURSOR FOR

SELECT idTietHoc,ngayHoc FROM dbo.TietHocBu WHERE ngayHoc>@ngayHocDeleted AND tietBatDau = @tietBatDau AND maLopHoc =@maLopHoc AND @ngayHocDeleted > (SELECT MAX(ngayHoc) FROM dbo.TietHoc WHERE maLopHoc =maLopHoc)
DECLARE @idTietHoc INT, @ngayHoc DATE  
 
OPEN Trig;

FETCH NEXT FROM Trig INTO @idTietHoc,@ngayHoc
WHILE @@FETCH_STATUS = 0
BEGIN
IF( dbo.fHocBuDefault(@maLopHoc,@ngayHoc)=1 AND  DATEADD(DAY,-7,@ngayHoc) = @ngayHocPre)
BEGIN	
SET @ngayHocPre = (SELECT TOP 1 ngayHoc FROM dbo.TietHocBu WHERE idTietHoc =@idTietHoc)
	UPDATE dbo.TietHocBu SET ngayHoc = DATEADD(DAY,-7,@ngayHoc) WHERE idTietHoc =@idTietHoc
	
	END
    

FETCH NEXT FROM Trig INTO @idTietHoc,@ngayHoc

		END;
		CLOSE Trig;

		DEALLOCATE Trig;
        GO
 

  
  


