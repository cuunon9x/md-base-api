using md.Data.Entites;
using md.Services.ViewModels;
using md.Services.ViewModels.ShopViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace md.Services.IRepositories
{
    public interface IShopRepository
    {
        Task<Guid> CreateAsync(ShopRequest shopRequest);

        Task<bool> UpdateAsync(Shop shop);

        Task<bool> DeleteAsync(Guid guid);

        Task<Shop> GetShopByIdAsync(Guid guid);
        List<Shop> SearchShopsByName(string textSearch);
        Task<PagedResult<Shop>> GetAllShopAsync(int pageNumber, int pageSize);
    }
}
