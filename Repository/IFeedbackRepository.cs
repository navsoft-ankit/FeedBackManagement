using Authservice.Models;

public interface IFeedbackRepository
{
    Task<List<Feedback>> GetAllFeedbacksAsync();
    Task<Feedback> GetFeedbackByIdAsync(int id);
    Task<List<Feedback>> GetFeedbacksByUserIdAsync(int userId);
    Task AddFeedbackAsync(Feedback feedback);
    Task UpdateFeedbackAsync(Feedback feedback);
    Task DeleteFeedbackAsync(int id);
}