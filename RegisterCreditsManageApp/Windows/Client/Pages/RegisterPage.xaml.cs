using Microsoft.EntityFrameworkCore;
using RegisterCreditsManageApp.Models;
using System.Windows;
using System.Windows.Controls;

namespace RegisterCreditsManageApp.Windows.Client.Pages
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        private int idSemester = 1;
        private List<Data> dataGridRegisterList = new List<Data>();
        private Student currentStudent = LoginWindow.CurrentStudent;

        public RegisterPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RadioButtonClassWaitRegister.IsChecked = true;
            LoadDataPopupChooseSemester();
        }

        private void LoadDataPopupChooseSemester()
        {
            Style btnInPopupStyle = this.Resources["BtnInPopup"] as Style;
            int currentSemester = currentStudent.IdMainClassNavigation.IdCurrentRegisterSemester;
            for (int i = 1; i <= currentSemester; i++)
            {
                Button btn = new Button();
                btn.Content = "Học kỳ " + i;
                btn.Style = btnInPopupStyle;
                btn.Width = btnChooseSemester.ActualWidth;
                btn.Click += (s, e) =>
                {
                    idSemester = i;
                    btnChooseSemester.Content = btn.Content;
                    popupChooseSemester.IsOpen = false;
                    GetDataGridClassWaitRegister();
                };
                popupChooseSemesterData.Children.Add(btn);
            }
        }

        private void GetDataGridClassWaitRegister()
        {
            var classWaitRegisterList = AppDbContext._Context.RegisterCredits.Include(registerCredit => registerCredit.IdSubjectNavigation)
                                                                             .Where(registerCredit => registerCredit.IsRegister == false)
                                                                             .Where(registerCredit => registerCredit.IdStudent == currentStudent.IdStudent)
                                                                             .ToList();

            dataGridRegisterList.Clear();
            DataGridRegister.ItemsSource = null;
            for (int i = 0; i < classWaitRegisterList.Count; i++)
            {
                Data data = new Data
                {
                    NumericalOrder = i + 1,
                    IdClassRoom = classWaitRegisterList[i].IdClassRoom,
                    NumberOfCredit = classWaitRegisterList[i].IdSubjectNavigation.NumberOfCredits,
                    SubjectName = classWaitRegisterList[i].IdSubjectNavigation.Name
                };
                dataGridRegisterList.Add(data);
            }

            DataGridRegister.ItemsSource = dataGridRegisterList;
        }

        private void GetDataGridClassRegistered()
        {
            var classWaitRegisterList = AppDbContext._Context.RegisterCredits.Include(registerCredit => registerCredit.IdClassRoomNavigation)
                                                                             .Include(registerCredit => registerCredit.IdSubjectNavigation)
                                                                             .Where(registerCredit => registerCredit.IsRegister == true)
                                                                             .Where(registerCredit => registerCredit.IdStudent == currentStudent.IdStudent)
                                                                             .Where(registerCredit => registerCredit.IdClassRoomNavigation.IdSemester == idSemester).ToList();

            dataGridRegisterList.Clear();
            DataGridRegister.ItemsSource = null;
            for (int i = 0; i < classWaitRegisterList.Count; i++)
            {
                Data data = new Data
                {
                    NumericalOrder = i + 1,
                    IdClassRoom = classWaitRegisterList[i].IdClassRoom,
                    NumberOfCredit = classWaitRegisterList[i].IdSubjectNavigation.NumberOfCredits,
                    SubjectName = classWaitRegisterList[i].IdSubjectNavigation.Name
                };
                dataGridRegisterList.Add(data);
            }

            DataGridRegister.ItemsSource = dataGridRegisterList;
        }

        private void RadioButtonClassNotRegistered_CheckedAndUnchecked(object sender, RoutedEventArgs e)
        {
            if (RadioButtonClassWaitRegister.IsChecked.Value)
            {
                GetDataGridClassWaitRegister();
            }
            else
            {
                GetDataGridClassRegistered();
            }
        }

        public class Data
        {
            public int NumericalOrder { get; set; }
            public string IdClassRoom { get; set; }
            public string SubjectName { get; set; }
            public int NumberOfCredit { get; set; }

        }

        private void btnChooseSemester_Click(object sender, RoutedEventArgs e)
        {
            popupChooseSemester.IsOpen = true;
        }
    }
}
