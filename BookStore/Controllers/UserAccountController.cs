using BookStore.Models;
using BookStore.ModelView;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookStore.Controllers
{
    public class UserAccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;
		private readonly BookStoreContext context;

		public  UserAccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,BookStoreContext context) 
        {
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.context = context;
		}
        public IActionResult Register()
        {
            return View("Register");
        }
        public async Task< IActionResult> SaveRegister(UserRegisterViewModel userRegisterViewModel)
        {
          
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                
                
                user.UserName = userRegisterViewModel.UserName;
                user.FirstName = userRegisterViewModel.FirstName;
                user.LastName = userRegisterViewModel.LastName;
                user.PhoneNumber = userRegisterViewModel.PhoneNumber;
             var result=  await userManager.CreateAsync(user,userRegisterViewModel.Password);
                if (result.Succeeded) 
                {
                  await  userManager.AddToRoleAsync(user, "User");
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("ShowProduct", "Products");
                }
                foreach (var item in result.Errors)
                { 
                    ModelState.AddModelError("",item.Description);    
                }
            }
            return View("Register", userRegisterViewModel);  

        }
        public IActionResult LogIn() 
        {
            return View("LogIn");
        }
        public async Task<ActionResult> SaveLogin(UserLoginViewModel userLoginViewModel) 
        {
            if (ModelState.IsValid) 
            {
                
            var user= await userManager.FindByNameAsync(userLoginViewModel.UserName);
                if (user != null) 
                {
                var result=await   userManager.CheckPasswordAsync(user,userLoginViewModel.Password);
                    if (result == true)
                    { 
                      
                    await  signInManager.SignInAsync(user, userLoginViewModel.RememberMe);
						return RedirectToAction("ShowProduct", "Products");
					}
                }
                ModelState.AddModelError("", "User_Name Or Password Wrong");
            }

            return View("LogIn", userLoginViewModel);

        }

        
    }
}
