using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.RegionRepository
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();
        Task<Region?> GetByIdAsync(Guid regionId);
        Task<Region> AddAsync(Region region);
        Task<Region?> UpdateAsync(Guid regionId, Region region);
        Task<Region?> DeleteAsync(Guid regionId);
    }
}
