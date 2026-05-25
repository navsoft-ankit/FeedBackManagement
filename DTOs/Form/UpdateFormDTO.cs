public class UpdateFormDTO
{
    public string Title { get; set; }
    public string Description { get; set; }

    public List<QuestionDTO> Questions { get; set; }
    public string? Note { get; set; }
}