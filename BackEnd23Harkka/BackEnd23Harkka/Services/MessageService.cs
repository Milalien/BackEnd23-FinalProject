using BackEnd23Harkka.Models;
using BackEnd23Harkka.Repositories;

namespace BackEnd23Harkka.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _repository;
        public MessageService(IMessageRepository repository) {
            _repository= repository;
        }

        public Task<bool> DeleteMessageAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Message?> GetMessageAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Message>> GetMessagesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Message> NewMessageAsync(Message message)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateMessageAsync(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
