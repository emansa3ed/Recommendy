using Shared.DTO.Feedback;
using Shared.RequestFeatures;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IFeedbackService
    {
        Task CreateFeedbackAsync(string CompanyID, int PostId, string StudentId, FeedbackCreationDto feedback);

        Task DeleteFeedbackAsync(string CompanyID,string StudentId, int PostId,  FeedbackDelationDto FeedbackId);

        Task<FeedBackDto> GetFeedbackAsync(int FeedbackId);
        Task<PagedList<FeedBackDto>> GetAllFeedbackAsync(string CompanyID, int PostId, FeedBackParameters Feedback);

    }
}