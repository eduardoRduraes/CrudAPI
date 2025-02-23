using CrudAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudAPI.Data;

public class AppDataContext : DbContext
{
    public AppDataContext(DbContextOptions<AppDataContext> options) : base(options) { }
    
    public DbSet<Ong> Ongs { get; set; }
    public DbSet<Caso> Casos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ong>()
            .HasMany<Caso>(o => o.Casos)
            .WithOne(o => o.Ong)
            .HasForeignKey(o => o.OngId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}