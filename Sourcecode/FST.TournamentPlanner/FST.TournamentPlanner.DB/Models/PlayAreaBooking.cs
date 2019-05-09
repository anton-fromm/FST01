using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace FST.TournamentPlanner.DB.Models
{
    [Table("PlayAreaBooking", Schema = "tp")]
    public class PlayAreaBooking : BaseEntity
    {
        public PlayArea PlayArea { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
