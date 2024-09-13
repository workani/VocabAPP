using System;

namespace personalJournal
{
    internal class Program
    {
        // static void Main(string[] args)
        // {
        //     int firstValue = 500;
        //     int secondValue = 600;
        //     int largerValue = Math.Max(firstValue, secondValue);
        //
        //     
        //     Console.WriteLine(largerValue);
        // }

        static void Main(string[] args)
        {
            int firstValue = 500;
            int secondValue = 600;
            int largerValue = MaxValue(firstValue, secondValue);

            Console.WriteLine(largerValue);
        }

        static int MaxValue(int a, int b)
        {
            if (a > b)
                return a;
            else
                return b;
        }
        
    }
}   