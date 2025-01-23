using SalesDatePrediction.Domain.Entities;

namespace SalesDatePrediction.Application.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
    }
}