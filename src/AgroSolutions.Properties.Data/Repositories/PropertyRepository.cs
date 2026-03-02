using AgroSolutions.Properties.Domain.Entities;
using AgroSolutions.Properties.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AgroSolutions.Properties.Data.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly PropertiesContext _ctx;

        public PropertyRepository(PropertiesContext ctx)
        {
            _ctx = ctx;
        }

        public async Task AddAsync(Property property)
        {
            if (property is null)
                throw new ArgumentNullException("Field must be valid");

            await _ctx.Properties.AddAsync(property);
            await _ctx.SaveChangesAsync();

        }

        public async Task DeleteAsync(Guid id)
        {
            var property = await _ctx.Properties.FirstOrDefaultAsync(x => x.Id == id);

            if (property is null)
                return;

            _ctx.Properties.Remove(property);
            await _ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<Property>> GetAllAsync()
        {
            return await _ctx.Properties.ToListAsync();
        }

        public async Task<Property> GetByIdAsync(Guid id)
        {
            var property = await _ctx.Properties.FirstOrDefaultAsync(x => x.Id == id);

            if (property is null)
                return null;

            return property;

        }

        public async Task UpdateAsync(Guid id, Property entity)
        {
            var property = await _ctx.Properties.FirstOrDefaultAsync(x => x.Id == id);

            if (property is null)
                return;

            property.Name = entity.Name;
            property.Location = entity.Location;

            _ctx.Properties.Update(property);
            await _ctx.SaveChangesAsync();

        }
    }
}
