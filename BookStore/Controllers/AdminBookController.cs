using BookStore.Models;
using BookStore.ModelView;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStore.Controllers
{
	public partial class AdminController : Controller
	{

        public IActionResult BookAdmin()
		{
			ViewBag.categories=categoryRepository.GetAllCategories();
            ViewBag.Authors=authorRepository.GetAllAuthores();	

            return View("BookAdmin");
		}
        public IActionResult SaveBook( AddBookViewModel bookViewModel) 
		{
			if (ModelState.IsValid)
			{
                if (bookViewModel.CategoryID != 0 && bookViewModel.AuthorIds.Any())
 
                {
                    Book book = new Book
                    {
                        Id = bookViewModel.Id,
                        Name = bookViewModel.Name,
                        Description = bookViewModel.Description,
                        Price = bookViewModel.Price,
                        CategoryID = bookViewModel.CategoryID,
                        AvailableQuantity = bookViewModel.AvailableQuantity,
                        ImageURL=bookViewModel.imageURL
                    };

                    bookRepository.Add(book);
                    bookRepository.Save(); 

                    foreach (var authorId in bookViewModel.AuthorIds)
                    {
                        var author = authorRepository.GetAuthorByID(authorId);
                        if (author != null)
                        {
                            AuthorBook authorBook = new AuthorBook
                            {
                                DateOfPublish=bookViewModel.date,
                                Bookcode = book.Id,
                                Author_no = author.Id
                                
                            };
                            authorBookRepository.Add(authorBook);
                        }
                    }
                    authorBookRepository.Save();

                    return RedirectToAction("BookAdmin");
                }
                ModelState.AddModelError("CategoryID", "enter category and author");
			
			}
            ViewBag.categories = categoryRepository.GetAllCategories();
			ViewBag.Authors = authorRepository.GetAllAuthores();
            return View("BookAdmin", bookViewModel);
		}
       
        public IActionResult GetBooks() 
		{
			var books =bookRepository.GetBooksWithCategoryAndAuthorAsTuple();
			return PartialView("GetBooks",books);
        }

        public IActionResult EditeBook( int id) 
        {
          var book=bookRepository.GetBookByID(id);
            EditeBookViewModel bookViewModel=new EditeBookViewModel();

            bookViewModel.BookID = book.Id;
            bookViewModel.BookName= book.Name;
            bookViewModel.AvailableQuantity = book.AvailableQuantity;
            bookViewModel.Price = book.Price; 
            bookViewModel.Description = book.Description;
            bookViewModel.Categoryid = book.CategoryID;
            bookViewModel.ImageURL= book.ImageURL;
            ViewBag.categories = categoryRepository.GetAllCategories();
            ViewData["Authors"] = authorRepository.GetAllAuthores();
            return View("EditeBook",bookViewModel);

        }
        public IActionResult SaveBookEdited(EditeBookViewModel editeBookViewModel)
        {

            if (ModelState.IsValid)
            {
               
              var book=  bookRepository.GetBookByID(editeBookViewModel.BookID);
                book.Name = editeBookViewModel.BookName;
                book.Price = editeBookViewModel.Price;
                book.AvailableQuantity = editeBookViewModel.AvailableQuantity;
                book.Description = editeBookViewModel.Description;
                book.ImageURL = editeBookViewModel.ImageURL;
               
                if (editeBookViewModel.Categoryid != 0)
                {
                    book.CategoryID = editeBookViewModel.Categoryid;
                }
                
                bookRepository.Save();
                var authorbooks = authorBookRepository.GetByBookId(editeBookViewModel.BookID);

                if (editeBookViewModel.authorIDs != null)
                {
                    if (editeBookViewModel.date == null)
                    {
                        ModelState.AddModelError("date", "enter date");
                        ViewBag.categories = categoryRepository.GetAllCategories();
                        ViewData["Authors"] = authorRepository.GetAllAuthores();
                        return View("EditeBook", editeBookViewModel);

                    }
                    else 
                    {
                        // 2️⃣ امسحهم
                        foreach (var item in authorbooks)
                        {
                            //  OldDate = item.DateOfPublish;
                            authorBookRepository.Delete(item);
                        }
                        authorBookRepository.Save();

                        foreach (var authorID in editeBookViewModel.authorIDs)
                        {
                            AuthorBook authorBook = new AuthorBook
                            {
                                Author_no = authorID,
                                Bookcode = book.Id,
                                DateOfPublish = editeBookViewModel.date
                            };
                            authorBookRepository.Add(authorBook);

                        }
                        authorBookRepository.Save();
                    }

                }

               if  ( editeBookViewModel.date != null )
                {
                    var authorbook = authorBookRepository.GetByBookId(editeBookViewModel.BookID);
                    foreach (var item in authorbook)
                    { 
                        item.DateOfPublish = editeBookViewModel.date;
                    }
                    authorBookRepository.Save();
                }


             
                return RedirectToAction("BookAdmin");


            }
            ViewBag.categories = categoryRepository.GetAllCategories();
            ViewData["Authors"] = authorRepository.GetAllAuthores();
            return View("EditeBook", editeBookViewModel);
            


        }

        public IActionResult DeleteBook(int id) 
        {
         
            bookRepository.DeleteBook(id);
            bookRepository.Save();
            return RedirectToAction("BookAdmin");

        }
        



    }
}
