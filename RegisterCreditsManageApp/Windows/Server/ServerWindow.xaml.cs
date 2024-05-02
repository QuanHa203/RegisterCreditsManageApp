using Microsoft.Data.SqlClient;
using RegisterCreditsManageApp.Windows.Alert;
using System.Windows;


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
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {            
            MainFrame.NavigationService.Navigate(new Uri("Windows/Server/Pages/HomePage.xaml", UriKind.Relative));
        }

        private void StudyProgramBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Windows/Server/Pages/StudyProgramPage.xaml", UriKind.Relative));
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Windows/Server/Pages/RegisterPage.xaml", UriKind.Relative));
        }

        private void AccountBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Windows/Server/Pages/AccountPage.xaml", UriKind.Relative));
        }

        private void NotifyBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Notify is click");
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }        
    }
}
