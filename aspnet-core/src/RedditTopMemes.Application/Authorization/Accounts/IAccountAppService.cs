using System.Threading.Tasks;
using Abp.Application.Services;
using RedditTopMemes.Authorization.Accounts.Dto;

namespace RedditTopMemes.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
