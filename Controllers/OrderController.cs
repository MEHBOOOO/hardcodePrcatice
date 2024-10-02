using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Controllers{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase{
        private readonly OrderContext _context;
        public OrderController(OrderContext context){
            _context = context;
        }

// get запрос на все заказы
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return Ok(await _context.Orders.ToListAsync());
        }

// get запрос по ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id){
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

// post запрос на создание нового заказа
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] Order newOrder){
            if (newOrder == null || string.IsNullOrWhiteSpace(newOrder.ProductName)){
            return BadRequest("ProductName is required.");
    }
            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = newOrder.Id }, newOrder);
}


// put запрос на обновление заказа
        [HttpPut("{id}")]
        public async Task<ActionResult<Order>> UpdateOrder(int id, [FromBody] Order updatedOrder){
            var existingOrder = await _context.Orders.FindAsync(id);
            if(existingOrder == null){
                return NotFound();
            }

            existingOrder.ProductName = updatedOrder.ProductName;
            existingOrder.Quantity = updatedOrder.Quantity;
            existingOrder.Price = updatedOrder.Price;

            await _context.SaveChangesAsync();

            return NoContent();
        }

//delete запрос на удаление заказа
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id){
            var order = await _context.Orders.FindAsync(id);
            if(order == null){
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
