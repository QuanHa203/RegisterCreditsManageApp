using RegisterCreditsManageApp.Models;
using System.Windows;
using System.Windows.Controls;

namespace RegisterCreditsManageApp.Windows.Server.Pages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
            List<Data> data = new List<Data>()
            {
                new Data(){Id=1,Subject="AAA",NumberOfCredits=1},
                new Data(){Id=2,Subject="AAA",NumberOfCredits=1},
                new Data(){Id=3,Subject="AAA",NumberOfCredits=1},
                new Data(){Id=4,Subject="AAA",NumberOfCredits=1}
            };
            DataGridSubject.ItemsSource =  data;
        }


        public class Data
        {
            public int Id { get; set; }
            public string Subject { get; set; }
            public int NumberOfCredits { get; set; }
            
        }

        private void btnShowPopup_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = true;
            
        }
    }
}
