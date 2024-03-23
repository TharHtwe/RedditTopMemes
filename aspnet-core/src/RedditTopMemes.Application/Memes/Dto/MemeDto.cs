using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditTopMemes.TopMemes.Dto
{
    [AutoMap(typeof(Meme))]
    public class MemeDto : EntityDto
    {
        public string MemeId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
