using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaChamados.Api.Data;
using SistemaChamados.Api.Models;

namespace SistemaChamados.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public TicketsController(ApplicationDbContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _db.Tickets
                .Include(t => t.Comments)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
            return Ok(list);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var ticket = await _db.Tickets.Include(t => t.Comments).FirstOrDefaultAsync(t => t.TicketId == id);
            if (ticket == null) return NotFound();
            return Ok(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Ticket input)
        {
            input.TicketId = Guid.NewGuid();
            input.CreatedAt = DateTime.UtcNow;
            _db.Tickets.Add(input);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = input.TicketId }, input);
        }

        [HttpPost("{id:guid}/comments")]
        public async Task<IActionResult> AddComment(Guid id, [FromBody] TicketComment comment)
        {
            var ticket = await _db.Tickets.FindAsync(id);
            if (ticket == null) return NotFound();

            comment.TicketCommentId = Guid.NewGuid();
            comment.TicketId = id;
            comment.CreatedAt = DateTime.UtcNow;
            _db.TicketComments.Add(comment);
            await _db.SaveChangesAsync();
            return Ok(comment);
        }

        [HttpPost("{id:guid}/suggest")]
        public async Task<IActionResult> Suggest(Guid id)
        {
            var suggestion = "Sugestão automática: verifique cabo de rede e reinicie o equipamento.";
            var ai = new AiHistory
            {
                AiHistoryId = Guid.NewGuid(),
                TicketId = id,
                Prompt = "Gerar sugestão para problema técnico (MVP)",
                Response = suggestion,
                ModelName = "mvp-simulado",
                CreatedAt = DateTime.UtcNow
            };
            _db.AiHistory.Add(ai);
            await _db.SaveChangesAsync();

            return Ok(new { suggestion });
        }
    }
}
