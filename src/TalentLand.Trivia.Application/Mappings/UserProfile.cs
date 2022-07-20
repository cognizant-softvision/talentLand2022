using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using TalentLand.Trivia.Application.Features.User.Commands;
using TalentLand.Trivia.Application.Features.User.Common;
using TalentLand.Trivia.Application.Features.User.GetUserByEmail;
using TalentLand.Trivia.Application.Features.User.GetUsers;
using TalentLand.Trivia.Domain;

namespace TalentLand.Trivia.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            //Get User By Email and Ger Users
            CreateMap<ICollection<User>, GetUsersViewModel>()
                .ForMember(d => d.Users, a => a.MapFrom(s => s));

            CreateMap<User, GetUserByEmailViewModel>()
                .ForMember(d => d.User, a => a.MapFrom(s => s));

            CreateMap<User, UserViewModel>()
                .ForMember(d => d.Email, a => a.MapFrom(s => s.Email))
                .ForMember(d => d.Company, a => a.MapFrom(s => s.Company))
                .ForMember(d => d.Name, a => a.MapFrom(s => s.Name))
                .ForMember(d => d.University, a => a.MapFrom(s => s.University))
                .ForMember(d => d.Company, a => a.MapFrom(s => s.Company))
                .ForMember(d => d.QA, a => a.MapFrom(s => s.QAs));

            CreateMap<ICollection<QA>, QAViewModel>()
                .ForMember(d => d.NumberOfRightAnswers, a => a.MapFrom(s => s.Count(p => p.Answer != null && p.Answer!.IsCorrect)))
                .ForMember(d => d.AnsweredDate, a => a.MapFrom(s =>
                (s.OrderByDescending(p => p.Question.Order).FirstOrDefault().CreationDate - s.OrderBy(p => p.Question.Order).FirstOrDefault().CreationDate).TotalSeconds));

            //Command Create User
            CreateMap<CreateUserCommand, User>()
                .ForMember(d => d.Email, a => a.MapFrom(s => s.Email))
                .ForMember(d => d.Name, a => a.MapFrom(s => s.Name))
                .ForMember(d => d.University, a => a.MapFrom(s => s.University))
                .ForMember(d => d.Company, a => a.MapFrom(s => s.Company));

            CreateMap<User, CreateUserViewModel>()
                .ForMember(d => d.UserId, a => a.MapFrom(s => s.Id));

        }
    }
}
