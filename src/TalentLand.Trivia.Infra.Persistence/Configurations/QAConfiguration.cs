using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentLand.Trivia.Domain;

namespace TalentLand.Trivia.Infra.Persistence.Configurations
{
    internal class QAConfiguration : IEntityTypeConfiguration<QA>
    {
        public void Configure(EntityTypeBuilder<QA> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
