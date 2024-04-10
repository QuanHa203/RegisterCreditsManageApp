using RegisterCreditsManageApp.Windows.Client.Pages;
using System.Windows;
using System.Windows.Controls;


namespace RegisterCreditsManageApp.Windows.Server
{
    /// <summary>
    /// Interaction logic for ServerWindow.xaml
    /// </summary>
    public partial class ServerWindow : MainWindow
    {
        public ServerWindow() : base()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Windows/Server/Pages/HomePage.xaml", UriKind.Relative));
        }

        private void ManagementBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Windows/Server/Pages/ManagementPage.xaml", UriKind.Relative));
        }

        private void AnalysisBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Windows/Server/Pages/AnalysisPage.xaml", UriKind.Relative));
        }

        private void AccountBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Windows/Server/Pages/AccountPage.xaml", UriKind.Relative));
        }

        private void NotifyBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
