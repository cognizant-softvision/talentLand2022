using System.Collections.Generic;

namespace TalentLand.Trivia.Domain
{
    public class User : BaseEntity
    {
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string University { get; set; } = null!;

        public string Company { get; set; } = null!;

        public virtual ICollection<QA> QAs { get; set; } = null!;
    }
}
