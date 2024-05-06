using System.ComponentModel.DataAnnotations;

namespace TalkClient.Model
{
    public class ChatMessage
    {
        public int Id { get; set; }
        [Required]
        public required string Message { get; set; }
        [Required]
        public required string Author { get; set; } 
        
        [Required]
        public required string Channel { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}
