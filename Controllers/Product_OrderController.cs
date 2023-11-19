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
        public async Task<ActionResult<IEnumerable<Product_OrderModel>>> GetProduct_Order()
        {
            return await _context.product_order.ToListAsync();
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
        public async Task<IActionResult> UpdateProduct_Order(int id, Product_OrderModel product_order)
        {
            if (id != product_order.id)
                return BadRequest();

            _context.Entry(product_order).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/product_order/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct_Order(int id)
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
