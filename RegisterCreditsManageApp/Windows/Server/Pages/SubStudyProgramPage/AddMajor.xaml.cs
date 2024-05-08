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
           if(MajorNameTextBox.Text == "")
           {
                AlertBox.Show("Vui lòng nhập tên ngành học!", "Thông báo", AlertButton.OK, AlertIcon.Information);
            }
           else
           {
                if (SubjectDataGrid.ItemsSource != null)
                {
                    Major major = new Major { Name = MajorNameTextBox.Text };
                    AppDbContext._Context.Add(major);
                    AppDbContext._Context.SaveChanges();
                }
                else
                {
                    AlertBox.Show("Vui lòng thêm môn học!", "Thông báo", AlertButton.OK, AlertIcon.Information);
                }
            }
               
        }

        private void AddSubjectButton_Click(object sender, RoutedEventArgs e)
        {
            new AddSubject().ShowDialog();
        }
    }
}
