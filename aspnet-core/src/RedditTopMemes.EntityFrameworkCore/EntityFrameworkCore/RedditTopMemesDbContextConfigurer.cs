using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace RedditTopMemes.EntityFrameworkCore
{
    public static class RedditTopMemesDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<RedditTopMemesDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<RedditTopMemesDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
