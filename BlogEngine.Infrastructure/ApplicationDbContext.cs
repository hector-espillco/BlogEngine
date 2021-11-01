using BlogEngine.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Entities = BlogEngine.Domain.Entities;

namespace BlogEngine.Infrastructure
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Entities.Author> Author { get; set; }
        public DbSet<Entities.Post> Post { get; set; }
        public DbSet<Entities.Comment> Comment { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Comment>()
                .HasOne(c => c.Author)
                .WithMany(u => u.Comments)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
