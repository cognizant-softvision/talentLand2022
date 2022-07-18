using MediatR;
using TalentLand.Trivia.Application.Wrappers;

namespace TalentLand.Trivia.Application.Features.User.Commands
{
    public class CreateUserCommand : IRequest<ApiResponse<CreateUserViewModel>>
    {
        public string Email { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string University { get; set; } = null!;

        public string Company { get; set; } = null!;
    }
}
