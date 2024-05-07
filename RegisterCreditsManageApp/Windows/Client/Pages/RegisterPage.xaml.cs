using RegisterCreditsManageApp.Models;
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

namespace RegisterCreditsManageApp.Windows.Client.Pages
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {            
            InitializeComponent();
            DataGridRegister.ItemsSource = new List<Data>()
            {
                new Data() { Stt = "1", MaHocPhan = "MaHocPhan1", MonHoc = "MonHoc1", SoTinChi = "1"},
                new Data() { Stt = "2", MaHocPhan = "MaHocPhan2", MonHoc = "MonHoc2", SoTinChi = "5"},
                new Data() { Stt = "3", MaHocPhan = "MaHocPhan3", MonHoc = "MonHoc3", SoTinChi = "7"},
                new Data() { Stt = "4", MaHocPhan = "MaHocPhan4", MonHoc = "MonHoc4", SoTinChi = "6"},
                new Data() { Stt = "5", MaHocPhan = "MaHocPhan5", MonHoc = "MonHoc5", SoTinChi = "1"},
            };
        }
        public class Data
        {
            public string Stt { get; set; }
            public string MaHocPhan { get; set; }
            public string MonHoc { get; set; }
            public string SoTinChi { get; set; }
        }
    }
}
