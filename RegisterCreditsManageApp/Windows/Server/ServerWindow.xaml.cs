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

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            StudyProgramRadioButton_Click(sender, e);
            StudyProgramRadioButton.IsChecked = true;
        }

        private void StudyProgramRadioButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Windows/Server/Pages/StudyProgramPage.xaml", UriKind.Relative));
        }

        private void RegisterRadioButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Windows/Server/Pages/RegisterPage.xaml", UriKind.Relative));
        }

        private void StudentManagerRadioButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Windows/Server/Pages/StudentPage.xaml", UriKind.Relative));
        }

        private void MainClassManagerRadioButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Windows/Server/Pages/MainClassPage.xaml", UriKind.Relative));
        }

        private void AccountRadioButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Windows/Server/Pages/AccountPage.xaml", UriKind.Relative));
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
