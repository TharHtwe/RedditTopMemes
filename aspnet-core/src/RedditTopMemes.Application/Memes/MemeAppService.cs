using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using RedditTopMemes.Memes.Dto;
using RedditTopMemes.TopMemes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditTopMemes.TopMemes
{
    [AbpAuthorize]
    public class MemeAppService : ApplicationService, IMemeAppService
    {
        private readonly IRepository<Meme, string> _memeRepository;
        private readonly IRepository<TopMemeItem> _topMemeItemRepository;

        public MemeAppService(IRepository<Meme, string> memeRepository, IRepository<TopMemeItem> topMemeItemRepository)
        {
            _memeRepository = memeRepository;
            _topMemeItemRepository = topMemeItemRepository;
        }

        public async Task<MemeHistoryResultDto> GetRankHistoriesAsync(EntityDto<string> input)
        {
            var meme = await _memeRepository.GetAsync(input.Id);

            if (meme == null) throw new UserFriendlyException("Not found");

            var histories = await _topMemeItemRepository.GetAll()
                                    .Where(x => x.MemeId == meme.Id)
                                    .Select(x => new MemeRankHistoryDto
                                    {
                                        Date = x.TopMeme.CreatedDate,
                                        Rank = x.Rank
                                    })
                                    .ToListAsync();

            return new MemeHistoryResultDto
            {
                MemePostedDate = meme.CreatedDate,
                Histories = histories.Take(8)
            };
        }
    }
}
