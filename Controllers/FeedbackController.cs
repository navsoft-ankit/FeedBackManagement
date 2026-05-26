using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Authservice.Data;
using Authservice.Models;
using Microsoft.EntityFrameworkCore;
using Authservice.DTOs.Form;

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

    // 🔓 PUBLIC: anyone can submit feedback
    [AllowAnonymous]
    [HttpPost("submit")]
    public async Task<IActionResult> SubmitFeedback([FromBody] SubmitFeedbackDTO dto)
    {
        var alreadySubmitted = await _context.Feedbacks
            .AnyAsync(f => f.FormId == dto.FormId && f.Email == dto.Email);

        if (alreadySubmitted)
            return BadRequest("Feedback already submitted");

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
                Response = a.Response
            }).ToList()
        };

        _context.Feedbacks.Add(feedback);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            Message = "Feedback submitted successfully",
            FeedbackId = feedback.Id
        });
    }

    // 🔐 ADMIN ONLY: view all feedback for a form
    [Authorize]
    [HttpGet("forms/{formId:guid}")]
    public async Task<IActionResult> GetFeedbackForForm(Guid formId)
    {
        var feedbacks = await _context.Feedbacks
            .Where(f => f.FormId == formId)
            .Select(f => new
            {
                f.Id,
                f.Name,
                f.Email,
                f.Designation,
                f.FinalNote,
                Answers = f.Answers.Select(a => new
                {
                    a.QuestionId,
                    a.Response
                }).ToList()
            })
            .ToListAsync();

        return Ok(feedbacks);
    }

    // 🔐 ADMIN ONLY: single feedback
    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetFeedback(Guid id)
    {
        var feedback = await _context.Feedbacks
            .Where(f => f.Id == id)
            .Select(f => new
            {
                f.Id,
                f.Name,
                f.Email,
                f.Designation,
                f.FinalNote,
                Answers = f.Answers.Select(a => new
                {
                    a.QuestionId,
                    a.Response
                }).ToList()
            })
            .FirstOrDefaultAsync();

        if (feedback == null)
            return NotFound();

        return Ok(feedback);
    }
}