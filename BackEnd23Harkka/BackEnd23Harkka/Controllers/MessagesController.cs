using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEnd23Harkka.Models;
using BackEnd23Harkka.Services;
using Microsoft.AspNetCore.Authorization;

namespace BackEnd23Harkka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService service)
        {
            _messageService = service;
            
        }

        // GET: api/Messages

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetMessages()
        {
            return Ok(await _messageService.GetMessagesAsync());
        }

        // GET: api/Messages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MessageDTO>> GetMessage(long id)
        {
            MessageDTO? message = await _messageService.GetMessageAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            return message;
        }

        // PUT: api/Messages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessage(int id, MessageDTO message)
        {
            if (id != message.Id)
            {
                return BadRequest();
            }

            bool result = await _messageService.UpdateMessageAsync(message);

            if (!result)
            {
                return NotFound(message);
            }

            return NoContent();

            
        }

        // POST: api/Messages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Message>> PostMessage(MessageDTO message)
        {
            MessageDTO newMessage = await _messageService.NewMessageAsync(message);
            if (newMessage == null)
            {
                return Problem();
            }
            

            return CreatedAtAction("GetMessage", new { id = message.Id }, message);
        }

        // DELETE: api/Messages/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            if(await _messageService.DeleteMessageAsync(id))
            {
                return Ok();
            }
            return NotFound();
        }

       
    }
}
