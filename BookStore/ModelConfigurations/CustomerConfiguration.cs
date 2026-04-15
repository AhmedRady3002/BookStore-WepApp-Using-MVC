using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.ModelConfigurations
{
	public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
	{
		public void Configure(EntityTypeBuilder<Customer> builder)
		{
			builder.Property(c=>c.CustomerID).UseIdentityColumn(seed:1,increment:1);
			builder.HasAlternateKey(c=>c.CustomerPhone);
            /*HasPrincipalKey يعني الفورين هايشاور علي ايه في الجدول التاني فانت حددتله هايشاور علي ايه*/
            builder.HasMany(c => c.Orders)
               .WithOne(o => o.Customer)
              .HasForeignKey(o => o.CustomerID)
              .OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(c => c.CustomerPhone)
                   .IsUnique();

        }
	}
}
