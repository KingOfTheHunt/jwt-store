using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtStore.Core.Contexts;
using JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate;
using Microsoft.IdentityModel.Tokens;

namespace JwtStore.Api.Extensions;

public static class JwtExtension
{
    public static string Generate(ResponseData user)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Configuration.Secrets.JwtKey);
        var crendentials = new SigningCredentials(new SymmetricSecurityKey(key), 
            SecurityAlgorithms.HmacSha256Signature);
        
        var tokenDescriptior = new SecurityTokenDescriptor 
        {
            Subject = GenerateClaims(user),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = crendentials
        };

        var token = handler.CreateToken(tokenDescriptior);

        return handler.WriteToken(token);
    }   

    private static ClaimsIdentity GenerateClaims(ResponseData user)
    {
        var claimsIdentity = new ClaimsIdentity();

        claimsIdentity.AddClaim(new Claim("Id", user.Id));
        claimsIdentity.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));
        claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.Email));
        foreach (var role in user.Roles)
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));

        return claimsIdentity;
    } 
}