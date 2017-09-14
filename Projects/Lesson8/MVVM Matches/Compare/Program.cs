namespace AsyncVsTaskDemo
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal class TestMethods
    {
        private const int _maxTasks = 100;
        private readonly object _lock = new object();
        private readonly List<int> _loggedThreads = new List<int>();

        public Tuple<long, int> ExecuteParallelTest()
        {
            this._loggedThreads.Clear();

            var sw = new Stopwatch();
            sw.Restart();

            Parallel.For(0, _maxTasks, this.LongRunningTask);

            sw.Stop();
            return new Tuple<long, int>(sw.ElapsedMilliseconds, this._loggedThreads.Count);
        }

        public Tuple<long, int> ExecuteTaskTest()
        {
            this._loggedThreads.Clear();

            var tasks = new List<Task>();

            var sw = new Stopwatch();
            sw.Restart();

            for (var i = 0; i < _maxTasks; i++)
            {
                var closure = i;
                tasks.Add(Task.Run(() => this.LongRunningTask(closure)));
            }
            Task.WaitAll(tasks.ToArray());

            sw.Stop();

            return new Tuple<long, int>(sw.ElapsedMilliseconds, this._loggedThreads.Count);
        }

        public Tuple<long, int> ExecuteAwaitTest()
        {
            this._loggedThreads.Clear();

            var tasks = new List<Task>();

            var sw = new Stopwatch();
            sw.Restart();

            for (var i = 0; i < _maxTasks; i++)
                tasks.Add(this.LongRunningAsync());
            Task.WaitAll(tasks.ToArray());

            sw.Stop();

            return new Tuple<long, int>(sw.ElapsedMilliseconds, this._loggedThreads.Count);
        }

        private void LongRunningTask(int i)
        {
            this.LogCurrentThread();
            Thread.Sleep(100);
        }

        private async Task LongRunningAsync()
        {
            this.LogCurrentThread();
            await Task.Delay(100);
        }

        private void LogCurrentThread()
        {
            lock (this._lock)
            {
                var threadId = Thread.CurrentThread.ManagedThreadId;
                if (this._loggedThreads.All(tid => tid != threadId))
                    this._loggedThreads.Add(threadId);
            }
        }
    }
}

namespace AsyncVsTaskDemo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        private static readonly TestMethods _tests = new TestMethods();

        static void Main(string[] args)
        {
            Program.RunTest("await", _tests.ExecuteAwaitTest);
            Program.RunTest("Parallel.For", _tests.ExecuteParallelTest);
            Program.RunTest("Task.Run", _tests.ExecuteTaskTest);

            Console.ReadKey(true);
        }

        static void RunTest(string testName, Func<Tuple<long, int>> testMethod)
        {
            const int testLoops = 10;

            var elapsedTime = new List<long>();
            var threadCount = new List<int>();

            for (var i = 0; i < testLoops; i++)
            {
                var result = testMethod();
                elapsedTime.Add(result.Item1);
                threadCount.Add(result.Item2);
            }

            Console.WriteLine("{0} average time: {1} ms", testName, elapsedTime.Average());
            Console.WriteLine("Thread count: min {0} – max {1}",
                threadCount.Min(),
                threadCount.Max());
        }
    }
}