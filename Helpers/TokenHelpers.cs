using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ShopDH.Models;

public class TokenHelpers
{
    private readonly JWTConfiguration _jwtConfig;

    public TokenHelpers(IOptions<Appsettings> jwt)
    {
        _jwtConfig = jwt.Value.JWTConfiguration;
    }

    public string GenarateAccessToken(Claim[] claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key));
        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            _jwtConfig.Issuer,
            _jwtConfig.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(10),
            signingCredentials: signIn);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    

}