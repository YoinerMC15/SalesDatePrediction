using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Application.UseCases;

namespace SalesDatePrediction.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly GetAllProductsUseCase _useCase;

        public ProductsController(GetAllProductsUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _useCase.ExecuteAsync();
            return Ok(products);
        }
    }
}
