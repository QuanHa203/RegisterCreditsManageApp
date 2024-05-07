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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;
using RegisterCreditsManageApp.Models;
using RegisterCreditsManageApp.Windows.Server.Pages.SubWindow;

namespace RegisterCreditsManageApp.Windows.Server.Pages
{
    /// <summary>
    /// Interaction logic for ManagementPage.xaml
    /// </summary>
    public partial class StudyProgramPage : Page
    {
        public StudyProgramPage()
        {
            InitializeComponent();
            
        }
        public class MajorData
        {
            private int idMajor {  get; set; }
            private string majorName {  get; set; }
        }
    private void FindMajorTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (FindMajorTextBox.Text != null && FindMajorTextBox.Text == "Tìm kiếm tên ngành học")
            {
                // Xóa placeholder text
                FindMajorTextBox.Text = string.Empty;
            }
        }

        private void FindMajorTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            // Nếu TextBox rỗng, khôi phục placeholder text
            if (FindMajorTextBox.Text != null && string.IsNullOrWhiteSpace(FindMajorTextBox.Text))
            {
                FindMajorTextBox.Text = "Tìm kiếm tên ngành học";
            }
        }
        private void NavigateToAddMajor_Click(object sender, RoutedEventArgs e)
        {
            new AddMajor().ShowDialog();
        }
    }
}
