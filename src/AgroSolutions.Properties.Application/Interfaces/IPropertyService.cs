using AgroSolutions.Properties.Application.DTOs;

namespace AgroSolutions.Properties.Application.Interfaces
{
    public interface IPropertyService
    {

        Task<PropertyOutputDto> GetByIdAsync(Guid id);
        Task<IEnumerable<PropertyOutputDto>> GetAllAsync();
        Task<PropertyOutputDto> UpdateAsync(Guid id, PropertyInputDto property);
        Task AddAsync(PropertyInputDto property);
        Task RemoveAsync(Guid id);
    }
}
