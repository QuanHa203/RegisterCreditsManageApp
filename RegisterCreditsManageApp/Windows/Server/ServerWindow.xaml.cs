using MaterialDesignThemes.Wpf;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RegisterCreditsManageApp.Models;
using RegisterCreditsManageApp.Windows.Alert;
using RegisterCreditsManageApp.Windows.Server.Pages;
using System.Net.Security;
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

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            StudyProgramBtn_Click(sender, e);
        }

        private void StudyProgramBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Windows/Server/Pages/StudyProgramPage.xaml", UriKind.Relative));
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Windows/Server/Pages/RegisterPage.xaml", UriKind.Relative));
        }

        private void StudentManagerBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Windows/Server/Pages/StudentPage.xaml", UriKind.Relative));
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
