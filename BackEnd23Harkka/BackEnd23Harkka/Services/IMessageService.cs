using BackEnd23Harkka.Models;

namespace BackEnd23Harkka.Services
{
    public interface IMessageService
    {
        Task<IEnumerable<Message>> GetMessagesAsync();
        Task<Message?> GetMessageAsync(long id);
        Task<Message> NewMessageAsync(Message message);
        Task<bool> UpdateMessageAsync(Message message);
        Task<bool> DeleteMessageAsync(long id);

    }
}
