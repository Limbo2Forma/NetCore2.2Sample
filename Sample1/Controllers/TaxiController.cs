using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sample1.Interfaces;
using Sample1.Models;

namespace Sample1.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TaxiController : ControllerBase {
        private readonly ITaxiRepository _taxiRepo;

        public TaxiController(ITaxiRepository taxiRepo) {
            _taxiRepo = taxiRepo;
        }

        // GET: api/Taxi/5/Details
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<TaxiSample>>> GetFirstTaxis(long id) {
            return await _taxiRepo.GetTop(id);
        }

        // GET: api/Taxi/5/Details
        [HttpGet("{id}/Details")]
        public async Task<ActionResult<TaxiSample>> GetTaxiSample(long id) {
            return await _taxiRepo.GetByID(id);
        }

        // GET: api/Taxi/5/Distance
        [HttpGet("{id}/Distance")]
        public async Task<ActionResult<string>> GetTaxiTripDistance(long id) {
            var result = await _taxiRepo.CalculateDistanceOfTrip(id);
            return result.ToString() + " km";
        }
    }
}
