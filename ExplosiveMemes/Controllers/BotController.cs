using ExplosiveMemes.Commands;
using ExplosiveMemes.Entities;
using ExplosiveMemes.Services;
using ExplosiveMemes.Utils;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ExplosiveMemes.Controllers
{
    public class BotController : ControllerBase
    {
        private Logger logger = LogManager.GetCurrentClassLogger();

        private CommandStore _commandStore;

        private BearService _bearService;

        private TimeSpan _maxBearTimeSpan = TimeSpan.FromHours(24);

        private ImageProcessor _imageProcessor;

        public BotController(CommandStore commandStore, BearService bearService, ImageProcessor imageProcessor)
        {
            _commandStore = commandStore;
            _bearService = bearService;
            _imageProcessor = imageProcessor;
        }

        [HttpPost]
        [Route(@"api/message/update")]
        public async Task<OkResult> Update([FromBody]Update update)
        {
            if (update == null)
                return Ok();

            var message = update.Message;

            if (message.Text == "/")
                return Ok();

            // logs
            logger.Info($"{message.Chat.FirstName} {message.Chat.LastName} (UserName: {message.Chat.Username})\n " +
                        $"Text: {message.Text}");

            // create user in db if it is not exists
            await EnsureUserCreated(message);

            // update user last message
            await UpdateUserLastMessage(message);

            var client = await Bot.Get();

            // check last bear
            if (CheckLastBearExpired(message) && message.Text!= "🍺")
            {
                await _bearService.ResponseNotBearAsync(message);
                return Ok();
            }

            // check photos
            if (message.Photo != null)
            {
                try
                {
                    await _imageProcessor.ProcessImage(message);
                }
                catch (Exception e)
                {
                    logger.Warn($"Result photo wasn't processed, because no face are found");
                    return Ok();
                }
               
                return Ok();
            }

            // check stickers
            if(message.Sticker != null)
                logger.Info($"Sticker file id: {message.Sticker.FileId}");


            if (message == null)
                return Ok();

            if (message.Sticker == null && _commandStore.TryToGet(message.Text, out var command))
                await command.Execute(message);

            else if (update.Type == UpdateType.Message && message.Sticker == null)
            {
                var sensitiveRecognizeCmd = new SensitiveRegonizeResponseCommand();
                    var result = await sensitiveRecognizeCmd.Execute(message);

                    await SensitiveProcessor.Process(result.Intonation, result.Value, message);
            }
            else
            {
                await ApiProvider.DisplayMenu(message.Chat.Id, "Чо ты щас сморозил? Может тебе совет дать какой-то?",
                true, "Совет");
            }

            return Ok();
        }

        private async Task EnsureUserCreated(Message message)
        {
            using (var dbContext = new DatabaseContext())
            {
                var exists = dbContext.Users.Any(u => u.Username == message.Chat.Username);

                var chat = message.Chat;

                if (!exists)
                {
                    var user = new BotUser()
                    {
                        ChatId = chat.Id.ToString(),
                        FirstName = chat.FirstName,
                        LastName = chat.LastName,
                        Username =  chat.Username
                    };

                    dbContext.Users.Add(user);

                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private async Task UpdateUserLastMessage(Message message)
        {
            using (var dbContext = new DatabaseContext())
            {
                var user = dbContext.Users.FirstOrDefault(u => u.Username == message.Chat.Username);

                user.LastMessage = DateTime.UtcNow;

                await dbContext.SaveChangesAsync();
            }
        }

        private bool CheckLastBearExpired(Message message)
        {
            using (var context = new DatabaseContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Username == message.Chat.Username);

                var utcNow = DateTime.UtcNow;

                var difference = utcNow - user.LastBear;

                if (difference > _maxBearTimeSpan)
                    return true;

                return false;
            }
        }
    }
}