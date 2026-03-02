using AgroSolutions.Properties.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroSolutions.Properties.Application.Interfaces
{
    public interface ICultureService
    {
        Task<CultureOutputDto> GetByIdAsync(Guid id);
        Task<IEnumerable<CultureOutputDto>> GetAllAsync();
        Task<CultureOutputDto> UpdateAsync(Guid id, CultureInputDto culture);
        Task AddAsync(CultureInputDto culture);
        Task RemoveAsync(Guid id);
    }
}
