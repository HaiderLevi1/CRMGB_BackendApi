using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using GreyBeardCRM.Models;
using Microsoft.EntityFrameworkCore;

namespace GreyBeardCRM.Controllers
{
    [RoutePrefix("api/customers")]
    public class CustomerController : ApiController
    {
        private readonly CustomerDBContext _context;

        public CustomerController(CustomerDBContext context)
        {
            _context = context;
        }

        // GET: api/customers
        [HttpGet, Route("")]
        public async Task<IHttpActionResult> GetCustomers()
        {
            var customers = await _context.Customers.ToListAsync();
            return Ok(customers);
        }

        // GET: api/customers/{id}
        [HttpGet, Route("{id:int}")]
        public async Task<IHttpActionResult> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        // POST: api/customers
        [HttpPost, Route("")]
        public async Task<IHttpActionResult> CreateCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("CustomerApi", new { id = customer.CustomerID }, customer); // Ensure "DefaultApi" is the correct route name
        }

        // PUT: api/customers/{id}
        [HttpPut, Route("{id:int}")]
        public async Task<IHttpActionResult> UpdateCustomer(int id, [FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingCustomer = await _context.Customers.FindAsync(id);
            if (existingCustomer == null)
                return NotFound();

            existingCustomer.CustomerName = customer.CustomerName;
            existingCustomer.Email = customer.Email;
            existingCustomer.PhoneNo = customer.PhoneNo;

            await _context.SaveChangesAsync();
            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }

        // DELETE: api/customers/{id}
        [HttpDelete, Route("{id:int}")]
        public async Task<IHttpActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return NotFound();

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }
    }
}
