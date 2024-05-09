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

namespace RegisterCreditsManageApp.Windows.Server.Pages.SubWindow
{
    /// <summary>
    /// Interaction logic for ModifySubject.xaml
    /// </summary>
    public partial class ModifySubject : Window
    {
        private int idMajor;
        private int idSubject;
        private int? idSemester;
        public ModifySubject(int idSubject, int idMajor)
        {
            InitializeComponent();
            this.idSubject = idSubject;
            this.idMajor = idMajor;
            // Load current major name into textBox
            var subject = AppDbContext._Context.Subjects.Include((subject) => subject.IdSemesterNavigation).FirstOrDefault((Subject s) => s.IdSubject == idSubject);
            SubjectNameTextBox.Text = $"{subject.Name}";
            CreditNumberTextBox.Text = $"{subject.NumberOfCredits}";
            AppDbContext._Context.Subjects.Entry(subject).State = EntityState.Detached;
            DisplaySemester();
            this.idMajor = idMajor;
        }
        public void DisplaySemester()
        {
            var btnStyle = this.Resources["SemesterBtn"] as Style;
            var SemesterList = AppDbContext._Context.Semesters.ToList();
            foreach (var semester in SemesterList)
            {
                Button btn = new Button()
                {
                    Style = btnStyle,
                    Content = semester.Name,
                };
                btn.Click += (s, e) =>
                {
                    idSemester = semester.IdSemester;
                    SemesterTitle.Text = semester.Name;
                    SemesterPopup.IsOpen = false;
                };
                SemesterListPopup.Children.Add(btn);
            }
        }
        private void SemesterToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (SemesterToggleButton.IsChecked == true)
            {
                SemesterPopup.IsOpen = true;
            }
            else
            {
                SemesterPopup.IsOpen = false;
            }
        }

        private void CancelModifyingSubjectBtn_Click(object sender, RoutedEventArgs e)
        {
            AlertResult result = AlertBox.Show("Bạn có chắc muốn hủy chỉnh sửa môn học này không?", "Thông báo", AlertButton.YesNo, AlertIcon.Question);
            if (result == AlertResult.Yes)
            {
                this.Close();
            }
        }

        private void UpdateSubjectBtn_Click(object sender, RoutedEventArgs e)
        {
            if (SubjectNameTextBox.Text == "" || CreditNumberTextBox.Text == "" || idSemester == null)
            {
                AlertBox.Show("Vui lòng nhập đầy đủ dữ liệu tên ngành học", "Thông báo", AlertButton.OK, AlertIcon.Warning);
                return;
            }

            Subject subject = new Subject { IdMajors = idMajor,IdSemester = idSemester.Value, IdSubject = idSubject, Name = SubjectNameTextBox.Text.Trim(), NumberOfCredits = int.Parse(CreditNumberTextBox.Text.Trim()) };
            AppDbContext._Context.Subjects.Update(subject);
            AppDbContext._Context.SaveChanges();

            AlertBox.Show("Đã sửa thông tin môn học thành công", "Thông báo", AlertButton.OK, AlertIcon.Information);
            this.Close();
        }
    }
}
