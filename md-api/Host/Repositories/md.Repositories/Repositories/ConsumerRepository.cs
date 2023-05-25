using md.Data.EF;
using md.Data.Entites;
using md.Repositories.IRepositories;
using md.Repositories.ViewModels.ConsumerViewModel;
using System;
using System.Threading.Tasks;

namespace md.Repositories.Repositories
{
    public class ConsumerRepository : IConsumerRepository
    {
        private readonly MdDbContext _context;
        public ConsumerRepository(MdDbContext context)
        {
            _context = context;
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
    }
}
