using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T_Manager.DTO;

namespace T_Manager.DAO
{
    class TietHocBuDAO
    {
        private static TietHocBuDAO instance;



        public static TietHocBuDAO Instance
        {
            get
            {
                if (instance == null) instance = new TietHocBuDAO();
                return TietHocBuDAO.instance;
            }

            private set
            {
                TietHocBuDAO.instance = value;
            }
        }

        private TietHocBuDAO() { }
        public List<TietHoc> LayDsTietHoc(string query)
        {
            DataTable dt = DataProvider.Instance.ExcuteQuery(query);
            List<TietHoc> dsTietHoc = new List<TietHoc>();
            foreach (DataRow item in dt.Rows)
            {
                if (item["ngayHoc"] != DBNull.Value)
                {
                    LopHoc lh = LopHocDAO.Instance.TimLopHoc(item["maLopHoc"] + "");
                    TietHoc th = new TietHocBu(lh, int.Parse(item["idTietHoc"].ToString()), item["tenPhong"].ToString(), Convert.ToDateTime(item["ngayHoc"]), int.Parse(item["trangThai"].ToString()), item["ghiChu"].ToString(), int.Parse(item["tietBatDau"].ToString()));
                    dsTietHoc.Add(th);
                }
                else
                {
                    LopHoc lh = LopHocDAO.Instance.TimLopHoc(item["maLopHoc"] + "");
                    TietHoc th = new TietHocBu(lh, int.Parse(item["idTietHoc"].ToString()), item["tenPhong"].ToString(),int.Parse(item["trangThai"].ToString()), item["ghiChu"].ToString());
                    dsTietHoc.Add(th);
                }

            }
            return dsTietHoc;

        }
    }
}
