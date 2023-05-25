using md.Data.EF;
using md.Data.Entites;
using md.Services.IRepositories;
using md.Services.ViewModels;
using md.Services.ViewModels.ConsumerViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace md.Services.Repositories
{
    public class ConsumerRepository : IConsumerRepository
    {
        private readonly MdDbContext _context;
        public ConsumerRepository(MdDbContext context)
        {
            _context = context;
        }
        public async Task<PagedResult<Consumer>> GetAllConsumerAsync(int pageNumber, int pageSize)
        {
            var query = from c in _context.Consumers
                        select new { c };

            int totalRow = await query.CountAsync();
            var data = new List<Consumer>();
            if (totalRow != 0)
            {
                data = await query.Skip((pageNumber - 1) * pageSize)
               .Take(pageSize)
               .Select(x => new Consumer()
               {
                   Id = x.c.Id,
                   Dob = x.c.Dob,
                   FullName = x.c.FullName,
                   Email = x.c.Email
               }).OrderBy(x => x.Email).ToListAsync();
            }
            var pagedResult = new PagedResult<Consumer>()
            {
                TotalResults = totalRow,
                PageSize = pageSize,
                PageNumber = pageNumber,
                Data = data
            };
            return pagedResult;
        }
        public async Task<Guid> CreateAsync(ConsumerRequest request)
        {
            var consumer = new Consumer()
            {
                Id = Guid.NewGuid(),
                Dob = request.Dob,
                Email = request.Email,
                FullName = request.FullName
            };
            _context.Consumers.Add(consumer);
            await _context.SaveChangesAsync();
            return consumer.Id;
        }

        public async Task<bool> DeleteAsync(Guid consumerId)
        {
            var consumer = await _context.Consumers.FindAsync(consumerId);
            if (consumer == null) return false;
            _context.Consumers.Remove(consumer);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Consumer> GetConsumerByIdAsync(Guid consumerId)
        {
            return await _context.Consumers.FindAsync(consumerId);
        }

        public async Task<bool> UpdateAsync(Consumer consumerUpdate)
        {
            var consumer = await _context.Consumers.FindAsync(consumerUpdate.Id);
            if (consumer == null) return false;
            consumer.Email = consumerUpdate.Email;
            consumer.FullName = consumerUpdate.FullName;
            consumer.Dob = consumerUpdate.Dob;
            _context.Consumers.Update(consumer);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<PagedResult<Consumer>> SearchConsumersByName(string textSearch, int pageNumber, int pageSize)
        {
            if (string.IsNullOrWhiteSpace(textSearch))
                return null;
            var query = from c in _context.Consumers
                        select new { c };
            int totalRow = await query.CountAsync();
            var data = new List<Consumer>();
            if (totalRow != 0)
            {
                data = query.Skip((pageNumber - 1) * pageSize)
                  .Take(pageSize)
                  .Select(x => new Consumer()
                  {
                      Id = x.c.Id,
                      Dob = x.c.Dob,
                      FullName = x.c.FullName,
                      Email = x.c.Email
                  }).OrderBy(x => x.Email).ToList();
                data= data.Where(x => x.Email.Contains(textSearch, StringComparison.OrdinalIgnoreCase)).OrderBy(x => x.Email).ToList();
            }
            var pagedResult = new PagedResult<Consumer>()
            {
                TotalResults = data.Count,
                PageSize = pageSize,
                PageNumber = pageNumber,
                Data = data
            };
            return pagedResult;
        }
    }
}
