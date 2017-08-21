using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ДЗ_5_и_6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonFactorial_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int number = 0;
                bool ok = int.TryParse(txbFact.Text, out number);
                if (!ok)
                    throw new Exception();
                if (number < 2 || number > 20)
                    throw new Exception();
                Thread thread = new Thread(new ParameterizedThreadStart(Factorial))
                {
                    Name = "Вторичный поток факториал"
                };
                thread.Start(number);
            }
            catch (Exception)
            {
                MessageBox.Show("Неправильно введено число");
            }
        }



        public void Factorial(object number)
        {
            string s = FactorialAndSum.Factorial((int)number).ToString();
            Dispatcher.Invoke(() => { txblResult.Text = "Факториал числа " + number + " равен " + s; });
        }
        public void Sum(object number)
        {
            string s = FactorialAndSum.Sum((int)number).ToString();
            Dispatcher.Invoke(() => { txblResult.Text = "Сумма чисел до числа " + number + " равна " + s; });

        }

        private void ButtonSum_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int number = 0;
                bool ok = int.TryParse(txbFact.Text, out number);
                if (!ok)
                    throw new Exception();

                Thread thread = new Thread(new ParameterizedThreadStart(Sum))
                {
                    Name = "Вторичный поток факториал"
                };
                thread.Start(number);
            }
            catch (Exception)
            {
                MessageBox.Show("Неправильно введено число");
            }
        }

        public void ParseFiles()
        {
            int i = 0;
            using (TextFieldParser parser = new TextFieldParser("Файл csv.csv"))
            {
                using (StreamWriter writetext = new StreamWriter("write.txt"))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(";");
                    while (!parser.EndOfData)
                    {
                        //Process row
                        string[] fields = parser.ReadFields();
                        foreach (string field in fields)
                        {
                            writetext.WriteLine(field + "    ");
                            Thread.Sleep(1);
                        }
                        i++;
                        Dispatcher.Invoke(() => progressBar.Value = i);
                        //progressBar.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new DispatcherOperationCallback(delegate
                        // {
                        //    System.Diagnostics.Debug.WriteLine("value is: " + progressBar.Value);
                        //    progressBar.Value = progressBar.Value + 1;
                        //   return null;
                        //   }), null);
                    }
                    MessageBox.Show("Парсинг завершен");
                    Dispatcher.Invoke(() => progressBar.Value = 0);
                }
            }
        }
        public int Max = 0;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Thread t1 = new Thread(() =>
            {
                Dispatcher.Invoke(() => progressBar.IsIndeterminate = true);
                Max = File.ReadAllLines("Файл csv.csv").Count();
                Thread.Sleep(3000);
                Dispatcher.Invoke(() =>
                {
                    progressBar.Maximum = Max;
                    progressBar.IsIndeterminate = false;
                });
            });
            t1.Start();

            Thread t2 = new Thread(ParseFiles);
            t2.Start();
        }

        private void ButtonMatrix_Click(object sender, RoutedEventArgs e)
        {
            Task t = new Task(() =>
            {
                int[,] matrix1 = new int[100, 100];
                matrix1 = FillMatrix(matrix1);
                int[,] matrix2 = new int[100, 100];
                matrix2 = FillMatrix(matrix2);
                int[,] matrix3 = new int[100, 100];

                Parallel.For(0, 100, i =>
              {
                  for (int j = 0; j < 100; j++)
                  {
                      matrix3[i, j] = 0;
                      for (int k = 0; k < 100; k++)
                      {
                          matrix3[i, j] += matrix3[i, k] * matrix3[k, j];
                      }
                      Thread.Sleep(1);
                  }

              });
                MessageBox.Show("Перемножение окончено");

            }
            );
            t.Start();


        }
        public int[,] FillMatrix(int[,] matrix)
        {
            Random r = new Random();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = r.Next(0, 11);
                }
            }
            return matrix;


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Random r = new Random();
            //for (int i = 0; i < 10000; i++)
            //{
            //    string path = @"E:\Рабочий стол\ДЗ 5 и 6\ДЗ 5 и 6\Directory\textfile" + i + ".txt";
            //    System.IO.StreamWriter textFile = new System.IO.StreamWriter(path);
            //    textFile.WriteLine(r.Next(1, 3) + " " + r.NextDouble() + " " + r.NextDouble());
            //    textFile.Close();
            //}
            string pathDat = @"E:\Рабочий стол\ДЗ 5 и 6\ДЗ 5 и 6\Directory\aaa.txt";
           
            TextWriter tw = TextWriter.Synchronized(new StreamWriter(pathDat, true));
            Parallel.For(0, 10000, i =>
            {
                string path = @"E:\Рабочий стол\ДЗ 5 и 6\ДЗ 5 и 6\Directory\textfile" + i + ".txt";
                StreamReader textfile = new StreamReader(path);
                string s = textfile.ReadLine(); string[] ss = s.Split(' ');
                double result = 0;
                if (int.Parse(ss[0]) == 1)
                {
                    result = double.Parse(ss[1]) * double.Parse(ss[2]);
                }
                else { result = double.Parse(ss[1]) / double.Parse(ss[2]); }
                //textFiledat.WriteLine("Результат вычисления из файла " + i + " равно " + result);
               
                tw.WriteLine("Результат вычисления из файла " + i + " равно " + result);
                tw.Flush();

            }
            );
        }
    }
}
