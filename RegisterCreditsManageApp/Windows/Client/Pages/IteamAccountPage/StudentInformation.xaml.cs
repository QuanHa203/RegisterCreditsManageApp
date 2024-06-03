using Microsoft.EntityFrameworkCore;
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

namespace RegisterCreditsManageApp.Windows.Client.Pages.IteamAccountPage
{
    /// <summary>
    /// Interaction logic for StudentInformation.xaml
    /// </summary>
    public partial class StudentInformation : Page
    {
        Student student = LoginWindow.CurrentStudent;
        
        public StudentInformation()
        {
            InitializeComponent();
        }

        private void textBoxDateOfBirth_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            textBoxIdStudent.Text = student.IdStudent;
            textBoxName.Text = student.Name;
            textBoxAddress.Text = student.Address;
            textBoxPhoneNumber.Text = student.PhoneNumber;
            textBoxEmail.Text = student.IdStudentNavigation.Email;
            textBoxDateOfBirth.Text = student.DateOfBirth.ToString();
            if (student.Gender == false)
            {
                radioButtonGenderMale.IsChecked = true;
            }
            else
            {
                RadioButtonGenderFemale.IsChecked = true;
            }    


        }
    }
}
