using BookStore.Models;

namespace BookStore.Repository
{
    public interface ICustomerRepository
    {
        public void Add(Customer customer);

        public Customer GetByID(int id);
       
        public void Update(Customer customer);
        public void Delete(int id);
        public Customer GetByPhone(string Phone);
        public List<Customer> GetAllCustomer();
        public void Save();
    }
}
