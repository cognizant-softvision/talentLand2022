using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TalentLand.Trivia.Application.Interfaces.Repositories;
using TalentLand.Trivia.Application.Wrappers;

namespace TalentLand.Trivia.Application.Features.User.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApiResponse<CreateUserViewModel>>
    { 
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<CreateUserViewModel>> Handle(CreateUserCommand createUserCommand, CancellationToken cancellationToken)
        {
            //Mapping
            var user = _mapper.Map<Domain.User>(createUserCommand);

            //create user
            var userCreated = await _userRepository.AddAsync(user, cancellationToken);

            var response = _mapper.Map<CreateUserViewModel>(userCreated);

            return new ApiResponse<CreateUserViewModel>(response);

        }
    }
}
