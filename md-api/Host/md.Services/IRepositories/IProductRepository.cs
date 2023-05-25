using md.Data.Entites;
using md.Services.ViewModels;
using md.Services.ViewModels.ProductViewModel;
using System;
using System.Threading.Tasks;

namespace md.Services.IRepositories
{
    public interface IProductRepository
    {
        Task<Guid> CreateAsync(ProductRequest productRequest);

        Task<bool> UpdateAsync(Product productUpdate);

        Task<bool> DeleteAsync(Guid productId);

        Task<Product> GetProductByIdAsync(Guid productId);
        Task<PagedResult<Product>> SearchProductsByName(string textSearch, int pageNumber, int pageSize);
        Task<PagedResult<Product>> GetAllProductAsync(int pageNumber, int pageSize);
    }
}
