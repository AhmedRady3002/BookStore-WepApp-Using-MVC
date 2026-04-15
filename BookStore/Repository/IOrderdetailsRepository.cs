using BookStore.Models;

namespace BookStore.Repository
{
    public interface IOrderdetailsRepository
    {
        public void Add(OrderDetails orderDetails);
        public void Remove(OrderDetails orderDetails);
        public void Update(OrderDetails orderDetails);
        public OrderDetails GetOderdetailsByOrderId(int id);
        public List<OrderDetails> GetAllOrderdetails();
        public void Save();
    }
}
