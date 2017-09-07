using System;
using System.Threading;

namespace SemaphoreApp
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 1; i < 6; i++)
            {
                Reader reader = new Reader(i);
            }

            Console.ReadLine();
        }
    }

    class Reader
    {
        // создаем семафор
        static Semaphore sem = new Semaphore(1, 1);
        Thread myThread;
        int count = 1;// счетчик чтения

        public Reader(int i)
        {
            myThread = new Thread(Read);
            myThread.Name = "Покупатель " + i.ToString();
            myThread.Start();
        }

        public void Read()
        {            
            while (count > 0)
            {
                sem.WaitOne();

                Console.WriteLine("{0} входит в магазин", Thread.CurrentThread.Name);

                Console.WriteLine("{0} делает покупки", Thread.CurrentThread.Name);
                Thread.Sleep(1000);

                Console.WriteLine("{0} покидает магазин", Thread.CurrentThread.Name);

                sem.Release();

                count--;
                Thread.Sleep(1000);
            }
        }
    }
}