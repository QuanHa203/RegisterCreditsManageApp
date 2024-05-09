using RegisterCreditsManageApp.Windows.Client.Pages.IteamAccountPage;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RegisterCreditsManageApp.Windows.Client.Pages
{
    /// <summary>
    /// Interaction logic for AccountPage.xaml
    /// </summary>
    public partial class AccountPage : Page
    {
        public AccountPage()
        {
            InitializeComponent(); 

            rdbStudentInformation.IsChecked = true;
            AccFrame.NavigationService.Navigate(new Uri("Windows/Client/Pages/IteamAccountPage/StudentInformation.xaml", UriKind.Relative));
        }

        private void rdbStudentInformation_Checked(object sender, RoutedEventArgs e)
        {
            AccFrame.NavigationService.Navigate(new Uri("Windows/Client/Pages/IteamAccountPage/StudentInformation.xaml", UriKind.Relative));

        }

        private void rdbEducationInformation_Checked(object sender, RoutedEventArgs e)
        {
            AccFrame.NavigationService.Navigate(new Uri("Windows/Client/Pages/IteamAccountPage/EducationInformation.xaml", UriKind.Relative));
        }

        private void rdbChangePassword_Checked(object sender, RoutedEventArgs e)
        {
            AccFrame.NavigationService.Navigate(new Uri("Windows/Client/Pages/IteamAccountPage/ChangePassword.xaml", UriKind.Relative));
        }
    }
}
