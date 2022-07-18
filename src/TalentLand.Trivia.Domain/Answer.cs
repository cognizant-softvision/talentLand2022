using System;

namespace TalentLand.Trivia.Domain
{
    public class Answer: BaseEntity
    {
        public Guid QuestionId { get; set; }

        public string AnswerText { get; set; } = null!;

        public int Order { get; set; }

        public bool IsCorrect { get; set; }
    }
}
