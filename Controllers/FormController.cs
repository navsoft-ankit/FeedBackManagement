using Microsoft.AspNetCore.Mvc;
using Authservice.DTOs.Form;
using Authservice.Service;

namespace Authservice.Controllers;

[ApiController]
[Route("api/forms")]
public class FormController : ControllerBase
{
    private readonly IFormService _service;

    public FormController(IFormService service)
    {
        _service = service;
    }

   [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateFormDTO dto)
    {
        var result = await _service.CreateFormAsync(dto);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        return Ok(await _service.GetFormAsync(id));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateFormDTO dto)
    {
        var result = await _service.UpdateFormAsync(id, dto);
        return Ok(result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        // Implement delete logic here
        var result = await _service.DeleteFormAsync(id);
        return Ok(result);
    }

    [HttpPost("submit")]
    public async Task<IActionResult> SubmitFeedback(SubmitFeedbackDTO dto)
    {
        var result = await _service.SubmitFeedbackAsync(dto);
        return Ok(new
        {
            success = true,
            message = "Feedback submitted successfully"
        });
    }
}