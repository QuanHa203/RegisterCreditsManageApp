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
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            classRoom = AppDbContext._Context.ClassRooms.FirstOrDefault(classRoom => classRoom.IdClassRoom == idClassRoom);
            TextBoxIdClassRoom.Text = classRoom.IdClassRoom;
            TextBoxClassRoomName.Text = classRoom.Name;
            TextBoxNumberOfCapacity.Text = classRoom.Capacity.ToString();
            TextBoxNumberOfCurrent.Text = classRoom.CurrentStudent.ToString();
            TextBoxSchedule.Text = classRoom.Schedule.ToString();
            TextBoxStartRegisterDate.Text = classRoom.StartRegisterDate.ToString();
            TextBoxEndRegisterDate.Text = classRoom.EndRegisterDate.ToString();
            if (classRoom.Status)
                RadioButtonStatusOpen.IsChecked = true;
            else
                RadioButtonStatusClose.IsChecked = true;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            string numberOfCapacity = TextBoxNumberOfCapacity.Text;
            string startRegisterDate = TextBoxStartRegisterDate.Text;
            string endRegisterDate = TextBoxEndRegisterDate.Text;
            string schedule = TextBoxSchedule.Text;
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
                    Status = RadioButtonStatusOpen.IsChecked.Value,
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

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBoxStartRegisterDate_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxStartRegisterDate.Text.Length > 0)
                TextBoxStartRegisterDatePlaceHolder.Visibility = Visibility.Hidden;
            else
                TextBoxStartRegisterDatePlaceHolder.Visibility = Visibility.Visible;
        }

        private void TextBoxEndRegisterDate_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxEndRegisterDate.Text.Length > 0)
                TextBoxEndRegisterDatePlaceHolder.Visibility = Visibility.Hidden;
            else
                TextBoxEndRegisterDatePlaceHolder.Visibility = Visibility.Visible;
        }
    }
}
