using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Laborator3.Data;
using Laborator3.Models;
using Laborator3.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace Laborator3.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoTasksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ToDoTasksController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        ///<summary>
        ///Return all ToDoTask elements from Database
        ///</summary>
        // GET: api/ToDoTasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoTaskViewModel>>> GettoDoTasks()
        {
           var toDoTasks = await _context.toDoTasks.ToListAsync();
               
           return _mapper.Map<List<ToDoTask>, List<ToDoTaskViewModel>>(toDoTasks);
        }

        ///<summary>
        ///Return all existing comments from one ToDotask Object
        ///</summary>
        // GET: api/ToDoTasks/5/comments
        [HttpGet("{id}/Comments")]
        public ActionResult<ToDoTaskWithCommentsViewModel> GetCommentsForToDoTask(int id)
        {
            var query = _context.toDoTasks.Where(t => t.Id == id).Include(t => t.Comments).Select(t => _mapper.Map<ToDoTaskWithCommentsViewModel>(t));

            if (query.ToList().Count > 0)
            {

                return query.ToList()[0];
            }
            else

                return NotFound();
        }

        ///<summary>
        ///Return one ToDotask Object
        ///</summary>
        // GET: api/ToDoTasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoTaskViewModel>> GetToDoTask(int id)
        {
            var toDoTask = await _context.toDoTasks.FindAsync(id);

            if (toDoTask == null)
            {
                return NotFound();
            }
                     
            return _mapper.Map<ToDoTask, ToDoTaskViewModel>(toDoTask);
        }

        ///<summary>
        ///Filter ToDotask Objects using a range of Dates
        ///</summary>
        // GET: api/ToDoTasks/fromDate/toDate
        [HttpGet]
        [Route("filter/{FromDate}/{ToDate}")]
        public async Task<ActionResult<IEnumerable<ToDoTaskViewModel>>> FilterToDoTaskOnDeadline(DateTime? FromDate, DateTime? ToDate)
        {
            if (FromDate == null || ToDate == null)
            {

                return _mapper.Map<List<ToDoTask>, List<ToDoTaskViewModel>>(await _context.toDoTasks.ToListAsync());

            }
            return _mapper.Map<List<ToDoTask>, List<ToDoTaskViewModel>>(await _context.toDoTasks.Where(p => p.DeadLine >= FromDate && p.DeadLine <= ToDate).ToListAsync());
        }

        ///<summary>
        ///Update one ToDotask Object
        ///</summary>
        // PUT: api/ToDoTasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(AuthenticationSchemes = "Identity.Application,Bearer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDoTask(int id, ToDoTaskViewModel toDoTaskViewModel)
        {
            if (id != toDoTaskViewModel.Id)
            {
                return BadRequest();
            }

            var toDoTask = _mapper.Map<ToDoTaskViewModel, ToDoTask>(toDoTaskViewModel);

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

            return Ok();
        }

        ///<summary>
        ///Add a new ToDotask Object
        ///</summary>
        // POST: api/ToDoTasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[Authorize(AuthenticationSchemes = "Identity.Application,Bearer")]
        [HttpPost]
        public async Task<ActionResult<ToDoTaskViewModel>> PostToDoTask(ToDoTaskViewModel toDoTaskRequest)
        {
            ToDoTask toDoTask = _mapper.Map<ToDoTask>(toDoTaskRequest);
            _context.toDoTasks.Add(toDoTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetToDoTask", new { id = toDoTask.Id }, toDoTask);
        }

        ///<summary>
        ///Add a new Comment to a toDoTask Object
        ///</summary>
        //POST: api/ToDoTask/5/Comment
        [Authorize(AuthenticationSchemes = "Identity.Application,Bearer")]
        [HttpPost("{id}/Comment")]
        public async Task<IActionResult> PostCommentForToDoTask(int id, CommentViewModel commentRequest)
        {
            Comment comment = _mapper.Map<CommentViewModel, Comment>(commentRequest);

            comment.ToDoTask = _context.toDoTasks.Find(id);
            comment.ToDoTaskId = id;
            if (comment.ToDoTask == null) 
            {
                return NotFound();
            }

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return Ok();
        }

        ///<summary>
        ///Delete a toDoTask Object
        ///</summary>
        // DELETE: api/ToDoTasks/5
        [Authorize(AuthenticationSchemes = "Identity.Application,Bearer")]
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

            return Ok();
        }

        ///<summary>
        ///Delete a Comment from To Do Task Object
        ///</summary>
        // DELETE: api/ToDoTask/Comment/5
        [Authorize(AuthenticationSchemes = "Identity.Application,Bearer")]
        [HttpDelete("Comment/{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {

            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return Ok();
        }


        private bool ToDoTaskExists(int id)
        {
            return _context.toDoTasks.Any(e => e.Id == id);
        }
    }
}
