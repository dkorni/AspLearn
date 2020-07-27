using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ExplosiveMemes.Utils;
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

            if (message == null)
                return Ok();

            if (_commandStore.TryToGet(message.Text, out var command))
                await command.Execute(message);

            else if (update.Type == UpdateType.Message && message.Sticker == null)
            {
                await ApiProvider.DisplayMenu(message.Chat.Id, "Чо ты щас сморозил? Может тебе совет дать какой-то?",
                    true, "Совет");
            }

            return Ok();
        }
    }
}