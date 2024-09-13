// template
using System;

namespace UserMenu
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RideApp rideApp = new RideApp();
            rideApp.RunApp();
        }
    }

    class RideApp
    {
        private int RidesCount { get; }
        private Dictionary<string, double> Rides { get; set; } = new Dictionary<string, double>();

        
        public void RunApp()
        {
            GreetUser();
        }

        private void GreetUser()
        {
            Console.WriteLine("Hi, my application will help you save all of your bike rides in a txt file.");
        }
        

        // get number from user and handle possible execptions
        private int GetNumber(string prompt)
        {
            int result;

            while (true)
            {
                try
                {
                    Console.Write(prompt);
                    result = Convert.ToInt32(Console.ReadLine());
                    return result;
                }
                catch (FormatException)
                {
                    Console.WriteLine("\u001b[31mPlease, type a valid number!\u001b[0m");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("\u001b[31mProvided number is too large or too small!\u001b[0m");
                }

            }
        }
        
        private double GetRealNumber(string prompt)
        {
            double result;

            while (true)
            {
                try
                {
                    Console.Write(prompt);
                    result = Convert.ToDouble(Console.ReadLine());
                    return result;
                }
                catch (FormatException)
                {
                    Console.WriteLine("\u001b[31mPlease, type a valid number!\u001b[0m");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("\u001b[31mProvided number is too large or too small!\u001b[0m");
                }

            }
        }
        
        private void GetRide()
        {
            int currentRide = 0;
            string rideDate;
            double rideRange;

            for (int i = 0; i < RidesCount; i++)
            {
                Console.Clear();
                
                Console.WriteLine($"Ride \u2116{i}");
                
                Console.Write("Date: ");
                rideDate = Console.ReadLine();
                
                Console.Write("");
                rideRange = GetRealNumber("Range: ");
                
                // save user's ride in Dctionary 
                Rides.Add(rideDate, rideRange);
            }
        }

        private string GetFileName()
        {
            Console.WriteLine("How do you want to name your file?");
            return Console.ReadLine();
        }
        

        private void SaveRides()
        {
            string fileName = GetFileName();
            int currentRide = 0;
            
            foreach (var ride in Rides)
            {
                File.AppendText(fileName, $"Ride \u2116{currentRide}");
                Fille
            }
        }
        

        // automatically assign value to RidesCount
        public RideApp()
        {
            RidesCount = GetNumber("How many rides do you want to add?: ");
        }
        
    }
    
}