using Microsoft.EntityFrameworkCore;
using RegisterCreditsManageApp.Models;
using RegisterCreditsManageApp.Windows.Alert;
using System.Windows;
using System.Windows.Controls;

namespace RegisterCreditsManageApp.Windows.Server.Pages.SubStudentPage
{
    /// <summary>
    /// Interaction logic for StudentEditWindow.xaml
    /// </summary>
    public partial class StudentEditWindow : Window
    {
        private Student student;
        int? idMajor;
        int? idMainClass;
        public StudentEditWindow(string idStudent)
        {
            InitializeComponent();
            student = AppDbContext._Context.Students.Include(student => student.IdStudentNavigation)
                                                    .Include(student => student.IdMajorsNavigation)
                                                    .Include(student => student.IdMainClassNavigation)
                                                    .FirstOrDefault(s => s.IdStudent == idStudent)!;
            idMajor = student.IdMajors;
            idMainClass = student.IdMainClass;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataToWindow();
            var majorList = AppDbContext._Context.Majors.ToList();

            // Load MajorList in Combobox
            foreach (var major in majorList)
            {
                RadioButton rb = new RadioButton();
                rb.Content = major.Name;
                rb.Style = customComboboxMajor.CustomComboboxStyleChildren;
                rb.Click += (s, e) =>
                {
                    rb.IsChecked = true;
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

            // Load MainClassList in Combobox
            AppDbContext._Context.MainClasses.Where(mainClass => mainClass.IdMajors == idMajor).ToList().ForEach(mainClass =>
            {
                RadioButton rb = new RadioButton();
                rb.Content = mainClass.Name;
                rb.Style = customComboboxMainClass.CustomComboboxStyleChildren;
                rb.Click += (s, e) =>
                {
                    rb.IsChecked = true;
                    idMainClass = mainClass.IdMainClass;
                    customComboboxMainClass._Text = mainClass.Name;
                    customComboboxMainClass._IsOpen = false;
                };
                customComboboxMainClass.CustomComboboxChildren.Add(rb);
            });
        }

        private void LoadDataToWindow()
        {
            customComboboxMajor._Text = student.IdMajorsNavigation.Name;
            customComboboxMainClass._Text = student.IdMainClassNavigation.Name;
            textBoxIdStudent.Text = student.IdStudent.Trim();
            textBoxName.Text = student.Name.Trim();
            textBoxDateOfBirth._Text = student.DateOfBirth!.Value.ToString();
            textBoxAddress.Text = student.Address!.Trim();
            textBoxPhoneNumber.Text = student.PhoneNumber!.Trim();
            textBoxEmail.Text = student.IdStudentNavigation.Email.Trim();
            textBoxPassword.Text = student.IdStudentNavigation.Password.Trim();

            if (student.Gender == true)
                radioButtonGenderMale.IsChecked = true;
            else
                RadioButtonGenderFemale.IsChecked = true;
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
                        rb.IsChecked = true;
                        idMainClass = mainClass.IdMainClass;
                        customComboboxMainClass._Text = mainClass.Name;
                        customComboboxMainClass._IsOpen = false;
                    };
                    customComboboxMainClass.CustomComboboxChildren.Add(rb);
                }
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
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

            try
            {
                AppDbContext._Context.Users.Update(user);
                AppDbContext._Context.SaveChanges();
                DateOnly dateOfBirthDateOnly = DateOnly.ParseExact(dateOfBirth, "dd/MM/yyyy");

                // Update Student
                student.IdMainClass = idMainClass.Value;
                student.IdMajors = idMajor.Value;
                student.Address = address;
                student.DateOfBirth = dateOfBirthDateOnly;
                student.Avatar = null;
                student.Gender = radioButtonGenderMale.IsChecked.Value;
                student.Name = name;
                student.PhoneNumber = phoneNumber;

                AppDbContext._Context.Students.Update(student);
                AppDbContext._Context.SaveChanges();
                AlertBox.Show("Đã sửa sinh viên thành công", "Thông báo", AlertButton.OK, AlertIcon.Success);
                this.Close();
            }

            catch (Exception ex)
            {
                AlertBox.Show($"Sửa sinh viên thất bại.\n{ex.Message}", "Lỗi", AlertButton.OK, AlertIcon.Error);
            }
            finally
            {
                AppDbContext._Context.Users.Entry(user).State = EntityState.Detached;
                AppDbContext._Context.Students.Entry(this.student).State = EntityState.Detached;
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