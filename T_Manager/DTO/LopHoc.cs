﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T_Manager.DTO
{
    class LopHoc
    {
        int maLopHoc;
        int thu;
        int tietBd;
       
        int soTiet;
      
        string lop;
        GiangVien gv;
        MonHoc mh;

        public int MaLopHoc { get => maLopHoc; set => maLopHoc = value; }
       
        public int SoTiet { get => soTiet; set => soTiet = value; }
 

        internal GiangVien GiangVien { get => gv; set => gv = value; }
        internal MonHoc MonHoc { get => mh; set => mh = value; }
        public string Lop { get => lop; set => lop = value; }
        public int Thu { get => thu; set => thu = value; }
        public int TietBd { get => tietBd; set => tietBd = value; }

        public LopHoc (int maLopHoc ,int thu, int tietBd, int soTiet, string lop, GiangVien gv, MonHoc mh){
            this.MaLopHoc = maLopHoc;
            this.TietBd = tietBd;
            this.Thu = thu;
            this.SoTiet = soTiet;
            
            this.GiangVien = gv;
            this.MonHoc = mh;
            this.Lop = lop;
        }
        public LopHoc( int soTiet, int thu, int tietBd, string lop, GiangVien gv, MonHoc mh)
        {
            this.TietBd= tietBd;
            this.Thu = thu;
            this.SoTiet = soTiet;
            this.Lop = lop;
            this.GiangVien = gv;
            this.MonHoc = mh;
        }
    }
}
