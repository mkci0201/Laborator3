using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Laborator3.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [StringLength(300)]
        public string Content { get; set; }
        public DateTime AddedAt{get; set;}
        [Range(0,5)]
        public int Rating { get; set; }

       public int ToDoTaskId { get; set; }
        public ToDoTask ToDoTask { get; set; }
    }
}
