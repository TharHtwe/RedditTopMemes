using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace RedditTopMemes.Controllers
{
    public abstract class RedditTopMemesControllerBase: AbpController
    {
        protected RedditTopMemesControllerBase()
        {
            LocalizationSourceName = RedditTopMemesConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
