using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Application.UseCases;
using SalesDatePrediction.Application.DTOs;

namespace SalesDatePrediction.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly GetClientOrdersUseCase _getClientOrdersUseCase;
        private readonly CreateOrderUseCase _createOrderUseCase;

        private readonly GetSalesDatePredictionUseCase _getSalesDatePredictionUseCase;

        public OrdersController(GetClientOrdersUseCase getClientOrdersUseCase, CreateOrderUseCase createOrderUseCase, GetSalesDatePredictionUseCase getSalesDatePredictionUseCase)
        {
            _getClientOrdersUseCase = getClientOrdersUseCase;
            _createOrderUseCase = createOrderUseCase;
            _getSalesDatePredictionUseCase=getSalesDatePredictionUseCase;

        }

        [HttpGet("client/{clientId}")]
        public async Task<IActionResult> GetOrdersByClientId(int clientId)
        {
            if (clientId <= 0)
                return BadRequest("El ID del cliente debe ser un número positivo.");

            var orders = await _getClientOrdersUseCase.ExecuteAsync(clientId);

            if (orders == null || !orders.Any())
                return NotFound($"No se encontraron órdenes para el cliente con ID {clientId}.");

            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _createOrderUseCase.ExecuteAsync(request);
                return Ok("Orden creada exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error al crear la orden: {ex.Message}");
            }
        }


        [HttpGet("predictions")]
        public async Task<IActionResult> GetSalesDatePredictions()
        {
            try
            {
                var predictions = await _getSalesDatePredictionUseCase.ExecuteAsync();

                if (!predictions.Any())
                    return NotFound("No se encontraron predicciones de fechas de órdenes.");

                return Ok(predictions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error al calcular las predicciones: {ex.Message}");
            }
        }
    }
}
