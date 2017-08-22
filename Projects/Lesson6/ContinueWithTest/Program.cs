using System;
using System.Threading;
using System.Threading.Tasks;

namespace ContinueWithTest
{
    class Program
    {
        //static void Main()
        //{
        //    Task task = new Task(TaskMethod);
        //    task.ContinueWith(TaskContinueWithMethod);
        //    task.Start();
        //    Console.WriteLine("Ждем окончания работы задач.");
        //    Console.ReadKey();
        //}
        //static void TaskMethod()
        //{
        //    Thread.Sleep(2000);
        //    Console.WriteLine("Выполняется TaskMethod метод.");
        //}
        //static void TaskContinueWithMethod(Task task)
        //{
        //    Thread.Sleep(2000);
        //    Console.WriteLine("Выполняется TaskContinueWithMethod метод.");
        //}

        static void Main()
        {
            int x = 3;
            int y = 2;
            Task task = new Task(() => TaskMethod(x, y));
            task.ContinueWith(TaskContinueWithMethod);
            task.Start();
            Console.WriteLine("  Ждем окончания работы задач.");
            Console.ReadKey();
        }
        static int TaskMethod(int x, int y)
        {
            Console.WriteLine("Выполняется TaskMethod метод.");
            Thread.Sleep(2000);
            return x + y;
        }
        static void TaskContinueWithMethod(Task task)
        {
            var taskStatus = task.Status;
            Console.WriteLine("Выполняется TaskContinueWithMethod метод.");
            Console.WriteLine($"Статус TaskMethod метода: {taskStatus}.");
        }

    }
}
