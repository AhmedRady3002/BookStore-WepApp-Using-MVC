using BookStore.Models;

namespace BookStore.Repository
{
    public class OrderRepository : IOrederRepository
    {
        private readonly BookStoreContext context;

        public OrderRepository(BookStoreContext context)
        {
            this.context = context;
        }

        public void Add(Order Order)
        {
            context.Orders.Add(Order);
        }

        public List<Order> GetAllOrders()
        {
           return context.Orders.ToList();
        }

        public Order GetOderById(int id )
        {
            return context.Orders.FirstOrDefault(o => o.OrderID == id);
        }

        public void Remove(Order Order)
        {
          context.Orders.Remove(Order);
        }

        public void Save()
        {
            context.SaveChanges(); 
        }

        public void Update(Order Order)
        {
           context.Orders.Update(Order);
        }
    }
}
