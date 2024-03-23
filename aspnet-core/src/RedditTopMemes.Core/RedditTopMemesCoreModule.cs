using Abp.Localization;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Runtime.Security;
using Abp.Threading.BackgroundWorkers;
using Abp.Timing;
using Abp.Zero;
using Abp.Zero.Configuration;
using RedditTopMemes.Authorization.Roles;
using RedditTopMemes.Authorization.Users;
using RedditTopMemes.Configuration;
using RedditTopMemes.Localization;
using RedditTopMemes.MultiTenancy;
using RedditTopMemes.Timing;

namespace RedditTopMemes
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class RedditTopMemesCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            // Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            RedditTopMemesLocalizationConfigurer.Configure(Configuration.Localization);

            // Enable this line to create a multi-tenant application.
            Configuration.MultiTenancy.IsEnabled = RedditTopMemesConsts.MultiTenancyEnabled;

            // Configure roles
            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            Configuration.Settings.Providers.Add<AppSettingProvider>();
            
            Configuration.Localization.Languages.Add(new LanguageInfo("fa", "فارسی", "famfamfam-flags ir"));
            
            Configuration.Settings.SettingEncryptionConfiguration.DefaultPassPhrase = RedditTopMemesConsts.DefaultPassPhrase;
            SimpleStringCipher.DefaultPassPhrase = RedditTopMemesConsts.DefaultPassPhrase;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(RedditTopMemesCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.Resolve<AppTimes>().StartupTime = Clock.Now;
        }
    }
}
