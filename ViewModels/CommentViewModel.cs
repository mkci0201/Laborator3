using System;

namespace Laborator3.ViewModels
{

    public class CommentViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }
        public DateTime AddedAt { get; set; }

        public int Rating { get; set; }

    }
}