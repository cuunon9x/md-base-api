using md.Data.Entites;
using md.Repositories.ViewModels.ConsumerViewModel;
using System;
using System.Threading.Tasks;

namespace md.Repositories.IRepositories
{
    public interface IConsumerRepository
    {
        Task<Guid> CreateAsync(ConsumerRequest request);

        Task<bool> UpdateAsync(Consumer request);

        Task<bool> DeleteAsync(Guid consumerId);

        Task<Consumer> GetConsumerByIdAsync(Guid consumerId);
    }
}
