using System.Windows;
using System.Windows.Controls;

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

        private void DataGridRow_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DataGridRow dataGridRow = (sender as DataGridRow)!;
            dataGridRow.IsSelected = true;
        }
    }
}
