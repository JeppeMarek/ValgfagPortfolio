namespace ValgfagPortfolio.Services;

public interface IMarkdownFileService
{
    Task<string> ReadFileAsync(string path);
    Task WriteFileAsync(string path, string content);
}