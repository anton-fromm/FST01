using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FST.TournamentPlanner.DB.Models
{
    [Table("PlayArea", Schema = "tp")]
    public class PlayArea
    {
        public Tournament Tournament { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
    }
}
