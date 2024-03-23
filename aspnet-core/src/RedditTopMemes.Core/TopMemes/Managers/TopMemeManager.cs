using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.UI;
using Newtonsoft.Json;
using RedditTopMemes.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RedditTopMemes.TopMemes.Managers
{
    public class TopMemeManager : DomainService, ITopMemeManager
    {
        private const string RedditBaseUrl = "https://www.reddit.com";
        private const string SubredditPath = "/r/memes/";
        private const string TopPath = "top.json";
        private const int Limit = 20;

        private readonly IRepository<TopMeme> _topMemeRepository;
        private readonly IRepository<Meme, string> _memeRepository;

        public TopMemeManager(IRepository<TopMeme> topMemeRepository, IRepository<Meme, string> memeRepository)
        {
            _topMemeRepository = topMemeRepository;
            _memeRepository = memeRepository;
        }

        public async Task<TopMeme> CreateTopMemeAsync()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "RedditTopMemes App");
                client.BaseAddress = new Uri(RedditBaseUrl);

                var response = await client.GetAsync($"{SubredditPath}{TopPath}?t=day&limit={Limit}&sort=top");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<RedditResponse>(content);

                if (result?.Data?.Children == null)
                {
                    throw new UserFriendlyException("No records found");
                }

                var topMeme = new TopMeme();

                int rank = 1;
                foreach (var child in result.Data.Children)
                {
                    var post = child.Data;

                    var meme = await _memeRepository.FirstOrDefaultAsync(x => x.Id == post.Id);
                    if (meme == null)
                    {
                        meme = await _memeRepository.InsertAsync(new Meme
                        {
                            Id = post.Id,
                            Title = post.Title,
                            Url = post.Url,
                            CreatedDate = DateTime.UnixEpoch.AddSeconds(post.Created_UTC).ToLocalTime(),
                        });
                    }

                    topMeme.TopMemeItems.Add(new TopMemeItem
                    {
                        Score = post.Score,
                        NumComments = post.Num_Comments,
                        Rank = rank,
                        MemeId = meme.Id
                    });

                    rank++;
                }

                await _topMemeRepository.InsertAsync(topMeme);

                return topMeme;
            }
        }
    }
}
