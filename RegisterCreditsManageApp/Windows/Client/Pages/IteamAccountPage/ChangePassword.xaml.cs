using RegisterCreditsManageApp.Models;
using RegisterCreditsManageApp.Windows.Alert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace RegisterCreditsManageApp.Windows.Client.Pages.IteamAccountPage
{
    /// <summary>
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Page
    {
        Student student = LoginWindow.CurrentStudent;
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            string OldPassword = textBoxOldPassword.Text.Trim();
            string NewPassword = textBoxNewPassword.Text.Trim();
            string ReEnterPassword = textBoxReEnterPassword.Text.Trim();
            if (OldPassword.Length == 0 || NewPassword.Length == 0 || ReEnterPassword.Length == 0)
            {
                AlertBox.Show("Vui lòng nhập đầy đủ dữ liệu", "Thông báo", AlertButton.OK, AlertIcon.Information);
                return;
            }
            User user = AppDbContext._Context.Users.FirstOrDefault(u => u.IdUser == student.IdStudentNavigation.IdUser);
            if (user != null)
            {
                string upass = user.Password.Trim();
                if( upass!= OldPassword)
                {
                    AlertBox.Show("Sai mật khẩu","Cảnh báo",AlertButton.OK, AlertIcon.Error);
                    textBoxOldPassword.Focus();
                }else
                {
                    if (NewPassword != ReEnterPassword)
                    {
                        AlertBox.Show("Mật khẩu mới không khớp", "Cảnh báo", AlertButton.OK, AlertIcon.Error);
                        textBoxReEnterPassword.Focus();
                    }
                    else
                    {
                        
                      AlertResult alertResult =  AlertBox.Show("Bạn có chắc chắn muốn đổi mật khẩu không ?", "Thông báo", AlertButton.YesNo, AlertIcon.Question);
                        
                        if (alertResult == AlertResult.Yes)
                        {
                            user.Password = NewPassword;
                            AppDbContext._Context.SaveChanges();
                            AlertBox.Show("Đổi mật khẩu thành công", "Thông báo", AlertButton.OK, AlertIcon.Success);
                            textBoxOldPassword.Clear();
                            textBoxNewPassword.Clear();
                            textBoxReEnterPassword.Clear();
                        }
                        else return;
                        
                    }
                }
            }
 
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            textBoxOldPassword.Clear();
            textBoxNewPassword.Clear();
            textBoxReEnterPassword.Clear(); 
        }
    }
}
