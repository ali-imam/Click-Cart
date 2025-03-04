using ClickCartAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClickCartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public OrderItemController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderItem>>> GetOrderItem()
        {
            var orderitem = await _db.OrderItems.ToListAsync();

            return Ok(orderitem);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItem>> GetOrderItemById(int id)
        {
            var orderitem = await _db.OrderItems.FindAsync(id);

            if (orderitem == null)
            {
                return NotFound();
            }
            return orderitem;

        }

        [HttpPost]
        public async Task<ActionResult<OrderItem>> CreateOrderItem(OrderItem obj)
        {
            await _db.OrderItems.AddAsync(obj);
            await _db.SaveChangesAsync();
            return Ok(obj);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<OrderItem>> UpdateOrderItem(int id, OrderItem orderitem)
        {
            if (id != orderitem.OrderItemId)
            {
                return BadRequest();
            }
            _db.Entry(orderitem).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return Ok(orderitem);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderItem>> DeleteOrderItem(int id)
        {
            var orderitem = await _db.OrderItems.FindAsync(id)
    ;
            if (orderitem == null)
            {
                return NotFound();
            }
            _db.OrderItems.Remove(orderitem);
            await _db.SaveChangesAsync();
            return Ok();
        }

    }
}
