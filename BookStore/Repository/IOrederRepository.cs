using BookStore.Models;

namespace BookStore.Repository
{
    public interface IOrederRepository
    {
        public void Add(Order Order);
        public void Remove(Order Order);
        public void Update(Order Order);
        public Order GetOderById(int id);
        public List<Order> GetAllOrders();
        public void Save();
    }
}
