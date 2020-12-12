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
    public class GameController : ControllerBase
    {
        private readonly FoosballContext _context;

        public GameController(FoosballContext context)
        {
            _context = context;
        }

        // GET: api/Game
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGame()
        {
             return await _context.Game.Include(t => t.Table).Include(t => t.Team1).Include(t => t.Team2).Include(t=>t.UserGames).ToListAsync();
        }

        [HttpGet("gamesbyteam/{id}")]
        public async Task<ActionResult<IEnumerable<Game>>> GetGamesFromTeam(int id)
        {
            return await _context.Game.Include(g => g.Team1).Include(g => g.Team2).Where(g => g.Team1ID == id || g.Team2ID == id).ToListAsync();
        }

        [HttpGet("live")]
        public async Task<ActionResult<IEnumerable<Game>>> GetLiveGames()
        {
            return await _context.Game.Include(g => g.Team1).Include(g => g.Team2).Where(g => g.GameStatusID == 2).ToListAsync();
        }

        [HttpGet("nextgamesbyteam/{id}")]
        public async Task<ActionResult<IEnumerable<Game>>> GetNextGamesFromTeam(int id)
        {
            DateTime date = DateTime.Today;
            return await _context.Game.Include(t => t.Table).Include(g => g.Team1).Include(g => g.Team2).Where(g => g.GameStatusID == 1 && g.Date > date).Where(g => g.Team1ID == id || g.Team2ID == id).ToListAsync();
        }

        // GET: api/Game/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(int id)
        {
            var game = await _context.Game.Include(t => t.Table).Include(t => t.Team1).Include(t => t.Team2).Include(t => t.UserGames).SingleOrDefaultAsync(i => i.GameID == id);

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }
        [Authorize]
        [HttpGet("competition/next")]
        public async Task<ActionResult<Game>> GetNextCompetitionGame()
        {
            DateTime date = DateTime.Today;
            var games = await _context.Game.Include(g => g.Team1).Include(g => g.Team2).Where(g => g.CompetitionID != null && g.Date > date).Where(g => g.GameStatusID == 1 || g.GameStatusID == 2).ToListAsync();
            var orderedgames = games.OrderBy(g => g.Date);
            var game = orderedgames.First();

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }
        [Authorize]
        [HttpGet("friendly/next")]
        public async Task<ActionResult<Game>> GetNextFriendlyGame()
        {
            DateTime date = DateTime.Today;
            var games = await _context.Game.Include(g => g.Team1).Include(g => g.Team2).Where(g => g.CompetitionID == null && g.Date > date ).Where(g => g.GameStatusID == 1 || g.GameStatusID == 2).ToListAsync();
            var orderedgames = games.OrderBy(g => g.Date);
            var game = orderedgames.First();

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }
        [Authorize]
        [HttpGet("competition/next/{id}")]
        public async Task<ActionResult<Game>> GetNextCompetitionGame(int id)
        {
            DateTime date = DateTime.Today;
            var games = await _context.Game.Include(g => g.Team1).Include(g => g.Team2).Where(g => g.CompetitionID != null && g.Date > date).Where(g => g.GameStatusID == 1 || g.GameStatusID == 2).Where(g=> g.Team1ID == id || g.Team2ID == id).ToListAsync();
            var orderedgames = games.OrderBy(g => g.Date);
            var game = orderedgames.First();

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }
        [Authorize]
        [HttpGet("friendly/next/{id}")]
        public async Task<ActionResult<Game>> GetNextFriendlyGame(int id)
        {
            DateTime date = DateTime.Today;
            var games = await _context.Game.Include(g => g.Team1).Include(g => g.Team2).Where(g => g.CompetitionID == null && g.Date > date && g.GameStatusID == 1).Where(g => g.Team1ID == id || g.Team2ID == id).ToListAsync();
            var orderedgames = games.OrderBy(g => g.Date);
            var game = orderedgames.First();

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }
        [Authorize]
        [HttpGet("friendly/next/user/{teamID}")]
        public async Task<ActionResult<Game>> GetNextFriendlyGameUser(int teamID)
        {
            DateTime date = DateTime.Today;
            var games = await _context.Game.Where(g => g.CompetitionID == null && g.Date > date && (g.Team1ID == teamID || g.Team2ID == teamID)).ToListAsync();
            var orderedgames = games.OrderBy(g => g.Date);
            var game = orderedgames.First();

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }
        [Authorize]
        [HttpGet("friendly/planned/team/{teamID}")]
        public async Task<ActionResult<IEnumerable<Game>>> GetPlannedFriendlyTeamGames(int teamID)
        {
            DateTime date = DateTime.Today;
            var games = await _context.Game.Where(g => g.CompetitionID == null && g.Date > date && (g.Team1ID == teamID || g.Team2ID == teamID)).OrderBy(g => g.Date).ToListAsync();

            if (games == null)
            {
                return NotFound();
            }

            return games;
        }
        [Authorize]
        [HttpGet("friendly/played/team/{teamID}")]
        public async Task<ActionResult<IEnumerable<Game>>> GetPlayedFriendlyTeamGames(int teamID)
        {
            DateTime date = DateTime.Today;
            var games = await _context.Game.Where(g => g.CompetitionID == null && g.Date < date && (g.Team1ID == teamID || g.Team2ID == teamID)).OrderBy(g => g.Date).ToListAsync();

            if (games == null)
            {
                return NotFound();
            }

            return games;
        }

        // PUT: api/Game/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, Game game)
        {
            if (id != game.GameID)
            {
                return BadRequest();
            }

            _context.Entry(game).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
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

        // POST: api/Game
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(Game game)
        {
            _context.Game.Add(game);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGame", new { id = game.GameID }, game);
        }

        // DELETE: api/Game/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Game>> DeleteGame(int id)
        {
            var game = await _context.Game.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            _context.Game.Remove(game);
            await _context.SaveChangesAsync();

            return game;
        }

        private bool GameExists(int id)
        {
            return _context.Game.Any(e => e.GameID == id);
        }
    }
}
