using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Sample1.Models {
    public class TaxiContext : DbContext {
        public TaxiContext (DbContextOptions<TaxiContext> options) : base(options) { }

        public DbSet<Sample1.Models.TaxiSample> TaxiSample { get; set; }
    }
}
