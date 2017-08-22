using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace ParallelForEachTest
{
    class Program
    {
        //static void Main()
        //{
        //    List<int> collection = new List<int>() { 1, 3, 5, 7, 9, 11, 13, 15, 17 };
        //    Parallel.ForEach(collection, ParallelMethod);

        //    Console.ReadKey();
        //}
        //static void ParallelMethod(int item)
        //{
        //    Console.WriteLine("Item: {0}. Task: {1}", item, Task.CurrentId);
        //    Thread.Sleep(1000);
        //}

        static void Main()
        {
            List<int> collection = new List<int>() { 1, 3, 5, 7, 9, 11, 13, 15, 17 };
            ParallelLoopResult state = Parallel.ForEach(collection, ParallelMethod);
            if (!state.IsCompleted)
                Console.WriteLine("Terminated when collection index = {0}", state.LowestBreakIteration);
            Console.ReadKey();
        }
        static void ParallelMethod(int item, ParallelLoopState state)
        {
            Console.WriteLine("Item: {0}. Task: {1}", item, Task.CurrentId);
            if (item == 9)
                state.Break();
            Thread.Sleep(1000);
        }


    }
}
