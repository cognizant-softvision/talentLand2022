using Newtonsoft.Json;

namespace TalentLand.Trivia.Domain.Services
{
    public class TriviaServiceResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; } = null!;
    }
}
