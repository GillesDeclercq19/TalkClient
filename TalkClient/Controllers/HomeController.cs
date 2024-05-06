using Microsoft.AspNetCore.Mvc;
using TalkClient.Model;
using TalkClient.Services;

namespace TalkClient.Cyb.UI.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly TalkService _talkService;

        public HomeController(TalkService talkService)
        {
            _talkService = talkService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string id)
        {
            var channels = await _talkService.GetChannelsAsync();
            var combined = new ChatViewModel { Channels = channels };

            if (!string.IsNullOrEmpty(id))
            {
                combined.CurrentChannelName = id;
                var messages = await _talkService.GetMessagesByChannelAsync(id);
                combined.Messages = messages;
            }

            return View(combined);
        }

        [HttpGet]
        public async Task<IActionResult> getMessages(int id)
        {
            var channels = await _talkService.GetChannelsAsync();
            var channel = channels.FirstOrDefault(x => x.Id == id);

            if (channel == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", new { id = channel.Name });
        }

        [HttpPost]
        public async Task<IActionResult> CreateChannel(string channelName)
        {
            var success = await _talkService.CreateChannelAsync(channelName);

            if (success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Failed to create channel, please try again");
                return View("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(string channel, string message, string author)
        {
            var success = await _talkService.CreateMessageAsync(channel, message, author);

            if (success)
            {
                return RedirectToAction("Index", new { id = channel });
            }
            else
            {
                ModelState.AddModelError("", "Failed to create message, please try again");
                return View("Index");
            }
        }
    }
}
