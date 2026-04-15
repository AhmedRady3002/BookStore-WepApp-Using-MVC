using BookStore.Models;

namespace BookStore.Repository
{
    public interface IAuthorBookRepository
    {
        public void Add( AuthorBook authorBook);
        public void Update(AuthorBook authorBook);
        public void Delete(AuthorBook authorBook);
        public List<int> GetAuthorNoByBookID(int bookID);
        public List<AuthorBook> GetByBookId(int bookid);
        public void Save();
    }
}
