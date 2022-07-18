using System;

namespace TalentLand.Trivia.Domain.Services
{
    public class TriviaServiceException: Exception
    {
        public TriviaServiceException(string message): base(message) { }

        public TriviaServiceException(string message, Exception innerException) 
            : base(message, innerException) { }
    }
}
