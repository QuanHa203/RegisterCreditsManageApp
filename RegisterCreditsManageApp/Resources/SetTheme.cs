using System.Windows;

namespace RegisterCreditsManageApp.Resources
{
    public class SetTheme
    {
        private static Uri darkModeUri = new Uri("/Resources/DarkMode.xaml", UriKind.Relative);
        private static Uri lightModeUri = new Uri("/Resources/LightMode.xaml", UriKind.Relative);

        public enum ThemeMode
        {
            DarkMode,
            LightMode
        }

        public static void SetThemeMode(ThemeMode mode)
        {
            // Get Add ResourceDictionary was merged in AppResourceDictionary
            var mergedDictionaries = App.Current.Resources.MergedDictionaries;
            switch (mode)
            {
                case ThemeMode.DarkMode:
                    {
                        foreach (var dictionary in mergedDictionaries)
                        {
                            // Check if lightModeResourceDictionary to delete
                            if (dictionary.Source.OriginalString.Contains(ThemeMode.LightMode.ToString()))
                            {
                                // Create darkModeDictionary
                                var darkModeResource = new ResourceDictionary { Source = darkModeUri };

                                // Delete lightModeDictionary
                                mergedDictionaries.Remove(dictionary);

                                // Add DarkModeDictionary
                                mergedDictionaries.Add(darkModeResource);
                                break;
                            }
                        }
                        break;
                    }
                case ThemeMode.LightMode:
                    {                        
                        foreach (var dictionary in mergedDictionaries)
                        {
                            // Check if darkModeResourceDictionary to delete
                            if (dictionary.Source.OriginalString.Contains(ThemeMode.DarkMode.ToString()))
                            {
                                var lightModeResource = new ResourceDictionary { Source = lightModeUri };
                                mergedDictionaries.Remove(dictionary);
                                mergedDictionaries.Add(lightModeResource);
                                break;
                            }
                        }
                        break;
                    }
                default:
                    {

                        break;
                    }
            }
        }
    }
}
