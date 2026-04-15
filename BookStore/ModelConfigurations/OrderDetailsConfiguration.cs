using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.ModelConfigurations
{
	public class OrderDetailsConfiguration : IEntityTypeConfiguration<OrderDetails>
	{
		public void Configure(EntityTypeBuilder<OrderDetails> builder)
		{
			builder.HasKey(od => new { od.OrderID, od.BookID });


		}
	}
}
