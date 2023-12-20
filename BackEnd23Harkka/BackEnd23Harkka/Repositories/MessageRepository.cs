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

        public async Task<bool> DeleteMessageAsync(Message message)
        {
            _context.Remove(message);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<Message?> GetMessageAsync(long id)
        {
            
            return await _context.Messages.FindAsync(id);
        }

        public async Task<IEnumerable<Message>> GetMessagesAsync()
        {
            return await _context.Messages.Where(x => x.Recipient == null).Include(s => s.Sender).Include(s => s.Recipient).OrderByDescending(x => x.Id).Take(10).ToListAsync();
        }
        public async Task<IEnumerable<Message>> SearchMessagesAsync(string searchtext)
        {
            return await _context.Messages.Where(x => x.Recipient == null).Where(x=>x.Title.Contains(searchtext)||x.Body.Contains(searchtext)).OrderByDescending(x => x.Id).Take(10).ToListAsync();
        }
        public async Task<IEnumerable<Message>> GetReceivedMessagesAsync(User user)
        {
            return await _context.Messages.Where(x => x.Recipient == user).Include(s => s.Sender).Include(s => s.Recipient).ToListAsync();
        }

        public async Task<IEnumerable<Message>> GetSentMessagesAsync(User user)
        {
            return await _context.Messages.Where(x => x.Sender == user).Include(s => s.Sender).Include(s => s.Recipient).ToListAsync();
        }

        public async Task<Message> NewMessageAsync(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return message;
        }

       

        public async Task<bool> UpdateMessageAsync(Message message)
        {
            _context.Entry(message).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            } catch
            {
                return false;
            }
            return true;
        }
    }
}
