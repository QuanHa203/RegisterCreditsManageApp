using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RegisterCreditsManageApp.Models;
using RegisterCreditsManageApp.Windows.Alert;
using RegisterCreditsManageApp.Windows.Server.Pages.SubStudyProgramPage;
using RegisterCreditsManageApp.Windows.Server.Pages.SubWindow;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace RegisterCreditsManageApp.Windows.Server.Pages
{
    /// <summary>
    /// Interaction logic for ManagementPage.xaml
    /// </summary>
    public partial class StudyProgramPage : Page
    {
        public StudyProgramPage()
        {
            InitializeComponent();
            LoadMajorData();
        }
        public void LoadMajorData()
        {
            List<Major> majorList = AppDbContext._Context.Majors.Include((major) => major.Subjects).ToList();
            var list = new List<MajorData>();
            foreach (Major major in majorList)
            {
                MajorData majorData = new MajorData()
                {
                    idMajor = major.IdMajors,
                    majorName = major.Name,
                    subjectNum = major.Subjects.Count,
                };
                list.Add(majorData);
            }
            MajorDataGrid.ItemsSource = list;
        }
        public class MajorData
        {
            public int idMajor { get; set; }
            public string majorName {  get; set; }
            public int subjectNum {  get; set; }
        }
    
        private void NavigateToAddMajor_Click(object sender, RoutedEventArgs e)
        {
            new AddMajor().ShowDialog();
        }

        private void OperationBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            StackPanel st = btn.Parent as StackPanel;
            Popup popup =  st.Children[0] as Popup;
            popup.IsOpen = true;
        }

        private void ModifyMajorBtn_Click(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            StackPanel st = radioButton.Parent as StackPanel;
            TextBlock tb = st.Children[3] as TextBlock;

            int idMajor = int.Parse(tb.Text);
            new ModifyMajor(idMajor).ShowDialog();
            LoadMajorData();
        }

        private void DeleteMajorBtn_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            StackPanel st = radioButton.Parent as StackPanel;
            TextBlock tb = st.Children[3] as TextBlock;

            int idMajor = int.Parse(tb.Text);

            AlertResult result = AlertBox.Show("Bạn có chắc muốn xóa ngành học (bao gồm các môn học) này không?", "Thông báo", AlertButton.YesNo, AlertIcon.Question);
            if (result == AlertResult.Yes)
            {
                Major major = AppDbContext._Context.Majors.FirstOrDefault((Major major) => major.IdMajors == idMajor);
                AppDbContext._Context.Remove(major);
                AppDbContext._Context.SaveChanges();
                AppDbContext._Context.Majors.Entry(major).State = EntityState.Detached;
                LoadMajorData();
            }

            e.Handled = true;
        }

        private void NavigateToSubject_Click(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            StackPanel st = radioButton.Parent as StackPanel;
            TextBlock tb = st.Children[3] as TextBlock;

            new SubjectsWindow(int.Parse(tb.Text)).ShowDialog();
            LoadMajorData();
        }

        private void searchTextBox_Click(object sender, RoutedEventArgs e)
        {
            string searchInput = searchTextBox._Text;
            if (searchInput.IsNullOrEmpty())
            {
                AlertBox.Show("Vui lòng nhập đầy đủ văn bản", "Thông báo", AlertButton.OK, AlertIcon.Warning);
                return;
            }

            var majorList = AppDbContext._Context.Majors.Include((major) => major.Subjects).Where(major => major.Name.Contains(searchInput)).ToList();
            if(majorList.Count == 0)
            {
                AlertBox.Show($"Không có tên ngành học '{searchInput}' cần tìm", "Thông báo", AlertButton.OK, AlertIcon.Information);
                return;
            }

            List<MajorData> list = new List<MajorData>();
            foreach(var major in majorList)
            {
                MajorData data = new MajorData
                {
                    idMajor = major.IdMajors,
                    majorName = major.Name,
                    subjectNum = major.Subjects.Count,
                };
                list.Add(data);
            }

            MajorDataGrid.ItemsSource = list;
        }
    }
}