using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T_Manager.DTO
{
    class GiangVien
    {
        //field
        string hoGv;
        string tenGv;
        string maGv;
        //properties
        public string HoGv { get => hoGv; set => hoGv = value; }
        public string TenGv { get => tenGv; set => tenGv = value; }
        public string MaGv { get => maGv; set => maGv = value; }
        public string HoTenGV { get => HoGv + " " + TenGv; }
        //constructor
        public GiangVien(string hoGv,string tenGv,string maGv)
        {
            this.HoGv = hoGv;
            this.TenGv = tenGv;
            this.MaGv = maGv;
        }
    }
}
