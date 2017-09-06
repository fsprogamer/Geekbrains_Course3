using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SyncVsAsync
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

        //private void button_Click(object sender, RoutedEventArgs e)
        //{
        //    int num = 5;
        //    textBlock.Text = MethodSync(num).ToString();
        //}

        //static int MethodSync(int x)
        //{
        //    int result = 1;

        //    Thread.Sleep(10000);
        //    result = x * x;
        //    return result;
        //}

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            int num = 5;

            int result = await MethodAsync(num);
            Thread.Sleep(1000);
            textBlock.Text = result.ToString();
        }

        static Task<int> MethodAsync(int x)
        {
            int result = 1;

            return Task.Run(() =>
            {
                Thread.Sleep(10000);
                result = x * x;
                return result;
            });
        }
    }
}

