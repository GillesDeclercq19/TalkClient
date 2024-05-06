
using System.Net.Http.Json;
using TalkClient.Model;

namespace TalkClient.Services
{
    public class TalkService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TalkService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IList<ChatChannel>> GetChannelsAsync()
        {
            var httpClient = _httpClientFactory.CreateClient("TalkApi");
            var channelsResponse = await httpClient.GetAsync("api/chat-channels");
            channelsResponse.EnsureSuccessStatusCode();
            return await channelsResponse.Content.ReadFromJsonAsync<IList<ChatChannel>>();
        }

        public async Task<IList<ChatMessage>> GetMessagesByChannelAsync(string channelName)
        {
            var httpClient = _httpClientFactory.CreateClient("TalkApi");
            var messageResponse = await httpClient.GetAsync($"api/chat-messages/?Channel={channelName}");
            messageResponse.EnsureSuccessStatusCode();
            return await messageResponse.Content.ReadFromJsonAsync<IList<ChatMessage>>();
        }

        public async Task<bool> CreateChannelAsync(string channelName)
        {
            if (string.IsNullOrWhiteSpace(channelName))
            {
                return false;
            }

            var httpClient = _httpClientFactory.CreateClient("TalkApi");
            var route = "api/chat-channels";
            var response = await httpClient.PostAsJsonAsync(route, new { Name = channelName });
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CreateMessageAsync(string channel, string message, string author)
        {
            var httpClient = _httpClientFactory.CreateClient("TalkApi");
            var route = "api/chat-messages";
            var response = await httpClient.PostAsJsonAsync(route, new { Author = author, Message = message, Channel = channel });
            return response.IsSuccessStatusCode;
        }
    }
}
