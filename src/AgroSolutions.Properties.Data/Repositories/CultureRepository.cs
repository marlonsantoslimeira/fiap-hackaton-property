using AgroSolutions.Properties.Domain.Entities;
using AgroSolutions.Properties.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AgroSolutions.Properties.Data.Repositories
{
    public class CultureRepository : ICultureRepository
    {
        private readonly PropertiesContext _ctx;

        public CultureRepository(PropertiesContext ctx)
        {
            _ctx = ctx;
        }

        public async Task AddAsync(Culture culture)
        {
            if (culture is null)
                throw new ArgumentNullException("Field must be valid");

            await _ctx.Cultures.AddAsync(culture);
            await _ctx.SaveChangesAsync();

        }

        public async Task DeleteAsync(Guid id)
        {
            var culture = await _ctx.Cultures.FirstOrDefaultAsync(x => x.Id == id);

            if (culture is null)
                return;

            _ctx.Cultures.Remove(culture);
            await _ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<Culture>> GetAllAsync()
        {
            return await _ctx.Cultures.ToListAsync();
        }

        public async Task<Culture> GetByIdAsync(Guid id)
        {
            var culture = await _ctx.Cultures.FirstOrDefaultAsync(x => x.Id == id);

            if (culture is null)
                return null;

            return culture;

        }

        public async Task UpdateAsync(Guid id, Culture entity)
        {
            var culture = await _ctx.Cultures.FirstOrDefaultAsync(x => x.Id == id);

            if (culture is null)
                return;

            culture.Name = entity.Name;
            culture.MaxTemperature = entity.MaxTemperature;
            culture.MinTemperature = entity.MinTemperature;
            culture.MaxMoist = entity.MaxMoist;
            culture.MinMoist = entity.MinMoist;
            culture.MaxPrecipitation = entity.MaxPrecipitation;
            culture.MinPrecipitation = entity.MinPrecipitation;


            _ctx.Cultures.Update(culture);
            await _ctx.SaveChangesAsync();

        }
    }
}
