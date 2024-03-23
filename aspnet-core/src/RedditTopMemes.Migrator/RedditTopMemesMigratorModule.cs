using Microsoft.Extensions.Configuration;
using Castle.MicroKernel.Registration;
using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using RedditTopMemes.Configuration;
using RedditTopMemes.EntityFrameworkCore;
using RedditTopMemes.Migrator.DependencyInjection;

namespace RedditTopMemes.Migrator
{
    [DependsOn(typeof(RedditTopMemesEntityFrameworkModule))]
    public class RedditTopMemesMigratorModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public RedditTopMemesMigratorModule(RedditTopMemesEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

            _appConfiguration = AppConfigurations.Get(
                typeof(RedditTopMemesMigratorModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                RedditTopMemesConsts.ConnectionStringName
            );

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(
                typeof(IEventBus), 
                () => IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                )
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(RedditTopMemesMigratorModule).GetAssembly());
            ServiceCollectionRegistrar.Register(IocManager);
        }
    }
}
