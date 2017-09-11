using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelForTest
{
    class Program
    {
        static void Main()
        {
            var results = Parallel.For(1, 10, ParallelMethod);
            Console.WriteLine("{0} {1}", results.IsCompleted, (results.LowestBreakIteration==null)?-1: results.LowestBreakIteration);
            Console.ReadKey();
        }
        static void ParallelMethod(int iteration)
        {
            Console.WriteLine("Iteration: {0} start.", iteration);
            Thread.Sleep(1000);
            Console.WriteLine("Iteration: {0} done.", iteration);
        }
    }
}
