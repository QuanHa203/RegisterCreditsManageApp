using Microsoft.EntityFrameworkCore;
using RegisterCreditsManageApp.Models;
using RegisterCreditsManageApp.Windows.Alert;
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
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        int idsemester = 1;
        Student student = AppDbContext._Context.Students.Include(student => student.IdMainClassNavigation).FirstOrDefault(student => student.IdStudent == "2");

        public HomePage()
        {

            InitializeComponent();
 
            Style btnStyle = this.Resources["btnpopup"] as Style;
            RenderDataGrid();
            
            
            for (int i = 1; i <= student.IdMainClassNavigation.IdCurrentRegisterSemester; i++)
            {
                Semester semester = AppDbContext._Context.Semesters.FirstOrDefault(semester => semester.IdSemester == i);
                Button btn = new Button();
                btn.Content = semester.Name;
                btn.Style = btnStyle;
                btn.Click += (object sender, RoutedEventArgs e) =>
                {
                    idsemester = semester.IdSemester;
                    RenderDataGrid();
                };
                StackPanelPopup.Children.Add(btn);

            }
            
        }


        public void RenderDataGrid()
        {
            List<ClassRoom> classRoomList = AppDbContext._Context.ClassRooms.Include(classroom => classroom.IdSubjectNavigation).Where(classroom => classroom.IdSemester == idsemester).Where(classroom => classroom.IdMainClass == student.IdMainClass).ToList();

            DataGridSubject.ItemsSource = classRoomList;
        }

        public class Data
        {
            public int Id { get; set; }
            public string Subject { get; set; }
            public int NumberOfCredits { get; set; }

        }

        private void btnShowPopup_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = true;
        }
    }
}
