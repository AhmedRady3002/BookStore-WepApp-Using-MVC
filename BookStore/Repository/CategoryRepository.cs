using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BookStoreContext context;

        public CategoryRepository(BookStoreContext context)
        {
            this.context = context;
        }
        public void Add(Category category)
        {
           context.Categories.Add(category);
        }

        public void Delete(Category category)
        {
           context.Categories.Remove(category);
        }

        public List<Category> GetAllCategories()
        {
            return context.Categories.ToList();
        }

        public Category GetById(int id)
        {
           return context.Categories.FirstOrDefault(c=>c.Id==id);
        }

       

        public  void Save()
        {
           context.SaveChanges();
        }

        public void Update(Category category)
        {
          context.Categories.Update(category);
        }

    }
}
