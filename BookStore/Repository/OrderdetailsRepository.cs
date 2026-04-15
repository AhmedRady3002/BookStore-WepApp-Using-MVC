using BookStore.Models;

namespace BookStore.Repository
{
    public class OrderdetailsRepository : IOrderdetailsRepository
    {
        private readonly BookStoreContext context;

        public OrderdetailsRepository(BookStoreContext context)
        {
            this.context = context;
        }
        public void Add(OrderDetails orderDetails)
        {
           context.OrderDetails.Add(orderDetails);
        }

        public List<OrderDetails> GetAllOrderdetails()
        {
            return context.OrderDetails.ToList();
        }

        public OrderDetails GetOderdetailsByOrderId(int id)
        {
            return context.OrderDetails.FirstOrDefault(od => od.OrderID == id);
        }

        public void Remove(OrderDetails orderDetails)
        {
            context.OrderDetails.Remove(orderDetails);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(OrderDetails orderDetails)
        {
           context.OrderDetails.Update(orderDetails);
        }
    }
}
