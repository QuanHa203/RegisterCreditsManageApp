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
using System.Windows.Shapes;
using static RegisterCreditsManageApp.Windows.Server.Pages.StudyProgramPage;

namespace RegisterCreditsManageApp.Windows.Server.Pages.SubWindow
{
    /// <summary>
    /// Interaction logic for AddSubject.xaml
    /// </summary>
    public partial class AddSubject : Window
    {
        private int? idSemester; 
        public AddSubject()
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
        private void CancelAddingSubjectBtn_Click(object sender, RoutedEventArgs e)
        {
            AlertResult result = AlertBox.Show("Bạn có chắc muốn hủy thêm môn học không?", "Thông báo", AlertButton.YesNo, AlertIcon.Question);
            if (result == AlertResult.Yes)
            {
                this.Close();
            }
        }

        private void SemesterToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if(SemesterToggleButton.IsChecked == true)
            {
                SemesterPopup.IsOpen = true;
            }
            else
            {
                SemesterPopup.IsOpen = false;
            }
        }

        private void AddSubjectBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
