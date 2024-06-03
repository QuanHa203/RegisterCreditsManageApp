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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RegisterCreditsManageApp.UC
{
    /// <summary>
    /// Interaction logic for SearchTextBox.xaml
    /// </summary>
    public partial class SearchTextBox : UserControl
    {
        public event RoutedEventHandler Click;
        public string _Text
        {
            get {return textBoxPlaceHolder._Text;}
            set { textBoxPlaceHolder._Text = value; }
        }

        public string _PlaceHolder
        {
            set {  textBoxPlaceHolder._PlaceHolder = value; }
        }

        public SearchTextBox()
        {
            InitializeComponent();
            
        }

        public void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            if(Click  != null)
            {
                Click(sender, e);
            }
        }
    }
}
