using System;
using TalentLand.Trivia.Application.Features.Question.Common;

namespace TalentLand.Trivia.Application.Features.Trivia
{
    public class StartOrchestrationViewModel
    {
        public Guid InstanceId { get; set; }

        public Guid UserId { get; set; }

        public QuestionViewModel Question { get; set; } = null!;
    }
}
