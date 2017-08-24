using System.Windows;

namespace Event
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Grid_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MessageBox.Show("Координаты " + e.GetPosition(this).ToString());
        }

        private void MainGrid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}
