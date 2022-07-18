using FluentValidation;

namespace TalentLand.Trivia.Application.Features.User.Commands
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(query => query.Email).NotEmpty().NotNull().EmailAddress();
            RuleFor(query => query.University).NotEmpty().NotNull();
            RuleFor(query => query.Name).NotEmpty().NotNull();
        }
    }
}