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

    public async Task<string> UploadImageAsync(
        IBrowserFile file,
        string folder,
        CancellationToken cancellationToken = default)
    {
        if (!AllowedTypes.Contains(file.ContentType))
            throw new InvalidOperationException("Ugyldigt filformat");

        if (file.Size > Maxfilesize)
            throw new InvalidOperationException("Filen er for stor. Max 5 MB");

        // env.WebRootPath = c:\home\site\wwwroot\wwwroot (på Azure)
        var uploadsRoot = Path.Combine(env.WebRootPath, "uploads", folder);
        Directory.CreateDirectory(uploadsRoot);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.Name)}";
        var filePath = Path.Combine(uploadsRoot, fileName);

        await using var stream = File.Create(filePath);
        await file.OpenReadStream(Maxfilesize).CopyToAsync(stream, cancellationToken);

        // VIGTIGT: leading slash
        return $"/uploads/{folder}/{fileName}";
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