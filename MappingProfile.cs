using AutoMapper;
using Laborator3.Models;
using Laborator3.ViewModels;
using Laborator3.ViewModels.Assignments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laborator3
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        { 
            CreateMap<ToDoTask, ToDoTaskViewModel>().ReverseMap();
            CreateMap<CommentViewModel, Comment>().ReverseMap();
            CreateMap<ToDoTask, ToDoTaskWithCommentsViewModel>();

            CreateMap<ApplicationUser, ApplicationUserViewModel>().ReverseMap();
            CreateMap<Assignment, AssignmentsForUserResponse>().ReverseMap();
            CreateMap<Assignment, NewAssignment>().ReverseMap();


        }
    }
}
