using Azure.Storage.Blobs;
using ImageUploader.Domain.Interfaces;
using ImageUploader.Domain.FileStorage;
using Microsoft.Extensions.Options;
using File = ImageUploader.Domain.ValueObjects.File;
using Azure.Identity;

namespace ImageUploader.Services;

public class FileService : IFileService
{
  private const string BLOB_CONTAINER_NAME = "<container-name>";
  private readonly FileStorageSettings _options;
  private readonly BlobServiceClient _blobServiceClient;
  private readonly BlobContainerClient _blobContainerClient;

  public FileService(IOptions<FileStorageSettings> options)
  {
    _options = options.Value;
    _blobServiceClient = new BlobServiceClient(new Uri(_options.BaseUrl), new DefaultAzureCredential());
    _blobContainerClient = _blobServiceClient.GetBlobContainerClient(BLOB_CONTAINER_NAME);
  }

  public async Task<bool> UploadAsync(File file)
  {
    try
    {
      if (!file.IsValid())
        return false;

      var filename = Guid.NewGuid().ToString();

      var blobClient = _blobContainerClient.GetBlobClient(filename);

      var stream = file.GetFileStream();

      await blobClient.UploadAsync(stream);

      return true;
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
      return  false;
    }
  }
}