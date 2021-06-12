using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laborator3.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime AssignedDate { get; set; }

        public List<ToDoTask> ToDoTasks { get; set; }
    }
}
