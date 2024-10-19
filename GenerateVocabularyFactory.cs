namespace VocabAPP;

public class GenerateVocabularyFactory 
{
    public static IGenerate Create()
    {
        return new GenerateVocabulary();
    }
}