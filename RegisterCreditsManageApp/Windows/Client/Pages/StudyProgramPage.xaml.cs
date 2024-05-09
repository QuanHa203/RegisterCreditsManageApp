using Microsoft.EntityFrameworkCore;
using RegisterCreditsManageApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RegisterCreditsManageApp.Windows.Client.Pages
{
    /// <summary>
    /// Interaction logic for StudyProgramPage.xaml
    /// </summary>
    public partial class StudyProgramPage : Page
    {
        private int idCurrentSemester;
        private int idMajor;
        public StudyProgramPage()
        {
            InitializeComponent();
            DisplaySemester();

        }
        public void LoadData()
        {
            var classRoomList = AppDbContext._Context.ClassRooms.Include((ClassRoom c) => c.IdSubjectNavigation)
                .Where((ClassRoom c) => c.IdSemester == idCurrentSemester)
                .Where((c) => c.IdMainClass == LoginWindow.CurrentStudent.IdMainClass).ToList();
            var list = new List<Data>();
            for (int i = 0; i<classRoomList.Count; i++)
            {
                Data data = new Data()
                {
                    stt = i+1,
                    subjectName = classRoomList[i].IdSubjectNavigation.Name,
                    idClassroom = classRoomList[i].IdClassRoom,
                    creditNum = classRoomList[i].IdSubjectNavigation.NumberOfCredits,
                };
                list.Add(data);
            }
            SubjectProgramDataGrid.ItemsSource = list;
        }
        public class Data
        {
            public int stt { set; get; }
            public string idClassroom {  set; get; }
            public int creditNum {  set; get; }
            public string subjectName {  get; set; }
        }
        public void DisplaySemester()
        {
            idCurrentSemester = LoginWindow.CurrentStudent.IdMainClassNavigation.IdCurrentRegisterSemester;

            var btnStyle = this.Resources["SemesterButton"] as Style;
            var CurrentSemesterList = AppDbContext._Context.Semesters.Where((Semester s) => s.IdSemester == idCurrentSemester).ToList();
            foreach (var semester in CurrentSemesterList)
            {
                Button btn = new Button()
                {
                    Style = btnStyle,
                    Content = semester.Name,
                };
                btn.Click += (s, e) =>
                {
                    semester.IdSemester = idCurrentSemester;
                    SemesterTitle.Text = semester.Name;
                    SemesterPopup.IsOpen = false;
                    LoadData();
                };
                SemesterListPopup.Children.Add(btn);
            }
        }
        private void SemesterToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (SemesterToggleButton.IsChecked == true)
            {
                SemesterPopup.IsOpen = true;
            }
            else
            {
                SemesterPopup.IsOpen = false;
            }
        }
    }
}
