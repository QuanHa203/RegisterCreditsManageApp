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
        public static AlertResult AlertResult;
        public static AlertResult Show(string alertText)
        {
            AlertWindow alert = new AlertWindow(alertText);
            alert.ShowDialog();
            return AlertResult.Cancel;
        }

        public static AlertResult Show(string alertText, string caption)
        {
            AlertWindow alert = new AlertWindow(alertText, caption);
            alert.ShowDialog();
            return AlertResult.Cancel;
        }

        public static AlertResult Show(string alertText, string caption, AlertButton button)
        {
            AlertWindow alert = new AlertWindow(alertText, caption, button);
            alert.ShowDialog();
            return AlertResult;
        }

        public static AlertResult Show(string alertText, string caption, AlertButton button, AlertIcon icon)
        {
            AlertWindow alert = new AlertWindow(alertText, caption, button, icon);
            alert.ShowDialog();
            return AlertResult.Cancel;
        }

        public static AlertResult Show(string alertText, string caption, AlertButton button, AlertIcon icon, AlertResult defaultResult)
        {
            AlertWindow alert = new AlertWindow(alertText, caption, button, icon, defaultResult);
            alert.ShowDialog();
            return defaultResult;
        }
    }
}