public class QuestionDTO
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public string Type { get; set; }
    public List<string> Options { get; set; }
}