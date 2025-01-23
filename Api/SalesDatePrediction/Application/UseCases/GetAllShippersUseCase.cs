using SalesDatePrediction.Application.DTOs;
using SalesDatePrediction.Application.Interfaces;

namespace SalesDatePrediction.Application.UseCases
{
    public class GetAllShippersUseCase
    {
        private readonly IShipperRepository _shipperRepository;

        public GetAllShippersUseCase(IShipperRepository shipperRepository)
        {
            _shipperRepository = shipperRepository;
        }

        public async Task<IEnumerable<ShipperDto>> ExecuteAsync()
        {
            var shippers = await _shipperRepository.GetAllShippersAsync();

            return shippers.Select(s => new ShipperDto
            {
                ShipperId = s.Shipperid,
                CompanyName = s.Companyname
            });
        }
    }
}
