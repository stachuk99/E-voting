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
                _appDbContext.Users.Add(user);
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
    }
}
