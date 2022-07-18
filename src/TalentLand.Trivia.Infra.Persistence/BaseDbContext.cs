using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TalentLand.Trivia.Domain;

namespace TalentLand.Trivia.Infra.Persistence
{
    public class BaseDbContext: DbContext
    {
        public BaseDbContext(DbContextOptions options): base(options) { }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            foreach(var entity in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entity.State) 
                {
                    case EntityState.Added:
                        entity.Entity.CreationDate = DateTime.UtcNow;
                        entity.Entity.Id = Guid.NewGuid();
                        break;
                    case EntityState.Modified:
                        entity.Entity.ModificationDate = DateTime.UtcNow;
                        break;
                }
            }

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //All decimals with correct precision
            foreach(var property in modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetPrecision(18);
                property.SetScale(6);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
