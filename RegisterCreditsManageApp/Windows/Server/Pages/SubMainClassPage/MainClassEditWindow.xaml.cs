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

        private void LoadDataToWindow()
        {
            MainClass mainClass = AppDbContext._Context.MainClasses.Include(mainClass => mainClass.IdCurrentRegisterSemesterNavigation)
                                                                   .Include(mainClass => mainClass.IdMajorsNavigation)
                                                                   .FirstOrDefault(mainClass => mainClass.IdMainClass == idMainClass)!;
            textBoxName.Text = mainClass.Name;
            textBoxCourseYear.Text = mainClass.CourseYear.ToString();
            idMajor = mainClass.IdMajors;
            idCurrentSemester = mainClass.IdCurrentRegisterSemester;

            customComboboxCurrentSemester._Text = mainClass.IdCurrentRegisterSemesterNavigation.Name;
            customComboboxMajor._Text = mainClass.IdMajorsNavigation.Name;
            AppDbContext._Context.MainClasses.Entry(mainClass).State = EntityState.Detached;
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
