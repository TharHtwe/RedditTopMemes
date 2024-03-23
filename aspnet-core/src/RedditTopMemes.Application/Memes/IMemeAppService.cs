using Abp.Application.Services;
using Abp.Application.Services.Dto;
using RedditTopMemes.Memes.Dto;
using RedditTopMemes.TopMemes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditTopMemes.TopMemes
{
    public interface IMemeAppService : IApplicationService
    {
        Task<MemeHistoryResultDto> GetRankHistoriesAsync(EntityDto<string> input);
    }
}
