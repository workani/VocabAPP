using DotNetEnv;
using OpenAI.Chat;

namespace VocabAPP
{
    class GenerateVocabulary
{
    // OpenAI client 
    private ChatClient Client;
    // Instance of the class which will store all user's preferences 
    private UserPreference Preference = new UserPreference();
    
    // store generated vocabulary
    private string Vocabulary = "";
    
    private string GetLanguageLevel()
    {
        Console.WriteLine($"Select the language level you want to master:");
        Console.WriteLine("1. A1\t\t2. A2");
        Console.WriteLine("3. B1\t\t4. B2");
        Console.WriteLine("5. C1\t\t6. C2");
        int choice = Input.GetInRangeInt(1, 6, "-> ");

        // Return user's choice 
        return choice switch
        {
            1 => "A1",
            2 => "A2",
            3 => "B1",
            4 => "B2",
            5 => "C1",
            6 => "C2",
            _ => throw new Exception("Incorrect choice!")
        };
    }

    private void GetUserPreference()
    {
        Preference.LanguageLevel = GetLanguageLevel();
        
        Console.Clear();

        // Check if user wants to specify the set's topic and file name
        if (Input.GetAgreement("Do you want to specify topic for vocabulary set(y/n): "))
        {
            Preference.VocabularyTopic = Input.GetString("Topic: ");
        }
        
        Preference.FileName = GenerateFileName();
            
        Console.Clear();
    }

    private string GenerateFileName()
    {
        string fileName = "";

        string timestamp = DateTime.Now.ToString("yyyy.MMdd.HH.mmssfff");
        // If the user specified the set's topic, add it to the file name.
        if (!string.IsNullOrEmpty(Preference.FileName))
        {
            fileName = $"{Preference.LanguageLevel}_{Preference.VocabularyTopic}_{Preference.CurrentDate}_{timestamp}.csv";
        }
        else
        {
            fileName = $"{Preference.LanguageLevel}_{Preference.CurrentDate}_{timestamp}.csv";
        }

        
        
        // Replace all whitespaces with underscores 
        return fileName.Replace(' ', '_');
    }

    private void GenerateVocabularySet()
    {
        string prompt = "";


        if (!string.IsNullOrEmpty(Preference.VocabularyTopic))
        {
            prompt =
                $"You need to generate a set with 20 lines of vocabulary in the following format {Preference.SourceLanguage};{Preference.TargetLanguage}." +
                $" Complexity of vocabulary should be at the {Preference.LanguageLevel} language level. The set's topic is {Preference.VocabularyTopic}." +
                $"In case the language is one where articles are mandatory (e.g., German, French, Spanish, Italian), always include the appropriate articles for all nouns and ensure there is a whitespace after each article." +
                $" DO NOT INCLUDE WHITESPACES OR ANYTHING ELSE OR CHANGE THE FORMAT. IT SHOULD BE PLAIN VOCABULARY WITHOUT ANY MARKDOWN." +
                $"ALWAYS SEPARATE WORDS WITH ;";
            
        }
        else
        {
            prompt = $"You need to generate a set with 20 lines of vocabulary in the following format {Preference.SourceLanguage};{Preference.TargetLanguage}." +
                     $" Complexity of vocabulary should be at the {Preference.LanguageLevel} language level." +
                     $"In case the language is one where articles are mandatory (e.g., German, French, Spanish, Italian), always include the appropriate articles for all nouns and ensure there is a whitespace after each article." +
                     $" DO NOT INCLUDE WHITESPACES OR ANYTHING ELSE OR CHANGE THE FORMAT. IT SHOULD BE PLAIN VOCABULARY WITHOUT ANY MARKDOWN." +
                     $"ALWAYS SEPARATE WORDS WITH ;";
            
        }

        
        // Generate vocabulary for user using gpt-4o, handle exceptions
        try
        {
            ChatCompletion generatedVocabulary = Client.CompleteChat(prompt);
           
            // Convert model's response to string 
            Vocabulary = generatedVocabulary.ToString();
        }
        catch (AggregateException) 
        {
            Program.Exit("\u001b[31mCannot communicate with OpenAI API. Please check your internet connection!\u001b[0m", 0);
        }
        catch (Exception)
        {
            Program.Exit("\u001b[31mSomething went wrong!\u001b[0m", 0);
        }
    }

    
    private void SaveToFile()
    {
        // Save vocabulary to a file 
        Preference.Path = $"Vocabulary/{Preference.TargetLanguage}/{Preference.FileName}";

        Directory.CreateDirectory($"Vocabulary/{Preference.TargetLanguage}");
        
        // File.WriteAllText(Preference.FileName, Vocabulary);
        
        File.WriteAllText(Preference.Path, Vocabulary);
    }
    
    // Initialise OpenAI client and use environment library as API key 
    public GenerateVocabulary(string sourceLanguage, string targetLanguage)
    {
        // Load api key from .env file
        Env.Load();
        
        Client = new ChatClient(model: "gpt-4o-mini", Environment.GetEnvironmentVariable("OPENAI_KEY"));

        Preference.SourceLanguage = sourceLanguage;
        Preference.TargetLanguage = targetLanguage;
    }

    public string Generate()
    {
        Console.Clear();
        GetUserPreference();
        GenerateVocabularySet();
        SaveToFile();

        return Preference.Path;
    }
}
}

// Store information about the user that is important for generating a vocabulary file
class UserPreference
{
    public string CurrentDate { get; } = "";
    public string SourceLanguage { get; set; } = "";
    public string TargetLanguage { get; set; } = "";
    public string LanguageLevel { get; set; } = "";
    public string VocabularyTopic { get; set; } = "";
    public string FileName { get; set; } = "";
    public string Path { get; set; } = "";

    // Automatically save current date
    public UserPreference()
    {
        CurrentDate = Input.GetCurrentFormatedDate("dd.MM");
    }
}



