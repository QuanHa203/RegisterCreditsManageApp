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
        public HomePage()
        {

            InitializeComponent();
            List<Data> data = new List<Data>()
            {
                new Data(){Id=1,Subject="AAA",NumberOfCredits=1},
                new Data(){Id=2,Subject="AAA",NumberOfCredits=1},
                new Data(){Id=3,Subject="AAA",NumberOfCredits=1},
                new Data(){Id=4,Subject="AAA",NumberOfCredits=1}
            };
            Style btnStyle = this.Resources["btnpopup"] as Style;

            DataGridSubject.ItemsSource = data;
            Student student = AppDbContext._Context.Students.Include(student => student.IdMainClassNavigation).FirstOrDefault(student => student.IdStudent == "2");

            for (int i = 1; i <= student.IdMainClassNavigation.IdCurrentRegisterSemester; i++)
            {
                Semester semester = AppDbContext._Context.Semesters.FirstOrDefault(semester => semester.IdSemester == i);
                Button btn = new Button();
                btn.Content = semester.Name;
                btn.Style = btnStyle;
                btn.Click += (object sender, RoutedEventArgs e) =>
                {
                    idsemester = semester.IdSemester;
                };
                StackPanelPopup.Children.Add(btn);

            }
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
