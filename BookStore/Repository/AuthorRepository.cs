using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BookStoreContext context;

        public AuthorRepository(BookStoreContext context)
        {
            this.context = context;
        }
        public void Add(Author author)
        {
           context.Authors.Add(author);
        }

        public void Delete(Author author)
        {
            context.Authors.Remove(author);
        }

        public List<Author> GetAllAuthores()
        {
            return context.Authors.ToList();
        }

        public Author GetAuthorByID(int id)
        {
          return  context.Authors.FirstOrDefault(a => a.Id == id);
        }

      

        public  void Save()
        {
            context.SaveChanges();
        }

        public void Update(Author author)
        {
            context.Authors.Update(author);
        }
    }
}
