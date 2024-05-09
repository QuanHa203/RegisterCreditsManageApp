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

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Windows/Client/Pages/HomePage.xaml", UriKind.Relative));
        }

        private void StudyProgramBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Windows/Client/Pages/StudyProgramPage.xaml", UriKind.Relative));
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Windows/Client/Pages/RegisterPage.xaml", UriKind.Relative));
        }

        private void AccountBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Windows/Client/Pages/AccountPage.xaml", UriKind.Relative));            
        }

        private void NotifyBtn_Click(object sender, RoutedEventArgs e)
        {
            AlertBox.Show("Chức năng này đang trong quá trình phát triển", "Thông báo", AlertButton.OK, AlertIcon.Information);
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            var alertResult = AlertBox.Show("Bạn có chắc chắn muốn thoát không?", "", AlertButton.YesNo, AlertIcon.Question);
            if (alertResult == AlertResult.Yes)                
                Application.Current.Shutdown();
        }

    }
}
