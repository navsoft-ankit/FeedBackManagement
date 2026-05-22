namespace Authservice.Models;

public class Answer
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid QuestionId { get; set; }

    public Guid? OptionId { get; set; }

    public string? TextValue { get; set; }
}