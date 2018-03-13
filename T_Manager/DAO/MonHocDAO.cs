using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T_Manager.DTO;

namespace T_Manager.DAO
{
    class MonHocDAO
    {
        private static MonHocDAO instance;

        public static MonHocDAO Instance
        {
            get
            {
                if (instance == null) instance = new MonHocDAO();
                return MonHocDAO.instance;
            }

            private set
            {
                MonHocDAO.instance = value;
            }
        }
        private MonHocDAO() { }

        public bool ThemDuLieu(MonHoc mh)
        {
            string query = @"insert into MonHoc(tenMonHoc,maMonHoc) values ( @tenMonHoc , @maMonHoc );";
            int result = 0;
            result = DataProvider.Instance.ExcuteNonQuery(query,new object[] { mh.TenMh, mh.MaMh });
            return result > 0;
        }
        public List<MonHoc> LayDanhSachTatCaMonHoc()
        {
            List<MonHoc> dsMonHoc = new List<MonHoc>();
            string query = "select*from MonHoc;";
            DataTable dt = DataProvider.Instance.ExcuteQuery(query);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    dsMonHoc.Add(new MonHoc(item["maMonHoc"].ToString(), item["tenMonHoc"].ToString()));

                }
            }
            return dsMonHoc;
        }
        public List<MonHoc> LayDanhSachMonHoc(string query , object[] param)
        {
            List<MonHoc> dsMonHoc = new List<MonHoc>();
            DataTable dt = DataProvider.Instance.ExcuteQuery(query,param);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    dsMonHoc.Add(new MonHoc(item["maMonHoc"].ToString(), item["tenMonHoc"].ToString()));

                }
            }
            return dsMonHoc;
        }

        public MonHoc TimMonHoc(string maMonHoc)
        {
            string query = "select * from MonHoc where maMonHoc = '" + maMonHoc + "';";
            DataTable dt = DataProvider.Instance.ExcuteQuery(query);
            MonHoc mh = null;
            if (dt.Rows.Count > 0)
            {
                mh = new MonHoc(dt.Rows[0]["maMonHoc"] + "", dt.Rows[0]["tenMonHoc"] + "");
            }
            return mh;

        }
        public List<MonHoc> TimMonHocTheoTen(string ten)
        {
            return LayDanhSachMonHoc(@"EXEC dbo.LayMonHocTheoTen	 @ten", new object[] { ten });
        }

    }
}
