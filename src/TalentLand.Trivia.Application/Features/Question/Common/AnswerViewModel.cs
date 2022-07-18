using System;

namespace TalentLand.Trivia.Application.Features.Question.Common
{
    public class AnswerViewModel
    {
        public Guid AnswerId { get; set; }
        public string Answer { get; set; } = null!;

        public int Order { get; set; }
    }
}
