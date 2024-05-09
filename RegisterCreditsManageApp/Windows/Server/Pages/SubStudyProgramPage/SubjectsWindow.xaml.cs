using Microsoft.EntityFrameworkCore;
using RegisterCreditsManageApp.Models;
using RegisterCreditsManageApp.Windows.Alert;
using RegisterCreditsManageApp.Windows.Server.Pages.SubWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RegisterCreditsManageApp.Windows.Server.Pages.SubStudyProgramPage
{
    /// <summary>
    /// Interaction logic for SubjectsWindow.xaml
    /// </summary>
    public partial class SubjectsWindow : Window
    {
        private int idMajor;
        private int idSubject;
        public SubjectsWindow(int idMajor)
        {
            this.idMajor = idMajor;
            InitializeComponent();
            var major = AppDbContext._Context.Majors.FirstOrDefault((Major id) => id.IdMajors ==  idMajor);
            MajorTitle.Content = $"Môn học ngành {major.Name}";

            LoadSubjectData(idMajor);
        }
        public void LoadSubjectData(int idMajor)
        {
            var subjectsList = AppDbContext._Context.Subjects.Where((Subject subject) => subject.IdMajors == idMajor)
                                                             .Include((subject) => subject.IdSemesterNavigation).ToList();
            var list = new List<SubjectData>();
            foreach (var subject in subjectsList)
            {
                SubjectData data = new SubjectData()
                {
                    idSubject = subject.IdSubject,
                    subjectName = subject.Name,
                    creditNum = subject.NumberOfCredits,
                    semesterTitle = subject.IdSemesterNavigation.Name,
                };
                list.Add(data);
            }
            SubjectDataGrid.ItemsSource = list;
        }

        public class SubjectData
        {
            public int idSubject {  get; set; }
            public int idSemester {  get; set; }
            public string semesterTitle {  get; set; }
            public int idMajor {  get; set; }
            public string subjectName {  get; set; }
            public int creditNum {  get; set; }
        }

        private void ExitSubjectsWindow_Click(object sender, RoutedEventArgs e)
        {
            AlertResult result = AlertBox.Show("Bạn có chắc muốn thoát xem môn học không?", "Thông báo", AlertButton.YesNo, AlertIcon.Question);
            if (result == AlertResult.Yes)
            {
                this.Close();
            }
        }

        private void AddSubjectButton_Click(object sender, RoutedEventArgs e)
        {
            new AddSubject(idMajor).ShowDialog();
            LoadSubjectData(idMajor);
        }

        private void ModifySubjectBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            StackPanel st = btn.Parent as StackPanel;
            TextBlock tb = st.Children[2] as TextBlock;

            int idSubject = int.Parse(tb.Text);
            new ModifySubject(idSubject, idMajor).ShowDialog();
            LoadSubjectData(idMajor);
        }

        private void DeleteSubjectBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            StackPanel st = btn.Parent as StackPanel;
            TextBlock tb = st.Children[2] as TextBlock;

            int idSubject = int.Parse(tb.Text);

            AlertResult result = AlertBox.Show("Bạn có chắc muốn môn học này không?", "Thông báo", AlertButton.YesNo, AlertIcon.Question);
            if (result == AlertResult.Yes)
            {
                Subject subject = AppDbContext._Context.Subjects.FirstOrDefault((Subject s) => s.IdSubject == idSubject);
                AppDbContext._Context.Remove(subject);
                AppDbContext._Context.SaveChanges();
                LoadSubjectData(idMajor);
            }
        }

        private void OperationBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            StackPanel st = btn.Parent as StackPanel;
            Popup popup = st.Children[0] as Popup;
            popup.IsOpen = true;
        }
    }
}
