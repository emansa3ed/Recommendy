using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Contracts;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure.Core;
using Entities.Exceptions;

namespace Repository
{
    public class FileRepository : IFileRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileRepository(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> UploadImage(string Location, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(extension) || !allowedExtensions.Contains(extension))
            {
                return "InvalidFileType";
            }

            var path = Path.Combine(_webHostEnvironment.WebRootPath, Location);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var fileName = Guid.NewGuid().ToString().Replace("-", string.Empty) + extension;
            var filePath = Path.Combine(path, fileName);

            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                return $"/{Location}/{fileName}";
            }
            catch (Exception)
            {
                return "FailedToUploadImage";
            }
        }

	
			
	}
}
