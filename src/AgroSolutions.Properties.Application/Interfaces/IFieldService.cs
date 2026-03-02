using AgroSolutions.Properties.Application.DTOs;

namespace AgroSolutions.Properties.Application.Interfaces
{
    public interface IFieldService
    {

        Task<FieldOutputDto> GetByIdAsync(Guid id);
        Task<IEnumerable<FieldOutputDto>> GetAllAsync();
        Task<FieldOutputDto> UpdateAsync(Guid id, FieldInputDto field);
        Task AddAsync(FieldInputDto field);
        Task RemoveAsync(Guid id);
    }
}
