using RegisterCreditsManageApp.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RegisterCreditsManageApp.Windows.Server
{
    /// <summary>
    /// Interaction logic for ServerWindow.xaml
    /// </summary>
    public partial class ServerWindow : Window
    {
        public ServerWindow()
        {
            InitializeComponent();

        }

        private void ThemeModeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            DoubleAnimationOfThemeModeCheckBox();
            SetTheme.SetThemeMode(SetTheme.ThemeMode.DarkMode);
        }

        private void ThemeModeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            DoubleAnimationOfThemeModeCheckBox();
            SetTheme.SetThemeMode(SetTheme.ThemeMode.LightMode);
        }

        /// <summary>
        /// Animation of ThemeModeCheckBox(Dark mode, Light mode button)
        /// </summary>
        private void DoubleAnimationOfThemeModeCheckBox()
        {
            Grid iconThemeMode = ThemeModeCheckBox.Template.FindName("IconThemeMode", ThemeModeCheckBox) as Grid;            
            // Get Parent of iconThemeMode
            FrameworkElement parentElement = VisualTreeHelper.GetParent(iconThemeMode) as FrameworkElement;            
            if (iconThemeMode != null && parentElement != null)
            {
                double from;
                double to;
                if (iconThemeMode.RenderTransform.Value.OffsetX >= 0)
                {
                    from = 0;
                    to = -30;
                }
                else
                {
                    from = -30;
                    to = 0;
                }

                TranslateTransform translateTransform = new TranslateTransform();
                iconThemeMode.RenderTransform = translateTransform;

                // Tạo DoubleAnimation cho thay đổi X của TranslateTransform
                DoubleAnimation animation = new DoubleAnimation();
                animation.From = from; // Giá trị ban đầu của X
                animation.To = to; // Giá trị cuối cùng của X
                animation.Duration = TimeSpan.FromMilliseconds(100); // Thời gian thực hiện animation

                // Áp dụng animation vào X của TranslateTransform
                translateTransform.BeginAnimation(TranslateTransform.XProperty, animation);
            }
        }
    }
}
