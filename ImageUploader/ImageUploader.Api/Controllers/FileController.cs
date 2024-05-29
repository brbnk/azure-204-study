using ImageUploader.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using File = ImageUploader.Domain.ValueObjects.File;

namespace ImageUploader.Api.Controller;

[ApiController]
[Route("[controller]")]
public class FileController(IFileService fileService) : ControllerBase 
{
  [HttpPost]
  public async Task<IActionResult> UploadFileAsync(IFormFile file) 
  {
    using var stream = file.OpenReadStream();

    var fileToUpload = new File(file.ContentType, file.Length, stream);

    var success = await fileService.UploadAsync(fileToUpload);

    if (!success)
      return BadRequest();

    return Ok();
  }
}