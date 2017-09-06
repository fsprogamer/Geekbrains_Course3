using System;
using System.Runtime.Remoting.Contexts;
using System.Threading;

class Program
{
    static void Main()
    {
        ClassWithSynchronizationAttribute obj = new ClassWithSynchronizationAttribute();
        for (int i = 0; i < 10; i++)
        {
            Thread thread = new Thread(obj.ThreadMethod) { Name = i.ToString() };
            thread.Start();
        }

        Console.ReadLine();
    }
    public class ClassWithSynchronizationAttribute
    //[Synchronization]
    //public class ClassWithSynchronizationAttribute : ContextBoundObject
    {
        private int _value;
        public void ThreadMethod()
        {
            Console.WriteLine("  Поток {0}: _value = {1}", Thread.CurrentThread.Name, _value);
            _value++;
            Thread.Sleep(2000);
        }
    }
}

