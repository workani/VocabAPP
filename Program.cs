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
        private AppState appState = new AppState();


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
            appState.FilePath = Input.GetFilePath("Path: ");
        }

        private void GetUserLanguage()
        {
            Console.Clear();
            string language = Input.GetString("Which language do you want to learn: ");

            // force first letter of user input to upper case
            appState.UserLanguage = char.ToUpper(language[0]) + language.Substring(1);
        }

        
        private void PopulateDictionary()
        {
            // save content of user's file 
            string fileContent = File.ReadAllText(appState.FilePath);

            // get number of lines in user file
            appState.LinesCount = fileContent.Split("\n", StringSplitOptions.RemoveEmptyEntries).Count();

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
            appState.Vocabulary = unshuffledVocabulary.OrderBy(x => Random.Shared.Next())
                .ToDictionary(w => w.Key, w => w.Value);
        }


        private string GetUserAnswer(string wordToTranslate, int wordCount)
        {
            Console.Clear();
            Console.WriteLine($"\u001b[35m(Word {wordCount}/{appState.LinesCount})\u001b[0m");
            Console.WriteLine($"Type {appState.UserLanguage} translation for \u001b[93m\"{wordToTranslate}\"\u001b[0m:");

            // get user input and force it to lower case
            return Input.GetString("-> ").ToLower();
        }

        // ask user for vocabulary and check if input is correct
        private void HandleVocabularyInput()
        {
            int currentWord = 1;
            string userAnswer;

            foreach (var vocab in appState.Vocabulary)
            {
                userAnswer = GetUserAnswer(vocab.Key, currentWord);

                if (userAnswer == vocab.Value.ToLower())
                    HandleCorrectAnswer(userAnswer, vocab.Value);
                else if (userAnswer == "exit")
                    break;
                else
                    HandleIncorrectAnswer(userAnswer, vocab.Value);

                currentWord++;
                Console.Clear();
            }

            PrintResults(appState.CorrectAnswers.Count(), appState.IncorrectAnswers.Count(), appState.LinesCount);
        }

        // print out message and store user's correct answer
        private void HandleCorrectAnswer(string userAnswer, string sourceWord)
        {
            Console.Clear();
            Console.WriteLine("\u001b[32mCorrect!\u001b[0m");
            appState.CorrectAnswers.Add(userAnswer, sourceWord);
            Thread.Sleep(1000);
        }


        private void HandleIncorrectAnswer(string userAnswer, string targetWord)
        {
            Console.Clear();
            Console.WriteLine("\u001b[31mIncorrect :(\u001b[0m");
            appState.IncorrectAnswers.Add(userAnswer, targetWord);
            Thread.Sleep(1000);
        }

        private void PrintResults(int correctAnswers, int incorrectAnswers, int totalWords)
        {
            Console.Clear();
            Console.WriteLine("+------------------------------End of the session-----------------------------+");
            Console.WriteLine($"\u001b[35mTotal score: {correctAnswers}/{totalWords}\u001b[0m");
            Console.WriteLine($"\u001b[32mCorrect answers: {correctAnswers}\u001b[0m");
            Console.WriteLine($"\u001b[31mIncorrect answers: {incorrectAnswers}\u001b[0m");
            Console.WriteLine("+-----------------------------------------------------------------------------+");
        }
    }


    // store all important info
    class AppState
    {
        public int LinesCount { get; set; } = 0;
        public string FilePath { get; set; }
        public string UserLanguage { get; set; }

        // Store shuffled vocabulary
        public Dictionary<string, string> Vocabulary { get; set; } = new Dictionary<string, string>();

        // Store correct and incorrect answers
        public Dictionary<string, string> CorrectAnswers { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> IncorrectAnswers { get; set; } = new Dictionary<string, string>();
    } 
}