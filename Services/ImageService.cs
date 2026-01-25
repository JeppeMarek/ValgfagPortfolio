using Microsoft.AspNetCore.Components.Forms;

namespace ValgfagPortfolio.Services;

public class ImageService : IImageService
{
    private const long Maxfilesize = 5 * 1024 * 1024;


    private static readonly string[] AllowedTypes =
    {
        "image/jpeg",
        "image/png",
        "image/webp"
    };

    private readonly IWebHostEnvironment env;

    public ImageService(IWebHostEnvironment env)
    {
        this.env = env;
    }

    public async Task<string> UploadImageAsync(IBrowserFile file, string folder,
        CancellationToken cancellationToken = default)
    {
        // Validate type and size
        if (!AllowedTypes.Contains(file.ContentType))
            throw new InvalidOperationException
                ("Ugyldigt fil format");
        if (file.Size > Maxfilesize)
            throw new InvalidOperationException("Filen er for stor. Max 5 mb");
        // Setup folder for uploads
        var uploadsRoot = Path.Combine(env.WebRootPath, "uploads", folder);
        Directory.CreateDirectory(uploadsRoot);
        // Generate an id for file and set the filepath
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.Name)}";
        var filePath = Path.Combine(uploadsRoot, fileName);
        // Copy file to folder
        await using var stream = File.Create(filePath);
        await file.OpenReadStream(Maxfilesize).CopyToAsync(stream, cancellationToken);
        // Return the relative filepath
        var relPath = $"uploads/{folder}/{fileName}";
        return relPath;
    }

    public async Task DeleteImageAsync(string filePath)
    {
        throw new NotImplementedException();
    }

    public async Task<string> UpdateImageAsync(IFormFile file, string id, string type,
        string oldImagePath)
    {
        throw new NotImplementedException();
    }
}