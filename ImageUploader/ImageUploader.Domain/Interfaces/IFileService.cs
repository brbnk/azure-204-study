using File = ImageUploader.Domain.ValueObjects.File;

namespace ImageUploader.Domain.Interfaces;

public interface IFileService
{
  public Task<bool> UploadAsync(File file);
}