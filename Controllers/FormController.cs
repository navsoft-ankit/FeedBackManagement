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
    public async Task<IActionResult> Create(CreateFormDTO dto)
    {
        var result = await _service.CreateFormAsync(dto);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        return Ok(await _service.GetFormAsync(id));
    }
}