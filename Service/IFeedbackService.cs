using Authservice.Models;
public interface IFeedbackService
{
    Task<List<Feedback>> GetAllFeedbacksAsync();

    Task<Feedback> GetFeedbackByIdAsync(Guid id);

    Task<List<Feedback>> GetFeedbacksByUserIdAsync(Guid userId);
    Task AddFeedbackAsync(Feedback feedback);

    Task UpdateFeedbackAsync(Feedback feedback);

    Task DeleteFeedbackAsync(Guid id);
}