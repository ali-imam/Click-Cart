using ClickCartAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClickCartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cart>>> GetCart()
        {
            var cart = await _db.Carts.ToListAsync();

            return Ok(cart);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> GetCartById(int id)
        {
            var cart = await _db.Carts.FindAsync(id);

            if (cart == null)
            {
                return NotFound();
            }
            return cart;

        }

        [HttpPost]
        public async Task<ActionResult<Cart>> CreateCart(Cart obj)
        {
            await _db.Carts.AddAsync(obj);
            await _db.SaveChangesAsync();
            return Ok(obj);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Cart>> UpdateCart(int id, Cart cart)
        {
            if (id != cart.CartId)
            {
                return BadRequest();
            }
            _db.Entry(cart).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return Ok(cart);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Cart>> DeleteCart(int id)
        {
            var cart = await _db.Carts.FindAsync(id)
    ;
            if (cart == null)
            {
                return NotFound();
            }
            _db.Carts.Remove(cart);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
