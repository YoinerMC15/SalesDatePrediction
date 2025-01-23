using SalesDatePrediction.Application.DTOs;
using SalesDatePrediction.Application.Interfaces;

namespace SalesDatePrediction.Application.UseCases
{
    public class GetAllProductsUseCase
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDto>> ExecuteAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();

            return products.Select(p => new ProductDto
            {
                ProductId = p.Productid,
                ProductName = p.Productname
            });
        }
    }
}
