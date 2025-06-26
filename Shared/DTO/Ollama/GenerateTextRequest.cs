using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.Ollama
{
    public record GenerateTextRequest
    {
        [Required(ErrorMessage = "Prompt is required.")]
        [MinLength(1, ErrorMessage = "Prompt cannot be empty.")]
        public string Prompt { get; set; }
        [Required(ErrorMessage = "Model is required.")]
        public string Model { get; set; } = "deepseek-r1:8b";
        public bool Stream { get; set; } = false;
        public string SystemPrompt { get; set; } = null;
    }

}
