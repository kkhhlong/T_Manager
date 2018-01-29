using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T_Manager.DTO
{
    class MonHoc
    {
        //field
        string maMh;
        string tenMh;
        //properties
        public string TenMh { get => tenMh; set => tenMh = value; }
        public string MaMh { get => maMh; set => maMh = value; }
        //constructor
        public MonHoc(string maMh,string tenMh)
        {
            this.TenMh = tenMh;
            this.MaMh = maMh;
        }
    }
}
