using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WhenAll
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
        static Task<int> SquareAsync(int x)
        {
            int result = 1;

            return Task.Run(() =>
            {
                result = x * x;
                Thread.Sleep(1000);
                return result;
            });
        }

        private async void buttonSequence_Click(object sender, RoutedEventArgs e)
        {
            int num = 5;

            SequenceButton.IsEnabled = false;

            textBlock1.Text = String.Empty;
            textBlock2.Text = String.Empty;
            textBlock3.Text = String.Empty;

            int result = await SquareAsync(num);
            textBlock1.Text = String.Format("Квадрат числа {0} равен {1}", num, result);

            num = 6;
            result = SquareAsync(num).GetAwaiter().GetResult();
            textBlock2.Text = String.Format("Квадрат числа {0} равен {1}", num, result);

            result = await Task.Run(() =>
            {
                int res = 1;
                for (int i = 1; i <= 9; i++)
                {
                    res += i * i;
                }
                return res;
            });
            textBlock3.Text = String.Format("Сумма квадратов чисел равна {0}", result);

            SequenceButton.IsEnabled = true;
        }

        private async void buttonParallel_Click(object sender, RoutedEventArgs e)
        {
            int num1 = 8;
            int num2 = 16;

            ParallelButton.IsEnabled = false;

            textBlock1.Text = String.Empty;
            textBlock2.Text = String.Empty;
            textBlock3.Text = String.Empty;

            Task<int> t1 = SquareAsync(num1);
            Task<int> t2 = SquareAsync(num2);
            Task<int> t3 = Task.Run(() =>
            {
                int res = 1;
                for (int i = 1; i <= 9; i++)
                {
                    res += i * i;
                }
                return res;
            });

            await Task.WhenAll(new[] { t1, t2, t3 });

            textBlock1.Text = String.Format("Квадрат числа {0} равен {1}", num1, t1.Result);
            textBlock2.Text = String.Format("Квадрат числа {0} равен {1}", num2, t2.Result);
            textBlock3.Text = String.Format("Сумма квадратов чисел равна {0}", t3.Result);

            ParallelButton.IsEnabled = true;
        }
    }
}
