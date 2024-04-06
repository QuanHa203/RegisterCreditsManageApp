using RegisterCreditsManageApp.Resources;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

        protected override void ThemeModeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            base.ThemeModeCheckBox_Checked(sender, e);
        }

        protected override void ThemeModeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            base.ThemeModeCheckBox_Unchecked(sender, e);
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void AnalysisBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ManagementBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AccountBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
