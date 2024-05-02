using Microsoft.Data.SqlClient;
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

namespace RegisterCreditsManageApp.Windows.Alert
{
    /// <summary>
    /// Interaction logic for AlertWindow.xaml
    /// </summary>
    public partial class AlertWindow : Window
    {
        public AlertWindow()
        {
            InitializeComponent();
            string connectionString = "Server =192.168.240.225,1433; UID = sa; Password = 270603; Database = RegisterCreditsManageApp; TrustServerCertificate = true;";
            var sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();   //Mở kết nối
            Console.Write(sqlConnection.State.ToString());
            MessageBox.Show(sqlConnection.State.ToString());
            //...Code truy vấn, cập nhật dữ dữ liệu ở đây
            sqlConnection.Close();  //Đóng kết nối sau khi sử dụng
        }

        public AlertWindow(string alertText, string caption, AlertButton button)
        {
            InitializeComponent();
        }

                        break;
                    }
                case AlertButton.OKCancel:
                    {

                        break;
                    }
            }
        }
        
        private static void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}