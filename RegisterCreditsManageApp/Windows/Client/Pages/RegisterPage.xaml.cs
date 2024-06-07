using Microsoft.EntityFrameworkCore;
using RegisterCreditsManageApp.Models;
using RegisterCreditsManageApp.Windows.Alert;
using RegisterCreditsManageApp.Windows.Client.Pages.ClassPendingRegisterPage;
using System.Windows;
using System.Windows.Controls;

namespace RegisterCreditsManageApp.Windows.Client.Pages
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        private List<Data> dataGridRegisterList = new List<Data>();
        private Student currentStudent = LoginWindow.CurrentStudent;
        private int idSemesterChoose;
        public RegisterPage()
        {
            InitializeComponent();            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RadioButtonClassWaitRegister.IsChecked = true;
            idSemesterChoose = currentStudent.IdMainClassNavigation.IdCurrentRegisterSemester;
            customComboboxChooseSemester._Text = currentStudent.IdMainClassNavigation.IdCurrentRegisterSemesterNavigation.Name;
            LoadDataPopupChooseSemester();
        }

        private void LoadDataPopupChooseSemester()
        {
            int currentSemester = currentStudent.IdMainClassNavigation.IdCurrentRegisterSemester;
            for (int i = 1; i <= currentSemester; i++)
            {
                RadioButton rb = new RadioButton();
                rb.Content = "Học kỳ " + i;                
                rb.Style = customComboboxChooseSemester.CustomComboboxStyleChildren;
                int index = i;
                rb.Click += (s, e) =>
                {
                    idSemesterChoose = index;
                    customComboboxChooseSemester._Text = $"Học kỳ {index}";
                    customComboboxChooseSemester._IsOpen = false;

                    if (RadioButtonClassWaitRegister.IsChecked!.Value == true)
                        GetDataGridClassWaitRegister();
                };
                customComboboxChooseSemester.CustomComboboxChildren.Add(rb);
            }
        }

        private void GetDataGridClassWaitRegister()
        {
            var classWaitRegisterList = AppDbContext._Context.RegisterCredits
                                        .Include(registerCredit => registerCredit.IdSubjectNavigation)
                                        .Where(registerCredit => registerCredit.IdStudent == currentStudent.IdStudent)
                                        .Where(registerCredit => registerCredit.IsRegister == false)
                                        .ToList();

            dataGridRegisterList.Clear();
            DataGridRegister.ItemsSource = null;
            for (int i = 0; i < classWaitRegisterList.Count; i++)
            {
                Data data = new Data
                {
                    NumericalOrder = i + 1,
                    IdSubject = classWaitRegisterList[i].IdSubjectNavigation.IdSubject,
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
            var classWaitRegisterList = AppDbContext._Context.RegisterCredits
                                        .Include(registerCredit => registerCredit.IdSubjectNavigation)
                                        .Where(registerCredit => registerCredit.IdStudent == currentStudent.IdStudent)
                                        .Where(registerCredit => registerCredit.IsRegister == true)
                                        .Where(registerCredit => registerCredit.IdSubjectNavigation.IdSemester == idSemesterChoose)
                                        .ToList();

            dataGridRegisterList.Clear();
            DataGridRegister.ItemsSource = null;
            for (int i = 0; i < classWaitRegisterList.Count; i++)
            {
                Data data = new Data
                {
                    NumericalOrder = i + 1,
                    IdSubject = classWaitRegisterList[i].IdSubjectNavigation.IdSubject,
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
            if (RadioButtonClassWaitRegister.IsChecked!.Value)
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
            public int IdSubject { get; set; }
            public int NumericalOrder { get; set; }
            public string IdClassRoom { get; set; } = null!;
            public string SubjectName { get; set; } = null!;
            public int NumberOfCredit { get; set; }

        }                


        private void DataGridRow_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DataGridRow dataGridRow = (sender as DataGridRow)!;
            var data = dataGridRow.Item as Data;

            var idSubject = data.IdSubject;

            //Lớp học phần chờ đăng ký
            if (RadioButtonClassWaitRegister.IsChecked == true)
            {
                new ClassPendingRegister(idSubject).ShowDialog();
                GetDataGridClassWaitRegister();
            }
        }
    }
}