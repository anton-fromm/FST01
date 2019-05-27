using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace FST.TournamentPlanner.DB.Models
{
    [DebuggerDisplay("{Name}")]
    [Table("Team", Schema = "tp")]
    public class Team : BaseEntity
    {
        public Tournament Tournamet { get; set; }
        public String Name { get; set; }

    }
}
