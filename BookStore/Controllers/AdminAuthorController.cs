using BookStore.Models;
using BookStore.ModelView;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public partial class AdminController : Controller
    {
       
        public IActionResult AuthorAdmin() 
        {
           List<Author> authors= authorRepository.GetAllAuthores();
            ViewBag.Authors=authors;
            return PartialView("AuthorAdmin");
        }
        public IActionResult SaveAuthor(AddAuthorViewModel authorViewModel)
        {
            if (ModelState.IsValid)
            {
                Author author=new Author();
                author.Name = authorViewModel.Name;
                authorRepository.Add(author);
                authorRepository.Save();
                return RedirectToAction("AuthorAdmin");
            }
            return RedirectToAction("AuthorAdmin");
        }

     
        
        public IActionResult EditeAuthor(int id) 
        {
            Author author = authorRepository.GetAuthorByID(id);
            return View("EditeAuthor",author);
        }
       
        public IActionResult SaveAuthorEdite(Author author ) 
        {
            if (ModelState.IsValid)
            {
                Author author1=authorRepository.GetAuthorByID(author.Id);
                author1.Name = author.Name;
                author1.Id= author.Id;
                authorRepository.Save();
                return RedirectToAction("AuthorAdmin");
            }
            else
            {
                return RedirectToAction("EditeAuthor", author);   
            
            }
           
        
        }

        public IActionResult DeleteAuthor(Author author) 
        {
            if (author != null)
            {
                authorRepository.Delete(author);
                authorRepository.Save();
                return RedirectToAction("AuthorAdmin");
            }
            return RedirectToAction("AuthorAdmin");


		}





	}

}
