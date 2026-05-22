using Authservice.Data;
using Authservice.DTOs.Form;
using Authservice.Models;

namespace Authservice.Service;

public class FormService : IFormService
{
    private readonly AppDbContext _context;

    public FormService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<FeedbackForm> CreateFormAsync(CreateFormDTO dto)
    {
        var form = new FeedbackForm
        {
            Title = dto.Title,
            Questions = dto.Questions.Select(q => new Question
            {
                Id = Guid.NewGuid(),
                Text = q.Text,
                Type = q.Type,
                Options = q.Options?.Select(o => new Option
                {
                    Id = Guid.NewGuid(),
                    Value = o
                }).ToList() ?? new List<Option>()
            }).ToList()
        };

        _context.FeedbackForms.Add(form);
        await _context.SaveChangesAsync();

        return form;
    }

    public async Task<FeedbackForm> GetFormAsync(Guid id)
    {
        return await _context.FeedbackForms.FindAsync(id);
    }
}