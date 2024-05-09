using Microsoft.EntityFrameworkCore;
using RegisterCreditsManageApp.Models;
using RegisterCreditsManageApp.Windows.Alert;
using RegisterCreditsManageApp.Windows.Server.Pages.SubMainClassPage;
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

namespace RegisterCreditsManageApp.Windows.Server.Pages
{
    /// <summary>
    /// Interaction logic for MainClassPage.xaml
    /// </summary>
    public partial class MainClassPage : Page
    {
        public MainClassPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataGrid();
        }        

        private void LoadDataGrid()
        {
            DataGridMainClass.ItemsSource = null;
            List<MainClass> mainClassList = AppDbContext._Context.MainClasses.Include(mainClass => mainClass.IdMajorsNavigation)
                                                                             .Include(mainClass => mainClass.IdCurrentRegisterSemesterNavigation)
                                                                             .ToList();
            DataGridMainClass.ItemsSource = mainClassList;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchTextBox.Text.Length > 0)
                SearchPlaceHolder.Visibility = Visibility.Hidden;

            else
                SearchPlaceHolder.Visibility = Visibility.Visible;
        }

        private void BtnShowPopup_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            StackPanel sp = btn.Parent as StackPanel;
            Popup popup = sp.Children[0] as Popup;
            popup.IsOpen = true;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            new MainClassAddWindow().ShowDialog();
            LoadDataGrid();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            StackPanel sp = btn.Parent as StackPanel;
            TextBlock textBlockIdMainClass = sp.Children[2] as TextBlock;
            int idMainClass = Convert.ToInt32(textBlockIdMainClass.Text);

            new MainClassEditWindow(idMainClass).ShowDialog();
            LoadDataGrid();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            StackPanel sp = btn.Parent as StackPanel;
            TextBlock textBlockIdMainClass = sp.Children[2] as TextBlock;
            int idMainClass = Convert.ToInt32(textBlockIdMainClass.Text);

            AlertResult alertResult = AlertBox.Show("Bạn có chắc muốn xóa lớp danh nghĩa này không?\n(Nếu xóa thì toàn bộ học sinh trong lớp sẽ bị xóa theo)", "Cảnh báo", AlertButton.YesNo, AlertIcon.Warning);
            if (alertResult == AlertResult.Yes)
            {
                try
                {
                    var mainClass = AppDbContext._Context.MainClasses.FirstOrDefault(mainClass => mainClass.IdMainClass == idMainClass);
                    AppDbContext._Context.MainClasses.Remove(mainClass);
                    AppDbContext._Context.SaveChanges();
                    AlertBox.Show("Đã xóa lớp danh nghĩa thành công!", "Thành công", AlertButton.OK, AlertIcon.Success);
                    LoadDataGrid();
                }
                catch (Exception ex)
                {
                    AlertBox.Show($"Lỗi, xóa lớp danh nghĩa thất bại.\nLỗi: {ex.Message}", "Lỗi", AlertButton.OK, AlertIcon.Error);
                }
            }
        }
    }
}
