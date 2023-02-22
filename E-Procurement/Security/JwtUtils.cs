using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using E_Procurement.Entities;
using Microsoft.IdentityModel.Tokens;

namespace E_Procurement.Security;

public class JwtUtils : IJwtUtils
{
    // Generate configuration has been made
    private readonly IConfiguration _configuration;

    public JwtUtils(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]);
        
        // Content of payload
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Audience = _configuration["JwtSettings:Audience"],
            Expires = DateTime.Now.AddMinutes(int.Parse(_configuration["JwtSettings:ExpiresInMinutes"])),
            Issuer = _configuration["JwtSettings:Issuer"],
            IssuedAt = DateTime.Now,
            Subject = new ClaimsIdentity(new List<Claim>
            {
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Role, user.ERole.ToString()),
                new(ClaimTypes.NameIdentifier, user.Id.ToString())
            }),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}