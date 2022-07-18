using AutoMapper;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Linq;
using TalentLand.Trivia.Application.Features.QA.Commands;
using TalentLand.Trivia.Domain;

namespace TalentLand.Trivia.Application.Mappings
{
    public class QAProfile : Profile
    {
        public QAProfile()
        {
            //Query collection
            CreateMap<KeyValuePair<string, StringValues>[], CreateUserQACommand>()
                .ForMember(d => d.UserId, a => a.MapFrom(s => s.FirstOrDefault(p => p.Key == "userId").Value.ToString()))
                .ForMember(d => d.InstanceId, a => a.MapFrom(s => s.FirstOrDefault(p => p.Key == "id").Value.ToString()))
                .ForMember(d => d.QuestionId, a => a.MapFrom(s => s.FirstOrDefault(p => p.Key == "questionId").Value.ToString()))
                .ForMember(d => d.AnswerId, a => a.MapFrom(s => s.FirstOrDefault(p => p.Key == "answerId").Value.ToString()))
                .ForMember(d => d.QuestionNumber, a => a.MapFrom(s => s.FirstOrDefault(p => p.Key == "questionNumber").Value.ToString()));

            //Get User By Email and Ger Users
            CreateMap<CreateUserQACommand, QA>()
                .ForMember(d => d.UserId, a => a.MapFrom(s => s.UserId))
                .ForMember(d => d.QuestionId, a => a.MapFrom(s => s.QuestionId))
                .ForMember(d => d.AnswerId, a => a.MapFrom(s => s.AnswerId));
        }
    }
}
