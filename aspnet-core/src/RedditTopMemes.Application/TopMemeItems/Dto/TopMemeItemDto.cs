using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditTopMemes.TopMemes.Dto
{
    [AutoMap(typeof(TopMemeItem))]
    public class TopMemeItemDto : EntityDto
    {
        public string MemeId { get; set; }
        public int TopMemeId { get; set; }
        public int Score { get; set; }
        public int NumComments { get; set; }
        public int Rank { get; set; }
    }
}
