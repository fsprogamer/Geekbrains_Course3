using MVVM.ViewModel;
using System.Windows;

namespace MVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new CarViewModel();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            ((CarViewModel)DataContext).AddCar();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            ((CarViewModel)DataContext).DeleteCar();
        }
    }
}
