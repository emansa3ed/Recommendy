using Shared.DTO.Student;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IProfileSuggestionService
    {
        Task<ProfileSuggestionDto> GetProfileSuggestionsAsync(string studentId);
    }
} 