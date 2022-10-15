using Core.Entities;
using Core.Interfaces;
using Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Services
{
    public class ProductService : IProductService
    {
        private readonly DatabaseContext _dbContext;

        public ProductService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Product>> GetProducts(FilterParams filterParams)
        {
          return await _dbContext.Products.AsNoTracking().Where(x =>
                  string.IsNullOrEmpty(filterParams.Search) ||
                  x.Band.ToString().ToLower().Contains(filterParams.Search) ||
                  x.PartSKU.ToLower().Contains(filterParams.Search) ||
                  x.Manufacturer.ToLower().Contains(filterParams.Search) ||
                  x.ItemDescription.ToLower().Contains(filterParams.Search) ||
                  x.CategoryCode.ToLower().Contains(filterParams.Search)
                  ).Skip(filterParams.PageSize * (filterParams.PageIndex - 1)).Take(filterParams.PageSize).ToListAsync();
        }

        public async Task<int> GetProductsCount(FilterParams filterParams)
        {
            return await _dbContext.Products.AsNoTracking().Where(x =>
                    string.IsNullOrEmpty(filterParams.Search) ||
                    x.Band.ToString().ToLower().Contains(filterParams.Search) ||
                    x.PartSKU.ToLower().Contains(filterParams.Search) ||
                    x.Manufacturer.ToLower().Contains(filterParams.Search) ||
                    x.ItemDescription.ToLower().Contains(filterParams.Search) ||
                    x.CategoryCode.ToLower().Contains(filterParams.Search)
                    ).CountAsync();
        }

        public async Task SaveProducts(List<Product> products)
        {
            await _dbContext.BulkMergeAsync(products, options => options.ColumnPrimaryKeyExpression = c => c.PartSKU);
        }
    }
}
