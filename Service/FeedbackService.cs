using Authservice.Models;
using Authservice.Repository;

namespace Authservice.Service
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackService(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public async Task<List<Feedback>> GetAllFeedbacksAsync()
        {
            return await _feedbackRepository.GetAllFeedbacksAsync();
        }

        public async Task<Feedback> GetFeedbackByIdAsync(Guid id)
        {
            return await _feedbackRepository.GetFeedbackByIdAsync(id);
        }

        public async Task<List<Feedback>> GetFeedbacksByUserIdAsync(Guid userId)
        {
            return await _feedbackRepository.GetFeedbacksByUserIdAsync(userId);
        }

        public async Task AddFeedbackAsync(Feedback feedback)
        {
            await _feedbackRepository.AddFeedbackAsync(feedback);
        }

        public async Task UpdateFeedbackAsync(Feedback feedback)
        {
            await _feedbackRepository.UpdateFeedbackAsync(feedback);
        }

        public async Task DeleteFeedbackAsync(Guid id)
        {
            await _feedbackRepository.DeleteFeedbackAsync(id);
        }

        
    }
}