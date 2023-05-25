using md.Data.EF;
using md.Data.Entites;
using md.Services.IRepositories;
using md.Services.ViewModels;
using md.Services.ViewModels.ShopViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace md.Services.Repositories
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

        public async Task<bool> DeleteAsync(Guid guid)
        {
            var shop = await _context.Shops.FindAsync(guid);
            if (shop == null) return false;
            _context.Shops.Remove(shop);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<PagedResult<Shop>> GetAllShopAsync(int pageNumber, int pageSize)
        {
            var query = from s in _context.Shops
                        select new { s };

            int totalRow = await query.CountAsync();

            var data = await query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new Shop()
                {
                    Id = x.s.Id,
                    Name = x.s.Name,
                    Location = x.s.Location
                }).OrderByDescending(x => x.Location).ToListAsync();

            var pagedResult = new PagedResult<Shop>()
            {
                TotalResults = totalRow,
                PageSize = pageSize,
                PageNumber = pageNumber,
                Data = data
            };
            return pagedResult;
        }

        public async Task<Shop> GetShopByIdAsync(Guid ShopId)
        {
            return await _context.Shops.FindAsync(ShopId);
        }

        public List<Shop> SearchShopsByName(string textSearch)
        {
            if (string.IsNullOrWhiteSpace(textSearch))
                return null;
            var Shops = _context.Shops.Where(p => p.Name.Contains(textSearch)).OrderBy(p => p.Name);
            return Shops.ToList();
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
