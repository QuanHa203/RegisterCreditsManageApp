using Microsoft.EntityFrameworkCore;
using RegisterCreditsManageApp.Models;
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
        private List<Data> dataGridRegisterList;
        List<MainClass> mainClassList;
        List<MainClass> mainClassNotRegisteredList;
        List<MainClass> mainClassRegisteredList;
        
        public RegisterPage()
        {
            InitializeComponent();
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {            
            mainClassNotRegisteredList = new List<MainClass>();
            mainClassRegisteredList = new List<MainClass>();
            dataGridRegisterList =  new List<Data>();

            mainClassList = AppDbContext._Context.MainClasses.Include(mainClass => mainClass.IdCurrentRegisterSemesterNavigation)
                                                                 .Include(mainClass => mainClass.IdMajorsNavigation)
                                                                 .ToList();
            var classRoomList = AppDbContext._Context.ClassRooms.ToList();
            foreach (var mainClass in mainClassList)
            {
                foreach (var classRoom in classRoomList)
                {
                    
                }
            }
            RadioButtonClassNotRegistered.IsChecked = true;
        }

        private void GetDataGridClassNotRegistered()
        {            
            foreach (var mainClass in mainClassList)
            {
                Data data = new Data
                {
                    IdSemester = mainClass.IdCurrentRegisterSemester.Value,
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
            
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchTextBox.Text.Length > 0)
                SearchPlaceHolder.Visibility = Visibility.Hidden;

            else
                SearchPlaceHolder.Visibility = Visibility.Visible;
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            var parent = btn.Parent as Panel;
            var idSemesterTextBlock = parent.Children[1] as TextBlock;
            var idMajorsTextBlock = parent.Children[2] as TextBlock;
            var idClassNameTextBlock = parent.Children[3] as TextBlock;

            int idSemester = Convert.ToInt32(idSemesterTextBlock.Text);
            int idMajors = Convert.ToInt32(idMajorsTextBlock.Text);
            int idMainClass = Convert.ToInt32(idClassNameTextBlock.Text);
            new RegisterClassesRoomWindow(idSemester, idMajors, idMainClass).ShowDialog();
        }

        private void BtnShowPopup_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;            
            var parent = btn.Parent as Panel;
            var popup = parent.Children[0] as Popup;
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
                MessageBox.Show("Hello");
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
    }
}
