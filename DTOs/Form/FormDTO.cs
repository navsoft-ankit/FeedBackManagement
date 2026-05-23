using Authservice.DTOs.Form;
public class FormDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public IEnumerable<object> Questions { get; internal set; }
}