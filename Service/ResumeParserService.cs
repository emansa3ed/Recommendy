using Contracts;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Entities.Exceptions;
using Entities.ResumeModels;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Service.Contracts;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Path = System.IO.Path;

namespace Service
{
	public class ResumeParserService : IResumeParserService
	{

		private readonly IRepositoryManager _repositoryManager;

		public ResumeParserService(IRepositoryManager repositoryManager)
		{
				_repositoryManager = repositoryManager;
		}


		public async Task<List<string>> UploadResume(IFormFile file)
		{
			string fileExtension = Path.GetExtension(file.FileName).ToLower();

			if (fileExtension != ".pdf" && fileExtension != ".docx" && fileExtension != ".doc")
			{
				throw new BadRequestException("Unsupported file format. Please upload PDF or Word documents.");
			}

			var tempFilePath = Path.GetTempFileName();
			Resume parsedResume;
			try
			{
				using (var stream = new FileStream(tempFilePath, FileMode.Create))
				{
					await file.CopyToAsync(stream);
				}

				parsedResume = ParseResume(tempFilePath, fileExtension);
			}
			finally
			{
				if (System.IO.File.Exists(tempFilePath))
					System.IO.File.Delete(tempFilePath);
			}
			return parsedResume.Skills;
		}

		public  Resume ParseResume(string filePath, string fileExtension)
		{
			string extractedText = ExtractTextFromDocument(filePath, fileExtension);

			var resume = new Resume
			{
				Name = ExtractName(extractedText),
				Email = ExtractEmail(extractedText),
				Phone = ExtractPhone(extractedText),
				Address = ExtractAddress(extractedText),
				Summary = ExtractSummary(extractedText),
				Skills = ExtractSkills(extractedText),
				WorkExperience = ExtractWorkExperience(extractedText),
				Education = ExtractEducation(extractedText)
			};

			return resume;
		}

		public List<string> GetSkillsList()
		{
			return _repositoryManager.SkillsRepository.GetAllSkills();
		}

		private string ExtractTextFromDocument(string filePath, string fileExtension)
		{
			if (fileExtension == ".pdf")
			{
				return ExtractTextFromPdf(filePath);
			}
			else if (fileExtension == ".docx")
			{
				return ExtractTextFromDocx(filePath);
			}
			else if (fileExtension == ".doc")
			{
				throw new NotImplementedException("Parsing .doc files is not implemented yet.");
			}

			throw new ArgumentException($"Unsupported file extension: {fileExtension}");
		}

		private string ExtractTextFromPdf(string filePath)
		{
			var sb = new StringBuilder();

			using (PdfReader reader = new PdfReader(filePath))
			{
				for (int page = 1; page <= reader.NumberOfPages; page++)
				{
					string pageText = PdfTextExtractor.GetTextFromPage(reader, page, new SimpleTextExtractionStrategy());
					sb.AppendLine(pageText);
				}
			}

			return sb.ToString();
		}

		private string ExtractTextFromDocx(string filePath)
		{
			var sb = new StringBuilder();

			using (WordprocessingDocument doc = WordprocessingDocument.Open(filePath, false))
			{
				if (doc.MainDocumentPart != null)
				{
					Body body = doc.MainDocumentPart.Document.Body;
					if (body != null)
					{
						foreach (var paragraph in body.Descendants<Paragraph>())
						{
							sb.AppendLine(paragraph.InnerText);
						}
					}
				}
			}

			return sb.ToString();
		}

		private string ExtractName(string text)
		{
			if (string.IsNullOrWhiteSpace(text))
				return "Unknown";

			var lines = text
				.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
				.Select(l => l.Trim())
				.Where(l => !string.IsNullOrWhiteSpace(l))
				.ToList();

			var disqualifyingPatterns = new[]
			{
		@"@",                   
        @"\bresume\b",          
        @"\bcurriculum\b",     
        @"\b(email|phone|address|contact|linkedin|github)\b"
	};

			var nameCandidate = lines
				.Take(5)
				.Where(line => !disqualifyingPatterns.Any(p => Regex.IsMatch(line, p, RegexOptions.IgnoreCase)))
				.FirstOrDefault(line => line.Split(' ').Length >= 2 && line.Length < 50);

			return nameCandidate ?? "Unknown";
		}


		private string ExtractEmail(string text)
		{
			var emailRegex = new Regex(@"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}");
			var match = emailRegex.Match(text);
			return match.Success ? match.Value : "";
		}

		private string ExtractPhone(string text)
		{
			var phoneRegex = new Regex(@"\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}");
			var match = phoneRegex.Match(text);
			return match.Success ? match.Value : "";
		}

		private string ExtractAddress(string text)
		{
			var lines = text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

			var addressCandidate = lines.FirstOrDefault(l =>
				(Regex.IsMatch(l, @"\b[A-Z]{2}\b \d{5}", RegexOptions.IgnoreCase) ||
				 Regex.IsMatch(l, @"\b(Street|St|Avenue|Ave|Road|Rd|Boulevard|Blvd|Lane|Ln|Drive|Dr)\b", RegexOptions.IgnoreCase)) &&
				l.Length < 150);

			return addressCandidate?.Trim() ?? "";
		}

		private string ExtractSummary(string text)
		{
			var summaryRegex = new Regex(@"(?i)(summary|profile|objective|about me)[\s:]+(.+?)(?=\n\n|\n[A-Z]|\Z)", RegexOptions.Singleline);
			var match = summaryRegex.Match(text);

			if (match.Success && match.Groups.Count > 2)
			{
				return match.Groups[2].Value.Trim();
			}

			return "";
		}

		private List<string> ExtractSkills(string text)
		{
			var knownSkills = _repositoryManager.SkillsRepository.GetAllSkills();
			var foundSkills = new List<string>();

			var skillsSectionRegex = new Regex(@"(?i)skills[\s:]+(.+?)(?=\n\n|\n[A-Z]|\Z)", RegexOptions.Singleline);
			var match = skillsSectionRegex.Match(text);

			if (match.Success && match.Groups.Count > 1)
			{
				string skillsText = match.Groups[1].Value;

				foreach (var skill in knownSkills)
				{
					if (Regex.IsMatch(skillsText, $@"\b{Regex.Escape(skill)}\b", RegexOptions.IgnoreCase) &&
						!foundSkills.Contains(skill))
					{
						foundSkills.Add(skill);
					}
				}
			}

			foreach (var skill in knownSkills)
			{
				if (Regex.IsMatch(text, $@"\b{Regex.Escape(skill)}\b", RegexOptions.IgnoreCase) &&
					!foundSkills.Contains(skill))
				{
					foundSkills.Add(skill);
				}
			}

			return foundSkills;
		}

		private List<WorkExperience> ExtractWorkExperience(string text)
		{
			if (string.IsNullOrWhiteSpace(text))
				return new List<WorkExperience>();

			var lines = text
				.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
				.Select(l => l.Trim())
				.Where(l => !string.IsNullOrWhiteSpace(l))
				.ToList();

			var startRx = new Regex(@"(?i)^(work experience|professional experience|experience|work history|employment)\b");
			var endRx = new Regex(@"(?i)^(education|projects|skills|certifications|awards|volunteer|publications)\b");
			int startIdx = lines.FindIndex(l => startRx.IsMatch(l));
			if (startIdx < 0) return new List<WorkExperience>();

			var section = lines
				.Skip(startIdx + 1)
				.TakeWhile(l => !endRx.IsMatch(l))
				.ToList();

			var dateRx = new Regex(
				@"\[?\s*(?<start>(?:Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)\.?\s*\d{4})\s*[-–to]+\s*(?<end>Present|Current|(?:Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)\.?\s*\d{4})\s*\]?",
				RegexOptions.IgnoreCase | RegexOptions.Compiled
			);

			bool IsHeader(string line)
			{
				var letters = line.Where(char.IsLetter).ToList();
				return letters.Count > 0
					&& letters.All(char.IsUpper)
					&& line.Split(' ').Length >= 2;
			}

			var blocks = new List<List<string>>();
			List<string> current = null;
			foreach (var ln in section)
			{
				if (IsHeader(ln))
				{
					if (current != null) blocks.Add(current);
					current = new List<string> { ln };
				}
				else if (current != null)
				{
					current.Add(ln);
				}
			}
			if (current != null) blocks.Add(current);

			var bulletRx = new Regex(@"^[\u2022\-\*]\s*(.+)");

			var results = new List<WorkExperience>();
			foreach (var blk in blocks)
			{
				var xp = new WorkExperience();

				var headerLines = blk
					.TakeWhile(l => !bulletRx.IsMatch(l) && !dateRx.IsMatch(l))
					.ToList();

				if (headerLines.Count > 0)
					xp.Position = headerLines[0];               
				if (headerLines.Count > 1)
					xp.Company = headerLines[1];               

				var dm = dateRx.Match(string.Join(" ", blk));
				if (dm.Success)
				{
					var s = dm.Groups["start"].Value.Replace(".", "");
					var e = dm.Groups["end"].Value.Replace(".", "");

					if (DateTime.TryParseExact(s, "MMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var sd))
						xp.StartDate = sd;

					if (e.Equals("Present", StringComparison.OrdinalIgnoreCase) ||
						e.Equals("Current", StringComparison.OrdinalIgnoreCase))
					{
						xp.EndDate = null;
					}
					else if (DateTime.TryParseExact(e, "MMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var ed))
					{
						xp.EndDate = ed;
					}
				}

				xp.Responsibilities = blk
					.Select(l => bulletRx.Match(l))
					.Where(m => m.Success)
					.Select(m => m.Groups[1].Value.Trim())
					.ToList();

				if (!string.IsNullOrEmpty(xp.Position) ||
					!string.IsNullOrEmpty(xp.Company) ||
					xp.Responsibilities.Count > 0)
				{
					results.Add(xp);
				}
			}

			return results;
		}





		private List<Education> ExtractEducation(string text)
		{
			if (string.IsNullOrWhiteSpace(text))
				return new List<Education>();

			var lines = text
				.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
				.Select(l => l.Trim())
				.Where(l => !string.IsNullOrWhiteSpace(l))
				.ToList();

			var headerPattern = @"(?i)^(education|educational background|academic background)\b";
			int start = lines.FindIndex(l => Regex.IsMatch(l, headerPattern));
			if (start < 0)
				return new List<Education>();

			var nextSectionPattern = @"(?i)^(experience|work experience|work history|employment|projects|skills|certifications)\b|^[A-Z][A-Za-z\s]+:$";
			var sectionLines = new List<string>();
			for (int i = start + 1; i < lines.Count; i++)
			{
				if (Regex.IsMatch(lines[i], nextSectionPattern))
					break;
				sectionLines.Add(lines[i]);
			}

			var rawEntries = Regex.Split(
					string.Join("\n", sectionLines),
					@"\n\s*(?:-|\u2022)\s*|\n{2,}"
				)
				.Select(e => e.Trim())
				.Where(e => !string.IsNullOrWhiteSpace(e));

			var degreeRegex = new Regex(
				@"(?ix)\b(bachelor(?:'s)?|master(?:'s)?|ph\.?d\.?|associate|doctorate|certificate|diploma)\b.*",
				RegexOptions.Compiled
			);
			var dateRegex = new Regex(
				@"(?i)\b(?:graduated|class of)?\s*(?:(?<month>Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)[a-z]*\.?\s*)?(?<year>\d{4})\b",
				RegexOptions.Compiled
			);

			var results = new List<Education>();
			foreach (var entry in rawEntries)
			{
				var edu = new Education();
				var entryLines = entry.Split('\n').Select(l => l.Trim()).ToList();

				edu.Institution = Regex.Replace(entryLines[0], @"\d{4}", "").Trim(new[] { ',', ' ' });

				var dm = degreeRegex.Match(entry);
				if (dm.Success)
				{
					var parts = dm.Value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
					edu.Degree = parts[0];
					edu.Field = string.Join(" ",
						parts.Skip(Regex.IsMatch(parts[1], @"^(of|in)$", RegexOptions.IgnoreCase) ? 2 : 1)
					).Trim();
				}

				var gm = dateRegex.Match(entry);
				if (gm.Success && int.TryParse(gm.Groups["year"].Value, out int yr))
				{
					int month = 5; 
					var m = gm.Groups["month"].Value;
					if (!string.IsNullOrEmpty(m) &&
						DateTime.TryParseExact(m, "MMM", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt))
					{
						month = dt.Month;
					}
					edu.GraduationDate = new DateTime(yr, month, 1);
				}

				if (!string.IsNullOrEmpty(edu.Institution))
					results.Add(edu);
			}

			return results;
		}


	}
}
