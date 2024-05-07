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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

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

            Text.Text = alertText;
            alertCaption.Text = caption;
            Style alerticonStyle = App.Current.Resources["QuestionSvg"] as Style;
            alertIcon.Style = alerticonStyle;
            Button alertBtn1 = FindName("alertBtn1") as Button;
            Button alertBtn2 = FindName("alertBtn2") as Button;
            Button alertBtn3 = FindName("alertBtn3") as Button;
            switch (button)
            {
                 
                case AlertButton.OK:
                    {
                        
                        
                        Style tbnStyle =  App.Current.Resources["AlertBtnOK"]as Style;
                        alertBtn1.Style = tbnStyle;
                        alertBtn1.Click += (sender, e) => {
                        AlertBox.AlertResult = AlertResult.OK;
                         this.Close();
                        };

                        alertBtn2.Visibility = Visibility.Collapsed;
                        alertBtn3.Visibility = Visibility.Collapsed;
                        break;
                    }
                case AlertButton.YesNo:
                    {
                        Style tbnStyle1 = App.Current.Resources["AlertBtnYES"] as Style;
                        alertBtn1.Style = tbnStyle1;
                        alertBtn1.Click += (sender, e) =>
                        {
                            AlertBox.AlertResult =AlertResult.Yes;
                            this.Close();
                        };
                        Style tbnStyle2 = App.Current.Resources["AlertBtnNO"] as Style;
                        alertBtn2.Style = tbnStyle2;
                        alertBtn2.Click += (sender, e) =>
                        {
                            AlertBox.AlertResult = AlertResult.No;
                            this.Close();
                        };
                        alertBtn3.Visibility = Visibility.Collapsed;
                        break;
                    }
                case AlertButton.OKCancel:
                    {
                        Style tbnStyle1 = App.Current.Resources["AlertBtnOK"] as Style;
                        alertBtn1.Style = tbnStyle1;
                        alertBtn1.Click += (sender, e) => {
                            AlertBox.AlertResult = AlertResult.OK;
                            this.Close();
                        };
                        Style tbnStyle2 = App.Current.Resources["AlertBtnCancel"] as Style;
                        alertBtn2.Style = tbnStyle2;
                        alertBtn2.Click += (sender, e) => {
                            AlertBox.AlertResult = AlertResult.Cancel;
                            this.Close();
                        };
                        alertBtn3.Visibility = Visibility.Collapsed;
                        break;
                    }
            }
        }

        public AlertWindow(string alertText, string caption)
        {
            InitializeComponent();

            Text.Text = alertText;
            alertCaption.Text = caption;
            Style alerticonStyle = App.Current.Resources["InfoSvg"] as Style;
            alertIcon.Style = alerticonStyle;
            alertBtn1.Visibility = Visibility.Collapsed;
            alertBtn2.Visibility = Visibility.Collapsed;
            alertBtn3.Visibility = Visibility.Collapsed;
        }

        public AlertWindow (string alertText)
        {
            InitializeComponent();

            Text.Text = alertText;
            alertCaption.Visibility = Visibility.Collapsed;
            Style alerticonStyle = App.Current.Resources["InfoSvg"] as Style;
            alertIcon.Style = alerticonStyle;
            alertBtn1.Visibility = Visibility.Collapsed;
            alertBtn2.Visibility = Visibility.Collapsed;
            alertBtn3.Visibility = Visibility.Collapsed;
        }

        public AlertWindow(string alertText, string caption, AlertButton button, AlertIcon icon)
        {
            InitializeComponent();

            Text.Text = alertText;
            alertCaption.Text = caption;
            

            switch (icon)
            {
                case AlertIcon.Information:
                {
                        Style alerticonStyle = App.Current.Resources["InfoSvg"] as Style;
                        alertIcon.Style = alerticonStyle;
                        switch (button)
                        {
                            case AlertButton.OK:
                                {
                                    Style tbnStyle = App.Current.Resources["AlertBtnOK"] as Style;
                                    alertBtn1.Style = tbnStyle;
                                    alertBtn1.Click += (sender, e) => {
                                        AlertBox.AlertResult = AlertResult.OK;
                                        this.Close();
                                    };

                                    alertBtn2.Visibility = Visibility.Collapsed;
                                    alertBtn3.Visibility = Visibility.Collapsed;
                                    break;
                                }
                            case AlertButton.YesNo:
                                {
                                    Style tbnStyle1 = App.Current.Resources["AlertBtnYES"] as Style;
                                    alertBtn1.Style = tbnStyle1;
                                    alertBtn1.Click += (sender, e) =>
                                    {
                                        AlertBox.AlertResult = AlertResult.Yes;
                                        this.Close();
                                    };
                                    Style tbnStyle2 = App.Current.Resources["AlertBtnNO"] as Style;
                                    alertBtn2.Style = tbnStyle2;
                                    alertBtn2.Click += (sender, e) =>
                                    {
                                        AlertBox.AlertResult = AlertResult.No;
                                        this.Close();
                                    };
                                    alertBtn3.Visibility = Visibility.Collapsed;
                                    break;
                                }
                            case AlertButton.OKCancel:
                                {
                                    Style tbnStyle1 = App.Current.Resources["AlertBtnOK"] as Style;
                                    alertBtn1.Style = tbnStyle1;
                                    alertBtn1.Click += (sender, e) => {
                                        AlertBox.AlertResult = AlertResult.OK;
                                        this.Close();
                                    };
                                    Style tbnStyle2 = App.Current.Resources["AlertBtnCancel"] as Style;
                                    alertBtn2.Style = tbnStyle2;
                                    alertBtn2.Click += (sender, e) => {
                                        AlertBox.AlertResult = AlertResult.Cancel;
                                        this.Close();
                                    };
                                    alertBtn3.Visibility = Visibility.Collapsed;
                                    break;
                                }
                        }
                        break;
                }
                    case AlertIcon.Question:
                    {
                        Style alerticonStyle = App.Current.Resources["QuestionSvg"] as Style;
                        alertIcon.Style = alerticonStyle;
                        switch (button)
                        {
                            case AlertButton.OK:
                                {
                                    Style tbnStyle = App.Current.Resources["AlertBtnOK"] as Style;
                                    alertBtn1.Style = tbnStyle;
                                    alertBtn1.Click += (sender, e) => {
                                        AlertBox.AlertResult = AlertResult.OK;
                                        this.Close();
                                    };
                                    alertBtn2.Visibility = Visibility.Collapsed;
                                    alertBtn3.Visibility = Visibility.Collapsed;
                                    break;
                                }
                            case AlertButton.YesNo:
                                {
                                    Style tbnStyle1 = App.Current.Resources["AlertBtnYES"] as Style;
                                    alertBtn1.Style = tbnStyle1;
                                    alertBtn1.Click += (sender, e) =>
                                    {
                                        AlertBox.AlertResult = AlertResult.Yes;
                                        this.Close();
                                    };
                                    Style tbnStyle2 = App.Current.Resources["AlertBtnNO"] as Style;
                                    alertBtn2.Style = tbnStyle2;
                                    alertBtn2.Click += (sender, e) =>
                                    {
                                        AlertBox.AlertResult = AlertResult.No;
                                        this.Close();
                                    };
                                    alertBtn3.Visibility = Visibility.Collapsed;
                                    break;
                                }
                            case AlertButton.OKCancel:
                                {
                                    Style tbnStyle1 = App.Current.Resources["AlertBtnOK"] as Style;
                                    alertBtn1.Style = tbnStyle1;
                                    alertBtn1.Click += (sender, e) => {
                                        AlertBox.AlertResult = AlertResult.OK;
                                        this.Close();
                                    };
                                    Style tbnStyle2 = App.Current.Resources["AlertBtnCancel"] as Style;
                                    alertBtn2.Style = tbnStyle2;
                                    alertBtn2.Click += (sender, e) => {
                                        AlertBox.AlertResult = AlertResult.Cancel;
                                        this.Close();
                                    };
                                    alertBtn3.Visibility = Visibility.Collapsed;
                                    break;
                                }
                        }
                        break;
                    }
                    case AlertIcon.Warning:
                    {
                        Style alerticonStyle = App.Current.Resources["WarningSvg"] as Style;
                        alertIcon.Style = alerticonStyle;
                        Brush fg = App.Current.Resources["AlertCaption_Warning"] as Brush;
                        alertCaption.Foreground =fg;
                        switch (button)
                        {
                            case AlertButton.OK:
                                {
                                    Style tbnStyle = App.Current.Resources["AlertBtnOK_Warning"] as Style;
                                    alertBtn1.Style = tbnStyle;
                                    alertBtn1.Click += (sender, e) => {
                                        AlertBox.AlertResult = AlertResult.OK;
                                        this.Close();
                                    };

                                    alertBtn2.Visibility = Visibility.Collapsed;
                                    alertBtn3.Visibility = Visibility.Collapsed;
                                    break;
                                }
                            case AlertButton.YesNo:
                                {
                                    Style tbnStyle1 = App.Current.Resources["AlertBtnYES_Warning"] as Style;
                                    alertBtn1.Style = tbnStyle1;
                                    alertBtn1.Click += (sender, e) =>
                                    {
                                        AlertBox.AlertResult = AlertResult.Yes;
                                        this.Close();
                                    };
                                    Style tbnStyle2 = App.Current.Resources["AlertBtnNO_Warning"] as Style;
                                    alertBtn2.Style = tbnStyle2;
                                    alertBtn2.Click += (sender, e) =>
                                    {
                                        AlertBox.AlertResult = AlertResult.No;
                                        this.Close();
                                    };
                                    alertBtn3.Visibility = Visibility.Collapsed;
                                    break;
                                }
                            case AlertButton.OKCancel:
                                {
                                    Style tbnStyle1 = App.Current.Resources["AlertBtnOK_Warning"] as Style;
                                    alertBtn1.Style = tbnStyle1;
                                    alertBtn1.Click += (sender, e) => {
                                        AlertBox.AlertResult = AlertResult.OK;
                                        this.Close();
                                    };
                                    Style tbnStyle2 = App.Current.Resources["AlertBtnCancel_Warning"] as Style;
                                    alertBtn2.Style = tbnStyle2;
                                    alertBtn2.Click += (sender, e) => {
                                        AlertBox.AlertResult = AlertResult.Cancel;
                                        this.Close();
                                    };
                                    alertBtn3.Visibility = Visibility.Collapsed;
                                    break;
                                }
                        }
                        break;  
                    }
                    case AlertIcon.Error:
                    {
                        Style alerticonStyle = App.Current.Resources["ErrorSvg"] as Style;
                        alertIcon.Style = alerticonStyle;
                        Brush fg = App.Current.Resources["AlertCaption_Error"] as Brush;
                        alertCaption.Foreground = fg;
                        switch (button)
                        {
                            case AlertButton.OK:
                                {


                                    Style tbnStyle = App.Current.Resources["AlertBtnOK_Error"] as Style;
                                    alertBtn1.Style = tbnStyle;
                                    alertBtn1.Click += (sender, e) => {
                                        AlertBox.AlertResult = AlertResult.OK;
                                        this.Close();
                                    };

                                    alertBtn2.Visibility = Visibility.Collapsed;
                                    alertBtn3.Visibility = Visibility.Collapsed;
                                    break;
                                }
                            case AlertButton.YesNo:
                                {
                                    Style tbnStyle1 = App.Current.Resources["AlertBtnYES_Error"] as Style;
                                    alertBtn1.Style = tbnStyle1;
                                    alertBtn1.Click += (sender, e) =>
                                    {
                                        AlertBox.AlertResult = AlertResult.Yes;
                                        this.Close();
                                    };
                                    Style tbnStyle2 = App.Current.Resources["AlertBtnNO_Error"] as Style;
                                    alertBtn2.Style = tbnStyle2;
                                    alertBtn2.Click += (sender, e) =>
                                    {
                                        AlertBox.AlertResult = AlertResult.No;
                                        this.Close();
                                    };
                                    alertBtn3.Visibility = Visibility.Collapsed;
                                    break;
                                }
                            case AlertButton.OKCancel:
                                {
                                    Style tbnStyle1 = App.Current.Resources["AlertBtnOK_Error"] as Style;
                                    alertBtn1.Style = tbnStyle1;
                                    alertBtn1.Click += (sender, e) => {
                                        AlertBox.AlertResult = AlertResult.OK;
                                        this.Close();
                                    };
                                    Style tbnStyle2 = App.Current.Resources["AlertBtnCancel_Error"] as Style;
                                    alertBtn2.Style = tbnStyle2;
                                    alertBtn2.Click += (sender, e) => {
                                        AlertBox.AlertResult = AlertResult.Cancel;
                                        this.Close();
                                    };
                                    alertBtn3.Visibility = Visibility.Collapsed;
                                    break;
                                }
                        }
                        break;
                    }
                    case AlertIcon.Success:
                    {
                        Style alerticonStyle = App.Current.Resources["SuccessSvg"] as Style;
                        alertIcon.Style = alerticonStyle;
                        Brush fg = App.Current.Resources["AlertCaption_Success"] as Brush;
                        alertCaption.Foreground = fg;
                        switch (button)
                        {
                            case AlertButton.OK:
                                {


                                    Style tbnStyle = App.Current.Resources["AlertBtnOK_Success"] as Style;
                                    alertBtn1.Style = tbnStyle;
                                    alertBtn1.Click += (sender, e) => {
                                        AlertBox.AlertResult = AlertResult.OK;
                                        this.Close();
                                    };

                                    alertBtn2.Visibility = Visibility.Collapsed;
                                    alertBtn3.Visibility = Visibility.Collapsed;
                                    break;
                                }
                            case AlertButton.YesNo:
                                {
                                    Style tbnStyle1 = App.Current.Resources["AlertBtnYES_Success"] as Style;
                                    alertBtn1.Style = tbnStyle1;
                                    alertBtn1.Click += (sender, e) =>
                                    {
                                        AlertBox.AlertResult = AlertResult.Yes;
                                        this.Close();
                                    };
                                    Style tbnStyle2 = App.Current.Resources["AlertBtnNO_Success"] as Style;
                                    alertBtn2.Style = tbnStyle2;
                                    alertBtn2.Click += (sender, e) =>
                                    {
                                        AlertBox.AlertResult = AlertResult.No;
                                        this.Close();
                                    };
                                    alertBtn3.Visibility = Visibility.Collapsed;
                                    break;
                                }
                            case AlertButton.OKCancel:
                                {
                                    Style tbnStyle1 = App.Current.Resources["AlertBtnOK_Success"] as Style;
                                    alertBtn1.Style = tbnStyle1;
                                    alertBtn1.Click += (sender, e) => {
                                        AlertBox.AlertResult = AlertResult.OK;
                                        this.Close();
                                    };
                                    Style tbnStyle2 = App.Current.Resources["AlertBtnCancel_Success"] as Style;
                                    alertBtn2.Style = tbnStyle2;
                                    alertBtn2.Click += (sender, e) => {
                                        AlertBox.AlertResult = AlertResult.Cancel;
                                        this.Close();
                                    };
                                    alertBtn3.Visibility = Visibility.Collapsed;
                                    break;
                                }
                        }
                        break;
                    }
            }
        }

        public AlertWindow(string alertText, string caption, AlertButton button, AlertIcon icon, AlertResult defaultResult)
        {
            InitializeComponent();

            Text.Text = alertText;
            alertCaption.Text = caption;
            switch (button)
            {
                case AlertButton.OK:
                    {


                        Style tbnStyle = App.Current.Resources["AlertBtnOK"] as Style;
                        alertBtn1.Style = tbnStyle;
                        alertBtn1.Click += (sender, e) => {
                            AlertBox.AlertResult = AlertResult.OK;
                            this.Close();
                        };

                        alertBtn2.Visibility = Visibility.Collapsed;
                        alertBtn3.Visibility = Visibility.Collapsed;
                        break;
                    }
                case AlertButton.YesNo:
                    {
                        Style tbnStyle1 = App.Current.Resources["AlertBtnYES"] as Style;
                        alertBtn1.Style = tbnStyle1;
                        alertBtn1.Click += (sender, e) =>
                        {
                            AlertBox.AlertResult = AlertResult.Yes;
                            this.Close();
                        };
                        Style tbnStyle2 = App.Current.Resources["AlertBtnNO"] as Style;
                        alertBtn2.Style = tbnStyle2;
                        alertBtn2.Click += (sender, e) =>
                        {
                            AlertBox.AlertResult = AlertResult.No;
                            this.Close();
                        };
                        alertBtn3.Visibility = Visibility.Collapsed;
                        break;
                    }
                case AlertButton.OKCancel:
                    {
                        Style tbnStyle1 = App.Current.Resources["AlertBtnOK"] as Style;
                        alertBtn1.Style = tbnStyle1;
                        alertBtn1.Click += (sender, e) => {
                            AlertBox.AlertResult = AlertResult.OK;
                            this.Close();
                        };
                        Style tbnStyle2 = App.Current.Resources["AlertBtnCancel"] as Style;
                        alertBtn2.Style = tbnStyle2;
                        alertBtn2.Click += (sender, e) => {
                            AlertBox.AlertResult = AlertResult.Cancel;
                            this.Close();
                        };
                        alertBtn3.Visibility = Visibility.Collapsed;
                        break;
                    }
            }

            switch (icon)
            {
                case AlertIcon.Information:
                    {
                        Style alerticonStyle = App.Current.Resources["InfoSvg"] as Style;
                        alertIcon.Style = alerticonStyle;
                        break;
                    }
                case AlertIcon.Question:
                    {
                        Style alerticonStyle = App.Current.Resources["QuestionSvg"] as Style;
                        alertIcon.Style = alerticonStyle;
                        break;
                    }
                case AlertIcon.Warning:
                    {
                        Style alerticonStyle = App.Current.Resources["WarningSvg"] as Style;
                        alertIcon.Style = alerticonStyle;
                        Brush fg = App.Current.Resources["AlertCaption_Warning"] as Brush;
                        alertCaption.Foreground = fg;
                        break;
                    }
                case AlertIcon.Error:
                    {
                        Style alerticonStyle = App.Current.Resources["ErrorSvg"] as Style;
                        alertIcon.Style = alerticonStyle;
                        Brush fg = App.Current.Resources["AlertCaption_Error"] as Brush;
                        alertCaption.Foreground = fg;
                        break;
                    }
                case AlertIcon.Success:
                    {
                        Style alerticonStyle = App.Current.Resources["SuccessSvg"] as Style;
                        alertIcon.Style = alerticonStyle;
                        Brush fg = App.Current.Resources["AlertCaption_Success"] as Brush;
                        alertCaption.Foreground = fg;
                        break;
                    }

            }
        }
            private void xAlertBtn(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}