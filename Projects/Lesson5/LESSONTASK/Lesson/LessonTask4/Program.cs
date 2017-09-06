using System;
using System.Threading;

class Program
{
    public class ThreadClass
    {
        private int _sleepTime;
        private string _message;
        public ThreadClass(int sleepTime, string message)
        {
            _sleepTime = sleepTime;
            _message = message;
        }
        public void ThreadClassMethod()
        {
            Thread.Sleep(_sleepTime);
            Console.WriteLine(_message);
        }
    }
    static void Main(string[] args)
    {
        ThreadClass threadClass = new ThreadClass(2000, "Поток завершен.");
        Thread thread = new Thread(new ThreadStart(threadClass.ThreadClassMethod))
        {
            Priority = ThreadPriority.Highest,
            Name = "Вторичный поток"
        };
        thread.Start();
        Console.WriteLine("Ждем окончания работы потока.");
    }   
}

