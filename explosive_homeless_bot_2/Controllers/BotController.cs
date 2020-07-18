using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using explosive_homeless_bot_2.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;

namespace explosive_homeless_bot_2.Controllers
{
    [Route("api/[controller]")]
    public class BotController : ControllerBase
    {
        private readonly ILogger<BotController> _logger;

        public BotController(ILogger<BotController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            _logger.Log(LogLevel.Information, "Received");
            return $"Received";
        }

        [HttpPost]
        public async Task<OkResult> Post([FromBody] Update update)
        {
            if (update == null) return Ok();

            _logger.Log(LogLevel.Information, $"Received {update.Message.Text}");

            await Bot.Client.SendTextMessageAsync(update.Message.Chat.Id, "Suka");
            return Ok();
        }
    }
}