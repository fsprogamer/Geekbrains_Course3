using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskTest
{
    class Program
    {
        static void Main()
        {
            Task task = new Task(TaskMethod);
            task.Start();
            Console.WriteLine("Ждем окончания работы задачи.");
            task.Wait();
            Console.ReadKey();
        }
        static void TaskMethod()
        {
            Console.WriteLine("Задача {0} выполняется.", Task.CurrentId);
            Thread.Sleep(2000);
            Console.WriteLine("Задача {0} завершена.", Task.CurrentId);
        }
    }
}
