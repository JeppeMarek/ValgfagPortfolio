namespace ValgfagPortfolio.Services;

public class ImageService : IImageService
{
    private const int MaxFileSize = 10 * 1024 * 1024;

    //TODO Implement this service with newcategory funcionality
    private readonly IWebHostEnvironment environment;

    public ImageService(IWebHostEnvironment environment)
    {
        this.environment = environment;
    }

    public async Task<string> UploadImageAsync(IFormFile file, string id, string type)
    {
        Console.WriteLine(
            $"File: {file?.FileName}, Size: {file?.Length}, Type: {type}"); //debugging

        if (file == null) throw new FileNotFoundException("Filen blev ikke fundet");
        if (file.Length > MaxFileSize) throw new Exception("Filen er for stor. Max 5 MB");
        if (!file.ContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase))
            throw new Exception("Fil typen er ikke godkendt. Kun .jpg & .png filer tilladt");
        var uploadsPath = Path.Combine(environment.WebRootPath, "Images", type);
        Directory.CreateDirectory(uploadsPath);

        var fileExtension = Path.GetExtension(file.FileName);
        var fileName = $"{id}_{Guid.NewGuid()}{fileExtension}";
        var filePath = Path.Combine(uploadsPath, fileName);

        // Save the file
        await using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        // Return web-accessible path instead of absolute file system path
        return $"/Images/{type}/{fileName}";
    }

    public Task DeleteImageAsync(string webPath)
    {
        // Delete file from disk
        if (!string.IsNullOrEmpty(webPath))
        {
            // Convert web path to absolute file system path
            var absolutePath = Path.Combine(environment.WebRootPath, webPath.TrimStart('/'));
            if (File.Exists(absolutePath)) File.Delete(absolutePath);
        }

        return Task.CompletedTask;
    }


    public async Task<string> UpdateImageAsync(IFormFile file, string id, string type,
        string oldImagePath)
    {
        // Delete old image if it exists
        if (!string.IsNullOrEmpty(oldImagePath)) await DeleteImageAsync(oldImagePath);
        // Upload new image
        return await UploadImageAsync(file, id, type);
    }
}