using Microsoft.AspNetCore.Mvc;
using OrderService.Models;

namespace OrderService.Controllers{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase{
        private static List<Order> orders = new List<Order>();
        private static int nextId = 1;

//get запрос на все продукты
        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetOrders(){
            return Ok(orders);
        }

//get запрос по ID
        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(int id){
            var order = orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

//post запрос на создание нового заказа
        [HttpPost]
        public ActionResult<Order> CreateOrder([FromBody] Order newOrder){
            if (newOrder == null || string.IsNullOrWhiteSpace(newOrder.ProductName)){
                return BadRequest("ProductName is required.");
            }
            newOrder.Id = nextId++;
            orders.Add(newOrder);
            return CreatedAtAction(nameof(GetOrder), new { id = newOrder.Id }, newOrder);
        }
    }
}
//удаление заказа
//обновление заказа
