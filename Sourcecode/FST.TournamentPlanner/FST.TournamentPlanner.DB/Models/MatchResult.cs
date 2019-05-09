using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace FST.TournamentPlanner.DB.Models
{
    [Table("MatchResult", Schema = "tp")]
    public class MatchResult : BaseEntity
    {
        public Match Match { get; set; }
        public Team Team { get; set; }
        public Int32? Score { get; set; }
    }
}
