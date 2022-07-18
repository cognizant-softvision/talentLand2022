using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TalentLand.Trivia.Application.Features.QA.Commands;
using TalentLand.Trivia.Application.Features.Question.Queries.GetAllQuestions;
using TalentLand.Trivia.Application.Features.Trivia;
using TalentLand.Trivia.Application.Features.User.Commands;
using TalentLand.Trivia.Domain.Services;

namespace TalentLand.Trivia.Function
{
    public class TriviaFunction
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        private const int NUMBER_OF_QUESTIONS = 5;
        private const int TIMEOUT_IN_SECONDS = 15;

        public TriviaFunction(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Orchestrator Start
        /// </summary>
        /// <param name="req"></param>
        /// <param name="client"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("StartOrchestration")]
        public async Task<HttpResponseMessage> StartOrchestration(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient client,
            ILogger log)
        {
            try
            {
                //create user
                var content = req.Content;
                var createUserCommand = await content.ReadAsAsync<CreateUserCommand>();
                var userCreated = await _mediator.Send(createUserCommand);

                // Function input comes from the request content.
                var instanceId = Guid.NewGuid().ToString().ToLower();
                log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

                //get all questions
                var getAllQuestions = await _mediator.Send(new GetAllQuestionsQuery() { Offset = 0, Limit = NUMBER_OF_QUESTIONS });

                var responseData = new StartOrchestrationViewModel
                {
                    InstanceId = Guid.Parse(instanceId),
                    UserId = userCreated.Data.UserId,
                    Question = getAllQuestions.Data.Questions.FirstOrDefault()
                };

                //start orchestrator
                await client.StartNewAsync("Orchestrator", instanceId);

                return client.CreateCheckStatusResponse(req, JsonConvert.SerializeObject(responseData));
            }
            catch (Exception ex)
            {
                throw new TriviaServiceException("An exception occurred while processing the request", ex);
            }
        }

        /// <summary>
        /// Orchestrator
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [FunctionName("Orchestrator")]
        public async Task Orchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            try
            {
                using (var cts = new CancellationTokenSource())
                {
                    for (int i = 1; i <= NUMBER_OF_QUESTIONS; i++)
                    {
                        DateTime dueTime = context.CurrentUtcDateTime.Add(TimeSpan.FromSeconds(TIMEOUT_IN_SECONDS));
                        Task durableTimeout = context.CreateTimer(dueTime, cts.Token);
                        Task answered = context.WaitForExternalEvent<bool>($"Answered{i}");

                        var taskResult = await Task.WhenAny(answered, durableTimeout);
                        if (taskResult != answered)
                        {
                            cts.Cancel();
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new TriviaServiceException("An exception occurred while processing the request", ex);
            }
        }

        /// <summary>
        /// Process answer
        /// </summary>
        /// <param name="req"></param>
        /// <param name="starter"></param>
        /// <returns></returns>
        [FunctionName("ProcessAnswer")]
        public async Task<IActionResult> ProcessAnswer(
              [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req,
              [DurableClient] IDurableOrchestrationClient starter)
        {
            try
            {
                //Mapping
                var queryParameters = req.Query.ToArray();
                var createUserQACommand = _mapper.Map<CreateUserQACommand>(queryParameters);

                //get the orchestration status
                var status = await starter.GetStatusAsync(createUserQACommand.InstanceId);

                if (status != null && status.RuntimeStatus == OrchestrationRuntimeStatus.Running)
                {
                    //create qa
                    var createdUserQAViewModel = await _mediator.Send(createUserQACommand);

                    await starter.RaiseEventAsync(createUserQACommand.InstanceId.ToString().ToLower(), $"Answered{createUserQACommand.QuestionNumber}", true);
                    return new OkObjectResult(createdUserQAViewModel);
                }
                else
                {
                    throw new TriviaServiceException("Timeout or trivia is already completed");
                }
            }
            catch (Exception ex)
            {
                throw new TriviaServiceException("An exception occurred while processing the request", ex);
            }
        }
    }
}