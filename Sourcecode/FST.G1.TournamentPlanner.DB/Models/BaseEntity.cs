using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FST.G1.TournamentPlanner.DB.Models
{
    public class BaseEntity : IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
