using Microsoft.EntityFrameworkCore;
using SalesDatePrediction.Application.Interfaces;
using SalesDatePrediction.Domain.Entities;

namespace SalesDatePrediction.Infrastructure.Persistence
{
    public class ShipperRepository : IShipperRepository
    {
        private readonly ApplicationDbContext _context;

        public ShipperRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Shipper>> GetAllShippersAsync()
        {
            return await _context.Shippers.ToListAsync();
        }
    }
}
