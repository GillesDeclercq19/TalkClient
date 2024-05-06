using System.ComponentModel.DataAnnotations;

namespace TalkClient.Model
{
    public class ChatViewModel
    {
        public IList<ChatChannel>? Channels { get; set; }
        public IList<ChatMessage>? Messages { get; set; }
        public string? CurrentChannelName { get; set; }

    }
}
