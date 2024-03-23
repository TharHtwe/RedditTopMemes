using Abp.Application.Services;
using Abp.Application.Services.Dto;
using RedditTopMemes.TopMemes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditTopMemes.TopMemes
{
    public interface ITopMemeAppService : IApplicationService
    {
        Task<PagedResultDto<TopMemeDto>> GetAll(PagedResultRequestDto input);
        Task<TopMemeDto> GetAsync(EntityDto<int> input);
        Task<TopMemeDto> GetLatest();
        Task SendToTelegramBot();
        Task<TopMemeDto> CreateAsync();
        Task DeleteAsync(EntityDto<int> input);
    }
}
