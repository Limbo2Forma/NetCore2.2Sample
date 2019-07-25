using Sample1.Models;
using System.Threading.Tasks;

namespace Sample1.Interfaces {
    public interface ITaxiRepository {
        Task<TaxiSample> GetByID(long id);
        Task<System.Collections.Generic.List<TaxiSample>> GetTop(long num);
        Task<decimal> CalculateDistanceOfTrip(long id);
    }
}
