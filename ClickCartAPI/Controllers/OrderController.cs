using ClickCartAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClickCartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public OrderController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetOrder()
        {
            var order = await _db.Orders.Include(order => order.Product).Include(order => order.User).ToListAsync();
            return Ok(order);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var order = await _db.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }
            return order;

        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(Order obj)
        {
            await _db.Orders.AddAsync(obj);
            await _db.SaveChangesAsync();
            return Ok(obj);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Order>> UpdateOrder(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }
            _db.Entry(order).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return Ok(order);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            var order = await _db.Orders.FindAsync(id)
    ;
            if (order == null)
            {
                return NotFound();
            }
            _db.Orders.Remove(order);
            await _db.SaveChangesAsync();
            return Ok();
        }

    }
}
