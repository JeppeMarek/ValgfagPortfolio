namespace ValgfagPortfolio.Services;

public interface IImageService
{
    Task<string> UploadImageAsync(IFormFile file, string id, string type);
    Task DeleteImageAsync(string filePath);
    Task<string> UpdateImageAsync(IFormFile file, string id, string type, string oldImagePath);
}