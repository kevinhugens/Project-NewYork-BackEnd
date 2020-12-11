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
    public class RankingController : ControllerBase
    {
        private readonly FoosballContext _context;

        public RankingController(FoosballContext context)
        {
            _context = context;
        }

        // GET: api/Ranking
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ranking>>> GetRanking()
        {
            return await _context.Ranking.Include(r => r.Team).ToListAsync();
        }

        // GET: api/Ranking/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Ranking>> GetRanking(int id)
        {
            var ranking = await _context.Ranking.Include(r => r.Team).FirstOrDefaultAsync(r => r.RankingID == id);

            if (ranking == null)
            {
                return NotFound();
            }

            return ranking;
        }

     
        [HttpGet("team/{id}")]
        public async Task<ActionResult<Ranking>> GetRankingByTeam(int id)
        {
            var ranking = await _context.Ranking.FirstOrDefaultAsync(r => r.TeamID == id);

            if (ranking == null)
            {
                return NotFound();
            }

            return ranking;
        }

        // PUT: api/Ranking/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRanking(int id, Ranking ranking)
        {
            if (id != ranking.RankingID)
            {
                return BadRequest();
            }

            _context.Entry(ranking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RankingExists(id))
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

        // POST: api/Ranking
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Ranking>> PostRanking(Ranking ranking)
        {
            _context.Ranking.Add(ranking);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRanking", new { id = ranking.RankingID }, ranking);
        }

        // DELETE: api/Ranking/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Ranking>> DeleteRanking(int id)
        {
            var ranking = await _context.Ranking.FindAsync(id);
            if (ranking == null)
            {
                return NotFound();
            }

            _context.Ranking.Remove(ranking);
            await _context.SaveChangesAsync();

            return ranking;
        }

        private bool RankingExists(int id)
        {
            return _context.Ranking.Any(e => e.RankingID == id);
        }
    }
}
