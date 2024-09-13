
using System;

namespace UserMenu
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Greetings!");
                Console.Write("Type first number: ");
                int firstNumber = Convert.ToInt32(Console.ReadLine());
            
                Console.Write("Type second number: ");
                int secondNumber = Convert.ToInt32(Console.ReadLine());
            
            
                double result = (double)firstNumber / secondNumber;
                Console.WriteLine("Your result is " + result);
                Thread.Sleep(1000);

            }
            
        }
    }
}