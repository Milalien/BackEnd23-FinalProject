using BackEnd23Harkka.Middleware;
using BackEnd23Harkka.Models;
using BackEnd23Harkka.Repositories;
using NuGet.Protocol.Core.Types;

namespace BackEnd23Harkka.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IUserAuthenticationService _authenticationService;
        public UserService(UserRepository repository, IUserAuthenticationService authenticationService)
        {
            _repository = repository;
            _authenticationService = authenticationService;
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

        public async Task<User?> NewUserAsync(User user)
        {
            User? newUser = _authenticationService.CreateUserCredentials(user);
            if(newUser!=null)
            {
                return await _repository.NewUserAsync(newUser);

            }
            return null;
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
