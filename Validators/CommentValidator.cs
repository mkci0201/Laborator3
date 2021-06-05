using FluentValidation;
using Laborator3.Data;
using Laborator3.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laborator3.Validators
{
    public class CommentValidator : AbstractValidator<CommentViewModel>
    {
             
            private readonly ApplicationDbContext _context;

            public CommentValidator(ApplicationDbContext context)
            {
                _context = context;
                RuleFor(x => x.Content).Length(3, 300);
                RuleFor(x => x.Rating).InclusiveBetween(1, 5);


            }

        }
    }

