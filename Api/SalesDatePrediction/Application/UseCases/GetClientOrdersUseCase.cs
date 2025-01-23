using SalesDatePrediction.Application.DTOs;
using SalesDatePrediction.Application.Interfaces;

namespace SalesDatePrediction.Application.UseCases
{
    public class GetClientOrdersUseCase
    {
        private readonly IOrderRepository _orderRepository;

        public GetClientOrdersUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<OrderDto>> ExecuteAsync(int clientId)
        {
            var orders = await _orderRepository.GetOrdersByClientIdAsync(clientId);

    
            return orders.Select(o => new OrderDto
            {
                OrderId = o.Orderid,
                RequiredDate = o.Requireddate,
                ShippedDate = o.Shippeddate,
                ShipName = o.Shipname,
                ShipAddress = o.Shipaddress,
                ShipCity = o.Shipcity
            });
        }
    }
}
