﻿using System;
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
        }

        public AlertWindow(string alertText, string caption, AlertButton button)
        {
            InitializeComponent();
            switch (button)
            {
                case AlertButton.OK:
                    {
                        
                        break;
                    }
                case AlertButton.YesNo:
                    {

                        break;
                    }
                case AlertButton.OKCancel:
                    {

                        break;
                    }
            }
        }
    }
}
