﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Internship
{
    public record InternshipPositionDto
    {

        [Required(ErrorMessage = "PositionId is required")]
        public int PositionId { get; set; }
        [Required(ErrorMessage = "Requirements is required")]
        public string Requirements { get; set; }
        [Required(ErrorMessage = "NumOfOpenings is required")]
        public int NumOfOpenings { get; set; }
    }
}
