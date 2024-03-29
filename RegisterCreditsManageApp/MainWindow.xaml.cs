using RegisterCreditsManageApp.Models;
using System.Windows;
using System.Linq;
using System.Collections;
using Microsoft.EntityFrameworkCore;

namespace RegisterCreditsManageApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AppDbContext context = new AppDbContext();            
        }
    }
}