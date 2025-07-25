﻿using System.Text.Json;
namespace Entities.ErrorDetail
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? Details { get; set; }
		public override string ToString() => JsonSerializer.Serialize(this);
    }
}