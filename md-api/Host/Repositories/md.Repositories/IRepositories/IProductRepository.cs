using md.Data.Entites;
using md.Repositories.ViewModels.ProductViewModel;
using System;
using System.Threading.Tasks;

namespace md.Repositories.IRepositories
{
    public interface IProductRepository
    {
        Task<Guid> CreateAsync(ProductRequest productRequest);

        Task<bool> UpdateAsync(Product productUpdate);

        Task<bool> DeleteAsync(Guid productId);

        Task<Product> GetProductByIdAsync(Guid productId);
    }
}
