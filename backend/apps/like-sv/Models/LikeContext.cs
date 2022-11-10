using Microsoft.EntityFrameworkCore;
using SeBackend.Common.Models;

namespace like_sv.Models
{
    public class LikeContext : DbContext
    {
        public LikeContext(DbContextOptions<LikeContext> options) : base(options)
        {
            
        }

        public DbSet<Like> Likes { get; set; } = null!;
    }
}