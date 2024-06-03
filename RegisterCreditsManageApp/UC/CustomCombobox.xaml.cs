using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RegisterCreditsManageApp.UC
{
    public partial class CustomCombobox : UserControl
    {
        public CustomCombobox()
        {
            InitializeComponent();     
            this.DataContext = this;
        }        

        public string _Text
        {
            get { return btnShowPopup.Content.ToString()!;}
            set { btnShowPopup.Content = value;}
        }        

        public bool _IsOpen
        {
            get { return popup.IsOpen; }
            set { popup.IsOpen = value;}
        }

        public Thickness _BorderThickness { get; set; } = new Thickness(1);

        public Thickness _Padding { get; set; } = new Thickness(8, 12, 8, 12);

        public UIElementCollection CustomComboboxChildren
        {
            get { return popupData.Children; }
        }

        public Style CustomComboboxStyleChildren
        {
            get { return (App.Current.Resources["RadioButtonInPopup"] as Style)!; }
        }

        public void btnShowPopup_Click(object sender, RoutedEventArgs e)
        {            
            _IsOpen = true;
        }
    }
}
