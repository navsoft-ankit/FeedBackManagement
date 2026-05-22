
namespace Authservice.Models;
    public class Feedback
    {
        public int Id { get; set; }
        public string Comments { get; set; }

        public string rating {get; set;}

        public int UserId { get; set; }
    }