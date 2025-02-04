using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public record GetScholarshipDto(
        int Id,
        string Name,
        string Description,
        decimal Cost,
        string Grants,
        DateTime ApplicationDeadline,
        string UrlApplicationForm,
        string UrlPicture,
        bool IsBanned,
        string EligibleGrade,
        DateTime StartDate,
        int Duration,
        int Degree,
        int Funded,
        string UniversityId,
        DateTime CreatedAt,
        Dictionary<string, string> Requirements
    );
}




