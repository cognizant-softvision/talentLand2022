using MediatR;
using TalentLand.Trivia.Application.Wrappers;

namespace TalentLand.Trivia.Application.Features.Question.Queries.GetAllQuestions
{
    public class GetAllQuestionsQuery : IRequest<ApiResponse<GetAllQuestionsViewModel>>
    {
        public int? Offset { get; set; }

        public int? Limit { get; set; }
    }
}
