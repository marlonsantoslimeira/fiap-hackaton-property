using AgroSolutions.Properties.Application.DTOs;
using AgroSolutions.Properties.Application.Interfaces;
using AgroSolutions.Properties.Domain.Entities;
using AgroSolutions.Properties.Domain.Interfaces;

namespace AgroSolutions.Properties.Application.Services
{
    public class CultureService : ICultureService
    {
        private readonly ICultureRepository _cultureRepository;

        public CultureService(ICultureRepository cultureRepository)
        {
            _cultureRepository = cultureRepository;
        }

        public async Task AddAsync(CultureInputDto culture)
        {
            if (culture is null)
                return;

            var entity = new Culture
            {
                Name = culture.Name,
                MaxTemperature = culture.MaxTemperature,
                MinTemperature = culture.MinTemperature,
                MaxMoist = culture.MaxMoist,
                MinMoist = culture.MinMoist,
                MaxPrecipitation = culture.MaxPrecipitation,
                MinPrecipitation = culture.MinPrecipitation,
                CreatedAt = DateTime.UtcNow
            };
            await _cultureRepository.AddAsync(entity);
        }

        public async Task<IEnumerable<CultureOutputDto>> GetAllAsync()
        {
            var cultures = await _cultureRepository.GetAllAsync();
            if(cultures is null || !cultures.Any())
                return Enumerable.Empty<CultureOutputDto>();

            return cultures.Select(c => new CultureOutputDto
            {
                Id = c.Id,
                Name = c.Name,
                MaxTemperature = c.MaxTemperature,
                MinTemperature = c.MinTemperature,
                MaxMoist = c.MaxMoist,
                MinMoist = c.MinMoist,
                MaxPrecipitation = c.MaxPrecipitation,
                MinPrecipitation = c.MinPrecipitation,
                CreatedAt = c.CreatedAt
            });
        }

        public async Task<CultureOutputDto> GetByIdAsync(Guid id)
        {
            var culture = await _cultureRepository.GetByIdAsync(id);
            if (culture is null)
                return null;

            return new CultureOutputDto
            {
                Id = culture.Id,
                Name = culture.Name,
                MaxTemperature = culture.MaxTemperature,
                MinTemperature = culture.MinTemperature,
                MaxMoist = culture.MaxMoist,
                MinMoist = culture.MinMoist,
                MaxPrecipitation = culture.MaxPrecipitation,
                MinPrecipitation = culture.MinPrecipitation,
                CreatedAt = culture.CreatedAt
            };
        }

        public async Task RemoveAsync(Guid id)
        {
            var culture = await _cultureRepository.GetByIdAsync(id);
            if (culture is null)
                return;

            await _cultureRepository.DeleteAsync(culture.Id);
        }

        public async Task<CultureOutputDto> UpdateAsync(Guid id, CultureInputDto culture)
        {       
            var entity = await _cultureRepository.GetByIdAsync(id);

            if (entity is null)
                return null;

            entity.Name = culture.Name;
            entity.MaxTemperature = culture.MaxTemperature;
            entity.MinTemperature = culture.MinTemperature;
            entity.MaxMoist = culture.MaxMoist;
            entity.MinMoist = culture.MinMoist;
            entity.MaxPrecipitation = culture.MaxPrecipitation;
            entity.MinPrecipitation = culture.MinPrecipitation;

            await _cultureRepository.UpdateAsync(entity.Id, entity);

            return new CultureOutputDto
            {
                Id = entity.Id,
                Name = entity.Name,
                MaxTemperature = entity.MaxTemperature,
                MinTemperature = entity.MinTemperature,
                MaxMoist = entity.MaxMoist,
                MinMoist = entity.MinMoist,
                MaxPrecipitation = entity.MaxPrecipitation,
                MinPrecipitation = entity.MinPrecipitation,
                CreatedAt = entity.CreatedAt
            };
        }
    }
}
