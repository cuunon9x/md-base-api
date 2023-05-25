using md.Data.EF;
using md.Data.Entites;
using md.Repositories.IRepositories;
using md.Repositories.ViewModels.ProductViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace md.Repositories.Repositories
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

        public async Task<Product> GetProductByIdAsync(Guid productId)
        {
            return await _context.Products.FindAsync(productId);
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
