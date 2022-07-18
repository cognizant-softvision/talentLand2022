using AutoMapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TalentLand.Trivia.Application.Interfaces.Repositories;
using TalentLand.Trivia.Application.Wrappers;

namespace TalentLand.Trivia.Application.Features.User.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, ApiResponse<GetUsersViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public GetUsersQueryHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<GetUsersViewModel>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllPagedUsersAsync(request.Offset!.Value, request.Limit!.Value, cancellationToken);
            
            //no users found
            if (null == users || users.Count == 0)
            {
                return new ApiResponse<GetUsersViewModel>(new GetUsersViewModel());
            }

            var response = _mapper.Map<GetUsersViewModel>(users);

            //order
            response.Users = response.Users
                .OrderByDescending(u => u.QA?.NumberOfRightAnswers)
                .ThenBy(u => u.QA?.AnsweredDate).ToList();

            return new ApiResponse<GetUsersViewModel>(response);
        }
    }
}
