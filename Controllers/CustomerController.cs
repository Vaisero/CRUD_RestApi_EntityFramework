using EF_CRUD_REST_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EF_CRUD_REST_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController:ControllerBase
    {
        private readonly AppDBContext _context;

        public CustomerController(AppDBContext context)
        {
            _context = context;
        }


        // GET: api/customer
        [HttpGet]
        public async Task<IActionResult> GetCustomer(
                                                     [FromQuery] int pageNumber = 1,
                                                     [FromQuery] int pageSize = int.MaxValue,
                                                     [FromQuery] string? first_name = null,
                                                     [FromQuery] string? last_name = null,
                                                     [FromQuery] DateTime date_of_birth = default(DateTime))
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Некорректные значения номера страницы или размера страницы.");
            }

            IQueryable<CustomerModel> filtered_customer = _context.customer;

            if (!string.IsNullOrEmpty(first_name))
                filtered_customer = filtered_customer.Where(c => c.first_name.Contains(first_name));

            if (!string.IsNullOrEmpty(last_name))
                filtered_customer = filtered_customer.Where(c => c.last_name.Contains(last_name));

            if (date_of_birth != default(DateTime)) 
            {
                filtered_customer = filtered_customer.Where(x => x.date_of_birth == DateOnly.FromDateTime(date_of_birth));
            }    

            var customerToReturn = await filtered_customer
            .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return Ok(customerToReturn);
        }

        // POST: api/customer
        [HttpPost]
        public async Task<ActionResult<CustomerModel>> CreateCustomer(CustomerModel customer)
        {
            _context.customer.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.id }, customer);
        }

        // PUT: api/customer
        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(long id, CustomerModel customer)
        {
            if (id != customer.id)
                return BadRequest();

            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/customer/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(long id)
        {
            var customer = await _context.customer.FindAsync(id);

            if (customer == null) 
                return NotFound();

            _context.customer.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
