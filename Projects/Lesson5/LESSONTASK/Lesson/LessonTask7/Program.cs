using System;
using System.Threading;
class Program
{
    private static object lockObject = new object();
    static void ThreadMethod(object state)
    {
        lock (lockObject)
        {
            int value = 0;

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Поток {0}: {1}", Thread.CurrentThread.Name, value);
                value++;
                Thread.Sleep(200);
            }
        }
    }
    static void Main()
    {
        ThreadPool.SetMinThreads(2, 2);
        ThreadPool.SetMaxThreads(5, 5);
        for (int i = 0; i < 5; i++)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadMethod));
        }
        Console.ReadLine();
    }   
}
