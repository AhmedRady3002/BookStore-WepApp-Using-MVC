using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly BookStoreContext context;

        public CustomerRepository(BookStoreContext context)
        {
            this.context = context;
        }

        public void Add(Customer customer)
        {
           context.Customers.Add(customer);
        }

        public void Delete(int id)
        {
          var customer=  GetByID(id);
            context.Customers.Remove(customer);
        }

        public List<Customer> GetAllCustomer()
        {
          return  context.Customers.ToList();
        }

        public Customer GetByID(int id)
        {
           return context.Customers.FirstOrDefault(c=>c.CustomerID==id);
        }

      

        public Customer GetByPhone(string Phone)
        {
         return  context.Customers.FirstOrDefault(c=>c.CustomerPhone==Phone);
        }

        public void Save()
        {
           context.SaveChanges();
        }

        public void Update(Customer customer)
        {
            context.Customers.Update(customer);
        }
    }
}
