namespace VocabAPP.Abstractions;

public interface ILoad
{
    Dictionary<string, string> Load();
    Dictionary<string, string> Load(string filePath);
}