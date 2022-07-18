using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TalentLand.Trivia.Infra.Persistence.Extensions;

namespace TalentLand.Trivia.Infra.Persistence
{
    public class GenericRepository<T> where T : class
    {
        protected readonly BaseDbContext _baseDbContext;
        protected DbSet<T> _entity;

        public GenericRepository(BaseDbContext baseDbContext)
        {
            _baseDbContext = baseDbContext;
            _entity = baseDbContext.Set<T>();
        }

        public virtual async Task<T?> GetByIdAsync(object[] keys, CancellationToken cancellationToken = default)
        {
            var entity = await _entity.FindAsync(keys, cancellationToken);
            if (entity == null) return null;

            return entity;
        }

        public virtual async Task<T?> GetByIdAsync(object id, CancellationToken cancellationToken = default)
            => await GetByIdAsync(new[] { id }, cancellationToken).ConfigureAwait(false);

        private IQueryable<T> GetQueryable() 
        {
            var query = _entity.AsNoTracking();
            return query;
        }

        public virtual IQueryable<T> GetPagedQueryable(int offset, int limit, Expression<Func<T, bool>>? filter, string? orderBy)
        {
            var query = GetQueryable();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (orderBy != null)
            {
                bool desc = false;
                if (orderBy.StartsWith("-")) 
                {
                    desc = true;
                    orderBy = orderBy.Substring(1);
                }
                query = query.OrderBy(orderBy, desc);
            }
            query = query.Skip(offset).Take(limit);

            return query;
        }

        public virtual IQueryable<T> GetAllPagedQueryable(int offset, int limit, string? orderBy)
        {
            var query = GetQueryable();           
            if (orderBy != null)
            {
                bool desc = false;
                if (orderBy.StartsWith("-"))
                {
                    desc = true;
                    orderBy = orderBy.Substring(1);
                }
                query = query.OrderBy(orderBy, desc);
            }
            query = query.Skip(offset).Take(limit);

            return query;
        }

        public virtual IQueryable<T> GetQueryable(Expression<Func<T, bool>>? filter, string? orderBy)
        {
            var query = GetQueryable();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (orderBy != null)
            {
                bool desc = false;
                if (orderBy.StartsWith("-"))
                {
                    desc = true;
                    orderBy = orderBy.Substring(1);
                }
                query = query.OrderBy(orderBy, desc);
            }

            return query;
        }

        public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _baseDbContext.AddAsync(entity, cancellationToken);
            await _baseDbContext.SaveChangesAsync(true, cancellationToken);
            return await Task.FromResult(entity);
        }

        public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _entity.Update(entity);
            await _baseDbContext.SaveChangesAsync(true, cancellationToken);
        }

        public virtual async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            _entity.Remove(entity);
            await _baseDbContext.SaveChangesAsync(true, cancellationToken);
        }
    }
}
