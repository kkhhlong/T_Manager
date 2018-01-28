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
            List<Teacher> teacher = new List<Teacher>();
            try
            {
                var package = new ExcelPackage(new FileInfo("excel003"));
                ExcelWorksheet workSheet = package.Workbook.Worksheets[1];//lấy sheet đầu

                for (int i = workSheet.Dimension.Start.Row; i < workSheet.Dimension.End.Row; i++)
                {
                    try
                    {
                        int column = 1;
                        string name = workSheet.Cells[i, column++].Value.ToString();

                        var f_manv = workSheet.Cells[i, column++].Value;

                        Teacher tCher = new Teacher();
                        {
                            tCher.ID = f_manv.ToString();
                        }
                        teacher.Add(tCher);
                    }
                    catch { }

                }
            }
            catch { }
            gridDanhsach.ItemsSource = teacher;



        }
    }
}
