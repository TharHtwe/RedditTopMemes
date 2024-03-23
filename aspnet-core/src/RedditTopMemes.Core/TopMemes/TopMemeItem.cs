using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditTopMemes.TopMemes
{
    public class TopMemeItem : Entity
    {
        public int TopMemeId { get; set; }
        public string MemeId { get; set; }
        public int Score { get; set; }
        public int NumComments { get; set; }
        public int Rank { get; set; }

        [ForeignKey(nameof(TopMemeId))]
        public virtual TopMeme TopMeme { get; set; }

        [ForeignKey(nameof(MemeId))]
        public virtual Meme Meme { get; set; }
    }
}
