using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T_Manager.DTO;

namespace T_Manager.DAO
{
    //Tìm kiếm tiết học theo Giảng Viên > Môn > Ngày > Phòng  return list
    //Tìm kiếm tiết học theo Hôm nay return list
    //Tìm kiếm tiết học theo Giảng Viên > Môn > Lớp retrun list
    //Tìm kiếm tiết học theo Giảng Viên > Môn > Trạng Thái return list
    //Tìm kiếm tiết học theo Môn
    //Tìm kiếm tiết học theo Môn > Lớp
    //Tìm kiếm tiết học theo Thứ,Tiết bắt đầu , Giảng Viên, 
    //Tìm kiếm tiết học theo Giảng Viên , Lớp , Thứ , Tiết Bắt Đầu 
    //Tìm kiếm tiết học theo LớpHoc
    //tìm kiếm tiết học theo Thứ
    
       
    class TietHocDAO
    {
        private static TietHocDAO instance;
       


        public static TietHocDAO Instance
        {
            get
            {
                if (instance == null) instance = new TietHocDAO();
                return TietHocDAO.instance;
            }

            private set
            {
                TietHocDAO.instance = value;
            }
        }
        
        private TietHocDAO() { }


        
        public bool themDuLieu(TietHoc th)
        {
            string query = @"insert into TietHoc(maLopHoc,ngayHoc,trangThai,ghiChu) values (  @maLopHoc , @ngayHoc , @trangThai , @ghiChu );";
            int result = 0;
            result = DataProvider.Instance.ExcuteNonQuery(query, new object[] { th.LopHoc.MaLopHoc, th.NgayHoc, th.TrangThai, th.GhiChu });
            return result > 0;
        }

        //ngày học bắt đầu 15/1/2018 , 15 tuần chính thức , 2 tuần dự trữ soTietHoc = 15
        public void TaoTietHoc(LopHoc lh, int soTietHoc, DateTime ngayNhapHoc)
        {
            int thuNgayNhapHoc = LayThuNgayNhapHoc(ngayNhapHoc);
            if (lh.Thu < thuNgayNhapHoc)
            {
                ngayNhapHoc = ngayNhapHoc.AddDays(7 - lh.Thu);
            }
            else ngayNhapHoc = ngayNhapHoc.AddDays(lh.Thu - thuNgayNhapHoc);

            for (int i = 0; i < soTietHoc; i++)
            {
                TietHoc th = new TietHoc(lh, ngayNhapHoc, -1, "");
                this.themDuLieu(th);

                ngayNhapHoc = ngayNhapHoc.AddDays(7);
            }
        }
        public List<TietHoc> LayDsTietHoc(string query)
        {
            DataTable dt = DataProvider.Instance.ExcuteQuery(query);
            List<TietHoc> dsTietHoc = new List<TietHoc>();
            foreach (DataRow item in dt.Rows)
            {
                LopHoc lh = LopHocDAO.Instance.TimLopHoc(item["maLopHoc"] + "");
                TietHoc th = new TietHoc(lh,Convert.ToDateTime(item["ngayHoc"]),int.Parse(item["trangThai"].ToString()),item["ghiChu"].ToString() );
                dsTietHoc.Add(th);
                
            }
            return dsTietHoc;
            
        }
        public List<TietHoc> LayDsTietHocParam(string query,object[] param)
        {
            DataTable dt = DataProvider.Instance.ExcuteQuery(query,param);
            List<TietHoc> dsTietHoc = new List<TietHoc>();
            foreach (DataRow item in dt.Rows)
            {
                LopHoc lh = LopHocDAO.Instance.TimLopHoc(item["maLopHoc"] + "");
                TietHoc th = new TietHoc(lh, Convert.ToDateTime(item["ngayHoc"]), int.Parse(item["trangThai"].ToString()), item["ghiChu"].ToString());
                dsTietHoc.Add(th);

            }
            return dsTietHoc;

        }

        // lấy thứ của ngày nhập học
        int LayThuNgayNhapHoc(DateTime ngayNhapHoc)
        {
            switch (ngayNhapHoc.DayOfWeek)
            {
                case DayOfWeek.Monday: return 2;
                case DayOfWeek.Tuesday: return 3;
                case DayOfWeek.Wednesday: return 4;
                case DayOfWeek.Thursday: return 5;
                case DayOfWeek.Friday: return 6;
                case DayOfWeek.Saturday: return 7;
                case DayOfWeek.Sunday: return 8;
                default: return 0;


            }
        }
        private DateTime NgayHocTheoTuan(int tuan)
        {
            DateTime ngayNhapHoc = Convert.ToDateTime((DataProvider.Instance.ExcuteQuery("EXEC dbo.NgayHocDauTien")).Rows[0][0]);
            int thu = LayThuNgayNhapHoc(ngayNhapHoc);
            //trở về thứ 2
            ngayNhapHoc = ngayNhapHoc.AddDays(thu - 2);
            //datetime của thứ 2 tuần hiện tại
            ngayNhapHoc = ngayNhapHoc.AddDays(7 * (tuan - 1));
            return ngayNhapHoc;
        }
        public List<TietHoc> DSTietHocTheoTuanHoc(int tuan)
        {
            DateTime ngayHoc = NgayHocTheoTuan(tuan); 

            return LayDsTietHocParam(@"EXEC LayTietHocTheoThoiGian @ngayBatDau , @ngayKetThuc ", new object[] { ngayHoc, ngayHoc.AddDays(7) });
            

            
        }
        
        public List<TietHoc> DSTietHocTheoTuanHoc_GiangVien(int tuan, string maGiangVien)
        {

            DateTime ngayHoc = NgayHocTheoTuan(tuan);

            return LayDsTietHocParam(@"EXEC dbo.LayTietHocTheoThoiGian_GiangVien @ngayBatDau , @ngayKetThuc , @maGiangVien ", new object[] { ngayHoc, ngayHoc.AddDays(7),maGiangVien });


        }
        public List<TietHoc> DSTietHocTheoTuanHoc_GiangVien_Lop(int tuan, string maGiangVien,string lop)
        {
            DateTime ngayHoc = NgayHocTheoTuan(tuan);

            return LayDsTietHocParam(@"EXEC dbo.LayTietHocTheoThoiGian_GiangVien_Lop @ngayBatDau , @ngayKetThuc , @maGiangVien , @maLop ", new object[] { ngayHoc, ngayHoc.AddDays(7), maGiangVien,lop });


        }
        public List<TietHoc> DSTietHocTheoTuanHoc_GiangVien_MonHoc(int tuan, string maGiangVien, string mon)
        {
            DateTime ngayHoc = NgayHocTheoTuan(tuan);

            return LayDsTietHocParam(@"EXEC dbo.LayTietHocTheoThoiGian_GiangVien_Mon @ngayBatDau , @ngayKetThuc , @maGiangVien , @maMonHoc ", new object[] { ngayHoc, ngayHoc.AddDays(7), maGiangVien, mon });


        }
        /// <summary>
        /// Dses the tiet hoc theo tuan hoc giang vien mon hoc thu.
        /// </summary>
        /// <param name="tuan">The tuan.</param>
        /// <param name="maGiangVien">The ma giang vien.</param>
        /// <param name="mon">The mon.</param>
        /// <param name="thu">Thu 2 = 2, Chu nhat = 8</param>
        /// <returns></returns>
        public List<TietHoc> DSTietHocTheoTuanHoc_GiangVien_MonHoc_Thu(int tuan, string maGiangVien, string mon,int thu)
        {
            DateTime ngayHoc = NgayHocTheoTuan(tuan);

            return LayDsTietHocParam(@"EXEC dbo.LayTietHocTheoThoiGian_GiangVien_Mon @ngayBatDau , @ngayKetThuc , @maGiangVien , @maMonHoc , @thu ", new object[] { ngayHoc, ngayHoc.AddDays(7), maGiangVien, mon , thu });


        }

        public List<TietHoc> DSTietHocTheoTuanHoc_GiangVien_MonHoc_Thu_TietBatDau(int tuan, string maGiangVien, string mon, int thu, int tietBatDau)
        {
            DateTime ngayHoc = NgayHocTheoTuan(tuan);

            return LayDsTietHocParam(@"EXEC dbo.LayTietHocTheoThoiGian_GiangVien_Mon_TietBatDau @ngayBatDau , @ngayKetThuc , @maGiangVien , @maMonHoc , @thu , @tietBatDau ", new object[] { ngayHoc, ngayHoc.AddDays(7), maGiangVien, mon, thu, tietBatDau });


        }





    }
    
}
