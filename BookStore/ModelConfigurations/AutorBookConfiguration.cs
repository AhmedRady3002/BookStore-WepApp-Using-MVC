using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.ModelConfigurations
{
	public class AutorBookConfiguration : IEntityTypeConfiguration<AuthorBook>
	{
		public void Configure(EntityTypeBuilder<AuthorBook> builder)
		{
			builder.HasKey(AB => new { AB.Bookcode, AB.Author_no });

			
			builder.HasOne(ab => ab.Book)
				   .WithMany(b => b.AuthorBooks)
				   .HasForeignKey(ab => ab.Bookcode)
				   .OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(ab => ab.Author)
				   .WithMany(a => a.AuthorBooks)
				   .HasForeignKey(ab => ab.Author_no)
				   .OnDelete(DeleteBehavior.Cascade);


		}
	}
}
