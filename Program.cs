using System;
using System.Reflection.Metadata.Ecma335;

// my custom library for getting user input
using GetUserInput;

namespace VocabAPP
{
    internal class Program
    {
        public static void Main(string[] args)
        {
                VocabApp app = new VocabApp();
                app.RunApp();
        }
    }

    class VocabApp
    {
        private int LinesCount = 0;
        private string FilePath;

        private string UserLanguage;
        
        
        // store shuffled vocabulary
        private Dictionary<string, string> Vocabulary = new Dictionary<string, string>();       

        public void RunApp()
        {
            PrintWelcomeMessage();
            GetUserLanguage();
            PopulateDictionary();
            HandleVocabularyInput();
        }
        

        private void PrintWelcomeMessage()
        {
            Console.Clear();
            Console.WriteLine("+------------------------------Welcome-----------------------------+");
            Console.WriteLine("With this app, you can learn new vocabulary from foreign languages.");
            Console.WriteLine("To start, please provide path to csv file with vocabulary.");
            Console.WriteLine("by @workani.");
            Console.WriteLine("+------------------------------------------------------------------+");
            FilePath = Input.GetFilePath("Path: ");
        }

        private void GetUserLanguage()
        {
            string language = Input.GetString("Which language do you want to learn: ");

            // force first letter of user input to upper case
            UserLanguage = char.ToUpper(language[0]) +  language.Substring(1);
        }

     
        
        private void PopulateDictionary()
        {
            // save content of user's file 
            string fileContent = File.ReadAllText(FilePath);

            // get number of lines in user file
            LinesCount = fileContent.Split("\n", StringSplitOptions.RemoveEmptyEntries).Count();
            
            // create a dictionary from user's file and populate it with english and german words respectively 
            Dictionary<string, string> vocab = fileContent
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
        
        // ask user for vocabulary and check if input is correct
        private void HandleVocabularyInput()
        {
            int currentWord = 1;
            string userAnswer;
            
            foreach (var vocab in Vocabulary)
            {
                Console.Clear();
                
                Console.WriteLine($"(Word {currentWord}/{LinesCount})");
                Console.WriteLine($"Type {UserLanguage} translation for \"{vocab.Key}\":");
                
                userAnswer = Input.GetString("-> ");
                
                currentWord++;
            }
        }
        
        
        
        




    }
    
}