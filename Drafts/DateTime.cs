using System;

namespace DateTime
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
            Console.Write("Type your birthday full date: ");

            DateTime userBirthday = DateTime.Parse(Console.ReadLine());

            TimeSpan userAge = DateTime.Now.Subtract(userBirthday);
            
            Console.WriteLine($"You are alive for {userAge.TotalDays} days or {userAge.TotalHours} hours or {userAge.TotalMinutes} minutes.");
        }
    }
}