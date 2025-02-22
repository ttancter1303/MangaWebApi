using System.Security.Claims;

namespace DemoApp.Domain.Abstractions.InfrastructureServices;

public interface IJwtTokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);

}