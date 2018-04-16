using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using T_Manager.DTO;
using Excel = Microsoft.Office.Interop.Excel;

namespace T_Manager.DAO
{
    abstract class LuuDuLieu
    {
        /// <summary>
        /// đọc file excel trả về 1 mảng object 2 chiều
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        static object[,] LayDuLieu(string path)
        {
            var xlApp = new Excel.Application();
            xlApp.Visible = false;
            var xlWorkBook = xlApp.Workbooks.Open(path);
            var xlWorkSheet = (Excel.Worksheet)xlWorkBook.Sheets.get_Item(1);
            Excel.Range xlRange = xlWorkSheet.UsedRange;
            object[,] dt = (object[,])xlRange.get_Value(Excel.XlRangeValueDataType.xlRangeValueDefault);
            xlApp.Quit();
            return dt;
        }  
        //Lưu  dữ liệu từ excel vào QLYGIANGVIEN.db
        static void LuuCSDL(object[,] dt, DateTime ngayNhapHoc, DateTime ngayKetThuc, int soBuoiHoc)
        { 
            DataProvider.Instance.ExcuteNonQuery(@" insert into ThongTinHoc values ( '"+String.Format("{0:yyyy-MM-dd}",ngayNhapHoc)+"','"+ String.Format("{0:yyyy-MM-dd}", ngayKetThuc)+"',"+ soBuoiHoc +")");
            for (int i = 2; i < dt.GetLength(0); i++)
            {
                //các thự tự column trong excel
                /*column:
                1 : thứ
                2 : tiết bắt đầu
                3 : số tiết
                4 : phòng
                5 : mã môn học
                6 : mã nhân viên
                7 : tên môn học
                8 : họ lót nhân viên
                9 : tên nhân viên
                10: mã lớp(của sinh viên)
                13: mã lớp học
                */
                int thu = int.Parse(dt[i, 1].ToString());
                int tietBatDau = int.Parse(dt[i, 2].ToString());
                int soTiet = int.Parse(dt[i, 3].ToString());
                string phong = dt[i, 4].ToString();
                string maMonHoc = dt[i, 5].ToString();
                string maGv = dt[i, 6].ToString();
                string tenMh = dt[i, 7].ToString();
                string hoLotGv;
                string tenGv;
                string maLop = dt[i, 10].ToString();
                int maLopHoc = int.Parse(dt[i, 13].ToString());
                //new GiangVien(họ lót, tên , mã)
               GiangVien gv = null;
               if (maGv != "")
                {
                    hoLotGv = dt[i, 8].ToString();
                    tenGv = dt[i, 9].ToString();
                    gv = new GiangVien(hoLotGv, tenGv, maGv);
                    GiangVienDAO.Instance.ThemDuLieu(gv);
                }
               // new MonHoc(Mã môn học, tên môn học)
                MonHoc mh = new MonHoc(maMonHoc, tenMh);
                MonHocDAO.Instance.ThemDuLieu(mh);
                
                //public LopHoc (int maLopHoc, int thu, int tietBd, int soTiet, string lop, GiangVien gv, MonHoc mh)
                LopHoc lh = new LopHoc(maLopHoc,thu,tietBatDau,soTiet,maLop,phong,gv,mh);
               
            
                LopHocDAO.Instance.ThemDuLieu(lh);

                TietHocDAO.Instance.TaoTietHoc(lh,soBuoiHoc,ngayNhapHoc,ngayKetThuc,phong,tietBatDau);

            }
            

        }
        

        static public string DocExcel(string path,DateTime ngayNhapHoc , DateTime ngayKetThuc , int soTiet)
        {
            DataProvider.Instance.ExcuteNonQuery("dbo.xoaDuLieu");
            object [,] dt = LayDuLieu(path);
            LuuCSDL(dt,ngayNhapHoc,ngayKetThuc,soTiet);
            return "";
        }

    }
}
