using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace Service.Contracts
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile imageFile);
        void DeleteFile(string fileNameWithExtension);
    }
}
