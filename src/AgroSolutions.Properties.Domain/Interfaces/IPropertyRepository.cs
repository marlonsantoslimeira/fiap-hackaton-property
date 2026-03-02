using AgroSolutions.Properties.Domain.Entities;

namespace AgroSolutions.Properties.Domain.Interfaces
{
    public interface IPropertyRepository
    {
        Task<Property> GetByIdAsync(Guid id);
        Task<IEnumerable<Property>> GetAllAsync();
        Task UpdateAsync(Guid id, Property property);
        Task AddAsync(Property property);
        Task DeleteAsync(Guid property);

    }
}
