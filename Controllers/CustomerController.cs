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
        public async Task<ActionResult<IEnumerable<CustomerModel>>> GetCustomer()
        {
            return await _context.customer.ToListAsync();
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
        public async Task<IActionResult> UpdateCustomer(int id, CustomerModel customer)
        {
            if (id != customer.id)
                return BadRequest();

            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/customer/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
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
