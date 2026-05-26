using Authservice.Data;
using Authservice.Models;
using Microsoft.EntityFrameworkCore;

namespace Authservice.Repository
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly AppDbContext _context;

        public FeedbackRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Feedback>> GetAllFeedbacksAsync()
        {
            return await _context.Feedbacks.ToListAsync();
        }

        public async Task<Feedback> GetFeedbackByIdAsync(Guid id)
        {
            return await _context.Feedbacks
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<List<Feedback>> GetFeedbacksByUserIdAsync(Guid userId)
        {
            return await _context.Feedbacks
                .Where(f => f.UserId == userId)
                .ToListAsync();
        }

        public async Task AddFeedbackAsync(Feedback feedback)
        {
            await _context.Feedbacks.AddAsync(feedback);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFeedbackAsync(Feedback feedback)
        {
            _context.Feedbacks.Update(feedback);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFeedbackAsync(Guid id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);

            if (feedback != null)
            {
                _context.Feedbacks.Remove(feedback);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<Answer>> GetAnswerByDateAsync(DateTime fromDate, DateTime toDate)
        {
            return await _context.Answers
                .Where(a => a.CreatedAt >= fromDate && a.CreatedAt <= toDate)
                .ToListAsync();
        }
    }
}