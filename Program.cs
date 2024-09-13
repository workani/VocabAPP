using System;
using System.Reflection.Metadata.Ecma335;

namespace VocabAPP
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // while (true)
            // {
                VocabApp app = new VocabApp();
                app.RunApp();
            // }
        }
    }

    class VocabApp
    {
        // private int LinesCount = 0;
        private string FilePath;
        
        // store shuffled vocabulary
        private Dictionary<string, string> Vocabulary = new Dictionary<string, string>();       

        public void RunApp()
        {
            PrintWelcomeMessage();
            GetFilePath();
            PopulateDictionary();
            PrintVocabulary();
        }

        // private void GetLinesCount(string filePath)
        // {
        //     LinesCount = File.ReadAllLines(filePath).Length;
        // }

        private void PrintWelcomeMessage()
        {
            Console.Clear();
            Console.WriteLine("+------------------------------Welcome-----------------------------+");
            Console.WriteLine("With this app, you can learn new vocabulary from foreign languages.");
            Console.WriteLine("To start, please provide path to csv file with vocabulary.");
            Console.WriteLine("by @workani.");
            Console.WriteLine("+------------------------------------------------------------------+");
        }

        private void GetFilePath()
        {
            string path;

            while (true)
            {
                Console.Write("Path: ");
                path = Console.ReadLine();

                if (File.Exists(path))
                {
                    FilePath = path;
                    Console.Clear();
                    return;
                }
                else
                {
                    Console.WriteLine("\u001b[31m Provided path to the file is incorrect. Please, try again!\u001b[0m");
                }
            }
        }

        // create a dictionary from user's file and populate it with english and german words respectively 
        private void PopulateDictionary()
        {
            Dictionary<string, string> vocab = File.ReadAllText(FilePath)
                .Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(w => w.Split(';'))
                .ToDictionary(w => w[0], w => w[1]);

            ShuffleVocabulary(vocab);
        }
        
        // shuffle user's vocabulary 
        private void ShuffleVocabulary(Dictionary<string, string> unshuffledVocabulary)
        {
            Vocabulary = unshuffledVocabulary.OrderBy(x => Random.Shared.Next())
                .ToDictionary(w => w.Key, w => w.Value);
        }

        private void PrintVocabulary()
        {
            foreach (var vocab in Vocabulary)
            {
                Console.WriteLine($"{vocab.Key} | {vocab.Value}");
            }
        }
        
        
        
        




    }
    
}