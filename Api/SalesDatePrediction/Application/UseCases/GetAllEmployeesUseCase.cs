using SalesDatePrediction.Application.DTOs;
using SalesDatePrediction.Application.Interfaces;

namespace SalesDatePrediction.Application.UseCases
{
    public class GetAllEmployeesUseCase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetAllEmployeesUseCase(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<EmployeeDto>> ExecuteAsync()
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync();

            return employees.Select(e => new EmployeeDto
            {
                EmpId = e.Empid,
                FullName = $"{e.Firstname} {e.Lastname}"
            });
        }
    }
}
