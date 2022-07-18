using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TalentLand.Trivia.Application.Features.QA.Commands;
using TalentLand.Trivia.Application.Interfaces.Services;
using TalentLand.Trivia.Application.Wrappers;

namespace TalentLand.Trivia.Application.Features.Trivia.ProcessAnswer
{
    public class ProcessAnswerCommandHandler : IRequestHandler<ProcessAnswerCommand, ApiResponse<CreateUserQAViewModel>>
    {
        private readonly ITriviaFunctionService _triviaFunctionService;

        public ProcessAnswerCommandHandler(ITriviaFunctionService triviaFunctionService)
        {
            _triviaFunctionService = triviaFunctionService;
        }

        public async Task<ApiResponse<CreateUserQAViewModel>> Handle(ProcessAnswerCommand processAnswerCommand, CancellationToken cancellationToken)
        {
            //invoke start orchestration function
            var processAnswerResult = await _triviaFunctionService.ProcessAnswerAsync(processAnswerCommand, cancellationToken);
            if (processAnswerResult == null)
            {
                return new ApiResponse<CreateUserQAViewModel>(null!);
            }

            return new ApiResponse<CreateUserQAViewModel>(processAnswerResult);
        }
    }
}
