using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using HexaLambdaAuthorization.Models;

namespace HexaLambdaAuthorization.Services
{

    public class JWTAuthenticationManager
    {
        private readonly string _key;
        public JWTAuthenticationManager(string key)
        {
            _key = key;
        }
        public async Task<string> GenerateToken(Cliente cliente)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, cliente.nome)
                }),

                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return await Task.FromResult(tokenHandler.WriteToken(token));

        }

        public async Task<string> GenerateAnonymousTokenAsync()
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Anonymous, "true")
                }),

                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return await Task.FromResult(tokenHandler.WriteToken(token));
        }
    }
}
