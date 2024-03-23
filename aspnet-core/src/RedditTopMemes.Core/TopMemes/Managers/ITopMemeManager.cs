using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditTopMemes.TopMemes.Managers
{
    public interface ITopMemeManager : IDomainService
    {
        Task<TopMeme> CreateTopMemeAsync();
    }
}
