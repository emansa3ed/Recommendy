using Entities.ResumeModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
	public interface IResumeParserService
	{
		public Resume ParseResume(string filePath, string fileExtension);
		public List<string> GetSkillsList();
		public Task<List<string>> UploadResume(IFormFile file);

	}

}
