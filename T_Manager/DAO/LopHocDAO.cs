﻿using System;
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
        

        public bool themDuLieu(LopHoc lh)
        {

            string query = @"INSERT INTO dbo.LopHoc( maLopHoc,thu,tietBatDau,soTiet,tenPhong,maLop,maGiangVien,maMonHoc) values ( @maLopHoc , @thu , @tietBatDau , @soTiet , @tenPhong , @maLop , @maGiangVien , @maMonHoc );";
            int result = 0;
            result = DataProvider.Instance.ExcuteNonQuery(query, new object[] { lh.MaLopHoc,lh.Thu, lh.TietBatDau,lh.SoTiet,lh.TenPhong,lh.Lop,(lh.GiangVien!=null?lh.GiangVien.MaGv:(object)DBNull.Value),lh.MonHoc.MaMh });
            return result > 0;
        }
        public List<LopHoc> layDsLopHoc(string query)
        {
            List<LopHoc> dsLopHoc = new List<LopHoc>();
            
            DataTable dt = DataProvider.Instance.ExcuteQuery(query);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    GiangVien gv =  GiangVienDAO.Instance.TimGiangVien(item["maGiangVien"].ToString());
                    MonHoc mh = MonHocDAO.Instance.TimMonHoc(item["maMonHoc"] + "");

                    dsLopHoc.Add(new LopHoc(int.Parse(item["maLopHoc"].ToString()), int.Parse(item["thu"].ToString()), int.Parse(item["soTiet"].ToString()),item["tenPhong"].ToString(),item["maLop"].ToString(),gv,mh));

                }
            }
            return dsLopHoc;
        }

       
    }
}