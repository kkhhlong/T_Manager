using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using T_Manager.DAO;
using T_Manager.DTO;

namespace T_Manager
{
    /// <summary>
    /// Interaction logic for BuoiHoc.xaml
    /// </summary>
    public partial class BuoiHoc : Window
    {
        TietHoc tietHocSelected;
        LopHoc lopHoc;
        string phong="";
        int tietBatDau;
        int trangThai=-2;
        DateTime ngayHoc;
        private List<TietHoc> sortBuoiHoc(List<TietHoc> th)
        {
            for (int i = 0; i < th.Count -1; i++)
            {
                for (int    j  = i+1; j < th.Count; j++)
                {
                    if (TietHoc.Compare(th[i], th[j]) > 0)
                    {
                        TietHoc temp = th[i];
                        th[i] = th[j];
                        th[j] = temp;
                    }
                }
                
            }
            return th;
        }
        
        public BuoiHoc(Object lopHoc)
        {

            this.lopHoc = (LopHoc)lopHoc;
            InitializeComponent();

            gridTietHoc.ItemsSource = sortBuoiHoc(TietHocDAO.Instance.DSTietHocTheoLopHoc(this.lopHoc.MaLopHoc + ""));

            tenGiangVien.Text = this.lopHoc.GiangVien.HoGv + " " + this.lopHoc.GiangVien.TenGv;
            tenMonHoc.Text = this.lopHoc.TenMH + " - " + this.lopHoc.NoiDung;
            noiDungMonHoc.Text = this.lopHoc.NoiDungLop;

        }
        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            try
            {
                if((TietHoc)e.Row.DataContext is TietHocBu)
                {
                    e.Row.Background = new SolidColorBrush(Color.FromRgb(222, 126, 217));
                  
                }
                else if (((TietHoc)e.Row.DataContext).TrangThai == 1 || ((TietHoc)e.Row.DataContext).TrangThai == 0)
                {
                    e.Row.Background = new SolidColorBrush(Colors.DarkGray);
                    e.Row.Opacity = 0.5;
                }
                
                else if (((TietHoc)e.Row.DataContext).TrangThai == 2)
                {
                    e.Row.Background = new SolidColorBrush(Color.FromRgb(212, 249, 123));
                }
                
            }
            catch
            {
            }
        }
        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }
        private void PreviewTextInputHandler(Object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
        private void TextBoxPasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

       

        private void gridTietHoc_SelectionChanged(object sender, SelectionChangedEventArgs e)//binding
        {
            try
            {
                tietHocSelected = ((TietHoc)gridTietHoc.Items[gridTietHoc.SelectedIndex]);
                ngayHocTxt.SelectedDate = tietHocSelected.NgayHoc;
                cbxTrangThai.SelectedIndex = tietHocSelected.TrangThai + 1;
                txtTenPhong.Text = tietHocSelected.TenPhong;
                txtTietBatDau.Text = tietHocSelected.TietBatDau + "";
                txtGhiChu.Text = tietHocSelected.GhiChu;
                if(!(tietHocSelected is TietHocBu))
                {
                    txtTietBatDau.IsEnabled= false;
                    ngayHocTxt.IsEnabled = false;
                }
                else
                {
                    txtTietBatDau.IsEnabled = true;
                    ngayHocTxt.IsEnabled = true;
                }
               
            
                
            }
            catch
            {
              
            }

        }

        private void txtTenPhong_TextChanged(object sender, TextChangedEventArgs e)
        {
            phong = txtTenPhong.Text;
            
        }

        private void txtTietBatDau_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(txtTietBatDau.Text, out int t))
            {
                tietBatDau = int.Parse(txtTietBatDau.Text);
            }
            else
            {
                tietBatDau = -2;
            }
               
            
        }

        private void cbxTrangThai_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            trangThai = cbxTrangThai.SelectedIndex-1;
            
        }

        private void ngayHocTxt_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ngayHoc = ngayHocTxt.SelectedDate.Value;
            
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            TietHoc th;
            if (tietHocSelected is TietHocBu)
            {
                th = new TietHocBu(this.lopHoc, tietHocSelected.Id, txtTenPhong.Text, ngayHocTxt.SelectedDate.Value, cbxTrangThai.SelectedIndex - 1, txtGhiChu.Text, int.Parse(txtTietBatDau.Text));
            }
            else
            {
                th = new TietHoc(tietHocSelected.LopHoc, tietHocSelected.Id, txtTenPhong.Text.ToUpper(), tietHocSelected.NgayHoc, cbxTrangThai.SelectedIndex - 1, txtGhiChu.Text);
            }
                TietHocDAO.Instance.SuaTietHoc(th);

            gridTietHoc.ItemsSource = sortBuoiHoc(TietHocDAO.Instance.DSTietHocTheoLopHoc(this.lopHoc.MaLopHoc + ""));


        }

        
    }
}
