using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using RegisterCreditsManageApp.Models;
using RegisterCreditsManageApp.Windows.Alert;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RegisterCreditsManageApp.Windows.Server.Pages.SubStudentPage
{
    /// <summary>
    /// Interaction logic for StudentCurrentRegisterCreditsWindow.xaml
    /// </summary>
    public partial class StudentCurrentRegisterCreditsWindow : Window
    {
        
        private string idS;
        public StudentCurrentRegisterCreditsWindow(string idStudent)
        {
            InitializeComponent();
            idS = idStudent;
        }

        private void rdbCreditsRegistered_Checked(object sender, RoutedEventArgs e)
        {
            GetDataGridCreditsRegistered();
        }

        private void rdbUnregisteredCredits_Checked(object sender, RoutedEventArgs e)
        {
            GetDataGridUnregisteredCredits();
        }

        private void BtnShowPopup_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            var parent = btn.Parent as Panel;
            var popup = parent.Children[0] as Popup;
            var stackPanelInPopup = ((popup.Child as Border)!.Child as StackPanel)!;
            RadioButton rbInPopup = (stackPanelInPopup.Children[0] as RadioButton)!;

            if (rdbCreditsRegistered.IsChecked!.Value)
                rbInPopup.Content = "Hủy đăng ký ";
            else
                rbInPopup.Content = "Đăng ký";
            popup.IsOpen = true;
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            rdbCreditsRegistered.IsChecked = true;
        }

        private void GetDataGridCreditsRegistered()
        {
            List<RegisterCredit> listCreditsRegistered = AppDbContext._Context.RegisterCredits.Where( re => re.IdStudent == idS ).Where(re => re.IsRegister == true ).ToList();
            DataGridRegister.ItemsSource = listCreditsRegistered;
        }

        private void GetDataGridUnregisteredCredits()
        {
            List<RegisterCredit> listCreditsRegistered = AppDbContext._Context.RegisterCredits.Where(re => re.IdStudent == idS).Where(re => re.IsRegister == false).ToList();
            DataGridRegister.ItemsSource = listCreditsRegistered;
        }

        private void RadioButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            var parent = radioButton.Parent as Panel;
            var idRegisterCreditsTextBlock = parent.Children[1] as TextBlock;
            int idRegisterCredits;
            int.TryParse(idRegisterCreditsTextBlock.Text, out idRegisterCredits);

            if (rdbCreditsRegistered.IsChecked == true)
            {
                var re =AppDbContext._Context.RegisterCredits.FirstOrDefault(re => re.IdRegisterCredits == idRegisterCredits);
                AlertResult alertResult = AlertBox.Show("Bạn có chắc chắn muốn hủy học phần không ?", "Thông báo", AlertButton.YesNo, AlertIcon.Question);

                if (alertResult == AlertResult.Yes)
                {
                    re.IsRegister = false;
                    AppDbContext._Context.SaveChanges();
                    AlertBox.Show("Hủy thành công", "Thông báo", AlertButton.OK, AlertIcon.Success);
                    GetDataGridCreditsRegistered();

                }
                else return;
            }
            else
            {
                var re = AppDbContext._Context.RegisterCredits.FirstOrDefault(re => re.IdRegisterCredits == idRegisterCredits);
                AlertResult alertResult = AlertBox.Show("Bạn có muốn đăng ký học phần không ?", "Thông báo", AlertButton.YesNo, AlertIcon.Question);

                if (alertResult == AlertResult.Yes)
                {
                    re.IsRegister = true;
                    AppDbContext._Context.SaveChanges();
                    AlertBox.Show("Đăng ký thành công thành công", "Thông báo", AlertButton.OK, AlertIcon.Success);
                    GetDataGridUnregisteredCredits();
                }
                else return;
            }
        }
    }
}
