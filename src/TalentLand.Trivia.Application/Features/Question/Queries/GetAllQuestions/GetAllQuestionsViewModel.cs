using System.Collections.Generic;
using TalentLand.Trivia.Application.Features.Question.Common;

namespace TalentLand.Trivia.Application.Features.Question.Queries.GetAllQuestions
{
    public class GetAllQuestionsViewModel
    {
        public GetAllQuestionsViewModel()
        {
            Questions = new List<QuestionViewModel>();
        }

        public ICollection<QuestionViewModel> Questions { get; set; }
    }
}
