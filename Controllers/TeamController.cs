using FormulaOneApp.Data;
using FormulaOneApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FormulaOneApp.Controllers
{
    [Route("api/[controller]")] // api/team
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly DataContext _context;

        public TeamController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _context.Teams.ToArrayAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) 
        {
            var team = await _context.Teams.FirstOrDefaultAsync(t => t.Id == id);

            if (team == null) return BadRequest("Invalid Id");

            return Ok(team);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeam([FromBody] Team team)
        {
            _context.Teams.AddAsync(team);
            _context.SaveChanges();

            return CreatedAtAction("Get", team.Id, team);
        }

        [HttpPatch]
        public async Task<IActionResult> Update(int id, string country)
        {
            var team = await _context.Teams.FirstOrDefaultAsync(t => t.Id == (int)id);

            if (team == null) return BadRequest("Invlid Id");

            team.Country = country;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var team = await _context.Teams.FirstOrDefaultAsync(t => t.Id == id);

            if (team == null) return BadRequest("Invalid Id");

            _context.Teams.Remove(team);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
