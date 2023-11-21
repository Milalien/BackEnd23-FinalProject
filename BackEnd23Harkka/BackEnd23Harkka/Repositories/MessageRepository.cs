using BackEnd23Harkka.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace BackEnd23Harkka.Repositories
{
    public class MessageRepository : IMessageRepository
    {

        private readonly MessageServiceContext _context;
        public MessageRepository(MessageServiceContext context)
        {
            _context = context;
        }

        public Task<bool> DeleteMessageAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<Message?> GetMessageAsync(long id)
        {
            return await _context.Messages.FindAsync(id);
        }

        public async Task<IEnumerable<Message>> GetMessagesAsync()
        {
            return await _context.Messages.ToListAsync();
        }

        public async Task<Message> NewMessageAsync(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
        }

        public Task<bool> UpdateMessageAsync(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
