using BookStore.Models;

namespace BookStore.Repository
{
    public interface IAuthorRepository
    {
        public void Add(Author author);
        public void Update(Author author);
        public void Delete(Author author);
      
        public List<Author> GetAllAuthores();
        public Author GetAuthorByID(int id);
        public  void Save();



    }
}
