using System;
using System.IO;
using System.Text;

namespace ConsoleIDispose
{
    class Program
    {
        static void Main(string[] args)
        {

            //using (FileStream fs = new FileStream("myFile.txt", FileMode.Open))
            //{
            //    //...
            //}
            // Преобразуется компилятором в:
            //FileStream fs = new FileStream("myFile.txt", FileMode.Open);
            //try
            //{
            //    //...
            //}
            //finally
            //{
            //    if (fs != null) ((IDisposable)fs).Dispose();
            //}


            //public sealed class HouseManager : IDisposable
            //{
            //    public readonly bool CheckMailOnDispose;
            //    public Demo(bool checkMailOnDispose)
            //    {
            //        CheckMailOnDispose = checkMailOnDispose;
            //    }
            //    public void Dispose()
            //    {
            //        if (CheckMailOnDispose) CheckTheMail();
            //        LockTheHouse();
            //    }
            //}

            //GC.Collect();
            //GC.WaitForPendingFinalizers();
            //GC.Collect();

            var weak = new WeakReference(new StringBuilder("weak"));
            Console.WriteLine(weak.Target); // weak
            GC.Collect();
            Console.WriteLine(weak.Target); // null
        }
    }
}
