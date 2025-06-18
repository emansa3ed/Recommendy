using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Ollama
{
    public record GenerateTextRequest
    {
        public string Prompt { get; set; }
        public string Model { get; set; } = "deepseek-r1:8b";
        public bool Stream { get; set; } = false;
        public string SystemPrompt { get; set; } = null;
        public string PromptType { get; set; } = "recommendation";
    }

}
