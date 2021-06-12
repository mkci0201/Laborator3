using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Laborator3.Models
{
    public class ToDoTask
    {
        public int Id { get; set; }


        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime AddedAt { get; set; }
    
        public DateTime DeadLine { get; set; }

        public ImportanceEnum Importance { get; set; }

        public StatusEnum Status { get; set; }

        public DateTime ClosedAt { get; set; }

        public List<Comment> Comments { get; set; }

        public List<Assignment> Assignments { get; set; }

    }
}
