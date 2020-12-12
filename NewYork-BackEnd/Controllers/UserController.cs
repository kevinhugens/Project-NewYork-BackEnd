using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewYork_BackEnd.Data;
using NewYork_BackEnd.Models;
using NewYork_BackEnd.Services;

namespace NewYork_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly FoosballContext _context;
        private IUserService _userService;

        public UserController(IUserService userService, FoosballContext context)
        {
            _userService = userService;
            _context = context;
        }

        // GET: api/User
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/User/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpGet("team")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersWithoutTeam()
        {
            return await _context.Users.Where(x => x.TeamID == null).ToListAsync();
        }

        [HttpGet("team/{teamid}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersByTeamID(int teamid)
        {
            return await _context.Users.Where(x=>x.TeamID == teamid).ToListAsync();
        }


        // PUT: api/User/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            User userindb = await _context.Users.FindAsync(user.UserID);
            if (id != user.UserID)
            {
                return BadRequest();
            }
            if(user.Password == null)
            {
                userindb.Password = "newpassword";
            }
            if (user.Password != userindb.Password)
            {
                userindb.HashSalt = Hashing.getSalt();
                userindb.Password = Hashing.getHash(user.Password, userindb.HashSalt);
            }
            userindb.FirstName = user.FirstName;
            userindb.LastName = user.LastName;
            userindb.Email = user.Email;
            userindb.DateOfBirth = user.DateOfBirth;

            _context.Entry(userindb).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        [HttpPut("updateprofilepicture/{filename}")]
        public async Task<IActionResult> PutUserProfilePicture(string filename, User user)
        {
            User userindb = await _context.Users.FindAsync(user.UserID);
            userindb.Photo = filename;

            _context.Entry(userindb).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (user == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // POST: api/User
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            user.Role = "user";
            user.HashSalt = Hashing.getSalt();
            user.Password = Hashing.getHash(user.Password, user.HashSalt);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserID }, user);
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            var user = _userService.Authenticate(userParam.Email, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Email or password is incorrect" });

            return Ok(user);
        }

        // DELETE: api/User/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        [Authorize]
        [HttpDelete("team/{userid}")]
        public async Task<ActionResult<User>> DeleteUserFromTeam(int userid)
        {
            var user = await _context.Users.FindAsync(userid);
            if (user == null)
            {
                return NotFound();
            }
            user.TeamID = null;
            _context.Entry(user).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(userid))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return user;
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserID == id);
        }
    }
}
