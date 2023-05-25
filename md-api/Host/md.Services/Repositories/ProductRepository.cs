using md.Data.EF;
using md.Data.Entites;
using md.Services.IRepositories;
using md.Services.ViewModels;
using md.Services.ViewModels.ProductViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace md.Services.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MdDbContext _context;
        public ProductRepository(MdDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> CreateAsync(ProductRequest productRequest)
        {
            var product = new Product()
            {
                Id = Guid.NewGuid(),
                Price = productRequest.Price,
                Name = productRequest.Name
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }

        public async Task<bool> DeleteAsync(Guid productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return false;
            _context.Products.Remove(product);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<PagedResult<Product>> GetAllProductAsync(int pageNumber, int pageSize)
        {
            var query = from p in _context.Products
                        select new { p };

            int totalRow = await query.CountAsync();

            var data = await query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new Product()
                {
                    Id = x.p.Id,
                    Name = x.p.Name,
                    Price = x.p.Price
                }).OrderBy(x => x.Name).ToListAsync();

            var pagedResult = new PagedResult<Product>()
            {
                TotalResults = totalRow,
                PageSize = pageSize,
                PageNumber = pageNumber,
                Data = data
            };
            return pagedResult;
        }

        public async Task<Product> GetProductByIdAsync(Guid productId)
        {
            return await _context.Products.FindAsync(productId);
        }

        public async Task<PagedResult<Product>> SearchProductsByName(string textSearch, int pageNumber, int pageSize)
        {
            if (string.IsNullOrWhiteSpace(textSearch))
                return null;
            var query = from p in _context.Products
                        select new { p };

            int totalRow = await query.CountAsync();
            var data = new List<Product>();
            if (totalRow != 0)
            {
                data = query.Skip((pageNumber - 1) * pageSize)
                  .Take(pageSize)
                  .Select(x => new Product()
                  {
                      Id = x.p.Id,
                      Name = x.p.Name,
                      Price = x.p.Price
                  }).ToList();
                data = data.Where(x => x.Name.Contains(textSearch, StringComparison.OrdinalIgnoreCase)).OrderBy(x=>x.Name).ToList();
            }
            var pagedResult = new PagedResult<Product>()
            {
                TotalResults = data.Count,
                PageSize = pageSize,
                PageNumber = pageNumber,
                Data = data
            };
            return pagedResult;
        }

        public async Task<bool> UpdateAsync(Product productUpdate)
        {
            var product = await _context.Products.FindAsync(productUpdate.Id);
            if (product == null) return false;
            product.Name = productUpdate.Name;
            product.Price = productUpdate.Price;
            _context.Products.Update(product);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
