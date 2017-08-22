using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskFactoryTest
{
    class Program
    {
        static void Main()
        {
            Task[] tasksArr = new Task[10];

            for (int i = 0; i < tasksArr.Length; i++)
            {
                tasksArr[i] = Task.Factory.StartNew(TaskMethod);
            }
            Console.WriteLine("Ждем окончания работы задачи.");
            Task.WaitAll(tasksArr);
            Console.ReadKey();
        }
        static void TaskMethod()
        {
            Console.WriteLine($"Задача {Task.CurrentId} выполняется.");
            Thread.Sleep(2000);
            Console.WriteLine($"Задача {Task.CurrentId} завершена.");
        }

    }
}
