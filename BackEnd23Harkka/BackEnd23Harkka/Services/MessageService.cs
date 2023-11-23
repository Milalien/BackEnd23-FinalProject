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

        public async Task<bool> DeleteMessageAsync(long id)
        {
            Message? message = await _repository.GetMessageAsync(id);
            if (message != null)
            {
                return await _repository.DeleteMessageAsync(message);
            } 
            return false;
        }

        public async Task<Message?> GetMessageAsync(long id)
        {
            return await _repository.GetMessageAsync(id);
        }

        public Task<IEnumerable<Message>> GetMessagesAsync()
        {
            return _repository.GetMessagesAsync();
        }

        public async Task<Message> NewMessageAsync(Message message)
        {
            return await _repository.NewMessageAsync(message);
        }

        public async Task<bool> UpdateMessageAsync(long id)
        {
            Message? message = await _repository.GetMessageAsync(id);
            if (message != null)
            {
                return await _repository.UpdateMessageAsync(message);
            }

            return false;
        }
    }
}
