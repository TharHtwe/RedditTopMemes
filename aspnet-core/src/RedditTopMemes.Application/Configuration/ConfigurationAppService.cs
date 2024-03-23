using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using RedditTopMemes.Configuration.Dto;

namespace RedditTopMemes.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : RedditTopMemesAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
