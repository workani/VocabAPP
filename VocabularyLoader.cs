namespace VocabAPP;

public class VocabularyLoader : ILoad
{
    private string FilePath = "";
    
    public Dictionary<string, string> Load(string filePath)
    {
        FilePath = filePath;
        PopulateDictionary();
        return ShuffleVocabulary(PopulateDictionary());
    }
    
    public Dictionary<string, string> Load(string sourceLanguage, string targetLanguage)
    {
        var vocabularyGenerator = new GenerateVocabulary(sourceLanguage, targetLanguage);
        
        // generate vocabulary file and save path to it
        
        FilePath = vocabularyGenerator.Generate();
        
        PopulateDictionary();
        
        return ShuffleVocabulary(PopulateDictionary());
    }

    
    private Dictionary<string, string> PopulateDictionary()
    {
        // save content of user's file 
        string fileContent = File.ReadAllText(FilePath);

        // create a dictionary from user's file and populate it with english and german words respectively 
        return fileContent
            .Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(w => w.Split(';'))
            .DistinctBy(w => w[0]) // skip all duplicates
            .ToDictionary(w => w[0], w => w[1]);
        
    }

    // shuffle user's vocabulary 
    private Dictionary<string, string> ShuffleVocabulary (Dictionary<string, string> unshuffledVocabulary)
    {
        return unshuffledVocabulary
                .OrderBy(x => Random.Shared.Next())
               .ToDictionary(w => w.Key, w => w.Value);
    }
}


