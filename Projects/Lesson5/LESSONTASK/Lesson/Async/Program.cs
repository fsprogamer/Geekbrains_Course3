using System;
using System.Threading;
using System.Threading.Tasks;

namespace Async
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 1; i < 10; i++)
            {
                My();
            }

            for (int i = 1; i < 10; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine($"* {i * 1000}");
            }
            Console.ReadLine();
        }

        static async void My()
        {
            string message = await GetMessage(3000);
            Console.WriteLine(message);
        }

        static Task<string> GetMessage(int time)
        {
            return Task.Run(() =>
            {
                Thread.Sleep(time);
                return $"bzzzz...{time.ToString()}";
            });
        }

        //}
        //class Program
        //{
        //    static void Main(string[] args)
        //    {
        //        //GetResultSync();
        //        GetResultAsync();
        //        Console.ReadLine();
        //    }

        //    static void GetResultSync()
        //    {
        //        int num = 5;
        //        int result = MethodSync(num);
        //        Console.WriteLine("Квадрат числа {0} равен {1}", num, result);
        //    }

        //    static int MethodSync(int x)
        //    {
        //        int result = 1;

        //        Thread.Sleep(10000);
        //        result = x * x;
        //        return result;
        //    }

        //    static async void GetResultAsync()
        //    {
        //        int num = 5;

        //        int result = await MethodAsync(num);
        //        Console.WriteLine("Квадрат числа {0} равен {1}", num, result);
        //    }

        //    static Task<int> MethodAsync(int x)
        //    {
        //        int result = 1;

        //        return Task.Run(() =>
        //        {
        //            Thread.Sleep(10000);
        //            result = x * x;
        //            return result;
        //        });
        //    }
    }
}
