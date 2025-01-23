using SalesDatePrediction.Domain.Entities;

namespace SalesDatePrediction.Application.Interfaces
{
    public interface IShipperRepository
    {
        Task<IEnumerable<Shipper>> GetAllShippersAsync();
    }
}
