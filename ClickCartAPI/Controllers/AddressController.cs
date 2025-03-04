using ClickCartAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClickCartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {

        private readonly ApplicationDbContext _db;

        public AddressController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<Address>>> GetAddress()
        {
            var address = await _db.Addresses.ToListAsync();

            return Ok(address);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddressById(int id)
        {
            var address = await _db.Addresses.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }
            return address;

        }

        [HttpPost]
        public async Task<ActionResult<Address>> CreateAddress(Address obj)
        {
            await _db.Addresses.AddAsync(obj);
            await _db.SaveChangesAsync();
            return Ok(obj);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Address>> UpdateAddress(int id, Address address)
        {
            if (id != address.AddressId)
            {
                return BadRequest();
            }
            _db.Entry(address).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return Ok(address);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Address>> DeleteAddress(int id)
        {
            var address = await _db.Addresses.FindAsync(id)
    ;
            if (address == null)
            {
                return NotFound();
            }
            _db.Addresses.Remove(address);
            await _db.SaveChangesAsync();
            return Ok();
        }

    }
}
