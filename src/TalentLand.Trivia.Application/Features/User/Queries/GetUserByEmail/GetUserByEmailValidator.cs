using FluentValidation;

namespace TalentLand.Trivia.Application.Features.User.GetUserByEmail
{
    public class GetUserByEmailValidator : AbstractValidator<GetUserByEmailQuery>
    {
        public GetUserByEmailValidator()
        {
            RuleFor(query => query.Email).NotEmpty().NotNull();
        }
    }
}
