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
using System.Windows.Shapes;

namespace RegisterCreditsManageApp.Windows.Server.Pages.SubStudentPage
{
    /// <summary>
    /// Interaction logic for StudentEditWindow.xaml
    /// </summary>
    public partial class StudentEditWindow : Window
    {
        private Student student;
        public StudentEditWindow(string idStudent)
        {
            InitializeComponent();
            student = AppDbContext._Context.Students.FirstOrDefault(s => s.IdStudent == idStudent);            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void textBoxDateOfBirth_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnMajors_Click(object sender, RoutedEventArgs e)
        {
            popupMajor.IsOpen = true;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
