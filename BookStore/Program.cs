using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookStore
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews()
            .AddViewOptions(options =>
               {
                    options.HtmlHelperOptions.ClientValidationEnabled = true;
              });

            // Add services to the container.
            builder.Services.AddControllersWithViews();
			builder.Services.AddDbContext<BookStoreContext>(op =>
			{
				op.UseSqlServer(builder.Configuration.GetConnectionString("DB_Store"));
			});

			builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
			builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
			builder.Services.AddScoped<IBookRepository, BookRepository>();
			builder.Services.AddScoped<IAuthorBookRepository, AuthorBookRepository>();
			builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
			builder.Services.AddScoped<IOrederRepository, OrderRepository>();
			builder.Services.AddScoped<IOrderdetailsRepository,OrderdetailsRepository>();
			builder.Services.AddIdentity<ApplicationUser, IdentityRole>(op => 
			{
				op.Password.RequireUppercase=false;
				op.Password.RequireNonAlphanumeric=false;
				op.Password.RequireDigit=false;
			}).AddEntityFrameworkStores<BookStoreContext>();

			builder.Services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromMinutes(120); // ??? ?????? ???
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
			});


			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
			}
			app.UseStaticFiles();

			app.UseRouting();
			app.UseSession();
			app.UseAuthentication();
			app.UseAuthorization();
			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=products}/{action=ShowProduct}/{id?}");

			app.Run();
		}
	}
}
