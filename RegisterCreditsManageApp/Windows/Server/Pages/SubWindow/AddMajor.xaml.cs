using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace RegisterCreditsManageApp.Windows.Server.Pages.SubWindow
{
    /// <summary>
    /// Interaction logic for AddMajor.xaml
    /// </summary>
    public partial class AddMajor : Window
    {
        public AddMajor()
        {
            InitializeComponent();
            LoadSubjectData();
        }
        public void LoadSubjectData()
        {
             SubjectDataGrid.ItemsSource = new List<SubjectData>()
            {
                new SubjectData {Id = 1, SubjectName = "Tin học văn phòng", CreditNum = 4, Semester = "Học kỳ 1"},
                new SubjectData {Id = 2, SubjectName = "Lập trình hướng đối tượng", CreditNum = 2, Semester = "Học kỳ 2"},
                new SubjectData {Id = 3, SubjectName = "Cấu trúc dữ liệu và giải thuật", CreditNum = 3, Semester = "Học kỳ 3"},
                new SubjectData {Id = 4, SubjectName = "Mạng máy tính", CreditNum = 4, Semester = "Học kỳ 6"},
                new SubjectData {Id = 5, SubjectName = "Lập trình DOTNET", CreditNum = 2, Semester = "Học kỳ 8"},
                new SubjectData {Id = 6, SubjectName = "Quản trị mạng", CreditNum = 3, Semester = "Học kỳ 5"},
            };
        }

        public class SubjectData
        {
            public int Id { get; set; }
            public string SubjectName { get; set; }
            public int CreditNum { get; set; }
            public string Semester { get; set; }
        }

        private void CancelAddingMajorBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddMajorBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddSubjectButton_Click(object sender, RoutedEventArgs e)
        {
            new AddSubject().ShowDialog();
        }
    }
}
