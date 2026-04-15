using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection.Emit;
namespace BookStore.Models
{
	public class BookStoreContext: IdentityDbContext<ApplicationUser>
	{
        public virtual DbSet<Customer> Customers { get; set; }
		public virtual DbSet<Order> Orders { get; set; }
		public virtual DbSet<Author> Authors  { get; set; }
		public virtual DbSet<Book> Books { get; set; }
		public virtual DbSet<Category> Categories { get; set; }
		public virtual DbSet<OrderDetails> OrderDetails { get; set; }
		public virtual DbSet<AuthorBook> AuthorBooks { get; set; }


		public BookStoreContext() 
		{
		}
		public BookStoreContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
		{

		}
		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//{
		//	//optionsBuilder.UseSqlServer("Server=.;Database=MY_BookStore;Trusted_Connection=True;Encrypt=False; trustservercertificate=True;");
		//	base.OnConfiguring(optionsBuilder);
		//}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfigurationsFromAssembly(typeof(Customer).Assembly);
			base.OnModelCreating(builder);
		}


	}
}
