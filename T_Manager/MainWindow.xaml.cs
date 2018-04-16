using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
using OfficeOpenXml.DataValidation;
using Microsoft.Win32;
using T_Manager.DAO;
using T_Manager.DTO;

namespace T_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            MessageBoxResult exit = MessageBox.Show("Bạn có thoát ?", "Thông báo", MessageBoxButton.YesNo);
            if (exit == MessageBoxResult.Yes) this.Close();
        }
        private void danhSach(object sender, RoutedEventArgs e)
        {
            MessageBoxResult exit = MessageBox.Show("Bạn có thoát ?", "Thông báo", MessageBoxButton.YesNo);
        }

        public class Teacher
        {
            public string ID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string ClassName { get; set; }

        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {

           // TietHocDAO.Instance.ThemTietHocBuTheoGiaiDoan(new DateTime(2018, 2, 5), new DateTime(2018, 2, 20));
            
        }
    }
}
