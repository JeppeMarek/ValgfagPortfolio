using Microsoft.AspNetCore.Components.Forms;

namespace ValgfagPortfolio.Services;

public interface IImageService
{
    Task<string> UploadImageAsync(IBrowserFile file, string folder, CancellationToken
        cancellationToken = default);

    Task DeleteImageAsync(string filePath);
    Task<string> UpdateImageAsync(IFormFile file, string id, string type, string oldImagePath);
}