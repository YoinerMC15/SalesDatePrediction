using Microsoft.EntityFrameworkCore;
using SalesDatePrediction.Application.DTOs;
using SalesDatePrediction.Application.Interfaces;
using SalesDatePrediction.Domain.Entities;

namespace SalesDatePrediction.Infrastructure.Persistence
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetOrdersByClientIdAsync(int clientId)
        {
            return await _context.Orders
                .Where(o => o.Custid == clientId)
                .ToListAsync();
        }
        public async Task<int> CreateOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order.Orderid;
        }

        public async Task AddOrderDetailAsync(OrderDetail orderDetail)
        {
            await _context.OrderDetails.AddAsync(orderDetail);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SalesDatePredictionDto>> GetSalesDatePredictionsAsync()
        {
            var customers = await _context.Customers.ToListAsync();
            var predictions = new List<SalesDatePredictionDto>();

            foreach (var customer in customers)
            {
                var orderDates = _context.Orders
                    .Where(o => o.Custid == customer.Custid)
                    .OrderBy(o => o.Orderdate)
                    .Select(o => o.Orderdate)
                    .ToList();

                if (!orderDates.Any())
                    continue; 

                var lastOrderDate = orderDates.Last();
                var totalDays = 0.0;

                for (int i = 1; i < orderDates.Count; i++)
                {
                    totalDays += (orderDates[i] - orderDates[i - 1]).TotalDays;
                }

                var averageDays = orderDates.Count > 1 ? totalDays / (orderDates.Count - 1) : 0;
                var nextPredictedOrder = lastOrderDate.AddDays(averageDays);

                predictions.Add(new SalesDatePredictionDto
                {
                    CustId = customer.Custid,
                    CustomerName = customer.Companyname,
                    LastOrderDate = lastOrderDate,
                    NextPredictedOrder = nextPredictedOrder
                });
            }

            return predictions;
        }

    }
}
