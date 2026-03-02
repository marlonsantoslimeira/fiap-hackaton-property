using AgroSolutions.Properties.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace AgroSolutions.Properties.Data.Configurations
{
    public class CultureConfig : IEntityTypeConfiguration<Culture>
    {
        public void Configure(EntityTypeBuilder<Culture> builder)
        {
            builder
                .ToTable("cultures");

            builder
                .HasKey(c => c.Id);
            builder
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder
                .Property(c => c.MaxTemperature)
                .IsRequired();
            builder
                .Property(c => c.MinTemperature)
                .IsRequired();
            builder
                .Property(c => c.MaxMoist)
                .IsRequired();
            builder
                .Property(c => c.MinMoist)
                .IsRequired();
            builder
                .Property(c => c.MaxPrecipitation)
                .IsRequired();

            builder
                .Property(c => c.CreatedAt)
                .IsRequired();
        }
    }
}
