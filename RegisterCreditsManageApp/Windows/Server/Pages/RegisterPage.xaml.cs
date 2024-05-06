using Microsoft.EntityFrameworkCore;
using RegisterCreditsManageApp.Models;
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
        private SolidColorBrush colorFocus;
        private SolidColorBrush colorLostFocus;
        private List<Data> dataGridRegisterList;        

        public RegisterPage()
        {
            InitializeComponent();
            DataContext = this;
            colorFocus = App.Current.Resources["DefaultFocusForeground"] as SolidColorBrush;
            colorLostFocus = App.Current.Resources["SecondForegroundColor"] as SolidColorBrush;
            GetDataGridRegister();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchTextBox.Text.Length > 0)

                SearchPlaceHolder.Visibility = Visibility.Hidden;
            else
                SearchPlaceHolder.Visibility = Visibility.Visible;
        }

        private void GetDataGridRegister()
        {
            dataGridRegisterList = new List<Data>();
            var mainClassList = AppDbContext._Context.MainClasses.Include(mainClass => mainClass.IdCurrentRegisterSemesterNavigation)
                                                                 .Include(mainClass => mainClass.IdMajorsNavigation)
                                                                 .ToList();
            foreach (var mainClass in mainClassList)
            {
                Data data = new Data
                {
                    IdSemester = mainClass.IdCurrentRegisterSemester.Value,
                    IdMajors = mainClass.IdMajors,
                    ClassName = mainClass.Name,
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
            Button btn = sender as Button;
            var parent = btn.Parent as Panel;
            var idSemesterTextBlock = parent.Children[parent.Children.Count - 2] as TextBlock;
            var idMajorsTextBlock = parent.Children[parent.Children.Count - 1] as TextBlock;
            AlertBox.Show("Hello");
            MessageBox.Show($"IdSemester: {idSemesterTextBlock.Text}");
            MessageBox.Show($"IdMajors: {idMajorsTextBlock.Text}");
        }

        private void BtnShowPopup_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;            
            var parent = btn.Parent as Panel;
            var popup = parent.Children[0] as Popup;
            popup.IsOpen = true;
        }
    }

    public class Data
    {
        public int IdSemester { get; set; }
        public int IdMajors { get; set; }
        public string ClassName { get; set; }
        public string MajorsName { get; set; }
        public string SemesterName { get; set; }
        public int CourseYear { get; set; }
    }
}
