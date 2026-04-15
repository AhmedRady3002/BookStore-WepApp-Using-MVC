using BookStore.Models;
using BookStore.ModelView;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace BookStore.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext context;
        private readonly IMemoryCache cache;

        public BookRepository(BookStoreContext context ,IMemoryCache cache)
        {
            this.context = context;
            this.cache = cache;
        }
        public void Add(Book book)
        {
           context.Books.Add(book);
            
        }

        public void DeleteBook(int id)
        {
           Book book =GetBookByID(id);
            context.Books.Remove(book);
        }

        public List<Book> GetAllBooks()
        {
           return context.Books.ToList();
        }


        public Book GetBookByID(int id)
        {
          return context.Books.FirstOrDefault(b=>b.Id==id);
          

        }

        public (int bookid, string bookname, double price,string description,
       int Quantity,string ImageURL, string Category_Name,
       string Author_Name, string Date)?
       GetBooksWithCategoryAndAuthorAsTupleByBookID(int id)
        {
            var result = context.Books
                .Where(b => b.Id == id)
                .Select(b => new
                {
                    bookid = b.Id,
                    bookname = b.Name,
                    price = b.Price,
                    description = b.Description,
                    Quantity = b.AvailableQuantity,
                    ImageURL = b.ImageURL,
                    Category_Name = b.Category.Name,
                    Authors = string.Join(", ",
                        b.AuthorBooks.Select(ab => ab.Author.Name)),
                    Date = b.AuthorBooks
                        .Select(ab => ab.DateOfPublish)
                        .FirstOrDefault()
                })
                .AsEnumerable() // هنا بس علشان ToString
                .Select(x => (
                    x.bookid,
                    x.bookname,
                    x.price,
                    x.description,
                    x.Quantity,
                    x.ImageURL,
                    x.Category_Name,
                    x.Authors,
                    x.Date.ToString()
                ))
                .FirstOrDefault();

            return result;
        }


        public List<ShowProductsViewModel> GetBookByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new List<ShowProductsViewModel>();
            var result = context.Books
        .Where(b => EF.Functions.Like(b.Name, $"%{name}%"))
        .Select(b => new ShowProductsViewModel
        {
            bookid = b.Id,
            bookname = b.Name,
            price = b.Price,
            Quantity = b.AvailableQuantity,
            ImageURL = b.ImageURL,
            Category_Name = b.Category.Name,
            Author_Name = string.Join(", ",
                b.AuthorBooks.Select(ab => ab.Author.Name)),
            Date = b.AuthorBooks
                .Select(ab => ab.DateOfPublish)
                .FirstOrDefault().ToString()
        })
        .ToList();

            return result;

        }

        public List<(int bookid, string bookname, double price,
              int Quantity, string ImageURL, string Category_Name,
              string Author_Name,string Date)>
         GetBooksWithCategoryAndAuthorAsTuple()
        {
           
                var result = context.Books
                  .Select(b => new
                  {
             bookid = b.Id,
             bookname = b.Name,
             price = b.Price,
             Quantity = b.AvailableQuantity,
             ImageURL = b.ImageURL,
             Category_Name = b.Category.Name,
             Authors = string.Join(", ",
                      b.AuthorBooks.Select(ab => ab.Author.Name)),
             Date  = b.AuthorBooks
               .Select(ab => ab.DateOfPublish)
                      .FirstOrDefault().ToString()

                  })
         .AsEnumerable()
         .Select(x => (x.bookid, x.bookname, x.price,
                       x.Quantity,x.ImageURL, x.Category_Name,
                       x.Authors,x.Date))
         .ToList();

            return result;


        }



        public void Save()
        {
            context.SaveChanges();
            
        }

        public void UpdateBook(Book book)
        {
            
            context.Books.Update(book);
        }
    }
}
