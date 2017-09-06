using System;
using System.Threading;
class Program
{
    static void Main()
    {
        int sleepTime = 2000; // величина приостановки потока
        Thread thread = new Thread(new ParameterizedThreadStart(ThreadMethod));
        thread.Name = "Вторичный поток";
        thread.Start(sleepTime);

        Console.WriteLine("Ждем окончания работы потока.");
    }
    static void ThreadMethod(object sleepTime) //принимает в качестве параметра один объект типа object
    {
        Thread.Sleep((int)sleepTime);
        Console.WriteLine($"{Thread.CurrentThread.Name} завершен.");
    }
}

