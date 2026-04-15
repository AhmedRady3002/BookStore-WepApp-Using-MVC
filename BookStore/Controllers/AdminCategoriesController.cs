using BookStore.Models;
using BookStore.ModelView;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
	public partial class AdminController : Controller
	{
		public IActionResult CategoryAdmin() 
		{
			ViewBag.Categories=categoryRepository.GetAllCategories();
			return PartialView("CategoryAdmin");
		}
		

		public IActionResult SaveCategory(AddCategoryNameViewModel categoryNameViewModel) 
		{
			if (ModelState.IsValid) 
			{
				Category category = new Category();
				category.Name = categoryNameViewModel.CategoryName;
				categoryRepository.Add(category);
				categoryRepository.Save();
				return RedirectToAction("CategoryAdmin");
			}
			throw new Exception();
		}

		public IActionResult EditCategory(int Id) 
		{
		Category category=categoryRepository.GetById(Id);

			return View("EditCategory", categoryRepository.GetById(Id));
		}
		public IActionResult DeleteCategory(Category category)
		{
			if (category != null)
			{
				categoryRepository.Delete(category);
				categoryRepository.Save();
				return RedirectToAction("CategoryAdmin");
			}
            return RedirectToAction("CategoryAdmin");
        }
		public IActionResult SaveEditCategory(Category category)
		{
			if (ModelState.IsValid)
			{
				//Category category1 = categoryRepository.GetById(category.Id);
				//category1.Name = category.Name;
				//category1.Id = category.Id;
				//authorRepository.Save();
				categoryRepository.Update(category);
				categoryRepository.Save();
				return RedirectToAction("CategoryAdmin");
			}
			else 
			{ 

			return View("EditCategory", category);

		    }

		}


	}
}
