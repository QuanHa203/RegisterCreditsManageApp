using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RegisterCreditsManageApp.Models;
using RegisterCreditsManageApp.UC;
using RegisterCreditsManageApp.Windows.Alert;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace RegisterCreditsManageApp.Windows.Server.Pages
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        private List<Data> dataGridRegisterList = new List<Data>();   
        
        public RegisterPage()
        {            
            InitializeComponent();
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RadioButtonClassNotRegistered.IsChecked = true;
        }        

        private void GetDataGridClassNotRegistered()
        {
            string sqlMainClassNotRegistered = @"SELECT mc.*
                            FROM MainClass mc
                            WHERE EXISTS (
                                SELECT s.IdSubject
                                FROM Subject s
                                WHERE s.IdSemester = mc.IdCurrentRegisterSemester
                                EXCEPT
                                SELECT cr.IdSubject
                                FROM ClassRoom cr
                                WHERE cr.IdMainClass = mc.IdMainClass AND cr.IdSemester = mc.IdCurrentRegisterSemester)";
            List<MainClass> mainClassNotRegisteredList = AppDbContext._Context.MainClasses.FromSqlRaw(sqlMainClassNotRegistered).Include(mc => mc.IdCurrentRegisterSemesterNavigation).Include(mc => mc.IdMajorsNavigation).ToList();

            dataGridRegisterList.Clear();
            DataGridRegister.ItemsSource = null;
            foreach (var mainClass in mainClassNotRegisteredList)
            {
                Data data = new Data
                {
                    IdSemester = mainClass.IdCurrentRegisterSemester,
                    IdMajors = mainClass.IdMajors,
                    IdMainClass = mainClass.IdMainClass,
                    MainClassName = mainClass.Name,
                    MajorsName = mainClass.IdMajorsNavigation.Name,
                    SemesterName = mainClass.IdCurrentRegisterSemesterNavigation.Name,
                    CourseYear = mainClass.CourseYear
                };
                dataGridRegisterList.Add(data);
            }
            DataGridRegister.ItemsSource = dataGridRegisterList;
        }

        private void GetDataGridClassRegistered() 
        {
            string sqlMainClassRegistered = @"SELECT mc.*
                            FROM MainClass mc
                            WHERE NOT EXISTS (
                                SELECT s.IdSubject
                                FROM Subject s
                                WHERE s.IdSemester = mc.IdCurrentRegisterSemester
                                EXCEPT
                                SELECT cr.IdSubject
                                FROM ClassRoom cr
                                WHERE cr.IdMainClass = mc.IdMainClass AND cr.IdSemester = mc.IdCurrentRegisterSemester)";

            List<MainClass> mainClassRegisteredList = AppDbContext._Context.MainClasses.FromSqlRaw(sqlMainClassRegistered).Include(mc => mc.IdCurrentRegisterSemesterNavigation).Include(mc => mc.IdMajorsNavigation).ToList();

            dataGridRegisterList.Clear();
            DataGridRegister.ItemsSource = null;
            foreach (var mainClass in mainClassRegisteredList)
            {
                Data data = new Data
                {
                    IdSemester = mainClass.IdCurrentRegisterSemester,
                    IdMajors = mainClass.IdMajors,
                    IdMainClass = mainClass.IdMainClass,
                    MainClassName = mainClass.Name,
                    MajorsName = mainClass.IdMajorsNavigation.Name,
                    SemesterName = mainClass.IdCurrentRegisterSemesterNavigation.Name,
                    CourseYear = mainClass.CourseYear
                };
                dataGridRegisterList.Add(data);
            }
            DataGridRegister.ItemsSource = dataGridRegisterList;
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            var parent = rb.Parent as Panel;
            var idSemesterTextBlock = parent.Children[1] as TextBlock;
            var idMajorsTextBlock = parent.Children[2] as TextBlock;
            var idClassNameTextBlock = parent.Children[3] as TextBlock;

            int idSemester = Convert.ToInt32(idSemesterTextBlock.Text);
            int idMajors = Convert.ToInt32(idMajorsTextBlock.Text);
            int idMainClass = Convert.ToInt32(idClassNameTextBlock.Text);

            if (RadioButtonClassNotRegistered.IsChecked == true)
            {
                new RegisterClassesRoomNotRegisterWindow(idSemester, idMajors, idMainClass).ShowDialog();
                GetDataGridClassNotRegistered();
            }
            else
            {
                new RegisterClassesRoomRegisteredWindow(idSemester, idMajors, idMainClass).ShowDialog();
                GetDataGridClassRegistered();
            }
        }

        private void BtnShowPopup_Click(object sender, RoutedEventArgs e)
        {            
            Button btn = sender as Button;
            var parent = btn.Parent as Panel;
            var popup = parent.Children[0] as Popup;
            var stackPanelInPopup = ((popup.Child as Border)!.Child as StackPanel)!;
            RadioButton rbInPopup = (stackPanelInPopup.Children[0] as RadioButton)!;

            if(RadioButtonClassNotRegistered.IsChecked!.Value)
                rbInPopup.Content = "Đăng ký lớp học phần";
            else
                rbInPopup.Content = "Chỉnh sửa lớp học phần";
            popup.IsOpen = true;            
        }

        private void RadioButtonClassNotRegistered_CheckedAndUnchecked(object sender, RoutedEventArgs e)
        {
            if(RadioButtonClassNotRegistered.IsChecked == true)
            {
                GetDataGridClassNotRegistered();
            }
            else if (RadioButtonClassNotRegistered.IsChecked == false)
            {
                GetDataGridClassRegistered();
            }
        }

        public class Data
        {
            public int IdSemester { get; set; }
            public int IdMajors { get; set; }
            public int IdMainClass { get; set; }
            public string MainClassName { get; set; }
            public string MajorsName { get; set; }
            public string SemesterName { get; set; }
            public int CourseYear { get; set; }
        }

        private void searchTextBox_Click(object sender, RoutedEventArgs e)
        {
            AlertBox.Show("Chức năng đang bảo trì!", "Thông báo", AlertButton.OK, AlertIcon.Warning);            
        }
    }
}
