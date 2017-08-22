using System.Threading;

namespace ДЗ_5_и_6
{
    public class FactorialAndSum
    {
        int number;
        public int Number {
            get { return number; }
            set { number = value; }
       }

        public static long Factorial(int number)
        {
            long result = 1;
            for (int i = 1; i <= number; i++)
                result = result* i;
            Thread.Sleep(2000);
            return result;
        }

        public static long Sum(int number)
        {
            long sum = 0;
            for (long i = 1; i <= number; i++)
                sum += i;
            Thread.Sleep(2000);
            return sum;
        }
    }
}
