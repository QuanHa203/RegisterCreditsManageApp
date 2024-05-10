using Microsoft.EntityFrameworkCore;
using RegisterCreditsManageApp.Models;
using RegisterCreditsManageApp.Windows.Alert;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ModifyMajor.xaml
    /// </summary>
    public partial class ModifyMajor : Window
    {
        private int idMajor;
        public ModifyMajor(int idMajor)
        {
            InitializeComponent();
            this.idMajor = idMajor;
            // Load current major name into textBox
            var major = AppDbContext._Context.Majors.FirstOrDefault((Major major) => major.IdMajors == idMajor);
            MajorNameTextBox.Text = $"{major.Name}";
            AppDbContext._Context.Majors.Entry(major).State = EntityState.Detached;
        }
        private void CancelModifyingMajorBtn_Click(object sender, RoutedEventArgs e)
        {
            AlertResult result = AlertBox.Show("Bạn có chắc muốn hủy chỉnh sửa ngành học không?", "Thông báo", AlertButton.YesNo, AlertIcon.Question);
            if (result == AlertResult.Yes)
            {
                this.Close();
            }
        }

        private void ModifyMajorBtn_Click(object sender, RoutedEventArgs e)
        {
            if(MajorNameTextBox.Text == "")
            {
                AlertBox.Show("Vui lòng nhập đầy đủ dữ liệu tên ngành học", "Thông báo", AlertButton.OK, AlertIcon.Warning);
                return;
            }

            Major major = new Major {IdMajors = idMajor, Name = MajorNameTextBox.Text.Trim()};
            AppDbContext._Context.Majors.Update(major);
            AppDbContext._Context.SaveChanges();
            AppDbContext._Context.Majors.Entry(major).State = EntityState.Detached;

            AlertBox.Show("Đã sửa tên ngành học thành công", "Thông báo", AlertButton.OK, AlertIcon.Information);
            this.Close();
        }
    }
}
