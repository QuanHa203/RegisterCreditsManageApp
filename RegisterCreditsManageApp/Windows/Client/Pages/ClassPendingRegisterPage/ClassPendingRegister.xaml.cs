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
using System.Windows.Shapes;

namespace RegisterCreditsManageApp.Windows.Client.Pages.ClassPendingRegisterPage
{
    /// <summary>
    /// Interaction logic for ClassPendingRegister.xaml
    /// </summary>
    public partial class ClassPendingRegister : Window
    {
        private Student currentStudent = LoginWindow.CurrentStudent;
        private int idSubject;
        private string idClassRoom;
        public ClassPendingRegister(int idSubject)
        {
            InitializeComponent();
            this.idSubject = idSubject;

            LoadClassPendingRegister();
        }
        public void LoadClassPendingRegister()
        {
            var classRegisterList = AppDbContext._Context.ClassRooms.Where(r => r.IdSubject == idSubject).ToList();

            var classPendingData = new List<Data>();
            for (int i = 0; i < classRegisterList.Count; i++)
            {
                Data data = new Data
                {
                    NumericalOrder = i + 1,
                    IdClassRoom = classRegisterList[i].IdClassRoom,
                    ClassroomName = classRegisterList[i].Name,
                    SubjectName = classRegisterList[i].IdSubjectNavigation.Name,
                    RegisteredCurrentNumber = classRegisterList[i].CurrentStudent,
                    ClassCapacity = classRegisterList[i].Capacity,
                };
                classPendingData.Add(data);
            }
            DataGridClassPendingRegister.ItemsSource = classPendingData;
        }
        public class Data
        {
            public int NumericalOrder { get; set; }
            public string IdClassRoom { get; set; }
            public string ClassroomName { get; set; }
            public string SubjectName { get; set; }
            public int RegisteredCurrentNumber { get; set; }
            public int ClassCapacity { get; set; }
        }

        private void DataGridRow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DataGridRow dataGridRow = (sender as DataGridRow)!;
            var data = dataGridRow.Item as Data;

            idClassRoom = data.IdClassRoom;
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            var subjectItem = DataGridClassPendingRegister.SelectedItem;
            if (subjectItem == null)
            {
                AlertBox.Show("Vui lòng chọn lớp học phần", "Thông báo", AlertButton.OK, AlertIcon.Information);
            }
            else
            {
                RegisterCredit registerCredit = AppDbContext._Context.RegisterCredits.Where(r => r.IdSubject == idSubject).Where(r => r.IdStudent == currentStudent.IdStudent).FirstOrDefault();
               
                if(registerCredit.IdClassRoomNavigation.CurrentStudent == registerCredit.IdClassRoomNavigation.Capacity)
                {
                    AlertBox.Show("Lớp học phần này đã đủ sĩ số", "Thông báo", AlertButton.OK, AlertIcon.Information);
                }
                else
                {
                    registerCredit.IdClassRoom = idClassRoom;
                    registerCredit.IdClassRoomNavigation.CurrentStudent += 1;
                    registerCredit.IsRegister = true;
                    AppDbContext._Context.Update(registerCredit);
                    AppDbContext._Context.SaveChanges();
                    AlertBox.Show("Đăng ký môn học thành công", "Thông báo", AlertButton.OK, AlertIcon.Success);
                    this.Close();
                }
            }
        }

        private void CancelRegisterBtn_Click_1(object sender, RoutedEventArgs e)
        {
            AlertResult result = AlertBox.Show("Bạn có chắc muốn hủy đăng ký học phần này không?", "Thông báo", AlertButton.YesNo, AlertIcon.Question);
            if (result == AlertResult.Yes)
            {
                this.Close();
            }
        }
    }
}
