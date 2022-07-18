using Microsoft.EntityFrameworkCore;
using TalentLand.Trivia.Domain;
using TalentLand.Trivia.Infra.Persistence.Configurations;

namespace TalentLand.Trivia.Infra.Persistence
{
    public class ApplicationDbContext: BaseDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {}

        public virtual DbSet<User>? User { get; set; }

        public virtual DbSet<User>? QA { get; set; }

        public virtual DbSet<User>? Answer { get; set; }

        public virtual DbSet<User>? Question { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionConfiguration());
            modelBuilder.ApplyConfiguration(new AnswerConfiguration());
            modelBuilder.ApplyConfiguration(new QAConfiguration());
        }


    }
}
