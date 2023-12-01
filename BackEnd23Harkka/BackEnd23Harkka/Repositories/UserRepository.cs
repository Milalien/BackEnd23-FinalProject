using BackEnd23Harkka.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd23Harkka.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MessageServiceContext _context;
        public UserRepository(MessageServiceContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteUserAsync(User user)
        {
            _context.Remove(user);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public async Task<User?> GetUserAsync(long id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetUserAsync(string username)
        {
            return _context.Users.Where(x=>x.userName == username).FirstOrDefault();
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();

        }

        public async Task<User> NewUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
