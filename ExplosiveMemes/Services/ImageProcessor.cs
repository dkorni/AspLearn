using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ExplosiveMemes.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;

namespace ExplosiveMemes.Services
{
    public class ImageProcessor
    {
        private TelegramBotClient _client = Bot.Get().Result;

        private string _waitMes = "Подожди дружочек, сейчас дед тебя подлатает...";

        private string _waitSticker = "CAACAgIAAxkBAAEBMixfNXL4bZerFmx7IZNkXa5I8C5GdwACNcsAAmOLRgzVH3jFvLg04hoE";

        private Logger logger = LogManager.GetCurrentClassLogger();

        private int _maxBomjs = 0;

        private string bomjPhotoPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Faces");

        public ImageProcessor()
        {
            _maxBomjs = Directory.EnumerateFiles(bomjPhotoPath).ToList().Count;
        }

        public async Task ProcessImage(Message message)
        {
            // send sticker
            await _client.SendStickerAsync(message.Chat.Id, _waitSticker);

            // send message to user to wait
            await _client.SendTextMessageAsync(message.Chat.Id, _waitMes);

            var photoId = message.Photo.FirstOrDefault()?.FileId;

            logger.Warn($"User {message.Chat.Username} sent photo {photoId}");

            var dstFilePath = await Download(photoId);

            // take bomj photo
            var bomjRandom = new Random().Next(1, _maxBomjs);

            var srcFilePath = $"{bomjPhotoPath}\\bomj{bomjRandom}.jpg";

            logger.Warn($"User photo is processed as bomj {bomjRandom}");

            // result path 
            var resultFileName = $"{Guid.NewGuid()}.jpg";

            var resultPath = $"{AppContext.BaseDirectory}ImagesResult";

            // check if directory created
            if (!Directory.Exists(resultPath))
                Directory.CreateDirectory(resultPath);

            resultPath += $"\\{resultFileName}";

            // todo make respnse when face is not recognized
            // use external python service to process image
            var isProcessed = await ProcessExternal(new ImageProceInfo()
            {
                Destination = dstFilePath,
                Source = srcFilePath,
                Output = resultPath
            });

            if (!isProcessed)
            {
                await _client.SendTextMessageAsync(message.Chat.Id,
                    "Сорян, тут сделать нечего хорошего не получится, либо морда кривая, либо я кривой. Попробуй еще разок 😉");
                logger.Warn($"Result photo wasn't processed, because no face are found");
                return;
            }

            logger.Warn($"Result photo  saved as {resultFileName}");

            // finally send result photo back to user
            using (var stream = new FileStream(resultPath, FileMode.Open))
            {
                var input = new InputOnlineFile(stream);
                await _client.SendPhotoAsync(message.Chat.Id, input);
                await _client.SendTextMessageAsync(message.Chat.Id, "Теперь выглядишь гораздо лучше братан!");
            }

            // save img
            // var bitmap = new Bitmap()
        }

        private async Task<string> Download(string photoId)
        {
            using (var httpClient = new HttpClient())
            {
                // get file path for download file
                var fileInfoRespnse =
                    await httpClient.GetAsync($"https://api.telegram.org/bot{AppSettings.Key}/getFile?file_id={photoId}");

                var jsonFileInfo = await fileInfoRespnse.Content.ReadAsStringAsync();

                var fileInfoResultDictionary = JsonConvert.DeserializeObject<JObject>(jsonFileInfo);

                var fileInfo = (JObject)fileInfoResultDictionary["result"];

                var file_path = fileInfo["file_path"].ToString();

                // download file
                var downloadedFileResponse =
                    await httpClient.GetAsync($"https://api.telegram.org/file/bot{AppSettings.Key}/{file_path}");

                var fileName = file_path.Split('/')[1];

                var localPath = $"{AppContext.BaseDirectory}ImagesToProc\\";

                // check if directory created
                if (!Directory.Exists(localPath))
                    Directory.CreateDirectory(localPath);

                localPath += fileName;

                // save file to local storage
                    using (var stream = new FileStream(localPath, FileMode.Create))
                {
                    await downloadedFileResponse.Content.CopyToAsync(stream);
                    stream.Close();
                }

                logger.Warn($"Original photo {photoId} saved as {fileName}");

                return localPath;
            }
        }

        private async Task<bool> ProcessExternal(ImageProceInfo imageProceInfo)
        {
            using (var httpClient = new HttpClient())
            {
                var serializedProcInfo = JsonConvert.SerializeObject(imageProceInfo);

                using (var request = new HttpRequestMessage(HttpMethod.Post, "http://127.0.0.1:5000/img/"))
                {
                    request.Content = new StringContent(serializedProcInfo);

                    var response = await httpClient.SendAsync(request);

                    if (response.StatusCode == HttpStatusCode.BadRequest)
                        return false;
                }
            }

            return true;
        }
    }
}
