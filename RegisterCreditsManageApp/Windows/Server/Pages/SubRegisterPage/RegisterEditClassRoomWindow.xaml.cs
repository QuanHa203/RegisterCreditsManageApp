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

namespace RegisterCreditsManageApp.Windows.Server.Pages.SubRegisterPage
{
    /// <summary>
    /// Interaction logic for RegisterEditClassRoomWindow.xaml
    /// </summary>
    public partial class RegisterEditClassRoomWindow : Window
    {
        private string idClassRoom;
        private ClassRoom classRoom;
        public RegisterEditClassRoomWindow(string idClassRoom)
        {
            this.idClassRoom = idClassRoom;
            classRoom = AppDbContext._Context.ClassRooms.FirstOrDefault(classRoom => classRoom.IdClassRoom == idClassRoom)!;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            textBoxIdClassRoom.Text = classRoom.IdClassRoom;
            textBoxClassRoomName.Text = classRoom.Name;
            textBoxNumberOfCapacity.Text = classRoom.Capacity.ToString();
            textBoxNumberOfCurrent.Text = classRoom.CurrentStudent.ToString();
            textBoxSchedule.Text = classRoom.Schedule.ToString();
            textBoxStartRegisterDate._Text = classRoom.StartRegisterDate.ToString();
            textBoxEndRegisterDate._Text = classRoom.EndRegisterDate.ToString();
            if (classRoom.Status)
                radioButtonStatusOpen.IsChecked = true;
            else
                radioButtonStatusClose.IsChecked = true;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            string numberOfCapacity = textBoxNumberOfCapacity.Text;
            string startRegisterDate = textBoxStartRegisterDate._Text;
            string endRegisterDate = textBoxEndRegisterDate._Text;
            string schedule = textBoxSchedule.Text;
            if (numberOfCapacity.Length == 0 || startRegisterDate.Length == 0 || endRegisterDate.Length == 0 || schedule.Length == 0)
            {
                AlertBox.Show("Vui lòng nhập đầy đủ dữ liệu", "Cảnh báo", AlertButton.OK, AlertIcon.Warning);
                return;
            }

            try
            {
                DateOnly startRegister = DateOnly.ParseExact(startRegisterDate, "dd/MM/yyyy");
                DateOnly endRegister = DateOnly.ParseExact(endRegisterDate, "dd/MM/yyyy");
                ClassRoom addClassRoom = new ClassRoom
                {
                    IdClassRoom = classRoom.IdClassRoom,
                    IdMainClass = classRoom.IdMainClass,
                    IdSubject = classRoom.IdSubject,
                    IdSemester = classRoom.IdSemester,
                    Name = classRoom.Name,
                    Schedule = schedule,
                    Status = radioButtonStatusOpen.IsChecked!.Value,
                    Capacity = Convert.ToInt32(numberOfCapacity),
                    CurrentStudent = classRoom.CurrentStudent,
                    StartRegisterDate = startRegister,
                    EndRegisterDate = endRegister,
                };
                AppDbContext._Context.Entry(classRoom).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                AppDbContext._Context.Update(addClassRoom);
                AppDbContext._Context.SaveChanges();

                AlertBox.Show($"Đã chỉnh sửa lớp học phần thành công", "Thành công", AlertButton.OK, AlertIcon.Success);
                this.Close();
            }

            catch (Exception ex)
            {
                AlertBox.Show($"{ex.Message}", "Lỗi", AlertButton.OK, AlertIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
