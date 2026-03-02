using AgroSolutions.Properties.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroSolutions.Properties.Domain.Interfaces
{
    public interface ICultureRepository
    {
        Task<Culture> GetByIdAsync(Guid id);
        Task<IEnumerable<Culture>> GetAllAsync();
        Task UpdateAsync(Guid id, Culture entity);
        Task AddAsync(Culture culture);
        Task DeleteAsync(Guid culture);

    }
}
