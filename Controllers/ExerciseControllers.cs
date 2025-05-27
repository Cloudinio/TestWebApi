using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Contexts;
using App.Models;
using App.DTOs;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExercisesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExercisesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Exercises
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExerciseDto>>> GetAll()
        {
            var items = await _context.Exercises
                .Include(e => e.ExerciseType)
                .Select(e => new ExerciseDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    CreatedAt = e.CreatedAt,
                    Type = new ExerciseTypeDto
                    {
                        Id = e.ExerciseType.Id,
                        Name = e.ExerciseType.Name,
                        CreatedAt = e.ExerciseType.CreatedAt
                    }
                })
                .ToListAsync();

            return Ok(items);
        }

        // GET: api/Exercises/{name}
        [HttpGet("{name}")]
        public async Task<ActionResult<ExerciseDto>> GetByName(string name)
        {
            var e = await _context.Exercises
                .Include(x => x.ExerciseType)
                .FirstOrDefaultAsync(x => x.Name == name);
            if (e == null)
                return NotFound();

            var dto = new ExerciseDto
            {
                Id = e.Id,
                Name = e.Name,
                CreatedAt = e.CreatedAt,
                Type = new ExerciseTypeDto
                {
                    Id = e.ExerciseType.Id,
                    Name = e.ExerciseType.Name,
                    CreatedAt = e.ExerciseType.CreatedAt
                }
            };
            return Ok(dto);
        }

        // POST: api/Exercises
        [HttpPost]
        public async Task<ActionResult<ExerciseDto>> Create(CreateExerciseDto input)
        {
            var entity = new Exercise
            {
                Name = input.Name,
                ExerciseTypeId = input.ExerciseTypeId,
                CreatedAt = DateTime.UtcNow
            };
            _context.Exercises.Add(entity);
            await _context.SaveChangesAsync();

            // загружаем тип для ответа
            await _context.Entry(entity).Reference(x => x.ExerciseType).LoadAsync();
            var dto = new ExerciseDto
            {
                Id = entity.Id,
                Name = entity.Name,
                CreatedAt = entity.CreatedAt,
                Type = new ExerciseTypeDto
                {
                    Id = entity.ExerciseType.Id,
                    Name = entity.ExerciseType.Name,
                    CreatedAt = entity.ExerciseType.CreatedAt
                }
            };
            return CreatedAtAction(nameof(GetByName), new { name = dto.Name }, dto);
        }

        // PUT: api/Exercises/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, UpdateExerciseDto input)
        {
            var entity = await _context.Exercises.FindAsync(id);
            if (entity == null)
                return NotFound();

            entity.Name = input.Name;
            entity.ExerciseTypeId = input.ExerciseTypeId;

            _context.Exercises.Update(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Exercises/{name}
        [HttpDelete("{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            var entity = await _context.Exercises.FirstOrDefaultAsync(x => x.Name == name);
            if (entity == null)
                return NotFound();

            _context.Exercises.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}