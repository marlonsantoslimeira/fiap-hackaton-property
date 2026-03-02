using AgroSolutions.Properties.Application.DTOs;
using AgroSolutions.Properties.Application.Interfaces;
using AgroSolutions.Properties.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroSolutions.Properties.Application.Services
{
    public class FieldService : IFieldService
    {
        private readonly IFieldRepository _fieldRepository;

        public FieldService(IFieldRepository fieldRepository)
        {
            _fieldRepository = fieldRepository;
        }
        public async Task AddAsync(FieldInputDto field)
        {
            if (field is null)
                throw new ArgumentException("Field must not be null");

            var fieldEntity = new Domain.Entities.Field
            {
                Id = Guid.NewGuid(),
                Name = field.Name,
                CultureId = field.CultureId,
                PropertyId = field.PropertyId,
                CreatedAt = DateTime.UtcNow
            };

            await _fieldRepository.AddAsync(fieldEntity);

        }

        public async Task<IEnumerable<FieldOutputDto>> GetAllAsync()
        {
            var fields = await _fieldRepository.GetAllAsync();
            return fields.Select(f => new FieldOutputDto
            {
                Id = f.Id,
                Name = f.Name,
                Culture = f.CultureId,
                PropertyId = f.PropertyId,
                CreatedAt = f.CreatedAt
            });
        }

        public async Task<FieldOutputDto> GetByIdAsync(Guid id)
        {
            var field = await _fieldRepository.GetByIdAsync(id);

            if (field is null)
                return null;

            return new FieldOutputDto
            {
                Id = field.Id,
                Name = field.Name,
                Culture = field.CultureId,
                PropertyId = field.PropertyId,
                CreatedAt = field.CreatedAt,
            };

        }

        public async Task RemoveAsync(Guid id)
        {
            await _fieldRepository.DeleteAsync(id);
        }

        public async Task<FieldOutputDto> UpdateAsync(Guid id, FieldInputDto field)
        {
            var fieldEntity = await _fieldRepository.GetByIdAsync(id);

            if(fieldEntity is null)
                return null;


            fieldEntity.Name = field.Name;
            fieldEntity.CultureId = field.CultureId;
            fieldEntity.PropertyId = field.PropertyId;
            
            await _fieldRepository.UpdateAsync(id, fieldEntity);

            return new FieldOutputDto
            {
                Id = fieldEntity.Id,
                Name = fieldEntity.Name,
                Culture = fieldEntity.CultureId,
                PropertyId = fieldEntity.PropertyId,
                CreatedAt = fieldEntity.CreatedAt
            };
        }
    }
}
