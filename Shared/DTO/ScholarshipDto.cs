using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.AspNetCore.Http;
//using Shared.Enums;

namespace Shared.DTO
{
    public record ScholarshipDto(
              string? Name,
       string? Description,
       decimal? Cost,
       string? Grants,
       DateTime? ApplicationDeadline,
       string? UrlApplicationForm,
       string? UrlPicture,
       IFormFile? ImageFile,
       string? EligibleGrade,
       DateTime? StartDate,
       int? Duration,
       Degree? Degree,
       Funded? Funded,
       Dictionary<string, string>? Requirements
   );
}
