using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T_Manager.DAO;

namespace T_Manager.DTO
{
    class TietHoc
    {
        int id;
        
        string tenPhong;
        LopHoc lh;
        DateTime ngayHoc = DateTime.MinValue;
        int trangThai;// trạng thái:-1 :bình thường, 0: trường cho nghỉ , 1: tự nghỉ , 2: đi trễ ;
        string ghiChu;
        
        public DateTime NgayHoc { get => ngayHoc; set => ngayHoc = value; }
        public int TrangThai { get => trangThai; set => trangThai = value; }
        public string GhiChu { get => ghiChu; set => ghiChu = value; }
        internal LopHoc LopHoc { get => lh; set => lh = value; }
        public virtual int TietBatDau { get => lh.TietBd; }
        public string TenPhong { get => tenPhong; set => tenPhong = value; }
        public string Thu { get => getThu(); }
        public virtual  string TuanHocBu { get => ""; }
        public string ShortNgayHoc {
            get
            {
                return NgayHoc==DateTime.MinValue?"": string.Format("{0:dd/MM/yyyy}", NgayHoc);
            }
        }
        public string TextTrangThai { get => getTrangThai(); }
        public string SoTiet { get => getSoTiet(); }
        public int Id { get => id; set => id = value; }
        public int Tuan { get => TietHocDAO.Instance.TuanHoc(this.ngayHoc); }

        public TietHoc(LopHoc lh,int id, string tenPhong, DateTime ngayHoc,int trangThai,string ghiChu)
        {
            this.TenPhong = tenPhong;
            this.Id = id;
            this.LopHoc = lh;
            this.NgayHoc = ngayHoc;
            this.GhiChu = ghiChu;
            this.TrangThai = trangThai;
        }
        public TietHoc(LopHoc lh, int id, string tenPhong, int trangThai, string ghiChu)
        {
            this.TenPhong = tenPhong;
            this.Id = id;
            this.LopHoc = lh;
            
            this.GhiChu = ghiChu;
            this.TrangThai = trangThai;
        }
        public TietHoc(LopHoc lh, string tenPhong, DateTime ngayHoc, int trangThai, string ghiChu)
        {
            this.TenPhong = tenPhong;
            this.Id = id;
            this.LopHoc = lh;
            this.NgayHoc = ngayHoc;
            this.GhiChu = ghiChu;
            this.TrangThai = trangThai;
        }
        string getSoTiet()
        {
            string soTiet = "";
            if (TietBatDau != 0)
            {
                soTiet = TietBatDau + ", ";
                for (int i = 1; i < LopHoc.SoTiet - 1; i++)
                {
                    soTiet += (TietBatDau + i) + ", ";
                }
                soTiet += ((TietBatDau + LopHoc.SoTiet) - 1) + "";
            }
           
            return soTiet;
        }
        string getTrangThai()
        {
            switch (trangThai)
            {
                case -1:
                    return "Bình thường";
                case 0:
                    return "Trường cho nghỉ";
                case 1:
                    return "Giảng viên nghỉ";
                default:
                    return "Đi trễ";

            }
        }
        string getThu()
        {
            switch (ngayHoc.DayOfWeek)
            {
                case DayOfWeek.Sunday:

                    return "Chủ Nhật";
                  


                case DayOfWeek.Monday:

                    return "Thứ Hai";
                  


                case DayOfWeek.Tuesday:
                    return "Thứ Ba";
                   

                case DayOfWeek.Wednesday:
                    return "Thứ Tư";
                  
                case DayOfWeek.Thursday:
                    return "Thứ Năm";
                   

                case DayOfWeek.Friday:
                    return "Thứ Sáu";
                   
                default:
                    return "Thứ Bảy";
               
            }
            
        }

        public static  int Compare(TietHoc x, TietHoc y)
        {
            return x.Tuan-y.Tuan;
        }
    }
}
