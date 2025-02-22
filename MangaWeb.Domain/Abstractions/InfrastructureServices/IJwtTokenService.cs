using System.Security.Claims;

namespace MangaWeb.Domain.Abstractions.InfrastructureServices;

public interface IJwtTokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);

}