using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskMethodWithParametersTest
{
    class Program
    {
        //static void Main()
        //{
        //    int timeSpan = 2000;
        //    Task task1 = new Task(() => TaskMethodWithParameters(timeSpan));
        //    task1.Start();
        //    Task.Factory.StartNew(() => TaskMethodWithParameters(timeSpan));
        //    Console.WriteLine("Ждем окончания работы задачи.");
        //    Console.ReadKey();
        //}
        //static void TaskMethodWithParameters(int timeSpan)
        //{
        //    Console.WriteLine($"Задача {Task.CurrentId} выполняется.");
        //    Console.WriteLine($"Значение timeSpan = {timeSpan} ms.");
        //    Thread.Sleep(timeSpan);
        //    Console.WriteLine($"Задача {Task.CurrentId} завершена.");
        //}

        static void Main()
        {
            int x = 3;
            int y = 2;
            string message = "тестовое сообщение";
            Console.WriteLine("Запуск задач.");
            Task<int> task1 = new Task<int>(() => TaskMethodAdd(x, y));
            task1.Start();
            Task<string> task2 = Task.Factory.StartNew(() => TaskMethodStringToUpper(message));
            int resultAdd = task1.Result;
            string resultStringToUpper = task2.Result;
            Console.WriteLine($"Результат TaskMethodAdd: {resultAdd}");
            Console.WriteLine($"Результат TaskMethodString: {resultStringToUpper}");
            Console.ReadKey();
        }
        static int TaskMethodAdd(int x, int y)
        {
            Thread.Sleep(2000);
            return x + y;
        }
        static string TaskMethodStringToUpper(string message)
        {
            Thread.Sleep(2000);
            return message.ToUpper();
        }


    }
}
