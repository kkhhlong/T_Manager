using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T_Manager.DTO;

namespace T_Manager.DAO
{
    class GiangVienDAO
    {
        private static GiangVienDAO instance;

        public static GiangVienDAO Instance
        {
            get
            {
                if (instance == null) instance = new GiangVienDAO();
                return GiangVienDAO.instance;
            }

            private set
            {
                GiangVienDAO.instance = value;
            }
        }
        private GiangVienDAO() { }

        public bool themDuLieu(GiangVien gv)
        {
            string query = @"insert into GiangVien(hoLotGiangVien,tenGiangVien,maGiangVien) values ( @hoLotGiangVien , @tenGiangVien , @maGiangVien );";
            int result = 0;
            result = DataProvider.Instance.ExcuteNonQuery(query, new object[] { gv.HoGv ,gv.TenGv ,gv.MaGv });
            return result > 0;
        }
        public List<GiangVien> LayDanhSachGiangVien()
        {
            List<GiangVien> dsGiangVien = new List<GiangVien>();
            string query = "select*from GiangVien;";
            DataTable dt = DataProvider.Instance.ExcuteQuery(query);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    dsGiangVien.Add(new GiangVien(item["hoLotGiangVien"].ToString(), item["tenGiangVien"].ToString(), item["maGiangVien"].ToString()));

                }
            }
            return dsGiangVien;
        }
        public GiangVien TimGiangVien(string maGiangVien)
        {
            string query = "select * from GiangVien where maGiangVien = " + maGiangVien + ";";
            DataTable dt =DataProvider.Instance.ExcuteQuery(query);
            GiangVien gv = null;
            if (dt.Rows.Count > 0)
            {
                gv = new GiangVien(dt.Rows[0]["hoLotGiangVien"]+"", dt.Rows[0]["tenGiangVien"]+"", dt.Rows[0]["maGiangVien"].ToString());
            }
            return gv;

        }

    }
}
