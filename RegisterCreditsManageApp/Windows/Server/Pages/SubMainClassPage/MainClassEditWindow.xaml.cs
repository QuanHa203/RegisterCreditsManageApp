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

namespace RegisterCreditsManageApp.Windows.Server.Pages.SubMainClassPage
{
    /// <summary>
    /// Interaction logic for MainClassEditWindow.xaml
    /// </summary>
    public partial class MainClassEditWindow : Window
    {
        int idMajor;
        int idCurrentSemester;
        int idMainClass;
        public MainClassEditWindow(int idMainClass)
        {
            InitializeComponent();
            this.idMainClass = idMainClass;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataToWindow();
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

        private void LoadDataToWindow()
        {
            MainClass mainClass = AppDbContext._Context.MainClasses.Include(mainClass => mainClass.IdCurrentRegisterSemesterNavigation)
                                                                   .Include(mainClass => mainClass.IdMajorsNavigation)
                                                                   .FirstOrDefault(mainClass => mainClass.IdMainClass == idMainClass)!;
            textBoxName.Text = mainClass.Name;
            textBoxCourseYear.Text = mainClass.CourseYear.ToString();
            idMajor = mainClass.IdMajors;
            idCurrentSemester = mainClass.IdCurrentRegisterSemester;

            btnCurrentSemester.Content = mainClass.IdCurrentRegisterSemesterNavigation.Name;
            btnMajors.Content = mainClass.IdMajorsNavigation.Name;
            AppDbContext._Context.MainClasses.Entry(mainClass).State = EntityState.Detached;
        }

        private void btnMajors_Click(object sender, RoutedEventArgs e)
        {
            popupMajor.IsOpen = true;
        }

        private void btnCurrentSemester_Click(object sender, RoutedEventArgs e)
        {
            popupCurrentSemester.IsOpen = true;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            string mainClassName = textBoxName.Text.Trim();
            string courseYear = textBoxCourseYear.Text.Trim();
            if (mainClassName.Length == 0 || courseYear.Length == 0)
            {
                AlertBox.Show("Vui lòng nhập đầy đủ dữ liệu", "Thông báo", AlertButton.OK, AlertIcon.Information);
                return;
            }

            try
            {
                MainClass mainClass = new MainClass
                {
                    IdMainClass = this.idMainClass,
                    CourseYear = Convert.ToInt32(courseYear),
                    IdMajors = idMajor,
                    Name = mainClassName,
                    IdCurrentRegisterSemester = idCurrentSemester
                };

                AppDbContext._Context.MainClasses.Update(mainClass);
                AppDbContext._Context.SaveChanges();
                AppDbContext._Context.MainClasses.Entry(mainClass).State = EntityState.Detached;
                AlertBox.Show("Đã chỉnh sừa lớp danh nghĩa thành công", "Thông báo", AlertButton.OK, AlertIcon.Success);
                this.Close();
            }
            catch (Exception ex)
            {
                AlertBox.Show($"Chỉnh sửa lớp danh nghĩa thất bại.\nLỗi: {ex.Message}", "Lỗi", AlertButton.OK, AlertIcon.Error);
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
