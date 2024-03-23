using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using RedditTopMemes.Configuration;
using Abp.Threading.BackgroundWorkers;
using RedditTopMemes.BackgroundWorkers;

namespace RedditTopMemes.Web.Host.Startup
{
    [DependsOn(
       typeof(RedditTopMemesWebCoreModule))]
    public class RedditTopMemesWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public RedditTopMemesWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(RedditTopMemesWebHostModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            var workManager = IocManager.Resolve<IBackgroundWorkerManager>();
            workManager.Add(IocManager.Resolve<TopMemeCrawler>());
            workManager.Add(IocManager.Resolve<TelegramBotWorker>());
        }
    }
}
