using Microsoft.Data.SqlClient;
using RegisterCreditsManageApp.Models;
using RegisterCreditsManageApp.Windows.Alert;
using System.Windows;
using System.Windows.Controls;

namespace RegisterCreditsManageApp.Windows.Server.Pages.SubStudentPage
{
    /// <summary>
    /// Interaction logic for StudentAddWindow.xaml
    /// </summary>
    public partial class StudentAddWindow : Window
    {
        int? idMajor;
        int? idMainClass;
        public StudentAddWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var majorList = AppDbContext._Context.Majors.ToList();
            foreach (var major in majorList)
            {
                RadioButton rb = new RadioButton();
                rb.Content = major.Name;
                rb.Style = customComboboxMajor.CustomComboboxStyleChildren;
                rb.Click += (s, e) =>
                {
                    // Set idMajor
                    idMajor = major.IdMajors;
                    customComboboxMajor._Text = major.Name;
                    customComboboxMajor._IsOpen = false;

                    // Set idMainClass = null and set Content of btnMainCLass = default
                    idMainClass = null;
                    customComboboxMainClass._Text = "Chọn lớp danh nghĩa";
                };
                customComboboxMajor.CustomComboboxChildren.Add(rb);
            }
        }

        private void btnMainClass_Click(object sender, RoutedEventArgs e)
        {
            if (idMajor == null)
            {
                AlertBox.Show("Vui lòng chọn chuyên ngành", "Thông báo", AlertButton.OK, AlertIcon.Warning);
                return;
            }

            if (idMainClass == null)
            {
                customComboboxMainClass.CustomComboboxChildren.Clear();
                var mainClassList = AppDbContext._Context.MainClasses.Where(mainClass => mainClass.IdMajors == idMajor).ToList();
                foreach (var mainClass in mainClassList)
                {
                    RadioButton rb = new RadioButton();
                    rb.Content = mainClass.Name;
                    rb.Style = customComboboxMainClass.CustomComboboxStyleChildren;
                    rb.Click += (s, e) =>
                    {
                        idMainClass = mainClass.IdMainClass;
                        customComboboxMainClass._Text = mainClass.Name;
                        customComboboxMainClass._IsOpen = false;
                    };
                    customComboboxMainClass.CustomComboboxChildren.Add(rb);
                }
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            string idStudent = textBoxIdStudent.Text.Trim();
            string address = textBoxAddress.Text.Trim();
            string dateOfBirth = textBoxDateOfBirth._Text.Trim();
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

            User user = new User
            {
                Email = email,
                IdRole = 2, // Id = 2 is Role of Student
                IdUser = idStudent,
                Password = password,
            };

            Student student = new Student
            {
                IdStudent = idStudent,
                IdMainClass = idMainClass.Value,
                IdMajors = idMajor.Value,
                Address = address,                
                Avatar = null,
                Gender = radioButtonGenderMale.IsChecked.Value,
                Name = name,
                PhoneNumber = phoneNumber
            };
            try
            {
                DateOnly dateOfBirthDateOnly = DateOnly.ParseExact(dateOfBirth, "dd/MM/yyyy");
                student.DateOfBirth = dateOfBirthDateOnly;

                AppDbContext._Context.Users.Add(user);
                AppDbContext._Context.SaveChanges();


                AppDbContext._Context.Students.Add(student);
                AppDbContext._Context.SaveChanges();

                AlertBox.Show("Đã thêm sinh viên thành công", "Thông báo", AlertButton.OK, AlertIcon.Success);
                this.Close();
            }

            catch (Exception ex)
            {
                var innerException = ex.InnerException;
                if(innerException != null )
                    AlertBox.Show($"Thêm sinh viên thất bại.\n{ex.InnerException.Message}", "Lỗi", AlertButton.OK, AlertIcon.Error);
                else
                    AlertBox.Show($"Thêm sinh viên thất bại.\n{ex.Message}", "Lỗi", AlertButton.OK, AlertIcon.Error);
            }
            finally
            {
                AppDbContext._Context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                AppDbContext._Context.Entry(student).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            }

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            var alertResult = AlertBox.Show("Bạn có chắc chắn muốn thoát không (Tất cả dữ liệu vừa nhập sẽ mất)?", "Cảnh báo", AlertButton.YesNo, AlertIcon.Warning);
            if (alertResult == AlertResult.Yes)
                this.Close();
        }
    }
}
