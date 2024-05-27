using BigonWebShoppingApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BigonWebShoppingApp.EFConfigurations
{
    public class SubscriberConfiguration : IEntityTypeConfiguration<Subscriber>
    {
        public void Configure(EntityTypeBuilder<Subscriber> builder)
        {
            builder.ToTable("Subscribers");
            builder.HasKey(x => x.Email);
            builder.Property(x => x.Email).HasMaxLength(100);
            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnType("Date");
            builder.Property(x=>x.IsAccept).HasDefaultValue("false");
        }
    }
}
