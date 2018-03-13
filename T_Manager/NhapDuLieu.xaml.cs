using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using T_Manager.DAO;

namespace T_Manager
{
    /// <summary>
    /// Interaction logic for NhapDuLieu.xaml
    /// </summary>
    public partial class NhapDuLieu : Window
    {

        string linkExcel = "";
        DateTime ngayBatDau;
        DateTime ngayKetThuc;
        int soTietHoc = 0;
         
        public NhapDuLieu()
        {
            InitializeComponent();
        }
        bool checkButton()
        {
            return (linkExcel == "" || ngayBatDau == null || ngayKetThuc == null || soTietHoc == 0) ? false : true;
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
       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            if (openFileDialog.ShowDialog() == true) {
                linkExcel = openFileDialog.FileName;
                string[] t = openFileDialog.FileName.Split('\\');
                link.Text = t[t.Length-1];
            }
            if (checkButton())
            {
                luu.IsEnabled = true;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            MessageBoxResult exit = MessageBox.Show("Thêm dữ liệu mới sẽ xóa hét các dữ liệu lớp học củ!!", "Cảnh Báo", MessageBoxButton.YesNo);
            if (exit == MessageBoxResult.Yes) LuuDuLieu.DocExcel(linkExcel, ngayBatDau, ngayKetThuc, soTietHoc);
        }
       
        

        private void ngayBatDauTxt_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (checkButton())
            {
                luu.IsEnabled = true;
            }
            ngayBatDau = ngayBatDauTxt.SelectedDate.Value;
        }

        private void ngayKetThucTxt_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (checkButton())
            {
                luu.IsEnabled = true;
            }
            ngayKetThuc = ngayKetThucTxt.SelectedDate.Value;
        }

        private void soTietHocTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            soTietHoc = int.TryParse(soTietHocTxt.Text,out int t)? int.Parse(soTietHocTxt.Text):0;
            if (checkButton())
            {
                luu.IsEnabled = true;
            }
            else
            {
                luu.IsEnabled = false;
            }

        }
    }
}
