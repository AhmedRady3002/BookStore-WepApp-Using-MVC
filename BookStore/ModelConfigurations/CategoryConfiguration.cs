using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.ModelConfigurations
{
	public class CategoryConfiguration : IEntityTypeConfiguration<Category>
	{
	
         public void Configure(EntityTypeBuilder<Category> builder)
		 {
			builder.Property(c=>c.Id).UseIdentityColumn(1,1);

		 }
	}
}
