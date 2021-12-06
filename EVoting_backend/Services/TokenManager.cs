using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using EVoting_backend.DB;
using EVoting_backend.DB.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVoting_backend.Services
{
    public class TokenManager
    {
        private readonly AppDbContext _appDbContext;

        public TokenManager(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public bool Validate(string token)
        {
            TokenValidationParameters @params = new TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Const.Secret)),
                ValidIssuer = Const.Issuer,
                ValidAudience = Const.Audiance,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ClockSkew = TimeSpan.Zero
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            try
            {
                handler.ValidateToken(token, @params, out SecurityToken outToken);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
