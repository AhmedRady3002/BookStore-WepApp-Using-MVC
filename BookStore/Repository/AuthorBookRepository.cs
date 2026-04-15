using BookStore.Models;

namespace BookStore.Repository
{
    public class AuthorBookRepository : IAuthorBookRepository
    {
        private readonly BookStoreContext context;

        public AuthorBookRepository(BookStoreContext context)
        {
            this.context = context;
        }
        public void Add(AuthorBook authorBook)
        {
            context.AuthorBooks.Add(authorBook);
        }

        public void Delete(AuthorBook authorBook)
        {
            context.AuthorBooks.Remove(authorBook);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(AuthorBook authorBook)
        {
            context.AuthorBooks.Update(authorBook);
        }
        public List<int> GetAuthorNoByBookID(int bookID)
        {
            return context.AuthorBooks.Where(ab=>ab.Bookcode== bookID).Select(a=>a.Author_no).ToList();
        }

        public List<AuthorBook> GetByBookId(int bookid)
        {
            return context.AuthorBooks.Where(ab=>ab.Bookcode==bookid).ToList();
        }

    }
}
