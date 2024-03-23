using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RedditTopMemes.Configuration;
using RedditTopMemes.TopMemes;
using RedditTopMemes.TopMemes.Dto;
using RedditTopMemes.TopMemes.Managers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RedditTopMemes.BackgroundWorkers
{
    public class TelegramSenderJob : AsyncBackgroundJob<int>, ITransientDependency
    {
        private readonly IRepository<TopMeme> _topMemeRepository;
        private readonly TelegramConfig _telegramConfig;
        private readonly IObjectMapper _objectMapper;

        public TelegramSenderJob(IRepository<TopMeme> topMemeRepository, TelegramConfig telegramConfig, IObjectMapper objectMapper)
        {
            _topMemeRepository = topMemeRepository;
            _telegramConfig = telegramConfig;
            _objectMapper = objectMapper;
        }

        [UnitOfWork]
        public override async Task ExecuteAsync(int topMemeId)
        {
            var topMeme = await _topMemeRepository.GetAll()
                                    .Include(x => x.TopMemeItems)
                                        .ThenInclude(t => t.Meme)
                                    .Select(x => new TopMemeDto
                                    {
                                        Id = x.Id,
                                        CreatedDate = x.CreatedDate,
                                        TopMemeItems = _objectMapper.Map<List<TopMemeItemDetailDto>>(x.TopMemeItems)
                                    })
                                    .Where(x => x.Id == topMemeId)
                                    .FirstOrDefaultAsync();

            // Generate the report file
            var fileName = "TopMemesReport.txt";
            using (var writer = new StreamWriter(fileName))
            {
                foreach (var meme in topMeme.TopMemeItems)
                {
                    await writer.WriteLineAsync($"Title: {meme.Title}");
                    await writer.WriteLineAsync($"URL: {meme.Url}");
                    await writer.WriteLineAsync($"Score: {meme.Score}");
                    await writer.WriteLineAsync($"Number of Comments: {meme.NumComments}");
                    await writer.WriteLineAsync(new string('-', 50));
                }
            }

            // Send the report file via Telegram Chatbot
            using (var client = new HttpClient())
            {
                var chatId = SettingManager.GetSettingValueForApplication(AppSettingNames.ChatId);
                if (string.IsNullOrEmpty(chatId)) chatId = _telegramConfig.ChatId;

                var url = $"https://api.telegram.org/bot{_telegramConfig.BotToken}/sendDocument?chat_id={chatId}";

                using (var form = new MultipartFormDataContent())
                using (var fileContent = new ByteArrayContent(File.ReadAllBytes(fileName)))
                {
                    fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                    form.Add(fileContent, "document", fileName);

                    await client.PostAsync(url, form);
                }
            }

            // Delete the report file
            File.Delete(fileName);
        }
    }
}
