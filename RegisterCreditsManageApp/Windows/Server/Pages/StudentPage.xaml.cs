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

        private void BtnShowPopup_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            var parent = btn.Parent as Panel;
            var popup = parent.Children[0] as Popup;
            popup.IsOpen = true;
        }

        private void RadioBtnRegisterCredit_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            var parent = radioButton.Parent as Panel;
            var idStudentTextBlock = parent.Children[3] as TextBlock;
            new StudentCurrentRegisterCreditsWindow(idStudentTextBlock.Text).ShowDialog();

            e.Handled = true;
        }

        private void RadioBtnEdit_Click(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            var parent = radioButton.Parent as Panel;
            var idStudentTextBlock = parent.Children[3] as TextBlock;
            new StudentEditWindow(idStudentTextBlock.Text).ShowDialog();
            LoadDataGrid();

            e.Handled = true;
        }

        private void RadioBtnDelete_Click(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            var parent = radioButton.Parent as Panel;
            var idStudentTextBlock = parent.Children[3] as TextBlock;
            string idStudent = idStudentTextBlock.Text;
            AlertResult alertResult = AlertBox.Show("Bạn có chắc muốn xóa sinh viên này không?", "Thông báo", AlertButton.YesNo, AlertIcon.Question);
            if (alertResult == AlertResult.Yes)
            {
                try
                {
                    var student = AppDbContext._Context.Students.FirstOrDefault(s => s.IdStudent == idStudent);
                    AppDbContext._Context.Students.Remove(student);                    

                    var user = AppDbContext._Context.Users.FirstOrDefault(u => u.IdUser == idStudent);
                    AppDbContext._Context.Users.Remove(user);

                    AppDbContext._Context.SaveChanges();
                    AlertBox.Show("Đã xóa sinh viên thành công!", "Thành công", AlertButton.OK, AlertIcon.Success);
                    LoadDataGrid();
                }
                catch (Exception ex)
                {
                    AlertBox.Show($"Lỗi, xóa sinh viên thất bại.\nLỗi: {ex.Message}", "Lỗi", AlertButton.OK, AlertIcon.Error);
                }
            }
            e.Handled = true;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            new StudentAddWindow().ShowDialog();
            LoadDataGrid();
        }

        public class Data
        {
            public string MajorName { get; set;}
            public int NumberOfSubject { get; set;}
        }
    }
}
