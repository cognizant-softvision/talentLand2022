using FluentValidation;

namespace TalentLand.Trivia.Application.Features.QA.Commands
{
    public class CreateUserQAValidator : AbstractValidator<CreateUserQACommand>
    {
        public CreateUserQAValidator()
        {
            RuleFor(query => query.InstanceId).NotEmpty().NotNull();
            RuleFor(query => query.AnswerId).NotEmpty().NotNull();
            RuleFor(query => query.QuestionId).NotEmpty().NotNull();
        }
    }
}
