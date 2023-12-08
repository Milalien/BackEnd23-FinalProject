using BackEnd23Harkka.Models;

namespace BackEnd23Harkka.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO?>> GetUsersAsync();
        Task<UserDTO?> GetUserAsync(long id);
        Task<UserDTO?> GetUserAsync(User user);

        Task<UserDTO?> NewUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(long id);
    }
}
