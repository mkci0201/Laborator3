using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laborator3.ViewModels.Assignments
{
    public class AssignmentsForUserResponse
    {
        public int Id { get; set; }
        public ApplicationUserViewModel User { get; set; }
        
        public DateTime AssignedDate { get; set; }

        public List<ToDoTaskViewModel> ToDoTasks { get; set; }

    }
}
