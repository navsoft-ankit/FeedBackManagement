using Microsoft.AspNetCore.Mvc;
using Authservice.DTOs.Form;
using Authservice.Data;
using Authservice.Models;

namespace Authservice.Controllers;

[ApiController]
[Route("api/feedback")]
public class FeedbackController : ControllerBase
{
    private readonly AppDbContext _context;

    public FeedbackController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Submit(SubmitFeedbackDTO dto)
    {
        var feedback = new Feedback
        {
            FormId = dto.FormId,
            Name = dto.Name,
            Email = dto.Email,
            Designation = dto.Designation,
            FinalNote = dto.FinalNote,
            Answers = dto.Answers.Select(a => new Answer
            {
                QuestionId = a.QuestionId,
                OptionId = a.OptionId,
                TextValue = a.TextValue
            }).ToList()
        };

        _context.Feedbacks.Add(feedback);
        await _context.SaveChangesAsync();

        return Ok("Feedback submitted");
    }
}