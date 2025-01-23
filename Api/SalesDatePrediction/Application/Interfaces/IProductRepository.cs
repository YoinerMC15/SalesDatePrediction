using SalesDatePrediction.Domain.Entities;

namespace SalesDatePrediction.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
    }
}
