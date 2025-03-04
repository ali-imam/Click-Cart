using ClickCartAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClickCartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public PaymentController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<Payment>>> GetPayment()
        {
            var payment = await _db.Payments.ToListAsync();

            return Ok(payment);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPaymentById(int id)
        {
            var payment = await _db.Payments.FindAsync(id);

            if (payment == null)
            {
                return NotFound();
            }
            return payment;

        }

        [HttpPost]
        public async Task<ActionResult<Payment>> CreatePayment(Payment obj)
        {
            await _db.Payments.AddAsync(obj);
            await _db.SaveChangesAsync();
            return Ok(obj);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Payment>> UpdatePayment(int id, Payment payment)
        {
            if (id != payment.PaymentId)
            {
                return BadRequest();
            }
            _db.Entry(payment).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return Ok(payment);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Payment>> DeletePayment(int id)
        {
            var payment = await _db.Payments.FindAsync(id)
    ;
            if (payment == null)
            {
                return NotFound();
            }
            _db.Payments.Remove(payment);
            await _db.SaveChangesAsync();
            return Ok();
        }

    }
}
