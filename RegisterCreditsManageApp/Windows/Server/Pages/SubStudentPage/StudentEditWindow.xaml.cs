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

namespace RegisterCreditsManageApp.Windows.Server.Pages.SubStudentPage
{
    /// <summary>
    /// Interaction logic for StudentEditWindow.xaml
    /// </summary>
    public partial class StudentEditWindow : Window
    {
        private Student student;
        private Style btnInPopupStyle;
        int? idMajor;
        int? idMainClass;
        public StudentEditWindow(string idStudent)
        {
            InitializeComponent();
            student = AppDbContext._Context.Students.Include(student => student.IdStudentNavigation)
                                                    .Include(student => student.IdMajorsNavigation)
                                                    .Include(student => student.IdMainClassNavigation)
                                                    .FirstOrDefault(s => s.IdStudent == idStudent);

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataToWindow();
            btnInPopupStyle = this.Resources["BtnInPopup"] as Style;
            var majorList = AppDbContext._Context.Majors.ToList();
            foreach (var major in majorList)
            {
                Button btn = new Button();
                btn.Content = major.Name;
                btn.Style = btnInPopupStyle;
                btn.Width = btnMajors.ActualWidth;
                btn.Click += (s, e) =>
                {
                    // Set idMajor
                    idMajor = major.IdMajors;
                    btnMajors.Content = major.Name;
                    popupMajor.IsOpen = false;

                    // Set idMainClass = null and set Content of btnMainCLass = default
                    idMainClass = null;
                    btnMainClass.Content = "Chọn lớp danh nghĩa";
                };
                popupMajorsData.Children.Add(btn);
            }

            AppDbContext._Context.MainClasses.ToList().ForEach(mainClass =>
            {
                Button btn = new Button();
                btn.Content = mainClass.Name;
                btn.Style = btnInPopupStyle;
                btn.Width = btnMainClass.ActualWidth;
                btn.Click += (s, e) =>
                {
                    idMainClass = mainClass.IdMainClass;
                    btnMainClass.Content = mainClass.Name;
                    popupMainClass.IsOpen = false;
                };
                popupMainClassData.Children.Add(btn);
            });
        }

        private void LoadDataToWindow()
        {
            idMajor = student.IdMajors;
            idMainClass = student.IdMainClass;
            btnMajors.Content = student.IdMajorsNavigation.Name;
            btnMainClass.Content = student.IdMainClassNavigation.Name;
            textBoxIdStudent.Text = student.IdStudent;
            textBoxName.Text = student.Name;
            textBoxDateOfBirth.Text = student.DateOfBirth.ToString();
            textBoxAddress.Text = student.Address;
            textBoxPhoneNumber.Text = student.PhoneNumber;
            textBoxEmail.Text = student.IdStudentNavigation.Email;
            textBoxPassword.Text = student.IdStudentNavigation.Password;
            if (student.Gender == true)
                radioButtonGenderMale.IsChecked = true;
            else
                RadioButtonGenderFemale.IsChecked = true;
        }

        private void textBoxDateOfBirth_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBoxDateOfBirth.Text.Length > 0)
                textBoxDateOfBirthPlaceHolder.Visibility = Visibility.Hidden;

            else
                textBoxDateOfBirthPlaceHolder.Visibility = Visibility.Visible;
        }

        private void btnMajors_Click(object sender, RoutedEventArgs e)
        {
            popupMajor.IsOpen = true;
        }

        private void btnMainClass_Click(object sender, RoutedEventArgs e)
        {
            if (idMajor == null)
            {
                AlertBox.Show("Vui lòng chọn chuyên ngành", "Thông báo", AlertButton.OK, AlertIcon.Warning);
            }
            else
            {
                popupMainClassData.Children.Clear();
                var mainClassList = AppDbContext._Context.MainClasses.Where(mainClass => mainClass.IdMajors == idMajor).ToList();
                foreach (var mainClass in mainClassList)
                {
                    Button btn = new Button();
                    btn.Content = mainClass.Name;
                    btn.Style = btnInPopupStyle;
                    btn.Width = btnMainClass.ActualWidth;
                    btn.Click += (s, e) =>
                    {
                        idMainClass = mainClass.IdMainClass;
                        btnMainClass.Content = mainClass.Name;
                        popupMainClass.IsOpen = false;
                    };
                    popupMainClassData.Children.Add(btn);
                }
                popupMainClass.IsOpen = true;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            string idStudent = textBoxIdStudent.Text.Trim();
            string address = textBoxAddress.Text.Trim();
            string dateOfBirth = textBoxDateOfBirth.Text.Trim();
            string name = textBoxName.Text.Trim();
            string phoneNumber = textBoxPhoneNumber.Text.Trim();
            string password = textBoxPassword.Text.Trim();
            string email = textBoxEmail.Text.Trim();

            // Check TextBox has value?
            if (idStudent.Length == 0 || address.Length == 0 || dateOfBirth.Length == 0 || name.Length == 0 || phoneNumber.Length == 0 || password.Length == 0 || email.Length == 0)
            {
                AlertBox.Show("Vui lòng nhập đầy đủ dữ liệu", "Thông báo", AlertButton.OK, AlertIcon.Information);
                return;
            }

            // Check Major has choose?
            if (idMajor == null)
            {
                AlertBox.Show("Vui lòng chọn chuyên ngành", "Thông báo", AlertButton.OK, AlertIcon.Information);
                return;
            }

            // Check MainCLass has choose
            if (idMainClass == null)
            {
                AlertBox.Show("Vui lòng chọn lớp danh nghĩa", "Thông báo", AlertButton.OK, AlertIcon.Information);
                return;
            }

            // Check Gender is checked
            if (radioButtonGenderMale.IsChecked.HasValue == false)
            {
                AlertBox.Show("Vui lòng chọn giới tính", "Thông báo", AlertButton.OK, AlertIcon.Information);
                return;
            }

            try
            {
                AppDbContext._Context.Students.Entry(this.student).State = EntityState.Detached;
                DateOnly dateOfBirthDateOnly = DateOnly.ParseExact(dateOfBirth, "dd/MM/yyyy");
                User user = new User
                {
                    Email = email,
                    IdRole = 2, // Id = 2 is Role of Student
                    IdUser = idStudent,
                    Password = password,
                };


                AppDbContext._Context.Users.Update(user);
                AppDbContext._Context.SaveChanges();
                AppDbContext._Context.Users.Entry(user).State = EntityState.Detached;
                Student student = new Student
                {
                    IdStudent = idStudent,
                    IdMainClass = idMainClass.Value,
                    IdMajors = idMajor.Value,
                    Address = address,
                    DateOfBirth = dateOfBirthDateOnly,
                    Avatar = null,
                    Gender = radioButtonGenderMale.IsChecked.Value,
                    Name = name,
                    PhoneNumber = phoneNumber
                };

                AppDbContext._Context.Students.Update(student);
                AppDbContext._Context.SaveChanges();
                AppDbContext._Context.Students.Entry(student).State = EntityState.Detached;
                AlertBox.Show("Đã sửa sinh viên thành công", "Thông báo", AlertButton.OK, AlertIcon.Success);
                this.Close();
            }

            catch (Exception ex)
            {
                AlertBox.Show($"Sửa sinh viên thất bại.\n{ex.Message}", "Lỗi", AlertButton.OK, AlertIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            var alertResult = AlertBox.Show("Bạn có chắc chắn muốn thoát không?", "", AlertButton.YesNo, AlertIcon.Question);
            if (alertResult == AlertResult.Yes)
                this.Close();
        }
    }
}