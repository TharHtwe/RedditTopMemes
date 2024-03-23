using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditTopMemes.TopMemes.Dto
{
    [AutoMap(typeof(TopMeme))]
    public class TopMemeDto : EntityDto
    {
        public DateTime CreatedDate { get; set; }

        public List<TopMemeItemDetailDto> TopMemeItems { get; set; }
    }
}
