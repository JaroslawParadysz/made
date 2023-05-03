using Made.Domain;
using Made.Infrastructure.Database.EntitiesConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Made.Infrastructure.Database;

public class Context : DbContext
{

    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("made")
            .ApplyConfigurationsFromAssembly(typeof(BatchEntityConfiguration).Assembly);
    }
    
    public DbSet<Batch> Batches { get; set; }
}