using AgroSolutions.Properties.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace AgroSolutions.Properties.Data.Configurations
{
    public class FieldConfig : IEntityTypeConfiguration<Field>
    {
        public void Configure(EntityTypeBuilder<Field> builder)
        {
            builder
                .ToTable("fields");

            builder
                .HasKey(f => f.Id);
            builder.Property(f => f.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(f => f.PropertyId)
                .IsRequired();

            builder
                .Property(f => f.CultureId)
                .IsRequired();

            builder
                .Property(f => f.CreatedAt)
                .IsRequired();

            builder
                .HasOne(f => f.Property)
                .WithMany(p => p.Fields);

            builder
                .HasOne(f => f.Culture)
                .WithMany();
        }
    }
}
