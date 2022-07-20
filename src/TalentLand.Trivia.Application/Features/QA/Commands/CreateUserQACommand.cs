using MediatR;
using System;
using TalentLand.Trivia.Application.Wrappers;

namespace TalentLand.Trivia.Application.Features.QA.Commands
{
    public class CreateUserQACommand : IRequest<ApiResponse<CreateUserQAViewModel>>
    {
        public string InstanceId { get; set; } = null!;

        public Guid UserId { get; set; }

        public Guid QuestionId { get; set; }

        public Guid? AnswerId { get; set; }

        public int QuestionNumber { get; set; }
    }
}
