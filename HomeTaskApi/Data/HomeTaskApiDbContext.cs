using HomeTaskApi.Configurations;
using HomeTaskApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeTaskApi.Data;

public class HomeTaskApiDbContext : DbContext
{
    public HomeTaskApiDbContext(DbContextOptions<HomeTaskApiDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MovieConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    public DbSet<Genre> Genres { get; set;}
    public DbSet<Movie> Movies { get; set;}
}
