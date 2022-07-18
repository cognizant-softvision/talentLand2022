using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace TalentLand.Trivia.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(object[] keys, CancellationToken cancellationToken = default);

        Task<T?> GetByIdAsync(object id, CancellationToken cancellationToken = default);

        IQueryable<T> GetPagedQueryable(int offset, int limit, Expression<Func<T, bool>>? filter, string? orderBy);

        IQueryable<T> GetAllPagedQueryable(int offset, int limit, string? orderBy);

        IQueryable<T> GetQueryable(Expression<Func<T, bool>>? filter, string? orderBy);

        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
    }
}
