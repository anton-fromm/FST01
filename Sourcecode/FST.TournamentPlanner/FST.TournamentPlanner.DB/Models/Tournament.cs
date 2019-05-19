using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace FST.TournamentPlanner.DB.Models
{
    [Table("Tournament", Schema = "tp")]
    public class Tournament : BaseEntity
    {
        public DateTime Start { get; set; }

        public ICollection<PlayArea> PlayAreas { get; set; }

        public ICollection<Match> Matches { get; set; }

        public ICollection<Team> Teams { get; set; }

        public String Description { get; set; }

        public int MatchDuration { get; set; }

        public int TeamCount { get; set; }

        public TournamentState State { get; set; }
    }
}
