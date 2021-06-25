using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Laborator3.Data;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Laborator3.Models;
using Laborator3.ViewModels.Assignments;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

namespace Laborator3.Controllers
{
    [Authorize(AuthenticationSchemes = "Identity.Application,Bearer")]
    [ApiController]
    [Route("api/[controller]")]
    public class AssignmentsController : ControllerBase

    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AssignmentsController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public AssignmentsController(ApplicationDbContext context, ILogger<AssignmentsController> logger, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }
        ///<summary>
        ///Add new Assignment Objects to the logged user
        ///</summary>
        // POST: api/Assignments
        [HttpPost]
        public async Task<ActionResult> AssignTask(NewAssignment newTaskAssignment)
        {
            var user = await _userManager.FindByNameAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

            List<ToDoTask> assignedTasks = new List<ToDoTask>();
            newTaskAssignment.AssignedToDoTaskIds.ForEach(pid =>
            {
                var taskWithId = _context.toDoTasks.Find(pid);
                if (taskWithId != null)
                {
                    assignedTasks.Add(taskWithId);
                }
            });

            if (assignedTasks.Count == 0)
            {
                return BadRequest();
            }

            var assignment = new Assignment
            {
                User = user,
                AssignedDate = newTaskAssignment.AssignedDate.GetValueOrDefault(),
                ToDoTasks = assignedTasks
            };

            _context.Assignments.Add(assignment);
            await _context.SaveChangesAsync();
            return Ok();
        }


        ///<summary>
        ///Return all ToDo Task Objects and Assignment Objects for the logged user
        ///</summary>
        //GET: api/Assignments
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var result = _context.Assignments.Where(a => a.User.Id == user.Id).Include(a => a.ToDoTasks).Select(a => _mapper.Map<AssignmentsForUserResponse>(a));

            return Ok(result.ToList());
        }

        ///<summary>
        ///Update one Assignment Object
        ///</summary>
        // PUT: api/Assignments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAssignment(int id, NewAssignment newAssignment)
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);
           
            if (!_context.Assignments.Any(a => (a.Id == id && a.User.Id == user.Id)))
            {    
                return NotFound();
            }
            List<ToDoTask> assignedTasks = new List<ToDoTask>();
            newAssignment.AssignedToDoTaskIds.ForEach(pid =>
            {
                var taskWithId = _context.toDoTasks.Find(pid);
                if (taskWithId != null)
                {
                    assignedTasks.Add(taskWithId);
                }
            });

            if (assignedTasks.Count == 0)
            {
                return BadRequest();
            }

            var assignment = _mapper.Map<NewAssignment, Assignment>(newAssignment);
            assignment.Id = id;
            assignment.ToDoTasks = assignedTasks;
            assignment.User = user;

            _context.Entry(assignment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Assignments.Any(e => e.Id == id))
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
        ///Delete an Assignment Object
        ///</summary>
        // DELETE: api/Assignment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssignment(int id)
        {

            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var assignment = await _context.Assignments.FindAsync(id);

            if (assignment == null || assignment.User.Id != user.Id)
            {
                return NotFound();
            }

            _context.Assignments.Remove(assignment);
            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}
