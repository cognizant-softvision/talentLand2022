using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TalentLand.Trivia.Application.Interfaces.Repositories;
using TalentLand.Trivia.Domain;

namespace TalentLand.Trivia.Infra.Persistence.Repositories
{
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(BaseDbContext baseDbContext) : base(baseDbContext) { }

        public async Task<ICollection<Question>> GetAllPagedQuestionsAsync(int offset, int limit, CancellationToken cancellationToken)
        {
            var questions = base.GetAllPagedQueryable(offset, limit, "Order")
                .Include(qa => qa.Answers);

            return await questions.ToListAsync(cancellationToken);
        }
    }
}
