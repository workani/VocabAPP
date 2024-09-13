// template
using System;
using System.Text;

namespace UserMenu
{
    internal class Program
    {
        
        private static Random NumberGenerator = new Random();
        
        static void Main(string[] args)
        {
            StringBuilder phoneNumbers = new StringBuilder();
            Random phoneNumberGenerator = new Random();

            for (int i = 0; i < 10000; i++)
            {
                string operatorCode = PickOpertaorCode();
                int phoneNumber = phoneNumberGenerator.Next(0, 10000000);
                phoneNumbers.AppendFormat("+49{0}{1}\n", operatorCode,  phoneNumber);
                Console.WriteLine(phoneNumbers);
            }
           
            
        }
        
        // pick randomly one german carrier code, e. 151
        static string PickOpertaorCode()
        {
            string choosedCode = String.Empty;
            int index = NumberGenerator.Next(0, 16);
            
            string[] operatorCodes = {
                "151", "152", "157", "159",
                "160", "162", "163",
                "170", "171", "172", "173", "174", "175", "176", "177", "178"
            };

            return operatorCodes[index];
        }
        
    }
}