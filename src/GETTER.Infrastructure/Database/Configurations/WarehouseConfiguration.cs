using GETTER.Domain.Warehouses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GETTER.Infrastructure.Database.Configurations;

internal sealed class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        builder.ToTable("Warehouses");

        builder.Property<long>("InternalId").HasColumnName("Id").ValueGeneratedOnAdd();
        builder.HasKey("InternalId").IsClustered();

        builder.Property(w => w.Id).HasColumnName("PublicId").ValueGeneratedNever();
        builder.HasIndex(w => w.Id).IsUnique();

        builder.Property(w => w.Name).IsRequired().HasMaxLength(50);
        builder.Property(w => w.Description).HasMaxLength(200);
        builder.Property(w => w.Address).HasMaxLength(200);
        builder.Property(w => w.Type).HasConversion<string>().HasMaxLength(20);

        builder.Property(w => w.Symbol).HasConversion(
            symbol => symbol.Value, 
            text => WarehouseSymbol.Create(text))
            .IsRequired().HasMaxLength(10);

        builder.HasIndex(w => w.Symbol).IsUnique();
    }
}
