using MediatR;
using TalentLand.Trivia.Application.Wrappers;

namespace TalentLand.Trivia.Application.Features.User.GetUserByEmail
{
    public class GetUserByEmailQuery : IRequest<ApiResponse<GetUserByEmailViewModel>>
    {
        public string Email { get; set; } = null!;
    }
}
