using RegisterCreditsManageApp.Windows.Alert;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

            List<Data> list = new List<Data>()
            {
                new Data {Id = 1, ClassName = "ClassName1", CourseName = "CourseName1", MajorsName = "MajorsName1", SemesterName = "SemesterName1"},
                new Data {Id = 2, ClassName = "ClassName2", CourseName = "CourseName2", MajorsName = "MajorsName2", SemesterName = "SemesterName2"},
                new Data {Id = 3, ClassName = "ClassName3", CourseName = "CourseName3", MajorsName = "MajorsName3", SemesterName = "SemesterName3"},
                new Data {Id = 4, ClassName = "ClassName4", CourseName = "CourseName4aaaaaaaaaaaaaaaaaaaaaa", MajorsName = "MajorsName4", SemesterName = "SemesterName4"},
                new Data {Id = 5, ClassName = "ClassName5", CourseName = "CourseName5", MajorsName = "MajorsName5", SemesterName = "SemesterName5"},
                new Data {Id = 6, ClassName = "ClassName6", CourseName = "CourseName6", MajorsName = "MajorsName6", SemesterName = "SemesterName6"},
                new Data {Id = 7, ClassName = "ClassName7", CourseName = "CourseName7", MajorsName = "MajorsName7", SemesterName = "SemesterName7"},
                new Data {Id = 8, ClassName = "ClassName8", CourseName = "CourseName8", MajorsName = "MajorsName8", SemesterName = "SemesterName8"},
                new Data {Id = 9, ClassName = "ClassName9", CourseName = "CourseName9", MajorsName = "MajorsName9", SemesterName = "SemesterName9"}
            };
            DataGridRegister.ItemsSource = list;
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
            DataGridRegister.ItemsSource = dataGridRegisterList;
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            var parent = btn.Parent as Panel;
            var id = parent.Children[parent.Children.Count - 1] as TextBlock;
            MessageBox.Show($"Click {id.Text}");
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
        public int Id { get; set; }
        public string ClassName { get; set; }
        public string MajorsName { get; set; }
        public string SemesterName { get; set; }
        public string CourseName { get; set; }
    }
}
