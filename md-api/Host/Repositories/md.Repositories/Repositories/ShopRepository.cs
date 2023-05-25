using md.Data.EF;
using md.Data.Entites;
using md.Repositories.IRepositories;
using md.Repositories.ViewModels.ShopViewModel;
using System;
using System.Threading.Tasks;

namespace md.Repositories.Repositories
{
    public class ShopRepository : IShopRepository
    {
        private readonly MdDbContext _context;
        public ShopRepository(MdDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> CreateAsync(ShopRequest request)
        {
            var shop = new Shop()
            {
                Id = Guid.NewGuid(),
                Location = request.Location,
                Name = request.Name
            };
            _context.Shops.Add(shop);
            await _context.SaveChangesAsync();
            return shop.Id;
        }

        public async Task<bool> DeleteAsync(Guid shopId)
        {
            var shop = await _context.Shops.FindAsync(shopId);
            if (shop == null) return false;
            _context.Shops.Remove(shop);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Shop> GetShopByIdAsync(Guid shopId)
        {
            return await _context.Shops.FindAsync(shopId);
        }

        public async Task<bool> UpdateAsync(Shop shopUpdate)
        {
            var shop = await _context.Shops.FindAsync(shopUpdate.Id);
            if (shop == null) return false;
            shop.Name = shopUpdate.Name;
            shop.Location = shopUpdate.Location;
            _context.Shops.Update(shop);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
