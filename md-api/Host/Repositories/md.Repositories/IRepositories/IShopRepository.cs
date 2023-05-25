using md.Data.Entites;
using md.Repositories.ViewModels.ShopViewModel;
using System;
using System.Threading.Tasks;

namespace md.Repositories.IRepositories
{
    public interface IShopRepository
    {
        Task<Guid> CreateAsync(ShopRequest request);

        Task<bool> UpdateAsync(Shop shopUpdate);

        Task<bool> DeleteAsync(Guid id);

        Task<Shop> GetShopByIdAsync(Guid id);
    }
}
