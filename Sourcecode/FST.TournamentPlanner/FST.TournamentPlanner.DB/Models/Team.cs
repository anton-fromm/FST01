using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace FST.TournamentPlanner.DB.Models
{
    [Table("Team", Schema = "tp")]
    public class Team : BaseEntity
    {
        public Tournament Tournamet { get; set; }
        public String Name { get; set; }

    }
}
