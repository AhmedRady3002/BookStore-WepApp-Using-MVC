using BookStore.Models;

namespace BookStore.Repository
{
    public interface ICategoryRepository
    {
        public void Add(Category category);
        public void Update(Category category);
        public void Delete(Category category);
       
        public List<Category> GetAllCategories();
        public Category GetById(int id);
        public void Save();

    }
}
