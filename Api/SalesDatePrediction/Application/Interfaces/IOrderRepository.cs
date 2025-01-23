using SalesDatePrediction.Application.DTOs;
using SalesDatePrediction.Domain.Entities;

namespace SalesDatePrediction.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrdersByClientIdAsync(int clientId);

        Task<int> CreateOrderAsync(Order order);

        Task AddOrderDetailAsync(OrderDetail orderDetail);

        Task<IEnumerable<SalesDatePredictionDto>> GetSalesDatePredictionsAsync();
        
    }
}