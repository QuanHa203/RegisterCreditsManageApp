using RegisterCreditsManageApp.Resources;
using RegisterCreditsManageApp.Windows.Alert;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace RegisterCreditsManageApp.Windows
{
    public abstract class MainWindow : System.Windows.Window
    {
        public MainWindow() : base()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        /// <summary>
        /// Handle event when user click on the ThemeModeCheckbox.
        /// In this case ThemeModeCheckbox is checked
        /// </summary>
        /// <param name="sender">sender must System.Windows.Controls.CheckBox</param>
        /// <param name="e"></param>
        protected virtual void ThemeModeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                CheckBox checkBox = (sender as CheckBox)!;
                ThemeModeCheckBoxAnimation(checkBox);
                SetTheme.SetThemeMode(SetTheme.ThemeMode.LightMode);
            }
            catch
            {

            }
        }

        /// <summary>
        /// Handle event when user click on the ThemeModeCheckbox.
        /// In this case ThemeModeCheckbox is unchecked
        /// </summary>
        /// <param name="sender">sender must System.Windows.Controls.CheckBox</param>
        /// <param name="e"></param>
        protected virtual void ThemeModeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                CheckBox checkBox = (sender as CheckBox)!;
                ThemeModeCheckBoxAnimation(checkBox);
                SetTheme.SetThemeMode(SetTheme.ThemeMode.DarkMode);
            }
            catch
            {

            }
        }

        /// <summary>
        /// Handle event mouse leave to add animation
        /// </summary>
        /// <param name="sender">sender must System.Windows.Controls.RadioButton</param>
        /// <param name="e"></param>
        protected void LeftMenuRadioButton_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                RadioButton radioButton = (sender as RadioButton)!;
                if (!radioButton.IsChecked!.Value)
                    LeftMenuRadioButtonMouseLeaveAnimation(radioButton);
            }
            catch { }            
        }

        /// <summary>
        /// Handle event mouse enter to add animation
        /// </summary>
        /// <param name="sender">sender must System.Windows.Controls.RadioButton</param>
        /// <param name="e"></param>
        protected void LeftMenuRadioButton_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                RadioButton radioButton = (sender as RadioButton)!;
                if (!radioButton.IsChecked!.Value)
                    LeftMenuRadioButtonMouseEnterAnimation(radioButton);
            }
            catch { }
        }

        /// <summary>
        /// Add Style when RadioButton is Checked
        /// </summary>
        /// <param name="sender">sender must System.Windows.Controls.RadioButton</param>
        /// <param name="e"></param>
        protected void LeftMenuRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                RadioButton radioButton = (sender as RadioButton)!;
                if (radioButton.IsChecked!.Value)
                    LeftMenuRadioButtonCheckedStyle(radioButton);
            }
            catch { }
        }

        /// <summary>
        /// Remove Style when RadioButton is UnChecked
        /// </summary>
        /// <param name="sender">sender must System.Windows.Controls.RadioButton</param>
        /// <param name="e"></param>
        protected void LeftMenuRadioButton_UnChecked(object sender, RoutedEventArgs e)
        {
            try
            {
                RadioButton radioButton = (sender as RadioButton)!;
                if (!radioButton.IsChecked!.Value)
                    LeftMenuRadioButtonUnCheckedStyle(radioButton);                    
            }
            catch { }
        }

        /// <summary>
        /// Animation of ThemeModeCheckBox(Dark mode, Light mode button)
        /// </summary>
        /// <param name="checkBox"></param>
        private void ThemeModeCheckBoxAnimation(CheckBox checkBox)
        {
            // Find child of checkbox
            Grid iconThemeMode = (checkBox.Template.FindName("IconThemeMode", checkBox) as Grid)!;

            // Get Parent of iconThemeMode
            FrameworkElement parentElement = (VisualTreeHelper.GetParent(iconThemeMode) as FrameworkElement)!;

            double from;
            double to;
            if (iconThemeMode.RenderTransform.Value.OffsetX >= 0)
            {
                // Caculator distance to move
                double distanceTo = parentElement.ActualWidth - (iconThemeMode.ActualWidth + iconThemeMode.Margin.Left + iconThemeMode.Margin.Right);
                from = 0;
                to = -distanceTo;
            }
            else
            {
                // Caculator distance to move
                double distanceFrom = parentElement.ActualWidth - (iconThemeMode.ActualWidth + iconThemeMode.Margin.Left + iconThemeMode.Margin.Right);
                from = -distanceFrom;
                to = 0;
            }

            TranslateTransform translateTransform = new TranslateTransform();

            // Reference transform of iconThemeMode to translateTransform variable
            iconThemeMode.RenderTransform = translateTransform;

            // Create DoubleAnimation
            DoubleAnimation animation = new DoubleAnimation();

            // Define value from
            animation.From = from;

            // Define value end
            animation.To = to;

            // Define duration of animation
            animation.Duration = System.TimeSpan.FromMilliseconds(100);

            // Apply animation in X axis of TranslateTranform
            translateTransform.BeginAnimation(TranslateTransform.XProperty, animation);

        }

        private void LeftMenuRadioButtonCheckedStyle(RadioButton radioButton)
        {
            // Get border from radioButton
            Border border = (radioButton.Content as Border)!;

            LinearGradientBrush brush = (border.Background as LinearGradientBrush)!;
            brush.GradientStops[1].Offset = 1;

            StackPanel stackPanel = (border.Child as StackPanel)!;
            Path path = ((stackPanel.Children[0] as Viewbox)!.Child as Path)!;
            var pathFillAndTextBlockForeground = (App.Current.Resources["SvgLeftMenuColorHover"] as SolidColorBrush)!;
            path.Fill = pathFillAndTextBlockForeground;
            TextBlock textBlock = (stackPanel.Children[1] as TextBlock)!;
            textBlock.Foreground = pathFillAndTextBlockForeground;
        }

        private void LeftMenuRadioButtonUnCheckedStyle(RadioButton radioButton)
        {
            // Get border from radioButton
            Border border = (radioButton.Content as Border)!;
            SolidColorBrush colorBorder = (SolidColorBrush)border.BorderBrush;
            border.BorderBrush = new SolidColorBrush(colorBorder.Color) { Opacity = 0};

            LinearGradientBrush brush = (border.Background as LinearGradientBrush)!;
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 1;
            animation.To = 0;
            animation.Duration = System.TimeSpan.FromMilliseconds(0);
            brush.GradientStops[1].BeginAnimation(GradientStop.OffsetProperty, animation);

            StackPanel stackPanel = (border.Child as StackPanel)!;
            Path path = ((stackPanel.Children[0] as Viewbox)!.Child as Path)!;
            path.SetValue(Path.FillProperty, DependencyProperty.UnsetValue);

            TextBlock textBlock = (stackPanel.Children[1] as TextBlock)!;
            textBlock.SetValue(TextBlock.ForegroundProperty, DependencyProperty.UnsetValue);
        }

        /// <summary>
        /// Animation of LeftMenuRadioButton when mouse enter
        /// </summary>
        /// <param name="checkBox"></param>
        private void LeftMenuRadioButtonMouseEnterAnimation(RadioButton radioButton)
        {
            // Get border from checkBox
            Border border = (radioButton.Content as Border)!;

            // Reference background of border to brush variable
            LinearGradientBrush brush = (border.Background as LinearGradientBrush)!;

            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0;
            animation.To = 1;
            animation.Duration = System.TimeSpan.FromMilliseconds(200);

            // Apply animation in Offset of second GradientStop (run from 0 to 1)
            brush.GradientStops[1].BeginAnimation(GradientStop.OffsetProperty, animation);   

            // Apply animation in BorderBrush (opacity from 0 to 1)
            border.BorderBrush.BeginAnimation(SolidColorBrush.OpacityProperty, animation);            

            StackPanel stackPanel = (border.Child as StackPanel)!;
            Path path = ((stackPanel.Children[0] as Viewbox)!.Child as Path)!;
            var pathFillAndTextBlockForeground = (App.Current.Resources["SvgLeftMenuColorHover"] as SolidColorBrush)!;
            path.Fill = pathFillAndTextBlockForeground;
            TextBlock textBlock = (stackPanel.Children[1] as TextBlock)!;
            textBlock.Foreground = pathFillAndTextBlockForeground;

            ColorAnimation colorAnimation = new ColorAnimation();
            colorAnimation.From = (path.Fill as SolidColorBrush)!.Color;
            colorAnimation.To = pathFillAndTextBlockForeground.Color;
            colorAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(200));

            Storyboard.SetTarget(colorAnimation, path);
            Storyboard.SetTargetProperty(colorAnimation, new PropertyPath("Fill.Color"));

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(colorAnimation);
            storyboard.Begin();

        }

        /// <summary>
        /// Animation of LeftMenuRadioButton when mouse leave
        /// </summary>
        /// <param name="radioButton"></param>
        private async void LeftMenuRadioButtonMouseLeaveAnimation(RadioButton radioButton)
        {
            // Get border from radioButton
            Border border = (radioButton.Content as Border)!;

            // Reference background of border to brush variable
            LinearGradientBrush brush = (border.Background as LinearGradientBrush)!;

            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 1;
            animation.To = 0;
            animation.Duration = System.TimeSpan.FromMilliseconds(200);

            // Apply animation in Offset of second GradientStop (run from 1 to 0)
            brush.GradientStops[1].BeginAnimation(GradientStop.OffsetProperty, animation);

            // Apply animation in BorderBrush (opacity from 1 to 0)
            border.BorderBrush.BeginAnimation(SolidColorBrush.OpacityProperty, animation);

            StackPanel stackPanel = (border.Child as StackPanel)!;
            Path path = ((stackPanel.Children[0] as Viewbox)!.Child as Path)!;

            TextBlock textBlock = (stackPanel.Children[1] as TextBlock)!;
            textBlock.SetValue(TextBlock.ForegroundProperty, DependencyProperty.UnsetValue);
            var pathColor = (App.Current.Resources["SvgLeftMenuColor"] as SolidColorBrush)!;

            await Task.Delay(120);
            path.SetValue(Path.FillProperty, DependencyProperty.UnsetValue);
        }
    }
}