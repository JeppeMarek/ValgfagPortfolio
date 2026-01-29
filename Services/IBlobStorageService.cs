using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace ValgfagPortfolio.Services;

public interface IBlobStorageService
{
    Task<Model.BlobContentInfo> GetBlobAsync(string name);
    Task<IEnumerable<string>> ListBlobsAsync();
    Task<string> UploadImageAsync(IBrowserFile file, string folder, CancellationToken cancellationToken = default);
    Task UploadFileAsync(string filePath, string fileName);
    Task UploadContentBlobAsync(string content, string fileName);
    Task DeleteBlobAsync(string name);
}