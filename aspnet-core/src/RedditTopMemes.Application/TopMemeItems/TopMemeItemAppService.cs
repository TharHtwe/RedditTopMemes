using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using RedditTopMemes.TopMemes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditTopMemes.TopMemes
{
    [AbpAuthorize]
    public class TopMemeItemAppService : ITopMemeItemAppService
    {
        private readonly IRepository<TopMemeItem> _topMemeItemRepository;

        public TopMemeItemAppService(IRepository<TopMemeItem> topMemeItemrepository)
        {
            _topMemeItemRepository = topMemeItemrepository;
        }
    }
}
