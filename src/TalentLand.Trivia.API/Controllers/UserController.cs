using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using TalentLand.Trivia.Application.Features.User.GetUserByEmail;
using TalentLand.Trivia.Application.Features.User.GetUsers;
using TalentLand.Trivia.Application.Wrappers;

namespace TalentLand.Trivia.API.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<ApiResponse<GetUsersViewModel>>> GetAllUsers([FromQuery] GetUsersQuery getUsersQuery)
        {
            var response = await _mediator.Send(getUsersQuery);
            return Ok(response);
        }
    }
}