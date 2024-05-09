using Microsoft.EntityFrameworkCore;
using RegisterCreditsManageApp.Models;
using RegisterCreditsManageApp.Windows.Alert;
using RegisterCreditsManageApp.Windows.Server.Pages.SubStudentPage;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace RegisterCreditsManageApp.Windows.Server.Pages
{
    /// <summary>
    /// Interaction logic for StudentPage.xaml
    /// </summary>
    public partial class StudentPage : Page
    {
        public StudentPage()
        {
            InitializeComponent();            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            LoadDataGrid();
        }

        private void LoadDataGrid()
        {
            DataGridStudent.ItemsSource = null;
            List<Student> studentList = null;
            studentList = AppDbContext._Context.Students.Include((student) => student.IdMainClassNavigation).ToList();
            DataGridStudent.ItemsSource = studentList;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchTextBox.Text.Length > 0)
                SearchPlaceHolder.Visibility = Visibility.Hidden;

            else
                SearchPlaceHolder.Visibility = Visibility.Visible;
        }

        private void BtnShowPopup_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            var parent = btn.Parent as Panel;
            var popup = parent.Children[0] as Popup;
            popup.IsOpen = true;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            new StudentAddWindow().ShowDialog();
            LoadDataGrid();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            var parent = btn.Parent as Panel;
            var idStudentTextBlock = parent.Children[2] as TextBlock;
            new StudentEditWindow(idStudentTextBlock.Text).ShowDialog();
            LoadDataGrid();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            var parent = btn.Parent as Panel;
            var idStudentTextBlock = parent.Children[2] as TextBlock;
            string idStudent = idStudentTextBlock.Text;
            AlertResult alertResult = AlertBox.Show("Bạn có chắc muốn xóa sinh viên này không?", "Thông báo", AlertButton.YesNo, AlertIcon.Question);
            if(alertResult == AlertResult.Yes)
            {
                try
                {
                    var student = AppDbContext._Context.Students.FirstOrDefault(student => student.IdStudent == idStudent);
                    AppDbContext._Context.Remove(student);
                    AppDbContext._Context.SaveChanges();
                    AlertBox.Show("Đã xóa sinh viên thành công!", "Thành công", AlertButton.OK, AlertIcon.Success);
                    LoadDataGrid();
                }
                catch (Exception ex)
                {
                    AlertBox.Show($"Lỗi, xóa sinh viên thất bại.\nLỗi: {ex.Message}", "Lỗi", AlertButton.OK, AlertIcon.Error);
                }
            }


        }
        public class Data
        {
            public string MajorName { get; set;}
            public int NumberOfSubject { get; set;}
        }
    }
}
