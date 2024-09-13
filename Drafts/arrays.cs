// arrays
using System;

namespace Arrays
{
    internal class Program
    { 
        static void Main(string[] args)
        {
            int[] numbers = new Int32[]
            {
                1, 2, 4, 3, 5, 3, 9, 8
            };
    
            
            // print out array without changes
            Console.Write("Numbers array: ");
            for (int i = 0, length = numbers.Length; i < length; i++)
            {
                Console.Write("{0} ", numbers[i]);
            }
            Console.WriteLine();
            
            // reverse array
            Array.Reverse(numbers);
            
            // print out reversed array
            Console.Write("Reversed numbers array: ");
            for (int i = 0, length = numbers.Length; i < length; i++)
            {
                Console.Write("{0} ", numbers[i]);
            }
            Console.WriteLine();

            // Reverse a string
            Console.Write("Type out some text: ");
            string userText = Console.ReadLine();

            char[] reversedString = userText.ToCharArray();
            string result = String.Empty;

            Array.Reverse(reversedString);

            for (int i = 0, length = reversedString.Length; i < length; i++)
            {
                result += reversedString[i];
            }
            
            // print out result
            Console.WriteLine("Your reversed text: {0}", result);
        }
    }
}