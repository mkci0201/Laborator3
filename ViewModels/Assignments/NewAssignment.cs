using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laborator3.ViewModels.Assignments
{
    public class NewAssignment
    {
        public List<int> AssignedToDoTaskIds { get; set; }
        public DateTime? AssignedDate { get; set; }
    }
}
