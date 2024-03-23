using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditTopMemes.Responses
{
    public class RedditResponse
    {
        public RedditData Data { get; set; }
    }

    public class RedditData
    {
        public List<RedditChild> Children { get; set; }
    }

    public class RedditChild
    {
        public RedditPost Data { get; set; }
    }

    public class RedditPost
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public int Score { get; set; }
        public int Num_Comments { get; set; }
        public double Created_UTC { get; set; }
    }
}
