using Laborator3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laborator3.ViewModels
{
    public class ToDoTaskWithCommentsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

      
        public DateTime AddedAt { get; set; }
        
        public DateTime DeadLine { get; set; }

        
        public ImportanceEnum Importance { get; set; }
        
        public StatusEnum Status { get; set; }

        public DateTime ClosedAt { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}
