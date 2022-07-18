using TalentLand.Trivia.Application.Features.Question.Common;

namespace TalentLand.Trivia.Application.Features.QA.Commands
{
    public class CreateUserQAViewModel
    {
        public string Message { get; set; } = null!;

        public QuestionViewModel NextQuestion { get; set; } = null!;

        public bool IsLastQuestion { get; set; }
    }
}
