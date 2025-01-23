using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Application.UseCases;

namespace SalesDatePrediction.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly GetAllEmployeesUseCase _useCase;

        public EmployeesController(GetAllEmployeesUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _useCase.ExecuteAsync();
            return Ok(employees);
        }
    }
}
