using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Laborator3.Data;
using Laborator3.Models;

namespace Laborator3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoTasksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ToDoTasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ToDoTasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoTask>>> GettoDoTasks()
        {
            return await _context.toDoTasks.ToListAsync();
        }

        // GET: api/ToDoTasks/5/comments
        [HttpGet("{id}/Comments")]
        public ActionResult<IEnumerable<Object>> GetCommentsForToDoTask(int id)
        {
            var query =  _context.Comments.Where(c => c.ToDoTask.Id == id).Include(c => c.ToDoTask).Select(c => new
            {
                ToDoTask = c.ToDoTask.Title,
                Comment = c.Content
            });

            return query.ToList();
        }

        // GET: api/ToDoTasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoTask>> GetToDoTask(int id)
        {
            var toDoTask = await _context.toDoTasks.FindAsync(id);

            if (toDoTask == null)
            {
                return NotFound();
            }

            return toDoTask;
        }

        // GET: api/ToDoTasks/fromDate/toDate
        [HttpGet]
        [Route ("filter/{FromDate}/{ToDate}")]
        public async Task<ActionResult<IEnumerable<ToDoTask>>> FilterToDoTaskOnDeadline(DateTime? FromDate, DateTime? ToDate)
        {
            if (FromDate == null || ToDate == null)
            {

                return await _context.toDoTasks.ToListAsync();

            }
            return await _context.toDoTasks.Where(p => p.DeadLine >= FromDate && p.DeadLine <= ToDate).ToListAsync();
        }

        // PUT: api/ToDoTasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDoTask(int id, ToDoTask toDoTask)
        {
            if (id != toDoTask.Id)
            {
                return BadRequest();
            }

            _context.Entry(toDoTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoTaskExists(id))
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

        // POST: api/ToDoTasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ToDoTask>> PostToDoTask(ToDoTask toDoTask)
        {
            _context.toDoTasks.Add(toDoTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetToDoTask", new { id = toDoTask.Id }, toDoTask);
        }

        //POST: api/ToDoTask/5/Comment
        [HttpPost("{id}/Comments")]
        public IActionResult PostCommentForToDoTask(int id, Comment comment)
        {
            comment.ToDoTask = _context.toDoTasks.Find(id);
            if (comment.ToDoTask == null) 
            {
                return NotFound();
            }
            _context.Comments.Add(comment);
            _context.SaveChanges();

            return Ok();
        }

        // DELETE: api/ToDoTasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoTask(int id)
        {
            var toDoTask = await _context.toDoTasks.FindAsync(id);
            if (toDoTask == null)
            {
                return NotFound();
            }

            _context.toDoTasks.Remove(toDoTask);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ToDoTaskExists(int id)
        {
            return _context.toDoTasks.Any(e => e.Id == id);
        }
    }
}
