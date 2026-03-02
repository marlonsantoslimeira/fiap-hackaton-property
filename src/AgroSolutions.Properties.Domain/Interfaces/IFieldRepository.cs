using AgroSolutions.Properties.Domain.Entities;

namespace AgroSolutions.Properties.Domain.Interfaces
{
    public interface IFieldRepository
    {
        Task<Field?> GetByIdAsync(Guid id);
        Task<List<Field>> GetAllAsync();
        Task UpdateAsync(Guid id, Field entity);
        Task AddAsync(Field field);
        Task DeleteAsync(Guid id);

    }
}
