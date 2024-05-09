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
        private int idSemester;
        public StudyProgramPage()
        {
            InitializeComponent();
            DisplaySemester();
        }
        public void DisplaySemester()
        {
            var btnStyle = this.Resources["SemesterBtn"] as Style;
            var SemesterList = AppDbContext._Context.Semesters.ToList();
            foreach (var semester in SemesterList)
            {
                Button btn = new Button()
                {
                    Style = btnStyle,
                    Content = semester.Name,
                };
                btn.Click += (s, e) =>
                {
                    idSemester = semester.IdSemester;
                    SemesterTitle.Text = semester.Name;
                    SemesterPopup.IsOpen = false;
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
