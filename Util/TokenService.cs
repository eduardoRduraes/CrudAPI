using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace CrudAPI.Util;

public class TokenService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TokenService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? GetOngIdFromToken()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        
        if(httpContext == null)
            return null;
        
        var authHeader = httpContext.Request.Headers["Authorization"].FirstOrDefault();
        
        if(string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer"))
            return null;
        
        var token = authHeader.Substring("Bearer ".Length);

        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token) as JwtSecurityToken;

        if (jwtToken == null)
            return null;
        
        var ongIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);
        
        return ongIdClaim != null ? Guid.Parse(ongIdClaim.Value) : null;
    }

}