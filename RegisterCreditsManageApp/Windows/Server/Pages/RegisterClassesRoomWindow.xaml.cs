using RegisterCreditsManageApp.Models;
using RegisterCreditsManageApp.Windows.Alert;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace RegisterCreditsManageApp.Windows.Server.Pages
{
    /// <summary>
    /// Interaction logic for RegisterClassesRoomWindow.xaml
    /// </summary>
    public partial class RegisterClassesRoomWindow : Window
    {
        private List<Data> dataGridRegisterClassesRoom = new List<Data>();
        public RegisterClassesRoomWindow(int idSemester, int idMajors, int idMainClass)
        {
            InitializeComponent();
            GetDataGridRegisterClassesRoom(idSemester, idMajors, idMainClass);
        }

        private void GetDataGridRegisterClassesRoom(int idSemester, int idMajors, int idMainClass)
        {
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
                dataGridRegisterClassesRoom.Add(data);
            }

            DataGridRegisterClassesRoom.ItemsSource = dataGridRegisterClassesRoom;
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
