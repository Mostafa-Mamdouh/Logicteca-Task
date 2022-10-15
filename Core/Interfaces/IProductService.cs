
using Core.Entities;
using Core.Model;

namespace Core.Interfaces
{
    public interface IProductService
    {
        Task SaveProducts(List<Product> products);
        Task<List<Product>> GetProducts(FilterParams filterParams);
        Task<int> GetProductsCount(FilterParams filterParams);

    }
}
