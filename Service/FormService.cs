using Authservice.Data;
using Authservice.Models;
using Microsoft.EntityFrameworkCore;
using Authservice.DTOs.Form;


namespace Authservice.Service;

public class FormService : IFormService
{
    private readonly AppDbContext _context;

    public FormService(AppDbContext context)
    {
        _context = context;
    }

    //CREATE FORM
    public async Task<FormResponseDTO> CreateFormAsync(CreateFormDTO dto)
    {
        var form = new FeedbackForm
        {
            Title = dto.Title,
            Description = dto.Description
        };

        _context.FeedbackForms.Add(form);
        await _context.SaveChangesAsync();

        var questionsDto = new List<QuestionDTO>();
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

            var optionsList = new List<string>();

            if (q.Options != null)
            {
                foreach (var o in q.Options)
                {
                    optionsList.Add(o);

                    _context.Options.Add(new Option
                    {
                        Value = o,
                        QuestionId = question.Id
                    });
                }
            }

            questionsDto.Add(new QuestionDTO
            {
                Id = question.Id.ToString(),
                Text = question.Text,
                Type = question.Type.ToString(),
                Options = optionsList
            });
        }

        await _context.SaveChangesAsync();

        return new FormResponseDTO
        {
            Id = form.Id,
            Title = form.Title,
            Description = form.Description,
            Questions = questionsDto
        };
    }

    //GET FORM
    public async Task<FormResponseDTO> GetFormAsync(Guid id)
    {
        var form = await _context.FeedbackForms
            .Include(f => f.Questions)
            .ThenInclude(q => q.Options)
            .FirstOrDefaultAsync(f => f.Id == id);

        if (form == null)
            return null;

        return new FormResponseDTO
        {
            Id = form.Id,
            Title = form.Title,
            Description = form.Description,
            Questions = form.Questions.Select(q => new QuestionDTO
            {
                Id = q.Id.ToString(),
                Text = q.Text,
                Type = q.Type.ToString(),
                Options = q.Options.Select(o => o.Value).ToList()
            }).ToList()
        };
    }

    //UPDATE FORM
    public async Task<FormResponseDTO> UpdateFormAsync(Guid id, UpdateFormDTO dto)
{
    var form = await _context.FeedbackForms
        .Include(f => f.Questions)
        .ThenInclude(q => q.Options)
        .FirstOrDefaultAsync(f => f.Id == id);

    if (form == null)
        return null;

    form.Title = dto.Title;
    form.Description = dto.Description;

    foreach (var dtoQ in dto.Questions)
    {
        var existingQ = form.Questions
            .FirstOrDefault(q => q.Id.ToString() == dtoQ.Id);

        if (existingQ != null)
        {
            existingQ.Text = dtoQ.Text;

            existingQ.Type =
                Enum.TryParse<QuestionType>(dtoQ.Type, true, out var type)
                    ? type
                    : QuestionType.Text;

            // 🔥 IMPORTANT: replace entire collection
             var oldOptions = _context.Options
                .Where(o => o.QuestionId == existingQ.Id);

            _context.Options.RemoveRange(oldOptions);

            await _context.SaveChangesAsync();

            if (dtoQ.Options != null)
            {
                var newOptions = dtoQ.Options.Select(o => new Option
                {
                    Value = o,
                    QuestionId = existingQ.Id
                });

                _context.Options.AddRange(newOptions);
            }
        }
    }

    await _context.SaveChangesAsync();

    return await GetFormAsync(id);
}
    //DELETE FORM
    public async Task<bool> DeleteFormAsync(Guid id)
    {
        var form = await _context.FeedbackForms.FindAsync(id);

        if (form == null) return false;

        _context.FeedbackForms.Remove(form);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> SubmitFeedbackAsync(SubmitFeedbackDTO dto)
    {
    var feedback = new Feedback
    {
        FormId = dto.FormId,
        Name = dto.Name,
        Email = dto.Email,
        Designation = dto.Designation,
        FinalNote = dto.FinalNote,
        Answers = new List<Answer>()
    };

    foreach (var a in dto.Answers)
    {
        feedback.Answers.Add(new Answer
        {
            QuestionId = a.QuestionId,
            Response = a.Response
        });
    }

    _context.Feedbacks.Add(feedback);
    await _context.SaveChangesAsync();

    return true;
    }

}