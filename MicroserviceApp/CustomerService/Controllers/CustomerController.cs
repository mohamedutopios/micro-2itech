using CustomerService.Models;
using CustomerService.Services;
using Microsoft.AspNetCore.Mvc;


namespace CustomerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly ICustomersService _customersService;

        public CustomerController(ICustomersService customersService)
        {
            _customersService = customersService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            var customers = await _customersService.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _customersService.GetCustomerByIdAsync(id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCustomer(Customer customer)
        {
            await _customersService.AddCustomerAsync(customer);
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomer(int id, Customer customer)
        {
            if (id != customer.Id) return BadRequest();
            await _customersService.UpdateCustomerAsync(customer);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            await _customersService.DeleteCustomerAsync(id);
            return NoContent();
        }
    }
}

