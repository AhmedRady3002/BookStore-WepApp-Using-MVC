using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.ModelConfigurations
{
	public class OredreConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.Property(o => o.OrderID).UseIdentityColumn(1, 1);
			builder.Property(o=>o.OrederDate).HasDefaultValueSql("SYSDATETIME()");

            builder.Property(o => o.OrderStatus)
             .HasConversion<string>();

        }
	}
}
