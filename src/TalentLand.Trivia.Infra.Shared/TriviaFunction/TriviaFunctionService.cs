using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TalentLand.Trivia.Application.Features.QA.Commands;
using TalentLand.Trivia.Application.Features.Trivia;
using TalentLand.Trivia.Application.Interfaces.Services;
using TalentLand.Trivia.Application.Wrappers;
using TalentLand.Trivia.Domain.Services;

namespace TalentLand.Trivia.Infra.Shared
{
    public class TriviaFunctionService: ITriviaFunctionService
    {
        private readonly IConfiguration _configuration;
        private static HttpClient client = new HttpClient();

        public TriviaFunctionService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<TriviaServiceResponse> StartOrchestrationAsync(
            StartOrchestrationCommand startOrchestrationCommand, 
            CancellationToken cancellationToken)
        {
            var content = new StringContent(JsonConvert.SerializeObject(startOrchestrationCommand), 
                Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(
                $"{_configuration["TriviaFunctionUrl"]}/api/StartOrchestration",
                content, cancellationToken);
            
            if (response.IsSuccessStatusCode)
            {
                var contentJson = await response.Content.ReadAsStringAsync();
                var triviaResponse = JsonConvert.DeserializeObject<TriviaServiceResponse>(contentJson);

                return triviaResponse!;
            }

            return null!;
        }

        public async Task<CreateUserQAViewModel> ProcessAnswerAsync(
            ProcessAnswerCommand processAnswerCommand,
            CancellationToken cancellationToken)
        {
            var content = new StringContent(JsonConvert.SerializeObject(processAnswerCommand),
                 Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(
                $"{_configuration["TriviaFunctionUrl"]}/api/ProcessAnswer?" +
                $"id={processAnswerCommand.InstanceId}" +
                $"&questionId={processAnswerCommand.QuestionId}" +
                $"&answerId={processAnswerCommand.AnswerId}" +
                $"&questionNumber={processAnswerCommand.QuestionNumber}" +
                $"&userId={processAnswerCommand.UserId}",
                content, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var contentJson = await response.Content.ReadAsStringAsync();
                var processAnswerResponse = JsonConvert.DeserializeObject<ApiResponse<CreateUserQAViewModel>>(contentJson);
                if (null != processAnswerResponse)
                {
                    return processAnswerResponse!.Data;
                }                
            }

            return null!;
        }
    }
}
