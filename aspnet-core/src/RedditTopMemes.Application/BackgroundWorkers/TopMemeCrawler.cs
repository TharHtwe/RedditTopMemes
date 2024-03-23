using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Threading;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using Abp.Timing;
using Microsoft.Extensions.Logging;
using RedditTopMemes.Authorization.Users;
using RedditTopMemes.Configuration;
using RedditTopMemes.TopMemes.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditTopMemes.BackgroundWorkers
{
    public class TopMemeCrawler : PeriodicBackgroundWorkerBase, ISingletonDependency
    {
        private readonly ITopMemeManager _topMemeManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly CrawlerConfig _crawlerConfig;

        public TopMemeCrawler(AbpTimer timer, ITopMemeManager topMemeManager, IUnitOfWorkManager unitOfWorkManager, ISettingManager settingManager, CrawlerConfig crawlerConfig)
            : base(timer)
        {
            _topMemeManager = topMemeManager;
            _unitOfWorkManager = unitOfWorkManager;
            _crawlerConfig = crawlerConfig;

            Timer.Period = crawlerConfig.Interval * 1000 * 60;
        }

        protected override void DoWork()
        {
            if (_crawlerConfig.Enabled)
            {
                using (var unitOfWork = _unitOfWorkManager.Begin())
                {
                    Logger.Info("Crawl Top Memes from background.");

                    AsyncHelper.RunSync(() => _topMemeManager.CreateTopMemeAsync());

                    unitOfWork.Complete();
                }
            }
        }
    }
}
