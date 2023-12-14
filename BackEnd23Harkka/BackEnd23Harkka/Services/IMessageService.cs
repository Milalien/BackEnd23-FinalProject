using BackEnd23Harkka.Models;

namespace BackEnd23Harkka.Services
{
    public interface IMessageService
    {
        Task<IEnumerable<MessageDTO?>> GetMessagesAsync();
        Task<IEnumerable<MessageDTO?>> GetSentMessagesAsync(string username);
        Task<IEnumerable<MessageDTO?>> GetReceivedMessagesAsync(string username);
        Task<IEnumerable<MessageDTO?>> SearchMessagesAsync(string searchtext);
        Task<MessageDTO?> GetMessageAsync(long id);
        Task<MessageDTO> NewMessageAsync(MessageDTO message);
        Task<bool> UpdateMessageAsync(MessageDTO message);
        Task<bool> DeleteMessageAsync(long id);
        
    }
}
