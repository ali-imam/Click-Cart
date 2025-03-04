using ClickCartAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClickCartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUser()
        {
            var user = await _db.Users.ToListAsync();

            return Ok(user);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _db.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            return user;

        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User obj)
        {
            await _db.Users.AddAsync(obj);
            await _db.SaveChangesAsync();
            return Ok(obj);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }
            _db.Entry(user).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _db.Users.FindAsync(id)
    ;
            if (user == null)
            {
                return NotFound();
            }
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
            return Ok();
        }

    }
}
