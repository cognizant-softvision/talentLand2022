using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using TalentLand.Trivia.Application.Features.QA.Commands;
using TalentLand.Trivia.Application.Features.Trivia;
using TalentLand.Trivia.Application.Wrappers;

namespace TalentLand.Trivia.API.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class TriviaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TriviaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("StartOrchestration")]
        public async Task<ActionResult<ApiResponse<StartOrchestrationViewModel>>> StartOrchestration([FromBody] StartOrchestrationCommand startOrchestrationCommand)
        {
            var response = await _mediator.Send(startOrchestrationCommand);
            return Ok(response);
        }

        [HttpPost("ProcessAnswer")]
        public async Task<ActionResult<ApiResponse<CreateUserQAViewModel>>> ProcessAnswer([FromBody] ProcessAnswerCommand processAnswerCommand)
        {
            var response = await _mediator.Send(processAnswerCommand);
            return Ok(response);
        }
    }
}