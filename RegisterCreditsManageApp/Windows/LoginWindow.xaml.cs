﻿using RegisterCreditsManageApp.Windows.Server;
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
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace RegisterCreditsManageApp.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private List<User> userList;
        private string password = "";
        private string filePath = "";
        private static Student currentStudent = null;
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
        public LoginWindow()
        {
            InitializeComponent();
            RememberCheckbox.Checked += RememberCheckbox_Checked1;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            userList = AppDbContext._Context.Users.ToList();
            EyeShowedToggle.Visibility = Visibility.Collapsed;
            UserTextbox.Focus();
            LoadUserFromFile();
            LoadPassFromFile();
        }
        private void RememberCheckbox_Checked1(object sender, RoutedEventArgs e)
        {
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
       
        //Login button
        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string userInput = UserTextbox.Text;
            string passwordInput = PasswordText.Password;

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
                            // Get student login success
                            currentStudent = AppDbContext._Context.Students.Include(student => student.IdMainClassNavigation)
                                                                           .Include(student => student.IdMainClassNavigation.IdCurrentRegisterSemesterNavigation)
                                                                           .Include(student => student.IdMajorsNavigation)
                                                                           .Include(student => student.IdStudentNavigation)

                                                                           .FirstOrDefault(student => student.IdStudent == user.IdUser)!;
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
                using(StreamWriter sw = new StreamWriter(f))
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

        private void ButtonToggle_Click(object sender, RoutedEventArgs e)
        {
            if(ButtonToggle.IsChecked == true)
            {
                EyeHiddenToggle.Visibility = Visibility.Collapsed;
                EyeShowedToggle.Visibility = Visibility.Visible;

                PasswordText.Visibility = Visibility.Collapsed ;
                PasswordTextbox.Visibility = Visibility.Visible;

                PasswordTextbox.Text = PasswordText.Password;
            }
            else
            {
                EyeHiddenToggle.Visibility = Visibility.Visible;
                EyeShowedToggle.Visibility = Visibility.Collapsed;

                PasswordText.Visibility = Visibility.Visible;
                PasswordTextbox.Visibility = Visibility.Collapsed;

                PasswordText.Password = PasswordTextbox.Text;
            }
        }

        private void PasswordTextbox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            password = PasswordText.Password;

        }

        private void PasswordText_PasswordChanged_1(object sender, RoutedEventArgs e)
        {
            password = PasswordTextbox.Text;

        }

        private void UserTextbox_KeyUp(object sender, KeyEventArgs e)
       {
            if (e.Key == Key.Down)
            {
                PasswordText.Focus();
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
                PasswordText.Focus();
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
    }
}