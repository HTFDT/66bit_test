using Microsoft.EntityFrameworkCore;
using web.Models;

namespace web.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Player>().ToTable("Players");
        modelBuilder.Entity<Team>().ToTable("Teams");
    }
}