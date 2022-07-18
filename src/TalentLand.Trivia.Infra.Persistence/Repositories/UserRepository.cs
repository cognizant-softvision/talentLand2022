using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TalentLand.Trivia.Application.Interfaces.Repositories;
using TalentLand.Trivia.Domain;

namespace TalentLand.Trivia.Infra.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public const string ORDER_PROPERTY = "CreationDate";

        public UserRepository(BaseDbContext baseDbContext) : base(baseDbContext) { }

        public async Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var usersQueryable = base.GetQueryable(p => p.Email == email, ORDER_PROPERTY)
               .Include(qa => qa.QAs)
                   .ThenInclude(q => q.Question)
               .Include(qa => qa.QAs)
                   .ThenInclude(a => a.Answer);
            return await usersQueryable.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<ICollection<User>> GetAllPagedUsersAsync(int offset, int limit, CancellationToken cancellationToken)
        {
            var usersQueryable = base.GetAllPagedQueryable(offset, limit, ORDER_PROPERTY)
                .Include(qa => qa.QAs)
                   .ThenInclude(q => q.Question)
               .Include(qa => qa.QAs)
                   .ThenInclude(a => a.Answer);

            return await usersQueryable.ToListAsync(cancellationToken);
        }
    }
}
