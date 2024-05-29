namespace ImageUploader.Domain.ValueObjects;

public sealed class File(string contentType, long length, Stream stream) : IValueObject
{
  private const long SIZE_LIMIT = 2097152;

  private static readonly IDictionary<string, List<byte[]>> _fileSignatures =
    new Dictionary<string, List<byte[]>>()
    {
      { "image/jpeg", new List<byte[]> 
        { 
          new byte[] { 0xFF, 0xD8, 0xFF } 
        } 
      }
    };

  public bool IsValid()
  {
    var reader = new BinaryReader(stream);

    var isValidContentType = _fileSignatures.TryGetValue(contentType, out var signatures);

    if (!isValidContentType || length > SIZE_LIMIT) 
    {
      return false;
    }

    var headerBytes = reader.ReadBytes(signatures.Max(s => s.Length));

    reader.BaseStream.Position = 0;

    return signatures.Any(signature => headerBytes.Take(signature.Length).SequenceEqual(signature));
  }

  public Stream GetFileStream() => stream;
}