using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TalentLand.Trivia.Application.Interfaces.Repositories;
using TalentLand.Trivia.Application.Wrappers;

namespace TalentLand.Trivia.Application.Features.User.GetUserByEmail
{
    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, ApiResponse<GetUserByEmailViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public GetUserByEmailQueryHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<GetUserByEmailViewModel>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email, cancellationToken);

            //no users found
            if (null == user)
            {
                return new ApiResponse<GetUserByEmailViewModel>(new GetUserByEmailViewModel());
            }

            var response = _mapper.Map<GetUserByEmailViewModel>(user);
            return new ApiResponse<GetUserByEmailViewModel>(response);
        }
    }
}
