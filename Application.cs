
using System.Diagnostics;
using System.Drawing;

namespace VocabAPP;

public class Application
{
    private readonly ILoad _load;

    private readonly Stopwatch _stopwatch;

    private AppState _appState;

    public Application(ILoad load)
    {
        _load = load;
        _appState = new AppState();
        _stopwatch = new Stopwatch();
    }

    public void RunApp()
    {
        PrintWelcomeMessage();
        
        GetUserLanguage();
        
        LoadVocabulary();
        
        GetLinesCount();
        
        PrintDictionary();
        
        _stopwatch.Start();
        HandleInput();
        _stopwatch.Stop();
    }

    private void PrintWelcomeMessage()
    {
        Console.Clear();
        Console.WriteLine("+------------------------------Welcome-----------------------------+");
        Console.WriteLine("With this app, you can learn new vocabulary from foreign languages.");
        Console.WriteLine("You can use your own vocabulary file or let the program generate it.");
        Console.WriteLine("by @workani.");
        Console.WriteLine("+------------------------------------------------------------------+");

    }

    private void GetUserLanguage()
    {
        var sourceLanguage =  Input.GetString("Your native language: ");
        var targetLanguage =  Input.GetString("Language that you want to learn: ");
        
        // Force first letter to uppercase 
        _appState.SourceLanguage = char.ToUpper(sourceLanguage[0]) + sourceLanguage.Substring(1);
        _appState.TargetLanguage = char.ToUpper(targetLanguage[0]) + targetLanguage.Substring(1);
    }

    private void LoadVocabulary()
    {
        if (Input.GetAgreement("Load your own csv vocabulary file?(y/n): "))
        {
            var path = Input.GetFilePath("Path: ");
            _appState.Vocabulary = _load.Load(path);    
        }
        else
        { 
            _appState.Vocabulary = _load.Load(_appState.SourceLanguage, _appState.TargetLanguage);    
        }
    }

    private void PrintDictionary()
    {
        Console.WriteLine("Take your time to learn the Vocabulary set:\n");
    
        // Print the table header
        Console.WriteLine($"{_appState.SourceLanguage,-20} | {_appState.TargetLanguage,-20}");
        Console.WriteLine(new string('-', 43));

        // Print each vocabulary entry
        foreach (var entry in _appState.Vocabulary)
        {
            Console.WriteLine($"{entry.Key,-20} | {entry.Value,-20}");
        }

        Console.WriteLine("\nPress any key to jump into practice...");
        Console.ReadLine();
    }



    private void GetLinesCount()
    {
        _appState.LinesCount = _appState.Vocabulary.Count();
    }
    
    
    private string GetUserAnswer(string wordToTranslate, int wordCount)
    {
        Console.Clear();
        Console.WriteLine($"(Word {wordCount}/{_appState.LinesCount})", Color.DarkMagenta);
        Console.WriteLine($"Type {_appState.TargetLanguage} translation for \"{wordToTranslate}\":");

        // get user input and force it to lower case
        return Input.GetString("-> ").ToLower();
    }

    // ask user for vocabulary and check if input is correct
    private void HandleInput()
    {
        int currentWord = 1;
        string userAnswer;

        foreach (var vocab in _appState.Vocabulary)
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

        PrintResults(_appState.CorrectAnswers.Count(), _appState.IncorrectAnswers.Count(), _appState.LinesCount);
    }

    // print out message and store user's correct answer
    private void HandleCorrectAnswer(string userAnswer, string sourceWord)
    {
        Console.Clear();
        Console.WriteLine("Correct!", Color.Green);
        _appState.CorrectAnswers.TryAdd(userAnswer, sourceWord);
        Thread.Sleep(1000);
    }


    private void HandleIncorrectAnswer(string userAnswer, string targetWord)
    {
        Console.Clear();
        Console.WriteLine("Incorrect :(", Color.Red);
        _appState.IncorrectAnswers.TryAdd(userAnswer, targetWord);
        Thread.Sleep(1000);
    }
    
    private void PrintResults(int correctAnswers, int incorrectAnswers, int totalWords)
    {
        Console.Clear();
        Console.WriteLine("+------------------------------End of the session-----------------------------+");
        Console.WriteLine($"Total score: {correctAnswers}/{totalWords}", Color.Magenta);
        Console.WriteLine($"Correct answers: {correctAnswers}", Color.Green);
        Console.WriteLine($"Incorrect answers: {incorrectAnswers}", Color.Red);
        Console.WriteLine($"Time: {_stopwatch.Elapsed.TotalMinutes:0.00} minutes", Color.Chartreuse);
        Console.WriteLine("+-----------------------------------------------------------------------------+");
    }

    
    // store all important info
    public class AppState
    {
        public int LinesCount { get; set; } = 0;
        public string FilePath { get; set; }
        public string SourceLanguage { get; set; }
        public string TargetLanguage { get; set; }

        // Store shuffled vocabulary
        public Dictionary<string, string> Vocabulary { get; set; }

        // Store correct and incorrect answers
        public Dictionary<string, string> CorrectAnswers { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> IncorrectAnswers { get; set; } = new Dictionary<string, string>();
    }
}