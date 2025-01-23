using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Application.UseCases;

namespace SalesDatePrediction.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShippersController : ControllerBase
    {
        private readonly GetAllShippersUseCase _useCase;

        public ShippersController(GetAllShippersUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllShippers()
        {
            var shippers = await _useCase.ExecuteAsync();
            return Ok(shippers);
        }
    }
}
