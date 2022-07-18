using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TalentLand.Trivia.Domain;

namespace TalentLand.Trivia.Application.Interfaces.Repositories
{
    public interface IQuestionRepository : IGenericRepository<Question>
    {
        Task<ICollection<Question>> GetAllPagedQuestionsAsync(int offset, int limit, CancellationToken cancellationToken);
    }
}
