using System;

namespace personalJournal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filepath = "journal.txt";
            
            // clear out terminal and print out welcome message
            // Console.Clear();
            // Console.WriteLine("Welcome to my script!");
            // Console.WriteLine("It will help you create and mantain your own personal journal.");

            choiceMenu();
        }
        
        static void choiceMenu()
        {
            int choice = 0;
            Console.Clear();
            Console.WriteLine("Please, choose an action:");
            Console.WriteLine("1. Add new note to the journal.");
            Console.WriteLine("2. Print out journal content.");
            Console.WriteLine("3. Quit");
            Console.Write("Your choice: ");
            choice = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            switch (choice)
            {
                case 1:
                    addNote();
                    break;
                case 2:
                    // printJournal();
                    break;
                default:
                    Console.Clear();
                    Environment.Exit(0);
                    break;
            }

            static void addNote()
            {
                char dateChoice;
                string userInput;
                string result;
                string date;
                
                // Console.Write("Do you want to add a date to your note (y/n)?: ");

                // do
                // {
                //     dateChoice = Console.ReadKey().KeyChar;
                //     dateChoice = char.ToLower(dateChoice);
                //
                //     if (dateChoice != 'y' && dateChoice != 'n')
                //     {
                //         Console.WriteLine(@"Please, press 'Y / y' or 'N / n' button");
                //     }
                //
                //     Console.Clear();
                // } while (dateChoice != 'y' && dateChoice != 'n');
                //

                Console.Write("Type out your note: ");
                userInput = Console.ReadLine();
                Console.Clear();
                
          
                Console.Write("Date: ");
                date = Console.ReadLine();
                    // compose user's note together by combining date and user input together
                result = $"{date}: {userInput}";
                
                Console.WriteLine("Preview of your note: ");
                Console.WriteLine(result);

            }
            
            
            
        }
    }
}