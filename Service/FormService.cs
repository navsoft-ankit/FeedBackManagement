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

    // ✅ CREATE FORM
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
                Id = question.Id,
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

    // ✅ GET FORM
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
                Id = q.Id,
                Text = q.Text,
                Type = q.Type.ToString(),
                Options = q.Options.Select(o => o.Value).ToList()
            }).ToList()
        };
    }

    //Update Form
    public async Task<FormResponseDTO> UpdateFormAsync(Guid id, UpdateFormDTO dto)
    {
        var form = await _context.FeedbackForms.FindAsync(id);

        if (form == null) return null;

        form.Title = dto.Title;
        form.Description = dto.Description;

        await _context.SaveChangesAsync();

        return await GetFormAsync(id);
    }

    //Delete Form

    public async Task<bool> DeleteFormAsync(Guid id)
    {
        var form = await _context.FeedbackForms.FindAsync(id);

        if (form == null) return false;

        _context.FeedbackForms.Remove(form);
        await _context.SaveChangesAsync();

        return true;
    }


}