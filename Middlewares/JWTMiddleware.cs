using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ShopDH.Middlewares;

public class JWTMiddleware
{
    private readonly RequestDelegate _next;

    public JWTMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (!string.IsNullOrEmpty(token))
        {
            var tokenDecoded = new JwtSecurityToken(jwtEncodedString: token);

            string username = tokenDecoded.Claims.First(c => c.Type == "Username").Value;
            string email = tokenDecoded.Claims.First(c => c.Type == "Email").Value;
            string id = tokenDecoded.Claims.First(c => c.Type == "Id").Value;
            UserContext? userContext = context.RequestServices.GetService<UserContext>();
            if (userContext != null)
            {
                userContext.Username = username;
                userContext.Email = email;
                userContext.Id = id;
            }
        }
        await _next(context);
    }
}

public class UserContext
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Id { get; set; }

}
