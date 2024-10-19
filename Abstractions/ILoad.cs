namespace VocabAPP.Abstractions;

public interface ILoad
{
    Dictionary<string, string> Load(string filePath);
    Dictionary<string, string> Load(string sourceLanguage, string targetLanguage);
}