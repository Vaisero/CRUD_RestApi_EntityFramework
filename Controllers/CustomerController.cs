using EF_CRUD_REST_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Xml.Linq;
using System.Linq;
using Microsoft.VisualBasic;

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
                                                     [FromQuery] DateTime? date_of_birth = null)
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

            if (date_of_birth.HasValue)
            {
                date_of_birth = date_of_birth.Value.Date;
                filtered_customer = filtered_customer.Where(c => c.date_of_birth.ToString().Contains(date_of_birth.ToString()));
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
