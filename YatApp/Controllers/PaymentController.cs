//using System;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Identity;
//using YatApp.Api;
//using Library.Data;
//using Library.Models;
//using Interface;
//using Dto;
//using Repo;

//namespace YatApp.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class PaymentController : BaseApiController
//    {
//        public PaymentController(IUnitofWork unitofWork) : base(unitofWork)
//        {
//        }

//        // GET: api/Payment
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Payment>>> GetPayments()
//        {
//            return await _unitofWork.Payments.ToListAsync();
//        }

//        // GET: api/Payment/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Payment>> GetPayment(int id)
//        {
//            var payment = await _unitofWork.Payments.FindAsync(id);

//            if (payment == null)
//            {
//                return NotFound();
//            }

//            return payment;
//        }

//        // PUT: api/Payment/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutPayment(int id, Payment payment)
//        {
//            if (id != payment.PaymentId)
//            {
//                return BadRequest();
//            }

//            _unitofWork.Entry(payment).State = EntityState.Modified;

//            try
//            {
//                await _unitofWork.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!PaymentExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/Payment
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<Payment>> PostPayment(Payment payment)
//        {
//            _unitofWork.Payments.Add(payment);
//            await _unitofWork.SaveChangesAsync();

//            return CreatedAtAction("GetPayment", new { id = payment.PaymentId }, payment);
//        }

//        // DELETE: api/Payment/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeletePayment(int id)
//        {
//            var payment = await _unitofWork.Payments.FindAsync(id);
//            if (payment == null)
//            {
//                return NotFound();
//            }

//            _unitofWork.Payments.Remove(payment);
//            await _unitofWork.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool PaymentExists(int id)
//        {
//            return _unitofWork.Payments.Any(e => e.PaymentId == id);
//        }
//    }
//}
