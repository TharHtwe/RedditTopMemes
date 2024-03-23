using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditTopMemes.Memes.Dto
{
    public class MemeHistoryResultDto
    {
        public DateTime MemePostedDate { get; set; }
        public IEnumerable<MemeRankHistoryDto> Histories { get; set;}
    }

    public class MemeRankHistoryDto
    {
        public DateTime Date { get; set; }
        public int Rank { get; set; }
    }
}
