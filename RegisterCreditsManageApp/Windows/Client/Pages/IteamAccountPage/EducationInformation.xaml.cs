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

namespace RegisterCreditsManageApp.Windows.Client.Pages.IteamAccountPage
{
    /// <summary>
    /// Interaction logic for EducationInformation.xaml
    /// </summary>
    public partial class EducationInformation : Page
    {
        Student student = LoginWindow.CurrentStudent;
        public EducationInformation()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            textBoxIdStudent.Text=student.IdStudent;
            textBoxNameMajors.Text=student.IdMajorsNavigation.Name;
            textBoxClass.Text = student.IdMainClassNavigation.Name;
            textBoxCourse.Text = student.IdMainClassNavigation.CourseYear.ToString();
            textBoxSemester.Text = student.IdMainClassNavigation.IdCurrentRegisterSemesterNavigation.Name;
            int sumNumberOfCredits = 0;

           List<ClassRoom> classRoomsList = AppDbContext._Context.ClassRooms.Include(classrom => classrom.IdSubjectNavigation).Where(classroom => classroom.IdMainClass == student.IdMainClass).ToList();
            

            foreach(var ClassRoom in classRoomsList)
            {
                sumNumberOfCredits += ClassRoom.IdSubjectNavigation.NumberOfCredits;
            }
               
            textBoxNumberOfCredits.Text = sumNumberOfCredits.ToString();

        }
    }
}
