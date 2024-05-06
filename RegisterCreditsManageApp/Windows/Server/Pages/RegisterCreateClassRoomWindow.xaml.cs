using Microsoft.EntityFrameworkCore;
using RegisterCreditsManageApp.Models;
using RegisterCreditsManageApp.Windows.Alert;
using System.Windows;
using System.Windows.Controls;

namespace RegisterCreditsManageApp.Windows.Server.Pages
{
    /// <summary>
    /// Interaction logic for RegisterCreateClassRoomWindow.xaml
    /// </summary>
    public partial class RegisterCreateClassRoomWindow : Window
    {
        MainClass mainClass;
        int idSubject;
        public RegisterCreateClassRoomWindow(int idSubject, int idMainClass)
        {
            InitializeComponent();
            this.idSubject = idSubject;
            mainClass = AppDbContext._Context.MainClasses.Include(mainClass => mainClass.Students).FirstOrDefault(mainClass => mainClass.IdMainClass == idMainClass);

            TextBoxClassRoomName.Text = mainClass.Name;
            TextBoxNumberOfCurrent.Text = mainClass.Students.Count.ToString();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            string idClassRoom = TextBoxIdClassRoom.Text;
            string numberOfCapacity = TextBoxNumberOfCapacity.Text;
            string startRegisterDate = TextBoxStartRegisterDate.Text;
            string endRegisterDate = TextBoxEndRegisterDate.Text;
            string schedule = TextBoxSchedule.Text;
            if (idClassRoom.Length == 0 || numberOfCapacity.Length == 0 || startRegisterDate.Length == 0 || endRegisterDate.Length == 0 || schedule.Length == 0)
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
                    IdClassRoom = idClassRoom,
                    IdMainClass = mainClass.IdMainClass,
                    IdSubject = idSubject,
                    Name = mainClass.Name,
                    Schedule = schedule,
                    Status = RadioButtonStatusOpen.IsChecked.HasValue,
                    Capacity = Convert.ToInt32(numberOfCapacity),
                    CurrentStudent = mainClass.Students.Count,
                    StartRegisterDate = startRegister,
                    EndRegisterDate = endRegister,
                };
                AppDbContext._Context.Add(addClassRoom);
                AppDbContext._Context.SaveChanges();

                List<RegisterCredit> registerCreditList = new List<RegisterCredit>();
                foreach (var student in mainClass.Students)
                {
                    RegisterCredit registerCredit = new RegisterCredit
                    {
                        IdStudent = student.IdStudent,
                        IdClassRoom = idClassRoom,
                        IdSubject = idSubject,
                    };
                    registerCreditList.Add(registerCredit);
                }
                    AppDbContext._Context.AddRange(registerCreditList);                    
                    AppDbContext._Context.SaveChanges();
                AlertBox.Show($"Đã tạo lớp học phần thành công", "Thành công", AlertButton.OK, AlertIcon.Success);
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
