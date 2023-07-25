public class CustomerService : ICustomerService
{
    private readonly CustomerDbContext _dbContext;

    public CustomerService(CustomerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Customer> GetAllCustomers()
    {
        return _dbContext.Customers.ToList();
    }

   public Customer? GetCustomerById(int id)
{
    return _dbContext.Customers.FirstOrDefault(c => c.Id == id);
}


    public void AddCustomer(Customer customer)
    {
        _dbContext.Customers.Add(customer);
        _dbContext.SaveChanges();
    }

    public void UpdateCustomer(Customer customer)
    {
        _dbContext.Customers.Update(customer);
        _dbContext.SaveChanges();
    }

    public void DeleteCustomer(int id)
    {
        var customer = _dbContext.Customers.FirstOrDefault(c => c.Id == id);
        if (customer != null)
        {
            _dbContext.Customers.Remove(customer);
            _dbContext.SaveChanges();
        }
    }

    public IEnumerable<Customer> SearchCustomers(string searchTerm)
    {
        return _dbContext.Customers.Where(c => c.FirstName.Contains(searchTerm) || c.LastName.Contains(searchTerm)).ToList();
    }
}
