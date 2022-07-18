using System.Collections.Generic;

namespace TalentLand.Trivia.Domain
{
    public class Question : BaseEntity
    {
        public string QuestionText { get; set; } = null!;

        public int Order { get; set; }

        public virtual ICollection<Answer> Answers { get; set; } = null!;
    }
}
