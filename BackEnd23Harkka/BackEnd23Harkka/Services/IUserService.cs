using BackEnd23Harkka.Models;

namespace BackEnd23Harkka.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User?> GetUserAsync(long id);
        Task<User?> NewUserAsync(User user);
        Task<bool> UpdateUserAsync(long id);
        Task<bool> DeleteUserAsync(long id);
    }
}
