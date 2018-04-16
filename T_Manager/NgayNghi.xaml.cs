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

namespace T_Manager
{
    /// <summary>
    /// Interaction logic for NgayNghi.xaml
    /// </summary>
    public partial class NgayNghi : Window
    {
        int tietBatDau;
        int tietKetThuc;
        DateTime ngayBatDau;
        DateTime ngayKetThuc;
        public NgayNghi()
        {
            InitializeComponent();
        }
        bool checkBtn()
        {
            return ngayBatDau == null || ngayKetThuc == null ? false : true;
        }
        private void luu_Click(object sender, RoutedEventArgs e)
        {
            string ghiChu = txtGhiChu.Text;
            if(int.TryParse(tietBatDauTxt.Text,out int t))
            {
                tietBatDau = int.Parse(tietBatDauTxt.Text);
            }
            if (int.TryParse(tietKetThucTxT.Text, out int c))
            {
                tietKetThuc = int.Parse(tietKetThucTxT.Text);
            }
            if (tietBatDau == 0 && tietKetThuc==0)
            {
                    TietHocDAO.Instance.ThemTietHocBuTheoGiaiDoan(ngayBatDau, ngayKetThuc,ghiChu);

            }
            else
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

        private void ngayBatDauTxt_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (checkBtn())
            {
                luu.IsEnabled = true;
            }
            ngayBatDau = ngayBatDauTxt.SelectedDate.Value;
        }

        private void ngayKetThucTxt_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (checkBtn())
            {
                luu.IsEnabled = true;
            }
            ngayKetThuc = ngayKetThucTxt.SelectedDate.Value;
        }
    }
}
