using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelTest
{
    class Program
    {
        static void Main()
        {
            Parallel.Invoke(
                ParallelMethod,
                ParallelMethod,
                ParallelMethod,
                () =>
                {
                    Console.WriteLine("Task: {0} start.", Task.CurrentId);
                    Thread.Sleep(5000);
                    Console.WriteLine("Task: {0} done.", Task.CurrentId);
                });

            Console.ReadKey();
        }
        static void ParallelMethod()
        {
            Console.WriteLine("Task: {0} start.", Task.CurrentId);
            Thread.Sleep(5000);
            Console.WriteLine("Task: {0} done.", Task.CurrentId);

        }
    }
}
