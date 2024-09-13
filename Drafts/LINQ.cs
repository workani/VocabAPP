using System;
using System.Collections;
using System.Security.Cryptography; // Import the necessary namespace for using generic lists

namespace Classes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();

            List<Phone> phones = new List<Phone>
            {
                new Phone { Brand = "Apple", Model = "iPhone 13", IMEI = "123456789012345", ReleaseYear = 2021, Price = 999 },
                new Phone { Brand = "Apple", Model = "iPhone 12", IMEI = "987654321098765", ReleaseYear = 2020, Price = 799 },
                new Phone { Brand = "Apple", Model = "iPhone SE", IMEI = "456789012345678", ReleaseYear = 2020, Price = 399 },
                new Phone { Brand = "Samsung", Model = "Galaxy S21", IMEI = "234567890123456", ReleaseYear = 2021, Price = 899 },
                new Phone { Brand = "Google", Model = "Pixel 6", IMEI = "678901234567890", ReleaseYear = 2021, Price = 599 },
                new Phone { Brand = "Samsung", Model = "Galaxy A52", IMEI = "345678901234567", ReleaseYear = 2021, Price = 499 },
                new Phone { Brand = "Samsung", Model = "Galaxy Note 20", IMEI = "890123456789012", ReleaseYear = 2021, Price = 1099 }
            };

            /* LINQ Method 

            // Where()
            var oldPhones = phones.Where(p => p.ReleaseYear == 2021);
            foreach (var phone in oldPhones)
            {
                Console.WriteLine(phone.Model);
            }
            // First()
            var applePhone = phones.First(p => p.Brand == "Apple" && p.ReleaseYear == 2020);
            Console.WriteLine(applePhone.Model);
        
            
            // TrueForAll() bool
            
            Console.WriteLine(phones.TrueForAll(p => p.ReleaseYear == 2021));
            
            // ForEach() content of the list
            phones.ForEach(p => Console.WriteLine($"{p.Model} {p.ReleaseYear}"));
            
            // Exists() bool
            var exists = phones.Exists(p => p.Model == "iPhone 12");
            Console.WriteLine(exists);
            
            
            // Sum() int
            var sum = phones.Sum(p => p.Price);
            Console.WriteLine(sum);
            
            
            // GetType()

            var type = phones.GetType();
            Console.WriteLine(type);
            */
            
            // Anonymus type

            var newApplePhones = from phone in phones
                where phone.ReleaseYear == 2021
                      && phone.Brand == "Apple"
                select new { phone.Model, phone.Price };
          
            foreach (var newApplePhone in newApplePhones)
            {
                Console.WriteLine($"{newApplePhone.Model}, {newApplePhone.Price}");
            }

        }
    }

    // Define the Phone class
    class Phone
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string IMEI { get; set; }
        public int ReleaseYear { get; set; } // Added a ReleaseYear property
        public int Price { get; set; } // Added an integer Price property
    }
}
