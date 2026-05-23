using Authservice.Models;

namespace Authservice.DTOs.Form;

public class CreateFormDTO
{
    public string Title { get; set; }

    public List<CreateQuestionDTO> Questions { get; set; }

    public string Description { get; set; }
}