using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using TalentLand.Trivia.Infra.Persistence;

namespace TalentLand.Trivia.UnitTesting.Mocks
{
    public class DbContextTest: ApplicationDbContext
    {
        public DbContextTest() : this(new DbContextOptions<ApplicationDbContext>()) { }

        public DbContextTest(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(CreateInMemoryDatabase()).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            base.OnConfiguring(optionsBuilder);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            foreach(var entity in ChangeTracker.Entries())
            {
                entity.State = EntityState.Detached;
            }

            return result;
        }

        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            return connection;
        }
    }
}
