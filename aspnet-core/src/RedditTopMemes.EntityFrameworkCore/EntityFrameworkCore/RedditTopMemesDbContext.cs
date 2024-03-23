using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using RedditTopMemes.Authorization.Roles;
using RedditTopMemes.Authorization.Users;
using RedditTopMemes.MultiTenancy;
using RedditTopMemes.TopMemes;

namespace RedditTopMemes.EntityFrameworkCore
{
    public class RedditTopMemesDbContext : AbpZeroDbContext<Tenant, Role, User, RedditTopMemesDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<TopMeme> TopMemes { get; set; }

        public DbSet<TopMemeItem> TopMemeItems { get; set; }

        public DbSet<Meme> Memes { get; set; }

        public RedditTopMemesDbContext(DbContextOptions<RedditTopMemesDbContext> options)
            : base(options)
        {
        }


    }
}
