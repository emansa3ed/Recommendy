using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Contracts;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure.Core;
using Entities.Exceptions;
using MimeDetective;

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

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png"};
			var allowedMimeTypes = new[] { "image/jpeg", "image/png"};
			var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(extension) || !allowedExtensions.Contains(extension))
            {
				throw new FileUploadBadRequestException("Invalid File Type");
            }

			if (file.Length > 10 * 1024 * 1024) // 10 MB
			{
				throw new FileUploadBadRequestException("File Too Large");
			}

			var stream = file.OpenReadStream();

			var inspector = new ContentInspectorBuilder()
			{
				Definitions = MimeDetective.Definitions.DefaultDefinitions.All()
			}.Build();
			
            var result = inspector.Inspect(stream);
			var mime = result.ByMimeType();
            var s =mime.Select(m => m.MimeType);
			if (mime == null || !s.Any(item => allowedMimeTypes.Contains(item)))
			{
				throw new FileUploadBadRequestException("Invalid File Type");
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
