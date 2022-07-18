using System;
using System.ComponentModel.DataAnnotations;

namespace TalentLand.Trivia.Domain
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? ModificationDate { get; set; }
    }
}
