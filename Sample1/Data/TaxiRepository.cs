using Dapper;
using Microsoft.Extensions.Configuration;
using Sample1.Interfaces;
using Sample1.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Sample1.Data {
    public class TaxiRepository : ITaxiRepository {
        private readonly IConfiguration _config;
        public TaxiRepository(IConfiguration config) {
            _config = config;
        }

        public IDbConnection Connection {
            get {
                return new System.Data.SqlClient.SqlConnection
                    (_config.GetConnectionString("TaxiContext"));
            }
        }

        public async Task<TaxiSample> GetByID(long id) {
            using (IDbConnection conn = Connection) {
                string sQuery = "SELECT * FROM dbo.TaxiSample WHERE Id = @id";
                conn.Open();
                var result = await conn.QueryAsync<TaxiSample>(sQuery, new { id });
                return result.FirstOrDefault();
            }
        }

        public async Task<List<TaxiSample>> GetTop(long num) {
            using (IDbConnection conn = Connection) {
                string sQuery = "SELECT TOP(@num)* FROM dbo.TaxiSample";
                conn.Open();
                var result = await conn.QueryAsync<TaxiSample>(sQuery, new { num });
                return result.ToList();
            }
        }

        public async Task<decimal> CalculateDistanceOfTrip(long id) {
            using (IDbConnection conn = Connection) {
                string sQuery = 
                    "SELECT dbo.fnCalculateDistance(pickup_longitude,pickup_latitude," +
                    "dropoff_longitude,dropoff_latitude) FROM dbo.TaxiSample WHERE Id = @id";
                conn.Open();
                var result = await conn.QueryAsync<decimal>(sQuery, new { id });
                return result.First();
            }
        }
    }
}
