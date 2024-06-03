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

namespace RegisterCreditsManageApp.Windows.Server.Pages.SubMainClassPage
{
    /// <summary>
    /// Interaction logic for MainClassAddWindow.xaml
    /// </summary>
    public partial class MainClassAddWindow : Window
    {
        int? idMajor;
        int? idCurrentSemester;
        public MainClassAddWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AppDbContext._Context.Majors.ToList().ForEach(major =>
            {
                RadioButton rb = new RadioButton();
                rb.Content = major.Name;
                rb.Style = customComboboxMajor.CustomComboboxStyleChildren;
                rb.Click += (s, e) =>
                {
                    idMajor = major.IdMajors;
                    customComboboxMajor._Text = major.Name;
                    customComboboxMajor._IsOpen = false;
                };
                customComboboxMajor.CustomComboboxChildren.Add(rb);
            });

            AppDbContext._Context.Semesters.ToList().ForEach(semester =>
            {
                RadioButton rb = new RadioButton();
                rb.Content = semester.Name;
                rb.Style = customComboboxCurrentSemester.CustomComboboxStyleChildren;
                rb.Click += (s, e) =>
                {
                    idCurrentSemester = semester.IdSemester;
                    customComboboxCurrentSemester._Text = semester.Name;
                    customComboboxCurrentSemester._IsOpen = false;
                };
                customComboboxCurrentSemester.CustomComboboxChildren.Add(rb);
            });
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            string mainClassName = textBoxName.Text.Trim();
            string courseYear = textBoxCourseYear.Text.Trim();
            if(mainClassName.Length == 0 || courseYear.Length == 0)
            {
                AlertBox.Show("Vui lòng nhập đầy đủ dữ liệu", "Thông báo", AlertButton.OK, AlertIcon.Information);
                return;
            }

            if(idMajor == null)
            {
                AlertBox.Show("Vui lòng chọn chuyên ngành", "Thông báo", AlertButton.OK, AlertIcon.Information);
                return;
            }

            if(idCurrentSemester == null)
            {
                AlertBox.Show("Vui lòng chọn học kỳ hiện tại", "Thông báo", AlertButton.OK, AlertIcon.Information);
                return;
            }

            try
            {
                MainClass mainClass = new MainClass
                {
                    CourseYear = Convert.ToInt32(courseYear),
                    IdMajors = idMajor.Value,
                    Name = mainClassName,
                    IdCurrentRegisterSemester = idCurrentSemester.Value
                };

                AppDbContext._Context.MainClasses.Add(mainClass);
                AppDbContext._Context.SaveChanges();
                AlertBox.Show("Đã thêm lớp danh nghĩa thành công", "Thông báo", AlertButton.OK, AlertIcon.Success);
                this.Close();
            }
            catch (Exception ex)
            {
                AlertBox.Show($"Thêm lớp danh nghĩa thất bại.\nLỗi: {ex.Message}", "Lỗi", AlertButton.OK, AlertIcon.Error);
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
