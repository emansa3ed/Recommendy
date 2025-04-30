using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IFileRepository
    {

        public Task<string> UploadImage(string Location, IFormFile file);

	}
}
