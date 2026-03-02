using AgroSolutions.Properties.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace AgroSolutions.Properties.Data.Configurations
{
    public class PropertyConfig : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder
                .ToTable("properties");

            builder
                .HasKey(f => f.Id);
            builder.Property(f => f.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(f => f.Location)
                .IsRequired();

            builder
                .Property(f => f.CreatedAt)
                .IsRequired();

            builder
                .HasMany(p => p.Fields)
                .WithOne(f => f.Property)
                .HasForeignKey(f => f.PropertyId);
        }
    }
}
