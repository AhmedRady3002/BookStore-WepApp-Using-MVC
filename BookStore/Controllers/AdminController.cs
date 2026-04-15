using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public partial class AdminController : Controller
    {
        private readonly IAuthorRepository authorRepository;
        private readonly IBookRepository bookRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IAuthorBookRepository authorBookRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IOrederRepository orederRepository;
        private readonly BookStoreContext context;

        public AdminController
        (IAuthorRepository authorRepository, IBookRepository bookRepository,
        ICategoryRepository categoryRepository, IAuthorBookRepository authorBookRepository,
         UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,
         RoleManager<IdentityRole> roleManager, IOrederRepository orederRepository,BookStoreContext context)

        {
            this.authorRepository = authorRepository;
            this.bookRepository = bookRepository;
            this.categoryRepository = categoryRepository;
            this.authorBookRepository = authorBookRepository;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.orederRepository = orederRepository;
            this.context = context;
        }

        
    }
}
