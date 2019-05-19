using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FST.TournamentPlanner.DB.Models
{
    [Table("Match", Schema = "tp")]
    public class Match : BaseEntity
    {
        [Required]
        public MatchResult TeamOne { get; set; }
        [Required]
        public MatchResult TeamTwo { get; set; }
        [Required]
        public PlayAreaBooking PlayAreaBooking { get; set; }
        public Match Successor { get; set; }
        public ICollection<Match> Predecessors { get; set; }
        public MatchState State { get; set; }
    }
}
