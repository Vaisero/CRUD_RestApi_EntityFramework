using EF_CRUD_REST_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EF_CRUD_REST_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Product_OrderController:ControllerBase
    {
        private readonly AppDBContext _context;

        public Product_OrderController(AppDBContext context)
        {
            _context = context;
        }


        // GET: api/product_order
        [HttpGet]
        public async Task<IActionResult> GetProduct_Order(
                                                     [FromQuery] int pageNumber = 1,
                                                     [FromQuery] int pageSize = int.MaxValue,
                                                     [FromQuery] long amount = 0,
                                                     [FromQuery] DateTimeOffset? date_and_time = null,
                                                     [FromQuery] short status_id = 0,
                                                     [FromQuery] long customer_id = 0)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Некорректные значения номера страницы или размера страницы.");
            }

            
            IQueryable<Product_OrderModel> filtered_product_order = _context.product_order;

            if (amount != 0) 
                filtered_product_order = filtered_product_order.Where(c => c.amount.ToString().Contains(amount.ToString()));
            if (date_and_time.HasValue)
                filtered_product_order = filtered_product_order.Where(c => c.date_and_time.ToLocalTime().ToString().Contains(date_and_time.ToString()));                           
            if (status_id != 0)
                filtered_product_order = filtered_product_order.Where(c => c.status_id.ToString().Contains(status_id.ToString()));
            if (customer_id != 0) 
                filtered_product_order = filtered_product_order.Where(c => c.customer_id.ToString().Contains(customer_id.ToString()));


            var product_orderToReturn = await filtered_product_order
                 .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return Ok(product_orderToReturn);
        }

        // POST: api/product_order
        [HttpPost]
        public async Task<ActionResult<Product_OrderModel>> CreateProduct_Order(Product_OrderModel product_order)
        {
            _context.product_order.Add(product_order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct_Order), new { id = product_order.id }, product_order);
        }

        // PUT: api/product_order
        [HttpPut]
        public async Task<IActionResult> UpdateProduct_Order(long id, Product_OrderModel product_order)
        {
            if (id != product_order.id)
                return BadRequest();

            
            _context.Entry(product_order).State = EntityState.Modified;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if ( ! _context.product_order.Any(e => e.id == id))
                    return NotFound();
                else
                    throw;
            }
            

            return NoContent();
        }

        // DELETE: api/product_order/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct_Order(long id)
        {
            var product_order = await _context.product_order.FindAsync(id);

            if (product_order == null)
                return NotFound();

            _context.product_order.Remove(product_order);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
