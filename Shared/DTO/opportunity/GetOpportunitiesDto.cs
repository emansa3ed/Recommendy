using Shared.DTO.Internship;
using Shared.DTO.Scholaship;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.opportunity
{
	public record GetOpportunitiesDto
	{

        public PagedList<GetScholarshipDto>? Scholarships { get; set; }
        public PagedList<InternshipDto>? Internships { get; set; }


    }
}
