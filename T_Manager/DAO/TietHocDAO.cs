﻿using System;
using System.Collections.Generic;
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

        //ngày học bắt đầu 15/2/2018 , 15 tuần chính thức , 2 tuần dự trữ soTietHoc = 15
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
               ngayNhapHoc = ngayNhapHoc.AddDays(7);
                TietHoc th = new TietHoc(lh, ngayNhapHoc, -1, "");
                this.themDuLieu(th);
            }
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
    }
    
}