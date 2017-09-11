using System;
using System.Threading;

public class ThreadClass
{
    public int SleepTime { get; set; }
    public string Message { get; set; }
}
//class Program
//{
//    static void Main()
//    {
//        ThreadClass threadClass = new ThreadClass
//        {
//            SleepTime = 2000,
//            Message = "Поток завершен."
//        };
//        Thread thread = new Thread(new ParameterizedThreadStart(ThreadMethod))
//        {
//            Priority = ThreadPriority.Highest,
//            Name = "Вторичный поток"
//        };
//        thread.Start(threadClass);
//        Console.WriteLine("Ждем окончания работы потока.");
//    }
//    static void ThreadMethod(object obj)
//    {
//        ThreadClass threadClass = (ThreadClass)obj;
//        Thread.Sleep(threadClass.SleepTime);
//        Console.WriteLine(threadClass.Message);
//    }
//}

class Program
{
    static void Main()
    {
        ThreadClass threadClass = new ThreadClass
        {
            SleepTime = 2000,
            Message = "Поток завершен."
        };
        Thread thread = new Thread(new ParameterizedThreadStart((object obj)=> {
            ThreadClass tClass = (ThreadClass)obj;
            Thread.Sleep(tClass.SleepTime);
            Console.WriteLine(tClass.Message);
        }))
        {
            Priority = ThreadPriority.Highest,
            Name = "Вторичный поток"
        };
        thread.Start(threadClass);
        Console.WriteLine("Ждем окончания работы потока.");
    }  
}

