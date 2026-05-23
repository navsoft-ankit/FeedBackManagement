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
        Description = dto.Description
    };

    _context.FeedbackForms.Add(form);
    await _context.SaveChangesAsync();

    foreach (var q in dto.Questions)
    {
        var question = new Question
        {
            Text = q.Text,
            Type = q.Type,
            FormId = form.Id
        };

        _context.Questions.Add(question);
        await _context.SaveChangesAsync();

        if (q.Options != null)
        {
            foreach (var o in q.Options)
            {
                _context.Options.Add(new Option
                {
                    Value = o,
                    QuestionId = question.Id
                });
            }
        }
    }

    await _context.SaveChangesAsync();

    return form;
}

    public async Task<FeedbackForm> GetFormAsync(Guid id)
    {
        return await _context.FeedbackForms.FindAsync(id);
    }
}