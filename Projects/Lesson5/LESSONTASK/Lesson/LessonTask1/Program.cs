using System;
using System.Threading;

class Program
{
    static void Main()
    {
        Thread thread = new Thread(new ThreadStart(ThreadMethod));
        thread.Name = "Вторичный поток";
        //thread.Priority = ThreadPriority.Normal;
        thread.Start();
        Console.WriteLine("Ждём окончания работы потока.");
        Console.ReadKey();
    }
    static void ThreadMethod()
    {
        Thread.Sleep(2000);
        Console.WriteLine($"{Thread.CurrentThread.Name} завершён.");
    }

}

