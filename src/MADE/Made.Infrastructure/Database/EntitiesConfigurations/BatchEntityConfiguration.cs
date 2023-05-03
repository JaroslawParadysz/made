using Made.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Made.Infrastructure.Database.EntitiesConfigurations;

public class BatchEntityConfiguration : IEntityTypeConfiguration<Batch>
{
    public void Configure(EntityTypeBuilder<Batch> builder)
    {
        builder.HasKey(b => b.Reference);
        builder
            .HasMany(b => b.AllocatedOrderLines)
            .WithOne().HasForeignKey(b => b.BatchReference);
    }
}