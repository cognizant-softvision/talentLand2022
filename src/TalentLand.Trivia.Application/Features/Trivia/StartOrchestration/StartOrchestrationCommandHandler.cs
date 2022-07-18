using MediatR;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using TalentLand.Trivia.Application.Interfaces.Services;
using TalentLand.Trivia.Application.Wrappers;

namespace TalentLand.Trivia.Application.Features.Trivia
{
    public class StartOrchestrationCommandHandler : IRequestHandler<StartOrchestrationCommand, ApiResponse<StartOrchestrationViewModel>>
    {
        private readonly ITriviaFunctionService _triviaFunctionService;

        public StartOrchestrationCommandHandler(ITriviaFunctionService triviaFunctionService)
        {
            _triviaFunctionService = triviaFunctionService;
        }

        public async Task<ApiResponse<StartOrchestrationViewModel>> Handle(StartOrchestrationCommand startOrchestrationCommand, CancellationToken cancellationToken)
        {
            //invoke start orchestration function
            var orchestrationResult = await _triviaFunctionService.StartOrchestrationAsync(startOrchestrationCommand, cancellationToken);
            if (orchestrationResult == null)
            {
                return new ApiResponse<StartOrchestrationViewModel>(null!);
            }

            var startOrchestrationViewModel = JsonConvert.DeserializeObject<StartOrchestrationViewModel>(orchestrationResult.Id);
            return new ApiResponse<StartOrchestrationViewModel>(startOrchestrationViewModel!);
        }
    }
}
