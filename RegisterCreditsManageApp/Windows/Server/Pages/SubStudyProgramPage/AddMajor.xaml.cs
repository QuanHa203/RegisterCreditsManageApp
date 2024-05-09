using Microsoft.EntityFrameworkCore;
using RegisterCreditsManageApp.Models;
using RegisterCreditsManageApp.Windows.Alert;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace RegisterCreditsManageApp.Windows.Server.Pages.SubWindow
{
    /// <summary>
    /// Interaction logic for AddMajor.xaml
    /// </summary>
    public partial class AddMajor : Window
    {
        public AddMajor()
        {
            InitializeComponent();
            LoadMajorDataGrid();
        }

        public void LoadMajorDataGrid()
        {
            List<Major> majorList = AppDbContext._Context.Majors.ToList();
            var list = new List<MajorData>();
            foreach (Major major in majorList)
            {
                MajorData majorData = new MajorData()
                {
                    majorName = major.Name,
                };
                list.Add(majorData);
            }
            MajorDataGrid.ItemsSource = list;
        }
        public class MajorData
        {
            public int idMajor { get; set; }
            public string majorName { get; set; }
        }
        private void CancelAddingMajorBtn_Click(object sender, RoutedEventArgs e)
        {
            AlertResult result = AlertBox.Show("Bạn có chắc muốn hủy thêm ngành học không?", "Thông báo", AlertButton.YesNo, AlertIcon.Question);
            if (result == AlertResult.Yes)
            {
                this.Close();
            }
        }

        private void AddMajorBtn_Click(object sender, RoutedEventArgs e)
        {
           if(MajorNameTextBox.Text.Trim() == "" || MajorNameTextBox.Text.Trim() == "Nhập tên ngành học")
           {
                AlertBox.Show("Vui lòng nhập tên ngành học!", "Thông báo", AlertButton.OK, AlertIcon.Information);
           }
            else
            {
                Major major = new Major {Name = MajorNameTextBox.Text.Trim() };
                AppDbContext._Context.Add(major);
                AppDbContext._Context.SaveChanges();
                AlertBox.Show("Thêm ngành học thành công", "Thông báo", AlertButton.OK, AlertIcon.Success);
                MajorNameTextBox.Clear();
                LoadMajorDataGrid();
            }
        }

        private void MajorNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(MajorNameTextBox.Text))
            {
                MajorNameTextBox.Text = "Nhập tên ngành học";
            }    
        }

        private void MajorNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if(MajorNameTextBox.Text == "Nhập tên ngành học")
            {
                MajorNameTextBox.Text = "";
            }    
        }
    }
}
