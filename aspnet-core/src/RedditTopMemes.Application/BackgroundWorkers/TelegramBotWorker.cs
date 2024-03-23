using Abp.Threading.BackgroundWorkers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Polling;
using Telegram.Bot;
using Abp.Dependency;
using RedditTopMemes.Configuration;
using Abp.Configuration;
using Microsoft.Extensions.Configuration;

namespace RedditTopMemes.BackgroundWorkers
{
    public class TelegramBotWorker : BackgroundWorkerBase, ISingletonDependency
    {
        private readonly TelegramConfig _telegramConfig;

        public TelegramBotWorker(TelegramConfig telegramConfig)
        {
            _telegramConfig = telegramConfig;
        }

        public override void Start()
        {
            TelegramBotClient telegramBotClient = new TelegramBotClient(_telegramConfig.BotToken);
            telegramBotClient.StartReceiving(UpdateHandler, ErrorHandler);
        }

        public override void Stop()
        {
            base.Stop();
        }

        private async Task UpdateHandler(ITelegramBotClient client, Telegram.Bot.Types.Update update, CancellationToken token)
        {
            if (update?.Message?.Text == null)
                return;
            
            if (update.Message.Text == "/hello")
            {
                await client.SendTextMessageAsync(update.Message.Chat.Id, "Hello! You can now get top 20 Reddit Memes List.");
                await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.ChatId, update.Message.Chat.Id.ToString());
            }
            else
            {
                await client.SendTextMessageAsync(update.Message.Chat.Id, "Please send /hello to start");
            }
        }

        private async Task ErrorHandler(ITelegramBotClient client, Exception exception, CancellationToken token)
        {

        }
    }
}
