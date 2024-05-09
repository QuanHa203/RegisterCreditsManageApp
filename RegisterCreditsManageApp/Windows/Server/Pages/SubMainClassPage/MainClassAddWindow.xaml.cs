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
            Style btnInPopupStyle = this.Resources["BtnInPopup"] as Style;
            AppDbContext._Context.Majors.ToList().ForEach(major =>
            {
                Button btn = new Button();
                btn.Content = major.Name;
                btn.Style = btnInPopupStyle;
                btn.Width = btnMajors.ActualWidth;
                btn.Click += (s, e) =>
                {
                    idMajor = major.IdMajors;
                    btnMajors.Content = major.Name;
                    popupMajor.IsOpen = false;
                };
                popupMajorsData.Children.Add(btn);
            });

            AppDbContext._Context.Semesters.ToList().ForEach(semester =>
            {
                Button btn = new Button();
                btn.Content = semester.Name;
                btn.Style = btnInPopupStyle;
                btn.Width = btnMajors.ActualWidth;
                btn.Click += (s, e) =>
                {
                    idCurrentSemester = semester.IdSemester;
                    btnCurrentSemester.Content = semester.Name;
                    popupCurrentSemester.IsOpen = false;
                };
                popupCurrentSemesterData.Children.Add(btn);
            });
        }

        private void btnMajors_Click(object sender, RoutedEventArgs e)
        {
            popupMajor.IsOpen = true;
        }

        private void btnCurrentSemester_Click(object sender, RoutedEventArgs e)
        {
            popupCurrentSemester.IsOpen = true;
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
