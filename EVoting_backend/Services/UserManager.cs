using EVoting_backend.DB;
using EVoting_backend.DB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EVoting_backend.Services
{
    public class UserManager
    {
        private readonly AppDbContext _appDbContext;

        public UserManager(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> AddUser(User user)
        {
            try
            {
                await _appDbContext.Users.AddAsync(user);
                await _appDbContext.SaveChangesAsync();
                return true;
            }catch(Exception)
            {
                return false;
            }
            
        }

        public async Task RemoveUser(User user)
        {
            _appDbContext.Users.Remove(user);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(p => p.Email == email);
        }

        public async Task<bool> SetToken(string email, string token)
        {
            try
            {
                User user  = await GetUserByEmail(email);
                user.Token = token;
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task ReleaseToken(string email)
        {
            User user = await GetUserByEmail(email);
            user.Token = null;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<bool> SetSecret(string email, string secret)
        {
            try
            {
                User user = await GetUserByEmail(email);
                user.Secret = secret;
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> SetIV(string email, string iv)
        {
            try
            {
                User user = await GetUserByEmail(email);
                user.iv = iv;
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
