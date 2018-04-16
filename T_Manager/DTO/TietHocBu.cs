using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T_Manager.DAO;

namespace T_Manager.DTO
{
    class TietHocBu : TietHoc
    {
        int tietBatDau;
        public override int TietBatDau { get => tietBatDau; }
        public override string TuanHocBu { get => TietHocDAO.Instance.layTietHocTheoId(Id).Tuan + ""; }
        public TietHocBu(LopHoc lh, int id, string tenPhong, DateTime ngayHoc, int trangThai, string ghiChu, int tietBatDau) : base(lh, id, tenPhong, ngayHoc, trangThai, ghiChu)
        {
            
            this.tietBatDau = tietBatDau;
            
        }
        public TietHocBu(LopHoc lh, int id, string tenPhong, int trangThai, string ghiChu) : base(lh, id, tenPhong, trangThai, ghiChu)
        {

          

        }



    }
}
