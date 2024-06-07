using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RegisterCreditsManageApp.Models;
using RegisterCreditsManageApp.Windows.Alert;
using RegisterCreditsManageApp.Windows.Client;
using RegisterCreditsManageApp.Windows.Server;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RegisterCreditsManageApp.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private List<User> userList;

        private string filePath = "";
        private string checkBoxFilePath = "checkboxStatus.txt";
        private string userFilePath = "user_account.txt";
        private string passFilePath = "pass_account.txt";
        private bool isPassUpdating = false;        
        private static Student currentStudent = null;
        private static User currentAdmin = null;
        public static Student CurrentStudent
        {
            get
            {
                if (currentStudent == null)
                    throw new Exception("Student only get in ClientWindow");
                else
                    return currentStudent;
            }
        }

        public static User CurrentAdmin
        {
            get
            {
                if (currentAdmin == null)
                    throw new Exception("Admin only get in ServertWindow");
                else
                    return currentAdmin;
            }
        }

        public LoginWindow()
        {
            var t = AppDbContext._Context.Students;            
            InitializeComponent();

            RememberCheckbox.Checked += RememberCheckbox_Checked1;
            RememberCheckbox.Unchecked += RememberCheckbox_Unchecked;

            PasswordText.Visibility = Visibility.Visible;
            PasswordTextbox.Visibility = Visibility.Collapsed;
        }

     
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            userList = AppDbContext._Context.Users.ToList();
            EyeShowedToggle.Visibility = Visibility.Collapsed;
            UserTextbox.Focus();

            LoadUserFromFile();
            LoadPassFromFile();

            if (File.Exists(checkBoxFilePath))
            {
                string fileContent = File.ReadAllText(checkBoxFilePath);

                bool isChecked = bool.Parse(fileContent);

                // Đặt trạng thái checkbox
                RememberCheckbox.IsChecked = isChecked;
            }
        }
        private void RememberCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            // Lưu giá trị false vào tệp
            File.WriteAllText(checkBoxFilePath, "false");
        }

        private void RememberCheckbox_Checked1(object sender, RoutedEventArgs e)
        {

            // Lưu giá trị true vào tệp
            File.WriteAllText(checkBoxFilePath, "true");

            //User and pass file path
            string userFilePath = "user_account.txt";
            string passFilePath = "pass_account.txt";

            //Check
            if(RememberCheckbox.IsChecked == true)
            {
                //Save data to file
                File.WriteAllText(userFilePath, UserTextbox.Text);
                File.WriteAllText(passFilePath, PasswordText.Password);
            }
            else
            {
                //Delete data in file
                File.WriteAllText(userFilePath, string.Empty);
                File.WriteAllText(passFilePath, string.Empty);
            }

        }
        //Load user data to textbox
        private void LoadUserFromFile()
        {
            string userFilePath = "user_account.txt";

            if (File.Exists(userFilePath))
            {
                using(StreamReader reader = new StreamReader(userFilePath))
                {
                    string fileContent = reader.ReadToEnd();
                    UserTextbox.Text = fileContent;
                }
            }
        }
        //Load pass data to textbox
        private void LoadPassFromFile()
        {
            string passFilePath = "pass_account.txt";

            if (File.Exists(passFilePath))
            {
                using (StreamReader reader = new StreamReader(passFilePath))
                {
                    string fileContent = reader.ReadToEnd();
                    PasswordText.Password = fileContent;
                }
            }
        }

        private void RememberCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            SaveUserToFile();
            SavePassToFile();
        }
        //save user to file
        private void SaveUserToFile()
        {
            string user = UserTextbox.Text;

            filePath = "user_account.txt";
            //Create new file
            using (FileStream f = new FileStream(filePath, FileMode.Create))
            {
                //Create a streamWriter to write data to file
                using (StreamWriter sw = new StreamWriter(f))
                {
                    sw.Write($"{user}");
                }
            }
        }
        //Save pass to file
        private void SavePassToFile()
        {
            string pass = PasswordText.Password;

            filePath = "pass_account.txt";
            //Create new file
            using (FileStream f = new FileStream(filePath, FileMode.Create))
            {
                //Create a streamWriter to write data to file
                using (StreamWriter sw = new StreamWriter(f))
                {
                    sw.Write($"{pass}");
                }
            }
        }

        //Login button
        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string userInput = UserTextbox.Text;
            string passwordInput = PasswordText.Password;
            //Check cac textbox
            if (userInput.Trim().Length == 0)
            {
                AlertBox.Show("Vui lòng nhập email!", "Thông báo", AlertButton.OK, AlertIcon.Error);
            }
            else if (passwordInput.Trim().Length == 0)
            {
                AlertBox.Show("Vui lòng nhập mật khẩu của bạn!", "Thông báo", AlertButton.OK, AlertIcon.Error);
            }
            //No data
            else if (userInput.Trim().Length == 0 && passwordInput.Trim().Length == 0)
            {
                AlertBox.Show("Vui lòng nhập email và mật khẩu của bạn!", "Thông báo", AlertButton.OK, AlertIcon.Error);
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
                    AlertBox.Show("Invalid email",
                        "Error",
                        AlertButton.OK,
                        AlertIcon.Error
                        );
                }
                //neu user co
                else
                {
                    if (user.Password.Trim() == passwordInput.Trim() && user.Email.Trim() == userInput.Trim())
                    {
                        if (user.IdRole == 1)
                        {
                            checkIfUserValid();
                            AlertBox.Show("Đăng nhập thành công", "Thông báo", AlertButton.OK, AlertIcon.Success);
                            currentAdmin = user;
                            ServerWindow sv = new ServerWindow();
                            sv.Show();
                            this.Close();
                        }
                        else
                        {
                            checkIfUserValid();
                            AlertBox.Show("Đăng nhập thành công", "Thông báo", AlertButton.OK, AlertIcon.Success);
                            // Get student login success
                            currentStudent = AppDbContext._Context.Students
                                            .Include(student => student.IdMainClassNavigation)
                                            .Include(student => student.IdMainClassNavigation.IdCurrentRegisterSemesterNavigation)
                                            .Include(student => student.IdMajorsNavigation)
                                            .Include(student => student.IdStudentNavigation)
                                            .FirstOrDefault(student => student.IdStudent.Trim() == user.IdUser.Trim())!;
                            ClientWindow cl = new ClientWindow();
                            cl.Show();
                            this.Close();
                        }
                    }
                    else
                    {
                        AlertBox.Show(
                            "Sai email hoặc mật khẩu",
                            "Đăng nhập thất bại",
                            AlertButton.OK,
                            AlertIcon.Information
                            );

                    }
                }
            }
            
        }
        public void checkIfUserValid()
        {
            // Kiểm tra nếu checkBox đc tích
            if (RememberCheckbox.IsChecked == true)
            {
                SavePassToFile();
                SaveUserToFile();
                File.WriteAllText(checkBoxFilePath, "true");
            }
            else
            {
                // Xóa dữ liệu file nếu checkbox không được chọn
                if (File.Exists(checkBoxFilePath) && File.Exists(userFilePath) && File.Exists(passFilePath))
                {
                    File.Delete(checkBoxFilePath);
                    File.Delete(userFilePath);
                    File.Delete(passFilePath);
                }
            }
        }
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            AlertResult result = AlertBox.Show("Bạn có chắc muốn thoát không?", "Thông báo", AlertButton.YesNo, AlertIcon.Question);
            if(result == AlertResult.Yes)
            {
                Application.Current.Shutdown();
            }    
        }
        
        private void ButtonToggle_Click(object sender, RoutedEventArgs e)
        {
            if(PasswordTextbox.Visibility == Visibility.Collapsed)
            {
                PasswordTextbox.Text = PasswordText.Password;

                EyeHiddenToggle.Visibility = Visibility.Collapsed;
                EyeShowedToggle.Visibility = Visibility.Visible;

                PasswordText.Visibility = Visibility.Collapsed ;
                PasswordTextbox.Visibility = Visibility.Visible;
            }
            else
            {
                PasswordText.Password = PasswordTextbox.Text;

                EyeHiddenToggle.Visibility = Visibility.Visible;
                EyeShowedToggle.Visibility = Visibility.Collapsed;

                PasswordText.Visibility = Visibility.Visible;
                PasswordTextbox.Visibility = Visibility.Collapsed;
            }
        }

        private void PasswordTextbox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if (!isPassUpdating)
            {
                isPassUpdating = true;
                PasswordText.Password = PasswordTextbox.Text;
                isPassUpdating = false;
            }
        }

        private void PasswordText_PasswordChanged_1(object sender, RoutedEventArgs e)
        {
            if (!isPassUpdating)
            {
                isPassUpdating = true;
                PasswordTextbox.Text = PasswordText.Password;
                isPassUpdating = false;
            }
        }

        private void UserTextbox_KeyUp(object sender, KeyEventArgs e)
       {
            if (e.Key == Key.Down)
            {
                if(PasswordText.Visibility == Visibility.Visible)
                {
                    PasswordText.Focus();

                }
                else
                PasswordTextbox.Focus();
            }
            if(e.Key == Key.Up)
            {
                UserTextbox.Focus();
            }
        }

        private void PasswordText_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                UserTextbox.Focus();
            }
            if (e.Key == Key.Down)
            {
                LoginBtn.Focus();
            }
        }

        private void LoginBtn_KeyUp(object sender, KeyEventArgs e)
        {
            if( e.Key == Key.Up)
            {
                if (PasswordText.Visibility == Visibility.Visible)
                {
                    PasswordText.Focus();

                }
                else
                    PasswordTextbox.Focus();
            }
            if (e.Key == Key.Down)
            {
                ExitBtn.Focus();
            }
        }

        private void ExitBtn_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Up)
            {
                LoginBtn.Focus();
            }
           
        }

        private void PasswordTextbox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                UserTextbox.Focus();
            }
            if (e.Key == Key.Down)
            {
                LoginBtn.Focus();
            }
        }

        private void ForgotPassWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            new FogotPasswordWindow().Show();
        }
    }
}