using System.Threading.Tasks;
using Abp.Application.Services;
using RedditTopMemes.Sessions.Dto;

namespace RedditTopMemes.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
