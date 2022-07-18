using FluentValidation;

namespace TalentLand.Trivia.Application.Features.User.GetUsers
{
    public class GetUsersValidator : AbstractValidator<GetUsersQuery>
    {
        public GetUsersValidator()
        {
            RuleFor(query => query.Offset).GreaterThanOrEqualTo(0).NotEmpty().NotNull();
            RuleFor(query => query.Limit).GreaterThanOrEqualTo(0).NotEmpty().NotNull();
        }
    }
}
