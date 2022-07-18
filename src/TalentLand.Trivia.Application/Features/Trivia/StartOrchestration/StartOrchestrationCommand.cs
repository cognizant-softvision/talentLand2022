using MediatR;
using TalentLand.Trivia.Application.Wrappers;

namespace TalentLand.Trivia.Application.Features.Trivia
{
    public class StartOrchestrationCommand : IRequest<ApiResponse<StartOrchestrationViewModel>>
    {
        public string Email { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string University { get; set; } = null!;

        public string Company { get; set; } = null!;
    }
}
