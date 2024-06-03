using RegisterCreditsManageApp.Windows.Alert;
using System.Windows;

namespace RegisterCreditsManageApp.Windows.Client
{
    /// <summary>
    /// Interaction logic for ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : RegisterCreditsManageApp.Windows.MainWindow
    {
        public ClientWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            HomeRadioButton_Click(sender, e);
            HomeRadioButton.IsChecked = true;
        }

        private void HomeRadioButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Windows/Client/Pages/HomePage.xaml", UriKind.Relative));
        }

        private void StudyProgramRadioButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Windows/Client/Pages/StudyProgramPage.xaml", UriKind.Relative));
        }

        private void RegisterRadioButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Windows/Client/Pages/RegisterPage.xaml", UriKind.Relative));
        }

        private void AccountRadioButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Windows/Client/Pages/AccountPage.xaml", UriKind.Relative));            
        }

        private void NotifyBtn_Click(object sender, RoutedEventArgs e)
        {
            AlertBox.Show("Chức năng này đang trong quá trình phát triển", "Thông báo", AlertButton.OK, AlertIcon.Information);
        }

        private void ExitRadioButton_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var alertResult = AlertBox.Show("Bạn có chắc chắn muốn thoát không?", "", AlertButton.YesNo, AlertIcon.Question);
            if (alertResult == AlertResult.Yes)
                Application.Current.Shutdown();

            // Ngăn không cho RadioButton thay đổi trạng thái khi được nhấp vào
            e.Handled = true;
        }
    }
}