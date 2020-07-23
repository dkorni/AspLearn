using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ExplosiveMemes.Controllers
{
    public class BotController : ControllerBase
    {
        private CommandStore _commandStore;

        public BotController(CommandStore commandStore)
        {
            _commandStore = commandStore;
        }

        [HttpPost]
        [Route(@"api/message/update")]
        public async Task<OkResult> Update([FromBody]Update update)
        {
            if (update == null)
                return Ok();

            var client = await Bot.Get();

            var message = update.Message;

            if (_commandStore.TryToGet(message.Text, out var command))
                await command.Execute(message);

            else if (update.Type == UpdateType.Message && message.Sticker == null)
            {
                await client.SendTextMessageAsync(update.Message.Chat.Id, update.Message.Text);
            }

            return Ok();
        }
    }
}