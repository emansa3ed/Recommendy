using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.DTO.Ollama
{
    public record OllamaResponse
    {
        [JsonPropertyName("response")]
        public string Response { get; set; }

    }
}