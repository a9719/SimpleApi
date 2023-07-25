using Microsoft.EntityFrameworkCore;

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

    public void UpdateCustomer(Customer existingCustomer ,Customer customer)

    {
        


         _dbContext.Entry(existingCustomer).State = EntityState.Detached;

    // Update the properties of the detached entity
    existingCustomer.FirstName = customer.FirstName; // Update other properties as needed
    existingCustomer.LastName=customer.LastName;
    existingCustomer.DateOfBirth=customer.DateOfBirth;

    // Attach the updated entity back to the context and mark it as modified
    _dbContext.Attach(existingCustomer).State = EntityState.Modified;

    // Save the changes to persist the updated entity
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
