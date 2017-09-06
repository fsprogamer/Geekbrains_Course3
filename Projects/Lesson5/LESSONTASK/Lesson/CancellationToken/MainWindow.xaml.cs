using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace CancellationToken
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CancellationTokenSource cts = new CancellationTokenSource();
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            int num = 5;
            textBlock.Text = String.Empty;

            if (cts.IsCancellationRequested)
                cts = new CancellationTokenSource();

            int result = await MethodAsync(num);

            Thread.Sleep(1000);
            textBlock.Text = result.ToString();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            cts.Cancel();
        }
        public Task<int> MethodAsync(int x)
        {
            int result = 1;

            return Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(1000);
                    if (cts.Token.IsCancellationRequested)
                    {
                        MessageBox.Show("Операция прервана токеном");
                        break;
                    }
                    result *= x;
                }
                return result;
            }, cts.Token);
        }
    }
}
