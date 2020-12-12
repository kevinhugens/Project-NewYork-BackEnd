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

namespace NewYork_BackEnd.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserGameController : ControllerBase
    {
        private readonly FoosballContext _context;

        public UserGameController(FoosballContext context)
        {
            _context = context;
        }

        // GET: api/UserGame
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserGame>>> GetUserGame()
        {
            return await _context.UserGame.Include(u => u.Player).ToListAsync();
        }

        // GET: api/UserGame/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserGame>> GetUserGame(int id)
        {
            var userGame = await _context.UserGame.Include(u => u.Player).FirstOrDefaultAsync(u => u.UserGameID == id);

            if (userGame == null)
            {
                return NotFound();
            }

            return userGame;
        }

        [HttpGet("game/{gameid}")]
        public async Task<ActionResult<IEnumerable<UserGame>>> GetUserGamesByGame(int gameid)
        {
            return await _context.UserGame.Include(u => u.Player).Where(u => u.GameID == gameid).ToListAsync();
        }

        // PUT: api/UserGame/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserGame(int id, UserGame userGame)
        {
            if (id != userGame.UserGameID)
            {
                return BadRequest();
            }

            _context.Entry(userGame).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserGameExists(id))
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

        // POST: api/UserGame
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<UserGame>> PostUserGame(UserGame userGame)
        {
            _context.UserGame.Add(userGame);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserGame", new { id = userGame.UserGameID }, userGame);
        }

        // DELETE: api/UserGame/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserGame>> DeleteUserGame(int id)
        {
            var userGame = await _context.UserGame.FindAsync(id);
            if (userGame == null)
            {
                return NotFound();
            }

            _context.UserGame.Remove(userGame);
            await _context.SaveChangesAsync();

            return userGame;
        }

        // DELETE: api/UserGame/5
        [Authorize]
        [HttpDelete("/deletegameanduser/{gameID}/{userID}")]
        public async Task<ActionResult<UserGame>> DeleteUserGameByGameAndUser(int gameID, int userID)
        {
            var userGame = await _context.UserGame.Where(u => u.GameID == gameID && u.UserID == userID).FirstAsync();
            if (userGame == null)
            {
                return NotFound();
            }

            _context.UserGame.Remove(userGame);
            await _context.SaveChangesAsync();

            return userGame;
        }

        private bool UserGameExists(int id)
        {
            return _context.UserGame.Any(e => e.UserGameID == id);
        }
    }
}
