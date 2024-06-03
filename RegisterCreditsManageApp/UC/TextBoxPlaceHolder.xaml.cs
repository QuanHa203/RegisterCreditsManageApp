using System.Windows;
using System.Windows.Controls;

namespace RegisterCreditsManageApp.UC
{
    /// <summary>
    /// Interaction logic for TextBlockWithPlaceHolder.xaml
    /// </summary>
    public partial class TextBoxPlaceHolder : UserControl
    {
        public string _PlaceHolder
        {
            set { placeHolder.Text = value; }
        }
        public string _Text
        {
            get { return text.Text; }
            set { text.Text = value; }
        }

        public Thickness _BorderThickness { get; set; } = new Thickness(1);

        public TextBoxPlaceHolder()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void text_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (text.Text.Length > 0)
                placeHolder.Visibility = Visibility.Hidden;

            else
                placeHolder.Visibility = Visibility.Visible;
        }
    }
}
