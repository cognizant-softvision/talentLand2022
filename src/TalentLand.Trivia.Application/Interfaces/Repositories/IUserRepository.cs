using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TalentLand.Trivia.Domain;

namespace TalentLand.Trivia.Application.Interfaces.Repositories
{
    public interface IUserRepository: IGenericRepository<User>
    {
        Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken);

        Task<ICollection<User>> GetAllPagedUsersAsync(int offset, int limit, CancellationToken cancellationToken);
    }
}
