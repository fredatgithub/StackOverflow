using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;

namespace AspNetCoreFileUpload.Controllers
{
    public class FileUploadController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Index()
        {
            var length = Request.Body.Length;

            var result = new { length = length };
            return new OkObjectResult(result);
        }

        private string GetFileName(IFormFile file)
        {
            return file.ContentDisposition
                            .Split(';')
                            .SingleOrDefault(part => part.Contains("filename"))
                            .Split('=')
                            .Last()
                            .Trim('"');
        }

        private async Task SaveAsync(IFormFile file, string fileName)
        {
            var bufferSize = 1024;
            using (var fileStream = System.IO.File.Create(fileName, bufferSize, FileOptions.Asynchronous))
            {
                var inputStream = file.OpenReadStream();
                await inputStream.CopyToAsync(fileStream, bufferSize);
            }
        }
    }
}