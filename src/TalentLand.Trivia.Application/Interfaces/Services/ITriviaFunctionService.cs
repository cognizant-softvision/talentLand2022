using System.Threading;
using System.Threading.Tasks;
using TalentLand.Trivia.Application.Features.QA.Commands;
using TalentLand.Trivia.Application.Features.Trivia;
using TalentLand.Trivia.Domain.Services;

namespace TalentLand.Trivia.Application.Interfaces.Services
{
    public interface ITriviaFunctionService
    {
        Task<TriviaServiceResponse> StartOrchestrationAsync(
            StartOrchestrationCommand startOrchestrationCommand,
            CancellationToken cancellationToken);

        Task<CreateUserQAViewModel> ProcessAnswerAsync(
            ProcessAnswerCommand processAnswerCommand,
            CancellationToken cancellationToken);
    }
}
