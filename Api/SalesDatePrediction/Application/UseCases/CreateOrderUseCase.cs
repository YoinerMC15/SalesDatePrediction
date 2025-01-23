using SalesDatePrediction.Application.DTOs;
using SalesDatePrediction.Application.Interfaces;
using SalesDatePrediction.Domain.Entities;

namespace SalesDatePrediction.Application.UseCases
{
    public class CreateOrderUseCase
    {
        private readonly IOrderRepository _orderRepository;

        public CreateOrderUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task ExecuteAsync(CreateOrderRequest request)
        {
            
            var order = new Order
            {
                Empid = request.EmpId,
                Shipperid = request.ShipperId,
                Shipname = request.ShipName,
                Shipaddress = request.ShipAddress,
                Shipcity = request.ShipCity,
                Orderdate = request.OrderDate,
                Requireddate = request.RequiredDate,
                Freight = request.Freight,
                Shipcountry = request.ShipCountry
            };

            int orderId = await _orderRepository.CreateOrderAsync(order);

            
            var orderDetail = new OrderDetail
            {
                Orderid = orderId,
                Productid = request.ProductId,
                Unitprice = request.UnitPrice,
                Qty = (short)request.Quantity,
                Discount = request.Discount
            };

            await _orderRepository.AddOrderDetailAsync(orderDetail);
        }
    }
}
