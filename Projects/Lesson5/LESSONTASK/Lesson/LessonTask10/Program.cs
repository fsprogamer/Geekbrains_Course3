using System;
using System.Threading;

class Program
{
    static AutoResetEvent waitHandler = new AutoResetEvent(true);
    static void Main()
    {
        for (int i = 0; i < 5; i++)
        {
            Thread thread = new Thread(ThreadMethod) { Name = i.ToString() };
            thread.Start();
        }

        Console.ReadLine();
    }
    static void ThreadMethod()
    {
        waitHandler.WaitOne();

        int value = 0;

        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine("Поток {0}: {1}", Thread.CurrentThread.Name, value);
            value++;
            Thread.Sleep(200);
        }
        
        waitHandler.Set();
    }
}
