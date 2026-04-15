using BookStore.Models;
using BookStore.ModelView;

namespace BookStore.Repository
{
    public interface IBookRepository
    {
        public void Add(Book book);
       
        public Book GetBookByID(int id);
        public List<ShowProductsViewModel> GetBookByName(string name);
        public void UpdateBook( Book book);
        public void DeleteBook(int id);
        public List<Book> GetAllBooks();

        public (int bookid, string bookname, double price,string description, int Quantity, string ImageURL, string Category_Name,string Author_Name, string Date)? GetBooksWithCategoryAndAuthorAsTupleByBookID(int id);
        public List<(int bookid, string bookname, double price, int Quantity, string ImageURL, string Category_Name, string Author_Name, string Date)> GetBooksWithCategoryAndAuthorAsTuple();
        public void Save();



    }
}
