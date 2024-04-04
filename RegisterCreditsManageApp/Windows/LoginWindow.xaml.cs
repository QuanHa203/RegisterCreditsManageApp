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
using System.Windows.Shapes;

namespace RegisterCreditsManageApp.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void AccountTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textValue = (TextBox)sender;
            if (textValue.Text == "Your account")
            {
                textValue.Text = "";
                textValue.Foreground = Brushes.White;
            }
        }
        private void AccountTextbox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textValue = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textValue.Text))
            {
                textValue.Text = "Your account";
                textValue.Foreground = Brushes.Gray;
            }
        }

        private void PasswordTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textValue = (TextBox)sender;
            if (textValue.Text == "Password")
            {
                textValue.Text = "";
                textValue.Foreground = Brushes.White;
            }
        }

        private void PasswordTextbox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textValue = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textValue.Text))
            {
                textValue.Text = "Password";
                textValue.Foreground = Brushes.Gray;
            }
        }

    }
}
