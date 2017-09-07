using System;
using System.Diagnostics;
using System.Threading;
class Program
{
    static void Main()
    {
        var stopwatch = Stopwatch.StartNew();
        
        Thread[] array = new Thread[4];
        for (int i = 0; i < array.Length; i++)
        {        
            array[i] = new Thread(new ThreadStart(Start)) { Name = i.ToString() };
            array[i].Start();
        }
        // Join all the threads.
        for (int i = 0; i < array.Length; i++)
        {
            array[i].Join();
        }
        Console.WriteLine("DONE: {0}", stopwatch.ElapsedMilliseconds);
        Console.ReadLine();
    }

    static void Start()
    {
        Console.WriteLine("Поток {0}", Thread.CurrentThread.Name);
        // This method takes ten seconds to terminate.
        Thread.Sleep(10000);
    }
}