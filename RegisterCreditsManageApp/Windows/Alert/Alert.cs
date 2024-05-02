﻿using System.Windows;

namespace RegisterCreditsManageApp.Windows.Alert
{
    public enum AlertResult
    {
        None,
        OK,
        Cancel,
        Yes,
        No
    }

    public enum AlertButton
    {
        OK = 0,
        OKCancel = 1,
        YesNo = 2
    }

    public enum AlertIcon
    {
        None = 0,
        Success = 1,
        Error = 2,
        Question = 3,
        Warning = 4,
        Information = 5
    }

    public class AlertBox
    {
        public static AlertResult Show(string alertText)
        {
            
            return AlertResult.None;
        }

        public static AlertResult Show(string alertText, string caption)
        {
            return AlertResult.None;
        }

        public static AlertResult Show(string alertText, string caption, AlertButton button)
        {
            AlertWindow alert = new AlertWindow(alertText, caption, button);
            alert.ShowDialog();
            return AlertResult.Cancel;
        }

        public static AlertResult Show(string alertText, string caption, AlertButton button, AlertIcon icon)
        {
            return AlertResult.Cancel;
        }
    }
}