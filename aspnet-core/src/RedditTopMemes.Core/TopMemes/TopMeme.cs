using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditTopMemes.TopMemes
{
    public class TopMeme : Entity
    {
        public DateTime CreatedDate { get; set; }

        public TopMeme()
        {
            CreatedDate = DateTime.Now;
            TopMemeItems = new List<TopMemeItem>();
        }

        public virtual ICollection<TopMemeItem> TopMemeItems { get; set; }
    }
}
