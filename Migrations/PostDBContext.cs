using System.ComponentModel.DataAnnotations.Schema;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace backend.Migrations;

public class PostDbContext : DbContext
{
    public DbSet<Post> Post { get; set; }
    public DbSet<User> Users { get; set; }

    public PostDbContext(DbContextOptions<PostDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Posts)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.PostId);
            entity.Property(e => e.Content).IsRequired();
            entity.Property(e => e.DateTime).IsRequired();
            entity.Property(e => e.Username).IsRequired();
            entity.Property(e => e.UserId).IsRequired(false);
        });
        
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId);
            entity.Property(e => e.Username).IsRequired();
            entity.HasIndex(x => x.Username).IsUnique();
            entity.Property(e => e.Password).IsRequired();
            entity.Property(e => e.FirstName).IsRequired();
            entity.Property(e => e.LastName).IsRequired();
            entity.Property(e => e.Bio).IsRequired();
        });
    }
}