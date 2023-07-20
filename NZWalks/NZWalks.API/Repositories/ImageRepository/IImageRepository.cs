using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.ImageRepository
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
