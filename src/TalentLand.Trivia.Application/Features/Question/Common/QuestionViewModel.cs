using System;
using System.Collections.Generic;

namespace TalentLand.Trivia.Application.Features.Question.Common
{
    public class QuestionViewModel
    {
        public QuestionViewModel()
        {
            Answers = new List<AnswerViewModel>();
        }

        public Guid QuestionId { get; set; }

        public string Question { get; set; } = null!;

        public int Order { get; set; }

        public ICollection<AnswerViewModel> Answers { get; set; }
    }
}
