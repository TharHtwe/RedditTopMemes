using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using RedditTopMemes.EntityFrameworkCore;
using RedditTopMemes.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace RedditTopMemes.Web.Tests
{
    [DependsOn(
        typeof(RedditTopMemesWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class RedditTopMemesWebTestModule : AbpModule
    {
        public RedditTopMemesWebTestModule(RedditTopMemesEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(RedditTopMemesWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(RedditTopMemesWebMvcModule).Assembly);
        }
    }
}