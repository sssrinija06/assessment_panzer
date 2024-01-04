using Customer_app_backend.Model;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Customer_app_backend.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private static List<Customer> _customers = GenerateSampleData();

        private static List<Customer> GenerateSampleData()
        {
            var customers = new List<Customer>();
            for (int i = 1; i <= 20; i++)
            {
                customers.Add(new Customer
                {
                    Id = i,
                    FirstName = $"Customer{i}",
                    LastName = $"Last{i}",
                    Email = $"customer{i}@example.com",
                    CreatedDateTime = DateTime.UtcNow,
                    LastUpdatedDateTime = DateTime.UtcNow
                });
            }
            return customers;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetCustomers()
        {
            return Ok(_customers);
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomer(int id)
        {
            var customer = _customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public ActionResult<Customer> AddCustomer([FromBody] Customer customer)
        {
            // Simulate database or storage addition
            customer.Id = _customers.Count + 1;
            customer.CreatedDateTime = DateTime.UtcNow;
            customer.LastUpdatedDateTime = DateTime.UtcNow;
            _customers.Add(customer);
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public ActionResult<Customer> UpdateCustomer(int id, [FromBody] Customer customer)
        {
            var existingCustomer = _customers.FirstOrDefault(c => c.Id == id);
            if (existingCustomer == null)
            {
                return NotFound();
            }

            existingCustomer.FirstName = customer.FirstName;
            existingCustomer.LastName = customer.LastName;
            existingCustomer.Email = customer.Email;
            existingCustomer.LastUpdatedDateTime = DateTime.UtcNow;

            return Ok(existingCustomer);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCustomer(int id)
        {
            var customer = _customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            _customers.Remove(customer);
            return NoContent();
        }
    }
}
