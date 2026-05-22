using Authservice.Models;
public class Question
{
    public Guid Id { get; set; }

    public Guid FormId { get; set; }
    public FeedbackForm Form { get; set; }   // ADD THIS

    public string Text { get; set; }

    public QuestionType Type { get; set; }

    public List<Option> Options { get; set; }
}