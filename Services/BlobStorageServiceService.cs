using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Components.Forms;
using ValgfagPortfolio.Model;

namespace ValgfagPortfolio.Services;

public class BlobStorageServiceService : IBlobStorageService
{
    private const string ContainerName = "images";
    private const long MaxFileSize = 5 * 1024 * 1024;

    private static readonly string[] AllowedTypes =
    {
        "image/jpeg",
        "image/png",
        "image/webp"
    };

    private readonly BlobServiceClient _client;

    public BlobStorageServiceService(BlobServiceClient client)
    {
        _client = client;
    }

    public async Task<Model.BlobContentInfo> GetBlobAsync(string name)
    {
        var containerClient = _client.GetBlobContainerClient(ContainerName);
        var blobClient = containerClient.GetBlobClient(name);
        var blobDownloadInfo = await blobClient.DownloadAsync();

        var blobInfo = new Model.BlobContentInfo 
        { 
            Content = blobDownloadInfo.Value.Content, 
            ContentType = blobDownloadInfo.Value.ContentType 
        };
        return blobInfo;
    }

    public async Task<IEnumerable<string>> ListBlobsAsync()
    {
        var containerClient = _client.GetBlobContainerClient(ContainerName);
        var blobs = new List<string>();
        
        await foreach (var blobItem in containerClient.GetBlobsAsync())
        {
            blobs.Add(blobItem.Name);
        }

        
        return blobs;
    }

    public async Task<string> UploadImageAsync(IBrowserFile file, string folder, CancellationToken cancellationToken = default)
    {
        if (!AllowedTypes.Contains(file.ContentType))
            throw new InvalidOperationException("Ugyldigt filformat");

        if (file.Size > MaxFileSize)
            throw new InvalidOperationException("Filen er for stor. Max 5 MB");

        var containerClient = _client.GetBlobContainerClient(ContainerName);
        
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.Name)}";
        var blobPath = $"{folder}/{fileName}";
        var blobClient = containerClient.GetBlobClient(blobPath);

        await using var stream = file.OpenReadStream(MaxFileSize);
        await blobClient.UploadAsync(stream, overwrite: true, cancellationToken);

        return blobClient.Uri.ToString();
    }

    public async Task UploadFileAsync(string filePath, string fileName)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"File not found: {filePath}");

        var containerClient = _client.GetBlobContainerClient(ContainerName);
        var blobClient = containerClient.GetBlobClient(fileName);

        await using var fileStream = File.OpenRead(filePath);
        await blobClient.UploadAsync(fileStream, overwrite: true);
    }

    public async Task UploadContentBlobAsync(string content, string fileName)
    {
        var containerClient = _client.GetBlobContainerClient(ContainerName);
        var blobClient = containerClient.GetBlobClient(fileName);

        await using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content));
        await blobClient.UploadAsync(stream, overwrite: true);
    }

    public async Task DeleteBlobAsync(string name)
    {
        var containerClient = _client.GetBlobContainerClient(ContainerName);
        var blobClient = containerClient.GetBlobClient(name);
        await blobClient.DeleteAsync();
    }
}