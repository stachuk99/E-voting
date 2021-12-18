using Microsoft.IdentityModel.Tokens;
using EVoting_backend.DB.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EVoting_backend.Services
{
    public class TokenGenerator
    {
        public string GenerateAccessToken(User user)
        {
            DateTime expiration = DateTime.Now.AddMinutes(15);
            var claims = new[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };
            return GenerateToken(Const.Secret, Const.Issuer, Const.Audiance, expiration, claims);
        }
        public string GenerateRefreshToken()
        {
            //TODO store in config
            DateTime expiration = DateTime.Now.AddMonths(1);
            return GenerateToken(Const.Secret, Const.Issuer, Const.Audiance, expiration);
        }
        private string GenerateToken(string secret, string issuer, string aud, DateTime expiration, IEnumerable<Claim> claims = null)
        {
            var secretBytes = Encoding.UTF8.GetBytes(secret);
            var key = new SymmetricSecurityKey(secretBytes);
            var algorithm = SecurityAlgorithms.HmacSha256;
            var signingCredentials = new SigningCredentials(key, algorithm);
            var token = new JwtSecurityToken(
                issuer,
                aud,
                claims,
                notBefore: DateTime.Now,
                expires: expiration,
                signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
