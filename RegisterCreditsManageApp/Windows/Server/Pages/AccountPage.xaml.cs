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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RegisterCreditsManageApp.Windows.Server.Pages
{
    /// <summary>
    /// Interaction logic for AccountPage.xaml
    /// </summary>
    public partial class AccountPage : Page
    {
        private User userAdmin;
        public AccountPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            userAdmin = LoginWindow.CurrentAdmin;
            textBoxEmail.Text = userAdmin.Email;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            string oldPassword = textBoxOldPassword.Text.Trim();
            string newPassword = textBoxNewPassword.Text.Trim();
            string newPasswordAgain = textBoxNewPasswordAgain.Text.Trim();

            // Check TextBox is empty
            if(oldPassword.Length == 0 || newPassword.Length == 0 || newPasswordAgain.Length == 0)
            {
                AlertBox.Show("Vui lòng nhập đầy đủ dữ liệu", "Thông báo", AlertButton.OK, AlertIcon.Information);
                return;
            }

            // Check is apply change password
            var alertResult = AlertBox.Show("Bạn có chắc chắn muốn đổi mật khẩu không?", "", AlertButton.YesNo, AlertIcon.Question);
            if (alertResult == AlertResult.No)
                return;
                
            // Check oldPassword is wrong
            if (oldPassword != userAdmin.Password.Trim())
            {
                AlertBox.Show("Đã nhập sai mật khẩu cũ", "Lỗi", AlertButton.OK, AlertIcon.Error);
                return;
            }

            // Check newPassword is not equal newPasswordAgain
            if (newPassword != newPasswordAgain)
            {
                AlertBox.Show("Mật khẩu mới không trùng với mật khẩu mới nhập lại", "Lỗi", AlertButton.OK, AlertIcon.Error);
                return;
            }

            // Change password
            User user = new User
            {
                IdUser = userAdmin.IdUser,
                IdRole = 1,     // IdRole = 1 is Admin
                Email = userAdmin.Email,
                Password = newPassword,                
            };
            AppDbContext._Context.Entry(userAdmin).State = Microsoft.EntityFrameworkCore.EntityState.Detached;

            AppDbContext._Context.Update(user);
            AppDbContext._Context.SaveChanges();
            AppDbContext._Context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            LoginWindow.CurrentAdmin = user;
            AlertBox.Show("Đã thay đổi mật khẩu thành công", "", AlertButton.OK, AlertIcon.Success);
            textBoxOldPassword.Text = "";
            textBoxNewPassword.Text = "";
            textBoxNewPasswordAgain.Text = "";
        }
    }
}
