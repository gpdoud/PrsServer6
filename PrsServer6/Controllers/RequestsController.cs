using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using PrsServer6.Models;

namespace PrsServer6.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase {
        private readonly PrsDbContext _context;

        public RequestsController(PrsDbContext context) {
            _context = context;
        }

        // GET: api/Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequest() {
            return await _context.Requests
                                    .Include(x => x.User)
                                    .ToListAsync();
        }

        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id) {
            var request = await _context.Requests
                                        .Include(x => x.User)
                                        .Include(x => x.Requestlines)
                                            .ThenInclude(x => x.Product)
                                        .SingleOrDefaultAsync(x => x.Id == id);

            if (request == null) {
                return NotFound();
            }

            return request;
        }

        // GET: api/Requests/reviews/5
        [HttpGet("reviews/{userId}")]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequestInReview(int userId) {
            return await _context.Requests
                                    .Include(x => x.User)
                                    .Where(x => x.Status.Equals(PrsServer6.Models.Request.StatusReview)
                                                    && x.UserId != userId)
                                    .ToListAsync();
        }


        // PUT: api/Requests/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, Request request) {
            if (id != request.Id) {
                return BadRequest();
            }

            _context.Entry(request).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!RequestExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        // PUT: api/Requests/Review/5
        [HttpPut("review/{id}")]
        public async Task<IActionResult> ReviewRequest(int id, Request Request) {
            /*
             * Because Request is a record, it is immutable. So to change
             * the value of Status, a new instance is created using WITH
             * which allows changes the values of the original instance.
             */
            var ApprovedRequest = Request with {
                Status = Request.Total <= 50
                                    ? Request.StatusApproved
                                    : Request.StatusReview
            };
            return await PutRequest(id, ApprovedRequest);
        }
        // PUT: api/Requests/Approve/5
        [HttpPut("approve/{id}")]
        public async Task<IActionResult> ApproveRequest(int id, Request Request) {
            var ApprovedRequest = Request with { Status = Request.StatusApproved };
            return await PutRequest(id, ApprovedRequest);
        }
        // PUT: api/Requests/Reject/5
        [HttpPut("reject/{id}")]
        public async Task<IActionResult> RejectRequest(int id, Request Request) {
            var RejectedRequest = Request with { Status = Request.StatusRejected };
            return await PutRequest(id, RejectedRequest);
        }

        // POST: api/Requests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(Request request) {
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequest", new { id = request.Id }, request);
        }

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(int id) {
            var request = await _context.Requests.FindAsync(id);
            if (request == null) {
                return NotFound();
            }

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestExists(int id) {
            return _context.Requests.Any(e => e.Id == id);
        }
    }
}
