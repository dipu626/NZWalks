using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Repositories.TokenRepository
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, IList<string> Roles);
    }
}
