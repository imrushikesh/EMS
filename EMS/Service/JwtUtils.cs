using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using EMS.Models;
using EMS.Service.Interface;
using EMS.util;



namespace EMS.Services
{
    public class JwtUtils : IJwtUtils
    {
        private readonly IConfiguration _configuration;

        public JwtUtils(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string JWT_SECRET
        {
            get
            {
                return _configuration.GetSection(ConstantHelper.JWT_SECRET).Value ?? string.Empty;
            }
        }

        public string JWT_ISSUER
        {
            get
            {
                return _configuration.GetSection(ConstantHelper.JWT_ISSUER).Value ?? string.Empty;
            }
        }

        public string GetJwtToken(TblUser user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            byte[] key = Encoding.UTF8.GetBytes(JWT_SECRET);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            ClaimsIdentity claimsIdentity = GetClaimsIdentity(user);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor();
            tokenDescriptor.Subject = claimsIdentity;
            tokenDescriptor.Issuer = JWT_ISSUER;
            tokenDescriptor.Expires = DateTime.UtcNow.AddMinutes(240);
            tokenDescriptor.SigningCredentials = signingCredentials;
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private ClaimsIdentity GetClaimsIdentity(TblUser user)
        {
            ClaimsIdentity identity = new();
            Claim[] claims = {
                new Claim("InternalUserId", user.UserId.ToString() ?? "0"),
                new Claim("UserName", user?.UserName?.ToString() ?? ""),
                new Claim("Role" , user?.Role?.ToString() ?? ""),
            };
            identity.AddClaims(claims);
            return identity;
        }
    }
}
