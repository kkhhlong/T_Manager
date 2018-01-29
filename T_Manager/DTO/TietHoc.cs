using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T_Manager.DTO
{
    class TietHoc
    {
        LopHoc lh;
        DateTime ngayHoc;
        int trangThai;// trạng thái: 0:bình thường , 1:nghỉ , 2:học bù , 3: đi trễ .... (mở rộng nếu cần)
        string ghiChu;

        public DateTime NgayHoc { get => ngayHoc; set => ngayHoc = value; }
        public int TrangThai { get => trangThai; set => trangThai = value; }
        public string GhiChu { get => ghiChu; set => ghiChu = value; }
        internal LopHoc LopHoc { get => lh; set => lh = value; }

        public TietHoc(LopHoc lh, DateTime ngayHoc,int trangThai,string ghiChu)
        {
            this.LopHoc = lh;
            this.NgayHoc = ngayHoc;
            this.GhiChu = ghiChu;
            this.TrangThai = trangThai;
        }
    }
}
