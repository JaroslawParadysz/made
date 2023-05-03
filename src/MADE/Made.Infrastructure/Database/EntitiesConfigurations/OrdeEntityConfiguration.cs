using Made.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Made.Infrastructure.Database.EntitiesConfigurations;

public class OrdeEntityConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);
        builder.HasMany(o => o.Lines)
            .WithOne()
            .HasForeignKey(ol => ol.OrderId);
    }
}