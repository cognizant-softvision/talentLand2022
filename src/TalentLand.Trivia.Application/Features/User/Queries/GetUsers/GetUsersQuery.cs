using MediatR;
using TalentLand.Trivia.Application.Wrappers;

namespace TalentLand.Trivia.Application.Features.User.GetUsers
{
    public class GetUsersQuery : IRequest<ApiResponse<GetUsersViewModel>>
    {
        public int? Offset { get; set; }

        public int? Limit { get; set; }
    }
}
