using RegisterCreditsManageApp.Windows.Server;
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
using MaterialDesignThemes.Wpf;
using RegisterCreditsManageApp.Models;
using System.Data;
using RegisterCreditsManageApp.Windows.Client;

namespace RegisterCreditsManageApp.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private List<User> userList;
        public LoginWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            userList = AppDbContext._Context.Users.ToList();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string userInput = UserTextbox.Text;
            string passwordInput = PasswordTextbox.Password;

            //Check cac textbox
            if (userInput.Trim().Length == 0)
            {
                MessageBox.Show("Please enter your user name!", "Invalid", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (passwordInput.Trim().Length == 0)
            {
                MessageBox.Show("Please enter your password!", "Invalid", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            //No data
            else if (userInput.Trim().Length == 0 && passwordInput.Trim().Length == 0)
            {
                MessageBox.Show("Please enter your user name and password!", "Invalid", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                //Khi co du lieu thi check value the textbox co trung voi gia tri dau tien tim thay trong csdl k
                User user = userList.FirstOrDefault(u =>
                {
                    if (u.Email.Trim() == userInput) return true;

                    return false;
                });
                //Neu user k co 
                if (user == null)
                {
                    MessageBox.Show("Invalid email",
                        "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                        );
                }
                //neu user co
                else
                {
                    if (user.Password.Trim() == passwordInput.Trim() && user.Email.Trim() == userInput.Trim())
                    {
                        if (user.IdRole == 1)
                        {
                            ServerWindow sv = new ServerWindow();
                            sv.Show();
                            this.Close();
                        }
                        else
                        {
                            ClientWindow cl = new ClientWindow();
                            cl.Show();
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show(
                            "Wrong user name or password",
                            "Login failed",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information
                            );

                    }
                }
            }

        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ForgotPassWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void RememberCheckbox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void EyeHiddenToggleBtn_Click_1(object sender, RoutedEventArgs e)
        {
            if(EyeHiddenToggleBtn.IsChecked == true)
            {
                PasswordTextbox.PasswordChar = '●';
            }
        }

        private void EyeShowedToggleBtn_Click(object sender, RoutedEventArgs e)
        {
            if(EyeHiddenToggleBtn.IsChecked == true)
            {
                PasswordTextbox.PasswordChar = '\0';

            }
        }
    }
}