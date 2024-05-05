using System.Windows;

namespace RegisterCreditsManageApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private bool isDragging;
        private Point lastMousePosition;
        protected override void OnExit(ExitEventArgs e)
        {

            base.OnExit(e);
        }
    }
}
