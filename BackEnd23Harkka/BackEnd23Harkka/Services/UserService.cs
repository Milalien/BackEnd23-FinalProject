using BackEnd23Harkka.Models;
using BackEnd23Harkka.Repositories;
using NuGet.Protocol.Core.Types;

namespace BackEnd23Harkka.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _repository;
        public UserService(UserRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> DeleteUserAsync(long id)
        {
            User? user = await _repository.GetUserAsync(id);
            if(user != null)
            {
                return await _repository.DeleteUserAsync(user);
            }
                return false;
            
        }

        public async Task<User?> GetUserAsync(long id)
        {
            return await _repository.GetUserAsync(id);
        }

        public Task<IEnumerable<User>> GetUsersAsync()
        {
            return _repository.GetUsersAsync();
        }

        public Task<User> NewUserAsync(User user)
        {
            return _repository.NewUserAsync(user);
        }

        public async Task<bool> UpdateUserAsync(long id)
        {
            User? user = await _repository.GetUserAsync(id);
            if (user != null)
            return await _repository.UpdateUserAsync(user);

            return false;
        }
    }
}
