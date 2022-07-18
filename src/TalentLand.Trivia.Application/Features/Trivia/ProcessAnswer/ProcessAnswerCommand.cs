using MediatR;
using System;
using TalentLand.Trivia.Application.Features.QA.Commands;
using TalentLand.Trivia.Application.Wrappers;

namespace TalentLand.Trivia.Application.Features.Trivia
{
    public class ProcessAnswerCommand : IRequest<ApiResponse<CreateUserQAViewModel>>
    {
        public Guid InstanceId { get; set; }

        public Guid UserId { get; set; }

        public Guid QuestionId { get; set; }

        public Guid AnswerId { get; set; }

        public int QuestionNumber { get; set; }
    }
}
