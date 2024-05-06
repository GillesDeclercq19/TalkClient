using System.ComponentModel.DataAnnotations;

namespace TalkClient.Model
{
    public class ChatChannel
    {
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
       }
}
