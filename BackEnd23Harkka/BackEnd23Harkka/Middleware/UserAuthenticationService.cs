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
        Task<bool> IsMyMessage(string username, long messageId);
    }
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IUserRepository _repository;
        private readonly IMessageRepository _messageRepository;
        public UserAuthenticationService(IUserRepository repository, IMessageRepository messageRepository)
        {
            _repository = repository;
            _messageRepository = messageRepository;
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
                joinDate = user.joinDate!=null? user.joinDate:DateTime.Now,
                lastLogin=DateTime.Now
            };
            return newUser;
        }

        public async Task<bool> IsMyMessage(string username, long messageId)
        {
            User? user = await _repository.GetUserAsync(username);
            if (user == null)
            {
                return false;
            }
            Message? message = await _messageRepository.GetMessageAsync(messageId);
            if (message == null) 
            { 
                return false;
            }
            if (message.Sender == user)
            {
                return true;
            }
            return false;
        }
    }
}
