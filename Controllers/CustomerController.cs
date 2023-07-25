using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace SimpleApi.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public IEnumerable<Customer> GetAllCustomers()
        {
            return _customerService.GetAllCustomers();
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomerById(int id)
        {
            var customer = _customerService.GetCustomerById(id);
            if (customer == null)
                return NotFound();
            return customer;
        }

        [HttpPost]
        public ActionResult<Customer> AddCustomer([FromBody] Customer customer)
        {
            _customerService.AddCustomer(customer);
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody] Customer customer)
        {
            var existingCustomer = _customerService.GetCustomerById(id);
            if (existingCustomer == null)
                return NotFound();

            customer.Id = id;
            _customerService.UpdateCustomer(existingCustomer, customer);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var existingCustomer = _customerService.GetCustomerById(id);
            if (existingCustomer == null)
                return NotFound();

            _customerService.DeleteCustomer(id);
            return NoContent();
        }

        [HttpGet("search")]
        public IEnumerable<Customer> SearchCustomers([FromQuery] string searchTerm)
        {
            return _customerService.SearchCustomers(searchTerm);
        }
    }
}
