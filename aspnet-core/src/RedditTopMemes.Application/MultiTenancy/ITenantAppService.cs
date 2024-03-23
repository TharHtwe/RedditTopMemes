using Abp.Application.Services;
using RedditTopMemes.MultiTenancy.Dto;

namespace RedditTopMemes.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

