using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T_Manager.DTO;

namespace T_Manager.DAO
{



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
            string query = @"insert into TietHoc(maLopHoc,ngayHoc,trangThai,ghiChu,tenPhong) values (  @maLopHoc , @ngayHoc , @trangThai , @ghiChu , @tenPhong );";
            int result = 0;
            result = DataProvider.Instance.ExcuteNonQuery(query, new object[] { th.LopHoc.MaLopHoc, th.NgayHoc, th.TrangThai, th.GhiChu, th.TenPhong, th.TietBatDau });
            return result > 0;
        }

        //ngày học bắt đầu 15/1/2018 , 15 tuần chính thức , 2 tuần dự trữ soTietHoc = 15
        public void TaoTietHoc(LopHoc lh, int soBuoiHoc, DateTime ngayNhapHoc, DateTime ngayKetThuc, string tenPhong, int tietBatDau)
        {
            int thuNgayNhapHoc = LayThuNgayNhapHoc(ngayNhapHoc);


            if (lh.Thu < thuNgayNhapHoc)
            {
                ngayNhapHoc = ngayNhapHoc.AddDays(7 - lh.Thu);
            }
            else ngayNhapHoc = ngayNhapHoc.AddDays(lh.Thu - thuNgayNhapHoc);

            for (int i = 0; i < soBuoiHoc; i++)
            {
                TietHoc th = new TietHoc(lh, tenPhong, ngayNhapHoc, -1, "");
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
                TietHoc th = new TietHoc(lh, int.Parse(item["idTietHoc"].ToString()), item["tenPhong"].ToString(), Convert.ToDateTime(item["ngayHoc"]), int.Parse(item["trangThai"].ToString()), item["ghiChu"].ToString());
                dsTietHoc.Add(th);

            }
            return dsTietHoc;

        }
        public List<TietHoc> LayDsTietHocParam(string query, object[] param)
        {
            DataTable dt = DataProvider.Instance.ExcuteQuery(query, param);
            List<TietHoc> dsTietHoc = new List<TietHoc>();
            foreach (DataRow item in dt.Rows)
            {
                LopHoc lh = LopHocDAO.Instance.TimLopHoc(item["maLopHoc"] + "");
                TietHoc th = new TietHoc(lh, int.Parse(item["id"].ToString()), item["tenPhong"].ToString(), Convert.ToDateTime(item["ngayHoc"]), int.Parse(item["trangThai"].ToString()), item["ghiChu"].ToString());
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
        #region TuanHoc
        private DateTime NgayHocTheoTuan(int tuan)
        {
            DateTime ngayNhapHoc = Convert.ToDateTime((DataProvider.Instance.ExcuteQuery("select * from ThongTinHoc")).Rows[0][0]);
            int thu = LayThuNgayNhapHoc(ngayNhapHoc);
            //trở về thứ 2
            ngayNhapHoc = ngayNhapHoc.AddDays(thu - 2);
            //datetime của thứ 2 tuần hiện tại
            ngayNhapHoc = ngayNhapHoc.AddDays(7 * (tuan - 1));
            return ngayNhapHoc;
        }
        public int TuanHoc(DateTime ngayHoc)
        {


            DateTime ngayNhapHoc = Convert.ToDateTime((DataProvider.Instance.ExcuteQuery("select * from ThongTinHoc")).Rows[0][0]);
            int thu = LayThuNgayNhapHoc(ngayNhapHoc);
            //trở về thứ 2
            ngayNhapHoc = ngayNhapHoc.AddDays(thu - 2);
            int k = ngayHoc.DayOfYear - ngayNhapHoc.DayOfYear;


            return k / 7 + 1;
        }
        public TietHoc layTietHocTheoId(int id)
        {
            return LayDsTietHoc("select * from TietHoc where idTietHoc = " + id)[0];
        }
        public List<TietHoc> DSTietHocTheoLopHoc(string maLopHoc)
        {
            List<TietHoc> dsBuoiHoc = LayDsTietHoc("Select * from TietHoc where maLopHoc ='" + maLopHoc + "'");
            List<TietHoc> dsHocBu = TietHocBuDAO.Instance.LayDsTietHoc("Select * from TietHocBu where maLopHoc ='" + maLopHoc + "'");
            foreach (var item in dsHocBu)
            {
                dsBuoiHoc.Add(item);
            }
            return dsBuoiHoc;
        }
      

       
       
      
        #endregion
       
        #region ThemTietHocBu
        public void ThemTietHocBuTheoTiet(int tietBatDau, int tietKetThuc, DateTime ngayNghi, string ghiChu)
        {
            DataProvider.Instance.ExcuteNonQuery("EXEC dbo.UpDateTietHocTheoTiet_NgayNghi @ngayNghi , @tietBatDau , @tietKetThuc , @ghiChu ", new object[] { tietBatDau, tietKetThuc, ngayNghi, ghiChu });
        }
        public void ThemTietHocBuTheoNgay(DateTime ngayNghi, string ghiChu)
        {
            DataProvider.Instance.ExcuteNonQuery("EXEC dbo.UpDateTietHocTheoNgayNghi @ngayNghi ", new object[] { ngayNghi });
        }
        public void ThemTietHocBuTheoGiaiDoan(DateTime ngayBatDau, DateTime ngayKetThuc, string ghiChu)
        {
            while (ngayBatDau.Month != ngayKetThuc.Month || ngayBatDau.Day != ngayKetThuc.Day || ngayBatDau.Year != ngayKetThuc.Year)
            {
                DataProvider.Instance.ExcuteNonQuery("EXEC dbo.UpDateTietHocTheoNgayNghi  " + string.Format("'{0:yyyy-MM-dd}', N'", ngayBatDau) + ghiChu + "'");
                ngayBatDau = ngayBatDau.AddDays(1);
            }
            DataProvider.Instance.ExcuteNonQuery("EXEC dbo.UpDateTietHocTheoNgayNghi  " + string.Format("'{0:yyyy-MM-dd}', N'", ngayBatDau) + ghiChu + "'");

        }


        #endregion

        public void SuaTietHoc(TietHoc th)
        {
            if (th is TietHocBu)
            {
                DataProvider.Instance.ExcuteNonQuery(String.Format("dbo.SuaBuoiHocBu @idTietHoc = {0},@maLopHoc = {1},@tietBatDau = {2},  @tenPhong = '{3}',@ngayHoc='{4:yyyy-MM-dd}', @trangThai={5}, @ghiChu = N'{6}' ", th.Id,th.LopHoc.MaLopHoc,th.TietBatDau, th.TenPhong, th.NgayHoc, th.TrangThai, th.GhiChu));

            }
            else
            {
                DataProvider.Instance.ExcuteNonQuery(String.Format("dbo.SuaBuoiHoc @id = '{0}',@tenPhong = {1},@trangThai = N'{2}',  @ghiChu = N'{3}' ", th.Id, th.TenPhong, th.TrangThai, th.GhiChu));
            }
        }
        
        








    }

}
