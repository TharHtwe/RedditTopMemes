using System.Threading.Tasks;
using RedditTopMemes.Configuration.Dto;

namespace RedditTopMemes.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
