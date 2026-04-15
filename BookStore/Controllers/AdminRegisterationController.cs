using Microsoft.AspNetCore.Mvc;
using BookStore.Models;

using BookStore.ModelView;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace BookStore.Controllers
{
    public partial class AdminController : Controller
    {
        public IActionResult Register()
        {
            return View("Register");
        }
        public async Task < IActionResult> SaveAdminRegister(AdminRegisterationViewModel adminRegisteration)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName=adminRegisteration.UserName;
                user.FirstName = adminRegisteration.FirstName;
                user.LastName = adminRegisteration.LastName;
                user.PhoneNumber = adminRegisteration.PhoneNumber;
            var result=  await  userManager.CreateAsync(user, adminRegisteration.Password);
                if (result.Succeeded)
                {
                  await  userManager.AddToRoleAsync(user, "Admin");
                   await signInManager.SignInAsync(user, false);
                    TempData["SuccessMessage"] = "Successfully ";
                    return RedirectToAction("Register");
                }
                foreach (var item in result.Errors)
                {
                    if (item.Code == "DuplicateUserName")
                    {
                        ModelState.AddModelError("Email", "Email is already exists");
                    }
                    //ModelState.AddModelError("", item.Description);
                }


            }
            return View("Register", adminRegisteration);

        }
        public IActionResult AddROle() 
        {
            return View("AddROle");
        }
        public async Task< IActionResult> SaveRole(AddRoleViewModel roleViewModel) 
        {
            if (ModelState.IsValid) 
            { 
            IdentityRole identityRole = new IdentityRole();
             identityRole.Name=roleViewModel.RoleName;
          var result= await  roleManager.CreateAsync(identityRole);
                if (result.Succeeded) 
                {
					TempData["SuccessMessage"] = "Successfully";
                    return RedirectToAction("AddROle");
				}
                foreach (var item in result.Errors) 
                {
                    ModelState.AddModelError("",item.Description);
                }

			}
            return View("AddROle", roleViewModel);
            
        }
        [AllowAnonymous]
        public async Task< IActionResult> LogOut() 
        {
           await signInManager.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("LogIn","UserAccount");
        }


    }
}
