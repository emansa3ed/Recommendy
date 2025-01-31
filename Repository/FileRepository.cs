using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Contracts;

namespace Repository
{
    public class FileRepository : IFileRepository
    {
       
        private readonly IWebHostEnvironment _webHostEnvironment;
       
        public FileRepository(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
      
        #region Handle Functions
        public async Task<string> UploadImage(string Location, IFormFile file)
        {
            var path = _webHostEnvironment.WebRootPath + "/" + Location + "/";
            var extention = Path.GetExtension(file.FileName);
            var fileName = Guid.NewGuid().ToString().Replace("-", string.Empty) + extention;
            if (file.Length > 0)
            {
                try
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream filestreem = File.Create(path + fileName))
                    {
                        await file.CopyToAsync(filestreem);
                        await filestreem.FlushAsync();
                        return $"/{Location}/{fileName}";
                    }
                }
                catch (Exception)
                {
                    return "FailedToUploadImage";
                }
            }
            else
            {
                return "NoImage";
            }
        }
        #endregion
    }
}