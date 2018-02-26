using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T_Manager.DTO
{
    class TietHoc
    {
        int tietBatDau;
        string tenPhong;
        LopHoc lh;
        DateTime ngayHoc;
        int trangThai;// trạng thái:-1:bình thường, 0: trường cho nghỉ , 1: tự nghỉ , 2:học bù , 3: đi trễ .... (mở rộng nếu cần)
        string ghiChu;

        public DateTime NgayHoc { get => ngayHoc; set => ngayHoc = value; }
        public int TrangThai { get => trangThai; set => trangThai = value; }
        public string GhiChu { get => ghiChu; set => ghiChu = value; }
        internal LopHoc LopHoc { get => lh; set => lh = value; }
        public int TietBatDau { get => tietBatDau; set => tietBatDau = value; }
        public string TenPhong { get => tenPhong; set => tenPhong = value; }

        public TietHoc(LopHoc lh, string tenPhong, int tietBatDau, DateTime ngayHoc,int trangThai,string ghiChu)
        {
            this.TenPhong = tenPhong;
            this.TietBatDau = tietBatDau;
            this.LopHoc = lh;
            this.NgayHoc = ngayHoc;
            this.GhiChu = ghiChu;
            this.TrangThai = trangThai;
        }
    }
}
