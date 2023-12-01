using BackEnd23Harkka.Models;
using BackEnd23Harkka.Repositories;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace BackEnd23Harkka.Middleware
 
{
    
    public interface IUserAuthenticationService
    {
        Task<User> Authenticate(string username, string password);
        User CreateUserCredentials(User user);
    }
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IUserRepository _repository;
        public UserAuthenticationService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            User? user;

            user = await _repository.GetUserAsync(username);
            if (user == null)
            {
                return null;
            }

            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: user.Salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 258 / 8));

            if (hashedPassword != user.Password)
            {
                return null;
            }

            return user;

        }

        public User CreateUserCredentials(User user)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: user.Password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 258 / 8));



            User newUser = new User
            {
                userName = user.userName,
                firstName = user.firstName,
                lastName = user.lastName,
                Salt = salt,
                Password = hashedPassword,
                joinDate = DateTime.Now
            };
            return newUser;
        }
    }
}
