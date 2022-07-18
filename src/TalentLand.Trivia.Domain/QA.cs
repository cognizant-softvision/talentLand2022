using System;

namespace TalentLand.Trivia.Domain
{
    public class QA : BaseEntity
    {
        public Guid UserId { get; set; }

        public Guid QuestionId { get; set; }

        public Guid AnswerId { get; set; }

        public virtual Question Question { get; set; } = null!;

        public virtual Answer Answer { get; set; } = null!;
    }
}
