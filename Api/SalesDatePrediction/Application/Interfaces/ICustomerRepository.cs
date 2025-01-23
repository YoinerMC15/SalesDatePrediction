using SalesDatePrediction.Domain.Entities;
namespace SalesDatePrediction.Application.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomersWithOrdersAsync();
    }
}
