using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace FST.TournamentPlanner.DB.Models
{

    [DebuggerDisplay("{Id}")]
    [Table("Match", Schema = "tp")]
    public class Match : BaseEntity
    {
        public MatchResult TeamOne { get; set; }
        public MatchResult TeamTwo { get; set; }
        public PlayAreaBooking PlayAreaBooking { get; set; }
        public Match Successor { get; set; }
        public ICollection<Match> Predecessors { get; set; }
        public MatchState State { get; set; }
    }
}
