public interface ICustomerService
{
    IEnumerable<Customer> GetAllCustomers();
    Customer? GetCustomerById(int id);
    void AddCustomer(Customer customer);
    void UpdateCustomer(Customer existingcustomer,Customer updatedCustomer);
    void DeleteCustomer(int id);
    IEnumerable<Customer> SearchCustomers(string searchTerm);
}
