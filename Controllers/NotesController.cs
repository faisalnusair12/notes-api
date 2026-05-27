using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notes_API.Data;
using Notes_API.DTOs;
using Notes_API.Models;

namespace Notes_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NotesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> Get()
        {
            return await _context.Notes.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> Get(int id) 
        {
            return await _context.Notes.FindAsync(id);
        }
        [HttpPost]
        public async Task<ActionResult<Note>> Create(CreateNoteDto dto) 
        {
            var note = new Note
            {
                Nots_Title = dto.Nots_Title,
                Nots_Description = dto.Nots_Description,
                IsImportant = dto.IsImportant,
                Created_Time = DateTime.Now
            };
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();
            return Ok(note);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Note>> Delete(int id) 
        {
            var delete = await _context.Notes.FindAsync(id);
            if (delete == null) 
            {
                return NotFound();
            }
            _context.Notes.Remove(delete);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Note>> Update(int id,UpdateNoteDto dto) 
        {
            var OldNote=_context.Notes.Find(id);
            if (OldNote == null) 
            {
                return NotFound();
            }
            OldNote.Nots_Title = dto.Nots_Title;
            OldNote.Nots_Description = dto.Nots_Description;
            OldNote.IsImportant = dto.IsImportant;
            await _context.SaveChangesAsync();
            return NoContent();

        }
    }
}