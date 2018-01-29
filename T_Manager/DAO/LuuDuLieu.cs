using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            return dt;
        }  
        //Lưu  dữ liệu từ excel vào QLYGIANGVIEN.db
        static void LuuCSDL(object[,] dt)
        {
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

                //new GiangVien(họ lót, tên , mã)
               GiangVien gv = null;
               if (dt[i, 6].ToString() != "")
                {
                    gv = new GiangVien(dt[i, 8].ToString(), dt[i, 9].ToString(), dt[i, 6].ToString());
                    GiangVienDAO.Instance.themDuLieu(gv);
                }
               // new MonHoc(Mã môn học, tên môn học)
                MonHoc mh = new MonHoc(dt[i, 5].ToString(), dt[i, 7].ToString());
                MonHocDAO.Instance.themDuLieu(mh);
                
                //new LopHoc(string mãlophoc,int thứ , int tietBatDau, int soTiet, string tenPhong, string lop, GiangVien gv, MonHoc mh)
                LopHoc lh = new LopHoc(int.Parse(dt[i, 13].ToString()), int.Parse(dt[i, 1].ToString()), int.Parse(dt[i, 2].ToString()), int.Parse(dt[i, 3].ToString()), dt[i, 4].ToString(),dt[i,10].ToString(), gv, mh);
               
            
                LopHocDAO.Instance.themDuLieu(lh);

                TietHocDAO.Instance.TaoTietHoc(lh, 15, new DateTime(2018, 1, 15));

            }
            

        }
        

        static public string DocExcel(string path)
        {
            object [,] dt =LayDuLieu(path);
            LuuCSDL(dt);
            return "";
        }

    }
}
