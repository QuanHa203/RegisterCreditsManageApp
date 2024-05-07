using RegisterCreditsManageApp.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace RegisterCreditsManageApp.Windows.Server.Pages
{
    /// <summary>
    /// Interaction logic for RegisterClassesRoomWindow.xaml
    /// </summary>
    public partial class RegisterClassesRoomNotRegisterWindow : Window
    {
        int idSemester;
        int idMajors;
        int idMainClass;
        private List<Data> dataGrid = new List<Data>();
        public RegisterClassesRoomNotRegisterWindow(int idSemester, int idMajors, int idMainClass)
        {
            InitializeComponent();
            this.idSemester = idSemester;
            this.idMajors = idMajors;
            this.idMainClass = idMainClass;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetDataGrid(idSemester, idMajors, idMainClass);
        }

        private void GetDataGrid(int idSemester, int idMajors, int idMainClass)
        {
            // Reset 
            dataGrid.Clear();
            DataGridRegisterClassesRoom.ItemsSource = null;

            string className = AppDbContext._Context.MainClasses.ToList().FirstOrDefault(mainClass => mainClass.IdMainClass == idMainClass).Name;

            // Get SubjectList based on IdMajors and IdSemester 
            var subjectList = (from list in AppDbContext._Context.Subjects
                               where list.IdMajors == idMajors && list.IdSemester == idSemester
                               select list).ToList();

            // Get ClassRoomList based on IdMainClass
            var classRoomList = (from list in AppDbContext._Context.ClassRooms
                                 where list.IdMainClass == idMainClass
                                 select list).ToList();

            // Get SubjectNeedRegisterList 
            var subjectNeedRegisterList = (from subject in subjectList
                                           where !classRoomList.Any(cr => cr.IdSubject == subject.IdSubject)
                                           select subject).ToList();


            foreach (var subject in subjectNeedRegisterList)
            {
                Data data = new Data
                {
                    IdSubject = subject.IdSubject,
                    IdMainClass = idMainClass,
                    SubjectName = subject.Name,
                    NumberOfCredits = subject.NumberOfCredits,
                    MainClassName = className
                };
                dataGrid.Add(data);
            }

            DataGridRegisterClassesRoom.ItemsSource = dataGrid;
        }        

        private void BtnShowPopup_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var parent = btn.Parent as Panel;
            var popup = parent.Children[0] as Popup;
            popup.IsOpen = true;
        }

        private void BtnAddClassRoom_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            var parent = btn.Parent as Panel;
            var idSubjectTextBlock = parent.Children[1] as TextBlock;
            var idMainClassTextBlock = parent.Children[2] as TextBlock;

            int idSubject = Convert.ToInt32(idSubjectTextBlock.Text);
            int idMainClass = Convert.ToInt32(idMainClassTextBlock.Text);
            new RegisterCreateClassRoomWindow(idSubject, idMainClass).ShowDialog();
            GetDataGrid(idSemester, idMajors, idMainClass);
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public class Data
        {
            public int IdSubject { get; set; }
            public int IdMainClass { get; set; }
            public string SubjectName { get; set; }
            public int NumberOfCredits { get; set; }
            public string MainClassName { get; set; }
        }
    }
}
