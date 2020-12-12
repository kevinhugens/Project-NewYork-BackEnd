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
    public class GameStatusController : ControllerBase
    {
        private readonly FoosballContext _context;

        public GameStatusController(FoosballContext context)
        {
            _context = context;
        }

        // GET: api/GameStatus
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameStatus>>> GetGameStatus()
        {
            return await _context.GameStatus.ToListAsync();
        }

        // GET: api/GameStatus/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<GameStatus>> GetGameStatus(int id)
        {
            var gameStatus = await _context.GameStatus.FindAsync(id);

            if (gameStatus == null)
            {
                return NotFound();
            }

            return gameStatus;
        }

        // PUT: api/GameStatus/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGameStatus(int id, GameStatus gameStatus)
        {
            if (id != gameStatus.GameStatusID)
            {
                return BadRequest();
            }

            _context.Entry(gameStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameStatusExists(id))
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

        // POST: api/GameStatus
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<GameStatus>> PostGameStatus(GameStatus gameStatus)
        {
            _context.GameStatus.Add(gameStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGameStatus", new { id = gameStatus.GameStatusID }, gameStatus);
        }

        // DELETE: api/GameStatus/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<GameStatus>> DeleteGameStatus(int id)
        {
            var gameStatus = await _context.GameStatus.FindAsync(id);
            if (gameStatus == null)
            {
                return NotFound();
            }

            _context.GameStatus.Remove(gameStatus);
            await _context.SaveChangesAsync();

            return gameStatus;
        }

        private bool GameStatusExists(int id)
        {
            return _context.GameStatus.Any(e => e.GameStatusID == id);
        }
    }
}
