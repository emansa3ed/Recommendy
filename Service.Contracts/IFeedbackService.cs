using Shared.DTO.Feedback;

namespace Service.Contracts
{
    public interface IFeedbackService
    {
        Task CreateFeedbackAsync(string CompanyID, int PostId, string StudentId, FeedbackCreationDto feedback);
        Task DeleteFeedbackAsync(string CompanyID, int PostId, string StudentId, FeedbackDelationDto FeedbackId);

	}
}
