using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories.RegionRepository
{
    public class SqlRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext _dbContext;

        public SqlRegionRepository(NZWalksDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Region> AddAsync(Region region)
        {
            await _dbContext.Regions.AddAsync(region);
            await _dbContext.SaveChangesAsync();    
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid regionId)
        {
            var region = await _dbContext.Regions.FindAsync(regionId);

            if (region == null)
            {
                return null;
            }

            _dbContext.Regions.Remove(region);
            await _dbContext.SaveChangesAsync();

            return region;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await _dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid regionId)
        {
            return await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == regionId);
        }

        public async Task<Region?> UpdateAsync(Guid regionId, Region updatedRegion)
        {
            var region = await _dbContext.Regions.FindAsync(regionId);

            if (region == null)
            {
                return null;
            }
            // Map DTO to domain model
            region.Code = updatedRegion.Code;
            region.Name = updatedRegion.Name;
            region.RegionImageUrl = updatedRegion.RegionImageUrl;

            await _dbContext.SaveChangesAsync();

            return region;
        }
    }
}
