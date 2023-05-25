using md.Data.Entites;
using md.Services.ViewModels;
using md.Services.ViewModels.ConsumerViewModel;
using System;
using System.Threading.Tasks;

namespace md.Services.IRepositories
{
    public interface IConsumerRepository
    {
        Task<Guid> CreateAsync(ConsumerRequest request);

        Task<bool> UpdateAsync(Consumer request);

        Task<bool> DeleteAsync(Guid consumerId);

        Task<Consumer> GetConsumerByIdAsync(Guid consumerId);
        Task<PagedResult<Consumer>> SearchConsumersByName(string textSearch, int pageNumber, int pageSize);
        Task<PagedResult<Consumer>> GetAllConsumerAsync(int pageNumber, int pageSize);
    }
}
