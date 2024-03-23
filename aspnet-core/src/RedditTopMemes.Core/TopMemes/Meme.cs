using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditTopMemes.TopMemes
{
    public class Meme : Entity<string>
    {
        [MaxLength(7)]
        public override string Id { get; set; }
        public string Title { get; set;}
        public string Url { get; set;}
        public DateTime CreatedDate { get; set; }
    }
}
