namespace ValgfagPortfolio.Services;

public class MarkdownFileService : IMarkdownFileService
{
    public async Task WriteFileAsync(string path, string content)
    {
        var streamwriter = new StreamWriter(path);
        await streamwriter.WriteLineAsync(content);
        await streamwriter.FlushAsync();
    }

    public async Task<string> ReadFileAsync(string path)
    {
        var streamReader = new StreamReader(path);
        var content = await streamReader.ReadToEndAsync();
        return content;
    }
}