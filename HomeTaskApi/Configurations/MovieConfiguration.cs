using HomeTaskApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeTaskApi.Configurations
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(x => x.Desc)
                   .IsRequired()
                   .HasMaxLength(300);
            builder.Property(x => x.CostPrice)
                   .IsRequired();
            builder.Property(x => x.Price)
                   .IsRequired();

            builder.HasOne(x => x.Genre)
                   .WithMany(x => x.Movies)
                   .HasForeignKey(x => x.GenreId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
