using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.WalkRepository
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
    }
}
