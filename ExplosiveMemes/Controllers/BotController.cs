using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ExplosiveMemes.Controllers
{
    public class BotController : ControllerBase
    {
        [HttpPost]
        [Route(@"api/message/update")]
        public async Task<OkResult> Update([FromBody]Update update)
        {
            if (update == null)
                return Ok();

            var client = await Bot.Get();

            Console.WriteLine("Hello");

            if (update.Type == UpdateType.Message && update.Message.Sticker == null)
                await client.SendTextMessageAsync(update.Message.Chat.Id, update.Message.Text);

            return Ok();
        }
    }
}