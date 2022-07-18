using AutoMapper;
using System.Collections.Generic;
using TalentLand.Trivia.Application.Features.Question.Common;
using TalentLand.Trivia.Application.Features.Question.Queries.GetAllQuestions;
using TalentLand.Trivia.Domain;

namespace TalentLand.Trivia.Application.Mappings
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            //Get User By Email and Ger Users
            CreateMap<ICollection<Question>, GetAllQuestionsViewModel>()
                .ForMember(d => d.Questions, a => a.MapFrom(s => s));

            CreateMap<Question, QuestionViewModel>()
                .ForMember(d => d.QuestionId, a => a.MapFrom(s => s.Id))
                .ForMember(d => d.Question, a => a.MapFrom(s => s.QuestionText))
                .ForMember(d => d.Order, a => a.MapFrom(s => s.Order))
                .ForMember(d => d.Answers, a => a.MapFrom(s => s.Answers));

            CreateMap<Answer, AnswerViewModel>()
                .ForMember(d => d.AnswerId, a => a.MapFrom(s => s.Id))
                .ForMember(d => d.Order, a => a.MapFrom(s => s.Order))
                .ForMember(d => d.Answer, a => a.MapFrom(s => s.AnswerText));
        }
    }
}
