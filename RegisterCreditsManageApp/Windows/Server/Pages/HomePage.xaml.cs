using RegisterCreditsManageApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using RegisterCreditsManageApp.Windows.Alert;
using RegisterCreditsManageApp.Windows.Client;

namespace RegisterCreditsManageApp.Windows.Server.Pages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
            var listSermester = AppDbContext._Context.Semesters.ToList();
            foreach(var ser in listSermester)
            {
                MessageBox.Show($"{ser.IdSemester} - {ser.SemesterName}  -  {ser.IdMajors}");
            }

            var listMajors = AppDbContext._Context.Majors.ToList();
            foreach (var majors in listMajors)
            {
                MessageBox.Show($"{majors.IdMajors} - {majors.MajorsName}  -  {majors.IdSemesters}");
            }
        }
    }
}
