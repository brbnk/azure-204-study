namespace ImageUploader.Domain.FileStorage;

public sealed class FileStorageSettings
{
  public string Provider { get; set; }

  public string  BaseUrl { get; set; }
}