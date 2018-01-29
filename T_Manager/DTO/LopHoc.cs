using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T_Manager.DTO
{
    class LopHoc
    {
        int maLopHoc =-1;
        int thu;
        int tietBatDau;
        int soTiet;
        string tenPhong;
        string lop;
        GiangVien gv;
        MonHoc mh;

        public int MaLopHoc { get => maLopHoc; set => maLopHoc = value; }
        public int Thu { get => thu; set => thu = value; }
        public int TietBatDau { get => tietBatDau; set => tietBatDau = value; }
        public int SoTiet { get => soTiet; set => soTiet = value; }
        public string TenPhong { get => tenPhong; set => tenPhong = value; }

        internal GiangVien GiangVien { get => gv; set => gv = value; }
        internal MonHoc MonHoc { get => mh; set => mh = value; }
        public string Lop { get => lop; set => lop = value; }

        public LopHoc (int maLopHoc, int thu, int tietBatDau, int soTiet,string tenPhong, string lop, GiangVien gv, MonHoc mh){
            this.MaLopHoc = maLopHoc;
            this.Thu = thu;
            this.TietBatDau = tietBatDau;
            this.SoTiet = soTiet;
            this.TenPhong = tenPhong;
            this.GiangVien = gv;
            this.MonHoc = mh;
            this.Lop = lop;
        }
        public LopHoc( int thu, int tietBatDau, int soTiet, string tenPhong,string lop, GiangVien gv, MonHoc mh)
        {
            
            this.Thu = thu;
            this.TietBatDau = tietBatDau;
            this.SoTiet = soTiet;
            this.TenPhong = TenPhong;
            this.Lop = lop;
            this.GiangVien = gv;
            this.MonHoc = mh;
        }
    }
}
