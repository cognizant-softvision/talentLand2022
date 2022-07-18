using FluentValidation;

namespace TalentLand.Trivia.Application.Features.Question.Queries.GetAllQuestions
{
    public class GetAllQuestionsValidator : AbstractValidator<GetAllQuestionsQuery>
    {
        public GetAllQuestionsValidator()
        {
            RuleFor(query => query.Offset).GreaterThanOrEqualTo(0).NotEmpty().NotNull();
            RuleFor(query => query.Limit).GreaterThanOrEqualTo(0).NotEmpty().NotNull();
        }
    }
}
