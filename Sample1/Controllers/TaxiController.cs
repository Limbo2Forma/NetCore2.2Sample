using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sample1.Models;

namespace Sample1.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TaxiController : ControllerBase {
        private readonly TaxiContext _context;

        public TaxiController(TaxiContext context) {
            _context = context;
        }

        // GET: api/Taxi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaxiSample>>> GetTaxiSample() {
            return await _context.TaxiSample.ToListAsync();
        }

        // GET: api/Taxi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaxiSample>> GetTaxiSample(long id) {
            var taxiSample = await _context.TaxiSample.FindAsync(id);

            if (taxiSample == null) {
                return NotFound();
            }

            return taxiSample;
        }     
    }
}
