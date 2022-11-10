using Microsoft.EntityFrameworkCore;
using SeBackend.Common.Models;

namespace comment_sv.Models
{
    public class CommentContext : DbContext
    {
        public CommentContext(DbContextOptions<CommentContext> options) : base(options)
        {
            
        }

        public DbSet<Comment> Comments { get; set; } = null!;
    }
}