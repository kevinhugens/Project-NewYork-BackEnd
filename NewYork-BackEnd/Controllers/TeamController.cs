using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using NewYork_BackEnd.Data;
using NewYork_BackEnd.Models;

namespace NewYork_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly FoosballContext _context;

        public TeamController(FoosballContext context)
        {
            _context = context;
        }

        // GET: api/Team
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeam()
        {
            return await _context.Team.Include(t => t.TeamMembers).Include(t => t.Captain).ToListAsync();
        }

        // GET: api/Team/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(int id)
        {
            var team = await _context.Team.Include(c => c.Captain).SingleOrDefaultAsync(i => i.TeamID == id);

            if (team == null)
            {
                return NotFound();
            }

            return team;
        }

        // PUT: api/Team/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam(int id, Team team)
        {
            if (id != team.TeamID)
            {
                return BadRequest();
            }

            _context.Entry(team).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
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

        // POST: api/Team
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Team>> PostTeam(Team team)
        {
            _context.Team.Add(team);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeam", new { id = team.TeamID }, team);
        }

        // DELETE: api/Team/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Team>> DeleteTeam(int id)
        {
            var team = await _context.Team.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            _context.Team.Remove(team);
            await _context.SaveChangesAsync();

            return team;
        }

        private bool TeamExists(int id)
        {
            return _context.Team.Any(e => e.TeamID == id);
        }

        [HttpPost("uploadimage")]
        public async Task<IActionResult> UploadTeamImage(IFormFile file)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(AppConfiguration.GetConfiguration("AccessKey"));
            CloudBlobClient BlobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = BlobClient.GetContainerReference("newyork-thebigapp");
            await container.CreateIfNotExistsAsync();
            CloudBlockBlob blob = container.GetBlockBlobReference(file.FileName);
            await blob.UploadFromStreamAsync(file.OpenReadStream());
            return Ok("File uploaded");
        }
    }
}
