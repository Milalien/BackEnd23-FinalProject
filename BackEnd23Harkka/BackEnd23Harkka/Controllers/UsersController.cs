using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEnd23Harkka.Models;
using BackEnd23Harkka.Services;

namespace BackEnd23Harkka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MessageServiceContext _context;
        private readonly IUserService _service;

        public UsersController(MessageServiceContext context, IUserService service)
        {
            _context = context;
            _service = service;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            
            return Ok(await _service.GetUsersAsync());

        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(long id)
        {
            UserDTO? dto = await _service.GetUserAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }
        

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(long id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            if (await _service.UpdateUserAsync(user))
            {
                return NoContent();
            }
            return NotFound();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(User user)
        {
            UserDTO? newUser = await _service.NewUserAsync(user);
            
            if(newUser == null)
            {
                return Problem("Username not available. Choose a different username.");
            }

            return CreatedAtAction(nameof(GetUser),new {id=newUser.userName}, newUser);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            if( await _service.DeleteUserAsync(id))
            {
                return NoContent();
            }
            return NotFound();
        }

        private bool UserExists(long id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
