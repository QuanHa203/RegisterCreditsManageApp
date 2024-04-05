using RegisterCreditsManageApp.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows.Input;
using RegisterCreditsManageApp.Models;

namespace RegisterCreditsManageApp.Windows.Client
{
    /// <summary>
    /// Interaction logic for ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        public ClientWindow()
        {
            InitializeComponent();
        }

        private void ThemeModeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            LinearGradientBrush b = new LinearGradientBrush();
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

        private void LeftMenuBtnMouseLeaveAnimation(Button btn)
        {
            Border border = btn.Content as Border;
            LinearGradientBrush brush = border.Background as LinearGradientBrush;
            //border.BorderBrush = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 1;
            animation.To = 0;
            animation.Duration = TimeSpan.FromMilliseconds(100);

            brush.GradientStops[1].BeginAnimation(GradientStop.OffsetProperty, animation);
            border.BorderBrush.BeginAnimation(SolidColorBrush.OpacityProperty, animation);
        }

        private void LeftMenuBtnMouseEnterAnimation(Button btn)
        {
            Border border = btn.Content as Border;
            LinearGradientBrush brush = border.Background as LinearGradientBrush;
            //border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3663fe"));

            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0;
            animation.To = 1;
            animation.Duration = TimeSpan.FromMilliseconds(100);

            brush.GradientStops[1].BeginAnimation(GradientStop.OffsetProperty, animation);
            border.BorderBrush.BeginAnimation(SolidColorBrush.OpacityProperty, animation);
        }

        private void LeftMenuBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            LeftMenuBtnMouseLeaveAnimation((Button)sender);
        }

        private void LeftMenuBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            LeftMenuBtnMouseEnterAnimation((Button)sender);
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StudyProgramBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AccountBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
