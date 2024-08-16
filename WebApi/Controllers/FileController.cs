using Microsoft.AspNetCore.Mvc;
using ResultWrapper.Library;
using WebApi.Contracts;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FileController : ControllerBase
{
     private readonly IWebHostEnvironment _environment;

     public FileController(IWebHostEnvironment environment)
     {
          _environment = environment;
     }

     [HttpPost]
     public async Task<Wrapper> UploadFile([FromForm] UploadFileDto dto)
     {
          var file = dto.File;
          var fileName = $"{Guid.NewGuid()}-{file.FileName}";
          var filePath = Path.Join(_environment.WebRootPath, "uploads", fileName);
          await using var fileStream = System.IO.File.OpenWrite(filePath);
          await file.CopyToAsync(fileStream);

          fileStream.Close();

          return (fileName, 200);
     }
}