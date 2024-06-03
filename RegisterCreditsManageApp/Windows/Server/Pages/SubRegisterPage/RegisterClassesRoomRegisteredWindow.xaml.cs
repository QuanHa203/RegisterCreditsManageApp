using RegisterCreditsManageApp.Models;
using RegisterCreditsManageApp.Windows.Alert;
using RegisterCreditsManageApp.Windows.Server.Pages.SubRegisterPage;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace RegisterCreditsManageApp.Windows.Server.Pages
{
    /// <summary>
    /// Interaction logic for RegisterClassesRoomRegisteredWindow.xaml
    /// </summary>
    public partial class RegisterClassesRoomRegisteredWindow : Window
    {
        int idSemester;
        int idMajors;
        int idMainClass;
        private List<Data> dataGrid = new List<Data>();
        public RegisterClassesRoomRegisteredWindow(int idSemester, int idMajors, int idMainClass)
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

            // Get SubjectNeedEditList 
            var subjectNeedEditList = (from subject in subjectList
                                       from classRoom in classRoomList
                                       where subject.IdSubject == classRoom.IdSubject
                                       select new
                                       {
                                           IdClassRoom = classRoom.IdClassRoom,
                                           SubjectName = subject.Name,
                                           NumberOfCredits = subject.NumberOfCredits
                                       }
                                       ).ToList();

            foreach (var subject in subjectNeedEditList)
            {
                Data data = new Data
                {
                    IdClassRoom = subject.IdClassRoom,
                    SubjectName = subject.SubjectName,
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

        private void RadioBtnEditClassRoom_Click(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (sender as RadioButton)!;
            var parent = (radioButton.Parent as Panel)!;
            var idClassRoomTextBlock = (parent.Children[2] as TextBlock)!;

            string idClassRoom = idClassRoomTextBlock.Text;
            new RegisterEditClassRoomWindow(idClassRoom).ShowDialog();
            GetDataGrid(idSemester, idMajors, idMainClass);

            e.Handled = true;
        }

        private void RadioBtnDeleteClassRoom_Click(object sender, RoutedEventArgs e)
        {
            var alertResult = AlertBox.Show($"Bạn có muốn xóa lớp học phần này không?)", "Cảnh báo", AlertButton.YesNo, AlertIcon.Warning);
            if (alertResult == AlertResult.Yes)
            {
                RadioButton radioButton = (sender as RadioButton)!;
                var parent = (radioButton.Parent as Panel)!;
                var idClassRoomTextBlock = (parent.Children[2] as TextBlock)!;
                string idClassRoom = idClassRoomTextBlock.Text;

                var classRoom = AppDbContext._Context.ClassRooms.FirstOrDefault(classRoom => classRoom.IdClassRoom == idClassRoom);
                AppDbContext._Context.ClassRooms.Remove(classRoom);
                AppDbContext._Context.SaveChanges();
                AlertBox.Show($"Đã xóa lớp học phần thành công", "Thành công", AlertButton.OK, AlertIcon.Success);
                GetDataGrid(idSemester, idMajors, idMainClass);

                e.Handled = true;
            }
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public class Data
        {
            public string IdClassRoom { get; set; }
            public string SubjectName { get; set; }
            public int NumberOfCredits { get; set; }
            public string MainClassName { get; set; }
        }
    }
}
