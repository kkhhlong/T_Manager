using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T_Manager.DTO;

namespace T_Manager.DAO
{
    class LopHocDAO
    {
        private static LopHocDAO instance;

        public static LopHocDAO Instance
        {
            get
            {
                if (instance == null) instance = new LopHocDAO();
                return LopHocDAO.instance;
            }

            private set
            {
                LopHocDAO.instance = value;
            }
        }
        private LopHocDAO() { }
        

        public bool ThemDuLieu(LopHoc lh)
        {

            string query = @"INSERT INTO dbo.LopHoc( maLopHoc,thu,tietBatDau,soTiet,tenPhong,maLop,maGiangVien,maMonHoc) values ( @maLopHoc , @thu , @tietBatDau , @soTiet , @tenPhong , @maLop , @maGiangVien , @maMonHoc );";
            int result = 0;
            result = DataProvider.Instance.ExcuteNonQuery(query, new object[] { lh.MaLopHoc,lh.Thu, lh.TietBatDau,lh.SoTiet,lh.TenPhong,lh.Lop,(lh.GiangVien!=null?lh.GiangVien.MaGv:(object)DBNull.Value),lh.MonHoc.MaMh });
            return result > 0;
        }
        /// <summary>
        /// Tại vì lớp học rất nhiều nên chỉ lấy danh sách lớp học qua câu truy vấn  
        /// syntax select * from LopHoc where ......
        /// </summary>
        /// <param name="query">Câu truy vấn</param>
        /// <returns>List<LopHoc></returns>
        public List<LopHoc> LayDsLopHoc(string query)
        {
            List<LopHoc> dsLopHoc = new List<LopHoc>();
            
            DataTable dt = DataProvider.Instance.ExcuteQuery(query);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                    
                {
                    GiangVien gv = null;
                    if (item["maGiangVien"].ToString().Length > 0)
                    gv =  GiangVienDAO.Instance.TimGiangVien(item["maGiangVien"].ToString());

                    MonHoc mh = MonHocDAO.Instance.TimMonHoc(item["maMonHoc"] + "");
                    // public LopHoc (int maLopHoc, int thu, int tietBatDau, int soTiet,string tenPhong, string lop, GiangVien gv, MonHoc mh)
                    dsLopHoc.Add(new LopHoc(int.Parse(item["maLopHoc"].ToString()), int.Parse(item["thu"].ToString()),int.Parse(item["tietBatDau"].ToString()) ,int.Parse(item["soTiet"].ToString()),item["tenPhong"].ToString(),item["maLop"].ToString(),gv,mh));

                }
            }
            return dsLopHoc;
        }
        /// <summary>
        /// Tìm Lớp học qua mã lớp học
        /// </summary>
        /// <param name="maLopHoc">The ma lop hoc.</param>
        /// <returns>LopHoc</returns>
        public LopHoc TimLopHoc(string maLopHoc)
        {
            string query = "select * from LopHoc where maLopHoc = '" + maLopHoc + "';";
           
            DataTable dt = DataProvider.Instance.ExcuteQuery(query);
            GiangVien gv = GiangVienDAO.Instance.TimGiangVien(dt.Rows[0]["maGiangVien"].ToString());
            MonHoc mh = MonHocDAO.Instance.TimMonHoc(dt.Rows[0]["maMonHoc"].ToString());
            var item = dt.Rows[0];
            LopHoc lh = new LopHoc(int.Parse(item["maLopHoc"].ToString()), int.Parse(item["thu"].ToString()), int.Parse(item["tietBatDau"].ToString()), int.Parse(item["soTiet"].ToString()), item["tenPhong"].ToString(), item["maLop"].ToString(), gv, mh);
            return lh;



        }

       
    }
}
