using ProductService.Models;
using ProductService.Repositories;

namespace ProductService.Services
{
    public class ProductsService : IProductsService
    {

        private readonly IProductRepository _repository;

        public ProductsService(IProductRepository repository)
        {
            _repository = repository;
        }


        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddProductAsync(Product product)
        {
            await _repository.AddAsync(product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _repository.UpdateAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
