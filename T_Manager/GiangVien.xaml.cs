using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using T_Manager.DTO;

namespace T_Manager
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Window    {
        public Page1()
        {
           
           
            InitializeComponent();
            List<GiangVien> listMh = GiangVienDAO.Instance.LayDanhSachTatCaGiangVien();
            foreach (var item in listMh)
            {
                listview.Items.Add(CreateElementGv(item));

            }
        }

       

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            listview.Items.Clear();
            List<GiangVien> listMh = GiangVienDAO.Instance.TimGiangVienTheoTen(search.Text);
            foreach (var item in listMh)
            {
                listview.Items.Add(CreateElementGv(item));

            }
        }
        private ListViewItem CreateElementGv(GiangVien gv)
        {
            ListViewItem lv = new ListViewItem();

            lv.Content = gv;
            
            
            lv.ContentTemplate = (DataTemplate)this.FindResource("myFirstItemTemplate");

            return lv;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NhapDuLieu input = new NhapDuLieu();
            input.ShowDialog();
            listview.Items.Clear();
            List<GiangVien> listGv = GiangVienDAO.Instance.LayDanhSachTatCaGiangVien();
            foreach (var item in listGv)
            {
                listview.Items.Add(CreateElementGv(item));

            }
        }

        private void listview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = listview.SelectedIndex;
            if(index != -1)
            {
                listviewMh.Items.Clear();
                GiangVien gv = (GiangVien)((ListViewItem)listview.Items.GetItemAt(index)).Content;
                List<LopHoc> listLh = LopHocDAO.Instance.LayDsLopHocTheoGiangVien(gv);
                foreach (var item in listLh)
                {
                    listviewMh.Items.Add(CreateElementMh(item));
                }
            }
            
        }
        private ListViewItem CreateElementMh(LopHoc mh)
        {
            ListViewItem lv = new ListViewItem();

            lv.Content = mh;


            lv.ContentTemplate = (DataTemplate)this.FindResource("LopHocItemTemplate");

            return lv;
        }

        private void listviewMh_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = listviewMh.SelectedIndex;
            if (index != -1)
            {
               
                LopHoc lh = (LopHoc)((ListViewItem)listviewMh.Items.GetItemAt(index)).Content;
                BuoiHoc bh = new BuoiHoc(lh);
                bh.Title =  lh.GiangVien.HoGv+" "+ lh.GiangVien.TenGv + "|" + lh.TenMH;
                bh.ShowDialog();
                
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NgayNghi windows = new NgayNghi();
            windows.ShowDialog();
        }
    }
}
