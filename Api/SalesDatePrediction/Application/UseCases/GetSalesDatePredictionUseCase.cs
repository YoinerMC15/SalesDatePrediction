using SalesDatePrediction.Application.DTOs;
using SalesDatePrediction.Application.Interfaces;

namespace SalesDatePrediction.Application.UseCases
{
    public class GetSalesDatePredictionUseCase
    {
        private readonly IOrderRepository _orderRepository;

        public GetSalesDatePredictionUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<SalesDatePredictionDto>> ExecuteAsync()
        {
            return await _orderRepository.GetSalesDatePredictionsAsync();
        }
    }
}
