using FluentValidation;
using Laborator3.Data;
using Laborator3.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laborator3.Validators
{
    public class ToDoTaskValidator : AbstractValidator<ToDoTaskViewModel>
    {
        private readonly ApplicationDbContext _context;

        public ToDoTaskValidator(ApplicationDbContext context)
        {
            _context = context;
            RuleFor(x => x.Title).Length(3, 40);
            RuleFor(x => x.Description).MaximumLength(100).NotNull();
            RuleFor(x => x.Importance).IsInEnum();
            RuleFor(x => x.Status).IsInEnum();
            RuleFor(x => x.AddedAt).NotEmpty();
            RuleFor(x => x.ClosedAt).GreaterThanOrEqualTo(x => x.AddedAt);
            RuleFor(x => x.DeadLine).GreaterThanOrEqualTo(x => x.AddedAt);
        }
      
    }
}
