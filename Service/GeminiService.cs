using DocumentFormat.OpenXml.Spreadsheet;
using Service.Contracts;
using AutoGen.Core;
using AutoGen.Gemini;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
	public class GeminiService : IGeminiService
	{
		public string SendRequest(string Prompt)
		{
			var apiKey = Environment.GetEnvironmentVariable("GeminiKey");


			var geminiAgent = new GeminiChatAgent(
					name: "gemini",
					model: "gemini-1.5-flash",
					apiKey: apiKey,
					systemMessage: "just answer without any additional text.")
				.RegisterMessageConnector()
				.RegisterPrintMessage();
			var reply = geminiAgent.SendAsync(Prompt).Result;
			return reply.GetContent();
		}
	}
}
