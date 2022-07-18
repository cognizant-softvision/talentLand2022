using TalentLand.Trivia.Application.Interfaces.Repositories;
using TalentLand.Trivia.Domain;

namespace TalentLand.Trivia.Infra.Persistence.Repositories
{
    public class QARepository : GenericRepository<QA>, IQARepository
    {
        public QARepository(BaseDbContext baseDbContext) : base(baseDbContext) { }
    }
}
