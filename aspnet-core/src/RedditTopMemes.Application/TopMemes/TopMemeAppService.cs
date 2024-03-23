using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.BackgroundJobs;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using AutoMapper.Internal.Mappers;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RedditTopMemes.BackgroundWorkers;
using RedditTopMemes.Configuration;
using RedditTopMemes.Responses;
using RedditTopMemes.TopMemes.Dto;
using RedditTopMemes.TopMemes.Managers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;

namespace RedditTopMemes.TopMemes
{
    [AbpAuthorize]
    public class TopMemeAppService : ApplicationService, ITopMemeAppService
    {
        private readonly IRepository<TopMeme> _topMemeRepository;
        private readonly ITopMemeManager _topMemeManager;
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly TelegramConfig _telegramConfig;

        public TopMemeAppService(IRepository<TopMeme> topMemeRepository, IRepository<TopMemeItem> topMemeItemRepository, ITopMemeManager topMemeManager, IBackgroundJobManager backgroundJobManager, TelegramConfig telegramConfig)
        {
            _topMemeRepository = topMemeRepository;
            _topMemeManager = topMemeManager;
            _backgroundJobManager = backgroundJobManager;
            _telegramConfig = telegramConfig;
        }

        public async Task<TopMemeDto> CreateAsync()
        {
            var topMeme = await _topMemeManager.CreateTopMemeAsync();
            return ObjectMapper.Map<TopMemeDto>(topMeme);
        }

        public async Task<TopMemeDto> GetAsync(EntityDto<int> input)
        {
            var topMeme = await _topMemeRepository.GetAll()
                                    .Include(x => x.TopMemeItems)
                                        .ThenInclude(t => t.Meme)
                                    .Select(x => new TopMemeDto
                                    {
                                        Id = x.Id,
                                        CreatedDate = x.CreatedDate,
                                        TopMemeItems = ObjectMapper.Map<List<TopMemeItemDetailDto>>(x.TopMemeItems)
                                    })
                                    .FirstOrDefaultAsync(x => x.Id == input.Id);

            if (topMeme == null) throw new UserFriendlyException("Not found");

            return topMeme;
        }

        public async Task<PagedResultDto<TopMemeDto>> GetAll(PagedResultRequestDto input)
        {
            var query = _topMemeRepository.GetAll()
                                    .Include(x => x.TopMemeItems)
                                        .ThenInclude(t => t.Meme);

            var topMemes = query.Select(x => new TopMemeDto
                                {
                                    Id = x.Id,
                                    CreatedDate = x.CreatedDate,
                                    TopMemeItems = ObjectMapper.Map<List<TopMemeItemDetailDto>>(x.TopMemeItems)
                                })
                                .OrderByDescending(x => x.Id)
                                .PageBy(input);

            return new PagedResultDto<TopMemeDto>(query.Count(), await topMemes.ToListAsync());
        }

        public async Task<TopMemeDto> GetLatest()
        {
            var topMeme = await _topMemeRepository.GetAll()
                                    .Include(x => x.TopMemeItems)
                                        .ThenInclude(t => t.Meme)
                                    .Select(x => new TopMemeDto
                                    {
                                        Id = x.Id,
                                        CreatedDate = x.CreatedDate,
                                        TopMemeItems = ObjectMapper.Map<List<TopMemeItemDetailDto>>(x.TopMemeItems)
                                    })
                                    .OrderByDescending(x => x.Id)
                                    .FirstOrDefaultAsync();

            return topMeme;
        }

        public async Task SendToTelegramBot()
        {
            var latest = await this.GetLatest();

            if (latest == null)
            {
                throw new UserFriendlyException("No top memes found. Please crawl latest and try again.");
            }

            await _backgroundJobManager.EnqueueAsync<TelegramSenderJob, int>(latest.Id);
        }

        public async Task DeleteAsync(EntityDto<int> input)
        {
            var topMeme = await _topMemeRepository.GetAsync(input.Id);
            await _topMemeRepository.DeleteAsync(topMeme);
        }
        
    }
}
