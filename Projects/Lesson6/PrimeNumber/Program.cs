using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PrimeNumber
{
    class Program
    {
        static List<int> primes = new List<int>();
        static void Main(string[] args)
        {
            FindPrime(1, 100);

            foreach (int i in primes)
                Console.WriteLine(i);
        }

        static void FindPrime(int start, int end)
        {

            //for (int number = start; number < end; ++number)
            //{
            //    if (IsPrime(number))
            //    {
            //        primes.Add(number);
            //    }
            //}

            Parallel.For(start, end, number =>
            {
                if (IsPrime(number))
                {
                    lock (primes)
                    {
                        primes.Add(number);
                    }
                }
            });
        }

        static bool IsPrime(int number)
        {

            if (number == 1) return false;
            if (number == 2) return true;

            if (number % 2 == 0) return false; // Even number     

            for (int i = 2; i < number; i++)
            { // Advance from two to include correct calculation for '4'
                if (number % i == 0) return false;
            }

            return true;
        }
    }
}
